using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using System.Data;
    public interface IBasEquipPartRelationService : IBaseService<BasEquipPartRelation>
    {
        //设备部件信息分页方法
        PageResult<BasEquipPartRelation> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipPartRelationService.QueryParams queryParams);
        //获取部门下一个编号
        string GetNextEquipPartRelationCode();

        DataSet GetEquipPartsByEquipCode(string equipCode);
    }
}
