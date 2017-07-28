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
    /// 特巡报告基础信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class PatrolBaseInfo
    {
        [DataMember(Name = "agency_shop_list")]
        public List<NodeInfo> agency_shop_list { get; set; }

        [DataMember(Name = "filiale_list")]
        public List<NodeInfo> filiale_list { get; set; }

        [DataMember(Name = "reporter_list")]
        public List<NodeInfo> reporter_list { get; set; }

        [DataMember(Name = "machine_type_list")]
        public List<NodeInfo> machine_type_list { get; set; }

        [DataMember(Name = "machine_status_list")]
        public List<NodeInfo> machine_status_list { get; set; }

        [DataMember(Name = "check_list")]
        public List<NodeInfo> check_list { get; set; }
    }
}
