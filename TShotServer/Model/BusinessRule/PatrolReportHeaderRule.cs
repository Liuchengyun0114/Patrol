using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Controller;

namespace Model.BusinessRule
{
    /// <summary>
    /// 特巡报告头表业务类
    /// </summary>
    public class PatrolReportHeaderRule
    {
        #region 内部属性

        private static PatrolReportHeaderHelper controller = new PatrolReportHeaderHelper();

        #endregion

        #region 基础业务增、删、改、差

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity">新增对象</param>
        /// <returns>true=成功,false=失败</returns>
        public static bool Insert(PatrolReportHeader entity)
        {
            if (entity != null)
            {
                return controller.Insert(entity);
            }
            return false;
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity">要更新对象</param>
        /// <param name="updateKeys">更新列表</param>
        /// <returns>true=成功,false=失败</returns>
        public static bool Update(PatrolReportHeader entity,Hashtable updateKeys)
        {
            if (entity != null && updateKeys != null && updateKeys.Count > 0)
            {
                return controller.Update(entity,updateKeys);
            }
            return false;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity">删除对象 主键必须设置</param>
        /// <returns></returns>
        public static bool Delete(PatrolReportHeader entity)
        {
            if (entity != null)
            {
                return controller.Delete(entity);
            }
            return false;
        }

        /// <summary>
        /// 返回用户所有数据集合
        /// </summary>
        /// <returns></returns>
        public static List<PatrolReportHeader> SelectAll()
        {
            return controller.SelectAll();
        }

        /// <summary>
        /// 根据用户名称查询唯一用户
        /// </summary>
        /// <param name="searchInfo">带有PatrolNo的查询对象</param>
        /// <returns>指定UserCD值的个人信息</returns>
        public static PatrolReportHeader Select(PatrolReportHeader searchInfo)
        {
            if (searchInfo == null || searchInfo.PatrolNO == String.Empty)
            {
                return null;
            }
            return controller.Select(searchInfo);
        }

        /// <summary>
        /// 根据指定条件查询用户集合
        /// </summary>
        /// <param name="searchInfo">查询条件</param>
        /// <returns>用户列表</returns>
        public static List<PatrolReportHeader> SearchByCondition(SearchInfo searchInfo) {
            if (searchInfo == null)
            {
                return null;
            }

            return controller.SearchByCondition(searchInfo);        
        }

        #endregion

        #region 扩展业务接口 
        public static PatrolReportHeader GetInstance(string patrolno) {
            if (patrolno == null || patrolno == String.Empty)
            {
                return null;
            }
            PatrolReportHeader header = new PatrolReportHeader();
            header.PatrolNO = patrolno;
            return controller.Select(header);
        }
        #endregion


        #region 辅助方法
        /// <summary>
        /// 生成唯一编号
        /// </summary>
        /// <returns></returns>
        public static string GenerateNO()
        {
            return Common.Genetor.GenerateNO(PatrolReportHeaderHelper.PrefixCode);
        }
        #endregion
    }
}
