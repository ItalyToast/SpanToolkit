using System;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit
{
    internal class PackedInt
    {
        internal static (int value, int size) Read7BitEncodedInt(ReadOnlySpan<byte> span)
        {
			int value = 0;
			int bits = 0;
			int pos = 0;
			while (bits != 35)
			{
				byte b = span[pos++];
				value |= (b & 0x7F) << bits;
				bits += 7;
				if ((b & 0x80) == 0)
				{
					return (value, pos);
				}
			}
			throw new FormatException("Bad 7bit encoded int.");
		}

		internal static int Write7BitEncodedInt(Span<byte> span, int value)
        {
			uint num = (uint)value;
			int pos = 0;
            while (num >= 0x80)
            {
				num >>= 7;
				span[pos++] = (byte)(num | 0x80);
			}
			span[pos++] = (byte)num;
			return pos;
		}
    }
}
