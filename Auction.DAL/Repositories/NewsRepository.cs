using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;


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

        public bool Remove(int id)
        {
            News newsToRemove=_dbContext.News.FirstOrDefault(n=>n.NewsId == id);
            if(newsToRemove != null)
            {
                _dbContext.News.Remove(newsToRemove);
                return true;
            }
            return false;
            
        }

        public News Get(Func<News, bool> predicate)
        {
            return _dbContext.News.Include("Pictures").FirstOrDefault(predicate);
        }

        public List<News> GetList()
        {
            return _dbContext.News.ToList();
        }
    }
}
