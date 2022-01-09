using Auction.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface IPageService<T> where T : class
    {

        IndexViewModel<T> GetPage(int page, int pageSize, ICollection<T> pageList);
    }
}
