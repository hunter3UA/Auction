using Auction.DAL.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class CreateLotModel
    {
        [Required]
        public string LotName { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім")]
        public double Price { get; set; }

        [Required(ErrorMessage ="Поле не може бути порожнім")]
        public double Step { get; set; }

        [Required]
        public int EndAt { get; set; }


        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

    }
}
