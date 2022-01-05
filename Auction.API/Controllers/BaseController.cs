using Auction.BLL.LoginModels;
using System.Web.Mvc;

namespace Auction.API.Controllers
{
    public class BaseController : Controller
    {
        
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}