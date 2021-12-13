using Auction.DAL.Models;
using System.Collections.Generic;

namespace Auction.DAL.Repositories.Abstract
{
    public interface ILotRepository:IGenricRepository<Lot>
    {

        List<Lot> GetAllBySellerId(int sellerId);

    }
}
