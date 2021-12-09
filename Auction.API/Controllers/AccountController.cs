using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.UoW;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace Auction.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet]
        public ActionResult Register()
        {  
            return View();
        }    
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                await _accountService.CreateUser(registerModel);
                return RedirectToAction("Index", "Home");
            }         
            return View(registerModel);
        }
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                _accountService.Login(loginModel);
                return RedirectToAction("Index", "Home");

            }
            return View(loginModel);
        }

      
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}