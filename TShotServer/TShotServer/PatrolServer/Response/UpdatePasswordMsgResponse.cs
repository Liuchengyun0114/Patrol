using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace PatrolServer.Message.Response
{
    /// <summary>
    /// 修改密码  接口编号=1001
    /// </summary>
    [DataContract]
    [XmlRoot("root")]
    public class UpdatePasswordMsgResponse:BaseResponseMessage
    {
    }
}
