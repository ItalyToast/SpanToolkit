using System;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit
{
    ref struct SpanReader
    {
        ReadOnlySpan<byte> buffer;

        public ReadOnlySpan<byte> Current => buffer;
    }
}
