using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasEquipTypeManager : IBaseManager<BasEquipType>
    {
        PageResult<BasEquipType> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipTypeService.QueryParams queryParams);

        string GetNextEquipTypeCode();
    }
}
