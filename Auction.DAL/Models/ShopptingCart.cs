using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.DAL.Models
{
    public class ShopptingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }   


        [Column("fk_UserId")]
        public int? UserId { get; set; }
        [Required,ForeignKey(nameof(UserId))]        
        public User User { get; set; }

        [ForeignKey("CartId")]
        public List<Lot> Lots { get; set; }=new List<Lot>();
    }
}
