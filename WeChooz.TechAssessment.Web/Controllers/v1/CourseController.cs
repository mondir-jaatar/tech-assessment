using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WeChooz.TechAssessment.Application.Features.Courses.Queries.GetCourseFromDropdownSessionAdminPage;

namespace WeChooz.TechAssessment.Web.Controllers.v1;

[ApiVersion("1.0")]
public class CourseController : BaseApiController
{
    [HttpGet("get-from-dropdown-session-admin-page")]
    public async Task<IActionResult> GetFromDropdownSessionAdminPageAsync([FromQuery] GetCourseFromDropdownSessionAdminPageQuery query) => Ok(await Mediator.Send(query));
}