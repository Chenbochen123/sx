using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasEquipPartInfoService : IBaseService<BasEquipPartInfo>
    {   
        //设备部件信息分页方法
        PageResult<BasEquipPartInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipPartInfoService.QueryParams queryParams);
        //获取部门下一个编号
        string GetNextEquipPartInfoCode(string equipTypeCode);
    }
}
