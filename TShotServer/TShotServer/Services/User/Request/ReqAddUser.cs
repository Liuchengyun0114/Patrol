using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;
using PatrolServer.Services.User.Request.Entity;

namespace PatrolServer.Services.User.Request
{
    /// <summary>
    /// 从建友系统新增用户到特巡系统 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqAddUser
    {

        [DataMember(Name = "account")]
        public string account { get; set; }

        [DataMember(Name = "token")]
        public string token { get; set; }

        [DataMember(Name = "user")]
        public UserInfo user { get; set; }
    }
}
