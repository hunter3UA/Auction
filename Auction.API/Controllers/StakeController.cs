﻿using Auction.API.Filters;
using Auction.BLL.Services;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

/*TODO: сделать ограничение на добавление отрицательных чисел и чисел меншей ставки и уведомление об создании ставки*/
namespace Auction.API.Controllers
{
    [Authentication(true)]
    public class StakeController : BaseController
    {
        private readonly IStakeService _stakeService;
        public StakeController(IStakeService stakeService)
        {
            _stakeService = stakeService;
        }


        [HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> AddStake(int lotId=0, double stake=0)
        {
            await _stakeService.AddStake(lotId, stake, User.LoginId);
            ViewBag.Success = "Ставка зроблена";  
            return RedirectToAction("LotPage","Lot",new { lotId = lotId});
        }


        
        [HttpGet]
        public ActionResult MyStakes(int page=1)
        {
            List<Stake> stakes = _stakeService.GetListOfStakes(User.LoginId);
            IndexViewModel<Stake> ivm = PageService<Stake>.GetPage(
                page,
                Convert.ToInt32(ConfigurationManager.AppSettings["CountOfStakesOnPage"]),
                stakes
                );
            return View(ivm);

        }


        public ActionResult StakePage(int page=0)
        {
            if (page <= 1)
                return RedirectToAction("MyStakes");
            List<Stake> stakes = _stakeService.GetListOfStakes(User.LoginId);
            IndexViewModel<Stake> ivm = PageService<Stake>.GetPage(
                page,
                Convert.ToInt32(ConfigurationManager.AppSettings["CountOfStakesOnPage"]),
                stakes
                );
            return PartialView(ivm);
      
        }
    }
}

/*
 *   IndexViewModel<Stake> ivm = _stakeService.GetPageOfStakes(page, User.LoginId);
            return View(ivm);
 * 
 * 
 * 
 * List<Stake> stakes = _stakeService.GetListOfStakes( User.LoginId);
            IndexViewModel<Stake> ivm = PageService<Stake>.GetPage()
            return View(ivm);


 
 */