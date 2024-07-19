namespace Inforce_Api.Models.DTO.User.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
