using MediatR;
using WeChooz.TechAssessment.Application.Wrappers;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetCourseFromDropdownSessionAdminPage;

public class GetCourseFromDropdownSessionAdminPageQuery: IRequest<Response<IEnumerable<CourseFromDropdownSessionAdminPageDto>>>
{
}

public record CourseFromDropdownSessionAdminPageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TrainerFromDropdownSessionAdminPageDto Trainer { get; set; }
}

public record TrainerFromDropdownSessionAdminPageDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}