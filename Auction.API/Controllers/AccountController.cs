using Auction.BLL.ViewModels;
using System.Web.Mvc;

namespace Auction.API.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            return null;
        }



    }
}