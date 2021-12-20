using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.API.Controllers
{
    public class AdminController : BaseController
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddCategories()
        {


            return null;

        }
    }
}