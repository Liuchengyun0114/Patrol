using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.User.Request.Entity
{
    /// <summary>
    /// 用户权限信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class UserInfo
    {
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "password")]
        public string password { get; set; }

        [DataMember(Name = "isadmin")]
        public string isadmin { get; set; }

        [DataMember(Name = "search_range")]
        public string search_range { get; set; }

        [DataMember(Name = "creator")]
        public string creator { get; set; }

        [DataMember(Name = "updator")]
        public string updator { get; set; }
    }
}
