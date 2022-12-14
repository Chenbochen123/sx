using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasEquipTypeService : IBaseService<BasEquipType>
    {
        //�豸�����ҳ����
        PageResult<BasEquipType> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipTypeService.QueryParams queryParams);
        //��ȡ�豸������һ�����
        string GetNextEquipTypeCode();
    }
}
