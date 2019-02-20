﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT809.Protocol;
using JT809.Protocol.Extensions;
using JT809.Protocol.MessageBody;
using JT809.Protocol.Exceptions;
using JT809.Protocol.SubMessageBody;
using JT809.Protocol.Enums;
using JT809.Protocol.Metadata;

namespace JT809.Protocol.Test.JT809SubMessageBody
{
    public class JT809_0x1500_0x1502Test
    {
        [Fact]
        public void Test1()
        {
            JT809_0x1500_0x1502 jT809_0x1500_0x1502 = new JT809_0x1500_0x1502
            {
                    PhotoRspFlag= JT809_0x1502_PhotoRspFlag.完成拍照,
                    VehiclePosition=new JT809VehiclePositionProperties
                    {
                         Encrypt= JT809_VehiclePositionEncrypt.未加密,
                         Day=19,
                         Month=7,
                         Year=2012,
                         Hour=15,
                         Minute=15,
                         Second=15,
                         Lon= 133123456,
                         Lat= 24123456,
                         Vec1=53,
                         Vec2=45,
                         Vec3=1234,
                         Direction=45,
                         Altitude=45,
                         State=1,
                         Alarm=1
                    },
                     LensID=123,
                     SizeType=1,
                     Type=1,
            };
            var hex = JT809Serializer.Serialize(jT809_0x1500_0x1502).ToHexString();
            //"01 00 13 07 07 DC 0F 0F 0F 07 EF 4D 80 01 70 18 40 00 35 00 2D 00 00 04 D2 00 2D 00 2D 00 00 00 01 00 00 00 01 7B 00 00 00 00 01 01"
            Assert.Equal("0100130707DC0F0F0F07EF4D80017018400035002D000004D2002D002D00000001000000017B000000000101", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "01 00 13 07 07 DC 0F 0F 0F 07 EF 4D 80 01 70 18 40 00 35 00 2D 00 00 04 D2 00 2D 00 2D 00 00 00 01 00 00 00 01 7B 00 00 00 00 01 01".ToHexBytes();
            JT809_0x1500_0x1502 jT809_0x1500_0x1502 = JT809Serializer.Deserialize<JT809_0x1500_0x1502>(bytes);
            Assert.Equal(JT809_0x1502_PhotoRspFlag.完成拍照, jT809_0x1500_0x1502.PhotoRspFlag);
            Assert.Equal(JT809_VehiclePositionEncrypt.未加密, jT809_0x1500_0x1502.VehiclePosition.Encrypt);
            Assert.Equal(19,  jT809_0x1500_0x1502.VehiclePosition.Day);
            Assert.Equal(7,  jT809_0x1500_0x1502.VehiclePosition.Month);
            Assert.Equal(2012,  jT809_0x1500_0x1502.VehiclePosition.Year);
            Assert.Equal(15,  jT809_0x1500_0x1502.VehiclePosition.Hour);
            Assert.Equal(15,  jT809_0x1500_0x1502.VehiclePosition.Minute);
            Assert.Equal(15, jT809_0x1500_0x1502.VehiclePosition.Second);
            Assert.Equal((uint)133123456, jT809_0x1500_0x1502.VehiclePosition.Lon);
            Assert.Equal((uint)24123456, jT809_0x1500_0x1502.VehiclePosition.Lat);
            Assert.Equal((ushort)53, jT809_0x1500_0x1502.VehiclePosition.Vec1);
            Assert.Equal((ushort)45, jT809_0x1500_0x1502.VehiclePosition.Vec2);
            Assert.Equal((uint)1234, jT809_0x1500_0x1502.VehiclePosition.Vec3);
            Assert.Equal((ushort)45, jT809_0x1500_0x1502.VehiclePosition.Direction);
            Assert.Equal((ushort)45, jT809_0x1500_0x1502.VehiclePosition.Altitude);
            Assert.Equal((uint)1, jT809_0x1500_0x1502.VehiclePosition.State);
            Assert.Equal((uint)1, jT809_0x1500_0x1502.VehiclePosition.Alarm);
            Assert.Equal(123, jT809_0x1500_0x1502.LensID);
            Assert.Equal(1, jT809_0x1500_0x1502.SizeType);
            Assert.Equal(1, jT809_0x1500_0x1502.Type);
        }
    }
}
