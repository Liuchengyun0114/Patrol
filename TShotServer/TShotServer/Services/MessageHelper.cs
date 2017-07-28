using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatrolServer.Services
{
    public class MessageHelper
    {
        public class ReturnMsg
        {
            public static string Success { get { return "操作成功"; } }
            public static string Failed { get { return "操作失败"; } }   
        }
        /// <summary>
        /// 返回状态
        /// </summary>
        public enum ReturnFlag {  
            Success = 0,           
            Failed = 1
        }

        /// <summary>
        /// 接口值
        /// </summary>
        public enum AppFunctionIDs
        {
            UpdatePassword = 1001,
            GetUserList = 1002,
            GetBaseInfo = 1003,
            UploadImage = 1004,
            UploadPatrolInformation = 1005,
            LoginCheck = 1006,
            GetServerList = 1007
        }

        /// <summary>
        /// 接口值
        /// </summary>
        public enum WebFunctionIDs
        {
            GetPatrolBase = 1000,
            GetPatrolList = 1001,
            GetPatrol = 1002,
            UpdatePatrol = 1003,
            DeletePatrol = 1004,
            ShowReport = 1005,
            GetUserBase = 2000,
            GetUserList = 2001,
            GetUser = 2002,
            AddUser = 2003,
            UpdateUser = 2004,
            DeleteUser = 2005
        }
    }
}
