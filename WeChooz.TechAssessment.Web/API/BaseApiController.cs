using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WeChooz.TechAssessment.Web.API;

[ApiController]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] TODO
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator? _mediator;
    public IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}