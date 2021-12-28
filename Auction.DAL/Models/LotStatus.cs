using System.ComponentModel.DataAnnotations;

namespace Auction.DAL.Models
{
    public class LotStatus
    {
        [Key]
        public int LotStatusId { get; set; }
        [Required]
        public string LotStatusName { get; set; }
    }
}
