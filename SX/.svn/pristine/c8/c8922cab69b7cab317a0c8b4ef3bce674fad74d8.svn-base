using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstMaterialInventoryDetailService : BaseService<PstMaterialInventoryDetail>, IPstMaterialInventoryDetailService
    {
		#region 构造方法

        public PstMaterialInventoryDetailService() : base(){ }

        public PstMaterialInventoryDetailService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialInventoryDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string barcode { get; set; }
            public PageResult<PstMaterialInventoryDetail> pageParams { get; set; }
        }

        public PageResult<PstMaterialInventoryDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialInventoryDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.OrderID
	                                , A.ProductNo, A.MaterCode, D.MaterialName, A.StorageNum, A.StorageWeight, A.PieceWeight, A.InventoryNum
	                                , A.InventoryWeight, A.ProfitLossFlag, A.DiffNum, A.DiffWeight, A.RecordDate, A.Remark
                                from PstMaterialInventoryDetail A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
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

        public int GetByStorage(string billNo, string storageID, string inventoryDate)
        {
            string sql = @"insert into PstMaterialInventoryDetail(BillNo, StorageID, StoragePlaceID, OrderID, Barcode, MaterCode, StorageNum, StorageWeight, InventoryNum, InventoryWeight, ProfitLossFlag, DiffNum, DiffWeight, RecordDate)
                            select '" + billNo + @"', A.StorageID, A.StoragePlaceID, 1, A.Barcode, B.MaterCode, sum(case when StoreInOut = 'I' then A.Num else -1*A.Num end) StorageNum,
	                            sum(case when StoreInOut = 'I' then Weight else -1*Weight end) StorageWeight, sum(case when StoreInOut = 'I' then A.Num else -1*A.Num end) InventoryNum,
	                            sum(case when StoreInOut = 'I' then Weight else -1*Weight end) InventoryWeight, '0' ProfitLossFlag, 0 DiffNum, 0 DiffWeight, GETDATE() RecordDate
                            from PstStorageDetail A
                            left join PstStorage B on A.Barcode = B.Barcode and A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID
                            where CONVERT(varchar(7), A.RecordDate, 120) = '" + inventoryDate + @"' and A.StorageID = '" + storageID + @"'
                            group by A.StorageID, A.StoragePlaceID, A.Barcode, B.MaterCode";
            int count = this.GetBySql(sql).ExecuteNonQuery();

            return count;
        }

        public DataSet GetByBillNo(string BillNo)
        {
            string sql = @"select A.BillNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.OrderID
	                                , A.ProductNo, A.MaterCode, D.MaterialName, A.StorageNum, A.StorageWeight, A.PieceWeight, A.InventoryNum
	                                , A.InventoryWeight, A.ProfitLossFlag, A.DiffNum, A.DiffWeight, A.RecordDate, A.Remark
                                from PstMaterialInventoryDetail A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.BillNo = '" + BillNo + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
