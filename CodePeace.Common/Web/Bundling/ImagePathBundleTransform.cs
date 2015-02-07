using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace DrFoster.Common.Web.Bundling
{
    public class ImagePathBundleTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            if (response.ContentType == "text/css")
            {
                var fromUri = new Uri(context.HttpContext.Server.MapPath("~/"));
                var toUri = new Uri(context.HttpContext.Server.MapPath(context.BundleVirtualPath));
                string imageUrlRoot = context.HttpContext.Request.ApplicationPath + "/" + fromUri.MakeRelativeUri(toUri).ToString();
                response.Content = response.Content.Replace("url(images", "url(" + imageUrlRoot + "/images");
            }
        }
    }
}
