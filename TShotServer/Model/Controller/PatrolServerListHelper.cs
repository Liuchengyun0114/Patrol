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
    /// 服务器列表控制类
    /// </summary>
    public class PatrolServerListHelper
    {
        /// <summary>
        /// 主键前缀代码
        /// </summary>
        public static string PrefixCode { get { return "PSL"; } }

        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true=成功;false=失败</returns>
        public bool Insert(PatrolServerList entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    context.PatrolServerList.AddObject(entity);
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
        public bool Delete(PatrolServerList entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolServerList instance = context.PatrolServerList.Where("it.Address=@Address", new ObjectParameter("Address", entity.Address)).First();
                    //标记删除
                    context.PatrolServerList.DeleteObject(instance);
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
        public bool Update(PatrolServerList entity, Hashtable updateKeys)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolServerList instance = context.PatrolServerList.Where("it.Address=@Address", new ObjectParameter("Address", entity.Address)).First();
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
        public List<PatrolServerList> SelectAll()
        {
            List<PatrolServerList> list = new List<PatrolServerList>();
            try
            {
                SQLEntities context = new SQLEntities();
                //添加所有记录
                list.AddRange(context.PatrolServerList.ToList());
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
        public List<PatrolServerList> SearchByCondition(SearchInfo searchInfo)
        {
            List<PatrolServerList> list = new List<PatrolServerList>();
            try
            {
                SQLEntities context = new SQLEntities();

                list.AddRange(context.PatrolServerList.Where(searchInfo.WhereExpress, searchInfo.Parameters.ToArray()).ToList());
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
        public PatrolServerList Select(PatrolServerList searchInfo)
        {
            PatrolServerList instance = null;
            try
            {
                SQLEntities context = new SQLEntities();

                instance = context.PatrolServerList.Where("it.Address=@Address", new ObjectParameter("Address", searchInfo.Address)).First();

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
        public PatrolServerList Copy(PatrolServerList source)
        {
            PatrolServerList target = new PatrolServerList();

            target.Address = source.Address;
            target.Name = source.Name;
            target.IsMainServer = source.IsMainServer;
            target.SortCD = source.SortCD;

            return target;
        }

        /// <summary>
        /// 记录需要更新的字段
        /// </summary>
        /// <param name="update"></param>
        private static void SetUpdateValue(PatrolServerList current, Hashtable updateKeys)
        {
            foreach (DictionaryEntry item in updateKeys)
            {
                switch (item.Key.ToString().ToLower())
                {

                    case "address":
                        current.Address = item.Value.ToString();
                        break;
                    case "name":
                        current.Name = item.Value.ToString();
                        break;
                    case "ismainserver":
                        current.IsMainServer = item.Value.ToString();
                        break;
                    case "sortcd":
                        current.SortCD = Convert.ToInt32(item.Value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
