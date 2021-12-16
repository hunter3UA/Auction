using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.ViewModels
{
    public class IndexViewModel
    {
        public List<LotModel> Lots { get; set; }
        public PageInfo PageInfo { get; set; }

        public FiltersModel FiltersModel { get; set; }
    }
}
