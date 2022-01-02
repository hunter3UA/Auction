using System.ComponentModel.DataAnnotations;

namespace Auction.DAL.Models
{
    public class SupportForm
    {

        [Key]
        public int SupportFormId { get; set; }

        [Required]
        public string Topic { get; set; }


        public User User { get; set; }

        public int LoginId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
