using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Controller;

namespace Model.BusinessRule
{
    /// <summary>
    /// 特巡数据字典业务类
    /// </summary>
    public class PatrolCodeMstRule
    {
        #region 内部属性

        private static PatrolCodeMstHelper controller = new PatrolCodeMstHelper();

        public enum CodeType {
            ContactorType,//联系人类型
            ContactType,//联系方式
            MachineStatus,//机器工作状态
            QuestionLevel,//问题程度
            SpotStatus //点检对象状态        
        }
        //特殊处理后台提交有无不正常图片对应多个数据库状态
        public const string HasErrImage = @"SSS0001";
        #endregion

        #region 基础业务增、删、改、差

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity">新增对象</param>
        /// <returns>true=成功,false=失败</returns>
        public static bool Insert(PatrolCodeMst entity)
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
        public static bool Update(PatrolCodeMst entity,Hashtable updateKeys)
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
        public static bool Delete(PatrolCodeMst entity)
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
        public static List<PatrolCodeMst> SelectAll()
        {
            return controller.SelectAll();
        }

        /// <summary>
        /// 根据用户名称查询唯一用户
        /// </summary>
        /// <param name="searchInfo">带有PatrolNo的查询对象</param>
        /// <returns>指定UserCD值的个人信息</returns>
        public static PatrolCodeMst Select(PatrolCodeMst searchInfo)
        {
            if (searchInfo == null || searchInfo.CodeCD == String.Empty || searchInfo.CodeTypeCD == String.Empty)
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
        public static List<PatrolCodeMst> SearchByCondition(SearchInfo searchInfo) {
            if (searchInfo == null)
            {
                return null;
            }

            return controller.SearchByCondition(searchInfo);        
        }

        #endregion

        #region 扩展业务接口 
        
        /// <summary>
        /// 取得联系人类型列表
        /// </summary>
        /// <returns></returns>
        public static List<PatrolCodeMst> GetListOfContactorType()
        {
            SearchInfo searchInfo = new SearchInfo();
            Hashtable map = new Hashtable();
            //联系人类型
            map.Add("CodeTypeCD", CodeType.ContactorType.ToString());
            searchInfo.CreateSearchInfo(map);

            return controller.SearchByCondition(searchInfo);
        }

        /// <summary>
        /// 取得联系方式列表
        /// </summary>
        /// <returns></returns>
        public static List<PatrolCodeMst> GetListOfContactType()
        {
            SearchInfo searchInfo = new SearchInfo();
            Hashtable map = new Hashtable();
            //联系方式
            map.Add("CodeTypeCD", CodeType.ContactType.ToString());
            searchInfo.CreateSearchInfo(map);

            return controller.SearchByCondition(searchInfo);
        }

        /// <summary>
        /// 取得机器工作状态列表
        /// </summary>
        /// <returns></returns>
        public static List<PatrolCodeMst> GetListOfMachineStatus()
        {
            SearchInfo searchInfo = new SearchInfo();
            Hashtable map = new Hashtable();
            //机器工作状态
            map.Add("CodeTypeCD", CodeType.MachineStatus.ToString());
            searchInfo.CreateSearchInfo(map);

            return controller.SearchByCondition(searchInfo);
        }

        /// <summary>
        /// 取得问题程度列表
        /// </summary>
        /// <returns></returns>
        public static List<PatrolCodeMst> GetListOfQuestionLevel()
        {
            SearchInfo searchInfo = new SearchInfo();
            Hashtable map = new Hashtable();
            //问题程度
            map.Add("CodeTypeCD", CodeType.QuestionLevel.ToString());
            searchInfo.CreateSearchInfo(map);

            return controller.SearchByCondition(searchInfo);
        }

        /// <summary>
        /// 取得部位点检状态列表
        /// </summary>
        /// <returns></returns>
        public static List<PatrolCodeMst> GetListOfSpotStatus()
        {
            SearchInfo searchInfo = new SearchInfo();
            Hashtable map = new Hashtable();
            //点检状态
            map.Add("CodeTypeCD", CodeType.SpotStatus.ToString());
            searchInfo.CreateSearchInfo(map);

            return controller.SearchByCondition(searchInfo);
        }

        #endregion

        #region 辅助方法
        
        #endregion
    }
}
