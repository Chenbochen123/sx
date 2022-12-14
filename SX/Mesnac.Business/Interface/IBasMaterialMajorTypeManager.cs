using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialMajorTypeManager : IBaseManager<BasMaterialMajorType>
    {
        PageResult<BasMaterialMajorType> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialMajorTypeService.QueryParams queryParams);

        string GetNextMaterialMajorTypeCode();
    }
}
