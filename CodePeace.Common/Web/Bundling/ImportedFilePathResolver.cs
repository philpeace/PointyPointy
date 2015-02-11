using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using dotless.Core.Input;

namespace CodePeace.Common.Web.Bundling
{
    public class ImportedFilePathResolver : IPathResolver
    {
        private string _currentFileDirectory;
        private string _currentFilePath;

        public ImportedFilePathResolver(string currentFilePath)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                throw new ArgumentNullException("currentFilePath");
            }

            CurrentFilePath = currentFilePath;
        }

        public string CurrentFilePath
        {
            get { return _currentFilePath; }
            set
            {
                _currentFilePath = value;
                _currentFileDirectory = Path.GetDirectoryName(value);
            }
        }

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
                filePath = Path.GetFullPath(Path.Combine(_currentFileDirectory, filePath));
            }

            return filePath;
        }
    }
}