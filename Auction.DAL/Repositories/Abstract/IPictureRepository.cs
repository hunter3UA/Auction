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

        List<Picture> AddRange(List<Picture> picturesToAdd);
        List<Picture> GetPicturesByLotId(int lotId);
    }
}
