using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    /// <summary>
    /// ISysRoleActionManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 11:46:27
    /// </summary>
    /// <remarks></remarks>
    public interface ISysRoleActionManager : IBaseManager<SysRoleAction>
    {
        /// <summary>
        /// ����û���ɫ
        /// �ﱾǿ @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="sourceRoleID">The source role ID.</param>
        /// <param name="targetRoleID">The target role ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int CopyForm(string sourceRoleID, string targetRoleID);
        /// <summary>
        /// ��ӽ�ɫ��������Ȩ��
        /// �ﱾǿ @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int AppendRoleAction(string roleid, string actionid);
        /// <summary>
        /// ɾ����ɫ�ĵ�������Ȩ��
        /// �ﱾǿ @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <param name="actionid">The actionid.</param>
        /// <remarks></remarks>
        void RemoveRoleAction(string roleid, string actionid);
        /// <summary>
        /// ��ɫ����Ȩ�޿���
        /// �ﱾǿ @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <remarks></remarks>
        void ClearUserInRole(string roleid);
        /// <summary>
        /// �����ɫȨ��
        /// �ﱾǿ @ 2013-04-03 11:46:28
        /// </summary>
        /// <param name="roleid">The roleid.</param>
        /// <remarks></remarks>
        void ClearRoleAction(string roleid);
    }
}
