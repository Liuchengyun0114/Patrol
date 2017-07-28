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
    /// 特巡报告头部控制类
    /// </summary>
    public class PatrolReportHeaderHelper
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
        public bool Insert(PatrolReportHeader entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    context.PatrolReportHeader.AddObject(entity);
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
        public bool Delete(PatrolReportHeader entity)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    PatrolReportHeader instance = context.PatrolReportHeader.Where("it.PatrolNO=@PatrolNO", new ObjectParameter("PatrolNO", entity.PatrolNO)).First();
                    //标记删除
                    context.PatrolReportHeader.DeleteObject(instance);
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
        public bool Update(PatrolReportHeader entity, Hashtable updateKeys)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    Console.WriteLine(entity.PatrolNO);
                    PatrolReportHeader instance = context.PatrolReportHeader.Where("it.PatrolNO=@PatrolNO", new ObjectParameter("PatrolNO", entity.PatrolNO)).First();
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
        public List<PatrolReportHeader> SelectAll()
        {
            List<PatrolReportHeader> list = new List<PatrolReportHeader>();
            try
            {
                SQLEntities context = new SQLEntities();
                //添加所有记录
                list.AddRange(context.PatrolReportHeader.ToList());
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
        public List<PatrolReportHeader> SearchByCondition(SearchInfo searchInfo)
        {
            List<PatrolReportHeader> list = new List<PatrolReportHeader>();
            try
            {
                SQLEntities context = new SQLEntities();

                list.AddRange(context.PatrolReportHeader.Where(searchInfo.WhereExpress, searchInfo.Parameters.ToArray()).ToList());
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
        public PatrolReportHeader Select(PatrolReportHeader searchInfo)
        {
            PatrolReportHeader instance = null;
            try
            {
                SQLEntities context = new SQLEntities();

                instance = context.PatrolReportHeader.Where("it.PatrolNO=@PatrolNO", new ObjectParameter("PatrolNO", searchInfo.PatrolNO)).First();

                context.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" 查询特巡异常："+ ex.Message);
            }
            return instance;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public PatrolReportHeader Copy(PatrolReportHeader source)
        {
            PatrolReportHeader target = new PatrolReportHeader();

            target.Contaction1 = source.Contaction1;
            target.Contaction2 = source.Contaction2;
            target.ContactorName1 = source.ContactorName1;
            target.ContactorName2 = source.ContactorName2;
            target.ContactorType1 = source.ContactorType1;
            target.ContactorType2 = source.ContactorType2;
            target.ContactType1 = source.ContactType1;
            target.ContactType2 = source.ContactType2;
            target.CreatedAt = source.CreatedAt;
            target.Creator = source.Creator;
            target.IsAvailable = source.IsAvailable;
            target.IsEmergency = source.IsEmergency;
            target.MakerCD = source.MakerCD;
            target.MachineNO = source.MachineNO;
            target.MachineStatus = source.MachineStatus;
            target.MachineType = source.MachineType;
            target.PatrolNO = source.PatrolNO;
            target.Remarks = source.Remarks;
            target.ReportDate = source.ReportDate;
            target.Reporter = source.Reporter;
            target.ReportStatus = source.ReportStatus;
            target.ReportUri = source.ReportUri;
            target.UpdatedAt = source.UpdatedAt;
            target.Updator = source.Updator;
            target.WorkedTimes = source.WorkedTimes;
            target.WorkNO = source.WorkNO;
            target.Province = source.Province;
            target.City = source.City;
            target.Address = source.Address;
            return target;
        }

        /// <summary>
        /// 记录需要更新的字段
        /// </summary>
        /// <param name="update"></param>
        private static void SetUpdateValue(PatrolReportHeader current, Hashtable updateKeys)
        {
            foreach (DictionaryEntry item in updateKeys)
            {
                switch (item.Key.ToString().ToLower())
                {
                    case "patrolno":
                        current.PatrolNO = item.Value.ToString();
                        break;
                    case "reportdate":
                        current.ReportDate = item.Value.ToString();
                        break;
                    case "reporter":
                        current.Reporter = item.Value.ToString();
                        break;
                    case "reportstatus":
                        current.ReportStatus = item.Value.ToString();
                        break;
                    case "makercd":
                        current.MakerCD = item.Value.ToString();
                        break;
                    case "machinetype":
                        current.MachineType = item.Value.ToString();
                        break;
                    case "machineno":
                        current.MachineNO = item.Value.ToString();
                        break;
                    case "workedtimes":
                        current.WorkedTimes = Convert.ToDouble(item.Value);
                        break;
                    case "machinestatus":
                        current.MachineStatus = item.Value.ToString();
                        break;
                    case "isemergency":
                        current.IsEmergency = item.Value.ToString();
                        break;
                    case "remarks":
                        current.Remarks = item.Value.ToString();
                        break;
                    case "contactortype1":
                        current.ContactorType1 = item.Value.ToString();
                        break;
                    case "contactorname1":
                        current.ContactorName1 = item.Value.ToString();
                        break;
                    case "contacttype1":
                        current.ContactType1 = item.Value.ToString();
                        break;
                    case "contaction1":
                        current.Contaction1 = item.Value.ToString();
                        break;
                    case "contactortype2":
                        current.ContactorType2 = item.Value.ToString();
                        break;
                    case "contactorname2":
                        current.ContactorName2 = item.Value.ToString();
                        break;
                    case "contacttype2":
                        current.ContactType2 = item.Value.ToString();
                        break;
                    case "contaction2":
                        current.Contaction2 = item.Value.ToString();
                        break;
                    case "workno":
                        current.WorkNO = item.Value.ToString();
                        break;
                    case "reporturi":
                        current.ReportUri = item.Value.ToString();
                        break;
                    case "province":
                        current.Province = item.Value.ToString();
                        break;
                    case "city":
                        current.City = item.Value.ToString();
                        break;
                    case "address":
                        current.Address = item.Value.ToString();
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
