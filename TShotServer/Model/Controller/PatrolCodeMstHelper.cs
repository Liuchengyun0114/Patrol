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
    /// 数据字典控制类
    /// </summary>
    public class PatrolCodeMstHelper
    {
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true=成功;false=失败</returns>
        public bool Insert(PatrolCodeMst entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    context.PatrolCodeMst.AddObject(entity);
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
        public bool Delete(PatrolCodeMst entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolCodeMst instance = context.PatrolCodeMst.Where("it.CodeTypeCD=@CodeTypeCD and it.CodeCD =@CodeCD", new ObjectParameter("CodeTypeCD", entity.CodeTypeCD), new ObjectParameter("CodeCD", entity.CodeCD)).First();
                    //标记删除
                    context.PatrolCodeMst.DeleteObject(instance);
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
        public bool Update(PatrolCodeMst entity, Hashtable updateKeys)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolCodeMst instance = context.PatrolCodeMst.Where("it.CodeTypeCD=@PatrolNO and it.CodeCD =@CodeCD", new ObjectParameter("CodeTypeCD", entity.CodeTypeCD), new ObjectParameter("CodeCD", entity.CodeCD)).First();
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
        public List<PatrolCodeMst> SelectAll()
        {
            List<PatrolCodeMst> list = new List<PatrolCodeMst>();
            try
            {
                SQLEntities context = new SQLEntities();
                //添加所有记录
                list.AddRange(context.PatrolCodeMst.ToList());
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
        public List<PatrolCodeMst> SearchByCondition(SearchInfo searchInfo)
        {
            List<PatrolCodeMst> list = new List<PatrolCodeMst>();
            try
            {
                SQLEntities context = new SQLEntities();

                list.AddRange(context.PatrolCodeMst.Where(searchInfo.WhereExpress, searchInfo.Parameters.ToArray()).ToList());
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
        public PatrolCodeMst Select(PatrolCodeMst searchInfo)
        {
            PatrolCodeMst instance = null;
            try
            {
                SQLEntities context = new SQLEntities();

                instance = context.PatrolCodeMst.Where("it.CodeTypeCD=@CodeTypeCD and it.CodeCD =@CodeCD", new ObjectParameter("CodeTypeCD", searchInfo.CodeTypeCD), new ObjectParameter("CodeCD", searchInfo.CodeCD)).First();

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
        public PatrolCodeMst Copy(PatrolCodeMst source)
        {
            PatrolCodeMst target = new PatrolCodeMst();

            target.CodeCD = source.CodeCD;
            target.CodeTypeCD = source.CodeTypeCD;
            target.CodeTypeName = source.CodeTypeName;
            target.CodeName = source.CodeName;
            target.CodeValue = source.CodeValue;
            target.SortCD = source.SortCD;

            return target;
        }

        /// <summary>
        /// 记录需要更新的字段
        /// </summary>
        /// <param name="update"></param>
        private static void SetUpdateValue(PatrolCodeMst current, Hashtable updateKeys)
        {
            foreach (DictionaryEntry item in updateKeys)
            {
                switch (item.Key.ToString().ToLower())
                {
                    case "codecd":
                        current.CodeCD = item.Value.ToString();
                        break;
                    case "codetypecd":
                        current.CodeTypeCD = item.Value.ToString();
                        break;
                    case "codetypename":
                        current.CodeTypeName = item.Value.ToString();
                        break;
                    case "codename":
                        current.CodeName = item.Value.ToString();
                        break;
                    case "codevalue":
                        current.CodeValue = item.Value.ToString();
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
