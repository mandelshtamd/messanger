using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTO
{
    public sealed class CredentialsDTO
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Login should contain at least 3 character and not more then 20")]
        public String Login { get; }
        [Required]
        [StringLength(24, MinimumLength = 3, ErrorMessage = "Password should contain at least 3 character and not more then 24")]
        public String Password { get; }

        public CredentialsDTO(String login, String password)
        {
            Login = login;
            Password = password;
        }
    }
}