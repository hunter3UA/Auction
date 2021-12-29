using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface IStakeService
    {

        Task<Stake> AddStakeAsync(int lotId, double stake, int loginId);
        List<Stake> GetListOfStakes(int loginId);
        Task<bool> RemoveStakeAsync(int stakeId);
    }
}
