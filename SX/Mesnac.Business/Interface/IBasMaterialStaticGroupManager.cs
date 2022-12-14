using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    public interface IBasMaterialStaticGroupManager : IBaseManager<BasMaterialStaticGroup>
    {
        PageResult<BasMaterialStaticGroup> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialStaticGroupService.QueryParams queryParams);
    }
}
