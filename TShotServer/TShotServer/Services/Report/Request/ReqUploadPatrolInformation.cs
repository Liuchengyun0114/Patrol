using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;
using PatrolServer.Services.Report.Request.Entity;

namespace PatrolServer.Services.Report.Request
{
    /// <summary>
    /// 上传特巡报告 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqUploadPatrolInformation
    {
        [DataMember(Name = "model_code")]
        public string model_code { get; set; }

        [DataMember(Name = "model_id")]
        public string model_id { get; set; }

        [DataMember(Name = "worked_times")]
        public string worked_times { get; set; }

        [DataMember(Name = "model_status_code")]
        public string model_status_code { get; set; }

        [DataMember(Name = "is_urgent")]
        public string is_urgent { get; set; }

        [DataMember(Name = "remarks")]
        public string remarks { get; set; }

        //联系人1
        [DataMember(Name = "contact_type_code1")]
        public string contact_type_code1 { get; set; }

        [DataMember(Name = "phone_type_code1")]
        public string phone_type_code1 { get; set; }

        [DataMember(Name = "contactor_name1")]
        public string contactor_name1 { get; set; }

        [DataMember(Name = "phone_number1")]
        public string phone_number1 { get; set; }

        //联系人2
        [DataMember(Name = "contact_type_code2")]
        public string contact_type_code2 { get; set; }

        [DataMember(Name = "phone_type_code2")]
        public string phone_type_code2 { get; set; }

        [DataMember(Name = "contactor_name2")]
        public string contactor_name2 { get; set; }
        
        [DataMember(Name = "phone_number2")]
        public string phone_number2 { get; set; }

        [DataMember(Name = "province")]
        public string province { get; set; }

        [DataMember(Name = "city")]
        public string city { get; set; }

        [DataMember(Name = "address")]
        public string address { get; set; }   

        [DataMember(Name = "check_list")]
        public List<PicInfo> check_list { get; set; }

        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "token")]
        public string token { get; set; }

        [DataMember(Name = "patrol_id")]
        public string patrol_id { get; set; }

        [DataMember(Name = "dir_name")]
        public string dir_name { get; set; }
    }
}
