using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
    public class NewsService:INewsService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPictureService _pictureService;

        public NewsService(IUnitOfWork unitOfWork, IPictureService pictureService)
        {

            _unitOfWork = unitOfWork;
            _pictureService = pictureService;
        }
        public async Task<News> CreateNewsAsync(News newsToAdd,HttpRequestBase request)
        {
           
             _unitOfWork.NewsRepository.Add(newsToAdd);
             await _unitOfWork.SaveAsync();  
             return newsToAdd;
                  
        }
        public News GetNews(Func<News,bool> predicate)
        {
            News newsToView = _unitOfWork.NewsRepository.Get(predicate);
            if(newsToView==null)
                return new News();
            return newsToView;

        }   
        public List<News> GetListOfNews()
        {
            List<News> news = _unitOfWork.NewsRepository.GetList();
            return news;
            
        }
    }
}