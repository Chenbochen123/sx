using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasEquipPartInfoManager : IBaseManager<BasEquipPartInfo>
    {
        PageResult<BasEquipPartInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipPartInfoService.QueryParams queryParams);
        string GetNextEquipPartInfoCode(string equipTypeCode);
    }
}
