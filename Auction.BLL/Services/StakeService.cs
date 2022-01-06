﻿using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
    public class StakeService:IStakeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILotService _lotService;


        public StakeService(IUnitOfWork unitOfWork,ILotService lotService)
        {     
            _unitOfWork = unitOfWork; 
            _lotService = lotService;
        }
        public async Task<Stake> AddStakeAsync(int lotId, double stake,int loginId)
        {
            try
            {
                Stake stakeToAdd = new Stake();
                Lot lotOfStake = _unitOfWork.LotRepository.Get(l => l.LotId == lotId);
                stakeToAdd.CreatedAt = DateTime.Now;
                stakeToAdd.UserId = _unitOfWork.UserRepository.Get(u => u.LoginId == loginId).UserId;
                stakeToAdd.Sum = stake;
                stakeToAdd.LotId = lotId;
                lotOfStake.Stakes.Add(stakeToAdd);
                lotOfStake.CurrentPrice = stake + lotOfStake.Step;
                _unitOfWork.LotRepository.Update(lotOfStake);
                await _unitOfWork.SaveAsync();
                return stakeToAdd;
            }catch{ return new Stake(); }
        }
        public List<Stake> GetListOfStakes(int loginId)
        {
            try
            {
                User userOfStakes = _unitOfWork.UserRepository.Get(u => u.LoginId == loginId);
                List<Stake> stakesByUser = _unitOfWork.StakeRepository.GetList(s => s.UserId == userOfStakes.UserId && !s.IsRemoved);
                stakesByUser.Reverse();
                return stakesByUser;
            }catch { return new List<Stake>(); }
            
        }

        public async Task<bool> RemoveStakeAsync(int stakeId)
        {
            try
            {
                Stake stakeToRemove = _unitOfWork.StakeRepository.Get(s => s.StakeId == stakeId);
                if (stakeToRemove != null)
                {
                    Lot lotOfStake = _unitOfWork.LotRepository.Get(l => l.LotId == stakeToRemove.LotId);
                    if (!lotOfStake.IsSoldOut)
                    {
                       // _unitOfWork.StakeRepository.RemoveStake(stakeToRemove);
                        stakeToRemove.IsRemoved = true;
                        await _unitOfWork.SaveAsync();
                        List<Stake> stakesOfLot = _unitOfWork.StakeRepository.GetList(s => s.LotId == lotOfStake.LotId);
                        if (stakesOfLot.Count() > 0)
                        {
                            double maxStake = stakesOfLot.Max(s => s.Sum);
                            lotOfStake.CurrentPrice = maxStake + lotOfStake.Step;
                        }
                        else
                        {
                            lotOfStake.CurrentPrice = lotOfStake.Price + lotOfStake.Step;
                        }

                        await _unitOfWork.SaveAsync();
                        return true;
                    }
                }
                return false;
            }catch { return false; }
            
        }
    }
}
