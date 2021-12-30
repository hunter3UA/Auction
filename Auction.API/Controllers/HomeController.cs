using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using Auction.BLL.Services;
using Newtonsoft.Json;
/*

TODO: Cart Update
TODO: проверить все токены


TODO: переделать пагинацию и фильтрацию 


*/

namespace Auction.API.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILotService _lotService;
        private readonly ICategoryService _categoryService;

      
        public HomeController(ILotService lotService,ICategoryService categoryService)
        {
            _lotService = lotService;
            _categoryService = categoryService;        
        }


        
        public ActionResult Index(int page=1,string filters = null,FiltersModel filtersModel=null)
        {
            List<LotModel> lots = _lotService.GetPageOfLots();
            IndexViewModel<LotModel> ivm = PageService<LotModel>.GetPage(
                page,
                3,
                lots
                );
            ViewData["Categories"] = _categoryService.GetCategories();
    
            //if (!string.IsNullOrEmpty(filters))
            //    ivm.FiltersModel = JsonConvert.DeserializeObject<FiltersModel>(filters);
            //els
                ivm.FiltersModel = filtersModel;
            
             return View(ivm);

        }
        

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
    }
}
/*
<div class="flex-container">
    <div class="flex-item-row filters-item" id="filtersSection">
        <label> Фільтри:</label>
        <div class="flex-container flex-item-col">
            @using (Ajax.BeginForm("Pages", new { page = 1 }, new AjaxOptions { UpdateTargetId = "updatedPage" }, new { id = "filtersForm" }))
            {
                <div class="flex-item-row" id="chbFlags">
                    Категорія:
                    @foreach (var item in categories.Categories)
                    {
                        <p>
                            @item.CategoryName <input type="checkbox" id="@item.CategoryId" name="Categories" value="@item.CategoryName" />

                        </p>
                    }
                </div>
                <div class="flex-item-row">            
                    Назва:@Html.TextBoxFor(m=>m.FiltersModel.LotName, "", new { id = "txtLotName" })
                </div>
                <div class="flex-item-row">
         
                    Порядок: @Html.DropDownListFor(m => m.FiltersModel.Order, orderItems)
                </div>
                <div class="flex-item-row">
                    Критерій: @Html.DropDownListFor(m=>m.FiltersModel.Criterion,criterions)
                </div>
                <div class="flex-item-row">
                    <input type="submit" value="Enter" class="btn btn-info" id="btnFiltersSubmit" />
                </div>
                if ((User as CustomPrincipal) != null && (User as CustomPrincipal).IsInRole("Admin"))
                {
                    <div>
                        за статусом
                    </div>
                }
            }
            @using (Ajax.BeginForm("Pages", new { page = 1, Filters = "" }, new AjaxOptions { UpdateTargetId = "updatedPage" }, null))
            {
                <input type="submit" value="Зкинути фільтри" id="btnClearFilters" />
            }

        </div>
    </div>
    <div class="flex-item-row">
        <div class="flex-container flex-item-col">
            <div class="flex-item-row">
                <div id="updatedPage">
                    @foreach (var item in Model.Collection)
                    {
                        <div id="lotItem" class="flex-container">
                            <div class="flex-item-row">
                                @{
                                    var picture = item.Pictures.Where(p => p.IsTittle == true).FirstOrDefault();
                                    if (picture == null)
                                    {
                                        <img src="~/Content/img/noImage_search.png" />
                                    }
                                    else
                                    {
                                        string path = Path.GetFileNameWithoutExtension(picture.Name)
                                                    + "_"
                                                    + "search"
                                                    + Path.GetExtension(picture.Name);
                                        <img src="/Pictures/Lots/@picture.LotId/@path" />
                                    }
                                }
                            </div>
                            <div class="flex-item-row lot-details">
                                Назва: @item.LotName
                                <br />
                                Стартова ціна: @item.Price
                                <br />
                                Категорія: @item.Category.CategoryName
                                <br />
                            </div>
                            <div class="flex-item-row">
                                <div class="flex-container flex-item-col">
                                    <div class="flex-item-row">
                                        @Html.ActionLink("Дивитись сторінку", "LotPage", "Lot", new { lotId = item.LotId }, new { @class = "btn btn-info" })
                                    </div>
                                    <div class="flex-item-row">
                                        @{
                                            if ((User as CustomPrincipal) != null && (User as CustomPrincipal).LoginId == item.LoginId)
                                            {
                                                @Html.ActionLink("Редагувати", "Edit", "Lot", new { id = item.LotId }, new { @class = "btn btn-success" });

                                            }

                                        }
                                    </div>
                                </div>
                            </div>
                        </div>
                    }

                </div>

            </div>
        </div>
    </div>
</div>*/