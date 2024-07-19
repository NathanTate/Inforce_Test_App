using Inforce_Api.Models;

namespace Inforce_Api.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
