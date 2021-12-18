using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
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

        [HttpGet, Authentication(true)]
        public new ActionResult Profile()
        {
            UserModel model = _accountService.GetUser(User.LoginId);
            return View(model);
        }
        [HttpPost]
        public new async Task<ActionResult> Profile(UserModel updateUserModel)
        {
            updateUserModel.Email=User.Identity.Name;
            await _accountService.Update(updateUserModel,User.LoginId);
            return View(updateUserModel);
        }


        [HttpGet,Authentication(false)]
        public ActionResult Register()
        {  
            return View();
        }    
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User newUser= await _accountService.CreateUser(registerModel);
                if (newUser.UserId == 0)
                {
                    ViewBag.IncorrectEmail = "Користувач з таким логіном вже існує";
                    return View(registerModel);
                }
                return RedirectToAction("Index", "Home");
            }         
            return View(registerModel);
        }

        [HttpGet,Authentication(false)]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                User userToLogin= _accountService.Login(loginModel);
                if (userToLogin.UserId == 0)
                {
                    ViewBag.UserNotFound = "Користувача не знайдено";
                    return View(loginModel);
                }
                return RedirectToAction("Profile", "Account");

            }
            return View(loginModel);
        }

      
        [HttpGet,MyAuth("Admin")]
        public ActionResult Admin()
        {
            return View();
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}