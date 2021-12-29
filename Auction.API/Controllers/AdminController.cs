using Auction.API.Filters;
using Auction.BLL.Services;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Auction.API.Controllers
{

    [MyAuth("Admin")]
    public class AdminController : BaseController
    {
 
        private readonly IAccountService _accountService;
        private readonly IStatusService _statusService;
        private readonly ILotService _lotService;

        public AdminController(IAccountService accountService, IStatusService statusService,ILotService lotService)
        {
            _accountService = accountService;
            _statusService = statusService;
            _lotService = lotService;
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
        [HttpGet]
        public ActionResult DisableUser()
        {
            return PartialView();
        }


        
        public async Task<ActionResult> RemoveUser(int id=0)
        {
            bool isEnabled = await _accountService.DisableUserAsync(id);
            if (isEnabled)
                ViewBag.NotifyMsg = "Користувача заблоковано";
            return View("AdminPanel");
        }

        [ValidateAntiForgeryToken]
        public ActionResult GetUserInfo(string userToSearch)
        {
            UserModel searchedUser= _accountService.GetUser(u=>u.Login.Email==userToSearch);
            return PartialView(searchedUser);
        }

        //public Task<ActionResult> ShowProcessedLots(int page=1)
        //{
        //    List<LotModel> processedLots = _lotService.GetList(l => l.Status.LotStatusName == "Processed");
       

        //}



    }
}