using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using System.Data;
    public interface IPpmRubConsumeService : IBaseService<PpmRubConsume>
    {
        PageResult<PpmRubConsume> GetTablePageDataBySql(Mesnac.Data.Implements.PpmRubConsumeService.QueryParams queryParams);
        DataTable GetTotalPageDataBySql(string begindate, string enddate, string chejian, string equipcode, string matertype, string matercode);
    }
}
