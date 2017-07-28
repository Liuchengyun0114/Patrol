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
    /// 用户基础信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class UserBaseInfo
    {
        [DataMember(Name = "agency_shop_list")]
        public List<NodeInfo> agency_shop_list { get; set; }

        [DataMember(Name = "filiale_list")]
        public List<NodeInfo> filiale_list { get; set; }
    }
}
