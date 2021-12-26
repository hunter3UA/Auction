using Auction.API.Filters;
using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

/*
TODO:добавить уведомление о установки картинки в качестве главной,
добавить удаление картинки
*/
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
                Lot addedLot =  await _lotService.CreateLot(lotModel,User.LoginId, Request);
                return RedirectToAction("Edit","Lot",new { id=addedLot.LotId});
            }
            
            return View("Create",lotModel);
        }


        [HttpGet,Authentication(true)]
        public ActionResult BySeller(int page=1)
        {
            IndexViewModel<LotModel> ivm = _lotService.GetLotsBySeller(page, u => u.LoginId == User.LoginId);
            return View(ivm);
        }


        [HttpGet]
        public ActionResult LotPage(int lotId=0)
        {
            LotModel lotModel = _lotService.GetLot(lotId);
            if (lotModel==null)
                return RedirectToAction("Index","Home");
            return View(lotModel);
        }

        [HttpGet,Authentication(true)]
        public ActionResult Edit(int id=0)
        {
            LotModel lotModel= _lotService.GetLot(id);
           
            if(lotModel != null && lotModel.LoginId == User.LoginId)
            {
                ViewData["Categories"] = _categoryService.GetCategories();
                return View(lotModel);           
            }
            return RedirectToAction("BySeller");
        }

        [HttpPost,Authentication(true),ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int lotId, LotModel modelToUpdate)
        {
            await _lotService.UpdateLot(lotId, modelToUpdate);
           
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
        [HttpPost,Authentication(true)]
        public async Task<ActionResult> UploadLotPictures(int lotId)
        {
            await _pictureService.AddPictures(Request, lotId);
            return RedirectToAction("Edit",new { id=lotId });

        }


        [HttpDelete]
        public ActionResult RemoveLotPicture(int lotId,int pictureId)
        {
            return RedirectToAction("Edit", new { id = lotId });
        }


        [HttpGet,Authentication(true)]
        public ActionResult AcquiredLots(int page=1)
        {
            IndexViewModel<LotModel> ivm=_lotService.GetAcquiredLots(User.LoginId,page);       
            return View(ivm);
        }
    }
}