using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasDeptManager : IBaseManager<BasDept>
    {
        PageResult<BasDept> GetTablePageDataBySql(Mesnac.Data.Implements.BasDeptService.QueryParams queryParams);

        string GetNextDepCode();
    }
}
