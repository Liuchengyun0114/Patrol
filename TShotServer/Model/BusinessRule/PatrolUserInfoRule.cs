using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Controller;

namespace Model.BusinessRule
{
    /// <summary>
    /// 用户信息业务类
    /// </summary>
    public class PatrolUserInfoRule
    {
        #region 内部属性

        private static PatrolUserInfoHelper controller = new PatrolUserInfoHelper();

        #endregion

        #region 基础业务增、删、改、差

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity">新增对象</param>
        /// <returns>true=成功,false=失败</returns>
        public static bool Insert(PatrolUserInfo entity)
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
        public static bool Update(PatrolUserInfo entity,Hashtable updateKeys)
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
        public static bool Delete(PatrolUserInfo entity)
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
        public static List<PatrolUserInfo> SelectAll()
        {
            return controller.SelectAll();
        }

        /// <summary>
        /// 根据用户名称查询唯一用户
        /// </summary>
        /// <param name="searchInfo">带有UserCD值的查询对象</param>
        /// <returns>指定UserCD值的个人信息</returns>
        public static PatrolUserInfo Select(PatrolUserInfo searchInfo)
        {
            if (searchInfo == null || searchInfo.UserCD == String.Empty)
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
        public static List<PatrolUserInfo> SearchByCondition(SearchInfo searchInfo) {
            if (searchInfo == null)
            {
                return null;
            }

            return controller.SearchByCondition(searchInfo);        
        }

        #endregion

        #region 扩展业务接口 

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="usercd">账号</param>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="pwd">新密码</param>
        /// <returns></returns>
        public static bool UpdatePassword(string usercd, string oldpwd, string pwd) {
            //无效参数
            if (usercd == String.Empty || pwd == String.Empty)
            {
                return false;
            }
            PatrolUserInfo user = new PatrolUserInfo();
            user.UserCD = usercd;

            PatrolUserInfo olduser = Select(user);
            //用户不存在
            if (olduser == null)
            {
                return false;
            }
            //旧密码输入错误
            if (olduser.UserPassword != oldpwd)
            {
                return false;
            }
            Hashtable updatekeys = new Hashtable();
            updatekeys.Add("UserPassword", pwd);
            updatekeys.Add("Token", CreateToken());
            updatekeys.Add("TokenInvalid", SetTokenInvalid(Common.TokenLife));
            updatekeys.Add("UpdatedAt", DateTime.Now);

            return Update(olduser, updatekeys);
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="usercd">用户账号</param>
        /// <param name="pwd">用户密码</param>
        /// <returns>返回token</returns>
        public static string LoginCheck(string usercd, string pwd)
        {
            string token = String.Empty;
            //无效参数
            if (usercd == String.Empty || pwd == String.Empty)
            {
                return token;
            }
            PatrolUserInfo user = new PatrolUserInfo();
            user.UserCD = usercd;

            PatrolUserInfo entity = Select(user);
            //用户不存在
            if (entity == null)
            {
                return token;
            }
            //旧密码输入错误
            if (entity.UserPassword != pwd)
            {
                return token;
            }
            //查询到有效用户,更新token并返回token到客户端
            token = CreateToken();
            Hashtable updatekeys = new Hashtable();
            updatekeys.Add("Token", token);
            updatekeys.Add("TokenInvalid", SetTokenInvalid(Common.TokenLife));
            updatekeys.Add("UpdatedAt", DateTime.Now);

            bool success = controller.Update(entity,updatekeys);
            if (!success)
            {   
                //更新token失败
                token = String.Empty; 
            }            
            return token;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 生成唯一编号
        /// </summary>
        /// <returns></returns>
        public static string GenerateNO()
        {
            return Common.Genetor.GenerateNO(PatrolUserInfoHelper.PrefixCode);
        }

        /// <summary>
        /// 创建Hash值Token字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateToken()
        {
            return Common.GetHash().ToString();

        }
        
        /// <summary>
        /// 计算Token失效日期
        /// </summary>
        /// <param name="hourCount"></param>
        /// <returns></returns>
        public static DateTime SetTokenInvalid(double hourCount)
        {
            return DateTime.Now.AddHours(hourCount);
        }

        /// <summary>
        /// 检查token是否有效
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <param name="token">token</param>
        /// <returns>true=有效,false=失效</returns>
        public static bool CheckToken(PatrolUserInfo entity, string token)
        {
            bool success = false;
            if (entity != null)
            {
                PatrolUserInfo user = Select(entity);
                if (user != null)
                {
                    if (user.Token.Trim() == token.Trim() && user.TokenInvalid != null && DateTime.Now.CompareTo(user.TokenInvalid) >= 0)
                    {
                        //token 相等 并且当前访问日期在有效期之内
                        success = true;
                    }
                }
            }

            return success;
        }

        #endregion
    }
}
