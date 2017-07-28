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
    /// 建友员工信息控制类
    /// </summary>
    public class STAFFMSTHelper
    {
        /// <summary>
        /// 主键前缀代码
        /// </summary>
        public static string PrefixCode { get { return "STA"; } }
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true=成功;false=失败</returns>
        public bool Insert(STAFFMST entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    context.STAFFMST.AddObject(entity);
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
        public bool Delete(STAFFMST entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    //删除操作

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
        public bool Update(STAFFMST entity, Hashtable updateKeys)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    //更新操作

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
        public List<STAFFMST> SelectAll()
        {
            List<STAFFMST> list = new List<STAFFMST>();
            try
            {
                SQLEntities context = new SQLEntities();
                //添加所有记录
                list.AddRange(context.STAFFMST.ToList());
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
        public List<STAFFMST> SearchByCondition(SearchInfo searchInfo)
        {
            List<STAFFMST> list = new List<STAFFMST>();
            try
            {
                SQLEntities context = new SQLEntities();

                list.AddRange(context.STAFFMST.Where(searchInfo.WhereExpress, searchInfo.Parameters.ToArray()).ToList());
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
        public STAFFMST Select(STAFFMST searchInfo)
        {
            STAFFMST instance = null;
            try
            {
                SQLEntities context = new SQLEntities();

                //查询单个用户

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
        public STAFFMST Copy(STAFFMST source)
        {
            STAFFMST target = new STAFFMST();

            //字段复制操作

            return target;
        }

        /// <summary>
        /// 记录需要更新的字段
        /// </summary>
        /// <param name="update"></param>
        private static void SetUpdateValue(STAFFMST current, Hashtable updateKeys)
        {
            foreach (DictionaryEntry item in updateKeys)
            {
                switch (item.Key.ToString().ToLower())
                {
                        //字段更新对应
                    default:
                        break;
                }
            }
        }
    }
}
