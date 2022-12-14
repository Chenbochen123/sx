using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    /// <summary>
    /// 功能操作管理 接口定义
    /// 孙本强 @ 2013-04-03 11:46:06
    /// </summary>
    /// <remarks></remarks>
    public interface ISysPageActionManager : IBaseManager<SysPageAction>
    {
        /// <summary>
        /// 获取所有的页面操作信息
        /// 孙本强 @ 2013-04-03 11:46:07
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetAllPageMenuAction();
        /// <summary>
        /// 获取所有的页面操作信息
        /// 孙本强 @ 2013-04-03 11:46:07
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetAllPageMenuAction(string powerName);
        /// <summary>
        /// 获取当前页面用户的操作信息
        /// 孙本强 @ 2013-04-03 11:46:07
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<SysPageAction> GetUserPageActionList(string url, string userid);

    }
}
