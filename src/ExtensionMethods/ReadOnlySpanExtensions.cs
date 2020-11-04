using System;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit
{
    public static class ReadOnlySpanExtensions
    {
        public static SpanReader AsSpanReader(this ReadOnlySpan<byte> buffer)
            => new SpanReader(buffer);

        public static SpanReaderLittleEndian AsSpanReaderLittleEndian(this ReadOnlySpan<byte> buffer)
            => new SpanReaderLittleEndian(buffer);

        public static SpanReaderBigEndian AsSpanReaderBigEndian(this ReadOnlySpan<byte> buffer)
            => new SpanReaderBigEndian(buffer);

        public static SpanParser AsSpanParser(this ReadOnlySpan<char> str) 
            => new SpanParser(str);
    }
}
