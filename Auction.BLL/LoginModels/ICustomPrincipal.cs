using Auction.DAL.Models;
using System.Security.Principal;

namespace Auction.BLL.LoginModels
{
    public interface ICustomPrincipal:IPrincipal
    {
        int LoginId { get; set; }
        AccountType AccountType { get; set; }

    }
}
