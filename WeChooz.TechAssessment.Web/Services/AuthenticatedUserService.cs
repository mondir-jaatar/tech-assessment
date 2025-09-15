using System.Security.Claims;
using WeChooz.TechAssessment.Application.Interfaces.Services;

namespace WeChooz.TechAssessment.Web.Services;

public class AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUserService
{
    public Guid? UserId => GetUserId(httpContextAccessor.HttpContext?.User);

    private Guid? GetUserId(ClaimsPrincipal? user)
    {
        if (user == null || !user.Identity.IsAuthenticated)
        {
            return null;
        }

        // var userIdClaim = user.FindFirst(JwtClaimTypes.Id)?.Value;
        var userIdClaim = "";

        if (Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        return null;
    }
}