using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace LogWriter
{
    public class LogDemo
    {
        public static void Main()
        {
            //TestDemo();
        }

        /// <summary>
        /// 测试机制
        /// </summary>
        private static void TestDemo() {
            for (int i = 0; i < 1000; i++)
            {
                MyLog.Loger.Debug("测试数据测试数据测试数据测试数据测试数据测试数据测试数据测试数据");
                if (MyLog.Loger.IsInfoEnabled)
                {
                    MyLog.Loger.Info("测试数据测试数据测试数据测试数据测试数据测试数据测试数据测试数据");

                }
                try
                {
                    throw new Exception("This is an Exception");
                }
                catch (Exception ex)
                {
                    MyLog.Loger.Error("测试数据测试数据测试数据测试数据测试数据测试数据测试数据测试数据" + ex.Message);

                }
                MyLog.Loger.Warn("测试数据测试数据测试数据测试数据测试数据测试数据测试数据测试数据");

            }
            Console.Read();
        }

    }
}
