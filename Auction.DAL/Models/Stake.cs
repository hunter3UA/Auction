using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Models
{
    public class Stake
    {
        [Key]
        public long StakeId { get; set; }

        [Required,Column("fk_LotId")]
        public int LotId { get; set; }    
        [Required,Column("fk_UserId")]
        public int UserId { get; set; } 
        [Required]
        public double Sum { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
