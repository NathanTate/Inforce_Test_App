using Microsoft.AspNetCore.Identity;

namespace Inforce_Api.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public List<ShortenUrl> ShortenUrls { get; set; } = new();
    }
}
