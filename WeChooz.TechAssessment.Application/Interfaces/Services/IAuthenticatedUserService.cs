namespace WeChooz.TechAssessment.Application.Interfaces.Services;

public interface IAuthenticatedUserService
{
    public Guid? UserId { get; set; }
}