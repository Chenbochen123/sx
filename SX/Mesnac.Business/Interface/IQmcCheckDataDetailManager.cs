using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Implements;
    public interface IQmcCheckDataDetailManager : IBaseManager<QmcCheckDataDetail>
    {
        DataSet GetDataSetByCheckId(string checkId);
        DataTable GetSPCReport(QmcCheckDataDetailService.QueryParams param);
    }
}
