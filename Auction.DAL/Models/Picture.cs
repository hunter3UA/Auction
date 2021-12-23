using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Models
{
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public bool IsTittle { get; set; }


        public int? LotId { get; set; }

        [Column("fk_NewsId")]
        public long? NewsId { get; set; }
       
    }
}
