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

        Task<Stake> AddStake(int lotId, double stake, int loginId);
    }
}
