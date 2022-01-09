using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Abstract
{
    public interface ICartRepository
    {
        ShopptingCart Get(Func<ShopptingCart, bool> predicate);
        ShopptingCart Add(ShopptingCart cartToAdd);
    }
}
