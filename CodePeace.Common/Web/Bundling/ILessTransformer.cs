using System.Collections.Generic;
using System.IO;

namespace DrFoster.Common.Web.Bundling
{
    public interface ILessTransformer
    {
        SourceTransformation Transform(IEnumerable<FileInfo> files);
    }
}