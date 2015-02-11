using System.Web.Optimization;
using CodePeace.Common.Web.Bundling;

namespace PointyPointy
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles, bool enableOptimizations)
        {
            var mainLessBundle = new Bundle("~/Content/css", new LessBundleTransform())
                                        .Include("~/Content/less/site.less");

            if (enableOptimizations)
            {
                mainLessBundle.Transforms.Add(new CssMinify());
            }

            bundles.Add(mainLessBundle);

            // Scripts
            bundles.Add(new ScriptBundle("~/Scripts/head")
                .Include("~/Scripts/modernizr-*")
            );

            var scriptBundle = new ScriptBundle("~/Scripts/foot")
                        .Include("~/Scripts/jquery-{version}.js")
                        .Include("~/scripts/affix.js")
                        .Include("~/scripts/alert.js")
                        .Include("~/scripts/button.js")
                        .Include("~/scripts/carousel.js")
                        .Include("~/scripts/collapse.js")
                        .Include("~/scripts/dropdown.js")
                        .Include("~/scripts/modal.js")
                        .Include("~/scripts/tooltip.js")
                        .Include("~/scripts/popover.js")
                        .Include("~/scripts/scrollspy.js")
                        .Include("~/scripts/tab.js")
                        .Include("~/scripts/transition.js");

            scriptBundle.Orderer = new AsIsBundleOrderer();
            bundles.Add(scriptBundle);
        }
    }
}