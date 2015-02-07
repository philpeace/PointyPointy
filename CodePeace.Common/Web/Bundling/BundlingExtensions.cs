using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Optimization;

namespace DrFoster.Common.Web.Bundling
{
    public static class BundlingExtensions
    {
        public static Bundle TransformWith(this Bundle bundle, IBundleTransform transform)
        {
            bundle.Transforms.Add(transform);

            return bundle;
        }

        public static Bundle BuildWith(this Bundle bundle, IBundleBuilder builder)
        {
            bundle.Builder = builder;

            return bundle;
        }
    }
}
