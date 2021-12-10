using Auction.BLL.LoginModels;
using System.Web.Mvc;
using System.Web.Routing;

namespace Auction.API.Filters
{
    public class AuthenticationAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool isAuth;
        public AuthenticationAttribute(bool isAuth)
        {
            this.isAuth = isAuth;
        }


        public void OnAuthorization(AuthorizationContext filterContext)
        {
            ICustomPrincipal currentUser= filterContext.HttpContext.User as ICustomPrincipal;

            if (isAuth &&  currentUser==null)
            {
                RouteValueDictionary profileDictionary= new RouteValueDictionary();
                profileDictionary.Add("controller", "Account");
                profileDictionary.Add("action", "Login");

                filterContext.Result=new RedirectToRouteResult(profileDictionary);
            }
            else if(!isAuth && currentUser!=null)
            {
                RouteValueDictionary profileDictionary = new RouteValueDictionary();
                profileDictionary.Add("controller", "Account");
                profileDictionary.Add("action", "LogOut");

                filterContext.Result = new RedirectToRouteResult(profileDictionary);
            }
        }
    }
}