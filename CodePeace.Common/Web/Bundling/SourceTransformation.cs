using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrFoster.Common.Web.Bundling
{
    public class SourceTransformation
    {
        public string Source
        {
            get;
            set;
        }

        public IEnumerable<FileInfo> Dependencies
        {
            get;
            set;
        }
    }
}
