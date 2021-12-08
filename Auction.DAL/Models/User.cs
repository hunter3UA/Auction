using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auction.DAL.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required,MaxLength(100),MinLength(2)]
        public string FirstName { get; set; }
        [Required,MaxLength(100),MinLength(2)]
        public string LastName { get; set; }    
        [Required,Phone]     
        public string PhoneNumber { get; set; }
        [Required]
        public double Balance { get; set; }
        [Required]
        public DateTime RegistredAt { get; set; }


        [Column("fk_LoginId")]
        public int? LoginId { get; set; }   
        [Required,ForeignKey(nameof(LoginId))]
        public Login Login { get; set; }


        [Column("fk_ShoppingCartId")]
        public int? ShoppingCartId { get; set; }
        [ForeignKey(nameof(ShoppingCartId))]
        public ShopptingCart ShopptingCart { get; set; }

        public virtual List<Lot> Lots { get; set; }

    }
}
