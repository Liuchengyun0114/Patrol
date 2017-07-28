using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web;
using System.IO;
using System.Xml;
using PatrolServer.Services.Base;
using PatrolServer.Services.Report;
using PatrolServer.Services.User;
using PatrolServer.Services.Patrol;

namespace PatrolServer.Services
{
    /// <summary>
    /// 服务控制中心 包含启动服务
    /// </summary>
    public class ServicesController
    {
        /// <summary>
        /// 所有服务控制中心
        /// </summary>
        public static void Init()
        {
            //基础服务
            BaseHost.Init(false);
            //特巡报告服务
            ReportHost.Init(false);
            //用户信息服务
            UserHost.Init(false);

            //Web管理后台特巡报告服务
            PatrolHost.Init(false);  
        }
    }
}
