using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmStorageService : BaseService<PpmStorage>, IPpmStorageService
    {
		#region 构造方法

        public PpmStorageService() : base(){ }

        public PpmStorageService(string connectStringKey) : base(connectStringKey){ }

        public PpmStorageService(NBear.Data.Gateway way) : base(way){ }

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
            public PageResult<PpmStorage> pageParams { get; set; }
        }

        public PageResult<PpmStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, A.ProductNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.FactoryID, E.FacName, A.MaterCode, D.MaterialName, A.ProcDate, A.Num, A.PieceWeight, A.RealWeight, A.RecordDate, 0 NewNum
                                from PpmStorage A
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

        public PageResult<PpmStorage> GetTablePageDataBySql1(QueryParams queryParams)
        {
            PageResult<PpmStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, A.Num, A.PieceWeight, A.RealWeight from PpmStorage A 
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
            return this.GetBySql(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, A.Num, A.PieceWeight, A.RealWeight from PpmStorage A 
                                    left join BasStorage B on A.StorageID = B.StorageID
                                    left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID where RealWeight > 0").ToDataSet();
        }

        public PpmStorage getPpmStorage(string Barcode, string StorageID, string StoragePlaceID, string MaterCode)
        {
            PpmStorage storage = this.GetBySql("select * from PpmStorage where Barcode = '" + Barcode + "' and StorageID = '" + StorageID + "' and StoragePlaceID = '" + StoragePlaceID + "' and MaterCode = '" + MaterCode + "'").ToFirst<PpmStorage>();

            return storage;
        }

        public DataSet GetStorage(string Barcodes)
        {
            return this.GetBySql(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName, A.Num, A.PieceWeight, A.RealWeight, A.Remark, A.InputDate, 0 NewNum
                                from PpmStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode where barcode in (" + Barcodes + ")").ToDataSet();
        }
    }
}
