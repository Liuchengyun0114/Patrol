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
    /// 点检部位控制类
    /// </summary>
    public class PatrolSpotPartsHelper
    {
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true=成功;false=失败</returns>
        public bool Insert(PatrolSpotParts entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    context.PatrolSpotParts.AddObject(entity);
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
        public bool Delete(PatrolSpotParts entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolSpotParts instance = context.PatrolSpotParts.Where("it.ID=@ID", new ObjectParameter("ID", entity.ID)).First();
                    //标记删除
                    context.PatrolSpotParts.DeleteObject(instance);
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
        public bool Update(PatrolSpotParts entity, Hashtable updateKeys)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolSpotParts instance = context.PatrolSpotParts.Where("it.ID=@ID", new ObjectParameter("ID", entity.ID)).First();
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
        public List<PatrolSpotParts> SelectAll()
        {
            List<PatrolSpotParts> list = new List<PatrolSpotParts>();
            try
            {
                SQLEntities context = new SQLEntities();
                //添加所有记录
                list.AddRange(context.PatrolSpotParts.ToList());
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
        public List<PatrolSpotParts> SearchByCondition(SearchInfo searchInfo)
        {
            List<PatrolSpotParts> list = new List<PatrolSpotParts>();
            try
            {
                SQLEntities context = new SQLEntities();

                list.AddRange(context.PatrolSpotParts.Where(searchInfo.WhereExpress, searchInfo.Parameters.ToArray()).ToList());
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
        public PatrolSpotParts Select(PatrolSpotParts searchInfo)
        {
            PatrolSpotParts instance = null;
            try
            {
                SQLEntities context = new SQLEntities();

                instance = context.PatrolSpotParts.Where("it.ID=@ID", new ObjectParameter("ID", searchInfo.ID)).First();

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
        public PatrolSpotParts Copy(PatrolSpotParts source)
        {
            PatrolSpotParts target = new PatrolSpotParts();

            target.ID = source.ID;
            target.Name = source.Name;
            target.ParentID = source.ParentID;
            target.SortCD = source.SortCD;

            return target;
        }

        /// <summary>
        /// 记录需要更新的字段
        /// </summary>
        /// <param name="update"></param>
        private static void SetUpdateValue(PatrolSpotParts current, Hashtable updateKeys)
        {
            foreach (DictionaryEntry item in updateKeys)
            {
                switch (item.Key.ToString().ToLower())
                {
                    case "id":
                        current.ID = item.Value.ToString();
                        break;
                    case "name":
                        current.Name = item.Value.ToString();
                        break;
                    case "parentid":
                        current.ParentID = item.Value.ToString();
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
