using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }  
        [Required]
        public string LastName { get; set; }    
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,DataType(DataType.Password), MinLength(8)]
        public string Password { get; set; }
        [Required,Compare("Password")]
        public string PasswordRepeat { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
