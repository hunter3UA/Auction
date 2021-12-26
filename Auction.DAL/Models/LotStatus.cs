using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
