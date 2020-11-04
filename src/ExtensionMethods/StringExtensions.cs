using System;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit
{
    public static class StringExtensions
    {
        public static SpanParser AsSpanParser(this string str) => new SpanParser(str);
    }
}
