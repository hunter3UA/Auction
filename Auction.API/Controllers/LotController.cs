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

namespace Auction.API.Controllers
{
    [Authentication(true)]
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
        [HttpGet]
        public ActionResult Create()
        {
            
            ViewData["Categories"] = _categoryService.GetCategories();
            return View();
        }



        [HttpPost,ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLotModel lotModel)
        {
            if (ModelState.IsValid)
            {
                Lot addedLot =  await _lotService.CreateLot(lotModel,User.LoginId, Request);
                return RedirectToAction("Edit","Lot",new { id=addedLot.LotId});
            }
            
            return RedirectToAction("Create");

        }


        [HttpGet]
        public ActionResult BySeller()
        {
            return View(_lotService.GetLotsBySellerId(User.LoginId));
        }


        [HttpGet]
        public ActionResult LotPage(int lotId=0)
        {
  
            LotModel lotModel =  _lotService.GetLot(lotId);
            if (lotModel==null)
                return RedirectToAction("Index","Home");
            return View(lotModel);
        }


        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            LotModel lotModel= _lotService.GetLot(id);
            if(lotModel != null)
            {
                if (lotModel.LoginId == User.LoginId)
                {
                    ViewData["Categories"] = _categoryService.GetCategories();
                    return View(lotModel);
                }
            }
            return RedirectToAction("BySeller");
        }


        [HttpGet]
        public ActionResult LotPictures(int lotId=0)
        {

            if(lotId== 0)
                return RedirectToAction("BySeller");
            List<Picture> picturesByLotid=_pictureService.GetByLotId(lotId);
            return PartialView(picturesByLotid);

        }     
        public JsonResult PictureSetAsTittle(int lotId,int pictureId)
        {
            _pictureService.SetTittle(lotId, pictureId);
            return Json(true,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> UploadLotPictures(int lotId)
        {
            await _lotService.AddPictures(Request, lotId);
            return RedirectToAction("Edit",new { id=lotId });

        }
    }
}