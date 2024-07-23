using System.ComponentModel.DataAnnotations;

namespace Tazkarti.Models.AuthModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public String UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
