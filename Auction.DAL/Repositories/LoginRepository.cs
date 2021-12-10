using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
//!!! Изменить наследование 
namespace Auction.DAL.Repositories
{
    public class LoginRepository:IGenricRepository<Login>
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

        public bool Delete(Func<Login, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Login Get(Func<Login, bool> predicate)
        {
            return _dbContext.Logins.Include("AccountType").FirstOrDefault(predicate);
        }

        public IEnumerable<Login> GetAll()
        {
            throw new NotImplementedException();
        }

        public Login Update(Login item)
        {
            throw new NotImplementedException();
        }
    }
}
