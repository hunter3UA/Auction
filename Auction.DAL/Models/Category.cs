using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.DAL.Models
{
 
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required,Index("UniqueCategoryName",IsUnique =true),MaxLength(300),MinLength(2)]     
        public string CategoryName { get; set; } 

    }
}
