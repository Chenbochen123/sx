using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IPmtMixTypeManager : IBaseManager<PmtMixType>
    { 
        /// <summary>
        /// ��ҳ����
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<PmtMixType> GetTablePageDataBySql(Mesnac.Data.Implements.PmtMixTypeService.QueryParams queryParams);
        /// <summary>
        /// ��ȡ��һ������ֵ
        /// </summary>
        /// <returns></returns>
        int GetPmtMixTypeNextPrimaryKeyValue();
    }
}
