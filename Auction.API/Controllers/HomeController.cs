using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/*

TODO: Cart Update
TODO: проверить все токены

TODO: добавить приставку async асинхронным методам
TODO: убрать возможность редактирования некоторых полей товара

TODO:добавить уведомление о установки картинки в качестве главной,
добавить удаление картинки
 */

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
            IndexViewModel<LotModel> ivm = _lotService.GetPageOfLots(page,Filters,filtersModel);
            ViewData["Categories"] = _categoryService.GetCategories();
            return View(ivm);

        }


       
        public ActionResult Pages(int page = 0, string Filters = null, FiltersModel filtersModel = null)
        {
            if (page == 0)
                return RedirectToAction("Index");
            IndexViewModel<LotModel> ivm = _lotService.GetPageOfLots(page, Filters, filtersModel);
            return PartialView("Pages",ivm);
        }

       


        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
    }
}
