using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using System.Data;
    public class PpmRubberAdjustService : BaseService<PpmRubberAdjust>, IPpmRubberAdjustService
    {
		#region 构造方法

        public PpmRubberAdjustService() : base(){ }

        public PpmRubberAdjustService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberAdjustService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string barcode { get; set; }
            public string storageID { get; set; }
            public string storagePlaceID { get; set; }
            public string equipCode { get; set; }
            public string materCode { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string shiftID { get; set; }
            public string shiftClassID { get; set; }
            public string adjustFlag { get; set; }
            public PageResult<PpmRubberAdjust> pageParams { get; set; }
        }

        public DataSet GetRubberAdjustDetailReportBySql(QueryParams queryParams)
        {

            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode,A.StorageID,C.StorageName,A.StoragePlaceID,D.StoragePlaceName,J.StorageName as ToStorageName,A.ShelfBarcode,E.ShiftName,A.RecordDate,H.MaterialName,A.RealNum,A.AdjustWeight From PpmRubberAdjust A  
                                left join BasStorage C on A.StorageID = C.StorageID
	                                left join BasStoragePlace D on A.StoragePlaceID = D.StoragePlaceID
	                                left join PptShift E on A.ShiftID = E.ObjID
	                                 left join BasMaterial H on A.MaterCode = H.MaterialCode
	                                 left join BasStorage J on A.ToStorageID = J.StorageID
	                                left join BasStoragePlace K on A.ToStoragePlaceID = K.StoragePlaceID
                                    where A.DeleteFlag = '0' ");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.ToString() + "'");
            }

            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (queryParams.shiftID != "all")
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
        }

        public DataSet GetRubberAdjustReportBySql(QueryParams queryParams)
        {
            
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select C.StorageName,D.StoragePlaceName,convert(nvarchar(10),A.RecordDate,120) recorddate,J.StorageName as ToStorageName,H.MaterialName,sum(A.RealNum) as  realnum,sum(A.AdjustWeight) as  adjustweight From PpmRubberAdjust A  
                                left join BasStorage C on A.StorageID = C.StorageID
	                                left join BasStoragePlace D on A.StoragePlaceID = D.StoragePlaceID
	                                left join PptShift E on A.ShiftID = E.ObjID
	                                 left join BasMaterial H on A.MaterCode = H.MaterialCode
	                                 left join BasStorage J on A.ToStorageID = J.StorageID
	                                left join BasStoragePlace K on A.ToStoragePlaceID = K.StoragePlaceID
                                    where A.DeleteFlag = '0' ");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.ToString() + "'");
            }
           
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (queryParams.shiftID != "all")
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            sqlstr.Append(" group by C.StorageName,J.StorageName,D.StoragePlaceName,convert(nvarchar(10),A.RecordDate,120),H.MaterialName");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
        }


        public PageResult<PpmRubberAdjust> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberAdjust> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select distinct A.Barcode, A.StorageID, C.StorageName, A.StoragePlaceID, D.StoragePlaceName, A.ShelfBarcode, A.BarcodeStart, A.BarcodeEnd,
	                                A.ShelfNum, A.MemNote, A.PlanDate, A.ShiftID, E.ShiftName, A.ShiftClassID, F.ClassName, A.EquipCode, G.EquipName, A.RecordDate,
	                                A.MaterCode, H.MaterialName, A.ValidDate, A.ProductWeight, A.StockFlag, A.RealWeight, I.AdjustWeight, case when A.RealWeight > 0 then '0' else '1' end AdjustFlag,
	                                I.ToStorageID, J.StorageName ToStorageName, I.ToStoragePlaceID, K.StoragePlaceName ToStoragePlaceName
                                from PpmRubberStorage A
	                                left join PpmRubberStorageDetail B on A.Barcode = B.Barcode and A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID
	                                left join BasStorage C on A.StorageID = C.StorageID
	                                left join BasStoragePlace D on A.StoragePlaceID = D.StoragePlaceID
	                                left join PptShift E on A.ShiftID = E.ObjID
	                                left join PptClass F on A.ShiftClassID = F.ObjID
	                                left join BasEquip G on A.EquipCode = G.EquipCode
	                                left join BasMaterial H on A.MaterCode = H.MaterialCode
	                                left join PpmRubberAdjust I on A.StorageID = I.StorageID and A.StoragePlaceID = I.StoragePlaceID and A.Barcode = I.Barcode
	                                left join BasStorage J on I.ToStorageID = J.StorageID
	                                left join BasStoragePlace K on I.ToStoragePlaceID = K.StoragePlaceID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }

            if (queryParams.shiftID != "all")
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");

            if (queryParams.shiftClassID != "all")
                sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");

            if (queryParams.adjustFlag == "0")
                sqlstr.AppendLine(" AND A.RealWeight > 0");
            else if (queryParams.adjustFlag == "1")
                sqlstr.AppendLine(" and (A.RealWeight = 0 and B.OperType = '002' and B.RubberType = 'O')");
            else
                sqlstr.AppendLine(" and (A.RealWeight > 0 or (A.RealWeight = 0 and B.OperType = '002' and B.RubberType = 'O'))");

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

        public string SubmitRubberAdjust(string storageID, string storagePlaceID, string barcode, decimal realWeight, string operPerson, string shiftID, string shiftClassID, string toStorageID, string toStoragePlaceID)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmSubmitRubberAdjust");
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("Barcode", this.TypeToDbType(barcode.GetType()), barcode);
            sps.AddInputParameter("RealWeight", this.TypeToDbType(realWeight.GetType()), realWeight);
            sps.AddInputParameter("OperPerson", this.TypeToDbType(operPerson.GetType()), operPerson);
            sps.AddInputParameter("ShiftID", this.TypeToDbType(shiftID.GetType()), shiftID);
            sps.AddInputParameter("ShiftClassID", this.TypeToDbType(shiftClassID.GetType()), shiftClassID);
            sps.AddInputParameter("ToStorageID", this.TypeToDbType(toStorageID.GetType()), toStorageID);
            sps.AddInputParameter("ToStoragePlaceID", this.TypeToDbType(toStoragePlaceID.GetType()), toStoragePlaceID);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public string CancelRubberAdjust(string storageID, string storagePlaceID, string barcode, decimal realWeight, string toStorageID, string toStoragePlaceID)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmCancelRubberAdjust");
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("Barcode", this.TypeToDbType(barcode.GetType()), barcode);
            sps.AddInputParameter("RealWeight", this.TypeToDbType(realWeight.GetType()), realWeight);
            sps.AddInputParameter("ToStorageID", this.TypeToDbType(toStorageID.GetType()), toStorageID);
            sps.AddInputParameter("ToStoragePlaceID", this.TypeToDbType(toStoragePlaceID.GetType()), toStoragePlaceID);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }

//        public class QueryParams
//        {
//            public string billNo { get; set; }
//            public DateTime beginDate { get; set; }
//            public DateTime endDate { get; set; }
//            public string chkResultFlag { get; set; }
//            public PageResult<PpmRubberAdjust> pageParams { get; set; }
//        }

//        public PageResult<PpmRubberAdjust> GetTablePageDataBySql(QueryParams queryParams)
//        {
//            PageResult<PpmRubberAdjust> pageParams = queryParams.pageParams;
//            StringBuilder sqlstr = new StringBuilder();
//            sqlstr.AppendLine(@"select A.BillNo, A.AdjustDate, A.SourceStorage , B.StorageName SourceStorageName, 
//	                                A.TargetStorage, C.StorageName TargetStorageName, A.MakerPerson, D.UserName,
//	                                A.ChkResultFlag, A.Remark
//                                from PpmRubberAdjust A
//                                left join BasStorage B on A.SourceStorage = B.StorageID
//                                left join BasStorage C on A.TargetStorage = C.StorageID
//                                left join BasUser D on A.MakerPerson = D.WorkBarcode
//                                where A.DeleteFlag = '0'");
//            if (!string.IsNullOrEmpty(queryParams.billNo))
//            {
//                sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
//            }
//            if (queryParams.beginDate != DateTime.MinValue)
//                sqlstr.AppendLine(" AND A.AdjustDate >= '" + queryParams.beginDate.ToString() + "'");
//            if (queryParams.endDate != DateTime.MinValue)
//                sqlstr.AppendLine(" AND A.AdjustDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
//            if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
//            {
//                sqlstr.AppendLine(" AND A.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
//            }

//            pageParams.QueryStr = sqlstr.ToString();
//            if (pageParams.PageSize < 0)
//            {
//                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
//                pageParams.DataSet = css.ToDataSet();
//                return pageParams;
//            }
//            else
//            {
//                return this.GetPageDataBySql(pageParams);
//            }
//        }

//        public string GetBillNo()
//        {
//            int rows = this.GetBySql("select BillNo from PpmRubberAdjust where DeleteFlag = '0' and SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
//            if (rows > 0)
//                return this.GetBySql("select 'DB' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PpmRubberAdjust where DeleteFlag = '0' and SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
//            else
//                return this.GetBySql("select 'DB' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
//        }
    }
}
