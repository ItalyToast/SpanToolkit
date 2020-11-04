using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit
{
    public ref struct SpanWriter
    {
        Span<byte> buffer;

        public Span<byte> Current => buffer;

        public int Length => buffer.Length;

        public SpanWriter(Span<byte> buffer)
        {
            this.buffer = buffer;
        }

        public void WriteBoolean(bool value)
        {
            buffer[0] = (byte)(value ? 1 : 0);
            Advance(1);
        }

        public void WriteSByte(sbyte value)
        {
            buffer[0] = (byte)value;
            Advance(1);
        }

        public void WriteByte(byte value)
        {
            buffer[0] = value;
            Advance(1);
        }

        public void WriteInt16BigEndian(short value)
        {
            BinaryPrimitives.WriteInt16BigEndian(buffer, value);
            Advance(2);
        }

        public void WriteInt16LittleEndian(short value)
        {
            BinaryPrimitives.WriteInt16LittleEndian(buffer, value);
            Advance(2);
        }

        public void WriteInt32BigEndian(int value)
        {
            BinaryPrimitives.WriteInt32BigEndian(buffer, value);
            Advance(4);
        }

        public void WriteInt32LittleEndian(int value)
        {
            BinaryPrimitives.WriteInt32LittleEndian(buffer, value);
            Advance(4);
        }

        public void WriteInt64BigEndian(long value)
        {
            BinaryPrimitives.WriteInt64BigEndian(buffer, value);
            Advance(8);
        }

        public void WriteInt64LittleEndian(long value)
        {
            BinaryPrimitives.WriteInt64LittleEndian(buffer, value);
            Advance(8);
        }

        public void WriteUInt16BigEndian(ushort value)
        {
            BinaryPrimitives.WriteUInt16BigEndian(buffer, value);
            Advance(2);
        }

        public void WriteUInt16LittleEndian(ushort value)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);
            Advance(2);
        }

        public void WriteUInt32BigEndian(uint value)
        {
            BinaryPrimitives.WriteUInt32BigEndian(buffer, value);
            Advance(4);
        }

        public void WriteUInt32LittleEndian(uint value)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);
            Advance(4);
        }

        public void WriteUInt64BigEndian(ulong value)
        {
            BinaryPrimitives.WriteUInt64BigEndian(buffer, value);
            Advance(8);
        }

        public void WriteUInt64LittleEndian(ulong value)
        {
            BinaryPrimitives.WriteUInt64LittleEndian(buffer, value);
            Advance(8);
        }

        public void WriteFloatBigEndian(float value)
            => WriteInt32BigEndian(BitConverter.SingleToInt32Bits(value));

        public void WriteFloatLittleEndian(float value)
            => WriteInt32LittleEndian(BitConverter.SingleToInt32Bits(value));

        public void WriteDoubleBigEndian(double value)
            => WriteInt64BigEndian(BitConverter.DoubleToInt64Bits(value));

        public void WriteDoubleLittleEndian(double value)
            => WriteInt64BigEndian(BitConverter.DoubleToInt64Bits(value));

        public void Write7BitEncodedInt(int value)
        {
            var size = PackedInt.Write7BitEncodedInt(buffer, value);
            Advance(size);
        }

        public void WriteString(string str,  Encoding encoding)
        {
            int count = encoding.GetByteCount(str);
            Write7BitEncodedInt(count);
            encoding.GetBytes(str, buffer);
            Advance(count);
        }

        public void WriteBytes(ReadOnlySpan<byte> value)
        {
            value.CopyTo(buffer);
            Advance(value.Length);
        }

        public void Advance(int size)
        {
            buffer = buffer.Slice(size);
        }

        public Span<byte> DeferWrite(int size)
        {
            var result = buffer.Slice(0, size);
            buffer = buffer.Slice(size);
            return result;
        }
    }
}
