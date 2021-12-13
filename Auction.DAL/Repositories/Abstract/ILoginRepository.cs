using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Abstract
{
    public interface ILoginRepository
    {
        Login Add(Login loginToAdd);
        Login Get(Func<Login, bool> predicate);

    }
}
