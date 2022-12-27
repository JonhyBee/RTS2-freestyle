using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Tools
{
    internal static class Extensions
    {
        public static void ForEach<T>(this HashSet<T> values, Action<T> action)
        {
            values.ToList().ForEach(action);
        }
    }
}
