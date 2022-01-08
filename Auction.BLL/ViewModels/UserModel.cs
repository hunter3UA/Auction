using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class UserModel
    {

        public int LoginId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }      
        public bool IsEnabled { get; set; }
    }
}
