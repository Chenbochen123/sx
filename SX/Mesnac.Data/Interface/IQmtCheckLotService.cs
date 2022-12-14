using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IQmtCheckLotService : IBaseService<QmtCheckLot>
    {
        DataSet GetCheckLotResultByParas(IQmtCheckLotParams paras);
    }

    public interface IQmtCheckLotParams
    {
        string PlanDate { get; set; }
        string ZJSID { get; set; }
        string MaterCode { get; set; }
        string EquipCode { get; set; }
        string ShiftId { get; set; }
        string StandCode { get; set; }
    }
}
