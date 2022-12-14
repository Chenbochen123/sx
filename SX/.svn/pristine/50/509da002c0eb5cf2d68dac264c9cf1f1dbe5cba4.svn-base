using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;

    public interface IQmtCheckStandMasterService : IBaseService<QmtCheckStandMaster>
    {
        DataSet GetDataByParas(IQmtCheckStandMasterParams queryParams);

        void UpdateStandVisionStatByGUID(string guid);

        DataSet GetCheckStandInfoByParas(IQmtCheckStandMasterQueryInfoParams queryParams);
    }

    public interface IQmtCheckStandMasterParams
    {
        string StandId { get; set; }
        string StandCode { get; set; }
        string MaterCode { get; set; }
        string DefineDate { get; set; }
        string WorkerBarcode { get; set; }
        string StandVision { get; set; }
        string StandVisionStat { get; set; }
        string StandDate { get; set; }
        string Choiceness { get; set; }
        string RegDateTime { get; set; }
        string DeleteFlag { get; set; }
        string GUID { get; set; }
        PageResult<QmtCheckStandMaster> PageResult { get; set; }
        string PmtType { get; set; }
    }

    public interface IQmtCheckStandMasterQueryInfoParams
    {
        string MaterCode { get; set; }
        string StandVisionStat { get; set; }
        string StandCode { get; set; }
        string PmtType { get; set; }
    }

}
