using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    public interface IBasWorkCoefficientService : IBaseService<BasWorkCoefficient>
    {
        PageResult<BasWorkCoefficient> GetTablePageDataBySql(Mesnac.Data.Implements.BasWorkCoefficientService.QueryParams queryParams);
        //获取下一个编号
        string GetNextObjID();
    }
    
}
