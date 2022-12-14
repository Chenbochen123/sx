using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmEquipScanRateService : BaseService<PpmEquipScanRate>, IPpmEquipScanRateService
    {
		#region 构造方法

        public PpmEquipScanRateService() : base(){ }

        public PpmEquipScanRateService(string connectStringKey) : base(connectStringKey){ }

        public PpmEquipScanRateService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetPpmScanCalcWorkShop(string startDate, string endDate, string workShopCode, string zjsID)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmScanCalcWorkShop");
            sps.AddInputParameter("StartDate", this.TypeToDbType(startDate.GetType()), startDate);
            sps.AddInputParameter("EndDate", this.TypeToDbType(endDate.GetType()), endDate);
            sps.AddInputParameter("WorkShopCode", this.TypeToDbType(workShopCode.GetType()), workShopCode);
            sps.AddInputParameter("UserID", this.TypeToDbType(zjsID.GetType()), zjsID);

            return sps.ToDataSet();
        }

        public DataSet GetPpmScanCalcEquipCode(string startDate, string endDate, string workShopCode)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmScanCalcEquipCode");
            sps.AddInputParameter("StartDate", this.TypeToDbType(startDate.GetType()), startDate);
            sps.AddInputParameter("EndDate", this.TypeToDbType(endDate.GetType()), endDate);
            sps.AddInputParameter("WorkShopCode", this.TypeToDbType(workShopCode.GetType()), workShopCode);

            return sps.ToDataSet();
        }

        public DataSet GetPpmScanCalcHrCode(string startDate, string endDate, string workShopCode)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmScanCalcHrCode");
            sps.AddInputParameter("StartDate", this.TypeToDbType(startDate.GetType()), startDate);
            sps.AddInputParameter("EndDate", this.TypeToDbType(endDate.GetType()), endDate);
            sps.AddInputParameter("WorkShopCode", this.TypeToDbType(workShopCode.GetType()), workShopCode);

            return sps.ToDataSet();
        }

        public DataSet GetPpmScanCalcDetail(string startDate, string endDate, string zjsID)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmScanCalcDetail");
            sps.AddInputParameter("StartDate", this.TypeToDbType(startDate.GetType()), startDate);
            sps.AddInputParameter("EndDate", this.TypeToDbType(endDate.GetType()), endDate);
            sps.AddInputParameter("ZJSID", this.TypeToDbType(zjsID.GetType()), zjsID);

            return sps.ToDataSet();
        }
    }
}
