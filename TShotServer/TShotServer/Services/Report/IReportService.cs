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

namespace PatrolServer.Services.Report
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IReportService”。
    [ServiceContract]
    public interface IReportService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "uploadImage",ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream UploadImage(Stream data);
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "uploadImage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream UploadImageOptions(Stream data);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "uploadPatrolInformation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream UploadPatrolInformation(Stream data);
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "uploadPatrolInformation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream UploadPatrolInformationOptions(Stream data);
        [OperationContract]
        [WebGet(UriTemplate = "show", ResponseFormat = WebMessageFormat.Json)]
        Stream ShowInfo();
    }
}
