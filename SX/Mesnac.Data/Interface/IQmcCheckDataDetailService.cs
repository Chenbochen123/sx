using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Implements;
    public interface IQmcCheckDataDetailService : IBaseService<QmcCheckDataDetail>
    {
        DataSet GetDataSetByCheckId(string checkId);
        DataTable GetSPCReport(QmcCheckDataDetailService.QueryParams param);
    }
}
