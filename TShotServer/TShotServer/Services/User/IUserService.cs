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
using PatrolServer.Services.User.Request;

namespace PatrolServer.Services.User
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserService”。
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updatePassword", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream UpdatePassword(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "updatePassword", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream UpdatePasswordOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "loginCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream LoginCheck(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "loginCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream LoginCheckOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "add", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream AddUser(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "add", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream AddUserOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "update", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream UpdateUser(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "update", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream UpdateUserOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream DeleteUser(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream DeleteUserOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getUserList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetUserList(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getUserList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetUserListOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetUser(Stream data);


        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Stream GetUserOptions(Stream data);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getUserBase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetUserBase(Stream data);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "getUserBase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetUserBaseOptions(Stream data);

        [OperationContract]
        [WebGet(UriTemplate = "show", ResponseFormat = WebMessageFormat.Json)]
        Stream ShowInfo();
    }
}
