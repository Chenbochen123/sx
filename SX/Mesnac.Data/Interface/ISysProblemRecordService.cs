using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface ISysProblemRecordService : IBaseService<SysProblemRecord>
    {
        PageResult<SysProblemRecord> GetTablePageDataBySql(Mesnac.Data.Implements.SysProblemRecordService.QueryParams queryParams);
    }
}
