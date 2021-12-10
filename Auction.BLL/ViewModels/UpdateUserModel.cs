using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class UpdateUserModel
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }



    }
}
