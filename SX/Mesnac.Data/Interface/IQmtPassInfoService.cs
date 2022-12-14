using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;

    public interface IQmtPassInfoService : IBaseService<NBear.Common.Entity>
    {
        DataSet GetDataInfoByQueryParams(IQmtPassInfoQueryParams paras);
        void Pass(string barcode, string passUserId, string passUserName, string passMemo);
    }

    public interface IQmtPassInfoQueryParams
    {
        string BeginPlanDate { get; set; }
        string EndPlanDate { get; set; }
        string BeginPassDate { get; set; }
        string EndPassDate { get; set; }
        string PassFlag { get; set; }
        string ShiftId { get; set; }
        string ZJSID { get; set; }
        string Barcode { get; set; }
        string LLBarcode { get; set; }

        PageResult<NBear.Common.Entity> PageResult { get; set; }

    }
}
