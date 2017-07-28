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
    /// 获取代理店用户数据 接口编号=1002
    /// </summary>
    [DataContract]
    [XmlRoot("root")]
    public class GetUserListMsgResponse:BaseResponseMessage
    {
        /// <summary>
        /// 用户列表总条数
        /// </summary>
        private int _itemCount;
        [DataMember]
        [XmlElement("itemCount")]
        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        private List<UserInfo> _userList;
        [DataMember]
        [XmlElement("list")]
        public List<UserInfo> UserList
        {
            get { return _userList; }
            set { _userList = value; }
        }
    }

    /// <summary>
    /// 用户信息结构
    /// </summary>
    [DataContract]
    [XmlRoot("root")]
    public class UserInfo {
        /// <summary>
        /// 用户名
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
        /// 密码
        /// </summary>
        private string _password;
        [DataMember]
        [XmlElement("password")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        private string _userName;
        [DataMember]
        [XmlElement("userName")]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }        
    }

}
