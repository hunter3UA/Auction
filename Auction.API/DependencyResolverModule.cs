using Auction.DAL.UoW;
using Ninject.Modules;

namespace Auction.API
{
    public class DependencyResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}