using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    /// <summary>
    /// SysRoleActionService 实现类
    /// 孙本强 @ 2013-04-03 12:53:21
    /// </summary>
    /// <remarks></remarks>
    public class SysRoleActionService : BaseService<SysRoleAction>, ISysRoleActionService
    {
		#region 构造方法

        /// <summary>
        /// 类 SysRoleActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:21
        /// </summary>
        /// <remarks></remarks>
        public SysRoleActionService() : base(){ }

        /// <summary>
        /// 类 SysRoleActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:21
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysRoleActionService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// 类 SysRoleActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:21
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysRoleActionService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        /// <summary>
        /// 角色操作权限拷贝
        /// 孙本强 @ 2013-04-03 12:50:58
        /// 孙本强 @ 2013-04-03 12:53:21
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CopyForm(string sourceRoleID, string targetRoleID)
        {
            string sql = @"INSERT INTO SysRoleAction(RoleID,ActionID) SELECT " + targetRoleID
                  + ",ActionID FROM SysRoleAction WHERE RoleID=" + sourceRoleID;
            NBear.Data.CustomSqlSection css = this.GetBySql(sql);
            return css.ExecuteNonQuery();
        }
    }
}
