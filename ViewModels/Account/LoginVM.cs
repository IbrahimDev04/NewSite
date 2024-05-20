using System.ComponentModel.DataAnnotations;

namespace GameApp.ViewModels.Account
{
    public class LoginVM
    {
        [Required, MinLength(6, ErrorMessage = "Lenght Error")]
        public string UsernameOrEmail { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
