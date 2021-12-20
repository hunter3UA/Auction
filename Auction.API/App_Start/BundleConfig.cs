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
                .Include("~/Scripts/jquery-ui-{version}.js")
                .Include("~/Scripts/Slider/lightslider.min.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Scripts/custom.js")              
                .Include("~/Scripts/modernizr-*")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                .Include("~/Scripts/dropzone/dropzone.min.js")
                );                        

            bundles.Add(new StyleBundle("~/Content/css")
                .Include( "~/Content/bootstrap.css")               
                .Include("~/Content/Site.css")
                .Include("~/Content/custom/custom.css")
                .Include("~/Content/themes/base/jquery-ui.min.css")
                .Include("~/Content/Slider/lightslider.min.css")
                .Include("~/Scripts/dropzone/basic.min.css")
                .Include("~/Scripts/dropzone/dropzone.css")
               );
                
            BundleTable.EnableOptimizations = true;
                      
        }
    }
}
