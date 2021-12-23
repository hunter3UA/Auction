using Auction.DAL.Models;
using System;
using System.Collections.Generic;

namespace Auction.DAL.Repositories.Abstract
{
    public interface ILotRepository
    {

        IEnumerable<Lot> GetAllBySellerId(int sellerId);
        Lot Add(Lot lotToAdd);
        Lot Get(Func<Lot, bool> predicate);
        IEnumerable<Lot> GetAll();
        Lot Update(Lot lotToUpdate);
        IEnumerable<Lot> GetList(Func<Lot, bool> predicate);

    }
}
