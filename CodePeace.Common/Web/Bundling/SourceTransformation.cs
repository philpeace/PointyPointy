using System.Collections.Generic;
using System.IO;

namespace CodePeace.Common.Web.Bundling
{
    public class SourceTransformation
    {
        public string Source { get; set; }

        public IEnumerable<FileInfo> Dependencies { get; set; }
    }
}

