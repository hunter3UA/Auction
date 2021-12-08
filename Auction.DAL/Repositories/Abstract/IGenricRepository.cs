using System;
using System.Collections.Generic;

namespace Auction.DAL.Repositories.Abstract
{
    public interface IGenricRepository<T> where T : class
    {  
        T Add(T item); 
        T Get(Func<T,bool> predicate);    
        IEnumerable<T> GetAll();    
        T Update(T item);   
        bool Delete(Func<T,bool> predicate);
    }
}
