using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Models
{
    public class Subscription
    {
        [Key]
        public int SubcriptionId { get; set; }
        [Required]
        public int fk_LotId { get; set; }
        [Required]
        public int fk_UserId { get; set; }



    }
}
