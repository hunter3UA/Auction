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
        public async Task<Lot> CreateLotAsync(CreateLotModel lotModel,int loginId, HttpRequestBase request)
        {
            try
            {
                Lot newLot = _mapper.Map<Lot>(lotModel);
                newLot.EndAt = DateTime.Now.AddDays(lotModel.EndAt);
                newLot.CreatedAt = DateTime.Now;
                newLot.SoldAt = null;
                newLot.Seller = _unitOfWork.UserRepository.Get(u => u.Login.LoginId == loginId);
                newLot.IsSoldOut = false;
                newLot.LotCode = DateTime.Now.Ticks / 2000;
                newLot.Category = _unitOfWork.CategoryRepository.Get(c => c.CategoryId == lotModel.CategoryId);
                newLot.StatusId = 1;
                _unitOfWork.LotRepository.Add(newLot);
                await _unitOfWork.SaveAsync();
                if (request.Files != null)
                    await _pictureService.AddPicturesAsync(request, newLot.LotId);
                return newLot;
            }catch (Exception ex)
            {
                return new Lot();
            }
        }      
        public LotModel GetLot(int lotId)
        {
           Lot lotById= _unitOfWork.LotRepository.Get(l=>l.LotId==lotId);       
        
           return _mapper.Map<LotModel>(lotById);;
        }
        public List<LotModel> GetLotsBySeller(Func<User,bool> predicate)
        {
            User user = _unitOfWork.UserRepository.Get(predicate);
            if (user != null)
            {
                List<Lot> lots = _unitOfWork.LotRepository.GetList(u=>u.SellerId==user.UserId).ToList();                 
                List<LotModel> lotModels=   _mapper.Map<List<LotModel>>(lots);
                return lotModels;
            }       
            return new List<LotModel>();
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
                allLots = allLots.Where(l => l.LotName.ToLower().Contains(filtersModel.LotName.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(filtersModel.Criterion))
            {
                switch (filtersModel.Criterion)
                {
                    case "PriceCriterion":
                        allLots = filtersModel.Order == "Desc" ? allLots.OrderByDescending(l => l.Price).ToList() : allLots.OrderBy(l => l.Price).ToList();
                        break;
                    case "DateCriterion":
                        allLots = filtersModel.Order == "Desc" ? allLots.OrderByDescending(l => l.CreatedAt).ToList() : allLots.OrderBy(l => l.CreatedAt).ToList();
                        break;
                }
            }
            return _mapper.Map<List<LotModel>>(allLots);
        }
        public IndexViewModel<LotModel> GetPageOfLots(int page, string Filters, FiltersModel filtersModel)
        {
            FiltersModel FiltersModel = filtersModel;
            if (!string.IsNullOrEmpty(Filters))
                FiltersModel = JsonConvert.DeserializeObject<FiltersModel>(Filters);
            IndexViewModel<LotModel> ivm = PageService<LotModel>.GetPage(
                page,
                Convert.ToInt32(ConfigurationManager.AppSettings["CountOfLotsOnPage"]),
                GetByFilters(FiltersModel));
            ivm.FiltersModel = FiltersModel;
            return ivm;
        }

        public List<LotModel> GetAcquiredLots(int loginId)
        {
            User userByLogin= _unitOfWork.UserRepository.Get(u=>u.LoginId==loginId);
            ShopptingCart cart = _unitOfWork.CartRepository.Get(c => c.UserId == userByLogin.UserId);
            List<LotModel> lotModels=_mapper.Map<List<LotModel>>(cart.Lots);
            return lotModels;

        }
        public async Task<LotModel> UpdateLotAsync(int lotId,LotModel modelForUpdate)
        {
            Lot lotToUpdate = _unitOfWork.LotRepository.Get(l => l.LotId == lotId);
            if (lotToUpdate.IsSoldOut)
            {
                lotToUpdate.LotName = modelForUpdate.LotName;
                lotToUpdate.Description = modelForUpdate.Description;
                _unitOfWork.LotRepository.Update(lotToUpdate);
                await _unitOfWork.SaveAsync();
                return modelForUpdate;
            }
            return new LotModel();

        }

        public List<LotModel> GetList(Func<Lot,bool> predicate)
        {
            List<Lot> lots=_unitOfWork.LotRepository.GetList(predicate).ToList();
            return null;

        }
        
      
    }
}
