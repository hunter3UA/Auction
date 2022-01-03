using Auction.BLL.Services;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

/*

TODO: Cart Update
TODO: проверить все токены


TODO: добавить сохранение listbox в фильтрах


 */

namespace Auction.API.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILotService _lotService;
        private readonly ICategoryService _categoryService;
        private readonly IEmailService _emailService;

        public HomeController(ILotService lotService, ICategoryService categoryService,IEmailService emailService)
        {
            _lotService = lotService;
            _categoryService = categoryService;
            _emailService = emailService;
        }


        public ActionResult Index(int page = 1, string Filters = null, FiltersModel filtersModel = null)
        {
          
           

            var filters = string.IsNullOrEmpty(Filters) ? filtersModel : JsonConvert.DeserializeObject<FiltersModel>(Filters);
            List<LotModel> lotModels = _lotService.GetByFilters(filters);       
            IndexViewModel<LotModel> ivm = PageService<LotModel>.
                GetPage(
                page,
                3,
                lotModels.Where(l=>l.Status.LotStatusName=="Permitted").ToList()
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