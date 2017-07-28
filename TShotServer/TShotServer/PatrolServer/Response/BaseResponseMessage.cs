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
    /// 基本响应消息
    /// </summary>
    [DataContract]
    [XmlRoot("root")]
    public class BaseResponseMessage
    {
        private string _functionId;

        [DataMember]
        [XmlElement("function_id")]
        public string FunctionId
        {
            get { return _functionId; }
            set { _functionId = value; }
        }
        private string _returnFlag;

        [DataMember]
        [XmlElement("return_flag")]
        public string ReturnFlag
        {
            get { return _returnFlag; }
            set { _returnFlag = value; }
        }
        private string _returnmsg;  

        [DataMember]
        [XmlElement("return_msg")]
        public string Returnmsg
        {
            get { return _returnmsg; }
            set { _returnmsg = value; }
        }
    }
}
