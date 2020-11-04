using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace SpanToolkit
{
    public ref struct SpanReaderLittleEndian
    {
        ReadOnlySpan<byte> buffer;

        public ReadOnlySpan<byte> Current => buffer;

        public int Length => buffer.Length;

        public SpanReaderLittleEndian(ReadOnlySpan<byte> buffer)
        {
            this.buffer = buffer;
        }

        public bool ReadBoolean()
            => ReadBytes(1)[0] != 0;

        public sbyte ReadSByte()
            => (sbyte)ReadBytes(1)[0];

        public byte ReadByte()
            => ReadBytes(1)[0];

        public short ReadInt16()
            => BinaryPrimitives.ReadInt16LittleEndian(ReadBytes(2));

        public ushort ReadUInt16()
            => BinaryPrimitives.ReadUInt16LittleEndian(ReadBytes(2));

        public int ReadInt32()
            => BinaryPrimitives.ReadInt32LittleEndian(ReadBytes(4));

        public uint ReadUInt32()
            => BinaryPrimitives.ReadUInt32LittleEndian(ReadBytes(4));

        public long ReadInt64()
           => BinaryPrimitives.ReadInt64LittleEndian(ReadBytes(8));

        public ulong ReadUInt64()
            => BinaryPrimitives.ReadUInt64LittleEndian(ReadBytes(8));

        public float ReadFloat()
            => BitConverter.Int32BitsToSingle(ReadInt32());

        public double ReadDouble()
            => BitConverter.Int64BitsToDouble(ReadInt64());

        public int Read7BitEncodedInt()
        {
            var (value, size) = PackedInt.Read7BitEncodedInt(buffer);
            Advance(size);
            return value;
        }

        public string ReadString(Encoding encoding)
        {
            var data = ReadBytes(Read7BitEncodedInt());
            return encoding.GetString(data);
        }

        public ReadOnlySpan<byte> ReadBytes(int size)
        {
            var result = buffer.Slice(0, size);
            buffer = buffer.Slice(size);
            return result;
        }

        public void Advance(int size) => buffer = buffer.Slice(size);
    }
}
