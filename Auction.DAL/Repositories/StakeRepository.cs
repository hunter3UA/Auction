using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Auction.DAL.Repositories
{
    public class StakeRepository : IStakeRepository
    {
        private readonly AuctionDbContext _dbContext;


        public StakeRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Stake Add(Stake stakeToAdd)
        {
            return _dbContext.Stakes.Add(stakeToAdd);
        }


        public List<Stake> GetList(Func<Stake, bool> predicate)
        {
            return _dbContext.Stakes.Include("Lot").Where(predicate).ToList();
        }

        public Stake Get(Func<Stake, bool> predicate)
        {
            return _dbContext.Stakes.FirstOrDefault(predicate);
        }


        public bool RemoveStake(Stake stakeToRemove)
        {
            if (stakeToRemove != null)
            {
                _dbContext.Stakes.Remove(stakeToRemove);
                return true;
            }
            return false;
        }


        public bool RemoveRangeStake(List<Stake> stakesToRemove)
        {
            if (stakesToRemove != null && stakesToRemove.Capacity > 0)
            {
                _dbContext.Stakes.RemoveRange(stakesToRemove);
                return true;
            }
            return false;
        }
        public void SetStakeAsMain(int lotId,long stakeId)
        {
            SqlParameter lotParam=new SqlParameter("@LotId",lotId);
            SqlParameter stakeParam = new SqlParameter("@StakeId", stakeId);
            _dbContext.Database.ExecuteSqlCommand("exec stp_Stake_SetMain @StakeId, @LotId", stakeParam, lotParam);
        }


    }
}


