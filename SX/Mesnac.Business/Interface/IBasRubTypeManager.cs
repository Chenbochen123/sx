using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasRubTypeManager : IBaseManager<BasRubType>
    {
        PageResult<BasRubType> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubTypeService.QueryParams queryParams);

        string GetNextRubTypeCode();
    }
}
