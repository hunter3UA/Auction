using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories
{
    public class PictureRepository:IPictureRepository
    {
        private readonly AuctionDbContext _dbContext;

        public PictureRepository(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Picture> AddRange(List<Picture> picturesToAdd)
        {
            return _dbContext.Pictures.AddRange(picturesToAdd).ToList();
        }


        public List<Picture> GetPicturesByLotId(int lotId)
        {
            return _dbContext.Pictures.Where(p=>p.LotId == lotId).ToList();
        }

    }
}
