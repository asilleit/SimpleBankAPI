using System.ComponentModel.DataAnnotations;

namespace SimpleBankAPI.Models.Request
{
    public class LoginUserRequest
    {
        [Required, MaxLength(20), MinLength(5)]
        public string Username { get; set; }
        [Required, MinLength(6), MaxLength(20)]
        public string Password { get; set; }


        public static User UsertoLoginRequest(LoginUserRequest request)
        {
            var user = new User()
            {
                Username = request.Username,
                Password = request.Password

            };
            return user;
        }
    }


}
