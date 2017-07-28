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
using System.Data;
using System.Data.SqlClient;

namespace Model.EntityManager
{


    /// <summary>
    /// 特巡报告联合查询控制类
    /// </summary>
    public class PatrolEntity
    {
        private static readonly String connectString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        #region 获取特巡报告列表

        /// <summary>
        /// 获取特巡报告列表
        /// </summary>
        /// <param name="queryString">查询条件</param>
        /// <param name="rangeSting">分页条件</param>
        /// <returns></returns>
        public static DataTable getPatrolList(string queryString, string rangeSting)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        conn.Open();
                        Console.WriteLine("连接成功...");
                        String sqlString = getListSqlString(queryString, rangeSting);
                        Console.WriteLine(sqlString);
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlString, conn);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        conn.Close();

                        return ds.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 取得列表
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="rangeSting"></param>
        /// <returns></returns>
        private static string getListSqlString(string queryString, string rangeSting)
        {
            string sql = String.Empty;
            //查询特巡报告列表sql     
            sql = "   select dp.* from (";
            sql += "	select distinct row_number() over(order by dt.patrolno asc) orderno,";
            sql += "		dt.PatrolNO,";
            sql += "		dt.ReportDate,";
            sql += "		dt.ReporterName as Reporter,";
            sql += "		dt.Customer,";
            sql += "		dt.ReportStatus,";
            sql += "		dt.MachineNM as MachineType,";
            sql += "		dt.MachineNO,";
            sql += "		dt.ErrImageCount";
            sql += "	from";
            sql += "	(";
            sql += "		select ";
            sql += "			t.PatrolNO,";
            sql += "			t.ReportDate,";
            sql += "			t.Reporter,";
            sql += "			t.ReportStatus,";
            sql += "			t.MachineType,";
            sql += "			t.MachineNO,";
            sql += "			t.MachineStatus,";
            sql += "			t.IsEmergency,";
            sql += "			t.Remarks,";
            sql += "			d.LocationCode ,";
            sql += "			d.SpotCode,";
            sql += "			d.Status as HasErrImage,";
            sql += "			sta.StaffNM as  ReporterName,";
            sql += "			sta.CompanyCD as AgencyShop,";
            sql += "			sta.SubCompanyCD as Filiale,";
            sql += "			cust.CUSTNM as Customer,";
            sql += "			mtm.MachineNM as MachineNM,";
            sql += "            isnull(dc.ErrImageCount,0) as ErrImageCount ";
            sql += "		from PatrolReportHeader t";
            sql += "		left join PatrolReportDetail d";
            sql += "		on (t.patrolno = d.patrolno)";
            sql += "		left join MACHINEMST mac";
            sql += "		on (t.machinetype = mac.machinecd	and t.makercd = mac.makercd and t.machineno = mac.machineserialnum)";
            sql += "		left join MACHINETYPEMST mtm";
            sql += "		on (t.machinetype = mtm.machinecd	and t.makercd = mtm.makercd)";
            sql += "		left join STAFFMST sta";
            sql += "		on (t.reporter = sta.staffcd)";
            sql += "		left join CUSTMST cust";
            sql += "		on (mac.CustCD = cust.CUSTCD)";
            sql += "         left join (select prd.PatrolNO, count(prd.PatrolNO) as ErrImageCount";
            sql += "         from PatrolReportDetail prd";
            sql += "         where prd.Status <> 'SSS0001'";
            sql += "         group by prd.PatrolNO) dc";
            sql += "         on (t.PatrolNO = dc.PatrolNO)";
            sql += "		where 1=1";
            sql += queryString;
            sql += "	) dt";
            sql += "         ) dp";
            sql += "	where 1=1";
            sql += rangeSting;

            return sql;
        }
        
        #endregion

        #region 获取特巡报告列表总数量

        /// 获取特巡报告列表总数
        /// </summary>
        /// <param name="queryString">sql</param>
        /// <returns></returns>
        public static int getPatrolCount(string queryString)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        conn.Open();
                        Console.WriteLine("连接成功...");
                        String sqlString = getCountSqlString(queryString);
                        SqlCommand comman = new SqlCommand(sqlString, conn);
                        int count = (int)comman.ExecuteScalar();
                        conn.Close();

                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 取得列表数量
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="rangeSting"></param>
        /// <returns></returns>
        private static string getCountSqlString(string queryString)
        {
            string sql = String.Empty;
            //查询特巡报告列表sql            
            sql = "	    select ";
            sql += "		count(*) as total";
            sql += "	from";
            sql += "	(";
            sql += "        select distinct ";
            sql += "			t.PatrolNO";
            sql += "		from PatrolReportHeader t";
            sql += "		left join PatrolReportDetail d";
            sql += "		on (t.patrolno = d.patrolno)";
            sql += "		left join MACHINEMST mac";
            sql += "		on (t.machinetype = mac.machinecd	and t.makercd = mac.makercd and t.machineno = mac.machineserialnum)";
            sql += "		left join MACHINETYPEMST mtm";
            sql += "		on (t.machinetype = mtm.machinecd	and t.makercd = mtm.makercd)";
            sql += "		left join STAFFMST sta";
            sql += "		on (t.reporter = sta.staffcd)";
            sql += "		left join CUSTMST cust";
            sql += "		on (mac.CustCD = cust.CUSTCD)";
            sql += "         left join (select prd.PatrolNO, count(prd.PatrolNO) as ErrImageCount";
            sql += "         from PatrolReportDetail prd";
            sql += "         where prd.Status <> 'SSS0001'";
            sql += "         group by prd.PatrolNO) dc";
            sql += "         on (t.PatrolNO = dc.PatrolNO)";
            sql += "		where 1=1";
            sql += queryString;
            sql += "	) dt";

            return sql;
        }

        #endregion

        #region 新增特巡报告及明细控制
        public static bool InsertPatrol(PatrolReportHeader header, List<PatrolReportDetail> detailList) {
            SQLEntities context = new SQLEntities();
            bool success = false;
            if (header != null && detailList != null && detailList.Count > 0)
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        context.PatrolReportHeader.AddObject(header);
                        foreach (PatrolReportDetail item in detailList)
                        {
                            context.PatrolReportDetail.AddObject(item);
                        }
                        trans.Complete();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
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
        #endregion

        #region 查询特巡报告及明细控制

        public static DataTable getPatrolHeader(string patrolno)
        {
            SQLEntities context = new SQLEntities();
            DataTable dt = new DataTable();
            if (patrolno != null && patrolno != String.Empty)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlConnection conn = new SqlConnection(connectString))
                        {
                            conn.Open();
                            Console.WriteLine("连接成功...");
                            String sqlString = getHeaderSqlString(patrolno);
                            Console.WriteLine(sqlString);
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlString, conn);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            conn.Close();
                            scope.Complete();
                            dt = ds.Tables[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            context.Dispose();

            return dt;
        }
       
        public static DataTable getPatrolDetail(string patrolno)
        {
            SQLEntities context = new SQLEntities();
            DataTable dt = new DataTable();
            if (patrolno != null && patrolno != String.Empty)
            {
                try
                {

                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlConnection conn = new SqlConnection(connectString))
                        {
                            conn.Open();
                            Console.WriteLine("连接成功...");
                            String sqlString = getDetailSqlString(patrolno);
                            Console.WriteLine(sqlString);
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlString, conn);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            conn.Close();
                            scope.Complete();
                            dt = ds.Tables[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            context.Dispose();

            return dt;
        }

        private static string getHeaderSqlString(string patrolno){
            string sql = String.Empty;
            
            sql += "	select  ";
            sql += "		t.*, ";
            sql += "		sta.StaffNM as  ReporterName, ";
            sql += "		sta.Mobile as  ReporterPhone, ";
            sql += "		cust.CUSTNM as Customer, ";
            sql += "		mtm.MachineNM as MachineName,	 ";
            sql += "		pcm.CodeName as MachineStatusName	 ";
            sql += "	from ";
            sql += "	( ";
            sql += "		select h.*   ";
            sql += "		from PatrolReportHeader h  ";
            sql += "		where h.PatrolNO = '" + patrolno + "' ";
            sql += "	) t ";
            sql += "	left join MACHINEMST mac ";
            sql += "	on (t.machinetype = mac.machinecd	and t.makercd = mac.makercd and t.machineno = mac.machineserialnum) ";
            sql += "	left join MACHINETYPEMST mtm ";
            sql += "	on (t.machinetype = mtm.machinecd	and t.makercd = mtm.makercd) ";
            sql += "	left join STAFFMST sta ";
            sql += "	on (t.reporter = sta.staffcd) ";
            sql += "	left join CUSTMST cust ";
            sql += "	on (mac.CustCD = cust.CUSTCD) ";
            sql += "	left join PatrolCodeMst pcm ";
            sql += "	on (t.MachineStatus = pcm.CodeCD) ";

            return sql;        
        }

        private static string getDetailSqlString(string patrolno)
        {
            string sql = String.Empty;

            sql += "	select  ";
            sql += "		t.*, ";
            sql += "		d.CodeName as StatusName, ";
            sql += "		f.CodeName as QuestionLevelName, ";
            sql += "		h.Name as LocationCodeName, ";
            sql += "		r.Name as SpotCodeName ";
            sql += "	from  ";
            sql += "	(	 ";
            sql += "		select s.*   ";
            sql += "		from PatrolReportDetail s  ";
            sql += "		where s.PatrolNO = '" + patrolno + "' ";
            sql += "	) t ";
            sql += "	left join PatrolCodeMst d ";
            sql += "	on t.[Status] = d.CodeCD ";
            sql += "	left join PatrolCodeMst f ";
            sql += "	on t.QuestionLevel = f.CodeCD ";
            sql += "	left join PatrolSpotParts h ";
            sql += "	on t.LocationCode = h.ID ";
            sql += "	left join PatrolSpotParts r ";
            sql += "	on t.SpotCode = r.ID ";

            return sql;
        }
        
        #endregion

        #region 更新特巡报告头部和明细

        public static bool updatePatrol(string headerSql, string detailSql)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            if (headerSql != null && headerSql != String.Empty && detailSql != null && detailSql != String.Empty)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlConnection conn = new SqlConnection(connectString))
                        {
                            Console.WriteLine("连接成功...");
                            String sqlString = headerSql + detailSql;
                            Console.WriteLine(sqlString);
                            SqlCommand command = new SqlCommand(sqlString, conn);
                            conn.Open();

                            success = command.ExecuteNonQuery() > 0;

                            conn.Close();
                            scope.Complete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);                    
                }
            }

            context.Dispose();

            return success;
        }

        #endregion

        #region 根据哈希值获得特巡编号

        /// <summary>
        /// 根据哈希值取得特巡编号
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        public static string getPatrolNobyReportId(string reportid)
        {
            string patrolno = String.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        conn.Open();
                        Console.WriteLine("连接成功...");
                        String sqlString = getPatrolNobyReportIdSqlString(reportid);
                        SqlCommand comman = new SqlCommand(sqlString, conn);
                        patrolno = (string)comman.ExecuteScalar();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return patrolno;
        }

        /// <summary>
        /// 根据报告书id获得特巡报告书的编号
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="rangeSting"></param>
        /// <returns></returns>
        private static string getPatrolNobyReportIdSqlString(string reportid)
        {
            string sql = String.Empty;
            //查询特巡报告列表sql     
            sql = "    select t.patrolno";
            sql += "    from patrolreportheader t ";
            sql += "    where t.reporturi = '" + reportid + "'";

            return sql;
        }

        #endregion


        #region 创建特巡报告书更新uri地址

        /// <summary>
        /// 创建特巡报告书特巡地址
        /// </summary>
        /// <param name="reportid"></param>
        /// <returns></returns>
        public static bool CreateReportUri(string reporturi, string patrolno)
        {
            bool success = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectString))
                    {
                        conn.Open();
                        Console.WriteLine("连接成功...");
                        String sqlString = getCreateReportUriSqlString(reporturi,patrolno);
                        SqlCommand comman = new SqlCommand(sqlString, conn);
                        patrolno = (string)comman.ExecuteScalar();
                        conn.Close();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }

        /// <summary>
        /// 根据特巡编号创建特巡报告书的地址
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="rangeSting"></param>
        /// <returns></returns>
        private static string getCreateReportUriSqlString(string reporturi, string patrolno)
        {
            string sql = String.Empty;
            //更新特巡报告书uri    
            sql = "  update PatrolReportHeader ";
            sql += "  set ReportUri = '" + reporturi + "' ";
            sql += "  where PatrolNO = '" + patrolno + "';";
            Console.WriteLine(sql);
            return sql;
        }

        #endregion

        #region 常量

        public enum HeaderPropertyFlag
        {
            //头部属性
            PatrolNO,
            ReportDate,
            Reporter,
            ReportStatus,
            MakerCD,
            MachineType,
            MachineNO,
            WorkedTimes,
            MachineStatus,
            MachineStatusName,
            IsEmergency,
            Remarks,
            ContactorType1,
            ContactorName1,
            ContactType1,
            Contaction1,
            ContactorType2,
            ContactorName2,
            ContactType2,
            Contaction2,
            WorkNO,
            ReportUri,
            Province,
            City,
            Address,
            IsAvailable,
            Creator,
            CreatedAt,
            Updator,
            UpdatedAt,

            //头部扩展属性
            ReporterName,
            MakerCDName,
            MachineTypeName,
            MachineName,
            Customer,
            OrderNO,
            ReporterPhone,
            ErrImageCount
        }
        
        public enum DetailPropertyFlag {
            //详情属性
            PatrolNo,
            SubNO,
            LocationCode,
            SpotCode,
            Status,
            QuestionLevel,
            Remarks,
            PicUrl,
            IsSelected,
            IsImportant,

            //详情扩展属性
            LocationCodeName,
            SpotCodeName,
            QuestionLevelName
        }

        #endregion
    }
}
