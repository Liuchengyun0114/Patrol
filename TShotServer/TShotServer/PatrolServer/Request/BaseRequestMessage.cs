using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace PatrolServer.Message.Request
{
    /// <summary>
    /// 基本请求消息
    /// </summary>
    [DataContract]
    [XmlRoot("root")]
    public class BaseRequestMessage:IDisposable
    {
        public Stream Data { get; set; }

        public void Dispose()
        {
            if (Data != null)
            {
                Data.Dispose();
            }
        }
    }
}
