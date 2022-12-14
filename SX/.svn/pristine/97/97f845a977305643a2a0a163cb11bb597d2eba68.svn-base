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
    /// SysRoleActionManager 实现类
    /// 孙本强 @ 2013-04-03 11:36:42
    /// </summary>
    /// <remarks></remarks>
    public class SysRoleActionManager : BaseManager<SysRoleAction>, ISysRoleActionManager
    {
        #region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:36:42
        /// </summary>
        private ISysRoleActionService service;

        /// <summary>
        /// 类 SysRoleActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:36:42
        /// </summary>
        /// <remarks></remarks>
        public SysRoleActionManager()
        {
            this.service = new SysRoleActionService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysRoleActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:36:42
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysRoleActionManager(string connectStringKey)
        {
            this.service = new SysRoleActionService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysRoleActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:36:42
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysRoleActionManager(NBear.Data.Gateway way)
        {
            this.service = new SysRoleActionService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// 清除用户角色
        /// 孙本强 @ 2013-04-03 11:36:42
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <remarks></remarks>
        public void ClearUserInRole(string roleid)
        {
            WhereClip where = new WhereClip();
            where.And(SysRoleAction._.RoleID == roleid);
            this.service.DeleteByWhere(where);
        }
        /// <summary>
        /// 添加角色单个操作权限
        /// 孙本强 @ 2013-04-03 11:36:42
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int AppendRoleAction(string roleid, string actionid)
        {
            SysRoleAction role = new SysRoleAction();
            role.RoleID = Convert.ToInt32(roleid);
            role.ActionID = Convert.ToInt32(actionid);
            return this.Insert(role);
        }
        /// <summary>
        /// 删除角色的单个操作权限
        /// 孙本强 @ 2013-04-03 11:36:42
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <remarks></remarks>
        public void RemoveRoleAction(string roleid, string actionid)
        {
            WhereClip where = new WhereClip();
            where.And(SysRoleAction._.RoleID == roleid);
            where.And(SysRoleAction._.ActionID == actionid);
            this.service.DeleteByWhere(where);
        }

        /// <summary>
        /// 角色操作权限拷贝
        /// 孙本强 @ 2013-04-03 11:36:43
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CopyForm(string sourceRoleID, string targetRoleID)
        {
            return this.service.CopyForm(sourceRoleID, targetRoleID);
        }

        /// <summary>
        /// 清除角色权限
        /// 孙本强 @ 2013-04-03 11:36:43
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <remarks></remarks>
        public void ClearRoleAction(string roleid)
        {
            WhereClip where = new WhereClip();
            where.And(SysRoleAction._.RoleID == roleid);
            this.service.DeleteByWhere(where);
        }
    }
}
