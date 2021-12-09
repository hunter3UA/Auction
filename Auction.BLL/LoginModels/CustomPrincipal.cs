using Auction.DAL.Models;
using System.Security.Principal;

namespace Auction.BLL.LoginModels
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public int LoginId { get; set; }
        public AccountType AccountType { get; set; }

        public IIdentity Identity { get;private set; }

        public CustomPrincipal(string name)
        {
            this.Identity=new GenericIdentity(name);
        }


        public bool IsInRole(string role)
        {
            return this.AccountType.AccountTypeName == role;    
        }
    }
}
