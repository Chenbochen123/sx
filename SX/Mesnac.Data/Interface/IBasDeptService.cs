using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasDeptService : IBaseService<BasDept>
    {
        //部门分页方法
        PageResult<BasDept> GetTablePageDataBySql(Mesnac.Data.Implements.BasDeptService.QueryParams queryParams);
        //获取部门下一个编号
        string GetNextDepCode();
    }
}
