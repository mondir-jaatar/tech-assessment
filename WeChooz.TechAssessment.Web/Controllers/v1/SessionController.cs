using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateParticipantsCommand;
using WeChooz.TechAssessment.Application.Features.Sessions.Commands.UpdateSessionCommand;
using WeChooz.TechAssessment.Application.Features.Sessions.Queries.GetSessionsFromAdminListingPage;
using WeChooz.TechAssessment.Application.Features.Sessions.Queries.GetSessionsFromPublicListingPage;

namespace WeChooz.TechAssessment.Web.Controllers.v1;

[ApiVersion("1.0")]
public class SessionController : BaseApiController
{
    [AllowAnonymous]
    [HttpGet("get-from-public-listing-page")]
    public async Task<IActionResult> GetFromPublicListingPageAsync([FromQuery] GetSessionsFromPublicListingPageQuery query) => Ok(await Mediator.Send(query));

    [HttpGet("get-from-admin-listing-page")]
    public async Task<IActionResult> GetFromAdminListingPageAsync([FromQuery] GetSessionsFromAdminListingPageQuery query) => Ok(await Mediator.Send(query));

    [Authorize(Policy = "Formation")]
    [HttpPut]
    public async Task<IActionResult> Put(UpdateSessionCommand query) => Ok(await Mediator.Send(query));

    [Authorize(Policy = "Formation")]
    [Authorize(Policy = "Sales")]
    [HttpPut("participants")]
    public async Task<IActionResult> UpdateParticipants([FromBody] UpdateParticipantsCommand command) => Ok(await Mediator.Send(command));
}