using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.ViewModels
{
    public class LotModel
    {


        public int LotId { get; set; }
        public string LotName { get; set; }
       
        public double Price { get; set; }
        
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public bool IsSoldOut { get; set; }

        public User Seller { get; set; }

        public Category Category { get; set; }

        public long LotCode { get; set; }
    }
}
/*  [Key]
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
        [Required]
        public long LotCode { get; set; }      
        public DateTime? SoldAt { get; set; }
         
        [Column("fk_SellerId")]
        public int? SellerId { get; set; }
        [Required,ForeignKey(nameof(SellerId))]    
        public User Seller { get; set; }

        [Column("fk_CategoryId")]
        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }*/