using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// IPmtActionManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 11:48:30
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtActionManager : IBaseManager<PmtAction>
    {
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 11:48:31
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<PmtAction> GetTablePageDataBySql(PmtActionManager.QueryParams queryParams);
    }
}
