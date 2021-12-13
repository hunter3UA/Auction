using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface ILotService
    {

        Task<Lot> CreateLot(LotModel lotModel, int loginId);

    }
}
