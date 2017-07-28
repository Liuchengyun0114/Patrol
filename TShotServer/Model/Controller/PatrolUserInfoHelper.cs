using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Transactions;

namespace Model.Controller
{
    /// <summary>
    /// 用户信息控制类
    /// </summary>
    public class PatrolUserInfoHelper
    {
        /// <summary>
        /// 主键前缀代码
        /// </summary>
        public static string PrefixCode { get { return "PUI"; } }

        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true=成功;false=失败</returns>
        public bool Insert(PatrolUserInfo entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    context.PatrolUserInfo.AddObject(entity);
                    trans.Complete();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
                if (success)
                {
                    //提交保存
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            context.Dispose();

            return success;
        }

        /// <summary>
        /// 根据主键删除实例
        /// </summary>
        /// <param name="oldEntity">设置了主键Id的对象</param>
        /// <returns></returns>
        public bool Delete(PatrolUserInfo entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolUserInfo instance = context.PatrolUserInfo.Where("it.UserCD=@UserCD", new ObjectParameter("UserCD", entity.UserCD)).First();
                    //标记删除
                    context.PatrolUserInfo.DeleteObject(instance);
                    trans.Complete();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
                if (success)
                {
                    //提交保存
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            context.Dispose();

            return success;
        }

        /// <summary>
        /// 根据更新列表更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="updateKeys">更新字段列表 键值对</param>
        /// <returns>true=成功,false=失败</returns>
        public bool Update(PatrolUserInfo entity, Hashtable updateKeys)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolUserInfo instance = context.PatrolUserInfo.Where("it.UserCD=@UserCD", new ObjectParameter("UserCD", entity.UserCD)).First();
                    //更新数据操作
                    SetUpdateValue(instance, updateKeys);
                    trans.Complete();
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
                if (success)
                {
                    //提交保存
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            context.Dispose();

            return success;
        }

        /// <summary>
        /// 返回所有整个表数据
        /// </summary>
        /// <returns></returns>
        public List<PatrolUserInfo> SelectAll()
        {
            List<PatrolUserInfo> list = new List<PatrolUserInfo>();
            try
            {
                SQLEntities context = new SQLEntities();
                //添加所有记录
                list.AddRange(context.PatrolUserInfo.ToList());
                context.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        /// <summary>
        /// 根据查询条件查询数据集合
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        public List<PatrolUserInfo> SearchByCondition(SearchInfo searchInfo)
        {
            List<PatrolUserInfo> list = new List<PatrolUserInfo>();
            try
            {
                SQLEntities context = new SQLEntities();

                list.AddRange(context.PatrolUserInfo.Where(searchInfo.WhereExpress, searchInfo.Parameters.ToArray()).ToList());
                context.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }

        /// <summary>
        /// 根据查询条件查询单条记录
        /// </summary>
        /// <param name="searchInfo">查询条件,主键查询</param>
        /// <returns></returns>
        public PatrolUserInfo Select(PatrolUserInfo searchInfo)
        {
            PatrolUserInfo instance = null;
            try
            {
                SQLEntities context = new SQLEntities();

                instance = context.PatrolUserInfo.Where("it.UserCD=@UserCD", new ObjectParameter("UserCD", searchInfo.UserCD)).First();

                context.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return instance;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="source">源对象</param>
        /// <returns></returns>
        public PatrolUserInfo Copy(PatrolUserInfo source)
        {
            PatrolUserInfo target = new PatrolUserInfo();

            target.UserCD = source.UserCD;
            target.UserPassword = source.UserPassword;
            target.Token = source.Token;
            target.TokenInvalid = source.TokenInvalid;
            target.IsAdmin = source.IsAdmin;
            target.SearchRange = source.SearchRange;
            target.IsAvailable = source.IsAvailable;
            target.Creator = source.Creator;
            target.CreatedAt = source.CreatedAt;
            target.Updator = source.Updator;
            target.UpdatedAt = source.UpdatedAt;


            return target;
        }

        /// <summary>
        /// 记录需要更新的字段
        /// </summary>
        /// <param name="update"></param>
        private static void SetUpdateValue(PatrolUserInfo current, Hashtable updateKeys)
        {
            foreach (DictionaryEntry item in updateKeys)
            {
                switch (item.Key.ToString().ToLower())
                {

                    case "usercd":
                        current.UserCD = item.Value.ToString();
                        break;
                    case "userpassword":
                        current.UserPassword = item.Value.ToString();
                        break;
                    case "token":
                        current.Token = item.Value.ToString();
                        break;
                    case "tokeninvalid":
                        current.TokenInvalid = Convert.ToDateTime(item.Value);
                        break;
                    case "isadmin":
                        current.IsAdmin = item.Value.ToString();
                        break;
                    case "searchrange":
                        current.SearchRange = item.Value.ToString();
                        break;
                    case "isavailable":
                        current.IsAvailable = item.Value.ToString();
                        break;
                    case "creator":
                        current.Creator = item.Value.ToString();
                        break;
                    case "createdat":
                        current.CreatedAt = Convert.ToDateTime(item.Value);
                        break;
                    case "updator":
                        current.Updator = item.Value.ToString();
                        break;
                    case "updatedat":
                        current.UpdatedAt = Convert.ToDateTime(item.Value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
