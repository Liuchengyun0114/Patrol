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
    /// 特巡报告详情信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class PatrolDetailInfo
    {
        [DataMember(Name = "patrol_no")]
        public string patrol_no { get; set; }

        [DataMember(Name = "sub_no")]
        public string sub_no { get; set; }

        [DataMember(Name = "is_selected")]
        public bool is_selected { get; set; }

        [DataMember(Name = "is_important")]
        public string is_important { get; set; }

        [DataMember(Name = "pic_url")]
        public string pic_url { get; set; }
        
        [DataMember(Name = "location_code")]
        public string location_code { get; set;}

        [DataMember(Name = "location_code_name")]
        public string location_code_name { get; set; }
            
        [DataMember(Name = "spot_code")]
        public string spot_code { get; set; }
        
        [DataMember(Name = "spot_code_name")]
        public string spot_code_name { get; set; }

        [DataMember(Name = "remarks")]
        public string remarks { get; set; }
    }
}
