using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.Patrol.Response.Entity
{
    /// <summary>
    /// 节点树映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class NodeInfo
    {
        [DataMember(Name = "code")]
        public string code { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "sub_list")]
        public List<NodeInfo> sub_list { get; set; }
    }
}
