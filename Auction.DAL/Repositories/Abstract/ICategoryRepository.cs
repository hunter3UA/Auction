using Auction.DAL.Models;
using System.Collections.Generic;

namespace Auction.DAL.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}
