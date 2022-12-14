using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IEqmSapSparePartService : IBaseService<EqmSapSparePart>
    {
        //Sap备件的分页方法
        PageResult<EqmSapSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSapSparePartService.QueryParams queryParams);
        //获取下一个入库编号
        string GetNextSparePartStoreInCode(DateTime storeInDate);
    }
}
