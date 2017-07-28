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
    /// 根据Id获取单个用户信息 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqGetUser
    {
        [DataMember(Name = "account")]
        public string account { get; set; }

        [DataMember(Name = "token")]
        public string token { get; set; }

        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

    }
}
