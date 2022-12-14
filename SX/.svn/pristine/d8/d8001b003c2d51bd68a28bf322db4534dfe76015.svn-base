using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberAdjustingDetailService : BaseService<PpmRubberAdjustingDetail>, IPpmRubberAdjustingDetailService
    {
		#region 构造方法

        public PpmRubberAdjustingDetailService() : base(){ }

        public PpmRubberAdjustingDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberAdjustingDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public PageResult<PpmRubberAdjustingDetail> pageParams { get; set; }
        }

        public PageResult<PpmRubberAdjustingDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberAdjustingDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.OrderID, A.RecordDate, 
	                                A.MaterCode, D.MaterialName, A.DecreaseOrAddFlag, A.AdjustingNum, A.PieceWeight, A.AdjustingWeight
                                from PpmRubberAdjustingDetail A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
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

        public DataSet GetByBillNo(string BillNo)
        {
            return this.GetBySql(@"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.OrderID, A.RecordDate, 
	                                    A.MaterCode, D.MaterialName, A.DecreaseOrAddFlag, A.AdjustingNum, A.PieceWeight, A.AdjustingWeight
                                    from PpmRubberAdjustingDetail A
                                    left join BasStorage B on A.StorageID = B.StorageID
                                    left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                    left join BasMaterial D on A.MaterCode = D.MaterialCode
                                    where A.BillNo = '" + BillNo + "'").ToDataSet();
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            string sql = @"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.OrderID, A.RecordDate, 
	                            A.MaterCode, D.MaterialName, A.DecreaseOrAddFlag, A.AdjustingNum, A.PieceWeight, A.AdjustingWeight
                            from PpmRubberAdjustingDetail A
                            left join BasStorage B on A.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.BillNo = '" + BillNo + "' and A.Barcode = '" + Barcode + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
