using System.ComponentModel.DataAnnotations;

namespace Blazor.Data.Models
{
    public class CreateUser
    {


        public string Email { get; set; }


        public string UserName { get; set; }


        public string Password { get; set; }


        public string FullName { get; set; }

    }
}
