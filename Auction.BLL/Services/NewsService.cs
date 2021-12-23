using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Auction.BLL.ViewModels;

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



        public async Task<News> CreateNews(News newsToAdd,HttpRequestBase request)
        {
            try
            {
                _unitOfWork.NewsRepository.Add(newsToAdd);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex) { }          
            await AddNewsPictures(request,newsToAdd);
            return newsToAdd;

        }

        public News GetNews(Func<News,bool> predicate)
        {
            News newsToView = _unitOfWork.NewsRepository.Get(predicate);
            if(newsToView==null)
                return new News();
            return newsToView;

        }



        public async Task<List<Picture>> AddNewsPictures(HttpRequestBase request,News addedNews)
        {
            List<Picture> pictures = new List<Picture>();
            HttpPostedFileBase checkFile = request.Files[0];
            if (checkFile.FileName != "")
            {
                for (int i = 0; i < request.Files.Count; i++)
                {
                    HttpPostedFileBase postedFileBase = request.Files[i];
                    Picture picture = _pictureService.Save(postedFileBase, addedNews.NewsId, ConfigurationManager.AppSettings["NewsPictures"]);
                  
                    picture.IsTittle = false; 
                    pictures.Add(picture);
                    
                    _pictureService.CreateThumb(
                        picture,
                        Convert.ToInt32(ConfigurationManager.AppSettings["PictureMainhWidth"]),
                        Convert.ToInt32(ConfigurationManager.AppSettings["PictureMainhHeight"]),
                        ConfigurationManager.AppSettings["PictureMainSize"]
                        );
                   
                }
                addedNews.Pictures.AddRange(pictures);
                _unitOfWork.PictureRepository.AddRange(pictures);
                await _unitOfWork.SaveAsync();   
            }
            return pictures;
        }



        public IndexViewModel<News> GetPageOfNews(int page)
        {

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["CountOfNewsOnPage"]);
            List<News> newsToView = _unitOfWork.NewsRepository.GetList();
            newsToView.Reverse();
            PageInfo pageInfo=new PageInfo { PageNumber=page,PageSize=pageSize,TotalItems=newsToView.Count};
            newsToView=newsToView.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            IndexViewModel<News> ivm=new IndexViewModel<News> { PageInfo = pageInfo,Collection=newsToView };

            return ivm;
        }
    }
}
