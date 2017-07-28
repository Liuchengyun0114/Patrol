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
using DataBase.EntityManager;

namespace DataBase.Controller
{
    public class ContactorInfoController
    {
        private ContactorInfoEntity _entity;

        public ContactorInfoEntity Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        private SqlCommand _command;

        public SqlCommand Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public ContactorInfoController(ContactorInfoEntity entity,SqlCommand command) {
            this._entity = entity;
            this._command = command;
        }
        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns></returns>
        public bool Insert() {
            //sql语句
            string sql = String.Empty;
            sql += "	INSERT INTO [TshotContactorInfo]";
	        sql += "				 ([ContactNO]";
	        sql += "				 ,[ContactorType]";
	        sql += "				 ,[ContactorName]";
	        sql += "				 ,[ContactType]";
	        sql += "				 ,[Contaction])";
	        sql += "			 values";
	        sql += "						 ('" + this.Entity.ContactNO + "'";
            sql += "						 ,'" + this.Entity.ContactorType + "'";
            sql += "						 ,'" + this.Entity.ContactorName + "'";
            sql += "						 ,'" + this.Entity.ContactType + "'";
            sql += "						 ,'" + this.Entity.Contaction + "');";

            int result = 0;
            try
            {
                //执行sql
                this.Command.CommandText = sql;
                result = this.Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result > 0;
        }
        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <returns></returns>
        public bool Update(ContactorInfoEntity newEntity)
        {
            //sql语句
            string sql = String.Empty;

            sql += "	UPDATE [TshotContactorInfo]";
            sql += "	 SET    [ContactNO] = 'abc'";
            sql += "			,[ContactorType] = 'aaa'";
            sql += "			,[ContactorName] = 'dddd'";
            sql += "			,[ContactType] = 'aaaa'";
            sql += "			,[Contaction] = 'sdf'";
            sql += "	WHERE [ContactNO] = '123';";

            int result = 0;
            try
            {
                //执行sql
                this.Command.CommandText = sql;
                result = this.Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                result = -1;
            }

            return result > 0;
            
        }
    }
}
