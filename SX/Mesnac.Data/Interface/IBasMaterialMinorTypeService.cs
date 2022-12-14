using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialMinorTypeService : IBaseService<BasMaterialMinorType>
    {
        //物料细类分页方法
        PageResult<BasMaterialMinorType> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialMinorTypeService.QueryParams queryParams);
        PageResult<BasMaterialMinorType> GetQueryRubSectDataPageBySql(Mesnac.Data.Implements.BasMaterialMinorTypeService.QueryParams queryParams);
        //获取物料细类下一个编号
        string GetNextMaterialMinorTypeCode(string majorid);
        //获取细类ObjID
        string GetNextMaterialMinorObjIDCode();
    }
}
