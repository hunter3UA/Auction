using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Models
{
    public class Notification
    {

        [Key]
        public int NotificationId { get; set; }
        [Column("fk_LoginId")]
        public int LoginId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
