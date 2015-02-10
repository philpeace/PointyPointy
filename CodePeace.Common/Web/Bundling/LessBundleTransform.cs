using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Optimization;

namespace CodePeace.Common.Web.Bundling
{
    public class LessBundleTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse bundle)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (bundle == null)
            {
                throw new ArgumentNullException("bundle");
            }

            context.HttpContext.Response.Cache.SetLastModifiedFromFileDependencies();

            var transformer = new LessTransformer();
            IEnumerable<FileInfo> files = bundle.Files.Select(f => new FileInfo(context.HttpContext.Server.MapPath(f.VirtualFile.VirtualPath)));

            SourceTransformation sourceTransform = transformer.Transform(files);

            if (BundleTable.EnableOptimizations)
            {
                string basePath = context.HttpContext.Server.MapPath("~/");

                bundle.Files = sourceTransform.Dependencies.Distinct().Select(f => BuildBundleFile(f, basePath));
            }

            foreach (FileInfo file in sourceTransform.Dependencies)
            {
                context.HttpContext.Response.AddFileDependency(file.FullName);
            }

            bundle.ContentType = "text/css";
            bundle.Content = sourceTransform.Source;
        }

        private static BundleFile BuildBundleFile(FileInfo f, string basePath)
        {
            string virtualPath = string.Format("~/{0}", f.FullName.Substring(basePath.Length));
            VirtualFile virtualFile = HostingEnvironment.VirtualPathProvider.GetFile(virtualPath);
            string includedVirtualPath = VirtualPathUtility.ToAppRelative(virtualPath);

            return new BundleFile(includedVirtualPath, virtualFile);
        }
    }
}