using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PatrolServer.Message
{
    public abstract class MSBase
    {
        public string FunctionID = "1000";
        public string ReturnFlg = "0";
        public string ReturnMsg = "操作成功Success";
        protected readonly string JSONStart = "{";
        protected readonly string JSONClose = "}";
        public string JSONBody { get; set; }
        public string Data { get; set; }
        /// <summary>
        /// 返回最终JSON对象
        /// </summary>
        /// <returns></returns>
        public string Response2JSON() {
            return JSONStart + Data2JSON() + JSONClose;
        }

        protected string Header2JSON() {
            return "'function_id':'" + this.FunctionID + "','return_flag':'" + this.ReturnFlg + "','return_msg':'" + this.ReturnMsg + "'";           
        }

        /// <summary>
        /// 将数据转换成JSON格式体 子类实现
        /// </summary>
        /// <returns></returns>
        protected abstract string Data2JSON();
        /// <summary>
        /// 将流数据转换成字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected abstract string Stream2String(Stream data);
    }
}
