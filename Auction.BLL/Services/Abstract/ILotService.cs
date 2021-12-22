using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Auction.BLL.Services.Abstract
{
    public interface ILotService
    {

        Task<Lot> CreateLot(CreateLotModel lotModel, int loginId, HttpRequestBase request);
        List<LotModel> GetAll();
        LotModel GetLot(int lotId);
        List<LotModel> GetLotsBySellerId(int sellerId);
        List<LotModel> GetByFilters(FiltersModel filtersModel);
        IndexViewModel<LotModel> GetPageOfLots(int page, string Filters, FiltersModel filtersModel);
        Task<List<Picture>> AddPictures(HttpRequestBase request, int lotId);
        Task<LotModel> UpdateLot(int lotId, LotModel modelForUpdate);

    }
}
