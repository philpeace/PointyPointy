using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Optimization;

namespace DrFoster.Common.Web.Bundling
{
    public class ImagePathBundleBuilder : IBundleBuilder
    {
        internal static IBundleBuilder Instance;

        static ImagePathBundleBuilder()
        {
            ImagePathBundleBuilder.Instance = new DefaultBundleBuilder();
        }

        public ImagePathBundleBuilder()
        {
        }

        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            string concatenationToken = null;

            if (files != null)
            {
                if (context != null)
                {
                    if (bundle != null)
                    {
                        string appPath = ImagePathBundleBuilder.GetAppPath(context);
                        var stringBuilder = new StringBuilder();
                        string boundaryIdentifier = "";

                        if (context.EnableInstrumentation)
                        {
                            boundaryIdentifier = ImagePathBundleBuilder.GetBoundaryIdentifier(bundle);
                            stringBuilder.AppendLine(ImagePathBundleBuilder.GenerateBundlePreamble(boundaryIdentifier));
                        }

                        if (string.IsNullOrEmpty(bundle.ConcatenationToken))
                        {
                            if (bundle.Transforms.Any(transform => typeof (JsMinify).IsAssignableFrom(transform.GetType())))
                            {
                                concatenationToken = ";";
                            }
                        }
                        else
                        {
                            concatenationToken = bundle.ConcatenationToken;
                        }

                        if (concatenationToken == null || context.EnableInstrumentation)
                        {
                            concatenationToken = System.Environment.NewLine;
                        }

                        foreach (BundleFile file in files)
                        {
                            if (context.EnableInstrumentation)
                            {
                                stringBuilder.Append(ImagePathBundleBuilder.GetFileHeader(appPath, file, ImagePathBundleBuilder.GetInstrumentedFileHeaderFormat(boundaryIdentifier)));
                            }

                            string content = ProcessImageReferences(file, context);

                            stringBuilder.Append(content);
                            stringBuilder.Append(concatenationToken);
                        }

                        return stringBuilder.ToString();
                    }
                    else
                    {
                        throw new ArgumentNullException("bundle");
                    }
                }
                else
                {
                    throw new ArgumentNullException("context");
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private string ProcessImageReferences(BundleFile file, BundleContext context)
        {
            var filePath = context.HttpContext.Server.MapPath(file.IncludedVirtualPath);

            var content = File.ReadAllText(filePath);
            var fromUri = new Uri(context.HttpContext.Server.MapPath("~/"));
            var toUri = new Uri(Path.GetDirectoryName(filePath));
            
            string imageUrlRoot = context.HttpContext.Request.ApplicationPath + "/" + fromUri.MakeRelativeUri(toUri).ToString();
            content = content.Replace("url(images", "url(" + imageUrlRoot + "/images");
            
            return content;
        }

        internal static string ConvertToAppRelativePath(string fullName, string appPath)
        {
            string str;

            if (!fullName.StartsWith(appPath, StringComparison.OrdinalIgnoreCase))
            {
                str = fullName;
            }
            else
            {
                str = fullName.Replace(appPath, "~/");
            }

            str = str.Replace(char.ConvertFromUtf32(92), char.ConvertFromUtf32(47));

            return str;
        }

        private static string GenerateBundlePreamble(string bundleHash)
        {
            Dictionary<string, string> instrumentedBundlePreamble = ImagePathBundleBuilder.GetInstrumentedBundlePreamble(bundleHash);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("/* ");

            foreach (string key in instrumentedBundlePreamble.Keys)
            {
                stringBuilder.Append(string.Concat(key, "=", instrumentedBundlePreamble[key], ";"));
            }

            stringBuilder.Append(" */");

            return stringBuilder.ToString();
        }

        private static string GetAppPath(BundleContext context)
        {
            if (context.HttpContext == null || context.HttpContext.Request == null)
            {
                return string.Empty;
            }
            else
            {
                return context.HttpContext.Request.PhysicalApplicationPath;
            }
        }

        private static string GetBoundaryIdentifier(Bundle bundle)
        {
            Type type;

            if (bundle.Transforms == null || bundle.Transforms.Count <= 0)
            {
                type = typeof (ImagePathBundleTransform);
            }
            else
            {
                type = bundle.Transforms[0].GetType();
            }

            int hashCode = type.FullName.GetHashCode();

            return Convert.ToBase64String(Encoding.Unicode.GetBytes(hashCode.ToString(CultureInfo.InvariantCulture)));
        }

        private static string GetFileHeader(string appPath, BundleFile file, string fileHeaderFormat)
        {
            if (!string.IsNullOrEmpty(fileHeaderFormat))
            {
                object[] appRelativePath = new object[1];
                appRelativePath[0] = ImagePathBundleBuilder.ConvertToAppRelativePath(file.VirtualFile.Name, appPath);
                
                return string.Concat(string.Format(CultureInfo.InvariantCulture, fileHeaderFormat, appRelativePath), "\r\n");
            }
            else
            {
                return string.Empty;
            }
        }

        private static Dictionary<string, string> GetInstrumentedBundlePreamble(string boundaryValue)
        {
            Dictionary<string, string> strs = new Dictionary<string, string>();
            strs["Bundle"] = "System.Web.Optimization.Bundle";
            strs["Boundary"] = boundaryValue;
            
            return strs;
        }

        private static string GetInstrumentedFileHeaderFormat(string boundaryValue)
        {
            return string.Concat("/* ", boundaryValue, " \"{0}\" */");
        }
    }
}