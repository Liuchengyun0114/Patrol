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
    public class UserEntity
    {
        private static readonly String connectString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        #region 获取特巡报告列表

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="queryString">查询条件</param>
        /// <param name="rangeSting">分页条件</param>
        /// <returns></returns>
        public static DataTable getUserList(string queryString, string rangeSting)
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
        /// 取得用户列表sql
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="rangeSting"></param>
        /// <returns></returns>
        private static string getListSqlString(string queryString, string rangeSting)
        {
            string sql = String.Empty;
            //查询特巡报告列表sql            
            sql = "	select distinct dt.orderno,";
            sql += "		dt.UserCD, ";
            sql += "		dt.UserName, ";
            sql += "		Case  ";
            sql += "			when dt.IsPatrolUser is null then 0 ";
            sql += "			else 1 ";
            sql += "		End as IsPatrolUser, ";
            sql += "		dt.IsAdmin, ";
            sql += "		dt.SearchRange, ";
            sql += "		dt.AgencyShop, ";
            sql += "		dt.Filiale ";
            sql += "	from  ";
            sql += "	( ";
            sql += "		select  row_number() over(order by dd.UserCD asc) orderno ,";
            sql += "			dd.*  ";
            sql += "		from  ";
            sql += "		(			 ";
            sql += "			select ";
            sql += "				t.StaffCD as UserCD, ";
            sql += "				t.StaffNM as UserName, ";
            sql += "				t.CompanyCD as AgencyShopCD, ";
            sql += "				t.SubCompanyCD as FilialeCD, ";
            sql += "				d.UserCD as IsPatrolUser, ";
            sql += "				d.IsAdmin, ";
            sql += "				d.SearchRange, ";
            sql += "				c.CompanyNM as AgencyShop, ";
            sql += "				sc.SubCompanyAbbrNM as Filiale	 ";
            sql += "			from StaffMst t  ";
            sql += "			left join PatrolUserInfo d ";
            sql += "			on (t.StaffCD = d.UserCD) ";
            sql += "			left join CompanyMst c ";
            sql += "			on (t.CompanyCD = c.CompanyCD) ";
            sql += "			left join SubCompanyMst sc ";
            sql += "			on (t.SubCompanyCD = sc.SubCompanyCD) ";
            sql += "		    where 1=1 ";
            sql += queryString;
            sql += "		) dd ";
            sql += "	) dt  ";
            sql += "	where 1=1 ";
            sql += rangeSting;

            return sql;
        }
        
        #endregion


        #region 获取用户列表总数量

        /// 获取用户列表总数
        /// </summary>
        /// <param name="queryString">sql</param>
        /// <returns></returns>
        public static int getUserCount(string queryString)
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
            sql = "	    select";
            sql += "		count(*) as total";
            sql += "	from";
            sql += "	( ";
            sql += "		select  distinct ";
            sql += "				t.StaffCD ";
            sql += "			from StaffMst t  ";
            sql += "			left join PatrolUserInfo d ";
            sql += "			on (t.StaffCD = d.UserCD) ";
            sql += "			left join CompanyMst c ";
            sql += "			on (t.CompanyCD = c.CompanyCD) ";
            sql += "			left join SubCompanyMst sc ";
            sql += "			on (t.SubCompanyCD = sc.SubCompanyCD) ";
            sql += "		    where 1=1 ";
            sql += queryString;
            sql += "	) dt  ";

            return sql;
        }

        #endregion

        #region 新增建友用户到特巡系统控制
        public static bool InsertUser(PatrolUserInfo newuser) {
            SQLEntities context = new SQLEntities();
            bool success = false;
            if (newuser != null)
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    try
                    {
                        context.PatrolUserInfo.AddObject(newuser);
                        Console.WriteLine("新增对象完毕");
                        trans.Complete();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("新增对象异常" + ex.Message);
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

        #region 查询用户信息

        public static DataTable getUserInfo(string usercd)
        {
            SQLEntities context = new SQLEntities();
            DataTable dt = new DataTable();
            if (usercd != null && usercd != String.Empty)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlConnection conn = new SqlConnection(connectString))
                        {
                            conn.Open();
                            Console.WriteLine("连接成功...");
                            String sqlString = getUserSqlString(usercd);
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
       

        private static string getUserSqlString(string usercd){
            string sql = String.Empty;
            //获取指定用户
            sql = "	    select";
            sql += "		dt.UserCD, ";
            sql += "		dt.UserName, ";
            sql += "		Case  ";
            sql += "			when dt.IsPatrolUser is null then 0 ";
            sql += "			else 1 ";
            sql += "		End as IsPatrolUser, ";
            sql += "		dt.IsAdmin, ";
            sql += "		dt.SearchRange, ";
            sql += "		dt.AgencyShop, ";
            sql += "		dt.Filiale ";
            sql += "	from";
            sql += "	( ";
            sql += "			select ";
            sql += "				t.StaffCD as UserCD, ";
            sql += "				t.StaffNM as UserName, ";
            sql += "				d.UserCD as IsPatrolUser, ";
            sql += "				d.IsAdmin, ";
            sql += "				d.SearchRange, ";
            sql += "				c.CompanyNM as AgencyShop, ";
            sql += "				sc.SubCompanyAbbrNM as Filiale	 ";
            sql += "			from ( ";
            sql += "                select u.* from StaffMst u ";
            sql += "                where u.StaffCD='" + usercd + "'";  
            sql += "            ) t  ";
            sql += "			left join PatrolUserInfo d ";
            sql += "			on (t.StaffCD = d.UserCD) ";
            sql += "			left join CompanyMst c ";
            sql += "			on (t.CompanyCD = c.CompanyCD) ";
            sql += "			left join SubCompanyMst sc ";
            sql += "			on (t.SubCompanyCD = sc.SubCompanyCD) ";
            sql += "	) dt ";

            return sql;        
        }
        
        #endregion

        #region 更新用户权限

        public static bool updateUser(string sql)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            if (sql != null && sql != String.Empty)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlConnection conn = new SqlConnection(connectString))
                        {
                            Console.WriteLine("连接成功...");
                            String sqlString = sql;
                            Console.WriteLine(sqlString);
                            SqlCommand command = new SqlCommand(sqlString, conn);
                            conn.Open();

                            success = command.ExecuteNonQuery() > 0;

                            scope.Complete();
                            conn.Close();
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


        #region 从特巡系统删除用户

        public static bool deleteUser(string sql)
        {
            SQLEntities context = new SQLEntities();
            bool success = false;
            if (sql != null && sql != String.Empty)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        using (SqlConnection conn = new SqlConnection(connectString))
                        {
                            Console.WriteLine("连接成功...");
                            String sqlString = sql;
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

        #region 常量

        public enum PropertyFlag
        {
            //头部属性
            UserCD,
            UserPassword,
            Token,
            IsAdmin,
            SearchRange,
            IsAvailable,
            Creator,
            CreatedAt,
            Updator,
            UpdatedAt,
            UserName,
            AgencyShop,
            Filiale,
            IsPatrolUser,
            OrderNO
        }
        /// <summary>
        /// 是否系统管理员标志
        /// </summary>
        public enum AdminFlag
        {
            User = 0,
            Admin = 1
        }
        /// <summary>
        /// 查询范围权限
        /// </summary>
        public enum SearchRangeFlag
        {
            All = 0,
            Filiale = 1,
            Personal = 2
        }
        #endregion
    }
}
