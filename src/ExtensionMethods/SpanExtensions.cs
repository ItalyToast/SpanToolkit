using System;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit
{
    public static class SpanExtensions
    {
        public static SpanWriter AsSpanWriter(this Span<byte> buffer)
            => new SpanWriter(buffer);


        public static SpanWriterLittleEndian AsSpanWriterLittleEndian(this Span<byte> buffer)
            => new SpanWriterLittleEndian(buffer);

        public static SpanWriterBigEndian AsSpanWriterBigEndian(this Span<byte> buffer)
            => new SpanWriterBigEndian(buffer);
    }
}
