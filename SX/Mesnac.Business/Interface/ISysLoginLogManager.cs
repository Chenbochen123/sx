using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysLoginLogManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 11:18:05
    /// </summary>
    /// <remarks></remarks>
    public interface ISysLoginLogManager : IBaseManager<SysLoginLog>
    {
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 11:18:05
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<SysLoginLog> GetTablePageDataBySql(SysLoginLogManager.QueryParams queryParams);
    }
}
