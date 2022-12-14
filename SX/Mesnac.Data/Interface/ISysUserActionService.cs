using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysUserActionService �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 12:50:11
    /// </summary>
    /// <remarks></remarks>
    public interface ISysUserActionService : IBaseService<SysUserAction>
    {
        /// <summary>
        /// �û�Ȩ�޿���
        /// �ﱾǿ @ 2013-04-03 12:50:11
        /// </summary>
        /// <param name="sourceUserID">The source user ID.</param>
        /// <param name="targetUserID">The target user ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int CopyForm(string sourceUserID, string targetUserID);
        /// <summary>
        /// ͨ����ɫ�����û�Ȩ��
        /// �ﱾǿ @ 2013-04-03 12:50:11
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int SetUserActionByRole(string userid);
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:50:11
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<SysUserAction> GetUserTablePageDataByAction(SysUserActionService.QueryParams queryParams);
    }
}
