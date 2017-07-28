using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.User.Request
{
    /// <summary>
    /// 登录验证 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqLoginCheck
    {
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "password")]
        public string password { get; set; }
    }
}
