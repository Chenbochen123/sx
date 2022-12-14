using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface ICltQmtCheckCtrlService : IBaseService<CltQmtCheckCtrl>
    {
        DataSet GetAvgTrendDataSetByQueryParams(ICltQmtCheckCtrlQueryParams paras);
        DataSet GetCheckNotHGItemCount(ICltQmtCheckCtrlQueryParams paras);
        DataSet GetCheckChart(ICltQmtCheckCtrlQueryParams paras);
        DataSet GetFormatValue(ICltQmtCheckCtrlQueryParams paras);
    }

    public interface ICltQmtCheckCtrlQueryParams
    {
        string BeginDate { get; set; }
        string EndDate { get; set; }
        string MaterCode { get; set; }
        string WorkShopCode { get; set; }
        string ZJSID { get; set; }
        string ItemCd { get; set; }
        string StatisticType { get; set; }
        string UserID { get; set; }
    }
}
