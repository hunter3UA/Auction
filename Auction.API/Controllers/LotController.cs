using Auction.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.API.Controllers
{
    public class LotController : BaseController
    {

        private readonly ILotService _lotService;
        public LotController(ILotService lotService)
        {
            _lotService = lotService;
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        
    }
}