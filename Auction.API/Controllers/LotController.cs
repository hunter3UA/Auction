using Auction.API.Filters;
using Auction.BLL.Services;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;



namespace Auction.API.Controllers
{
   
    public class LotController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILotService _lotService;
        private readonly IPictureService _pictureService;

        public LotController(ICategoryService catetoryService,ILotService lotService,IPictureService pictureService)
        {
            _categoryService = catetoryService;
            _lotService = lotService;
            _pictureService = pictureService;
        }
        [HttpGet,Authentication(true)]
        public ActionResult Create()
        {
            
            ViewData["Categories"] = _categoryService.GetCategories();
            return View();
        }



        [HttpPost,ValidateAntiForgeryToken,Authentication(true)]
        public async Task<ActionResult> Create(CreateLotModel lotModel)
        {
            if (ModelState.IsValid)
            {
                Lot addedLot =  await _lotService.CreateLotAsync(lotModel,User.LoginId, Request);
                return RedirectToAction("Edit","Lot",new { id=addedLot.LotId});
            }
            ViewData["Categories"] = _categoryService.GetCategories();
            return View("Create",lotModel);
        }


        [HttpGet,Authentication(true)]
        public ActionResult BySeller(int page=1)
        {
            List<LotModel> lotsOfSeller = _lotService.GetList(l=>l.Seller.LoginId==User.LoginId);
            IndexViewModel<LotModel> ivm = PageService<LotModel>.GetPage(
                 page,
                 Convert.ToInt32(ConfigurationManager.AppSettings["CountOfLotBySeller"]),
                 lotsOfSeller
                );
            ivm.Collection.Reverse();
            return View(ivm);
        }

        [HttpGet]
        public ActionResult LotPage(int lotId=0)
        {
            LotModel lotModel = _lotService.GetLot(l=>l.LotId==lotId && l.StatusId==2);     
            if (lotModel==null || lotModel.LotId==0)
                return RedirectToAction("Index","Home");
            return View(lotModel);
        }

        [HttpGet,Authentication(true)]
        public ActionResult Edit(int id=0)
        {
          
            LotModel lotModel= _lotService.GetLot(l=>l.LotId==id);
            if (lotModel != null  && lotModel.LotId!=0)
            {
                if (lotModel.LoginId == User.LoginId || User.AccountType.AccountTypeName == "Admin")
                {
                    ViewData["Categories"] = _categoryService.GetCategories();
                    return View(lotModel);
                }
            }
            return RedirectToAction("BySeller");
        }

        [HttpPost,Authentication(true),ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int lotId, LotModel modelToUpdate)
        {
            await _lotService.UpdateLotAsync(lotId, modelToUpdate);          
            return RedirectToAction("Edit", new { id=modelToUpdate.LotId});
        }


        [HttpGet]
        public ActionResult LotPictures(int lotId=0)
        {
            if(lotId== 0)
                return RedirectToAction("BySeller");
            List<Picture> picturesByLotid=_pictureService.GetList(p=>p.LotId==lotId);
            return PartialView(picturesByLotid);
        }     
        [Authentication(true)]
        public ActionResult PictureSetAsTittle(int lotId,int pictureId)
        {
            _pictureService.SetTittle(lotId, pictureId);
            return RedirectToAction("Edit", new { id = lotId });
        }


        [Authentication(true)]
        public async Task<ActionResult> RemovePicture(int lotId,int pictureId)
        {
            LotModel lotModel= _lotService.GetLot(l=>l.LotId==lotId);
            if (!lotModel.IsSoldOut)
            {
                await _pictureService.RemovePicture(lotModel.LotId,
                    ConfigurationManager.AppSettings["PicturesFolder"],
                    ConfigurationManager.AppSettings["LotsPictures"],
                    pictureId
                    );
            }
            return RedirectToAction("Edit", new { id = lotId });
        }


      
        public async Task<ActionResult> UploadLotPictures(int lotId)
        {

         
            await _pictureService.AddPicturesAsync(Request, lotId);
            return RedirectToAction("Edit", new {  id =lotId});
           
        }


        [HttpGet,Authentication(true)]
        public ActionResult AcquiredLots(int page=1)
        {
            List<LotModel> acquiredLots=_lotService.GetAcquiredLots(User.LoginId);
            IndexViewModel<LotModel> ivm=PageService<LotModel>.GetPage(
                page,
                Convert.ToInt32(ConfigurationManager.AppSettings["CountAcquiredLots"]),
                acquiredLots
                );       
            return View(ivm);
        }
    }
}
