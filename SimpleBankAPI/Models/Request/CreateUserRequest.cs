using System.ComponentModel.DataAnnotations;

namespace SimpleBankAPI.Models.Request
{
    public class CreateUserRequest
    {
        [Required, EmailAddress]
        public string email { get; set; }
        [Required, MinLength(16)]
        public string FullName { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }

        [Required, MinLength(8)]
        public string UserName { get; set; }
        public static User RequestToUser(CreateUserRequest request)
        {
            var user = new User()
            {
                Username = request.UserName,
                Email = request.email,
                Password = request.Password,
                FullName = request.FullName
            };
            return user;
        }


    }
}
