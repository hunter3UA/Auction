using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Auction.BLL.Services.Abstract
{
    public interface ILotService
    {
        List<LotModel> GetList(Func<Lot, bool> predicate);
        Task<Lot> CreateLotAsync(CreateLotModel lotModel, int loginId, HttpRequestBase request);
        LotModel GetLot(Func<Lot,bool> predicate);
        List<LotModel> GetByFilters(FiltersModel filtersModel);
        Task<LotModel> UpdateLotAsync(int lotId, LotModel modelForUpdate);
        List<LotModel> GetAcquiredLots(int loginId);
        Task<bool> UpdateLotStatusAsync(int lotId, int statusId);
        Task<bool> DisableLot(int lotId);

    }
}
