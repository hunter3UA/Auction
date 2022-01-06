using Auction.BLL.Mapper;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;



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
                newLot.CurrentPrice = lotModel.Price+lotModel.Step;
                _unitOfWork.LotRepository.Add(newLot);
                await _unitOfWork.SaveAsync();
                if (request.Files != null)
                    await _pictureService.AddPicturesAsync(request, newLot.LotId);
                return newLot;
            }catch { return new Lot(); }
            
                
            
        }      
        public LotModel GetLot(Func<Lot,bool> predicate)
        {
            try
            {
                Lot lotById = _unitOfWork.LotRepository.Get(predicate);
                if (lotById != null && lotById.LotId != 0)
                    return _mapper.Map<LotModel>(lotById);
                return new LotModel();
            }catch { return new LotModel(); }
            
        }
     

        public List<LotModel> GetByFilters(FiltersModel filtersModel)
        {
            try
            {
                List<Lot> allLots = _unitOfWork.LotRepository.GetAll().ToList();
                allLots = allLots.Where(l => !l.IsSoldOut).ToList();
                if (filtersModel != null && filtersModel.Categories.Count() > 0)
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
                if (!string.IsNullOrEmpty(filtersModel.Status))
                {
                    allLots = allLots.Where(l => l.StatusId == Convert.ToInt32(filtersModel.Status)).ToList();
                }
                return _mapper.Map<List<LotModel>>(allLots);
            }catch { return new List<LotModel>();}          
        }
      
        
        public async Task<LotModel> UpdateLotAsync(int lotId,LotModel modelForUpdate)
        {
            try
            {
                Lot lotToUpdate = _unitOfWork.LotRepository.Get(l => l.LotId == lotId);
                if (lotToUpdate != null && !lotToUpdate.IsSoldOut)
                {
                    lotToUpdate.LotName = modelForUpdate.LotName;
                    lotToUpdate.Description = modelForUpdate.Description;
                    await _unitOfWork.SaveAsync();
                    return modelForUpdate;
                }
            }catch { return new LotModel(); }           
            return new LotModel();

        }

        public async Task<bool> UpdateLotStatusAsync(int lotId, int statusId)
        {
            try
            {
                Lot lotById = _unitOfWork.LotRepository.Get(l => l.LotId == lotId);
                if (lotById != null)
                {
                    lotById.StatusId = statusId;
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }catch { return false; }
            return false;
        }

        public async Task<bool> DisableLot(int lotId)
        {
            Lot lot = _unitOfWork.LotRepository.Get(l => l.LotId == lotId);
            if (lot != null)
            {
                List<Stake> stakesToRemove = _unitOfWork.StakeRepository.GetList(s=>s.LotId==lot.LotId);
                bool isDisabled = _unitOfWork.StakeRepository.RemoveRangeStake(stakesToRemove);
                if (isDisabled)
                {                
                    lot.StatusId = 3;
                    await _unitOfWork.SaveAsync();
                    return true;
                }
            }
            return false;
        }

        public List<LotModel> GetList(Func<Lot,bool> predicate)
        {
            try
            {
                List<Lot> lots = _unitOfWork.LotRepository.GetList(predicate).ToList();
                return lots != null ? _mapper.Map<List<LotModel>>(lots) : new List<LotModel>();
            }catch { return new List<LotModel>(); }
            
        }

        public List<LotModel> GetAcquiredLots(int loginId)
        {
            try
            {
                User userByLogin = _unitOfWork.UserRepository.Get(u => u.LoginId == loginId);
                ShopptingCart cart = _unitOfWork.CartRepository.Get(c => c.UserId == userByLogin.UserId);
                return cart==null || cart.ShoppingCartId==0 ? new List<LotModel>()  : _mapper.Map<List<LotModel>>(cart.Lots);
            }catch { return new List<LotModel>(); }
        }
    }
}
