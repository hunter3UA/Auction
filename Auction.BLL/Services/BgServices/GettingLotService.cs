using Auction.DAL.Models;
using Auction.DAL.UoW;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Auction.BLL.Services.BgServices
{
    public class GettingLotService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            IUnitOfWork _unitOfWork = new UnitOfWork();
            List<Lot> lots = _unitOfWork.LotRepository.GetList(l => !l.IsSoldOut && l.EndAt < DateTime.Now).ToList();
            foreach(var lot in lots)
            {
                List<Stake> stakesOfLot = _unitOfWork.StakeRepository.GetList(s=>s.LotId == lot.LotId).ToList();
                if (stakesOfLot != null && stakesOfLot.Count()>0)
                {

                    double maxStakeSum = stakesOfLot.Max(s => s.Sum);
                    var maxStake = stakesOfLot.FirstOrDefault(s => s.Sum == maxStakeSum);                
                    ShopptingCart cartOfUser=_unitOfWork.CartRepository.Get(c=>c.UserId==maxStake.UserId);
                    if(cartOfUser == null)
                    {
                        cartOfUser=new ShopptingCart();
                        cartOfUser.User=_unitOfWork.UserRepository.Get(u=>u.UserId==maxStake.UserId);   
                        cartOfUser.UserId=maxStake.UserId;
                        _unitOfWork.CartRepository.Add(cartOfUser);
                    }                   
                    lot.IsSoldOut = true; 
                    lot.SoldAt = DateTime.Now;
                    lot.BuyerId = maxStake.UserId;
                    _unitOfWork.LotRepository.Update(lot);
                    cartOfUser.Lots.Add(lot);
                    await _unitOfWork.SaveAsync();
                }

            }
        }
    }
}