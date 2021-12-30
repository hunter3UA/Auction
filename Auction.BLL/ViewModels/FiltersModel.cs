using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.ViewModels
{
    public class FiltersModel
    {

        public List<string> Categories { get;set; }=new List<string>();

        public string LotName { get; set; }

        public string Date { get; set; }

        public string Order { get; set; }
        public string Criterion { get; set; }


    }
}
