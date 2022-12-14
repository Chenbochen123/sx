using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    using System.Data;
    public interface IEqmSparePartStoreService : IBaseService<EqmSparePartStore>
    {
        //单位的分页方法
        PageResult<EqmSparePartStore> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartStoreService.QueryParams queryParams);
        DataSet GetSparePartStoreDetail(string sparePartCode);
    }
}
