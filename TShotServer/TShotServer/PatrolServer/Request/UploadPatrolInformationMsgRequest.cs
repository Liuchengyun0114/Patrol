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
    /// 上传特巡  接口编号=1005
    /// </summary>
    /// 
    [DataContract]
    [XmlRoot("root")]
    public class UploadPatrolInformationMsgRequest:BaseRequestMessage
    {
    }
}
