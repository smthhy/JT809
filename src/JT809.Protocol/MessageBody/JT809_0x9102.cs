﻿using JT809.Protocol.Enums;
using JT809.Protocol.Extensions;
using JT809.Protocol.Formatters;
using JT809.Protocol.MessagePack;
using JT809.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.MessageBody
{
    /// <summary>
    ///  平台链路连接情况与车辆定位消息传输情况上报请求消息
    /// <para>链路类型：从链路</para>
    /// <para>消息方向:上级平台往下级平台</para>
    /// <para>业务类型标识:DOWN_MANAGE_MSG_REQ</para>
    /// </summary>
    public class JT809_0x9102 : JT809ExchangeMessageBodies, IJT809MessagePackFormatter<JT809_0x9102>,IJT809_2019_Version
    {
        public override ushort MsgId => JT809BusinessType.平台链路连接情况与车辆定位消息传输情况上报请求消息_2019.ToUInt16Value();
        public override string Description => "平台链路连接情况与车辆定位消息传输情况上报请求消息";
        public override JT809_LinkType LinkType => JT809_LinkType.subordinate;

        public override JT809Version Version => JT809Version.JTT2019;

        /// <summary>
        /// 平台唯一编码
        /// </summary>
        public string PlateformId { get; set; }
        /// <summary>
        /// 开始时间，用 UTC 时间表示
        /// 注：采用 UTC 时间表示，如 2010-1-10 9:7:54 的 UTC 值为 1263085674，其在协议中表示为0x000000004B49286A.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间，用 UTC 时间表示
        /// 注：采用 UTC 时间表示，如 2010-1-10 9:7:54 的 UTC 值为 1263085674，其在协议中表示为0x000000004B49286A.
        /// </summary>
        public DateTime EndTime { get; set; }
        public JT809_0x9102 Deserialize(ref JT809MessagePackReader reader, IJT809Config config)
        {
            JT809_0x9102 value = new JT809_0x9102();
            value.SubBusinessType = reader.ReadUInt16();
            value.DataLength = reader.ReadUInt32();
            value.PlateformId = reader.ReadBigNumber(11);
            value.StartTime = reader.ReadUTCDateTime();
            value.EndTime = reader.ReadUTCDateTime();
            return value;
        }

        public void Serialize(ref JT809MessagePackWriter writer, JT809_0x9102 value, IJT809Config config)
        {
            writer.WriteUInt16(value.SubBusinessType);
            writer.WriteUInt32(27);
            writer.WriteBigNumber(value.PlateformId, 11);
            writer.WriteUTCDateTime(value.StartTime);
            writer.WriteUTCDateTime(value.EndTime);
        }
    }
}
