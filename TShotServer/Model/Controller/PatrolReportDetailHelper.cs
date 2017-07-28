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
    /// 特巡报告详情控制类
    /// </summary>
    public class PatrolReportDetailHelper
    {
        /// <summary>
        /// 主键前缀代码
        /// </summary>
        public static string PrefixCode { get { return "PRN"; } }
        /// <summary>
        /// 新增对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>true=成功;false=失败</returns>
        public bool Insert(PatrolReportDetail entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    context.PatrolReportDetail.AddObject(entity);
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
        public bool Delete(PatrolReportDetail entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolReportDetail instance = context.PatrolReportDetail.Where("it.PatrolNO=@PatrolNO and it.SubNO =@SubNO", new ObjectParameter("PatrolNO", entity.PatrolNO), new ObjectParameter("SubNO", entity.SubNO)).First();
                    //标记删除
                    context.PatrolReportDetail.DeleteObject(instance);
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
        public bool Update(PatrolReportDetail entity, Hashtable updateKeys)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolReportDetail instance = context.PatrolReportDetail.Where("it.PatrolNO=@PatrolNO and it.SubNO =@SubNO", new ObjectParameter("PatrolNO", entity.PatrolNO), new ObjectParameter("SubNO", entity.SubNO)).First();
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
        public List<PatrolReportDetail> SelectAll()
        {
            List<PatrolReportDetail> list = new List<PatrolReportDetail>();
            try
            {
                SQLEntities context = new SQLEntities();
                //添加所有记录
                list.AddRange(context.PatrolReportDetail.ToList());
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
        public List<PatrolReportDetail> SearchByCondition(SearchInfo searchInfo)
        {
            List<PatrolReportDetail> list = new List<PatrolReportDetail>();
            try
            {
                SQLEntities context = new SQLEntities();

                list.AddRange(context.PatrolReportDetail.Where(searchInfo.WhereExpress, searchInfo.Parameters.ToArray()).ToList());
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
        public PatrolReportDetail Select(PatrolReportDetail searchInfo)
        {
            PatrolReportDetail instance = null;
            try
            {
                SQLEntities context = new SQLEntities();

                instance = context.PatrolReportDetail.Where("it.PatrolNO=@PatrolNO and it.SubNO =@SubNO", new ObjectParameter("PatrolNO", searchInfo.PatrolNO), new ObjectParameter("SubNO", searchInfo.SubNO)).First();

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
        public PatrolReportDetail Copy(PatrolReportDetail source)
        {
            PatrolReportDetail target = new PatrolReportDetail();

            target.PatrolNO = source.PatrolNO;
            target.SubNO = source.SubNO;
            target.LocationCode = source.LocationCode;
            target.SpotCode = source.SpotCode;
            target.Status = source.Status;
            target.QuestionLevel = source.QuestionLevel;
            target.Remarks = source.Remarks;
            target.PicUrl = source.PicUrl;
            target.IsSelected = source.IsSelected;
            target.IsImportant = source.IsImportant;

            return target;
        }

        /// <summary>
        /// 记录需要更新的字段
        /// </summary>
        /// <param name="update"></param>
        private static void SetUpdateValue(PatrolReportDetail current, Hashtable updateKeys)
        {
            foreach (DictionaryEntry item in updateKeys)
            {
                switch (item.Key.ToString().ToLower())
                {
                    case "patrolno":
                        current.PatrolNO = item.Value.ToString();
                        break;
                    case "subno":
                        current.SubNO = Convert.ToInt32(item.Value);
                        break;
                    case "locationcode":
                        current.LocationCode = item.Value.ToString();
                        break;
                    case "spotcode":
                        current.SpotCode = item.Value.ToString();
                        break;
                    case "status":
                        current.Status = item.Value.ToString();
                        break;
                    case "questionlevel":
                        current.QuestionLevel = item.Value.ToString();
                        break;
                    case "remarks":
                        current.Remarks = item.Value.ToString();
                        break;
                    case "picurl":
                        current.PicUrl = item.Value.ToString();
                        break;
                    case "isselected":
                        current.IsSelected = item.Value.ToString();
                        break;
                    case "isimportant":
                        current.IsImportant = item.Value.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
