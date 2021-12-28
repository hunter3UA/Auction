using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.DAL.Repositories.Abstract
{
    public interface IPictureRepository
    {
        Picture Get(Func<Picture, bool> predicate);
        bool Remove(Picture pictureToRemove);
        List<Picture> AddRange(List<Picture> picturesToAdd);
        List<Picture> GetList(Func<Picture, bool> predicate);
        void SetPictureAsTittle(int lotId, int pictureId);
    }
}
