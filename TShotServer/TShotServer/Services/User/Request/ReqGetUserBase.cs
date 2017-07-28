using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;

namespace PatrolServer.Services.User.Request
{
    /// <summary>
    /// 获取用户查询条件基础数据 客户端请求数据包装对象
    /// </summary>
    [DataContract]
    public class ReqGetUserBase
    {
    }
}
