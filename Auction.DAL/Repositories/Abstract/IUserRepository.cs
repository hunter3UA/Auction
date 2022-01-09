using Auction.DAL.Models;
using System;
using System.Collections.Generic;

namespace Auction.DAL.Repositories.Abstract
{
    public interface IUserRepository
    {

        User Add(User userToAdd);
        User Get(Func<User, bool> predicate);
        IEnumerable<User> GetAll();
        User Update(User user);
        bool Delete(Func<User, bool> predicate);
    }
}
