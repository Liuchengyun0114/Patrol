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
    /// 获取用户 接口编号=2002
    /// </summary>
    [DataContract]
    public class ResGetUser
    {
        [DataMember(Name = "function_id")]
        public string function_id { get; set; }

        [DataMember(Name = "return_flag")]
        public string return_flag { get; set; }

        [DataMember(Name = "return_msg")]
        public string return_msg { get; set; }

        [DataMember(Name = "user")]
        public UserInfo user { get; set; }

        public ResGetUser()
        {
            this.function_id = ((int)MessageHelper.WebFunctionIDs.GetUser).ToString();//注意枚举ToString直接得到字符串
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

        /// <summary>
        /// 取得指定用户数据
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static UserInfo getUserInfo(DataTable source)
        {
            UserInfo obj = new UserInfo();
            if (source != null && source.Rows.Count > 0)
            {
                DataRow item = source.Rows[0];
                obj.agency_shop = item[UserEntity.PropertyFlag.AgencyShop.ToString()].ToString();
                obj.department = obj.agency_shop + "/" + item[UserEntity.PropertyFlag.Filiale.ToString()].ToString();
                bool ispatroluser = (item[UserEntity.PropertyFlag.IsPatrolUser.ToString()].ToString() == "1") ? true : false;
                obj.is_patrol_user = ispatroluser;
                obj.isadmin = item[UserEntity.PropertyFlag.IsAdmin.ToString()].ToString();
                obj.name = item[UserEntity.PropertyFlag.UserName.ToString()].ToString();
                obj.search_range = item[UserEntity.PropertyFlag.SearchRange.ToString()].ToString();
                obj.user_id = item[UserEntity.PropertyFlag.UserCD.ToString()].ToString();
            }

            return obj;
        }

    }
}
