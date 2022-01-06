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
            if (model.LoginId == 0)
                return RedirectToAction("Index", "Home");
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
                User newUser =  await _authenticationService.CreateUserAsync(registerModel);
                if (newUser.UserId == 0)
                {
                    ViewBag.IncorrectEmail = "Користувач з таким логіном вже існує";
                    return View(registerModel);
                }
                bool isSent = _emailService.SendPasswordConfirmed(registerModel.Email,"Підтвердження пароля","ConfirmRegistration");
                ViewBag.Msg = isSent==true ? "Вам надіслано лист з підтвердженням" : "Виникла помилка під час реєстрації";
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


        [HttpPost,Authentication(true),ValidateAntiForgeryToken]
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
            if (isConfirmed)
            {
                ViewBag.Msg = "Email підтверджено";  
            }
            return View("Login");
        }

        [HttpGet,Authentication(false)]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [Authentication(false)]
        public ActionResult SendResetToken(string Email)
        {
            bool isSent = _emailService.SendResetPasswordKey(Email);          
            ViewBag.Msg =isSent ?  "Вам на пошту надіслано листа для скидання пароля": "Сталася помилка";
            return View("ResetPassword");
        }


        [HttpPost,Authentication(false)]
        public async Task<ActionResult> ResetPassword(string Email,string Token,string Password)
        {
            
            bool isReset = await _accountService.ResetPassword(Email,Token,Password);
            if(isReset==true)
            {
                ViewBag.Msg = "Пароль оновлено";
                return View("Login");
            }
            return View("Login");
        }

        [Authentication(false)]
        public PartialViewResult ForgotPassword()
        {
            return PartialView();
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}

