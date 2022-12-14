using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasEquipTypeService : IBaseService<BasEquipType>
    {
        //设备分类分页方法
        PageResult<BasEquipType> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipTypeService.QueryParams queryParams);
        //获取设备分类下一个编号
        string GetNextEquipTypeCode();
    }
}
