using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private AuctionDbContext _dbContext;

        public CartRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ShopptingCart Get(Func<ShopptingCart,bool> predicate)
        {
            return _dbContext.ShoppingCarts.FirstOrDefault(predicate);
        }
        public ShopptingCart Add(ShopptingCart cartToAdd)
        {
            return _dbContext.ShoppingCarts.Add(cartToAdd);
        }

    }
}
