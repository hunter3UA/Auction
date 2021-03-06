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
        Task<List<Picture>> AddPicturesAsync(HttpRequestBase request, int lotId);
        Picture Save(HttpPostedFileBase postedFile, long id, string pictureTypeFolder);
        List<Picture> GetList(Func<Picture, bool> predicate);
        void CreateThumb(Picture pictureInfo, int width, int height, string resizedFor);
        void SaveThumb(Image thumbFileToSave, string savePath, string saveFileName);
        Image Resize(Image imageToResize, Size size);
        bool SetTittle(int lotId, int pictureId,int loginId);
        Task<bool> RemovePicture(int id, string directory, string subDirectory, int pictureId);


    }
}
