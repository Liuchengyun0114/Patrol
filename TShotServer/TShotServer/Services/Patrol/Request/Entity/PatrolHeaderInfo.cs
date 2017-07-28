using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.Patrol.Request.Entity
{
    /// <summary>
    /// 特巡报告头部信息映射类,用于序列化Json或者反序列化对象
    /// 暂时只能修改备注信息(后续加入所有字段)
    /// </summary>
    [DataContract]
    public class PatrolHeaderInfo
    {
        //此处属性用于显示列表时候

        [DataMember(Name = "patrol_no")]
        public string patrol_no { get; set; }

        [DataMember(Name = "report_date")]
        public string report_date { get; set; }

        [DataMember(Name = "reporter")]
        public string reporter { get; set; }

        [DataMember(Name = "reporter_name")]
        public string reporter_name { get; set; }

        [DataMember(Name = "report_status")]
        public string report_status { get; set; }

        [DataMember(Name = "machine_type")]
        public string machine_type { get; set; }

        [DataMember(Name = "machine_no")]
        public string machine_no { get; set; }

        [DataMember(Name = "machine_name")]
        public string machine_name { get; set; }

        [DataMember(Name = "customer")]
        public string customer { get; set; }

        //下面用于查看详情时需要属性

        [DataMember(Name = "maker")]
        public string maker { get; set; }

        [DataMember(Name = "worked_times")]
        public string worked_times { get; set; }

        [DataMember(Name = "machine_status")]
        public string machine_status { get; set; }

        [DataMember(Name = "machine_status_name")]
        public string machine_status_name { get; set; }

        [DataMember(Name = "is_emergency")]
        public string is_emergency { get; set; }

        [DataMember(Name = "remarks")]
        public string remarks { get; set; }

        [DataMember(Name = "contactor_name")]
        public string contactor_name { get; set; }

        [DataMember(Name = "contactor_phone")]
        public string contactor_phone { get; set; }

        [DataMember(Name = "operator_name")]
        public string operator_name { get; set; }

        [DataMember(Name = "operator_phone")]
        public string operator_phone { get; set; }

        [DataMember(Name = "work_no")]
        public string work_no { get; set; }

        [DataMember(Name = "report_uri")]
        public string report_uri { get; set; } 
    }
}
