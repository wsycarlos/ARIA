using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Thrift.Protocol;
using Thrift.Transport;

namespace Limbo.Net
{
    public static class NetCoding
    {
        public static byte[] Serialize(NetMsg msg)
        {
            MemoryStream stream = new MemoryStream();
            TCompactProtocol protocol = new TCompactProtocol(new TStreamTransport(stream, stream));
            msg.Write(protocol);
            return stream.ToArray();
        }

        public static NetMsg Deserialize(byte[] byteArr)
        {
            MemoryStream stream = new MemoryStream(byteArr);
            TCompactProtocol protocol = new TCompactProtocol(new TStreamTransport(stream, stream));
            NetMsg _msg = new NetMsg();
            _msg.Read(protocol);
            return _msg;
        }
    }
}