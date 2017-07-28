using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web;
using System.IO;
using System.Xml;

namespace PatrolServer.Services.User
{
    /// <summary>
    /// 基础服务控制器
    /// </summary>
    public class UserHost
    {
        /// <summary>
        /// 编程或者配置文件启动服务
        /// </summary>
        /// <param name="startWithConfig">是否通过编程启动服务,默认配置文件启动</param>
        /// <returns></returns>
        public static bool Init(bool startByUser) {
            if (startByUser)
            {
                return InitByUser();
            }
            return InitByConfig();        
        }

        /// <summary>
        /// 通过配置文件启动服务
        /// </summary>
        /// <returns></returns>
        private static bool InitByConfig()
        {
            bool success = false;
            try
            {
                ServiceHost serviceHost = new ServiceHost(typeof(UserService));
                serviceHost.Opened += new EventHandler(serviceHost_Opened);
                serviceHost.Open();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        /// <summary>
        /// 编程设置启动服务
        /// </summary>
        /// <returns></returns>
        private static bool InitByUser()
        {
            bool success = false;
            try
            {
                Uri baseAddress = new Uri("http://localhost:8732/PatrolService/User/");
                ServiceHost serviceHost = new ServiceHost(typeof(UserService));
                WebHttpBinding binding = new WebHttpBinding();
                binding.MaxBufferSize = int.MaxValue;
                binding.MaxReceivedMessageSize = int.MaxValue;
                binding.TransferMode = TransferMode.Buffered;
                ServiceEndpoint endpoint = serviceHost.AddServiceEndpoint(typeof(IUserService), binding, baseAddress);
                WebHttpBehavior httpBehavior = new WebHttpBehavior();
                endpoint.Behaviors.Add(httpBehavior);
                serviceHost.Opened += new EventHandler(serviceHost_Opened);
                serviceHost.Open();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }
        
        /// <summary>
        /// 服务启动事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void serviceHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("UserService Start Success!");
        }
    }
}
