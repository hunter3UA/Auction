using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
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



        [HttpPost]
        public ActionResult MyStakes()
        {

            return View();

        }
    }
}