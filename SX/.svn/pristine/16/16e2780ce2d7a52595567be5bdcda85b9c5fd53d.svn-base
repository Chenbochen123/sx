using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmcSpecService : IBaseService<QmcSpec>
    {
        PageResult<QmcSpec> GetTablePageDataBySql(Mesnac.Data.Implements.QmcSpecService.QueryParams queryParams);
        //获取下一个规格编号
        string GetNextSpecId();
    }
}
