using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using NBear.Common;
    using Mesnac.Entity;
    /// <summary>
    /// ISysUserRoleManager 接口定义
    /// 孙本强 @ 2013-04-03 11:46:40
    /// </summary>
    /// <remarks></remarks>
    public interface ISysUserRoleManager : IBaseManager<SysUserRole>
    {
        /// <summary>
        /// 获取用户对应的权限
        /// 孙本强 @ 2013-04-03 11:46:40
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<SysRole> GetRoleList(SysRole role, BasUser user);
        /// <summary>
        /// 获取权限对应的用户.
        /// 孙本强 @ 2013-04-03 11:46:40
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<BasUser> GetRoleUserList(SysRole role, BasUser user);
        /// <summary>
        /// 清楚用户角色
        /// 孙本强 @ 2013-04-03 11:46:40
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <remarks></remarks>
        void ClearUserRole(string userid);
    }
}
