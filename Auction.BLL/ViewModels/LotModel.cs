using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.ViewModels
{
    public class LotModel
    {

     
        [Required]
        public string LotName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        
       
      



    }
}
