using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.UoW;


namespace Auction.BLL.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CategoriesModel GetCategories()
        {
            CategoriesModel model = new CategoriesModel();
            model.Categories = _unitOfWork.CategoryRepository.GetAll();
            return model;
        }
       
        
    }
}
