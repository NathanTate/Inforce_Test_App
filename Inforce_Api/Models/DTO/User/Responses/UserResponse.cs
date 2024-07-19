namespace Inforce_Api.Models.DTO.User.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
