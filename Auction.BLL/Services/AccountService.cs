using Auction.BLL.Mapper;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using AutoMapper;

namespace Auction.BLL.Services
{
    public class AccountService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperConfig.Configure().CreateMapper();
        }


        public User CreateUser(RegisterModel registerModel)
        {
            

            return null;
           
        }
    }
}
