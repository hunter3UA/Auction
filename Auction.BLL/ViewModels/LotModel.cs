using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Auction.BLL.ViewModels
{
    public class LotModel
    {
        public int LotId { get; set; }
        [Required]
        public string LotName { get; set; }
        [Required]
        public double Price { get; set; }

        public double CurrentPrice { get; set; }

        [Required]
        public double Step { get; set; }


        [Required]
        public string Description { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime? SoldAt { get; set; }
        public int CategoryId { get; set; }
        public bool IsSoldOut { get; set; }
        public User Seller { get; set; }
        public int LoginId { get; set; }
        public Category Category { get; set; }
        public LotStatus Status { get; set; }
        public long LotCode { get; set; }
        public List<Picture> Pictures { get; set; }

        public List<Stake> Stakes { get; set; }


        public int? CartId { get; set; }
    }
}
