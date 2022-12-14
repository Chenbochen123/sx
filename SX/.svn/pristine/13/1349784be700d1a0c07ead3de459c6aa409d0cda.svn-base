using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstShopStorageService : BaseService<PstShopStorage>, IPstShopStorageService
    {
		#region 构造方法

        public PstShopStorageService() : base(){ }

        public PstShopStorageService(string connectStringKey) : base(connectStringKey){ }

        public PstShopStorageService(NBear.Data.Gateway way) : base(way){ }

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
            public string llbarcode { get; set; }
            public PageResult<PstShopStorage> pageParams { get; set; }
        }

        public PageResult<PstShopStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstShopStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, A.ProductNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.FactoryID, E.FacName, A.MaterCode, D.MaterialName, A.ProcDate, CONVERT(varchar(10), DATEADD(DAY, D.ValidDate, A.ProcDate), 120) ValidDate, A.Num,
A.PieceWeight,
case when (A.RealWeight -dbo.FuncGetSplitWeight(A.Barcode,A.StorageID,A.StoragePlaceID) )>0 then  (A.RealWeight -dbo.FuncGetSplitWeight(A.Barcode,A.StorageID,A.StoragePlaceID) ) else 0 end  as RealWeight, 
A.RecordDate, 0 NewNum, SUBSTRING(A.Barcode, 15, 4) FacCode, SUBSTRING(A.Barcode, 10, 5) SendDate
                                from PstShopStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                left join BasFactoryInfo E on A.FactoryID = E.ObjID
                                ");
            if (!string.IsNullOrEmpty(queryParams.llbarcode))
            {
                sqlstr.AppendLine("inner join (select barcode,storageid,storageplaceid from PstShopStorageDetail where boxcode like '%" + queryParams.llbarcode + "%') h on a.barcode = h.barcode and a.storageid = h.storageid and a.storageplaceid = h.storageplaceid ");
            }
            sqlstr.AppendLine("  where 1 = 1 and  len( A.Barcode) = 21  and  (A.RealWeight -dbo.FuncGetSplitWeight(A.Barcode,A.StorageID,A.StoragePlaceID) )>0 ");
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
                sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
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

        public PageResult<PstShopStorage> GetTablePageDataBySql1(QueryParams queryParams)
        {
            PageResult<PstShopStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, A.Num, A.PieceWeight, A.RealWeight 
                                from PstShopStorage A 
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

        public DataSet GetStoreOutData()
        {
            return this.GetBySql(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, A.Num, A.PieceWeight, A.RealWeight 
                                    from PstShopStorage A 
                                    left join BasStorage B on A.StorageID = B.StorageID
                                    left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID where RealWeight > 0").ToDataSet();
        }

        public PstShopStorage getPstShopStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode)
        {
            PstShopStorage shopStorage = this.GetBySql("select * from PstShopStorage where Barcode = '" + Barcode + "' and StorageID = '" + StorageID + "' and StoragePlaceID = '" + StoragePlaceID + "' and MaterCode = '" + MaterCode + "'").ToFirst<PstShopStorage>();

            return shopStorage;
        }

        public PstShopStorage getPstShopStorage(string Barcode, string StorageID, string StoragePlaceID, string SourceBillNo, string SourceOrderID)
        {
            string sql = @"select A.* from PstShopStorage A
                            left join PstShopStorageDetail B on A.Barcode = B.Barcode and A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID
                            where A.Barcode = '" + Barcode + "' and A.StorageID = '" + StorageID + "' and A.StoragePlaceID = '" + StoragePlaceID + "' and B.SourceBillNo = '" + SourceBillNo + "' and SourceOrderID = '" + SourceOrderID + "'";
            PstShopStorage shopStorage = this.GetBySql(sql).ToFirst<PstShopStorage>();

            return shopStorage;
        }

        public DataSet GetShopStorage(string Barcodes)
        {
            return this.GetBySql(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName, A.Num, A.PieceWeight, A.RealWeight, A.Remark, A.InputDate, 0 NewNum
                                from PstShopStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode where barcode in (" + Barcodes + ")").ToDataSet();
        }

        public DataSet GetShopStorageTotal(QueryParams queryParams)
        {
            PageResult<PstShopStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select SUM(Num) TotalNum, SUM(RealWeight) TotalWeight from PstShopStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                left join BasFactoryInfo E on A.FactoryID = E.ObjID ");
            if (!string.IsNullOrEmpty(queryParams.llbarcode))
            {
                sqlstr.AppendLine("inner join (select barcode,storageid,storageplaceid from PstShopStorageDetail where boxcode like '%" + queryParams.llbarcode + "%') h on a.barcode = h.barcode and a.storageid = h.storageid and a.storageplaceid = h.storageplaceid ");
            }
            sqlstr.AppendLine("  where 1 = 1");
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
                sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
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

        public string GetNewBarcode(string barcode, string storageID, string storagePlaceID)
        {
            string sql = string.Empty;
            //暂时不考虑库房库位语句
            if (barcode.Length == 18)
                sql = "select '" + barcode + "' + RIGHT('0000' + convert(varchar, convert(int, SUBSTRING(Barcode, 19, 4)) + 1), 4) NewBarocde from PstShopStorage where SUBSTRING(Barcode, 1, 18) = '" + barcode + "'";
            else if(barcode.Length==21)
                sql = "select '" + barcode + "' + RIGHT('0000' + convert(varchar, convert(int, SUBSTRING(Barcode, 22, 4)) + 1), 4) NewBarocde from PstShopStorage where SUBSTRING(Barcode, 1, 21) = '" + barcode + "'";
            //考虑库房库位语句
            //sql = "select '" + barcode + "' + RIGHT('0000' + convert(varchar, convert(int, SUBSTRING(Barcode, 19, 4)) + 1), 4) NewBarocde from PstShopStorage where SUBSTRING(Barcode, 1, 18) = '" + barcode + "' and StorageID = '" + storageID + "' and StoragePlaceID = '" + storagePlaceID + "'";
            if (this.GetBySql(sql).ToDataSet().Tables[0].Rows.Count > 0)
                return this.GetBySql(sql).ToScalar().ToString();
            else
                return barcode + "0001";
        }
    }
}
