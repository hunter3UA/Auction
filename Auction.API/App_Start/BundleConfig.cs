using System.Web.Optimization;

namespace Auction.API
{
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery.validate*") 
                .Include("~/Scripts/modernizr-*")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/custom.js")
                );                        

            bundles.Add(new StyleBundle("~/Content/css").Include( "~/Content/bootstrap.css","~/Content/Site.css","~/Content/custom/custom.css"));
                
            BundleTable.EnableOptimizations = true;
                      
        }
    }
}
