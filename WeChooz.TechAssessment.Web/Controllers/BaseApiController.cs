using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeChooz.TechAssessment.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Cookies")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator? _mediator;
    public IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}