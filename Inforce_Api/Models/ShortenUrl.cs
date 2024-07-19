using System.ComponentModel.DataAnnotations;

namespace Inforce_Api.Models
{
    public class ShortenUrl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string LongUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ShortUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Code { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        [Required]
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
