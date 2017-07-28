using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;
using PatrolServer.Services.Base.Response.Entity;

namespace PatrolServer.Services.Base.Response
{

    /// <summary>
    /// 获取基础数据返回数据对象 接口编号=1002
    /// </summary>
    [DataContract]
    public class ResGetBaseInfo
    {

        [DataMember(Name = "function_id")]
        public string function_id { get; set; }

        [DataMember(Name = "return_flag")]
        public string return_flag { get; set; }

        [DataMember(Name = "return_msg")]
        public string return_msg { get; set; }
        
        [DataMember(Name = "model_list")]
        public List<NodeInfo> model_list { get; set; }

        [DataMember(Name = "check_list")]
        public List<NodeInfo> check_list { get; set; }

        [DataMember(Name = "machine_status_list")]
        public List<NodeInfo> machine_status_list { get; set; }

        [DataMember(Name = "level_list")]
        public List<NodeInfo> level_list { get; set; }

        [DataMember(Name = "spot_status_list")]
        public List<NodeInfo> spot_status_list { get; set; }

        [DataMember(Name = "phone_type_list")]
        public List<NodeInfo> phone_type_list { get; set; }

        [DataMember(Name = "contact_type_list")]
        public List<NodeInfo> contact_type_list { get; set; }


        #region 方法区域
        public ResGetBaseInfo()
        {
            this.function_id = ((int)MessageHelper.AppFunctionIDs.GetBaseInfo).ToString();//注意枚举ToString直接得到字符串
            this.SetFailed();
        }
        public void SetSuccess()
        {
            this.return_flag = ((int)MessageHelper.ReturnFlag.Success).ToString();
            this.return_msg = MessageHelper.ReturnMsg.Success;
        }
        public void SetFailed()
        {
            this.return_flag = ((int)MessageHelper.ReturnFlag.Failed).ToString();
            this.return_msg = MessageHelper.ReturnMsg.Failed;
        }
        #endregion
    }
}
