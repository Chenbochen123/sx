using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using NBear.Common;
    using NBear.Data;
    using System.Data;
    public interface IBasEquipManager : IBaseManager<BasEquip>
    {
        PageResult<BasEquip> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipService.QueryParams queryParams);
        string GetNextEquipCode(string equipTypeCode);
        DataSet EquipStorageQuery(Mesnac.Business.Implements.BasEquipManager.QueryParams queryParams);
        DataSet EquipStorageQueryByCode(Mesnac.Business.Implements.BasEquipManager.QueryParams queryParams);
        DataSet UpdateEquipStorage(Mesnac.Business.Implements.BasEquipManager.QueryParams queryParams);

        DataSet getMiLanEquipNodeByWorkShopCode(string workshopCode);
    }
}
