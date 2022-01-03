using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.BLL.Services.Abstract
{
    public interface IEmailService
    {
        void SendPasswordConfirmed(string Email);
    }
}
