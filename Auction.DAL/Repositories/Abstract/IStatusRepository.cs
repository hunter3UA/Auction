using Auction.DAL.Models;
using System;
using System.Collections.Generic;

namespace Auction.DAL.Repositories.Abstract
{
    public interface IStatusRepository
    {
        LotStatus Get(Func<LotStatus, bool> predicate);
        List<LotStatus> GetList();
    }
}
