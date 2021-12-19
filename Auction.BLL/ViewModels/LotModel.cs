﻿using Auction.DAL.Models;
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
        [Required]
        public string LotName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public bool IsSoldOut { get; set; }

        public User Seller { get; set; }

        public Category Category { get; set; }

        public long LotCode { get; set; }

        public List<Picture> Pictures { get; set; }
    }
}