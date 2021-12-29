using Auction.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface IPasswordService
    {
       
        Salted_Hash CreateSaltedHash(string stringToHash, int saltLenght);
        bool CheckSaltedHash(string stringToCheck, Salted_Hash saltedHash);
    }
}
