using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web;
using System.IO;
using System.Xml;
using PatrolServer.Services;
using LogWriter;

namespace PatrolServer
{
    public class Start
    {
        static void Main(string[] args)
        {

            //初始化公共环境
            Console.WriteLine("初始化全局环境...");
            MyLog.Loger.Info("初始化环境...");
            CommonInfo.Init();
            //启动服务
            Console.WriteLine("服务正在启动...");
            MyLog.Loger.Info("服务启动中...");
            ServicesController.Init();

            Console.Read();
        }
    }
}
