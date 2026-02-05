using System.ComponentModel.DataAnnotations;

namespace ProductApi.Entities.Dtos.Auth
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
