using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class LoginModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Невірний пароль")]
        public string Password { get; set; }
    }
}
