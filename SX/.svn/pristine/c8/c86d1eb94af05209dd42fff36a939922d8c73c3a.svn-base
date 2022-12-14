using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Data.Components;
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    /// <summary>
    /// SysUserActionManager 实现类
    /// 孙本强 @ 2013-04-03 11:40:46
    /// </summary>
    /// <remarks></remarks>
    public class SysUserActionManager : BaseManager<SysUserAction>, ISysUserActionManager
    {
        #region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:40:46
        /// </summary>
        private ISysUserActionService service;

        /// <summary>
        /// 类 SysUserActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <remarks></remarks>
        public SysUserActionManager()
        {
            this.service = new SysUserActionService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysUserActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysUserActionManager(string connectStringKey)
        {
            this.service = new SysUserActionService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysUserActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysUserActionManager(NBear.Data.Gateway way)
        {
            this.service = new SysUserActionService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : SysUserActionService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 清除用户权限
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <remarks></remarks>
        public void ClearUserAction(string userid)
        {
            WhereClip where = new WhereClip();
            where.And(SysUserAction._.UserCode == userid);
            this.service.DeleteByWhere(where); 
        }
        /// <summary>
        /// 添加用户单个操作权限
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int AppendUserAction(string userid, string actionid)
        {
            SysUserAction useract = new SysUserAction();
            useract.UserCode = userid;
            useract.ActionID = Convert.ToInt32(actionid);
            return this.Insert(useract);
        }
        /// <summary>
        /// 删除角色的单个操作权限
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <remarks></remarks>
        public void RemoveUserAction(string userid, string actionid)
        {
            WhereClip where = new WhereClip();
            where.And(SysUserAction._.UserCode == userid);
            where.And(SysUserAction._.ActionID == actionid);
            this.service.DeleteByWhere(where);
        }
        /// <summary>
        /// 用户权限拷贝
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <param name="sourceUserID">The source user ID.</param>
        /// <param name="targetUserID">The target user ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CopyForm(string sourceUserID, string targetUserID)
        {
            return this.service.CopyForm(sourceUserID, targetUserID);
        }

        /// <summary>
        /// 通过角色设置用户权限
        /// 孙本强 @ 2013-04-03 11:40:47
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int SetUserActionByRole(string userid)
        {
            return this.service.SetUserActionByRole(userid);
        }

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:40:48
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysUserAction> GetUserTablePageDataByAction(QueryParams queryParams)
        {
            return this.service.GetUserTablePageDataByAction(queryParams);
        }
    }
}
