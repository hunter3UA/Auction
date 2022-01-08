using System.ComponentModel.DataAnnotations;

namespace Auction.BLL.ViewModels
{
    public class CreateLotModel
    {
        [Required]
        public string LotName { get; set; }
        [Required(ErrorMessage = "Поле не може бути порожнім"),Range(50,1000000000)]
        public double Price { get; set; }

        [Required(ErrorMessage ="Поле не може бути порожнім"),Range(20,50000000)]
        public double Step { get; set; }

        [Required]
        public int EndAt { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

    }
}
