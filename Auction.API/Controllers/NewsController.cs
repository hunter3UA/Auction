using Auction.API.Filters;
using Auction.BLL.Services;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Auction.API.Controllers
{
    public class NewsController : BaseController
    {

        private readonly INewsService _newsService;


        public NewsController(INewsService newsService)
        {
            _newsService = newsService;


        }
        [HttpGet,MyAuth("Admin")]
        public ActionResult CreateNews()
        {
            return View();
        }

        [HttpPost,MyAuth("Admin")]
        public async Task<ActionResult> CreateNews(News news)
        {
            await _newsService.CreateNewsAsync(news, Request);
            return RedirectToAction("PageNews", new { newsId = news.NewsId });
        }

        [HttpGet]
        public ActionResult GetNews(int page=1)
        {

            List<News> news = _newsService.GetListOfNews();
            IndexViewModel<News> ivm = PageService<News>.GetPage(
                page,
                Convert.ToInt32(ConfigurationManager.AppSettings["CountOfNewsOnPage"]),
                news
                );
            ivm.Collection.Reverse();
            return View(ivm);
        }


 
        public PartialViewResult NewsPartial(int page = 1)
        {
            List<News> news = _newsService.GetListOfNews();
            IndexViewModel<News> ivm = PageService<News>.GetPage(
                page,
                Convert.ToInt32(ConfigurationManager.AppSettings["CountOfNewsOnPage"]),
                news
                );
            ivm.Collection.Reverse();
            return PartialView(ivm);

        }


        [HttpGet]
        public ActionResult PageNews(int newsId = 0)
        {
            News newsToView = _newsService.GetNews(n => n.NewsId == newsId);

            return View(newsToView);
        }
    }
}


