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

        Task<Lot> CreateLot(CreateLotModel lotModel, int loginId, HttpRequestBase request);
        LotModel GetLot(int lotId);
        IndexViewModel<LotModel> GetLotsBySeller(int page, Func<User, bool> predicate);
        List<LotModel> GetByFilters(FiltersModel filtersModel);
        IndexViewModel<LotModel> GetPageOfLots(int page, string Filters, FiltersModel filtersModel);
        Task<LotModel> UpdateLot(int lotId, LotModel modelForUpdate);
        IndexViewModel<LotModel> GetAcquiredLots(int loginId, int page);

    }
}
