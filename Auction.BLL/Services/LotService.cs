using Auction.BLL.Mapper;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using AutoMapper;
using System;
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


        public async Task<Lot> CreateLot(LotModel lotModel,int loginId)
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


    }
}
