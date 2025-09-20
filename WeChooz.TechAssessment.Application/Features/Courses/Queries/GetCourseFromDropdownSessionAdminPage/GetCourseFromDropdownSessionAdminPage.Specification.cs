using Ardalis.Specification;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetCourseFromDropdownSessionAdminPage;

public class GetCourseFromDropdownSessionAdminPageSpecification: Specification<Course, CourseFromDropdownSessionAdminPageDto>
{
    public GetCourseFromDropdownSessionAdminPageSpecification()
    {
        Query.Include(c => c.Trainer);
        
        Query.Select(c => new()
        {
            Id = c.Id,
            Name = c.Name,
            Trainer = new()
            {
                FirstName = c.Trainer.FirstName,
                LastName = c.Trainer.LastName,
            }
        });
    }
}