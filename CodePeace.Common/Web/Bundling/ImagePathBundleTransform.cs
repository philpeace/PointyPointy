using System;
using System.Web.Optimization;

namespace CodePeace.Common.Web.Bundling
{
    public class ImagePathBundleTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            if (response.ContentType != "text/css")
            {
                return;
            }

            var fromUri = new Uri(context.HttpContext.Server.MapPath("~/"));
            var toUri = new Uri(context.HttpContext.Server.MapPath(context.BundleVirtualPath));
            Uri relativeUri = fromUri.MakeRelativeUri(toUri);

            string imageUrlRoot = string.Format("{0}/{1}", context.HttpContext.Request.ApplicationPath, relativeUri);

            response.Content = response.Content.Replace("url(images", "url(" + imageUrlRoot + "/images");
        }
    }
}