using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasRubTypeService : IBaseService<BasRubType>
    {
        PageResult<BasRubType> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubTypeService.QueryParams queryParams);
        //获取胶料下一个编号
        string GetNextRubTypeCode();
    }
}
