using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Auction.API.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILotService _lotService;
        private readonly ICategoryService _categoryService;

      
        public HomeController(ILotService lotService,ICategoryService categoryService)
        {
            _lotService = lotService;
            _categoryService = categoryService;
         
        }


        [HttpGet]
        public ActionResult Index(int page=1, string Filters=null, FiltersModel filtersModel=null)
        {
            IndexViewModel ivm = _lotService.GetPageOfLots(page,Filters,filtersModel);
            ViewData["Categories"] = _categoryService.GetCategories();
            return View(ivm);

        }


       
        public ActionResult Pages(int page = 0, string Filters = null, FiltersModel filtersModel = null)
        {
            if (page == 0)
                return RedirectToAction("Index");
            IndexViewModel ivm = _lotService.GetPageOfLots(page, Filters, filtersModel);
            return PartialView("Pages",ivm);
        }

        [HttpGet]
        public ActionResult SubscriptionsPage(int page=1)
        {
            return null;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
    }
}
