using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.DAL.Models
{
    public class Login
    {
        [Key]
        public int LoginId { get; set; }
        [Required,EmailAddress(ErrorMessage ="Невірний email"),Index("UniqueEmail",IsUnique =true),MaxLength(200)]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }   
        [Required]
        public bool IsEnabled { get; set; }
       
        
        [Column("fk_AccountTypeId")]
        public int? AccountTypeId { get; set; }
        [Required]
        public AccountType AccountType { get; set; }

       
      


    }
}
