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
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/custom.js")
                .Include("~/Scripts/jquery-ui-{version}.js")
                .Include("~/Scripts/modernizr-*")
                );                        

            bundles.Add(new StyleBundle("~/Content/css")
                .Include( "~/Content/bootstrap.css")               
                .Include("~/Content/Site.css")
                .Include("~/Content/custom/custom.css")
                .Include("~/Content/themes/base/all.css")
               );
                
            BundleTable.EnableOptimizations = true;
                      
        }
    }
}
