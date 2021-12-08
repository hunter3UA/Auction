using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.DAL.Models
{
    public class Lot
    {
        [Key]
        public int LotId { get; set; }
        [Required,MinLength(2),MaxLength(500)]     
        public string LotName { get; set; }
        [Required]
        public double Price { get; set; } 
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsSoldOut { get; set; }        
        [Required]
        public DateTime CreatedAt { get; set; } 
      
        public DateTime SoldAt { get; set; }


        
        [Column("fk_SellerId")]
        public int? SellerId { get; set; }
        [Required,ForeignKey(nameof(SellerId))]    
        public User Seller { get; set; }

        [Column("fk_CategoryId")]
        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

      
        
    }
}
