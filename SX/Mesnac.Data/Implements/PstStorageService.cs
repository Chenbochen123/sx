using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class PstStorageService : BaseService<PstStorage>, IPstStorageService
    {
		#region 构造方法

        public PstStorageService() : base(){ }

        public PstStorageService(string connectStringKey) : base(connectStringKey){ }

        public PstStorageService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string storageID { get; set; }
            public string storagePlaceID { get; set; }
            public string materCode { get; set; }
            public string productNo { get; set; }
            public string barcode { get; set; }
            public string factoryID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string IsEmptyWeight { get; set; }
            public string FacErpcode { get; set; }
            public string alarmFlag { get; set; }
            public PageResult<PstStorage> pageParams { get; set; }
        }

        public PageResult<PstStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, A.ProductNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName
                                    , A.FactoryID, E.FacName, A.MaterCode, D.MaterialName, A.ProcDate
                                    , CONVERT(varchar(10), DATEADD(DAY, D.ValidDate, A.ProcDate), 120) ValidDate
                                    , A.Num, A.PieceWeight, A.RealWeight, A.RecordDate, 0 NewNum
                                    , SUBSTRING(A.Barcode, 15, 4) FacCode, SUBSTRING(A.Barcode, 10, 5) SendDate
                                    , dbo.FuncGetStorageStatus(A.Barcode, A.StorageID, '') Status, A.Batch
                                from PstStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                left join BasFactoryInfo E on A.FactoryID = E.ObjID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.FacErpcode))
            {
                sqlstr.AppendLine(" AND SUBSTRING(A.Barcode, 15, 4) = '" + queryParams.FacErpcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.productNo))
            {
                sqlstr.AppendLine(" AND A.ProductNo = '" + queryParams.productNo + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }

            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (queryParams.IsEmptyWeight == "1")
                sqlstr.AppendLine(" AND A.RealWeight = 0");
            else if (queryParams.IsEmptyWeight == "0")
                sqlstr.AppendLine(" AND A.RealWeight > 0");
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

        public PageResult<PstStorage> GetTablePageDataBySql1(QueryParams queryParams)
        {
            PageResult<PstStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, A.Num, A.PieceWeight, A.RealWeight from PstStorage A 
                                    left join BasStorage B on A.StorageID = B.StorageID
                                    left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID where RealWeight > 0");

            //sqlstr.AppendLine(" AND A.InputDate >= '" + queryParams.beginDate.ToString() + "'");
            //sqlstr.AppendLine(" AND A.InputDate <= '" + queryParams.endDate.ToString() + "'");
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

        public PageResult<PstStorage> GetTablePageDataBySql2(QueryParams queryParams)
        {
            PageResult<PstStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.MaterCode, C.MaterialName, SUM(RealWeight) TotalWeight, C.MaxStock, C.MinStock,
	                                case when SUM(RealWeight) > MaxStock and MaxStock > 0 then '1' when SUM(RealWeight) < MinStock then '2' else '0' end AlarmFlag
                                from PstStorage A
	                                left join BasMaterial C on A.MaterCode = C.MaterialCode
                                where RealWeight > 0");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            sqlstr.AppendLine(" group by A.MaterCode, C.MaterialName, C.MaxStock, C.MinStock");
            if (queryParams.alarmFlag == "0")
                sqlstr.AppendLine(" having (SUM(RealWeight) > MaxStock and MaxStock > 0) or (SUM(RealWeight) < MinStock)");
            else if (queryParams.alarmFlag == "1")
                sqlstr.AppendLine(" having SUM(RealWeight) > MaxStock and MaxStock > 0");
            else if (queryParams.alarmFlag == "2")
                sqlstr.AppendLine(" having SUM(RealWeight) < MinStock");
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
//        public PageResult<PstStorage> GetTablePageDataBySql2(QueryParams queryParams)
//        {
//            PageResult<PstStorage> pageParams = queryParams.pageParams;
//            StringBuilder sqlstr = new StringBuilder();
//            sqlstr.AppendLine(@"select A.StorageID, B.StorageName, A.MaterCode, C.MaterialName, SUM(RealWeight) TotalWeight, C.MaxStock, C.MinStock,
//	                                case when SUM(RealWeight) > MaxStock and MaxStock > 0 then '1' when SUM(RealWeight) < MinStock then '2' else '0' end AlarmFlag
//                                from PstStorage A
//	                                left join BasStorage B on A.StorageID = B.StorageID
//	                                left join BasMaterial C on A.MaterCode = C.MaterialCode
//                                where RealWeight > 0");
//            if (!string.IsNullOrEmpty(queryParams.storageID))
//            {
//                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
//            }
//            if (!string.IsNullOrEmpty(queryParams.materCode))
//            {
//                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
//            }
//            sqlstr.AppendLine(" group by A.StorageID, B.StorageName, A.MaterCode, C.MaterialName, C.MaxStock, C.MinStock");
//            if (queryParams.alarmFlag == "0")
//                sqlstr.AppendLine(" having (SUM(RealWeight) > MaxStock and MaxStock > 0) or (SUM(RealWeight) < MinStock)");
//            else if (queryParams.alarmFlag == "1")
//                sqlstr.AppendLine(" having SUM(RealWeight) > MaxStock and MaxStock > 0");
//            else if (queryParams.alarmFlag == "2")
//                sqlstr.AppendLine(" having SUM(RealWeight) < MinStock");
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

        public DataSet GetStorageInfo(string StorageID, string StoragePlaceID, string MaterCode)
        {
            string sql = @"select A.Barcode, A.ProductNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, 
                                A.FactoryID, E.FacSimpleName, A.MaterCode, D.MaterialName, A.ProcDate, CONVERT(varchar(10), 
                                DATEADD(DAY, D.ValidDate, A.ProcDate), 120) ValidDate, A.Num, A.PieceWeight, A.RealWeight, 
                                A.RecordDate, SUBSTRING(A.Barcode, 15, 4) FacCode, SUBSTRING(A.Barcode, 10, 5) SendDate, 
                                dbo.FuncGetStorageStatus(A.Barcode, A.StorageID, '') Status
                            from PstStorage A
                            left join BasStorage B on A.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            left join BasFactoryInfo E on A.FactoryID = E.ObjID
                            where 1 = 1 and A.RealWeight > 0";
            if (!string.IsNullOrEmpty(StorageID))
                sql += " AND A.StorageID = '" + StorageID + "'";
            if (!string.IsNullOrEmpty(StoragePlaceID))
                sql += " AND A.StoragePlaceID = '" + StoragePlaceID + "'";
            if (!string.IsNullOrEmpty(MaterCode))
                sql += " AND A.MaterCode = '" + MaterCode + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetStoreOutData()
        {
            return this.GetBySql(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, A.Num, A.PieceWeight, A.RealWeight from PstStorage A 
                                    left join BasStorage B on A.StorageID = B.StorageID
                                    left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID where RealWeight > 0").ToDataSet();
        }

        public PstStorage getPstStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode)
        {
            PstStorage storage = this.GetBySql("select * from PstStorage where Barcode = '" + Barcode + "' and StorageID = '" + StorageID + "' and StoragePlaceID = '" + StoragePlaceID + "' and MaterCode = '" + MaterCode + "'").ToFirst<PstStorage>();

            return storage;
        }

        public DataSet GetStorage(string Barcodes)
        {
            return this.GetBySql(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName, A.Num, A.PieceWeight, A.RealWeight, A.Remark, A.InputDate, 0 NewNum
                                from PstStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode where barcode in (" + Barcodes + ")").ToDataSet();
        }

        public DataSet GetStorageTotal(QueryParams queryParams)
        {
            PageResult<PstStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select SUM(Num) TotalNum, SUM(RealWeight) TotalWeight from PstStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                left join BasFactoryInfo E on A.FactoryID = E.ObjID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.FacErpcode))
            {
                sqlstr.AppendLine(" AND SUBSTRING(A.Barcode, 15, 4) = '" + queryParams.FacErpcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.productNo))
            {
                sqlstr.AppendLine(" AND A.ProductNo = '" + queryParams.productNo + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }

            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (queryParams.IsEmptyWeight == "1")
                sqlstr.AppendLine(" AND A.num = 0");
            else if (queryParams.IsEmptyWeight == "0")
                sqlstr.AppendLine(" AND A.num > 0");
            pageParams.QueryStr = sqlstr.ToString();

            return this.GetBySql(sqlstr.ToString()).ToDataSet();
        }
    }
}
