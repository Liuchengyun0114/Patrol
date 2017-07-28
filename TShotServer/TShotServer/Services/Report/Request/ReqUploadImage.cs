using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.Report.Request
{
    /// <summary>
    /// 上传图片 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqUploadImage
    {
        [DataMember(Name = "user_id")]
        public string user_id { get; set; }

        [DataMember(Name = "image_name")]
        public string image_name { get; set; }

        [DataMember(Name = "image_file")]
        public Stream image_file { get; set; }

        [DataMember(Name = "token")]
        public string token { get; set; }
    }
}
