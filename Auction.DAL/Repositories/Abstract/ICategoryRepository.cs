using Auction.DAL.Models;
using System;
using System.Collections.Generic;

namespace Auction.DAL.Repositories.Abstract
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category Get(Func<Category, bool> predicate);
        Category Add(Category newCategory);
    }
}
