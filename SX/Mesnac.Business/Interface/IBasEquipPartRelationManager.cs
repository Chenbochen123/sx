using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using System.Data;
    public interface IBasEquipPartRelationManager : IBaseManager<BasEquipPartRelation>
    {
        PageResult<BasEquipPartRelation> GetTablePageDataBySql(Mesnac.Data.Implements.BasEquipPartRelationService.QueryParams queryParams);
        string GetNextEquipPartRelationCode();
        DataSet GetEquipPartsByEquipCode(string equipCode);
    }
}
