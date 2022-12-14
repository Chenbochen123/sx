using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using NBear.Common;
    using NBear.Data;
    using System.Data;
    public interface IBasEquipService : IBaseService<BasEquip>
    {
        //�豸��Ϣ��ҳ����
        PageResult<BasEquip> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipService.QueryParams queryParams);
        //��ȡ�豸��Ϣ��һ�����
        string GetNextEquipCode(string equipTypeCode);
        DataSet EquipStorageQuery(Mesnac.Data.Implements.BasEquipService.QueryParams queryParams);
        DataSet EquipStorageQueryByCode(Mesnac.Data.Implements.BasEquipService.QueryParams queryParams);

        DataSet UpdateEquipStorage(Mesnac.Data.Implements.BasEquipService.QueryParams queryParams);
        DataSet getMiLanEquipNodeByWorkShopCode(string workshopCode);
    }
}
