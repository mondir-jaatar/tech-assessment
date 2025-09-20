using System.Security.Claims;

namespace WeChooz.TechAssessment.Web.Authentication;

public static class ClaimExtensions
{
    public static ClaimDto ToDto(this Claim claim) => new ClaimDto(claim.Type, claim.Value);

    public static ClaimDto[] ToDtos(this IEnumerable<Claim> claims) => claims.Select(c => c.ToDto()).ToArray();
}