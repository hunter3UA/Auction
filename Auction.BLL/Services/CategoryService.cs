using Auction.BLL.Services.Abstract;
using Auction.BLL.ViewModels;
using Auction.DAL.Models;
using Auction.DAL.UoW;
using System.Collections.Generic;

namespace Auction.BLL.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Category> GetCategories()
        {
           
            List<Category> categories = _unitOfWork.CategoryRepository.GetAll();
            return categories;
        }
       
        
    }
}
