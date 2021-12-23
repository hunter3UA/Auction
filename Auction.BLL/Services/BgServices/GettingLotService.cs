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
    public class GettingLotService : IJob
    {
      //  private readonly IUnitOfWork _unitOfWork;

        public GettingLotService(IUnitOfWork unitOfWork)
        {
      //      _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {

            if (1 == 1)
                throw new Exception();
            //List<Lot> lotsToBuy = _unitOfWork.LotRepository.GetList(l =>l.EndAt <DateTime.Now  && !l.IsSoldOut).ToList();
            //Category newCategory = _unitOfWork.CategoryRepository.Add(new Category { CategoryName = "Test" });
            //await _unitOfWork.SaveAsync();
            
        }
    }
}
