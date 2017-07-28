using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using CM = System.Configuration.ConfigurationManager;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.EntityModel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Specialized;

namespace DataBase
{
    /// <summary>
    /// 数据库工厂单例
    /// </summary>
    public static class SqlHelper
    {


        private static readonly String connectString = ConfigurationManager.ConnectionStrings["TShotConnect"].ConnectionString;
        private static readonly NameValueCollection ConfigList = CM.AppSettings;

        /// <summary>
        /// 执行sql,返回具有指定名称的数据集
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        /// <param name="datasetName">数据集名称</param>
        /// <returns></returns>
        public static DataSet RunSql(string sqlString,string datasetName)
        {
            Console.WriteLine(connectString);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    SqlCommand command = new SqlCommand(sqlString, conn);
                    Console.WriteLine(sqlString);
                    command.Connection = conn;
                    conn.Open();
                    Console.WriteLine("连接成功...");

                    SqlDataAdapter ada = new SqlDataAdapter(command.CommandText, conn);
                    DataSet ds = new DataSet(datasetName);
                    ada.Fill(ds);
                    conn.Close();
                    
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }            
        }
    }
}
