using Auction.DAL.Repositories.Abstract;
using System.Threading.Tasks;

namespace Auction.DAL.UoW
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ILotRepository LotRepository { get; }

        Task SaveAsync();
    }
}
