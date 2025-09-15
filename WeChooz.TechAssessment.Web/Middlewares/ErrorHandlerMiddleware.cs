using System.Net;
using System.Text.Json;
using TCLab.BuildingBlocks.Application.Wrappers;
using WeChooz.TechAssessment.Application.Exceptions;
using WeChooz.TechAssessment.Application.Wrappers;

namespace WeChooz.TechAssessment.Web.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string> { Succeeded = false, Message = $"{error.Message}{Environment.NewLine}{error.InnerException?.Message}" };

            switch (error)
            {
                case NotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.LogInformation(error, error.Message);
                    break;
                case ApiException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    logger.LogWarning(e, error.Message);
                    break;
                case ValidationException e:
                {
                    logger.LogInformation(e, e.Message);

                    var validationResponse = new ValidationErrorResponse { Succeeded = false, Message = error.Message };
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    validationResponse.Data = e.Errors;
                    var validationResult = JsonSerializer.Serialize(validationResponse);
                    await response.WriteAsync(validationResult);
                    return;
                }
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    logger.LogInformation(e, e.Message);
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    logger.LogError(error, responseModel.Message);
                    break;
            }

            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}