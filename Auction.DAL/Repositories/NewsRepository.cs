using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class NewsRepository:INewsRepository
    {
        private readonly AuctionDbContext _dbContext;

        public NewsRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public News Add(News newsToAdd)
        {
            return _dbContext.News.Add(newsToAdd);
        }



        public List<News> GetList()
        {
            return _dbContext.News.ToList();
        }
    }
}
