using Auction.BLL.LoginModels;
using Auction.BLL.Services;
using Ninject;
using Ninject.Web.Mvc;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Auction.API
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolverModule registrations = new DependencyResolverModule();
            var kernel = new StandardKernel(registrations);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
           
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                UserSerializationModel loginCookieModel = serializer.Deserialize<UserSerializationModel>(authenticationTicket.UserData);

                CustomPrincipal loggedInUser = new CustomPrincipal(authenticationTicket.Name);
                loggedInUser.LoginId = loginCookieModel.LoginId;
                loggedInUser.AccountType = loginCookieModel.AccountType;
                HttpContext.Current.User = loggedInUser;
                AccountService.ExtendCookieLifer(authenticationTicket);
         
            }

        }
    }
}
