using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Auction.BLL.Services.Abstract
{
    public interface IPictureService
    {
        Picture Save(HttpPostedFileBase postedFile, int lotId);
        List<Picture> GetByLotId(int lotId);
        void CreateThumb(Picture pictureInfo, int width, int height, string resizedFor);
        void SaveThumb(Image thumbFileToSave, string savePath, string saveFileName);
        Image Resize(Image imageToResize, Size size);
        void SetTittle(int lotId, int pictureId);
    }
}
