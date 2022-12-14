using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasRubTyrePartManager : IBaseManager<BasRubTyrePart>
    {
        PageResult<BasRubTyrePart> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubTyrePartService.QueryParams queryParams);

        string GetNextTyrePartCode();
    }
}
