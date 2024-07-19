using System.ComponentModel.DataAnnotations;

namespace Inforce_Api.Models.DTO.ShortenUrl.Responses
{
    public class ShortenUrlResponse
    {
        public int Id { get; set; }

        public string LongUrl { get; set; } = string.Empty;

        public string ShortUrl { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
