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
    /// 特巡报告映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class PaginatorInfo
    {
        [DataMember(Name = "PageIndex")]
        public int PageIndex { get; set; }

        [DataMember(Name = "PageSize")]
        public int PageSize { get; set; }
    }
}
