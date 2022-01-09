using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
    public static class PageService<T> where T : class
    {
        /// <summary>
        /// Метод для отримання сторінки 
        /// </summary>
        /// <param name="page">Номер сторінки</param>
        /// <param name="pageSize">Кількість елементів на сторінці</param>
        /// <param name="pageList">Коллекція яка буде розподілена по стороінкам</param>
        /// <returns></returns>
        public static IndexViewModel<T> GetPage(int page,int pageSize, ICollection<T> pageList)
        {
            IEnumerable<T> pageItems = pageList.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = pageList.Count };
            IndexViewModel<T> ivm = new IndexViewModel<T> { PageInfo = pageInfo, Collection = pageItems.ToList() };
            return ivm;
        }
    }
}
