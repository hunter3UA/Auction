using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Abstract
{
    public interface IStakeRepository
    {

        Stake Add(Stake stakeToAdd);
        Stake Get(Func<Stake, bool> predicate);
        List<Stake> GetList(Func<Stake, bool> predicate);
        bool RemoveStake(Stake stakeToRemove);
        bool RemoveRangeStake(List<Stake> stakesToRemove);
        void SetStakeAsMain(int lotId, long stakeId);
    }
}
