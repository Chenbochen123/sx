using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IEqmSparePartService : IBaseService<EqmSparePart>
    {
        //单位的分页方法
        PageResult<EqmSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartService.QueryParams queryParams);
        //获取物料下一个编号
        string GetNextSparePartCode(string MajorTypeID, string MinorTypeID);

        PageResult<EqmSparePart> GetSparePartBySearchKey(Mesnac.Data.Implements.EqmSparePartService.QueryParams queryParams);
    }
}
