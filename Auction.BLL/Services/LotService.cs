using Auction.BLL.Mapper;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.BLL.Services
{
    public class LotService:ILotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LotService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper=AutoMapperConfig.Configure().CreateMapper();
        }


        public async Task<Lot> CreateLot(CreateLotModel lotModel,int loginId)
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
            return newLot;
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
                List<Lot> lots = _unitOfWork.LotRepository.GetAllBySellerId(user.UserId);
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
        
        public List<LotModel> GetByFilters(FiltersModel filtersModel)
        {
            List<Lot> allLots = _unitOfWork.LotRepository.GetAll().ToList();
            if (filtersModel != null && filtersModel.Categories != null)
            {
                allLots = allLots.Where(l => filtersModel.Categories.Contains(l.Category.CategoryName)).ToList();
            }
            List<LotModel> lotModels = _mapper.Map<List<LotModel>>(allLots);
            return lotModels;
        }


        
        public IndexViewModel GetPageOfLots(int page, string Filters, FiltersModel filtersModel)
        {

            FiltersModel FiltersModel = filtersModel;
            if (!string.IsNullOrEmpty(Filters))
                FiltersModel = JsonConvert.DeserializeObject<FiltersModel>(Filters);
            int pageSize = 3;
            List<LotModel> models = GetByFilters(FiltersModel);
            IEnumerable<LotModel> lotsPerpages = models.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = models.Count };
            IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Lots = lotsPerpages.ToList() };
            ivm.FiltersModel = FiltersModel;
            return ivm;
        }


    }
}
