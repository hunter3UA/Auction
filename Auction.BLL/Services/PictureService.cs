using Auction.BLL.Services.Abstract;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
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
        public async Task<List<Picture>> AddPicturesAsync(HttpRequestBase request, int lotId)
        {
            try
            {
                Lot lotOfPictures = _unitOfWork.LotRepository.Get(l => l.LotId == lotId);
                List<Picture> pictures = new List<Picture>();
                HttpPostedFileBase checkFile = request.Files[0];
                if (checkFile.FileName != "" && !lotOfPictures.IsSoldOut)
                {
                    for (int i = 0; i < request.Files.Count; i++)
                    {
                        HttpPostedFileBase postedFileBase = request.Files[i];
                        if (CheckPictureExtension(Path.GetExtension(postedFileBase.FileName)))
                        {
                            Picture picture = Save(postedFileBase, lotOfPictures.LotId, ConfigurationManager.AppSettings["LotsPictures"]);
                            picture.IsTittle = false;
                            picture.LotId = lotOfPictures.LotId;
                            pictures.Add(picture);
                            CreateThumb(
                                picture,
                                Convert.ToInt32(ConfigurationManager.AppSettings["PicturePrevGalleryWidth"]),
                                Convert.ToInt32(ConfigurationManager.AppSettings["PicturePrevGalleryHeight"]),
                                ConfigurationManager.AppSettings["PictureGallerySize"]
                                );
                            CreateThumb(
                                picture,
                                Convert.ToInt32(ConfigurationManager.AppSettings["PictureMainhWidth"]),
                                Convert.ToInt32(ConfigurationManager.AppSettings["PictureMainhHeight"]),
                                ConfigurationManager.AppSettings["PictureMainSize"]
                                );
                            CreateThumb(
                                picture,
                                Convert.ToInt32(ConfigurationManager.AppSettings["PictureSearchWidth"]),
                                Convert.ToInt32(ConfigurationManager.AppSettings["PictureSearchHeight"]),
                                ConfigurationManager.AppSettings["PictureSearchingSize"]
                                );
                        }
                    }
                    _unitOfWork.PictureRepository.AddRange(pictures);
                    await _unitOfWork.SaveAsync();
                    
                }
                return pictures;
            } catch { return new List<Picture>(); }
           
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
                fileSaveFolder = ConfigurationManager.AppSettings["PicturesFolder"];               
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
            catch { new Picture(); }
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
                imageToResize.Dispose();
                resizedImage.Dispose();
            }
            catch{}

            
            
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

        public async Task<bool> RemovePicture(int id,string directory,string subDirectory, int pictureId)
        {
            try
            {
                Picture pictureToRemove = _unitOfWork.PictureRepository.Get(p => p.PictureId == pictureId);
                if (pictureToRemove != null)
                {
                    string path = Path.Combine(HttpContext.Current.Server.MapPath("~"), directory, subDirectory, id.ToString());
                    if (Directory.Exists(path))
                    {
                        string[] fs = Directory.GetFiles(path);
                        List<string> filesToRemove = fs.Where(f => f.Contains(Path.GetFileNameWithoutExtension(pictureToRemove.Name))).ToList();                      
                        
                        foreach (var file in filesToRemove)
                        {
                            File.Delete(Path.Combine(path, file));
                        }                     
                        bool isDeleted = _unitOfWork.PictureRepository.Remove(pictureToRemove);
                        await _unitOfWork.SaveAsync();
                        return isDeleted;
                    }
                }
                return false;
            }
            catch { return false; }

        }
        public bool SetTittle(int lotId,int pictureId)
        {
            try
            {
                Lot lotToUpdate = _unitOfWork.LotRepository.Get(l => l.LotId == lotId && !l.IsSoldOut);
                Picture pictureToSet = _unitOfWork.PictureRepository.Get(p => p.PictureId == pictureId);
                if (lotToUpdate != null && pictureToSet != null)
                {
                    _unitOfWork.PictureRepository.SetPictureAsTittle(lotId, pictureId);
                    return true;
                }
                return false;
            }catch { return false; }
           
        }
        public List<Picture> GetList(Func<Picture,bool> predicate)
        {
            return _unitOfWork.PictureRepository.GetList(predicate);

        }
        private bool CheckPictureExtension(string ext)
        {
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                return true;
            else
                return false;
        }
    }
}
