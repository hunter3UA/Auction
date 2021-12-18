﻿using Auction.API.Filters;
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
            
          
            await _lotService.CreateLot(lotModel,User.LoginId, Request);
            return RedirectToAction("Profile","Account");
        }


        [HttpGet]
        public ActionResult BySeller()
        {
            return View(_lotService.GetLotsBySellerId(User.LoginId));
        }


        [HttpGet]
        public ActionResult LotPage(int lotId)
        {
            LotModel lotModel =  _lotService.GetLot(lotId);
            return View(lotModel);
        }


        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            LotModel lotModel= _lotService.GetLot(id);
            ViewData["Categories"] = _categoryService.GetCategories();
            if (lotModel == null)
                return RedirectToAction("BySeller");
            return View(lotModel);
        }


        [HttpGet]
        public PartialViewResult LotPictures(int lotId=0)
        {
            List<Picture> picturesByLotid=_pictureService.GetByLotId(lotId);


            return PartialView(picturesByLotid);

        }
        [HttpPost]
        public async Task<ActionResult> UploadLotPictures(int lotId)
        {
            await _lotService.AddPictures(Request, lotId);


            return RedirectToAction("Edit",new { id=lotId });




        }
    }
}