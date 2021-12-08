using Auction.BLL.Login;
using System.Web.Mvc;

namespace Auction.API.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}