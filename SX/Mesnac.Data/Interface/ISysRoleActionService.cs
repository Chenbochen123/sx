using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    /// <summary>
    /// ISysRoleActionService 接口定义
    /// 孙本强 @ 2013-04-03 12:50:58
    /// </summary>
    /// <remarks></remarks>
    public interface ISysRoleActionService : IBaseService<SysRoleAction>
    {
        /// <summary>
        /// 角色操作权限拷贝
        /// 孙本强 @ 2013-04-03 12:50:58
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int CopyForm(string sourceRoleID, string targetRoleID);
    }
}
