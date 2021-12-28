using Auction.DAL.Models;
using System;
using System.Collections.Generic;

namespace Auction.BLL.Services.Abstract
{
    public interface IStatusService
    {
        LotStatus GetLotStatus(Func<LotStatus, bool> predicate);
        List<LotStatus> GetList();

    }
}
