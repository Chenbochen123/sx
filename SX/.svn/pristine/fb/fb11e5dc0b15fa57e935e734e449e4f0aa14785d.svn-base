using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialMinorTypeManager : IBaseManager<BasMaterialMinorType>
    {
        PageResult<BasMaterialMinorType> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialMinorTypeService.QueryParams queryParams);

        PageResult<BasMaterialMinorType> GetQueryRubSectDataPageBySql(Mesnac.Data.Implements.BasMaterialMinorTypeService.QueryParams queryParams);
        string GetNextMaterialMinorTypeCode(string majorid);
        string GetNextMaterialMinorObjIDCode();
    }
}
