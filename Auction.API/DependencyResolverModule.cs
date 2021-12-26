using Auction.BLL.Services;
using Auction.BLL.Services.Abstract;
using Auction.DAL.UoW;
using Ninject.Modules;

namespace Auction.API
{
    public class DependencyResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IAccountService>().To<AccountService>();
            Bind<ILotService>().To<LotService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IPictureService>().To<PictureService>();
            Bind<IStakeService>().To<StakeService>();
            Bind<INewsService>().To<NewsService>();
         
        }
    }
}