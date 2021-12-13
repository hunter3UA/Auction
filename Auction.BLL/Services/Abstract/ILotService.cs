using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface ILotService
    {

        Task<Lot> CreateLot(CreateLotModel lotModel, int loginId);
        List<LotModel> GetAll();
        LotModel GetLot(int lotId);
        List<LotModel> GetLotsBySellerId(int sellerId);

    }
}
