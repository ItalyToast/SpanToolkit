using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit
{
    public ref struct SpanWriterLittleEndian
    {
        Span<byte> buffer;

        public Span<byte> Current => buffer;

        public int Length => buffer.Length;

        public SpanWriterLittleEndian(Span<byte> buffer)
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

        public void WriteInt16(short value)
        {
            BinaryPrimitives.WriteInt16LittleEndian(buffer, value);
            Advance(2);
        }

        public void WriteUInt16(ushort value)
        {
            BinaryPrimitives.WriteUInt16LittleEndian(buffer, value);
            Advance(2);
        }

        public void WriteInt32(int value)
        {
            BinaryPrimitives.WriteInt32LittleEndian(buffer, value);
            Advance(4);
        }

        public void WriteUInt32(uint value)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(buffer, value);
            Advance(4);
        }

        public void WriteInt64(long value)
        {
            BinaryPrimitives.WriteInt64LittleEndian(buffer, value);
            Advance(8);
        }

        public void WriteUInt64(ulong value)
        {
            BinaryPrimitives.WriteUInt64LittleEndian(buffer, value);
            Advance(8);
        }

        public void WriteFloat(float value)
            => WriteInt32(BitConverter.SingleToInt32Bits(value));

        public void WriteDouble(double value)
            => WriteInt64(BitConverter.DoubleToInt64Bits(value));

        public void Write7BitEncodedInt(int value)
        {
            var size = PackedInt.Write7BitEncodedInt(buffer, value);
            Advance(size);
        }

        public void WriteString(string str, Encoding encoding)
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
