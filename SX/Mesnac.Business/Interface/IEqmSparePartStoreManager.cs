using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    using System.Data;
    public interface IEqmSparePartStoreManager : IBaseManager<EqmSparePartStore>
    {
        /// <summary>
        /// ∑÷“≥∑Ω∑®
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<EqmSparePartStore> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartStoreService.QueryParams queryParams);
        string UpdateSparePartStore(bool storeType, string sparePartCode, string standards, decimal sparePartNum);
        DataSet GetSparePartStoreDetail(string sparePartCode);
    }
}
