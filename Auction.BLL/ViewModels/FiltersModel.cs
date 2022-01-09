using System.Collections.Generic;

namespace Auction.BLL.ViewModels
{
    public class FiltersModel
    {

        public List<string> Categories { get;set; }=new List<string>();

        public string LotName { get; set; }

        public string Date { get; set; }

        public string Order { get; set; }
        public string Criterion { get; set; }

        public string Status { get; set; }

        public string LotCode { get; set; }


    }
}
