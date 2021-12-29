using System;
using System.Collections.Generic;
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

        public double CurrentPrice { get; set; }
        [Required]
        public double Step { get; set; }

        [Required]
        public string Description { get; set; }

        


        [Required]
        public bool IsSoldOut { get; set; }        
        [Required]
        public DateTime CreatedAt { get; set; } 

        [Required]
        public DateTime EndAt { get; set; }

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
        public Category Category { get; set; }


        [Column("fk_LotStatusId")]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        public LotStatus Status { get; set; }
        public List<Picture> Pictures { get; set; }     
        public List<Stake> Stakes { get; set; }
        [Column("fk_CartId")]
        public int? CartId { get; set; }


    }
}
