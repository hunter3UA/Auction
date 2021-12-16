using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Auction.API.Controllers
{
    [Authentication(true)]
    public class LotController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILotService _lotService;
        public LotController(ICategoryService catetoryService,ILotService lotService)
        {

            _categoryService = catetoryService;
            _lotService = lotService;
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            ViewData["Categories"] = _categoryService.GetCategories();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateLotModel lotModel)
        {

            var files=Request.Files;
            await _lotService.CreateLot(lotModel,User.LoginId);
            return RedirectToAction("Profile","Account");
        }


        [HttpGet]
        public ActionResult BySeller()
        {
            return View(_lotService.GetLotsBySellerId(User.LoginId));
        }


        [HttpGet]
        public ActionResult LotPage(int lotId)
        {
            LotModel lotModel =  _lotService.GetLot(lotId);
            return View(lotModel);
        }


    }
}