﻿using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class CreateLotModel
    {
        [Required]
        public string LotName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

    }
}