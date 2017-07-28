using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.User.Response.Entity
{
    /// <summary>
    /// 用户信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class UserInfo
    {
        [DataMember(Name = "order_no")]
        public string order_no { get; set; }

        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "department")]
        public string department { get; set; }

        [DataMember(Name = "agency_shop")]
        public string agency_shop { get; set; }

        [DataMember(Name = "filiale")]
        public string filiale { get; set; }

        [DataMember(Name = "isadmin")]
        public string isadmin { get; set; }

        [DataMember(Name = "search_range")]
        public string search_range { get; set; }

        [DataMember(Name = "is_patrol_user")]
        public bool is_patrol_user { get; set; }
    }
}
