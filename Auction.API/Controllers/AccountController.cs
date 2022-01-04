using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;


//TODO: Notification  of UpdatedProfiles


// TODO: убрать лишнее из accountService
namespace Auction.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;
        public AccountController(IAccountService accountService,IAuthenticationService authenticationService,IEmailService emailService)
        {
            _accountService = accountService;
            _authenticationService = authenticationService;
            _emailService = emailService;
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
            await _accountService.UpdateUserAsync(updateUserModel,User.LoginId);
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
                User newUser= await _authenticationService.CreateUserAsync(registerModel);
                if (newUser.UserId == 0)
                {
                    ViewBag.IncorrectEmail = "Користувач з таким логіном вже існує";
                    return View(registerModel);
                }
                _emailService.SendPasswordConfirmed(registerModel.Email);
                ViewBag.Msg = "Вам надіслано лист з підтвердженням";
                return View("Login");
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
                Login isAuthenticated = _authenticationService.Login(loginModel);
                if (isAuthenticated.LoginId==0)
                {
                    ViewBag.UserNotFound = "Користувача не знайдено";
                    return View(loginModel);
                }
                if(isAuthenticated!=null && isAuthenticated.IsConfirmed == false)
                {
                    ViewBag.Msg = "Підтвердіть реєстрацію";
                    return View(loginModel);
                }
                _authenticationService.SetLoginCookie(isAuthenticated);
                return RedirectToAction("Profile", "Account");
            }
            return View(loginModel);
        }


        [HttpGet]
        public ActionResult UpdatePassword()
        {
            return View();
        }


        [HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePassword(string oldPassword,string newPassword)
        {
            bool isUpdated=await _accountService.UpdatePasswordAsync(oldPassword,newPassword,User.LoginId);
            if (isUpdated)
            {
                LoginModel loginModel=new LoginModel() { Email = User.Identity.Name, Password = newPassword };
                FormsAuthentication.SignOut();
                _authenticationService.Login(loginModel);
                ViewBag.Msg = "Пароль оновлено";
                return View("Profile");
            }
            ViewBag.Msg = "Пароль невірний";
            return View("Profile");
        }



        [Authentication(false)]
        public async Task<ActionResult> ConfirmRegistration(string Email,string Token)
        {
        
            bool isConfirmed=  await _authenticationService.ConfirmAccount(Email,Token);
            if (isConfirmed==true)
            {
                ViewBag.Msg = "Email підтверджено";  
            }
            return View("Login");
        }


        public ActionResult ResetPassword()
        {
            return null;
        }


        //[HttpPost,Authentication(false)]
        //public async Task<ActionResult> ResetPassword()
        //{
           

        //    return null;
        //}

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}

