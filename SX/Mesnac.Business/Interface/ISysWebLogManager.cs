using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysWebLogManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 11:46:45
    /// </summary>
    /// <remarks></remarks>
    public interface ISysWebLogManager : IBaseManager<SysWebLog>
    {
        /// <summary>
        /// ��Ӳ�����־
        /// �ﱾǿ @ 2013-04-03 11:46:45
        /// </summary>
        /// <param name="sysWebLog">The sys web log.</param>
        /// <param name="sysPageMethod">The sys page method.</param>
        /// <remarks></remarks>
        void Append(SysWebLog sysWebLog, SysPageMethod sysPageMethod);

        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 11:46:45
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<SysWebLog> GetTablePageDataBySql(SysWebLogManager.QueryParams queryParams);
    }
}
