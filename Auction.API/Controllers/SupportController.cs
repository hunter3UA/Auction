using System.Web.Mvc;

namespace Auction.API.Controllers
{
    public class SupportController : BaseController
    {
        // GET: Support
        public ActionResult Index()
        {
            return View();
        }
    }
}