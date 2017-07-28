using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Controller;

namespace Model.BusinessRule
{
    /// <summary>
    /// 主键生成器业务类
    /// </summary>
    public class PatrolGenerateNORule
    {
        private PatrolGenerateNO _default;

        public PatrolGenerateNORule() {
            this._default = new PatrolGenerateNO();
            this._default.PrefixCode = String.Empty;
            this._default.CreatedAt = DateTime.Now;
            this._default.CurrentID = 1;
            this._default.DateType = 1;
            this._default.Increment = 1;
            this._default.MaxID = 99999;            
        }

        /// <summary>
        /// 生成编号
        /// </summary>
        /// <param name="code">前缀代码</param>
        /// <returns></returns>
        public string GenerateNO(string code) {
            string newNO = String.Empty;
            PatrolGenerateNOHelper helper = new PatrolGenerateNOHelper();
            this._default.PrefixCode = code.Trim().ToUpper();
            PatrolGenerateNO result = helper.Select(this._default);
            bool success = false;
            if (result == null)
            {
                //插入新类型
                if (helper.Insert(this._default))
                {
                    PatrolGenerateNO oldEntity = helper.Copy(this._default);
                    Hashtable updateKeys =  AutoUpdate(this._default);
                    success = helper.Update(oldEntity, updateKeys);
                    //获取对象
                    result = oldEntity;
                }
                else {
                    //插入失败
                    return null;                
                }
            }
            else
            {
                //存在对象直接更新
                PatrolGenerateNO oldEntity = helper.Copy(result);
                Hashtable updateKeys = AutoUpdate(result);
                success = helper.Update(oldEntity, updateKeys);
                //获取对象
                result = oldEntity;
            }
            if (success)
            {
                //创建新的NO
                newNO = result.PrefixCode + GetDateValue(result) + Padding(result.CurrentID.ToString(),result.MaxID);
            }
            return newNO;
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(PatrolGenerateNO entity) {
            PatrolGenerateNOHelper helper = new PatrolGenerateNOHelper();
            return helper.Delete(entity);
        }


        /// <summary>
        /// 每次生成编号后更新下一次可以用的最新编号
        /// </summary>
        /// <param name="current">当前的生成器</param>
        /// <returns>自动增长更新后的生成器对象</returns>
        private Hashtable AutoUpdate(PatrolGenerateNO current) {
            Hashtable result = new Hashtable();
            switch (current.DateType)
            {
                case (int)PatrolGenerateNOHelper.DateType.Day:
                    if (current.UpdatedAt.HasValue)
                    {
                        if (current.UpdatedAt.Value.Day != DateTime.Now.Day)
                        { 
                            //当前日与最后一次更新的日期不一样时候那么重置计数为0
                            current.CurrentID = 0;
                        }
                    }
                    break;
                case (int)PatrolGenerateNOHelper.DateType.Month:
                    if (current.UpdatedAt.HasValue)
                    {
                        if (current.UpdatedAt.Value.Month != DateTime.Now.Month)
                        {
                            //当前日与最后一次更新的日期不一样时候那么重置计数为0
                            current.CurrentID = 0;
                        }
                    }
                    break;
                case (int)PatrolGenerateNOHelper.DateType.Year:
                    if (current.UpdatedAt.HasValue)
                    {
                        if (current.UpdatedAt.Value.Year != DateTime.Now.Year)
                        {
                            //当前日与最后一次更新的日期不一样时候那么重置计数为0
                            current.CurrentID = 0;
                        }
                    }
                    break;
                default:
                    if (current.MaxID <= current.CurrentID)
                    {
                        //超出最大值了,那么无法插入或者重置
                        current.CurrentID = 0;
                    }
                    break;
            }
            //此处会自增
            current.CurrentID = current.CurrentID + current.Increment;
            current.UpdatedAt = DateTime.Now;
            //添加到更新列表
            result.Add("CurrentID", current.CurrentID);
            result.Add("UpdatedAt", current.UpdatedAt);
            return result;
        }
        /// <summary>
        /// 根据DateType获取日期的字符串值
        /// </summary>
        /// <param name="current">当前的生成器</param>
        /// <returns>日期字符串</returns>
        private String GetDateValue(PatrolGenerateNO current)
        {
            string DateValue = String.Empty;
            switch (current.DateType)
            {
                case (int)PatrolGenerateNOHelper.DateType.Day:
                    DateValue = DateTime.Now.ToString("yyyyMMdd");
                    break;
                case (int)PatrolGenerateNOHelper.DateType.Month:
                    DateValue = DateTime.Now.ToString("yyyyMM");
                    break;
                case (int)PatrolGenerateNOHelper.DateType.Year:
                    DateValue = DateTime.Now.ToString("yyyy");
                    break;
                default:
                    break;
            }
            return DateValue;
        }
        /// <summary>
        /// 自动补全编号长度
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="maxvalue">最大值</param>
        /// <returns>和最大值长度一样的字符串格式数值</returns>
        private string Padding(string value,long maxvalue) {
            string formatvalue = String.Empty;
            int len = maxvalue.ToString().Length;
            if (value.Length > len)
            {
                value = value.Substring(0,len);
            }
            formatvalue = value.PadLeft(len, '0');

            return formatvalue;
        }
    }
}
