using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IEqmSparePartManager : IBaseManager<EqmSparePart>
    {
        
        /// <summary>
        /// ∑÷“≥∑Ω∑®
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<EqmSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartService.QueryParams queryParams);

        string GetNextSparePartCode(string MajorTypeID, string MinorTypeID);

        PageResult<EqmSparePart> GetSparePartBySearchKey(Mesnac.Data.Implements.EqmSparePartService.QueryParams queryParams);
    }
}
