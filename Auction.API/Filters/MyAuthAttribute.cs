using Auction.BLL.LoginModels;
using System.Web.Mvc;
using System.Web.Routing;

namespace Auction.API.Filters
{
    public class MyAuthAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly string[] AccountTypes;
        public MyAuthAttribute(string accountTypes)
        {
            this.AccountTypes = accountTypes.Split(new char[] { ',' });       
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isValid = false;
            ICustomPrincipal currentUser= filterContext.HttpContext.User as ICustomPrincipal;
            if(currentUser!=null )
            {
                for(int i = 0; i < AccountTypes.Length; i++)
                {
                    if (currentUser.IsInRole(AccountTypes[i]))
                        isValid = true;
                }
            }
            if (!isValid || !currentUser.Identity.IsAuthenticated)
            {
                RouteValueDictionary profileRoutePath=new RouteValueDictionary();
                profileRoutePath.Add("controller", "Home");
                profileRoutePath.Add("action", "Index");
                filterContext.Result = new RedirectToRouteResult(profileRoutePath);
            }
        }
    }
}