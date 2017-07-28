using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.Patrol.Request
{
    /// <summary>
    /// 获取单个特巡报告信息 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqGetPatrol
    {
        [DataMember(Name = "account")]
        public string account { get; set; }

        [DataMember(Name = "token")]
        public string token { get; set; }

        [DataMember(Name = "patrol_no")]
        public string patrol_no { get; set; }
    }
}
