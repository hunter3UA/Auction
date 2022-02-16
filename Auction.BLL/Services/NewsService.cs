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
        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<News> CreateNewsAsync(News newsToAdd,HttpRequestBase request)
        {
            try
            {
                _unitOfWork.NewsRepository.Add(newsToAdd);
                await _unitOfWork.SaveAsync();
                return newsToAdd;
            } catch { return new News(); }
           
        }
        public News GetNews(Func<News,bool> predicate)
        {
            try
            {
                News newsToView = _unitOfWork.NewsRepository.Get(predicate);
                return newsToView != null ? newsToView : new News();
            }catch { return new News(); }

        }   
        public List<News> GetListOfNews()
        {
            try
            {
                List<News> news = _unitOfWork.NewsRepository.GetList();
                news.Reverse();
                return news != null ? news : new List<News>();
            }catch { return new List<News>(); } 
            
        }
        public async Task<bool> RemoveNews(int newsId)
        {
            bool isDeleted =   _unitOfWork.NewsRepository.Remove(newsId);
            await _unitOfWork.SaveAsync();
            return isDeleted;
        }
    }
}