using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmtCheckDetailService : IBaseService<QmtCheckDetail>
    {
        DataSet GetCheckRubberQualityReportDetailByParas(IQmtCheckRubberQualityReportDetailParams paras);
    }

    public interface IQmtCheckRubberQualityReportDetailParams
    {
        string CheckCode { get; set; }
        string SerialId { get; set; }
        string LLSerialID { get; set; }
        string IfCheckNum { get; set; }
        string Grade { get; set; }
    }
}
