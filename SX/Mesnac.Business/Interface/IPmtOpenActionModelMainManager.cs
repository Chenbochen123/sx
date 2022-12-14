using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPmtOpenActionModelMainManager : IBaseManager<PmtOpenActionModelMain>
    {
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// Ԭ�� @2014��9��29��11:04:06
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<PmtOpenActionModelMain> GetTablePageDataBySql(PmtOpenActionModelMainManager.QueryParams queryParams);
    }
}
