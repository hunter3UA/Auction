using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;


//TODO: Notification  of UpdatedProfiles

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
            UserModel model = _accountService.GetUser(u=>u.LoginId==User.LoginId);
            return View(model);
        }
        [HttpPost]
        public new async Task<ActionResult> Profile(UserModel updateUserModel)
        {
            updateUserModel.Email=User.Identity.Name;
            await _accountService.UpdateAsync(updateUserModel,User.LoginId);
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
                User newUser= await _accountService.CreateUserAsync(registerModel);
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
                bool isAuthenticated= _accountService.Login(loginModel);
                if (!isAuthenticated)
                {
                    ViewBag.UserNotFound = "Користувача не знайдено";
                    return View(loginModel);
                }
                return RedirectToAction("Profile", "Account");
            }
            return View(loginModel);
        }

            
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePassword(string oldPassword,string newPassword)
        {
            bool isUpdated=await _accountService.UpdatePasswordAsync(oldPassword,newPassword,User.LoginId);
            if (isUpdated)
            {
                LoginModel loginModel=new LoginModel() { Email = User.Identity.Name, Password = newPassword };
                FormsAuthentication.SignOut();
                _accountService.Login(loginModel);
                ViewBag.Msg = "Пароль оновлено";
                return View("Profile");
            }
            ViewBag.Msg = "Пароль невірний";
            return View("Profile");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}