using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmSemiStorageService : BaseService<PpmSemiStorage>, IPpmSemiStorageService
    {
		#region 构造方法

        public PpmSemiStorageService() : base(){ }

        public PpmSemiStorageService(string connectStringKey) : base(connectStringKey){ }

        public PpmSemiStorageService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string barcode { get; set; }
            public string equipCode { get; set; }
            public string materCode { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string shiftClassID { get; set; }
            public string backFlag { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<PpmSemiStorage> pageParams { get; set; }
        }

        public PageResult<PpmSemiStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmSemiStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, A.MaterCode, B.MaterialName, CONVERT(varchar(10), A.ValidDate, 120) ValidDate, A.ShiftID, C.ShiftName, A.ShiftClassID, D.ClassName,
                                    A.EquipCode, E.EquipName, A.BarcodeStart, A.BarcodeEnd, A.RealWeight, case when F.OperType = '007' then '1' else '0' end BackFlag, A.RecordDate
                                from PpmSemiStorage A
                                    left join BasMaterial B on A.MaterCode = B.MaterialCode
                                    left join PptShift C on A.ShiftID = C.ObjID
                                    left join PptClass D on A.ShiftClassID = D.ObjID
                                    left join BasEquip E on A.EquipCode = E.EquipCode
                                    left join PpmSemiStorageDetail F on A.Barcode = F.Barcode and A.StorageID = F.StorageID and A.StoragePlaceID = F.StoragePlaceID and F.OperType = '007'
                                where (A.RealWeight > 0 or F.OperType = '007')");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }

            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");

            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");

            if (queryParams.shiftClassID != "all")
                sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");

            if (queryParams.backFlag == "0")
                sqlstr.AppendLine(" AND A.RealWeight > 0");
            if (queryParams.backFlag == "1")
                sqlstr.AppendLine(" AND F.OperType = '007'");

            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }

        public string SubmitRubberBack(string storageID, string storagePlaceID, string barcode, decimal backWeight, string normalFlag, string backReason, string shiftID, string operPerson)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmSubmitRubberBack");
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("Barcode", this.TypeToDbType(barcode.GetType()), barcode);
            sps.AddInputParameter("BackWeight", this.TypeToDbType(backWeight.GetType()), backWeight);
            sps.AddInputParameter("NormalFlag", this.TypeToDbType(normalFlag.GetType()), normalFlag);
            sps.AddInputParameter("BackReason", this.TypeToDbType(backReason.GetType()), backReason);
            sps.AddInputParameter("ShiftID", this.TypeToDbType(shiftID.GetType()), shiftID);
            sps.AddInputParameter("OperPerson", this.TypeToDbType(operPerson.GetType()), operPerson);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public string CancelRubberBack(string barcode, string shiftID, string operPerson)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmCancelRubberBack");
            sps.AddInputParameter("Barcode", this.TypeToDbType(barcode.GetType()), barcode);
            sps.AddInputParameter("ShiftID", this.TypeToDbType(shiftID.GetType()), shiftID);
            sps.AddInputParameter("OperPerson", this.TypeToDbType(operPerson.GetType()), operPerson);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }
    }
}
