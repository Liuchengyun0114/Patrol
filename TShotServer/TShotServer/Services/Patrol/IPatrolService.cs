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
using PatrolServer.Services.Patrol.Request;

namespace PatrolServer.Services.Patrol
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IPatrolService”。
    [ServiceContract]
    public interface IPatrolService
    {

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getPatrolBase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetPatrolBase(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getPatrolBase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetPatrolBaseOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getPatrolList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetPatrolList(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getPatrolList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetPatrolListOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getPatrol", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetPatrol(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getPatrol", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetPatrolOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "update", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream UpdatePatrol(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "update", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream UpdatePatrolOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream DeletePatrol(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream DeletePatrolOptions(Stream data);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "show/{reportid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream ShowReport(string reportid);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "show/{reportid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream ShowReportOptions(string reportid);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "createReport/{patrolno}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateReport(string patrolno);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "createReport/{patrolno}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateReportOptions(string patrolno);

        [OperationContract]
        [WebGet(UriTemplate = "show", ResponseFormat = WebMessageFormat.Json)]
        Stream ShowInfo();
    }
}
