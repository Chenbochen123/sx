using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmEquipYieldReportService : BaseService<PpmEquipYieldReport>, IPpmEquipYieldReportService
    {
		#region 构造方法

        public PpmEquipYieldReportService() : base(){ }

        public PpmEquipYieldReportService(string connectStringKey) : base(connectStringKey){ }

        public PpmEquipYieldReportService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetYieldTotalReport(string totalType, string TotalMonth, string workShopCode, string equipCode, string zjsID)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmGetEquipYieldTotalReport");
            sps.AddInputParameter("WorkShopCode", this.TypeToDbType(workShopCode.GetType()), workShopCode);
            sps.AddInputParameter("EquipCode", this.TypeToDbType(equipCode.GetType()), equipCode);
            sps.AddInputParameter("Month", this.TypeToDbType(TotalMonth.GetType()), TotalMonth);
            sps.AddInputParameter("UserID", this.TypeToDbType(zjsID.GetType()), zjsID);
            sps.AddInputParameter("TotalType", this.TypeToDbType(totalType.GetType()), totalType);

            return sps.ToDataSet();
        }

        public DataSet GetYieldDetailReport(string TotalMonth, string workShopCode, string equipCode, string zjsID, string shiftID)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmGetEquipYieldDetailReport");
            sps.AddInputParameter("WorkShopCode", this.TypeToDbType(workShopCode.GetType()), workShopCode);
            sps.AddInputParameter("EquipCode", this.TypeToDbType(equipCode.GetType()), equipCode);
            sps.AddInputParameter("Month", this.TypeToDbType(TotalMonth.GetType()), TotalMonth);
            sps.AddInputParameter("UserID", this.TypeToDbType(zjsID.GetType()), zjsID);
            sps.AddInputParameter("ShiftID", this.TypeToDbType(shiftID.GetType()), shiftID);

            return sps.ToDataSet();
        }
    }
}
