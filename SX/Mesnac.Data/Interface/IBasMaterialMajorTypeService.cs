using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialMajorTypeService : IBaseService<BasMaterialMajorType>
    {  
        //物料大类分页方法
        PageResult<BasMaterialMajorType> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialMajorTypeService.QueryParams queryParams);
        //获取物料大类下一个编号
        string GetNextMaterialMajorTypeCode();
    }
}
