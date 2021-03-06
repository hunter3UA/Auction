using Auction.DAL.Models;
using Auction.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public bool Remove(Picture pictureToRemove)
        {
            if(pictureToRemove!=null && pictureToRemove.PictureId != 0)
            {
                _dbContext.Pictures.Remove(pictureToRemove);
                return true;
            }
            return false;
             
        }
        public Picture Get(Func<Picture,bool> predicate)
        {
            return _dbContext.Pictures.FirstOrDefault(predicate);
        }
        public List<Picture> AddRange(List<Picture> picturesToAdd)
        {
            return _dbContext.Pictures.AddRange(picturesToAdd).ToList();
        }
        public List<Picture> GetList(Func<Picture,bool> predicate)
        {
            return _dbContext.Pictures.Where(predicate).ToList();
        }
        public void SetPictureAsTittle(int lotId, int pictureId)
        {
            SqlParameter lotParam = new SqlParameter("@LotId",lotId);
            SqlParameter pictureParam = new SqlParameter("@PictureId", pictureId);
            _dbContext.Database.ExecuteSqlCommand("exec stp_Picture_SetTittle @LotId, @PictureId", lotParam, pictureParam);
        }

    }
}
