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
    /// 修改密码  接口编号=1001
    /// </summary>
    [DataContract]
    [XmlRoot("user")]
    public class UpdatePasswordMsgRequest:BaseRequestMessage
    {
        /// <summary>
        /// 登录的用户名
        /// </summary>
        private string _userid;
        [DataMember]
        [XmlElement("userid")]
        public string Userid
        {
            get { return _userid; }
            set { _userid = value; }
        }

        /// <summary>
        /// 旧密码
        /// </summary>
        private string _oldPsw;
        [DataMember]
        [XmlElement("oldPsw")]
        public string OldPsw
        {
            get { return _oldPsw; }
            set { _oldPsw = value; }
        }

        /// <summary>
        /// 新密码
        /// </summary>
        private string _newPsw;
        [DataMember]
        [XmlElement("newPsw")]
        public string NewPsw
        {
            get { return _newPsw; }
            set { _newPsw = value; }
        }
    }
}
