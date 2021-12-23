using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Auction.API.Controllers
{

    [MyAuth("Admin")]
    public class AdminController : BaseController
    {
        //private readonly INewsService _newsService;


        //public AdminController(INewsService newsService)
        //{
        //    _newsService = newsService;


        //}
        //[HttpGet]
        //public ActionResult CreateNews()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<ActionResult> CreateNews(News news)
        //{
        //    await _newsService.CreateNews(news,Request);
        //    return null;


        //}
        
        //[HttpGet]
        //public ActionResult GetNews()
        //{
        //    return null;
        //}



    }
}