using System;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit.ExtensionMethods
{
    public static class StringExtensions
    {
        internal static SpanParser AsSpanParser(this string str) => new SpanParser(str);
    }
}
