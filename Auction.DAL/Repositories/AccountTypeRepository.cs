using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Linq;

namespace Auction.DAL.Repositories
{
    public class AccountTypeRepository:IAccountTypeRepository
    {
        private readonly AuctionDbContext _dbContext;
        public AccountTypeRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AccountType Get(Func<AccountType, bool> predicate)
        {
            return _dbContext.AccountTypes.FirstOrDefault(predicate);
        }
    }
}
