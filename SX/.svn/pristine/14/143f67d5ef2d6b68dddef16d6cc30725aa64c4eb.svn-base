using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialStaticClassManager : IBaseManager<BasMaterialStaticClass>
    {
        PageResult<BasMaterialStaticClass> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialStaticClassService.QueryParams queryParams);

        string GetNextMaterialStaticClassCode();
    }
}
