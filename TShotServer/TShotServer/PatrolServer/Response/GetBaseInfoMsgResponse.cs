using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text;

namespace PatrolServer.Message.Response
{
    /// <summary>
    /// 获取基础数据 接口编号=1003
    /// </summary>
    [DataContract]
    [XmlRoot("root")]
    public class GetBaseInfoMsgResponse:BaseResponseMessage
    {
        /// <summary>
        /// 编号
        /// </summary>
        private string _code;
        [DataMember]
        [XmlElement("code")]
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        private string _name;
        [DataMember]
        [XmlElement("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
