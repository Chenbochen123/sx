using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    /// <summary>
    /// ISysRoleActionManager 接口定义
    /// 孙本强 @ 2013-04-03 11:46:27
    /// </summary>
    /// <remarks></remarks>
    public interface ISysRoleActionManager : IBaseManager<SysRoleAction>
    {
        /// <summary>
        /// 清除用户角色
        /// 孙本强 @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int CopyForm(string sourceRoleID, string targetRoleID);
        /// <summary>
        /// 添加角色单个操作权限
        /// 孙本强 @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int AppendRoleAction(string roleid, string actionid);
        /// <summary>
        /// 删除角色的单个操作权限
        /// 孙本强 @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <remarks></remarks>
        void RemoveRoleAction(string roleid, string actionid);
        /// <summary>
        /// 角色操作权限拷贝
        /// 孙本强 @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <remarks></remarks>
        void ClearUserInRole(string roleid);
        /// <summary>
        /// 清除角色权限
        /// 孙本强 @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <remarks></remarks>
        void ClearRoleAction(string roleid);
    }
}
