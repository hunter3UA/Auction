using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Auction.BLL.Services
{
    public class PictureService:IPictureService
    {

        private readonly IUnitOfWork _unitOfWork;

        public PictureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Метод для збереження оригніалу картинки
        /// </summary>
        /// <param name="postedFile">Об'єкт для створення картинки</param>
        /// <param name="lotId">Id лота, який створює папку для всіх картинок данного лота</param>
        /// <returns></returns>
        public Picture Save(HttpPostedFileBase postedFile, long id, string pictureTypeFolder)
        {
            Picture picture = new Picture();
            try
            {
                string webSiteFolder = HttpContext.Current.Server.MapPath("~");
                string fileSaveFolder = string.Empty;
                try
                {
                    fileSaveFolder = ConfigurationManager.AppSettings["PicturesFolder"];
                }
                catch (ConfigurationException)
                {
                    fileSaveFolder = "PictureFiles";
                }

                if (!Directory.Exists(Path.Combine(webSiteFolder,fileSaveFolder)))
                    Directory.CreateDirectory(Path.Combine(webSiteFolder, fileSaveFolder));
              
                if (!Directory.Exists(Path.Combine(webSiteFolder, fileSaveFolder, pictureTypeFolder)))
                    Directory.CreateDirectory(Path.Combine(webSiteFolder, fileSaveFolder, pictureTypeFolder));

                picture.Path = Path.Combine(webSiteFolder, fileSaveFolder, pictureTypeFolder, id.ToString());
                if (!Directory.Exists(Path.Combine(webSiteFolder, fileSaveFolder, pictureTypeFolder, id.ToString())))
                    Directory.CreateDirectory(Path.Combine(webSiteFolder, fileSaveFolder, pictureTypeFolder, id.ToString()));
                picture.Name = Path.GetFileNameWithoutExtension(postedFile.FileName)
                       + "_"
                       + DateTime.Now.Ticks.ToString()
                       + Path.GetExtension(postedFile.FileName);

                postedFile.SaveAs(Path.Combine(picture.Path, picture.Name));
            }
            catch (Exception ex) { }
            return picture;
        }

        /// <summary>
        /// Метод для створення картинок потрібного розміру
        /// </summary>
        /// <param name="pictureInfo">Оригінал картинки</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="resizedFor">Тип картинки</param>
        public void CreateThumb(Picture pictureInfo,int width,int height,string resizedFor)
        {
            try
            {
                Image imageToResize = Image.FromFile(Path.Combine(pictureInfo.Path, pictureInfo.Name)); 
                Size size = new Size(width, height);
                Image resizedImage =Resize(imageToResize, size);
                string FileSaveName = Path.GetFileNameWithoutExtension(pictureInfo.Name)
                    + "_"
                    + resizedFor
                    + Path.GetExtension(pictureInfo.Name);
                SaveThumb(resizedImage, pictureInfo.Path, FileSaveName);
            }catch (Exception ex)
            {

            }
        }

        public void SaveThumb(Image thumbFileToSave,string savePath,string saveFileName)
        {
            thumbFileToSave.Save(Path.Combine(savePath, saveFileName));
        }

        public Image Resize(Image imageToResize, Size size)
        {
            int sourceWidth = imageToResize.Width;
            int sourceHeight=imageToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent= nPercentW;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight=(int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(imageToResize,0,0,destWidth,destHeight);
            g.Dispose();
            return (Image)b;

        }    

        public void SetTittle(int lotId,int pictureId)
        {
            _unitOfWork.PictureRepository.SetPictureAsTittle(lotId, pictureId);
        }

        public List<Picture> GetList(Func<Picture,bool> predicate)
        {
            return _unitOfWork.PictureRepository.GetList(predicate);

        }

    }
}
