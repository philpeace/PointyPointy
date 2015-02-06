using System.Collections.Generic;
using System.IO;

namespace CodePeace.Common.Web.Bundling
{
    public interface ILessTransformer
    {
        SourceTransformation Transform(IEnumerable<FileInfo> files);
    }
}