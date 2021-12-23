using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
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
        [HttpPost,MyAuth("Admin")]
        public async Task<ActionResult> CreateNews(News news)
        {
            await _newsService.CreateNews(news, Request);
            return RedirectToAction("PageNews", new { newsId = news.NewsId });
        }

        [HttpGet]
        public ActionResult GetNews(int page=1)
        {
            IndexViewModel<News> ivm = _newsService.GetPageOfNews(page);
            return View(ivm);
        }


        [HttpGet]
        public ActionResult NewsPartial(int page = 1)
        {
            IndexViewModel<News> ivm = _newsService.GetPageOfNews(page);
            return View(ivm);

        }


        [HttpGet]
        public ActionResult PageNews(int newsId = 0)
        {
            News newsToView = _newsService.GetNews(n => n.NewsId == newsId);

            return View(newsToView);
        }
    }
}


/* [HttpGet]
        public ActionResult Index(int page=1, string Filters=null, FiltersModel filtersModel=null)
        {
            IndexViewModel<LotModel> ivm = _lotService.GetPageOfLots(page,Filters,filtersModel);
            ViewData["Categories"] = _categoryService.GetCategories();
            return View(ivm);

        }


       
        public ActionResult Pages(int page = 0, string Filters = null, FiltersModel filtersModel = null)
        {
            if (page == 0)
                return RedirectToAction("Index");
            IndexViewModel<LotModel> ivm = _lotService.GetPageOfLots(page, Filters, filtersModel);
            return PartialView("Pages",ivm);
        }
*/