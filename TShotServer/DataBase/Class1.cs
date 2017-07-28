using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using cm = System.Configuration.ConfigurationManager;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.EntityModel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace DataBase
{
    public class Class1
    {
        public static String connectString = ConfigurationManager.ConnectionStrings["TShotConnect"].ConnectionString;
        
        public static void Main() {
            Console.WriteLine(cm.AppSettings["con1"]);
            DataSet ds = SqlHelper.RunSql("select * from tshotspotparts","data");

            Console.Read();
        }
    }
}
