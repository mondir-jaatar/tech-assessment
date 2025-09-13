using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence.Repositories;

public class CourseRepositoryAsync(CourseDbContext context) : GenericRepositoryAsync<Course>(context), ICourseRepositoryAsync;