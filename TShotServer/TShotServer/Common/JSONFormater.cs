using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PatrolServer.Common
{
    /// <summary>
    /// 公共类库
    /// </summary>
    public class JSONFormater
    {
        public static Hashtable JsonString2Hash1(string json) {
            Hashtable hash = new Hashtable();
            if (json != String.Empty && json[0]=='{' && json[json.Length-1] == '}')
            {
                json = json.Substring(1, json.Length - 2);
                string[] propertyList = json.Split(',');
                foreach (string item in propertyList)
                {
                    // 以":分割键值对
                    string[] datalist = item.Split(':');
                    string key = datalist[0];
                    string value = datalist[1];
                    //左边字符串键名称
                    key = key.Substring(1, key.Length - 2);
                    //右边有可能是字符串，数值或者子Json对象
                    object rightValue;
                    if (value[0]=='\"' && value[value.Length -1]=='\"')
                    {
                        //字符串类型
                        hash.Add(key, value.Substring(1, value.Length - 2));
                    }
                    else if (value[0] == '{' && value[value.Length - 1] == '}')
                    {
                        //子对象类型
                        Hashtable subHash = JsonString2Hash(value);
                        hash.Add(key, subHash);
                    }
                    else {
                        //数值类型或者其他
                        hash.Add(key, value);
                    }
                }
            }
            return hash;
        }

        public static Hashtable JsonString2Hash(string json)
        {
            Hashtable hash = new Hashtable();
            if (json != String.Empty && json[0] == '{' && json[json.Length - 1] == '}')
            {
                json = json.Substring(1, json.Length - 2);                
                string[] propertyList = json.Split(',');
                foreach (string item in propertyList)
                {
                    // 以":分割键值对
                    string[] datalist = item.Split(':');
                    string key = datalist[0];
                    string value = datalist[1];
                    //左边字符串键名称
                    key = key.Substring(1, key.Length - 2);
                    //右边有可能是字符串，数值或者子Json对象
                    object rightValue;
                    if (value[0] == '\"' && value[value.Length - 1] == '\"')
                    {
                        //字符串类型
                        hash.Add(key, value.Substring(1, value.Length - 2));
                    }
                    else if (value[0] == '{' && value[value.Length - 1] == '}')
                    {
                        //子对象类型
                        Hashtable subHash = JsonString2Hash(value);
                        hash.Add(key, subHash);
                    }
                    else
                    {
                        //数值类型或者其他
                        hash.Add(key, value);
                    }
                }
            }
            return hash;
        }
    }

}
