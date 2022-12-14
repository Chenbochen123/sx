using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// IPmtRecipeLogManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 12:44:22
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtRecipeLogManager : IBaseManager<PmtRecipeLog>
    {
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:44:22
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<PmtRecipeLog> GetTablePageDataBySql(PmtRecipeLogManager.QueryParams queryParams);
    }
}
