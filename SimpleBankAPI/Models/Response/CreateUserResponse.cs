using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SimpleBankAPI.Models.Response
{
    public class CreateUserResponse
    {
        [Required, DefaultValue("1")]
        public int UserId { get; set; }
        [Required, DefaultValue("string")]
        public DateTime CreatedAt { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required, DefaultValue("string")]
        public DateTime PasswordChangedAt { get; set; }
        [Required]
        public string Username { get; set; }

        public static CreateUserResponse ToCreateUserResponse(User user)
        {
            var createUserResponse = new CreateUserResponse
            {
                UserId = user.Id,
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                FullName = user.FullName,
                PasswordChangedAt = DateTime.Today,
                Username = user.Username,

            };
            return createUserResponse;
        }

    }
}
