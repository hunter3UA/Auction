using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AuctionDbContext _dbContext;
        public CategoryRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category Get(Func<Category,bool> predicate)
        {
            return _dbContext.Categories.FirstOrDefault(predicate);
        }

    }
}
