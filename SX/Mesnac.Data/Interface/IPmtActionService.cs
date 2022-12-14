using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// IPmtActionService �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 13:01:00
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtActionService : IBaseService<PmtAction>
    {
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 13:01:00
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<PmtAction> GetTablePageDataBySql(PmtActionService.QueryParams queryParams);
    }
}
