using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using NBear.Common;
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysPageMenuManager 接口定义
    /// 孙本强 @ 2013-04-03 11:46:20
    /// </summary>
    /// <remarks></remarks>
    public interface ISysPageMenuManager : IBaseManager<SysPageMenu>
    {
        /// <summary>
        /// 获取用户操作的菜单列表
        /// 孙本强 @ 2013-04-03 11:46:20
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="parid">上次菜单ID</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<SysPageMenu> GetUserMenuPageList(string userid, string parid);


        /// <summary>
        /// 判断用户是否存在操作某个页面的权限
        /// 孙本强 @ 2013-04-03 11:46:20
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="pageurl">页面地址</param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool PagePermission(string userid, string pageurl);

        /// <summary>
        /// 获取当前页面的ID
        /// 孙本强 @ 2013-04-03 11:46:20
        /// </summary>
        /// <param name="pageurl">The pageurl.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int GetPageID(string pageurl);

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:46:20
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<SysPageMenu> GetTablePageDataBySql(SysPageMenuManager.QueryParams queryParams);
    }
}
