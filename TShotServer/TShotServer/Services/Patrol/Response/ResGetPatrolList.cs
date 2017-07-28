using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using Model;
using Model.EntityManager;
using PatrolServer.Services.Patrol.Response.Entity;

namespace PatrolServer.Services.Patrol.Response
{
    /// <summary>
    /// 获取所有的特巡报告列表 接口编号=1001
    /// </summary>
    [DataContract]
    public class ResGetPatrolList
    {
        [DataMember(Name = "function_id")]
        public string function_id { get; set; }

        [DataMember(Name = "return_flag")]
        public string return_flag { get; set; }

        [DataMember(Name = "return_msg")]
        public string return_msg { get; set; }

        [DataMember(Name = "count")]
        public int count { get; set; }

        [DataMember(Name = "patrol_list")]
        public List<PatrolInfo> patrol_list { get; set; }

        public ResGetPatrolList()
        {
            this.function_id = ((int)MessageHelper.WebFunctionIDs.GetPatrolList).ToString();//注意枚举ToString直接得到字符串
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

        //将datatable数据转换为Json
        public static List<PatrolInfo> Transfer(DataTable source)
        {
            List<PatrolInfo> ret = new List<PatrolInfo>();
            foreach (DataRow item in source.Rows)
            {
                PatrolInfo obj = new PatrolInfo();
                obj.order_no = item[PatrolEntity.HeaderPropertyFlag.OrderNO.ToString()].ToString();
                obj.customer = item[PatrolEntity.HeaderPropertyFlag.Customer.ToString()].ToString();
                obj.machine_no = item[PatrolEntity.HeaderPropertyFlag.MachineNO.ToString()].ToString();
                obj.machine_type = item[PatrolEntity.HeaderPropertyFlag.MachineType.ToString()].ToString();
                obj.patrol_no = item[PatrolEntity.HeaderPropertyFlag.PatrolNO.ToString()].ToString();
                obj.report_date = item[PatrolEntity.HeaderPropertyFlag.ReportDate.ToString()].ToString();
                obj.report_status = item[PatrolEntity.HeaderPropertyFlag.ReportStatus.ToString()].ToString();
                obj.reporter = item[PatrolEntity.HeaderPropertyFlag.Reporter.ToString()].ToString();
                obj.errimage_count = item[PatrolEntity.HeaderPropertyFlag.ErrImageCount.ToString()].ToString();
                ret.Add(obj);
            }
            return ret;
        }
    }
}
