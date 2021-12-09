using Auction.DAL.Models;
using System;

namespace Auction.DAL.Repositories.Abstract
{
    public interface IAccountTypeRepository
    {
        AccountType Get(Func<AccountType, bool> predicate);
    }
}
