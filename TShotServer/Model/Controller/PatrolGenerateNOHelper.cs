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
    /// 主键生成器控制类
    /// </summary>
    public class PatrolGenerateNOHelper
    {
        public enum DateType { 
            Default = 1,
            Never = 0,
            Day = 1,
            Month = 2,
            Year = 3        
        }

        /// <summary>
        /// 新增一个主键类型
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true=成功;false=失败</returns>
        public bool Insert(PatrolGenerateNO entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    context.PatrolGenerateNO.AddObject(entity);
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
        public bool Delete(PatrolGenerateNO entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolGenerateNO instance = context.PatrolGenerateNO.Where("it.PrefixCode=@PrefixCode", new ObjectParameter("PrefixCode", entity.PrefixCode)).First();
                    //标记删除
                    context.PatrolGenerateNO.DeleteObject(instance);
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
        public bool Update(PatrolGenerateNO entity, Hashtable updateKeys)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolGenerateNO instance = context.PatrolGenerateNO.Where("it.PrefixCode=@PrefixCode", new ObjectParameter("PrefixCode", entity.PrefixCode)).First();
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
        public List<PatrolGenerateNO> SelectAll()
        {
            List<PatrolGenerateNO> list = new List<PatrolGenerateNO>();
            try
            {
                SQLEntities context = new SQLEntities();
                //添加所有记录
                list.AddRange(context.PatrolGenerateNO.ToList());
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
        public List<PatrolGenerateNO> SearchByCondition(SearchInfo searchInfo)
        {
            List<PatrolGenerateNO> list = new List<PatrolGenerateNO>();
            try
            {
                SQLEntities context = new SQLEntities();
                
                list.AddRange(context.PatrolGenerateNO.Where(searchInfo.WhereExpress, searchInfo.Parameters.ToArray()).ToList());
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
        public PatrolGenerateNO Select(PatrolGenerateNO searchInfo)
        {
            PatrolGenerateNO instance = null;
            try
            {
                SQLEntities context = new SQLEntities();

                instance = context.PatrolGenerateNO.Where("it.PrefixCode=@PrefixCode", new ObjectParameter("PrefixCode", searchInfo.PrefixCode)).First();

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
        /// <param name="source"></param>
        /// <returns></returns>
        public PatrolGenerateNO Copy(PatrolGenerateNO source) {
            PatrolGenerateNO target = new PatrolGenerateNO();

            target.PrefixCode = source.PrefixCode;
            target.CurrentID = source.CurrentID;
            target.MaxID = source.MaxID;
            target.DateType = source.DateType;
            target.Increment = source.Increment;
            target.CreatedAt = source.CreatedAt;
            target.UpdatedAt = source.UpdatedAt;
            return target;
        }

        /// <summary>
        /// 记录需要更新的字段
        /// </summary>
        /// <param name="update"></param>
        private static void SetUpdateValue(PatrolGenerateNO current,Hashtable updateKeys)
        {
            foreach (DictionaryEntry item in updateKeys)
            {
                
                switch (item.Key.ToString().ToLower())
                {
                    case "prefixcode":
                        current.PrefixCode = item.Value.ToString();
                        break;
                    case "currentid":
                        current.CurrentID = Convert.ToInt64(item.Value);
                        break;
                    case "increment":
                        current.Increment = Convert.ToInt32(item.Value);
                        break;
                    case "maxid":
                        current.MaxID = Convert.ToInt64(item.Value);
                        break;
                    case "datetype":
                        current.DateType = Convert.ToInt32(item.Value);
                        break;
                    case "createdat":
                        current.CreatedAt = Convert.ToDateTime(item.Value);
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
