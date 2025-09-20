using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromAdminListingPage;
using WeChooz.TechAssessment.Application.Features.Courses.Queries.GetSessionsFromPublicListingPage;

namespace WeChooz.TechAssessment.Web.Controllers.v1;

[ApiVersion("1.0")]
public class SessionController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("get-from-public-listing-page")]
    public async Task<IActionResult> GetFromPublicListingPageAsync([FromQuery] GetSessionsFromPublicListingPageQuery query) => Ok(await Mediator.Send(query));
    
    [HttpGet("get-from-admin-listing-page")]
    public async Task<IActionResult> GetFromAdminListingPageAsync([FromQuery] GetSessionsFromAdminListingPageQuery query) => Ok(await Mediator.Send(query));
}