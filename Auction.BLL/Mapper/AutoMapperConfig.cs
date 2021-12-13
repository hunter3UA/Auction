using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using AutoMapper;

namespace Auction.BLL.Mapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            MapperConfiguration config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RegisterModel, Login>();
                    cfg.CreateMap<RegisterModel, User>();
                    cfg.CreateMap<User,UserModel>();
                    cfg.CreateMap<UserModel, User>();
                    cfg.CreateMap<LotModel, Lot>();
                    cfg.CreateMap<Lot, LotModel>().ForMember(l=>l.Seller,lm=>lm.Ignore());
                
                    cfg.CreateMap<CreateLotModel, Lot>();
                   
                });
            return config;
        }
    }
}
