using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auction.DAL.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly AuctionDbContext _dbContext;
        public UserRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User Add(User userToAdd)
        {
            return _dbContext.Users.Add(userToAdd);
        }    
        public User Get(Func<User, bool> predicate)
        {
            User userToSearch= _dbContext.Users.Include("Login").FirstOrDefault(predicate);      
            return userToSearch!=null ? userToSearch : new User();
        }
        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }
        public User Update(User user)
        {
            _dbContext.Entry(user).State=System.Data.Entity.EntityState.Modified;
            return user;
            
        } 
        /// <summary>
        /// Встановлює поле IsEnabled в значення false
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Delete(Func<User, bool> predicate)
        {
            User user = _dbContext.Users.FirstOrDefault(predicate);
            if (user != null)
            {
                user.IsEnabled = false;
                return true;
            }
            return false;
        }
    }
}
