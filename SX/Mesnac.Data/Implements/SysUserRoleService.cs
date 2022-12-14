using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    /// <summary>
    /// SysUserRoleService 实现类
    /// 孙本强 @ 2013-04-03 12:53:05
    /// </summary>
    /// <remarks></remarks>
    public class SysUserRoleService : BaseService<SysUserRole>, ISysUserRoleService
    {
        #region 构造方法

        /// <summary>
        /// 类 SysUserRoleService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:05
        /// </summary>
        /// <remarks></remarks>
        public SysUserRoleService() : base() { }

        /// <summary>
        /// 类 SysUserRoleService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:05
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysUserRoleService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 SysUserRoleService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:05
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysUserRoleService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法


        #region ISysRoleService 成员
        /// <summary>
        /// 获取用户对应的权限
        /// 孙本强 @ 2013-04-03 11:46:40
        /// 孙本强 @ 2013-04-03 12:53:05
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<SysRole> GetRoleList(SysRole role, BasUser user)
        {
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.AppendLine(@"SELECT r.* FROM dbo.BasUser u
                                    INNER JOIN dbo.SysUserRole ur ON u.WorkBarcode=ur.UserCode
                                    INNER JOIN dbo.SysRole r ON ur.RoleID=r.ObjID 
                                    WHERE u.DeleteFlag='0' AND r.DeleteFlag='0'");
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                sqlsb.AppendLine("AND u.UserName like @UserName");
            }
            if (!string.IsNullOrWhiteSpace(user.RealName))
            {
                sqlsb.AppendLine("AND u.RealName like @RealName");
            }
            if (!string.IsNullOrWhiteSpace(role.RoleName))
            {
                sqlsb.AppendLine("AND r.RoleName like @RoleName");
            }
            sqlsb.AppendLine(" ORDER BY r.SeqIdx");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlsb.ToString());
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                css.AddInputParameter("@UserName", this.TypeToDbType(user.UserName.GetType()), "%" + user.UserName + "%");
            }
            if (!string.IsNullOrWhiteSpace(user.RealName))
            {
                css.AddInputParameter("@RealName", this.TypeToDbType(user.RealName.GetType()), "%" + user.RealName + "%");
            }
            if (!string.IsNullOrWhiteSpace(role.RoleName))
            {
                css.AddInputParameter("@RoleName", this.TypeToDbType(role.RoleName.GetType()), "%" + role.RoleName + "%");
            }
            return css.ToArrayList<SysRole>();
        }

        /// <summary>
        /// 获取权限对应的用户.
        /// 孙本强 @ 2013-04-03 11:46:40
        /// 孙本强 @ 2013-04-03 12:53:05
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasUser> GetRoleUserList(SysRole role, BasUser user)
        {
            StringBuilder sqlsb = new StringBuilder();
            sqlsb.AppendLine(@"SELECT u.* FROM dbo.BasUser u
                                    INNER JOIN dbo.SysUserRole ur ON u.WorkBarcode=ur.UserCode
                                    INNER JOIN dbo.SysRole r ON ur.RoleID=r.ObjID 
                                    WHERE u.DeleteFlag='0' AND r.DeleteFlag='0'");
            if ((role.ObjID != null) && (role.ObjID > 0))
            {
                sqlsb.AppendLine("AND r.ObjID=" + role.ObjID.ToString());
            }
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                sqlsb.AppendLine("AND u.UserName like @UserName");
            }
            if (!string.IsNullOrWhiteSpace(user.RealName))
            {
                sqlsb.AppendLine("AND u.RealName like @RealName");
            }
            if (!string.IsNullOrWhiteSpace(role.RoleName))
            {
                sqlsb.AppendLine("AND r.RoleName like @RoleName");
            }
            sqlsb.AppendLine(" ORDER BY u.ObjID");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlsb.ToString());
            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                css.AddInputParameter("@UserName", this.TypeToDbType(user.UserName.GetType()), "%" + user.UserName + "%");
            }
            if (!string.IsNullOrWhiteSpace(user.RealName))
            {
                css.AddInputParameter("@RealName", this.TypeToDbType(user.RealName.GetType()), "%" + user.RealName + "%");
            }
            if (!string.IsNullOrWhiteSpace(role.RoleName))
            {
                css.AddInputParameter("@RoleName", this.TypeToDbType(role.RoleName.GetType()), "%" + role.RoleName + "%");
            }
            return css.ToArrayList<BasUser>();
        }
        #endregion
    }
}
