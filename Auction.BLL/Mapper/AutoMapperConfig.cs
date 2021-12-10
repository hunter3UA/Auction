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
                   
                });
            return config;
        }
    }
}
