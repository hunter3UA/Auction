using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
