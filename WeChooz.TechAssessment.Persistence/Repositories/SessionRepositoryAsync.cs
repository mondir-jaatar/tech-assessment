using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence.Repositories;

public class SessionRepositoryAsync(CourseDbContext dbContext) : GenericRepositoryAsync<Session>(dbContext), ISessionRepositoryAsync;