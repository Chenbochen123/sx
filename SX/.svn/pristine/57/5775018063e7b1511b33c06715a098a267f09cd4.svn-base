using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberReturninDetailService : BaseService<PpmRubberReturninDetail>, IPpmRubberReturninDetailService
    {
		#region 构造方法

        public PpmRubberReturninDetailService() : base(){ }

        public PpmRubberReturninDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberReturninDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string barcode { get; set; }
            public PageResult<PpmRubberReturninDetail> pageParams { get; set; }
        }

        public PageResult<PpmRubberReturninDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberReturninDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, Barcode, ProductNo, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName, ProcDate, 
                                ReturninNum, ReturninWeight, RecordDate, A.Remark, A.PieceWeight
                            from PpmRubberReturninDetail A
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0'");
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

        public DataSet GetByBillNo(string BillNo)
        {
            string sql = @"select A.BillNo, Barcode, ProductNo, E.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, 
                            A.MaterCode, D.MaterialName, ProcDate, ReturninNum, ReturninWeight, A.RecordDate, A.Remark, A.PieceWeight
                            from PpmRubberReturninDetail A
                            left join PpmRubberReturnin E on A.BillNo = E.BillNo
                            left join BasStorage B on E.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            string sql = @"select A.BillNo, Barcode, ProductNo, E.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, 
                            A.MaterCode, D.MaterialName, ProcDate, ReturninNum, ReturninWeight, A.RecordDate, A.Remark, A.PieceWeight
                            from PpmRubberReturninDetail A
                            left join PpmRubberReturnin E on A.BillNo = E.BillNo
                            left join BasStorage B on E.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "' and A.Barcode = '" + Barcode + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
