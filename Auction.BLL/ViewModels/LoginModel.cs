using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
