using System.Security.Claims;

namespace Inforce_Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
