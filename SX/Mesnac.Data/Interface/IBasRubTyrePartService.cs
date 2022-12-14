using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasRubTyrePartService : IBaseService<BasRubTyrePart>
    {
        PageResult<BasRubTyrePart> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubTyrePartService.QueryParams queryParams);

        string GetNextTyrePartCode();
    }
}
