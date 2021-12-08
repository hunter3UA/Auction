using Auction.DAL.Models;
using System.Security.Principal;

namespace Auction.BLL.Login
{
    public interface ICustomPrincipal:IPrincipal
    {
        int LoginId { get; set; }
        AccountType AccountType { get; set; }

    }
}
