using Auction.DAL.Models;
using System.Collections.Generic;

namespace Auction.BLL.Services.Abstract
{
    public interface ICategoryService
    {

        List<Category> GetCategories();
    }
}
