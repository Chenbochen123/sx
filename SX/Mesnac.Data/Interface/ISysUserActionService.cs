using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysUserActionService 接口定义
    /// 孙本强 @ 2013-04-03 12:50:11
    /// </summary>
    /// <remarks></remarks>
    public interface ISysUserActionService : IBaseService<SysUserAction>
    {
        /// <summary>
        /// 用户权限拷贝
        /// 孙本强 @ 2013-04-03 12:50:11
        /// </summary>
        /// <param name="sourceUserID">The source user ID.</param>
        /// <param name="targetUserID">The target user ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int CopyForm(string sourceUserID, string targetUserID);
        /// <summary>
        /// 通过角色设置用户权限
        /// 孙本强 @ 2013-04-03 12:50:11
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int SetUserActionByRole(string userid);
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:50:11
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysUserAction> GetUserTablePageDataByAction(SysUserActionService.QueryParams queryParams);
    }
}
