﻿using JT809.Protocol.JT809Attributes;
using JT809.Protocol.JT809Enums;
using JT809.Protocol.JT809Exceptions;
using JT809.Protocol.JT809Extensions;
using JT809.Protocol.JT809MessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.JT809Formatters.JT809MessageBodyFormatters
{
    public class JT809_0x9300Formatter : IJT809Formatter<JT809_0x9300>
    {
        public JT809_0x9300 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT809_0x9300 jT809_0X9300 = new JT809_0x9300();
            jT809_0X9300.SubBusinessType = (JT809SubBusinessType)JT809BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT809_0X9300.DataLength = JT809BinaryExtensions.ReadUInt32Little(bytes, ref offset);
            //JT809.Protocol.JT809Enums.JT809BusinessType 映射对应消息特性
            JT809BodiesTypeAttribute jT809SubBodiesTypeAttribute = jT809_0X9300.SubBusinessType.GetAttribute<JT809BodiesTypeAttribute>();
            if (jT809SubBodiesTypeAttribute == null)
            {
                throw new JT809Exception(JT809ErrorCode.GetAttributeError, $"JT809BodiesTypeAttribute Not Found>{jT809_0X9300.SubBusinessType.ToString()}");
            }
            try
            {
                jT809_0X9300.JT809SubBodies = JT809FormatterResolverExtensions.JT809DynamicDeserialize(JT809FormatterExtensions.GetFormatter(jT809SubBodiesTypeAttribute.JT809BodiesType), bytes.Slice(offset, (int)jT809_0X9300.DataLength), out readSize);
            }
            catch
            {
                throw new JT809Exception(JT809ErrorCode.SubBodiesParseError, $"SubBusinessType>{jT809_0X9300.SubBusinessType.ToString()}");
            }
            readSize = offset;
            return jT809_0X9300;
        }

        public int Serialize(IMemoryOwner<byte> memoryOwner, int offset, JT809_0x9300 value)
        {
            offset += JT809BinaryExtensions.WriteUInt16Little(memoryOwner, offset, (ushort)value.SubBusinessType);
            offset += JT809BinaryExtensions.WriteUInt32Little(memoryOwner, offset, value.DataLength);
            //JT809.Protocol.JT809Enums.JT809BusinessType 映射对应消息特性
            JT809BodiesTypeAttribute jT809SubBodiesTypeAttribute = value.SubBusinessType.GetAttribute<JT809BodiesTypeAttribute>();
            if (jT809SubBodiesTypeAttribute == null)
            {
                throw new JT809Exception(JT809ErrorCode.GetAttributeError, $"JT809BodiesTypeAttribute Not Found>{value.SubBusinessType.ToString()}");
            }
            try
            {
                offset = JT809FormatterResolverExtensions.JT809DynamicSerialize(JT809FormatterExtensions.GetFormatter(jT809SubBodiesTypeAttribute.JT809BodiesType), memoryOwner, offset, value.JT809SubBodies);
            }
            catch
            {
                throw new JT809Exception(JT809ErrorCode.SubBodiesParseError, $"SubBusinessType>{value.SubBusinessType.ToString()}");
            }
            return offset;
        }
    }
}