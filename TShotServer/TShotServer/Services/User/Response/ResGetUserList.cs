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
using Model.EntityManager;
using PatrolServer.Services.User.Response.Entity;

namespace PatrolServer.Services.User.Response
{
    /// <summary>
    /// 获取用户列表 接口编号=2001
    /// </summary>
    [DataContract]
    public class ResGetUserList
    {
        [DataMember(Name = "function_id")]
        public string function_id { get; set; }

        [DataMember(Name = "return_flag")]
        public string return_flag { get; set; }

        [DataMember(Name = "return_msg")]
        public string return_msg { get; set; }

        [DataMember(Name = "count")]
        public int count { get; set; }

        [DataMember(Name = "user_list")]
        public List<UserInfo> user_list { get; set; }

        public ResGetUserList()
        {
            this.function_id = ((int)MessageHelper.WebFunctionIDs.GetUserList).ToString();//注意枚举ToString直接得到字符串
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
        public static List<UserInfo> Transfer(DataTable source)
        {
            List<UserInfo> ret = new List<UserInfo>();
            foreach (DataRow item in source.Rows)
            {
                UserInfo obj = new UserInfo();
                obj.order_no = item[UserEntity.PropertyFlag.OrderNO.ToString()].ToString();
                obj.agency_shop = item[UserEntity.PropertyFlag.AgencyShop.ToString()].ToString();
                obj.filiale = item[UserEntity.PropertyFlag.Filiale.ToString()].ToString();
                obj.department = item[UserEntity.PropertyFlag.AgencyShop.ToString()].ToString();
                bool ispatroluser = (item[UserEntity.PropertyFlag.IsPatrolUser.ToString()].ToString() == "1") ? true : false;
                obj.is_patrol_user = ispatroluser;
                obj.isadmin = item[UserEntity.PropertyFlag.IsAdmin.ToString()].ToString();
                obj.name = item[UserEntity.PropertyFlag.UserName.ToString()].ToString();
                obj.search_range = item[UserEntity.PropertyFlag.SearchRange.ToString()].ToString();
                obj.user_id = item[UserEntity.PropertyFlag.UserCD.ToString()].ToString();
                ret.Add(obj);
            }
            return ret;
        }
    }
}
