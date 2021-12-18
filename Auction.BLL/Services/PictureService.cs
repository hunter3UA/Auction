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



        public Picture Save(HttpPostedFileBase postedFile,int lotId)
        {
          
            Picture picture = new Picture();
            try
            {
                string fileSaveFolder = string.Empty;
                string webSiteFolder = HttpContext.Current.Server.MapPath("~");
                try
                {
                    fileSaveFolder = ConfigurationManager.AppSettings["PicturesFolder"];
                }
                catch (ConfigurationException)
                {

                    fileSaveFolder = "PictureFiles";
                }

                
                if (!Directory.Exists(fileSaveFolder))
                {
                    Directory.CreateDirectory(Path.Combine(webSiteFolder, fileSaveFolder));
                }

                string lotFileFolder = Path.Combine(webSiteFolder, fileSaveFolder, lotId.ToString());

                if (!Directory.Exists(lotFileFolder))
                {
                    Directory.CreateDirectory(lotFileFolder);
                }
                picture.Path = lotFileFolder;
                string FileSaveName = Path.GetFileNameWithoutExtension(postedFile.FileName)
                    + "_"
                    + DateTime.Now.Ticks.ToString()
                    + Path.GetExtension(postedFile.FileName);
                picture.Name = FileSaveName;

                postedFile.SaveAs(Path.Combine(lotFileFolder, FileSaveName));
               

            }
            catch (Exception ex)
            {
               
            }
            return picture;
        }

        public void CreateThumb(Picture pictureInfo,int width,int height,string resizedFor)
        {
            Image imageToResize = Image.FromFile(Path.Combine(pictureInfo.Path, pictureInfo.Name));
            Size size = new Size(width, height);
            Image resizedImage =Resize(imageToResize, size);
            string FileSaveName = Path.GetFileNameWithoutExtension(pictureInfo.Name)
                    + "_"
                    + resizedFor
                    + Path.GetExtension(pictureInfo.Name);
            SaveThumb(resizedImage, pictureInfo.Path, FileSaveName);

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
     

        public List<Picture> GetByLotId (int lotId)
        {
            return _unitOfWork.PictureRepository.GetPicturesByLotId(lotId);

        }



    }
}
