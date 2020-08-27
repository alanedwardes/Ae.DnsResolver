﻿using Ae.DnsResolver.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace Ae.DnsResolver.Protocol
{
    public static class ByteExtensions
    {
        public static short ReadInt16(this byte[] bytes, ref int offset)
        {
            var value = bytes[offset + 1] << 0 |
                        bytes[offset + 0] << 8;
            offset += sizeof(short);
            return (short)value;
        }

        public static ushort ReadUInt16(this byte[] bytes, ref int offset) => (ushort)bytes.ReadInt16(ref offset);

        public static int ReadInt32(this byte[] bytes, ref int offset)
        {
            var value = bytes[offset + 3] << 0 |
                        bytes[offset + 2] << 8 |
                        bytes[offset + 1] << 16 |
                        bytes[offset + 0] << 24;
            offset += sizeof(int);
            return value;
        }

        public static uint ReadUInt32(this byte[] bytes, ref int offset) => (uint)bytes.ReadInt32(ref offset);

        public static string ToDebugString(this IEnumerable<byte> bytes)
        {
            if (bytes == null)
            {
                return "<null>";
            }

            return $"new [] {{{string.Join(", ", bytes)}}}";
        }

        public static byte[] ReadBytes(this byte[] bytes, int length, ref int offset)
        {
            var data = new byte[length];
            Array.Copy(bytes, offset, data, 0, length);
            offset += length;
            return data;
        }

        public static string[] ReadString(this byte[] bytes, ref int offset)
        {
            var parts = new List<string>();

            int? compressionOffset = null;
            while (offset < bytes.Length)
            {
                // get segment length or detect termination of segments
                int segmentLength = bytes[offset];

                // compressed name
                if (((DnsLabelType)segmentLength).HasFlag(DnsLabelType.Compressed))
                {
                    offset++;
                    if (!compressionOffset.HasValue)
                    {
                        // only record origin, and follow all pointers thereafter
                        compressionOffset = offset;
                    }

                    var mask = (1 << 14) - 1;
                    var pointerBytes = new byte[] { (byte)segmentLength, bytes[offset] };
                    var pointerOffset = 0;
                    var pointer = pointerBytes.ReadUInt16(ref pointerOffset) & mask;

                    if (segmentLength > (byte)DnsLabelType.Compressed)
                    {
                        offset = pointer;
                        segmentLength = bytes[offset];
                    }
                    else
                    {
                        // move pointer to compression segment
                        offset = bytes[offset];
                        segmentLength = bytes[offset];
                    }
                }

                if (segmentLength == (byte)DnsLabelType.Normal)
                {
                    if (compressionOffset.HasValue)
                    {
                        offset = compressionOffset.Value;
                    }
                    // move past end of name \0
                    offset++;
                    break;
                }

                // move pass length and get segment text
                offset++;
                parts.Add(Encoding.ASCII.GetString(bytes, offset, segmentLength));
                offset += segmentLength;
            }

            return parts.ToArray();
        }

        public static DnsHeader ReadDnsHeader(this byte[] bytes, ref int offset)
        {
            var header = new DnsHeader();
            header.Id = bytes.ReadUInt16(ref offset);
            header.Flags = bytes.ReadUInt16(ref offset);
            header.QuestionCount = bytes.ReadInt16(ref offset);
            header.AnswerRecordCount = bytes.ReadInt16(ref offset);
            header.NameServerRecordCount = bytes.ReadInt16(ref offset);
            header.AdditionalRecordCount = bytes.ReadInt16(ref offset);
            header.Labels = bytes.ReadString(ref offset);
            header.QueryType = (DnsQueryType)bytes.ReadUInt16(ref offset);
            header.QueryClass = (DnsQueryClass)bytes.ReadUInt16(ref offset);
            return header;
        }

        public static DnsAnswer ReadDnsAnswer(this byte[] bytes, ref int offset)
        {
            var result = new DnsAnswer();
            result.Header = bytes.ReadDnsHeader(ref offset);

            var records = new List<DnsResourceRecord>();
            for (var i = 0; i < result.Header.AnswerRecordCount + result.Header.NameServerRecordCount; i++)
            {
                records.Add(ReadDnsResourceRecord(bytes, ref offset));
            }
            result.Answers = records.ToArray();
            return result;
        }

        private static DnsResourceRecord ReadDnsResourceRecord(byte[] bytes, ref int offset)
        {
            var recordName = bytes.ReadString(ref offset);
            var recordType = (DnsQueryType)bytes.ReadUInt16(ref offset);
            var recordClass = (DnsQueryClass)bytes.ReadUInt16(ref offset);
            var recordTtl = bytes.ReadUInt32(ref offset);
            var recordDataLength = bytes.ReadUInt16(ref offset);
            var recordDataOffset = offset;

            var dataOffset = offset;

            DnsResourceRecord record;
            switch (recordType)
            {
                case DnsQueryType.A:
                case DnsQueryType.AAAA:
                    record = new DnsIpAddressRecord
                    {
                        IPAddress = new IPAddress(bytes.ReadBytes(recordDataLength, ref dataOffset))
                    };
                    break;
                case DnsQueryType.TEXT:
                case DnsQueryType.CNAME:
                case DnsQueryType.NS:
                case DnsQueryType.PTR:
                    record = new DnsTextRecord
                    {
                        Text = string.Join(".", bytes.ReadString(ref dataOffset))
                    };
                    break;
                case DnsQueryType.SOA:
                    record = new DnsSoaRecord
                    {
                        MName = string.Join(".", bytes.ReadString(ref dataOffset)),
                        RName = string.Join(".", bytes.ReadString(ref dataOffset)),
                        Serial = bytes.ReadUInt32(ref dataOffset),
                        Refresh = bytes.ReadInt32(ref dataOffset),
                        Retry = bytes.ReadInt32(ref dataOffset),
                        Expire = bytes.ReadInt32(ref dataOffset),
                        Minimum = bytes.ReadUInt32(ref dataOffset)
                    };
                    break;
                case DnsQueryType.MX:
                    record = new DnsMxRecord
                    {
                        Preference = bytes.ReadUInt16(ref dataOffset),
                        Exchange = string.Join(".", bytes.ReadString(ref dataOffset))
                    };
                    break;
                default:
                    record = new UnimplementedDnsResourceRecord();
                    break;
            }

            record.Name = recordName;
            record.Type = recordType;
            record.Class = recordClass;
            record.Ttl = recordTtl;
            record.DataLength = recordDataLength;
            record.DataOffset = recordDataOffset;

            offset += record.DataLength;

            return record;
        }

        public static IEnumerable<byte> ToBytes(this string[] strings)
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

        public static IEnumerable<byte> ToBytes(this object value)
        {
            var typeCode = Type.GetTypeCode(value.GetType());
            switch (typeCode)
            {
                case TypeCode.Int32:
                    var int32 = (int)value;
                    yield return (byte) (int32 >> 24);
                    yield return (byte) (int32 >> 16);
                    yield return (byte) (int32 >> 8);
                    yield return (byte) (int32 >> 0);
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

        public static IEnumerable<byte> WriteDnsHeader(this DnsHeader header)
        {
            IEnumerable<IEnumerable<byte>> Write()
            {
                yield return header.Id.ToBytes();
                yield return header.Flags.ToBytes();
                yield return header.QuestionCount.ToBytes();
                yield return header.AnswerRecordCount.ToBytes();
                yield return header.NameServerRecordCount.ToBytes();
                yield return header.AdditionalRecordCount.ToBytes();
                yield return header.Labels.ToBytes();
                yield return header.QueryType.ToBytes();
                yield return header.QueryClass.ToBytes();
            }

            return Write().SelectMany(x => x);
        }

        public static IEnumerable<byte> WriteDnsAnswer(this DnsAnswer answer)
        {
            IEnumerable<IEnumerable<byte>> Write(DnsResourceRecord resourceRecord)
            {
                yield return resourceRecord.Name.ToBytes();
                yield return resourceRecord.Type.ToBytes();
                yield return resourceRecord.Class.ToBytes();
                yield return resourceRecord.Ttl.ToBytes();
            }

            var header = answer.Header.WriteDnsHeader();
            var answers = answer.Answers.Select(Write).SelectMany(x => x).SelectMany(x => x);
            return header.Concat(answers);
        }
    }
}
