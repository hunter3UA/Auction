using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Models
{
    public class News
    {
        [Key]
        public long NewsId { get; set; }
        [Required]
        public string NewsTittle { get; set; }
        [Required]
        public string NewsText { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.Now;


        [ForeignKey("NewsId")]
        public List<Picture> Pictures { get; set; }=new List<Picture>();

    }
}
