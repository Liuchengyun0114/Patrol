using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.Base.Response.Entity
{
    /// <summary>
    /// 服务器信息映射类,用于序列化Json或者反序列化对象
    /// </summary>
    [DataContract]
    public class ServerInfo
    {
        [DataMember(Name = "address")]
        public string address { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "isMainServer")]
        public string isMainServer { get; set; }
    }
}
