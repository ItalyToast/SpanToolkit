using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Schema;

namespace SpanToolkit
{
    /// <summary>
    /// Reads primitive data types as binary values from a span and pushes the pointer forward.
    /// </summary>
    public ref struct SpanReader
    {
        ReadOnlySpan<byte> buffer;

        public ReadOnlySpan<byte> Current => buffer;

        public int Length => buffer.Length;

        public SpanReader(ReadOnlySpan<byte> buffer)
        {
            this.buffer = buffer;
        }

        public bool ReadBoolean()
            => ReadBytes(1)[0] != 0;

        public sbyte ReadSByte()
            => (sbyte)ReadBytes(1)[0];

        public byte ReadByte()
           => ReadBytes(1)[0];

        public short ReadInt16BigEndian()
            => BinaryPrimitives.ReadInt16BigEndian(ReadBytes(2));

        public short ReadInt16LittleEndian()
            => BinaryPrimitives.ReadInt16LittleEndian(ReadBytes(2));

        public int ReadInt32BigEndian()
            => BinaryPrimitives.ReadInt32BigEndian(ReadBytes(4));

        public int ReadInt32LittleEndian()
            => BinaryPrimitives.ReadInt32LittleEndian(ReadBytes(4));

        public long ReadInt64BigEndian()
            => BinaryPrimitives.ReadInt64BigEndian(ReadBytes(8));

        public long ReadInt64LittleEndian()
            => BinaryPrimitives.ReadInt64LittleEndian(ReadBytes(8));

        public ushort ReadUInt16BigEndian()
            => BinaryPrimitives.ReadUInt16BigEndian(ReadBytes(2));

        public ushort ReadUInt16LittleEndian()
            => BinaryPrimitives.ReadUInt16LittleEndian(ReadBytes(2));

        public uint ReadUInt32BigEndian()
            => BinaryPrimitives.ReadUInt32BigEndian(ReadBytes(4));

        public uint ReadUInt32LittleEndian()
            => BinaryPrimitives.ReadUInt32LittleEndian(ReadBytes(4));

        public ulong ReadUInt64BigEndian()
            => BinaryPrimitives.ReadUInt64BigEndian(ReadBytes(8));

        public ulong ReadUInt64LittleEndian()
            => BinaryPrimitives.ReadUInt64LittleEndian(ReadBytes(8));

        public float ReadFloatBigEndian()
            => BitConverter.Int32BitsToSingle(ReadInt32BigEndian());

        public float ReadFloatLittleEndian()
            => BitConverter.Int32BitsToSingle(ReadInt32LittleEndian());

        public double ReadDoubleBigEndian()
            => BitConverter.Int64BitsToDouble(ReadInt64BigEndian());

        public double ReadDoubleLittleEndian()
            => BitConverter.Int64BitsToDouble(ReadInt64LittleEndian());

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
