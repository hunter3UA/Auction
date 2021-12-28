using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using System.Web.Mvc;


namespace Auction.API.Controllers
{

    [MyAuth("Admin")]
    public class AdminController : BaseController
    {
 
        private readonly IAccountService _accountService;
        private readonly IStatusService _statusService;

        public AdminController(IAccountService accountService, IStatusService statusService)
        {
            _accountService = accountService;
            _statusService = statusService;
        }
        [HttpGet]
        public ActionResult AdminPanel()
        {
            return View();
        }
        
        public ActionResult SearchUser()
        {
            return PartialView();
        }

        public ActionResult DisableUser()
        {
         
            return PartialView();
        }



        [HttpPost]
        public ActionResult DisableUser(int loginIdToDisable)
        {
            _accountService.DisableUserAsync(loginIdToDisable);
            return null;
        }

        [ValidateAntiForgeryToken]
        public ActionResult GetUserInfo(string userToSearch)
        {
            UserModel searchedUser= _accountService.GetUser(u=>u.Login.Email==userToSearch);
            return PartialView(searchedUser);
        }


        

    }
}