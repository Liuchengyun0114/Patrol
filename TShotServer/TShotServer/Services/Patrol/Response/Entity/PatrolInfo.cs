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
    /// 特巡报告列表信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class PatrolInfo
    {
        //此处属性用于显示列表时候

        [DataMember(Name = "order_no")]
        public string order_no { get; set; }

        [DataMember(Name = "errimage_count")]
        public string errimage_count { get; set; }

        [DataMember(Name = "patrol_no")]
        public string patrol_no { get; set; }

        [DataMember(Name = "report_date")]
        public string report_date { get; set; }

        [DataMember(Name = "reporter")]
        public string reporter { get; set; }

        [DataMember(Name = "report_status")]
        public string report_status { get; set; }   

        [DataMember(Name = "machine_type")]
        public string machine_type { get; set; }

        [DataMember(Name = "machine_no")]
        public string machine_no { get; set; }

        [DataMember(Name = "customer")]
        public string customer { get; set; }
    }
}
