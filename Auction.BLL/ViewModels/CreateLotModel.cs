using System;
using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class CreateLotModel
    {
        [Required]
        public string LotName { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public double Step { get; set; }

        [Required]
        public DateTime EndAt { get; set; }


        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

    }
}
