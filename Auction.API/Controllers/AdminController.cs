using Auction.API.Filters;
using Auction.BLL.Services;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public PartialViewResult EnableUser()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult DisableUser()
        {
            return PartialView();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableUser(int id=0,bool enable=true)
        {
            bool isEnabled = await _accountService.EnableUserAsync(l=>l.LoginId==id,enable);
            if (isEnabled)
                ViewBag.NotifyMsg = "Дані користувача оновлено";
            return View("AdminPanel");
        }

        [ValidateAntiForgeryToken]
        public ActionResult GetUserInfo(string userToSearch)
        {
            UserModel searchedUser= _accountService.GetUser( u=>u.Login.Email==userToSearch);
            if (searchedUser.LoginId == 0)
                searchedUser = _accountService.GetUser(u => u.LoginId == Convert.ToInt32(userToSearch));
            return PartialView(searchedUser);
        }
        [HttpGet]
        public ActionResult LotsByStatus(int page = 1,FiltersModel filtersModel=null,string Filters=null)
        {   
          
      
            filtersModel =string.IsNullOrEmpty(Filters) ? filtersModel :  JsonConvert.DeserializeObject<FiltersModel>(Filters);
            List<LotModel> lots = _lotService.GetByFilters(filtersModel);
            lots.Reverse();
            IndexViewModel<LotModel> ivm = PageService<LotModel>.GetPage(
                page,
                Convert.ToInt32(ConfigurationManager.AppSettings["CountOfLots"]),
                lots
                );
            ivm.FiltersModel = filtersModel;
            int selectId= filtersModel!=null ? Convert.ToInt32(filtersModel.Status) : 1;                
            ViewData["Statuses"] = new SelectList(_statusService.GetList(), "LotStatusId", "LotStatusName", selectId);              
            return View(ivm);
        }

        public async Task<ActionResult> UpdateLotStatus(int statusId,int lotId)
        {
            await _lotService.UpdateLotStatusAsync(lotId,statusId);
            return RedirectToAction("LotsByStatus");
        }

    }
}