using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auction.DAL.Repositories
{
    public class LotRepository:ILotRepository
    {

        private readonly AuctionDbContext _dbContext;
        public LotRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Lot Add(Lot lotToAdd)
        {
           return  _dbContext.Lots.Add(lotToAdd);
        }

        public bool Delete(Func<Lot, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Lot Get(Func<Lot, bool> predicate)
        {
            return _dbContext.Lots.FirstOrDefault(predicate);
        }

        public IEnumerable<Lot> GetAll()
        {
            return _dbContext.Lots.ToList();
        }

        public Lot Update(Lot lotToUpdate)
        {
            _dbContext.Entry(lotToUpdate).State = System.Data.Entity.EntityState.Modified;
            return lotToUpdate;
        }
    }
}
