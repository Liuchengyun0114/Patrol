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
    /// 用户检索信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class UserSearchInfo
    {
        [DataMember(Name = "agency_shop")]
        public string agency_shop { get; set; }

        [DataMember(Name = "filiale")]
        public string filiale { get; set; }

        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "user_name")]
        public string user_name { get; set; }

        [DataMember(Name = "Paginator")]
        public PaginatorInfo Paginator { get; set; }
    }
}
