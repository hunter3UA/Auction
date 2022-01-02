using Auction.DAL.Models;
using Auction.DAL.UoW;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services.BgServices
{
    public class UpdatingLotService : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
             IUnitOfWork unitOfWork=new UnitOfWork();
             List<Lot> expiredLots = unitOfWork.LotRepository.GetList(
                l => l.StatusId==2 &&
                !l.IsSoldOut && l.EndAt<DateTime.Now
                ).ToList();

            expiredLots.ForEach(l =>
            {
                l.EndAt = DateTime.Now.AddDays(7);
            });
            await unitOfWork.SaveAsync();
        }
    }
}
