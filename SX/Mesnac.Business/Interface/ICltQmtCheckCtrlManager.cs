using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public interface ICltQmtCheckCtrlManager : IBaseManager<CltQmtCheckCtrl>
    {
        DataSet GetAvgTrendDataSetByQueryParams(ICltQmtCheckCtrlQueryParams paras);
        DataSet GetCheckNotHGItemCount(ICltQmtCheckCtrlQueryParams paras);
        DataSet GetCheckChart(ICltQmtCheckCtrlQueryParams paras);
        DataSet GetFormatValue(ICltQmtCheckCtrlQueryParams paras);
    }
}
