using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//TODO: добавить отображение картинок на странице новостей
namespace Auction.API.Controllers
{

    [MyAuth("Admin")]
    public class AdminController : BaseController
    {
        private readonly INewsService _newsService;


        public AdminController(INewsService newsService)
        {
            _newsService = newsService;


        }
        [HttpGet]
        public ActionResult ManageUser()
        {
            return View();
        }
        

        [HttpGet]
        public ActionResult DisableUser(int loginId)
        {
            return null;
        }

        public ActionResult GetLotsToConfirmed(int page=1)
        {


            return null;

        }
        

    }
}