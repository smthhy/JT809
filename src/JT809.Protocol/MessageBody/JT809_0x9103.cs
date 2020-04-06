﻿using JT809.Protocol.Enums;
using JT809.Protocol.Extensions;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using JT809.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using JT809.Protocol.Metadata;

namespace JT809.Protocol.MessageBody
{
    /// <summary>
    ///  下发平台间消息序列号通知消息
    /// <para>链路类型：从链路</para>
    /// <para>消息方向:上级平台往下级平台</para>
    /// <para>业务类型标识:DOWN_MANAGE_MSG_SN_INFORM</para>
    /// </summary>
    public class JT809_0x9103 : JT809ExchangeMessageBodies, IJT809MessagePackFormatter<JT809_0x9103>,IJT809_2019_Version
    {
        public override ushort MsgId => JT809BusinessType.下发平台间消息序列号通知消息_2019.ToUInt16Value();
        public override string Description => "下发平台间消息序列号通知消息";
        public override JT809_LinkType LinkType => JT809_LinkType.subordinate;
        public override JT809Version Version => JT809Version.JTT2019;

        public List<JT809ManageMsgSNInform> ManageMsgSNInform { get; set; } = new List<JT809ManageMsgSNInform>();
        public byte Count { get; set; }

        public JT809_0x9103 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_0x9103 value = new JT809_0x9103();
            value.SubBusinessType = reader.ReadUInt16();
            value.DataLength = reader.ReadUInt32();
            value.Count = reader.ReadByte();
            for(int i=0;i < value.Count; i++)
            {
                JT809ManageMsgSNInform item = new JT809ManageMsgSNInform();
                item.SubBusinessType = reader.ReadUInt16();
                item.MsgSN = reader.ReadUInt32();
                item.Time = reader.ReadUTCDateTime();
                value.ManageMsgSNInform.Add(item);
            }
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_0x9103 value, IJT809Config config)
        {
            writer.WriteUInt16(value.SubBusinessType);
            // 先写入内容，然后在根据内容反写内容长度
            writer.Skip(4, out int subContentLengthPosition);
            writer.WriteByte((byte)value.ManageMsgSNInform.Count);
            foreach(var item in value.ManageMsgSNInform)
            {
                writer.WriteUInt16(item.SubBusinessType);
                writer.WriteUInt32(item.MsgSN);
                writer.WriteUTCDateTime(item.Time);
            }
            writer.WriteInt32Return(writer.GetCurrentPosition() - subContentLengthPosition - 4, subContentLengthPosition);
        }
    }
}
