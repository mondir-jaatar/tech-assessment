using WeChooz.TechAssessment.Application.Interfaces.Services;

namespace WeChooz.TechAssessment.Shared;

public class DateTimeService: IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}