using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
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

        List<LotModel> models;
        public HomeController(ILotService lotService)
        {
            _lotService = lotService;
            models = _lotService.GetAll();
        }


        public ActionResult Index(int page=1)
        {
            int pageSize = 3; 
            IEnumerable<LotModel> lotsPerpages = models.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = models.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Lots = lotsPerpages.ToList() };
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