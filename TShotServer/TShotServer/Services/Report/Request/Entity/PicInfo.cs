using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;


namespace PatrolServer.Services.Report.Request.Entity
{
    /// <summary>
    /// 上传图片信息对象,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class PicInfo
    {
        [DataMember(Name = "pic_url")]
        public string pic_url { get; set; }

        [DataMember(Name = "location_code")]
        public string location_code { get; set; }

        [DataMember(Name = "spot_code")]
        public string spot_code { get; set; }
        
        [DataMember(Name = "spot_status_code")]
        public string spot_status_code { get; set; }
        
        [DataMember(Name = "level_code")]
        public string level_code { get; set; }

        [DataMember(Name = "part_remarks")]
        public string part_remarks { get; set; }

        [DataMember(Name = "is_important")]
        public string is_important { get; set; }
    }
}
