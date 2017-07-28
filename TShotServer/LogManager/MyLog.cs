using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using log4net;


//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace LogWriter
{
    public class MyLog
    {
        //日志对象
        private static log4net.ILog _Loger = null;

        public static log4net.ILog Loger
        {
            get
            {
                if (_Loger == null)
                {
                    //_Loger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    _Loger = log4net.LogManager.GetLogger(System.Environment.MachineName + "@" + System.Environment.UserDomainName);
                }
                return MyLog._Loger;
            }
            set { MyLog._Loger = value; }
        }
        private static string ConfigFile = "log4net.config";
        static MyLog()
        {
            if (System.IO.File.Exists(ConfigFile))
            {
                if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(ConfigFile));
                }
                else {
                    log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(ConfigFile));
                }
            }
            Console.WriteLine("日志启动完成");
        }
    }
}
