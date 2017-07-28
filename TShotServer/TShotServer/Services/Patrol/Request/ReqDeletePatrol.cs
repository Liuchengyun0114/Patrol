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
    /// 删除单个特巡报告头部和所有详情 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqDeletePatrol
    {
        [DataMember(Name = "patrol_no")]
        public string patrol_no { get; set; }
    }
}
