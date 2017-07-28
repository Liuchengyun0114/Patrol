using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.Report.Response.Entity
{
    /// <summary>
    /// 返回图片信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class PicInfo
    {
        [DataMember(Name = "image_name")]
        public string image_name { get; set; }

        [DataMember(Name = "pic_url")]
        public string pic_url { get; set; }
    }
}
