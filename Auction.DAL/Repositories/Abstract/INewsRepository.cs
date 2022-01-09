using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Abstract
{
    public interface INewsRepository
    {

        News Add(News newsToAdd);
        List<News> GetList();


        News Get(Func<News,bool> predicate);
    }
}
