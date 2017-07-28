using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;
using PatrolServer.Services.Patrol.Request.Entity;

namespace PatrolServer.Services.Patrol.Request
{
    /// <summary>
    /// 获取所有的特巡报告列表 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqGetPatrolList
    {
        [DataMember(Name = "account")]
        public string account { get; set; }

        [DataMember(Name = "token")]
        public string token { get; set; }

        [DataMember(Name = "search_info")]
        public PatrolSearchInfo search_info { get; set; }
    }
}
