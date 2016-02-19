using System;
using System.Linq;

namespace MC.eSIS.Core
{
    public static class ExtensionMethods
    {
        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            return list.Contains(source);
        }
    }
}
