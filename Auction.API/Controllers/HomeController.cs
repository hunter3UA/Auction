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

/*  [HttpGet]
        public ActionResult Index(int page=1, string Filters=null, FiltersModel filtersModel=null)
        {

            FiltersModel FiltersModel=filtersModel;
           
                
            if(!string.IsNullOrEmpty(Filters))
                FiltersModel= JsonConvert.DeserializeObject<FiltersModel>(Filters);
            int pageSize = 3;
            List<LotModel> models = _lotService.GetByFilters(FiltersModel);
            IEnumerable<LotModel> lotsPerpages = models.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = models.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Lots = lotsPerpages.ToList() };
            ivm.FiltersModel = FiltersModel;



            ViewData["Categories"] = _categoryService.GetCategories();

            return View(ivm);

        }*/