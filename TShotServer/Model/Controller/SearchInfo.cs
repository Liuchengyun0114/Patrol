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
    /// 根据条件查找帮助类(通用信息类)
    /// </summary>
    public class SearchInfo
    {
        private String _whereExpress = String.Empty;
        private List<ObjectParameter> _parameters = new List<ObjectParameter>();

        public String WhereExpress {
            get { return this._whereExpress; }
        }
        public List<ObjectParameter> Parameters {

            get { return this._parameters; }
        }

        /// <summary>
        /// Key=列名,Value=值
        /// </summary>
        /// <param name="searchInfo"></param>
        public void CreateSearchInfo(Hashtable searchInfo)
        {
            this._whereExpress = " 1=1 ";
            //根据查询条件生成表达式
            foreach (DictionaryEntry item in searchInfo)
            {
                String key = item.Key.ToString();
                string wherestring = " and it." + key + " =@" + key;
                ObjectParameter op = new ObjectParameter(key, item.Value);
                this._whereExpress += wherestring;
                this._parameters.Add(op);
            }
        }
        /// <summary>
        /// 将查询对象重置
        /// </summary>
        public void Clear() {
            this._whereExpress = String.Empty;
            this._parameters.Clear();            
        }
    
    }

}
