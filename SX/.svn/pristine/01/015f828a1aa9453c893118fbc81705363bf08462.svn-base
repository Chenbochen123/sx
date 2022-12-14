using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
using Mesnac.Data.Components;
    using System.Data;
    public interface IPpmRubConsumeManager : IBaseManager<PpmRubConsume>
    {
        PageResult<PpmRubConsume> GetTablePageDataBySql(Mesnac.Business.Implements.PpmRubConsumeManager.QueryParams queryParams);
        DataTable GetTotalPageDataBySql(string begindate, string enddate, string chejian, string equipcode, string matertype, string matercode);
    }
}
