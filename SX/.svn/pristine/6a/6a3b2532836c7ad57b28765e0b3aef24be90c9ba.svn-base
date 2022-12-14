using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;

    public interface IQmtQrigMasterService : IBaseService<QmtQrigMaster>
    {
        PageResult<QmtQrigMaster> GetDataByQueryParams(IQmtQrigMasterQueryParams queryParams);

        PageResult<QmtQrigMaster> GetDetailDataByQueryParams(IQmtQrigMasterQueryParams queryParams);

        int UpdateSerialIdByLLSerialId(string guid);

        DataSet StaticsQrigProductionAmount(IQmtQrigMasterStaticProdAmountParams paras);
    }

    public interface IQmtQrigMasterQueryParams
    {
        string SPlanDate { get; set; }
        string EPlanDate { get; set; }
        string SCheckDate { get; set; }
        string ECheckDate { get; set; }
        string EquipCode { get; set; }
        string ShiftId { get; set; }
        string ShiftClass { get; set; }
        string CheckPlanClass { get; set; }
        string WorkerBarcode { get; set; }
        string CheckEquipCode { get; set; }
        string MaterCode { get; set; }
        string StandCode { get; set; }
        string TestType { get; set; }
        string DeleteFlag { get; set; }

        string ZJSID { get; set; }
        string CheckItemTypeId { get; set; }

        PageResult<QmtQrigMaster> PageParams { get; set; }

    }

    public interface IQmtQrigMasterStaticProdAmountParams
    {
        string CheckSDate { get; set; }
        string CheckEDate { get; set; }
        string CheckPlanClass { get; set; }
        string WorkShopId { get; set; }
    }
}
