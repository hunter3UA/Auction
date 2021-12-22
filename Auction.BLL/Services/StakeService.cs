﻿using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
    public class StakeService:IStakeService
    {

        private readonly IUnitOfWork _unitOfWork;

        public StakeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Stake> AddStake(int lotId, double stake,int loginId)
        {
            Stake stakeToAdd = new Stake();
            Lot lotOfStake=_unitOfWork.LotRepository.Get(l=>l.LotId==lotId);
            stakeToAdd.CreatedAt = DateTime.Now;
            stakeToAdd.UserId = _unitOfWork.UserRepository.Get(u => u.LoginId == loginId).UserId;
            stakeToAdd.Sum = stake;
            stakeToAdd.LotId = lotId; 
            lotOfStake.Stakes.Add(stakeToAdd);
            lotOfStake.Price = stake;
            await _unitOfWork.SaveAsync();        
            return stakeToAdd;
        }


        public IndexViewModel<Stake> GetPageOfStakes(int page, int loginId)
        {

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["CountOfStakesOnPage"]);
            User userOfStakes = _unitOfWork.UserRepository.Get(u => u.LoginId == loginId);
            List<Stake> stakes = _unitOfWork.StakeRepository.GetList(s => s.UserId == userOfStakes.UserId);
            stakes.Reverse();
            PageInfo pageInfo=new PageInfo { PageNumber=page,PageSize=pageSize, TotalItems=stakes.Count};
            stakes=stakes.Skip((page-1)*pageSize).Take(pageSize).ToList();   
            IndexViewModel<Stake> ivm = new IndexViewModel<Stake> { PageInfo = pageInfo, Collection = stakes };
            return ivm;
        }


    }
}