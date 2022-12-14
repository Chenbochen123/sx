using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Business.Implements;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysUserActionManager 接口定义
    /// 孙本强 @ 2013-04-03 11:46:35
    /// </summary>
    /// <remarks></remarks>
    public interface ISysUserActionManager : IBaseManager<SysUserAction>
    {

        /// <summary>
        /// 清除用户权限
        /// 孙本强 @ 2013-04-03 11:46:35
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <remarks></remarks>
        void ClearUserAction(string userid);
        /// <summary>
        /// 添加用户单个操作权限
        /// 孙本强 @ 2013-04-03 11:46:35
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int AppendUserAction(string userid, string actionid);
        /// <summary>
        /// 删除角色的单个操作权限
        /// 孙本强 @ 2013-04-03 11:46:36
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <remarks></remarks>
        void RemoveUserAction(string userid, string actionid);

        /// <summary>
        /// 用户权限拷贝
        /// 孙本强 @ 2013-04-03 11:46:36
        /// </summary>
        /// <param name="sourceUserID">The source user ID.</param>
        /// <param name="targetUserID">The target user ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int CopyForm(string sourceUserID, string targetUserID);
        /// <summary>
        /// 通过角色设置用户权限
        /// 孙本强 @ 2013-04-03 11:46:36
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int SetUserActionByRole(string userid);

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:46:36
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysUserAction> GetUserTablePageDataByAction(SysUserActionManager.QueryParams queryParams);
    }
}
