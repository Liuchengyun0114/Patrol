using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace PatrolServer.Message.Request
{
    /// <summary>
    /// 获取代理店用户数据 接口编号=1002
    /// </summary>
    [DataContract]
    [XmlRoot("root")]
    public class GetUserListMsgRequest:BaseRequestMessage
    {
    }
}
