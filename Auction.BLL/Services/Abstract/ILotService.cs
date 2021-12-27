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
        Task<Lot> CreateLot(CreateLotModel lotModel, int loginId, HttpRequestBase request);
        LotModel GetLot(int lotId);
        List<LotModel> GetLotsBySeller(Func<User, bool> predicate);
        List<LotModel> GetByFilters(FiltersModel filtersModel);
        IndexViewModel<LotModel> GetPageOfLots(int page, string Filters, FiltersModel filtersModel);
        Task<LotModel> UpdateLot(int lotId, LotModel modelForUpdate);
        List<LotModel> GetAcquiredLots(int loginId);

    }
}
