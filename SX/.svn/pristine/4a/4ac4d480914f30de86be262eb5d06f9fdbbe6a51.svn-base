using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasFactoryTypeManager : IBaseManager<BasFactoryType>
    {
        PageResult<BasFactoryType> GetTablePageDataBySql(Mesnac.Data.Implements.BasFactoryTypeService.QueryParams queryParams);
        string GetNextFactoryTypeCode();
    }
}
