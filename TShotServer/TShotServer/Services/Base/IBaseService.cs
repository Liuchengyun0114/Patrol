using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace PatrolServer.Services.Base
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IBaseService”。
    [ServiceContract]
    public interface IBaseService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getUserList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetUserList();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getUserList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetUserListOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getBaseInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetBaseInfo();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getBaseInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetBaseInfoOptions();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getServerList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetServerList();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getServerList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetServerListOptions();

        [OperationContract]
        [WebGet(UriTemplate = "show", ResponseFormat = WebMessageFormat.Json)]
        Stream ShowInfo();
    }
}
