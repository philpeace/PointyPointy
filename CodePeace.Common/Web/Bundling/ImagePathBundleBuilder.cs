using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Optimization;

namespace CodePeace.Common.Web.Bundling
{
    public class ImagePathBundleBuilder : IBundleBuilder
    {
        internal static IBundleBuilder Instance;

        static ImagePathBundleBuilder()
        {
            Instance = new DefaultBundleBuilder();
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
                        var appPath = GetAppPath(context);
                        var stringBuilder = new StringBuilder();
                        var boundaryIdentifier = "";

                        if (context.EnableInstrumentation)
                        {
                            boundaryIdentifier = GetBoundaryIdentifier(bundle);
                            stringBuilder.AppendLine(GenerateBundlePreamble(boundaryIdentifier));
                        }

                        if (string.IsNullOrEmpty(bundle.ConcatenationToken))
                        {
                            if (bundle.Transforms.Any(transform => transform is JsMinify))
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
                            concatenationToken = Environment.NewLine;
                        }

                        foreach (var file in files)
                        {
                            if (context.EnableInstrumentation)
                            {
                                stringBuilder.Append(GetFileHeader(appPath, file, GetInstrumentedFileHeaderFormat(boundaryIdentifier)));
                            }

                            var content = ProcessImageReferences(file, context);

                            stringBuilder.Append(content);
                            stringBuilder.Append(concatenationToken);
                        }

                        return stringBuilder.ToString();
                    }

                    throw new ArgumentNullException("bundle");
                }

                throw new ArgumentNullException("context");
            }

            return string.Empty;
        }

        private static string ProcessImageReferences(BundleFile file, BundleContext context)
        {
            var filePath = context.HttpContext.Server.MapPath(file.IncludedVirtualPath);

            var content = File.ReadAllText(filePath);
            var fromUri = new Uri(context.HttpContext.Server.MapPath("~/"));
            var toUri = new Uri(Path.GetDirectoryName(filePath));
            var relativeUri = fromUri.MakeRelativeUri(toUri);

            var imageUrlRoot = string.Format("{0}/{1}", context.HttpContext.Request.ApplicationPath, relativeUri);
            content = content.Replace("url(images", "url(" + imageUrlRoot + "/images");

            return content;
        }

        internal static string ConvertToAppRelativePath(string fullName, string appPath)
        {
            var str = !fullName.StartsWith(appPath, StringComparison.OrdinalIgnoreCase) ? fullName : fullName.Replace(appPath, "~/");

            str = str.Replace(char.ConvertFromUtf32(92), char.ConvertFromUtf32(47));

            return str;
        }

        private static string GenerateBundlePreamble(string bundleHash)
        {
            var instrumentedBundlePreamble = GetInstrumentedBundlePreamble(bundleHash);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/* ");

            foreach (var key in instrumentedBundlePreamble.Keys)
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

            return context.HttpContext.Request.PhysicalApplicationPath;
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

            var hashCode = type.FullName.GetHashCode();

            return Convert.ToBase64String(Encoding.Unicode.GetBytes(hashCode.ToString(CultureInfo.InvariantCulture)));
        }

        private static string GetFileHeader(string appPath, BundleFile file, string fileHeaderFormat)
        {
            if (!string.IsNullOrEmpty(fileHeaderFormat))
            {
                var appRelativePath = new object[1];
                appRelativePath[0] = ConvertToAppRelativePath(file.VirtualFile.Name, appPath);

                return string.Concat(string.Format(CultureInfo.InvariantCulture, fileHeaderFormat, appRelativePath), "\r\n");
            }

            return string.Empty;
        }

        private static Dictionary<string, string> GetInstrumentedBundlePreamble(string boundaryValue)
        {
            var strs = new Dictionary<string, string>();
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