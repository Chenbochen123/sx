using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasDeptErpInfoService : IBaseService<BasDeptErpInfo>
    {
        //部门分页方法
        PageResult<BasDeptErpInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasDeptErpInfoService.QueryParams queryParams);
    }
}
