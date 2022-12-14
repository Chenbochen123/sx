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
    /// SysUserRoleManager ʵ����
    /// �ﱾǿ @ 2013-04-03 11:42:18
    /// </summary>
    /// <remarks></remarks>
    public class SysUserRoleManager : BaseManager<SysUserRole>, ISysUserRoleManager
    {
		#region ����ע���빹�췽��

        /// <summary>
        /// ���ݿ������
        /// �ﱾǿ @ 2013-04-03 11:42:18
        /// </summary>
        private ISysUserRoleService service;

        /// <summary>
        /// �� SysUserRoleManager ���캯��
        /// �ﱾǿ @ 2013-04-03 11:42:18
        /// </summary>
        /// <remarks></remarks>
        public SysUserRoleManager()
        {
            this.service = new SysUserRoleService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� SysUserRoleManager ���캯��
        /// �ﱾǿ @ 2013-04-03 11:42:18
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public SysUserRoleManager(string connectStringKey)
        {
			this.service = new SysUserRoleService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� SysUserRoleManager ���캯��
        /// �ﱾǿ @ 2013-04-03 11:42:18
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
        /// ��ȡ�û���Ӧ��Ȩ��
        /// �ﱾǿ @ 2013-04-03 11:42:18
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
        /// ��ȡȨ�޶�Ӧ���û�
        /// �ﱾǿ @ 2013-04-03 11:42:18
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
        /// ����û���ɫ
        /// �ﱾǿ @ 2013-04-03 11:42:18
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
