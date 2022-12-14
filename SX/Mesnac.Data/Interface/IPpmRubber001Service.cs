using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPpmRubber001Service : IBaseService<PpmRubber001>
    {
        PageResult<PpmRubber001> GetTablePageDataBySql(PpmRubber001Service.QueryParams queryParams);
        DataSet GetCondition(string ObjID);
    }
}
