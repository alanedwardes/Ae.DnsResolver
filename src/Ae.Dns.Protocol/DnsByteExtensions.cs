﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.Dns.Protocol
{
    /// <summary>
    /// Provides extension methods around reading and writing <see cref="byte"/> buffers.
    /// </summary>
    public static class DnsByteExtensions
    {
        private static short ReadInt16(params byte[] bytes)
        {
            return (short)(bytes[0] << 0 |
                           bytes[1] << 8);
        }

        internal static ushort ReadUInt16(params byte[] bytes) => (ushort)ReadInt16(bytes);

        internal static short ReadInt16(byte[] bytes, ref int offset)
        {
            var value = ReadInt16(bytes[offset + 1], bytes[offset + 0]);
            offset += sizeof(short);
            return value;
        }

        /// <summary>
        /// Read a <see cref="ushort"/> from the specified byte array, incrementing offset by the length of the data read.
        /// </summary>
        /// <param name="bytes">The buffer to read from.</param>
        /// <param name="offset">The offset to increment.</param>
        /// <returns>The read unsigned short.</returns>
        public static ushort ReadUInt16(byte[] bytes, ref int offset) => (ushort)ReadInt16(bytes, ref offset);

        internal static int ReadInt32(byte[] bytes, ref int offset)
        {
            var value = bytes[offset + 3] << 0 |
                        bytes[offset + 2] << 8 |
                        bytes[offset + 1] << 16 |
                        bytes[offset + 0] << 24;
            offset += sizeof(int);
            return value;
        }

        internal static uint ReadUInt32(byte[] bytes, ref int offset) => (uint)ReadInt32(bytes, ref offset);

        /// <summary>
        /// Convert the specified byte array to a C# statement to initialise it in code.
        /// </summary>
        /// <param name="bytes">The byte array to convert.</param>
        /// <returns>The new array statement for use in C# code.</returns>
        public static string ToDebugString(IEnumerable<byte> bytes)
        {
            if (bytes == null)
            {
                return "<null>";
            }

            return $"new [] {{{string.Join(", ", bytes)}}}";
        }

        /// <summary>
        /// Read the specified number of bytes from the buffer, incrementing offset by the length of the data read.
        /// </summary>
        /// <param name="bytes">The buffer to read from.</param>
        /// <param name="length">The number of byes to read.</param>
        /// <param name="offset">The offset to increment.</param>
        /// <returns></returns>
        public static byte[] ReadBytes(byte[] bytes, int length, ref int offset)
        {
            var data = new byte[length];
            Array.Copy(bytes, offset, data, 0, length);
            offset += length;
            return data;
        }

        internal static string[] ReadString(byte[] bytes, ref int offset, int? maxOffset = int.MaxValue)
        {
            var parts = new List<string>();

            int? originalOffset = null;
            while (offset < bytes.Length && offset < maxOffset)
            {
                byte currentByte = bytes[offset];

                var bits = new BitArray(new[] { currentByte });

                bool isCompressed = bits[7] && bits[6] && !bits[5] && !bits[4];
                bool isEnd = currentByte == 0;

                if (isCompressed)
                {
                    offset++;
                    if (!originalOffset.HasValue)
                    {
                        originalOffset = offset;
                    }

                    offset = (ushort)ReadInt16(bytes[offset], (byte)(currentByte & (1 << 6) - 1));
                }
                else if (isEnd)
                {
                    if (originalOffset.HasValue)
                    {
                        offset = originalOffset.Value;
                    }
                    offset++;
                    break;
                }
                else
                {
                    offset++;
                    var str = Encoding.ASCII.GetString(bytes, offset, currentByte);
                    parts.Add(str);
                    offset += currentByte;
                }
            }

            return parts.ToArray();
        }

        /// <summary>
        /// Serialise the specified <see cref="IDnsByteArrayWriter"/> to a buffer.
        /// </summary>
        /// <param name="writer">The instance to serialise to bytes.</param>
        /// <returns>The serialised byte array.</returns>
        public static byte[] ToBytes(IDnsByteArrayWriter writer)
        {
            return writer.WriteBytes().SelectMany(x => x).ToArray();
        }

        /// <summary>
        /// Deserialise the specified bytes to the supplied type.
        /// </summary>
        /// <typeparam name="TReader">The type to create from the byte array.</typeparam>
        /// <param name="bytes">The buffer to read from.</param>
        /// <returns>An instance of the type, created from the byte array.</returns>
        public static TReader FromBytes<TReader>(byte[] bytes) where TReader : IDnsByteArrayReader, new()
        {
            var offset = 0;
            return FromBytes<TReader>(bytes, ref offset);
        }

        internal static TReader FromBytes<TReader>(byte[] bytes, ref int offset) where TReader : IDnsByteArrayReader, new()
        {
            var reader = new TReader();
            reader.ReadBytes(bytes, ref offset);
            return reader;
        }

        /// <summary>
        /// Serialise the specified string array to a byte array.
        /// </summary>
        /// <param name="strings">The specified strings to serialise.</param>
        /// <returns>An enumerable of bytes representing the supplied strings.</returns>
        public static IEnumerable<byte> ToBytes(string[] strings)
        {
            foreach (var str in strings)
            {
                yield return (byte)str.Length;
                foreach (var c in str)
                {
                    yield return (byte)c;
                }
            }

            yield return 0;
        }

        /// <summary>
        /// Serialise the specified <see cref="IConvertible"/> value to a byte array.
        /// </summary>
        /// <param name="value">The specified value to convert to a byte array.</param>
        /// <returns>An enumerable of bytes representing the supplied <see cref="IConvertible"/> value.</returns>
        public static IEnumerable<byte> ToBytes(IConvertible value)
        {
            var typeCode = Type.GetTypeCode(value.GetType());
            switch (typeCode)
            {
                case TypeCode.Int32:
                    var int32 = (int)value;
                    yield return (byte)(int32 >> 24);
                    yield return (byte)(int32 >> 16);
                    yield return (byte)(int32 >> 8);
                    yield return (byte)(int32 >> 0);
                    break;
                case TypeCode.UInt32:
                    var uint32 = (uint)value;
                    yield return (byte)(uint32 >> 24);
                    yield return (byte)(uint32 >> 16);
                    yield return (byte)(uint32 >> 8);
                    yield return (byte)(uint32 >> 0);
                    break;
                case TypeCode.Int16:
                    var uint16 = (short)value;
                    yield return (byte)(uint16 >> 8);
                    yield return (byte)(uint16 >> 0);
                    break;
                case TypeCode.UInt16:
                    var int16 = (ushort)value;
                    yield return (byte)(int16 >> 8);
                    yield return (byte)(int16 >> 0);
                    break;
                default:
                    throw new NotImplementedException($"Unable to process type {typeCode} ({value})");
            }
        }
    }
}
