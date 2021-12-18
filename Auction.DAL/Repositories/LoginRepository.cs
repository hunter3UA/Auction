using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auction.DAL.Repositories
{
    public class LoginRepository:ILoginRepository
    {
        private readonly AuctionDbContext _dbContext;
        public LoginRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Login Add(Login loginToAdd)
        {
            
            return _dbContext.Logins.Add(loginToAdd); 
        }    
        public Login Get(Func<Login, bool> predicate)
        {
            return _dbContext.Logins.Include("AccountType").FirstOrDefault(predicate);
        }

        public bool Delete(Func<Login,bool> predicate)
        {
            Login loginToDelete = _dbContext.Logins.FirstOrDefault(predicate);
            if (loginToDelete != null)
            {
                loginToDelete.IsEnabled = false;
                return true;
            }
            return false;
        }

    }
}
