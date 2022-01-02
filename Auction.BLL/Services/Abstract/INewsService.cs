using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Auction.BLL.Services.Abstract
{
    public interface INewsService
    {
        News GetNews(Func<News, bool> predicate);
        Task<News> CreateNewsAsync(News newsToAdd, HttpRequestBase request);
        List<News> GetListOfNews();
    }
}
