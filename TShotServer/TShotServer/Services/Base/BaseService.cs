using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.Collections;
using PatrolServer.Common;
using Model;
using Model.Controller;
using Model.BusinessRule;
using PatrolServer.Services.Base;
using PatrolServer.Services.Base.Response;
using PatrolServer.Services.Base.Response.Entity;

namespace PatrolServer.Services.Base
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“BaseService”。
    public class BaseService : IBaseService
    {
        public Stream GetUserList()
        {
            //#region 测试数据
            
            //StreamReader srt = new StreamReader("userlist.json",Encoding.UTF8);
            
            //string sdata = srt.ReadToEnd();

            //srt.Close();
            //return new MemoryStream(Encoding.UTF8.GetBytes(sdata));

            //#endregion


            //返回流
            MemoryStream response = new MemoryStream();
            StringBuilder sb = new StringBuilder();
            StringBuilder userlist = new StringBuilder();
            StringBuilder status = new StringBuilder();
            List<PatrolUserInfo> list = PatrolUserInfoRule.SelectAll();
            //头部信息
            if (list == null)
            {
                status.AppendFormat("\"function_id\":\"{0}\",", "1002");
                status.AppendFormat("\"return_flag\":\"{0}\",", "1");
                status.AppendFormat("\"return_msg\":\"{0}\"", "操作失败");
            }
            else
            {
                status.AppendFormat("\"function_id\":\"{0}\",", "1002");
                status.AppendFormat("\"return_flag\":\"{0}\",", "0");
                status.AppendFormat("\"return_msg\":\"{0}\"", "操作成功");
            }
            //具体数据
            foreach (PatrolUserInfo item in list)
            {
                userlist.AppendFormat("{{");
                userlist.AppendFormat("\"user_id\":\"{0}\",", item.UserCD);
                userlist.AppendFormat("\"password\":\"{0}\",", item.UserPassword);
                userlist.AppendFormat("\"user_name\":\"{0}\"", item.UserCD);
                userlist.AppendFormat("}},");
            }
            if (userlist.Length > 0)
            {
                userlist.Remove(userlist.Length - 1, 1);
            }

            //组织最终数据
            sb.AppendFormat("{{");
            sb.AppendFormat("{0},", status);
            sb.AppendFormat("\"userlist\":{{ \"count\":{0},\"list\":[{1}]}}", list.Count, userlist);
            sb.AppendFormat("}}");

            return new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString()));
        }

        public Stream GetBaseInfo()
        {
            //#region 测试数据
            
            //StreamReader srt = new StreamReader("baseinfo.json", Encoding.UTF8);

            //string sdata = srt.ReadToEnd();

            //srt.Close();
            //return new MemoryStream(Encoding.UTF8.GetBytes(sdata));
            //#endregion

            ResGetBaseInfo response = new ResGetBaseInfo();
            try
            {
                //*.取得机型列表
                List<NodeInfo> modelList = List2Tree(MACHINETYPEMSTRule.GetList());

                //*.取得点检部位树形菜单
                List<PatrolSpotParts> spotPartsListBase = PatrolSpotPartsRule.GetList();
                List<NodeInfo> checkList = SpotPartsList2Tree(spotPartsListBase);

                //*.取得机器状态列表
                List<NodeInfo> machineStatusList =  List2Tree(PatrolCodeMstRule.GetListOfMachineStatus());

                //*.取得问题程度列表
                List<NodeInfo> questionLevelList = List2Tree(PatrolCodeMstRule.GetListOfQuestionLevel());

                //*.取得点检状态列表
                List<NodeInfo> spotStatusList = List2Tree(PatrolCodeMstRule.GetListOfSpotStatus());

                //*.取得联系方式列表
                List<NodeInfo> contactTypeList = List2Tree(PatrolCodeMstRule.GetListOfContactType());

                //*.取得联系人类型列表
                List<NodeInfo> contactorTypeList = List2Tree(PatrolCodeMstRule.GetListOfContactorType());

                //组织数据
                response.model_list = modelList;
                response.check_list = checkList;
                response.machine_status_list = machineStatusList;
                response.level_list = questionLevelList;
                response.spot_status_list = spotStatusList;
                response.phone_type_list = contactTypeList;
                response.contact_type_list = contactorTypeList;
                //输出成功状态
                response.SetSuccess();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResGetBaseInfo));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //FileStream fs = new FileStream("getbaseinfo.json", FileMode.Create, FileAccess.ReadWrite);
            //ms.Position = 0;
            //ms.CopyTo(fs);
            ////fs.Write(ms.ToArray(), 0, ms.Length);
            //fs.Close();

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;

        }

        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <returns></returns>
        public Stream GetServerList()
        {
            ResGetServerList response = new ResGetServerList();
            try
            {
                //*.取得机型列表
                List<ServerInfo> server_list = List2Tree(PatrolServerListRule.GetList());

                //组织数据
                response.server_list = server_list;

                //输出成功状态
                response.SetSuccess();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //将消息序列化为Json格式数据
            DataContractJsonSerializer obj2Json = new DataContractJsonSerializer(typeof(ResGetServerList));
            MemoryStream ms = new MemoryStream();
            obj2Json.WriteObject(ms, response);

            //FileStream fs = new FileStream("getserverlist.json", FileMode.Create, FileAccess.ReadWrite);
            //ms.Position = 0;
            //ms.CopyTo(fs);
            ////fs.Write(ms.ToArray(), 0, ms.Length);
            //fs.Close();

            //注意一定要设置流的位置到开始位置,否则没有消息输出
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 测试服务是否成功开启
        /// </summary>
        /// <returns></returns>
        public Stream ShowInfo()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes("Base Test"));
        }

        #region 辅助方法

        /// <summary>
        /// 将点检部位列表转换为树形列表
        /// </summary>
        /// <param name="source">点检部位列表</param>
        /// <returns></returns>
        public static List<NodeInfo> SpotPartsList2Tree(List<PatrolSpotParts> source){
            List<NodeInfo> tree = new List<NodeInfo>();
            try
            {
                if (source != null && source.Count > 0)
                {
                    List<PatrolSpotParts> partsList = source.Where(p => p.ParentID == "root").ToList<PatrolSpotParts>();
                    foreach (PatrolSpotParts item in partsList)
                    {
                        NodeInfo subTree = new NodeInfo();
                        subTree.code = item.ID;
                        subTree.name = item.Name;

                        List<PatrolSpotParts> subList = source.Where(p => p.ParentID == item.ID).ToList<PatrolSpotParts>();
                        List<NodeInfo> subNode = new List<NodeInfo>();
                        foreach (PatrolSpotParts instance in subList)
                        {
                            NodeInfo temp = new NodeInfo();
                            temp.code = instance.ID;
                            temp.name = instance.Name;
                            subNode.Add(temp);
                        }
                        subTree.sub_list = subNode;
                        tree.Add(subTree);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return tree;
        
        }

        /// <summary>
        /// 列表类型转换
        /// </summary>
        /// <param name="source">数据字典列表</param>
        /// <returns></returns>
        public static List<NodeInfo> List2Tree(List<PatrolCodeMst> source)
        {
            List<NodeInfo> list = new List<NodeInfo>();
            try
            {
                if (source != null && source.Count > 0)
                {
                    foreach (PatrolCodeMst item in source)
                    {
                        NodeInfo temp = new NodeInfo();
                        temp.code = item.CodeCD;
                        temp.name = item.CodeName;
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
        /// 机型列表类型转换
        /// </summary>
        /// <param name="source">数据字典列表</param>
        /// <returns></returns>
        public static List<NodeInfo> List2Tree(List<MACHINETYPEMST> source)
        {
            List<NodeInfo> list = new List<NodeInfo>();
            try
            {
                if (source != null && source.Count > 0)
                {
                    foreach (MACHINETYPEMST item in source)
                    {
                        NodeInfo temp = new NodeInfo();
                        temp.code = item.MACHINECD;
                        temp.name = item.MACHINENM;
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
        /// 列表类型转换
        /// </summary>
        /// <param name="source">数据字典列表</param>
        /// <returns></returns>
        public static List<ServerInfo> List2Tree(List<PatrolServerList> source)
        {
            List<ServerInfo> list = new List<ServerInfo>();
            try
            {
                if (source != null && source.Count > 0)
                {
                    foreach (PatrolServerList item in source)
                    {
                        ServerInfo temp = new ServerInfo();
                        temp.address = item.Address;
                        temp.name = item.Name;
                        temp.isMainServer = item.IsMainServer;
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

        #endregion


        public Stream GetUserListOptions()
        {
            return null;
        }

        public Stream GetBaseInfoOptions()
        {
            return null;
        }

        public Stream GetServerListOptions()
        {
            return null;
        }
    }
}
