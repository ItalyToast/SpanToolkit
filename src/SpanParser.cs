using System;
using System.Globalization;

namespace SpanToolkit
{
    public ref struct SpanParser
    {
        ReadOnlySpan<char> str;

        public ReadOnlySpan<char> Current => str;

        public int Length => str.Length;

        public SpanParser(ReadOnlySpan<char> span)
        {
            str = span;
        }

        public char First()
        {
            return str[0];
        }
        public char Last()
        {
            return str[str.Length - 1];
        }

        public ReadOnlySpan<char> SliceBetween(int startIndex, int endIndex)
        {
            return str.Slice(startIndex, endIndex - startIndex);
        }

        public void Shrink(int size)
        {
            str = str.Slice(0, str.Length - size);
        }

        #region string methods
        public bool EndsWith(char end) => str[str.Length - 1] == end;
        public bool EndsWith(ReadOnlySpan<char> end) => str.EndsWith(end);
        public bool EndsWith(ReadOnlySpan<char> end, StringComparison comparisonType) => str.EndsWith(end, comparisonType);

        public bool StartsWith(char start) => str[0] == start;
        public bool StartsWith(ReadOnlySpan<char> start) => str.StartsWith(start);
        public bool StartsWith(ReadOnlySpan<char> start, StringComparison comparisonType) => str.StartsWith(start, comparisonType);

        public int IndexOf(char c) => str.IndexOf(c);
        public int IndexOf(ReadOnlySpan<char> c) => str.IndexOf(c);
        public int IndexOf(ReadOnlySpan<char> c, StringComparison comparisonType) => str.IndexOf(c, comparisonType);

        public int LastIndexOf(char c) => str.LastIndexOf(c);
        public int LastIndexOf(ReadOnlySpan<char> c) => str.LastIndexOf(c);

        public ReadOnlySpan<char> Remove(int startIndex)
        {
            return str.Slice(0, startIndex);
        }
        #endregion

        #region Parsing
        public byte ParseByte(int size, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => byte.Parse(Read(size), style, provider);

        public short ParseShort(int size, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => short.Parse(Read(size), style, provider);
        public int ParseInt(int size, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null) 
            => int.Parse(Read(size), style, provider);

        public long ParseLong(int size, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => long.Parse(Read(size), style, provider);

        public float ParseSingle(int size, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => float.Parse(Read(size), style, provider);

        public double ParseDouble(int size, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => double.Parse(Read(size), style, provider);
        #endregion

        public ReadOnlySpan<char> Read(int size, int offset = 0)
        {
            var result = str.Slice(0, size);
            str = str.Slice(size + offset);
            return result;
        }

        public ReadOnlySpan<char> ReadTo(char c, int offset = 0) 
            => Read(str.IndexOf(c) + offset);

        public ReadOnlySpan<char> ReadTo(ReadOnlySpan<char> c, int offset = 0)
            => Read(str.IndexOf(c) + offset);

        public ReadOnlySpan<char> ReadWhile(Func<char, bool> filter, int offset = 0)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!filter(str[i]))
                {
                    return Read(i, offset);
                }
            }
            throw new ArgumentOutOfRangeException("Read past the buffer");
        }

        public ReadOnlySpan<char> ReadWhile(Func<char, int, bool> filter, int offset = 0)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!filter(str[i], i))
                {
                    return Read(i, offset);
                }
            }
            throw new ArgumentOutOfRangeException("Read past the buffer");
        }

        public void Advance(int size) => str = str.Slice(size);

        public void AdvanceTo(char c, int offset = 0) 
            => Advance(str.IndexOf(c) + offset);

        public void AdvanceTo(ReadOnlySpan<char> c, int offset = 0) 
            => Advance(str.IndexOf(c) + offset);

        #region operators
        public static implicit operator SpanParser(string str) => new SpanParser(str);
        public static implicit operator SpanParser(ReadOnlySpan<char> span) => new SpanParser(span);
        #endregion
    }
}
