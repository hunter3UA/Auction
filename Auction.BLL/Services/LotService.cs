using Auction.BLL.Mapper;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

//TODO: Переделать метод фильтрации 


namespace Auction.BLL.Services
{
    public class LotService:ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPictureService _pictureService;   

        public LotService(IUnitOfWork unitOfWork,IPictureService pictureService)
        {
            _unitOfWork = unitOfWork;
            _mapper=AutoMapperConfig.Configure().CreateMapper();
            _pictureService=pictureService;
        }

        public async Task<Lot> CreateLot(CreateLotModel lotModel,int loginId, HttpRequestBase request)
        {
            Lot newLot=_mapper.Map<Lot>(lotModel);
            newLot.CreatedAt = DateTime.Now;
            newLot.SoldAt = null;
            newLot.Seller= _unitOfWork.UserRepository.Get(u=>u.Login.LoginId==loginId);
            newLot.IsSoldOut = false;
            newLot.LotCode = DateTime.Now.Ticks/2000;
            newLot.Category=_unitOfWork.CategoryRepository.Get(c=>c.CategoryId==lotModel.CategoryId);
            _unitOfWork.LotRepository.Add(newLot);
            await _unitOfWork.SaveAsync();
            
            if(request.Files!=null)
                await AddPictures(request, newLot.LotId);     
            return newLot;
        }

        public async Task<List<Picture>> AddPictures(HttpRequestBase request,int lotId)
        {
            Lot lotOfPictures = _unitOfWork.LotRepository.Get(l => l.LotId == lotId);
            List<Picture> pictures = new List<Picture>();
            HttpPostedFileBase checkFile = request.Files[0];
            if (checkFile.FileName != "")
            {
                for (int i = 0; i < request.Files.Count; i++)
                {
                    HttpPostedFileBase postedFileBase = request.Files[i];
                    Picture picture = _pictureService.Save(postedFileBase, lotOfPictures.LotId);
                    picture.IsTittle = false;
                    picture.LotId = lotOfPictures.LotId;
                    pictures.Add(picture);
                    _pictureService.CreateThumb(
                        picture,
                        Convert.ToInt32(ConfigurationManager.AppSettings["PicturePrevGalleryWidth"]),
                        Convert.ToInt32(ConfigurationManager.AppSettings["PicturePrevGalleryHeight"]),
                        ConfigurationManager.AppSettings["PictureGallerySize"]
                        );
                    _pictureService.CreateThumb(
                        picture,
                        Convert.ToInt32(ConfigurationManager.AppSettings["PictureMainhWidth"]),
                        Convert.ToInt32(ConfigurationManager.AppSettings["PictureMainhHeight"]),
                        ConfigurationManager.AppSettings["PictureMainSize"]
                        );
                    _pictureService.CreateThumb(
                        picture,
                        Convert.ToInt32(ConfigurationManager.AppSettings["PictureSearchWidth"]),
                        Convert.ToInt32(ConfigurationManager.AppSettings["PictureSearchHeight"]),
                        ConfigurationManager.AppSettings["PictureSearchingSize"]
                        );
                }
                _unitOfWork.PictureRepository.AddRange(pictures);
                await _unitOfWork.SaveAsync();
            }
            return pictures;
          
        }

        public LotModel GetLot(int lotId)
        {
           Lot lotById= _unitOfWork.LotRepository.Get(l=>l.LotId==lotId);        
           LotModel lotModel= _mapper.Map<LotModel>(lotById);
           return lotModel;
        }

        public List<LotModel> GetLotsBySellerId(int loginId)
        {
            User user = _unitOfWork.UserRepository.Get(u => u.LoginId == loginId);
            if (user != null)
            {
                List<Lot> lots = _unitOfWork.LotRepository.GetAllBySellerId(user.UserId).ToList();
                List<LotModel> lotModelsBySeller = _mapper.Map<List<LotModel>>(lots); 
                return lotModelsBySeller;
            }
         
            return new List<LotModel>();

           
        }

        public List<LotModel> GetAll()
        {
            List<Lot> allLots= _unitOfWork.LotRepository.GetAll().ToList();
            List<LotModel> lotModels= _mapper.Map<List<LotModel>>(allLots);
            return lotModels;

        }
        /// <summary>
        /// Метод для фільтрації лотів
        /// </summary>
        /// <param name="filtersModel">Модель з критеріями фільтрації</param>
        /// <returns></returns>
        public List<LotModel> GetByFilters(FiltersModel filtersModel)
        {
            List<Lot> allLots = _unitOfWork.LotRepository.GetAll().ToList();
            if (filtersModel != null && filtersModel.Categories != null)
            {
                allLots = allLots.Where(l => filtersModel.Categories.Contains(l.Category.CategoryName)).ToList();
            }
            if (!string.IsNullOrEmpty(filtersModel.LotName))
            {
                allLots=allLots.Where(l=>l.LotName.ToLower().Contains(filtersModel.LotName.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(filtersModel.Criterion))
            {
                switch (filtersModel.Criterion)
                {
                    case "PriceCriterion":                      
                        allLots = filtersModel.Order == "Desc" ? allLots.OrderByDescending(l => l.Price).ToList():allLots.OrderBy(l=>l.Price).ToList();
                        break;                                         
                    case "DateCriterion":
                        allLots = filtersModel.Order=="Desc" ? allLots.OrderByDescending(l=>l.CreatedAt).ToList():allLots.OrderBy(l=>l.CreatedAt).ToList();
                        break;
                }
            }
            List<LotModel> lotModels = _mapper.Map<List<LotModel>>(allLots);
            return lotModels;
        }

        public IndexViewModel<LotModel> GetPageOfLots(int page, string Filters, FiltersModel filtersModel)
        {

            FiltersModel FiltersModel = filtersModel;
            if (!string.IsNullOrEmpty(Filters))
                FiltersModel = JsonConvert.DeserializeObject<FiltersModel>(Filters);
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["CountOfLotsOnPage"]);
            List<LotModel> models = GetByFilters(FiltersModel);
            IEnumerable<LotModel> lotsPerpages = models.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = models.Count };
            IndexViewModel<LotModel> ivm = new IndexViewModel<LotModel> { PageInfo = pageInfo, Collection = lotsPerpages.ToList() };
            ivm.FiltersModel = FiltersModel;
            return ivm;
        }


        public async Task<LotModel> UpdateLot(int lotId,LotModel modelForUpdate)
        {
            Lot lotToUpdate = _unitOfWork.LotRepository.Get(l => l.LotId == lotId);
            lotToUpdate.LotName=modelForUpdate.LotName;
            lotToUpdate.Description=modelForUpdate.Description; 
            lotToUpdate.Category=_unitOfWork.CategoryRepository.Get(c=>c.CategoryId==modelForUpdate.CategoryId);
            lotToUpdate.Price=modelForUpdate.Price;
            _unitOfWork.LotRepository.Update(lotToUpdate);
            await _unitOfWork.SaveAsync();
            return modelForUpdate;


        }



    }
}
