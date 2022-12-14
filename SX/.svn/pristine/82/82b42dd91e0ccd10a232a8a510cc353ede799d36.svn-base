using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IEqmSapSparePartManager : IBaseManager<EqmSapSparePart>
    {
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        PageResult<EqmSapSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSapSparePartService.QueryParams queryParams);
        //获取下一个入库编号
        string GetNextSparePartStoreInCode(DateTime storeInDate);
    }
}
