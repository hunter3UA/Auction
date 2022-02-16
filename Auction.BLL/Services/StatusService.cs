using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System;
using System.Collections.Generic;

namespace Auction.BLL.Services
{
    public class StatusService:IStatusService
    {

        private readonly IUnitOfWork _unitOfWork;

        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public LotStatus GetLotStatus(Func<LotStatus,bool> predicate)
        {
            LotStatus status = _unitOfWork.StatusRepository.Get(predicate);
            if (status != null)
                return status;
            return new LotStatus();
        }
        public List<LotStatus> GetList()
        {
            return _unitOfWork.StatusRepository.GetList();
        }

    }
}
