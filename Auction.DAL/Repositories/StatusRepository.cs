using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class StatusRepository:IStatusRepository
    {
        private readonly AuctionDbContext _dbContext;

        public StatusRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public LotStatus Get(Func<LotStatus, bool> predicate)
        {
            return _dbContext.LotStatuses.FirstOrDefault(predicate);
        }


        public List<LotStatus> GetList()
        {
            return _dbContext.LotStatuses.ToList();
        }


    }
}
