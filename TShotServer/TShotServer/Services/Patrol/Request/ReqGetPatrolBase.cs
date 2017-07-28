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
    /// 获取特巡报告查询条件基础信息 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqGetPatrolBase
    {
    }
}
