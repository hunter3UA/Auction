using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
