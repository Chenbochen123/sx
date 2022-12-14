using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Components;
    /// <summary>
    /// ISysLoginLogService �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 12:52:28
    /// </summary>
    /// <remarks></remarks>
    public interface ISysLoginLogService : IBaseService<SysLoginLog>
    {
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:52:28
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<SysLoginLog> GetTablePageDataBySql(SysLoginLogService.QueryParams queryParams);
    }
}
