using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using dotless.Core.Input;

namespace DrFoster.Common.Web.Bundling
{
    public class ImportedFilePathResolver : IPathResolver
    {
        private string currentFileDirectory;
        private string currentFilePath;

        public ImportedFilePathResolver(string currentFilePath)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                throw new ArgumentNullException("currentFilePath");
            }

            CurrentFilePath = currentFilePath;
        }

        /// <summary>
        /// Gets or sets the path to the currently processed file.
        /// </summary>
        public string CurrentFilePath
        {
            get
            {
                return currentFilePath;
            }
            set
            {
                currentFilePath = value;
                currentFileDirectory = Path.GetDirectoryName(value);
            }
        }

        /// <summary>
        /// Returns the absolute path for the specified improted file path.
        /// </summary>
        /// <param name="filePath">The imported file path.</param>
        public string GetFullPath(string filePath)
        {
            if (filePath.StartsWith("~"))
            {
                filePath = VirtualPathUtility.ToAbsolute(filePath);
            }

            if (filePath.StartsWith("/"))
            {
                filePath = HostingEnvironment.MapPath(filePath);
            }
            else if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.GetFullPath(Path.Combine(currentFileDirectory, filePath));
            }

            return filePath;
        }
    }
}