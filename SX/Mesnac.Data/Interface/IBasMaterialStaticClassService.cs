using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialStaticClassService : IBaseService<BasMaterialStaticClass>
    {
        //物料大类分页方法
        PageResult<BasMaterialStaticClass> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialStaticClassService.QueryParams queryParams);
        //获取物料大类下一个编号
        string GetNextMaterialStaticClassCode();
    }
}
