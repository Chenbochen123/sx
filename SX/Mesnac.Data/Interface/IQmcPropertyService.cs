using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmcPropertyService : IBaseService<QmcProperty>
    {
        PageResult<QmcProperty> GetTablePageDataBySql(Mesnac.Data.Implements.QmcPropertyService.QueryParams queryParams);
        //获取下一个属性编号
        string GetNextPropertyId();
    }
}
