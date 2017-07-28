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
    /// 修改密码 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqUpdatePassword
    {
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "old_pwd")]
        public string old_pwd { get; set; }

        [DataMember(Name = "new_pwd")]
        public string new_pwd { get; set; }
    }
}
