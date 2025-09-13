using WeChooz.TechAssessment.Application.Interfaces.Repositories;
using WeChooz.TechAssessment.Domain.Entities;

namespace WeChooz.TechAssessment.Persistence.Repositories;

public class TrainerRepositoryAsync(CourseDbContext courseDbContext) : GenericRepositoryAsync<Trainer>(courseDbContext), ITrainerRepositoryAsync;