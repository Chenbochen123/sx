using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    /// <summary>
    /// SysUserRoleManager 实现类
    /// 孙本强 @ 2013-04-03 11:42:18
    /// </summary>
    /// <remarks></remarks>
    public class SysUserRoleManager : BaseManager<SysUserRole>, ISysUserRoleManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:42:18
        /// </summary>
        private ISysUserRoleService service;

        /// <summary>
        /// 类 SysUserRoleManager 构造函数
        /// 孙本强 @ 2013-04-03 11:42:18
        /// </summary>
        /// <remarks></remarks>
        public SysUserRoleManager()
        {
            this.service = new SysUserRoleService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysUserRoleManager 构造函数
        /// 孙本强 @ 2013-04-03 11:42:18
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public SysUserRoleManager(string connectStringKey)
        {
			this.service = new SysUserRoleService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysUserRoleManager 构造函数
        /// 孙本强 @ 2013-04-03 11:42:18
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysUserRoleManager(NBear.Data.Gateway way)
        {
			this.service = new SysUserRoleService(way);
            base.BaseService = this.service;
        }

        #endregion


        /// <summary>
        /// 获取用户对应的权限
        /// 孙本强 @ 2013-04-03 11:42:18
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<SysRole> GetRoleList(SysRole role, BasUser user)
        {
           return this.service.GetRoleList(role,user);
        }
        /// <summary>
        /// 获取权限对应的用户
        /// 孙本强 @ 2013-04-03 11:42:18
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasUser> GetRoleUserList(SysRole role, BasUser user)
        {
            return this.service.GetRoleUserList(role, user);
        }


        /// <summary>
        /// 清楚用户角色
        /// 孙本强 @ 2013-04-03 11:42:18
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <remarks></remarks>
        public void ClearUserRole(string userid)
        {
            WhereClip where = new WhereClip();
            where.And(SysUserRole._.UserCode == userid);
            this.service.DeleteByWhere(where);
        }
    }
}
