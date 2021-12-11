using Auction.DAL.Models;
using Auction.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.API.Controllers
{
    public class DictionaryController : Controller
    {
        
        public DictionaryController(IUnitOfWork unitOfWork)
        {
           
        }
        public JsonResult Categories()
        {
            //List<Category> categories = _unitOfWork.CategoryRepository.GetAll();
            //return Json(categories,JsonRequestBehavior.AllowGet);
            return null;

        }
    }
}