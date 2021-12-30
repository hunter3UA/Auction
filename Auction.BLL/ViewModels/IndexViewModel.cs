using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.ViewModels
{
    public class IndexViewModel<T> where T : class
    {
        public List<T> Collection { get; set; }
        public PageInfo PageInfo { get; set; }

        public FiltersModel FiltersModel { get; set; }=new FiltersModel();
    }
}
