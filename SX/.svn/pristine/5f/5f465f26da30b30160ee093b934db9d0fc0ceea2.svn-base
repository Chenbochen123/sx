using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// 系统用户 接口定义
    /// 孙本强 @ 2013-04-03 13:10:37
    /// </summary>
    public interface IBasUserManager : IBaseManager<BasUser>
    {

        /// <summary>
        /// 获取用户ID
        /// 孙本强 @ 2013-04-26 16:10:37
        /// </summary>
        /// <value>The user ID.</value>
        string UserID { get; }
        /// <summary>
        /// 校验用户登录信息
        /// 孙本强 @ 2013-04-26 16:10:37
        /// </summary>
        /// <param name="loginUser">The login user.</param>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        bool Login(BasUser loginUser, out BasUser user);
        /// <summary>
        /// 用户登出
        /// 孙本强 @ 2013-04-26 16:10:37
        /// </summary>
        void Logout();

        /// <summary>
        /// 获取用户列表信息
        /// 孙本强 @ 2013-04-26 16:10:37
        /// </summary>
        /// <param name="queryParams">The query params.</param>
        /// <returns>PageResult{BasUser}.</returns>
        PageResult<BasUser> GetTablePageDataBySql(BasUserManager.QueryParams queryParams);

        /// <summary>
        /// 获取下一个用户编号
        /// 孙本强 @ 2013-04-26 16:10:37
        /// </summary>
        /// <returns>System.String.</returns>
        string GetNextWorkBarcode();

    }
}
