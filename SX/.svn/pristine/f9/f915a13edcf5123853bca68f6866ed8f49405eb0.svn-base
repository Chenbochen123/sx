using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstMaterialAdjustService : BaseService<PstMaterialAdjust>, IPstMaterialAdjustService
    {
		#region 构造方法

        public PstMaterialAdjustService() : base(){ }

        public PstMaterialAdjustService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialAdjustService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string chkResultFlag { get; set; }
            public string hrCode { get; set; }
            public string userCode { get; set; }
            public string barcode { get; set; }
            public string storageType { get; set; }
            public PageResult<PstMaterialAdjust> pageParams { get; set; }
        }

        public PageResult<PstMaterialAdjust> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialAdjust> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.AdjustDate, A.SourceStorage , B.StorageName SourceStorageName, 
	                                A.TargetStorage, C.StorageName TargetStorageName, A.MakerPerson, D.UserName,
	                                A.ChkResultFlag, A.Remark
                                from PstMaterialAdjust A
                                left join BasStorage B on A.SourceStorage = B.StorageID
                                left join BasStorage C on A.TargetStorage = C.StorageID
                                left join BasUser D on A.MakerPerson = D.WorkBarcode
                                where A.DeleteFlag = '0'");
            if (!string.IsNullOrEmpty(queryParams.storageType))
            {
                sqlstr.AppendLine(" AND B.StorageType = '" + queryParams.storageType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.AdjustDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.AdjustDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
            {
                sqlstr.AppendLine(" AND A.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.hrCode))
            {
                sqlstr.AppendLine(" AND D.HRCode = '" + queryParams.hrCode + "'");
            }

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

        public PageResult<PstMaterialAdjust> GetTablePageDataBySqlPrint(QueryParams queryParams)
        {
            PageResult<PstMaterialAdjust> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, D.UserName, C.Barcode, C.OrderID, E.MaterialName, A.AdjustDate, B.StorageName, G.StoragePlaceName, C.AdjustNum, C.AdjustWeight, C.PieceWeight, C.ProcDate, A.TargetStorage, C.TargetStoragePlace 
                                    from PstMaterialAdjust A
                                    left join BasStorage B on A.TargetStorage = B.StorageID
                                    left join PstMaterialAdjustDetail C on A.BillNo = C.BillNo
                                    left join BasUser D on D.WorkBarcode = '" + queryParams.userCode + @"'
                                    left join BasMaterial E on C.MaterCode = E.MaterialCode
                                    left join BasUser F on A.MakerPerson = F.WorkBarcode
                                    left join BasStoragePlace G on C.TargetStoragePlace = G.StoragePlaceID
                                where A.ChkResultFlag = '1' and B.StorageType = '1'");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.AdjustDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.AdjustDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND C.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.hrCode))
            {
                sqlstr.AppendLine(" AND D.HRCode = '" + queryParams.hrCode + "'");
            }

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

        public string GetBillNo()
        {
            int rows = this.GetBySql("select BillNo from PstMaterialAdjust where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'DB' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PstMaterialAdjust where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'DB' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public DataSet GetDetailInfo(string billNo, string storageID, string storagePlaceID, string barcode, string orderID)
        {
            string sql = @"select A.BillNo, D.UserName, C.Barcode, C.OrderID, C.MaterCode, E.MaterialSimpleName, E.MaterialOtherName, E.MaterialName, I.ProductPlace, A.AdjustDate, B.StorageName, G.StoragePlaceName, C.AdjustNum, C.AdjustWeight, C.PieceWeight, C.ProcDate, A.TargetStorage, C.TargetStoragePlace, H.ClassName,
                                case when E.MaxParkTime != null and E.MaxParkTime != 0 then DATEADD(DAY, E.MaxParkTime, C.ProcDate) else '2099-12-31 23:59:59' end ValidDate,
	                                DATEPART(MONTH, C.ProcDate) date1, DATEPART(DAY, C.ProcDate) date2, DATEPART(HOUR, C.ProcDate) date3, DATEPART(MI, C.ProcDate) date4,
                                    DATEPART(MONTH, case when E.MaxParkTime != null and E.MaxParkTime != 0 then DATEADD(DAY, E.MaxParkTime, C.ProcDate) else '2099-12-31 23:59:59' end) date5,
                                    DATEPART(DAY, case when E.MaxParkTime != null and E.MaxParkTime != 0 then DATEADD(DAY, E.MaxParkTime, C.ProcDate) else '2099-12-31 23:59:59' end) date6,
                                    DATEPART(HOUR, case when E.MaxParkTime != null and E.MaxParkTime != 0 then DATEADD(DAY, E.MaxParkTime, C.ProcDate) else '2099-12-31 23:59:59' end) date7,
                                    DATEPART(MI, case when E.MaxParkTime != null and E.MaxParkTime != 0 then DATEADD(DAY, E.MaxParkTime, C.ProcDate) else '2099-12-31 23:59:59' end) date8
                                from PstMaterialAdjust A
                                left join BasStorage B on A.TargetStorage = B.StorageID
                                left join PstMaterialAdjustDetail C on A.BillNo = C.BillNo
                                left join BasUser D on D.WorkBarcode = '000001'
                                left join BasMaterial E on C.MaterCode = E.MaterialCode
                                left join BasUser F on A.MakerPerson = F.WorkBarcode
                                left join BasStoragePlace G on C.TargetStoragePlace = G.StoragePlaceID
                                left join PptClass H on C.ShiftClassID = H.ObjID
                                left join PstStorage I on C.BillNo = I.Barcode and C.SourceStoragePlace = I.StoragePlaceID
                            where A.ChkResultFlag = '1' and B.StorageType = '1'";
            if (!string.IsNullOrEmpty(billNo))
                sql += " and A.BillNo = '" + billNo + "'";
            if (!string.IsNullOrEmpty(storageID))
                sql += " and A.TargetStorage = '" + storageID + "'";
            if (!string.IsNullOrEmpty(storagePlaceID))
                sql += " and C.TargetStoragePlace = '" + storagePlaceID + "'";
            if (!string.IsNullOrEmpty(barcode))
                sql += " and C.Barcode = '" + barcode + "'";
            if (!string.IsNullOrEmpty(orderID))
                sql += " and C.OrderID = '" + orderID + "'";

            return this.GetBySql(sql).ToDataSet();
        }

        public bool IsSameStorageType(string sourceStorageID, string targetStorageID)
        {
            string sql1 = "select StorageType from BasStorage where StorageID = '" + sourceStorageID + "'";
            string sql2 = "select StorageType from BasStorage where StorageID = '" + targetStorageID + "'";

            string sourceStorageType = this.GetBySql(sql1).ToScalar().ToString();
            string targetStorageType = this.GetBySql(sql2).ToScalar().ToString();

            if (sourceStorageType == targetStorageType)
                return true;
            else
                return false;
        }

        public DataSet GetSqlInfo(string sql)
        {
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
