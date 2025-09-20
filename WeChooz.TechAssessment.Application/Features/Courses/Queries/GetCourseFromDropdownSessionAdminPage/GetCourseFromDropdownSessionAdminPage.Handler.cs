using MediatR;
using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Application.Wrappers;

namespace WeChooz.TechAssessment.Application.Features.Courses.Queries.GetCourseFromDropdownSessionAdminPage;

public class GetCourseFromDropdownSessionAdminPageHandler(ICourseRepositoryAsync courseRepository) : IRequestHandler<GetCourseFromDropdownSessionAdminPageQuery, Response<IEnumerable<CourseFromDropdownSessionAdminPageDto>>>
{
    public async Task<Response<IEnumerable<CourseFromDropdownSessionAdminPageDto>>> Handle(GetCourseFromDropdownSessionAdminPageQuery request, CancellationToken cancellationToken)
    {
        var specification = new GetCourseFromDropdownSessionAdminPageSpecification();
        var entities = await courseRepository.GetBySpecificationAsync(specification, cancellationToken);

        return new(entities);
    }
}