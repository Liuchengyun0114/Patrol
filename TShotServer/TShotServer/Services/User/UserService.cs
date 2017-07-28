using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using PatrolServer.Common;
using Model;
using Model.BusinessRule;
using Model.EntityManager;
using PatrolServer.Services.User.Request;
using PatrolServer.Services.User.Response;
using PatrolServer.Services.User.Response.Entity;

namespace PatrolServer.Services.User
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“UserService”。
    public class UserService : IUserService
    {
        /// <summary>
        /// 密码更新接口 接口编号=1001
        /// </summary>
        /// <param name="data">Post数据</param>
        /// <returns></returns>
        public Stream UpdatePassword(Stream data)
        {
            System.ServiceModel.Channels.Message o = OperationContext.Current.RequestContext.RequestMessage;
            bool success = false;
            ResUpdatePassword response = new ResUpdatePassword();
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ReqUpdatePassword));           
                //读取器
                StreamReader sr = new StreamReader(data);                
                string dataString = String.Empty;
                while (!sr.EndOfStream)
                {
                    dataString = sr.ReadToEnd();
                    Console.WriteLine(dataString);

                    //反序列化json为对象注意顺序很重要
                    ReqUpdatePassword request = new ReqUpdatePassword();
                    MemoryStream temp = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
                    request = json.ReadObject(temp) as ReqUpdatePassword;

                    //关闭临时流
                    temp.Close();

                    //调用用户更新密码接口
                    if (request != null)
                    {
                        //*.数据合法性检查

                        //*.更新
                        success = PatrolUserInfoRule.UpdatePassword(request.user_id, request.old_pwd, request.new_pwd); 
                      
                    }

                    ////解析成对象
                    //Hashtable datalist = JSONFormater.JsonString2Hash(dataString);
                    //foreach (DictionaryEntry item in datalist)
                    //{
                    //    Console.WriteLine("{0}={1}", item.Key, item.Value);
                    //}

                }
                sr.Close();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResUpdatePassword));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);
            
            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 登录验证接口 接口编号 = 1006
        /// </summary>
        /// <param name="data">Post数据</param>
        /// <returns></returns>
        public Stream LoginCheck(Stream data)
        {
            System.ServiceModel.Channels.Message o = OperationContext.Current.RequestContext.RequestMessage;
            bool success = false;
            ResLoginCheck response = new ResLoginCheck();
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ReqLoginCheck));
                //读取器
                StreamReader sr = new StreamReader(data);
                string dataString = String.Empty;
                while (!sr.EndOfStream)
                {
                    dataString = sr.ReadToEnd();
                    Console.WriteLine(dataString);

                    //反序列化json为对象注意顺序很重要
                    ReqLoginCheck request = new ReqLoginCheck();
                    MemoryStream temp = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
                    request = json.ReadObject(temp) as ReqLoginCheck;

                    //关闭临时流
                    temp.Close();

                    //调用用户更新密码接口
                    if (request != null)
                    {
                        //*.数据合法性检查

                        //*.更新
                        response.token = PatrolUserInfoRule.LoginCheck(request.user_id, request.password);

                        if (response.token != String.Empty)
                        {
                            success = true;
                        }
                    }
                }
                sr.Close();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResLoginCheck));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;
        }

        public Stream ShowInfo()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes("User Test"));
        }


        public Stream AddUser(Stream data)
        {
            //#region 测试数据

            //StreamReader srt = new StreamReader("patrollist.json", Encoding.UTF8);

            //string sdata = srt.ReadToEnd();

            //srt.Close();
            //return new MemoryStream(Encoding.UTF8.GetBytes(sdata));

            //#endregion

            #region 查找数据
            ResAddUser response = new ResAddUser();
            bool success = true;
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ReqAddUser));
                //读取器
                StreamReader sr = new StreamReader(data);
                string dataString = String.Empty;
                while (!sr.EndOfStream)
                {
                    dataString = sr.ReadToEnd();
                    Console.WriteLine(dataString);

                    //反序列化json为对象注意顺序很重要
                    ReqAddUser request = new ReqAddUser();
                    MemoryStream temp = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
                    request = json.ReadObject(temp) as ReqAddUser;

                    //关闭临时流
                    temp.Close();

                    //调用用户更新密码接口
                    if (request != null)
                    {
                        //**********（此处加入代码） 根据查询条件 加入业务逻辑代码*************
                        if (request.account != null && request.account != String.Empty && request.token != null && request.token != String.Empty)
                        {
                            if (request.user != null)
                            {
                                Console.WriteLine("开始新增用户");
                                PatrolUserInfo userinfo = new PatrolUserInfo();
                                userinfo.UserCD = request.user.user_id;
                                userinfo.UserPassword = CommonInfo.DefaultPassword;
                                userinfo.SearchRange = SetSearchRange(request.user.search_range);
                                userinfo.IsAdmin = SetAdmin(request.user.isadmin);
                                userinfo.IsAvailable = "1";
                                userinfo.CreatedAt = DateTime.Now;
                                userinfo.Creator = request.account;
                                Console.WriteLine("我的测试数据" + userinfo.IsAdmin);
                                success = UserEntity.InsertUser(userinfo);
                            }
                        }
                        else {
                            Console.WriteLine("用户验证失败");
                        }
                    }
                    else {
                        Console.WriteLine("请求对象为空");
                    }
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResAddUser));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;

            #endregion
        }

        public Stream UpdateUser(Stream data)
        {
            //#region 测试数据

            //StreamReader srt = new StreamReader("patrollist.json", Encoding.UTF8);

            //string sdata = srt.ReadToEnd();

            //srt.Close();
            //return new MemoryStream(Encoding.UTF8.GetBytes(sdata));

            //#endregion

            #region 查找数据
            ResUpdateUser response = new ResUpdateUser();
            bool success = true;
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ReqUpdateUser));
                //读取器
                StreamReader sr = new StreamReader(data);
                string dataString = String.Empty;
                while (!sr.EndOfStream)
                {
                    dataString = sr.ReadToEnd();
                    Console.WriteLine(dataString);

                    //反序列化json为对象注意顺序很重要
                    ReqUpdateUser request = new ReqUpdateUser();
                    MemoryStream temp = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
                    request = json.ReadObject(temp) as ReqUpdateUser;

                    //关闭临时流
                    temp.Close();

                    //调用用户更新密码接口
                    if (request != null)
                    {
                        //**********（此处加入代码） 根据查询条件 加入业务逻辑代码*************
                        if (request.account != null && request.account != String.Empty && request.token != null && request.token != String.Empty)
                        {
                            string user = getUpdateUserSqlString(request);
                            success = UserEntity.updateUser(user);
                        }
                    }
                }
                sr.Close();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResUpdateUser));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;

            #endregion
        }

        public Stream DeleteUser(Stream data)
        {
            //#region 测试数据

            //StreamReader srt = new StreamReader("patrollist.json", Encoding.UTF8);

            //string sdata = srt.ReadToEnd();

            //srt.Close();
            //return new MemoryStream(Encoding.UTF8.GetBytes(sdata));

            //#endregion

            #region 查找数据
            ResDeleteUser response = new ResDeleteUser();
            bool success = true;
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ReqDeleteUser));
                //读取器
                StreamReader sr = new StreamReader(data);
                string dataString = String.Empty;
                while (!sr.EndOfStream)
                {
                    dataString = sr.ReadToEnd();
                    Console.WriteLine(dataString);

                    //反序列化json为对象注意顺序很重要
                    ReqDeleteUser request = new ReqDeleteUser();
                    MemoryStream temp = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
                    request = json.ReadObject(temp) as ReqDeleteUser;

                    //关闭临时流
                    temp.Close();

                    //调用用户更新密码接口
                    if (request != null)
                    {
                        //**********（此处加入代码） 根据查询条件 加入业务逻辑代码*************
                        if (request.account != null && request.account != String.Empty && request.token != null && request.token != String.Empty)
                        {
                            string user = getDeleteUserSqlString(request);
                            success = UserEntity.deleteUser(user);
                        }
                    }
                }
                sr.Close();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResDeleteUser));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;

            #endregion
        }


        public Stream GetUserList(Stream data)
        {
            #region 查找数据
            ResGetUserList response = new ResGetUserList();
            bool success = true;
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ReqGetUserList));
                //读取器
                StreamReader sr = new StreamReader(data);
                string dataString = String.Empty;
                while (!sr.EndOfStream)
                {
                    dataString = sr.ReadToEnd();
                    Console.WriteLine(dataString);

                    //反序列化json为对象注意顺序很重要
                    ReqGetUserList request = new ReqGetUserList();
                    MemoryStream temp = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
                    request = json.ReadObject(temp) as ReqGetUserList;

                    //关闭临时流
                    temp.Close();

                    //调用用户更新密码接口
                    if (request != null)
                    {
                        //**********（此处加入代码） 根据查询条件 加入业务逻辑代码*************
                        // 身份验证
                        if (request.account != null && request.account != String.Empty && request.token != null && request.token != String.Empty)
                        {
                            // sql查询字符串组织
                            string queryString = String.Empty;
                            string rangeString = String.Empty;
                            // 按条件查询
                            if (request.search_info != null)
                            {
                                #region 属性查询对应
                                if (request.search_info.agency_shop != null && request.search_info.agency_shop != String.Empty)
                                {
                                    queryString += " and t.CompanyCD = '" + request.search_info.agency_shop + "' ";
                                }
                                if (request.search_info.filiale != null && request.search_info.filiale != String.Empty)
                                {
                                    queryString += " and t.SubCompanyCD = '" + request.search_info.filiale + "' ";
                                }
                                if (request.search_info.user_id != null && request.search_info.user_id != String.Empty)
                                {
                                    //用户编号或者名称
                                    queryString += " and (t.StaffCD = '" + request.search_info.user_id + "' or  t.StaffNM = '" + request.search_info.user_id + "' ) ";
                                }

                                //分页控制对象范围
                                int pageIndex = 1;
                                int pageSize = 10;
                                if (request.search_info.Paginator != null)
                                {
                                    pageIndex = request.search_info.Paginator.PageIndex;
                                    pageIndex = pageIndex > 0 ? pageIndex : 1;
                                    pageSize = request.search_info.Paginator.PageSize;
                                    pageSize = pageSize > 0 ? pageSize : 10;

                                    int rangeStart = (pageIndex - 1) * pageSize + 1;
                                    int rangeEnd = pageIndex * pageSize;

                                    rangeString += " and orderno between " + rangeStart + " and " + rangeEnd + " ";
                                }
                                else
                                {
                                    rangeString += " and orderno between " + pageIndex + " and " + pageSize + " ";
                                }

                                //获取数据调用数据库接口
                                DataTable dt = UserEntity.getUserList(queryString, rangeString);
                                //转换数据
                                response.user_list = ResGetUserList.Transfer(dt);
                                response.count = UserEntity.getUserCount(queryString);

                                success = true;
                                #endregion
                            }
                        }
                    }
                }
                sr.Close();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResGetUserList));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;

            #endregion
        }

        public Stream GetUser(Stream data)
        {
            #region 查找数据
            ResGetUser response = new ResGetUser();
            bool success = true;
            try
            {
                DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(ReqGetUser));
                //读取器
                StreamReader sr = new StreamReader(data);
                string dataString = String.Empty;
                while (!sr.EndOfStream)
                {
                    dataString = sr.ReadToEnd();
                    Console.WriteLine(dataString);

                    //反序列化json为对象注意顺序很重要
                    ReqGetUser request = new ReqGetUser();
                    MemoryStream temp = new MemoryStream(Encoding.UTF8.GetBytes(dataString));
                    request = json.ReadObject(temp) as ReqGetUser;

                    //关闭临时流
                    temp.Close();

                    //调用用户更新密码接口
                    if (request != null)
                    {
                        //**********（此处加入代码） 根据查询条件 加入业务逻辑代码*************

                        if (request.account != null && request.account != String.Empty && request.token != null && request.token != String.Empty)
                        {
                            if (request.user_id != null && request.user_id != String.Empty)
                            {
                                DataTable user = UserEntity.getUserInfo(request.user_id);
                                response.user = ResGetUser.getUserInfo(user);

                                //设置成功状态
                                success = true;
                            }
                        }
                    }
                }
                sr.Close();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResGetUser));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;

            #endregion
        }

        /// <summary>
        /// 取得基础数据 用于查询用户条件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Stream GetUserBase(Stream data)
        {
            #region 查找数据
            ResGetUserBase response = new ResGetUserBase();
            bool success = true;
            try
            {
                UserBaseInfo userbase = new UserBaseInfo();

                //代理店信息列表
                List<NodeInfo> agency_shop_list = List2Tree(COMPANYMSTRule.GetList());

                //分公司信息列表
                List<NodeInfo> filiale_list = List2Tree(SUBCOMPANYMSTRule.GetList());


                userbase.agency_shop_list = agency_shop_list;
                userbase.filiale_list = filiale_list;
                //组织数据
                response.userbase = userbase;

                //输出成功状态
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //返回消息体
            if (success)
            {
                response.SetSuccess();
            }
            else
            {
                ////默认是失败
                //response.SetFailed();
            }
            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResGetUserBase));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;

            #endregion
        }


        #region 辅助方法


        /// <summary>
        /// 设置用户管理员权限
        /// </summary>
        /// <param name="isadmin"></param>
        /// <returns></returns>
        public static string SetAdmin(string isadmin)
        {
            string result = ((int)UserEntity.AdminFlag.User).ToString();
            if (isadmin != null && isadmin != String.Empty)
            {
                result = isadmin;
            }
            return result;
        }

        /// <summary>
        /// 设置用户查询特巡权限
        /// </summary>
        /// <param name="searchRange"></param>
        /// <returns></returns>
        public static string SetSearchRange(string searchRange)
        {
            string result = ((int)UserEntity.SearchRangeFlag.Personal).ToString();
            if (searchRange != null && searchRange != String.Empty)
            {
                result = searchRange;
            }
            return result;
        }

        /// <summary>
        /// 代理店列表类型转换
        /// </summary>
        /// <param name="source">数据字典列表</param>
        /// <returns></returns>
        public static List<NodeInfo> List2Tree(List<COMPANYMST> source)
        {
            List<NodeInfo> list = new List<NodeInfo>();
            try
            {
                if (source != null && source.Count > 0)
                {
                    foreach (COMPANYMST item in source)
                    {
                        NodeInfo temp = new NodeInfo();
                        temp.code = item.COMPANYCD;
                        temp.name = item.COMPANYNM;
                        list.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;

        }

        /// <summary>
        /// 分公司列表类型转换
        /// </summary>
        /// <param name="source">数据字典列表</param>
        /// <returns></returns>
        public static List<NodeInfo> List2Tree(List<SUBCOMPANYMST> source)
        {
            List<NodeInfo> list = new List<NodeInfo>();
            try
            {
                if (source != null && source.Count > 0)
                {
                    foreach (SUBCOMPANYMST item in source)
                    {
                        NodeInfo temp = new NodeInfo();
                        temp.code = item.SUBCOMPANYCD;
                        temp.name = item.SUBCOMPANYNM;
                        list.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;

        }

        /// <summary>
        /// 更新用户sql
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string getUpdateUserSqlString(ReqUpdateUser request)
        {
            Request.Entity.UserInfo item = request.user;
            string temp = String.Empty;
            if (item != null)
            {
                temp = "  update PatrolUserInfo set ";
                temp += " IsAdmin = '" + item.isadmin + "',";
                temp += " SearchRange = '" + item.search_range + "',";
                temp += " UpdatedAt = '" + DateTime.Now.ToString() + "',";
                temp += " Updator = '" + request.account + "' ";
                temp += " where UserCD = '" + item.user_id + "'";
                temp += " ; ";
            }

            return temp;
        }

        /// <summary>
        /// 删除用户sql
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string getDeleteUserSqlString(ReqDeleteUser request)
        {
            string temp = String.Empty;
            if (request.user_id != null && request.user_id != String.Empty)
            {
                temp = "  delete from PatrolUserInfo ";
                temp += " where UserCD = '" + request.user_id + "'";
                temp += " ; ";
            }

            return temp;
        }
        #endregion

        /// <summary>
        /// 解决跨域问题
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Stream UpdatePasswordOptions(Stream data)
        {
            return null;
        }

        public Stream LoginCheckOptions(Stream data)
        {
            return null;
        }

        public Stream AddUserOptions(Stream data)
        {
            return null;
        }

        public Stream UpdateUserOptions(Stream data)
        {
            return null;
        }

        public Stream DeleteUserOptions(Stream data)
        {
            return null;
        }

        public Stream GetUserListOptions(Stream data)
        {
            return null;
        }

        public Stream GetUserOptions(Stream data)
        {
            return null;
        }

        public Stream GetUserBaseOptions(Stream data)
        {
            return null;
        }
    }
}
