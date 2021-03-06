using Auction.BLL.Services;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Auction.API.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILotService _lotService;
        private readonly ICategoryService _categoryService;
   

        public HomeController(ILotService lotService, ICategoryService categoryService)
        {
            _lotService = lotService;
            _categoryService = categoryService;       
        }


        public ActionResult Index(int page = 1, string Filters = null, FiltersModel filtersModel = null)
        {           
                var filters = string.IsNullOrEmpty(Filters) ? filtersModel : JsonConvert.DeserializeObject<FiltersModel>(Filters);
                List<LotModel> lotModels = _lotService.GetByFilters(filters);
                IndexViewModel<LotModel> ivm = PageService<LotModel>.
                    GetPage(
                    page,
                    3,
                    lotModels.Where(l => l.Status.LotStatusName == "Permitted").ToList()
                    );
                ivm.FiltersModel = filters;
                ViewData["Categories"] = _categoryService.GetCategories();
                return View(ivm);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

    }
}