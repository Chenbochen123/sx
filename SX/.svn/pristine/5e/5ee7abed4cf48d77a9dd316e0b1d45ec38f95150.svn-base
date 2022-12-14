using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberAdjustDetailService : BaseService<PpmRubberAdjustDetail>, IPpmRubberAdjustDetailService
    {
		#region 构造方法

        public PpmRubberAdjustDetailService() : base(){ }

        public PpmRubberAdjustDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberAdjustDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public PageResult<PpmRubberAdjustDetail> pageParams { get; set; }
        }

        public PageResult<PpmRubberAdjustDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberAdjustDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.Barcode, A.OrderID, A.ProductNo, A.SourceStoragePlace, B.StoragePlaceName SourceStoragePlaceName,
	                                A.TargetStoragePlace, C.StoragePlaceName TargetStoragePlaceName, A.MaterCode, D.MaterialName, A.ProcDate,
	                                A.AdjustNum, A.PieceWeight, A.AdjustWeight, A.DeleteFlag, A.Remark
                                from PpmRubberAdjustDetail A
                                left join BasStoragePlace B on A.SourceStoragePlace = B.StoragePlaceID
                                left join BasStoragePlace C on A.TargetStoragePlace = C.StoragePlaceID
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
            return this.GetBySql(@"select A.BillNo, A.Barcode, A.ProductNo, A.SourceStoragePlace, B.StoragePlaceName SourceStoragePlaceName,
	                                    A.TargetStoragePlace, C.StoragePlaceName TargetStoragePlaceName, A.MaterCode, D.MaterialName, A.ProcDate,
	                                    A.AdjustNum, A.PieceWeight, A.AdjustWeight, A.Remark
                                    from PpmRubberAdjustDetail A
                                    left join BasStoragePlace B on A.SourceStoragePlace = B.StoragePlaceID
                                    left join BasStoragePlace C on A.TargetStoragePlace = C.StoragePlaceID
                                    left join BasMaterial D on A.MaterCode = D.MaterialCode
                                    where A.DeleteFlag = '0'
                                    and BillNo = '" + BillNo + "'").ToDataSet();
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            string sql = @"select A.BillNo, A.Barcode, A.ProductNo, A.SourceStoragePlace, B.StoragePlaceName SourceStoragePlaceName,
	                            A.TargetStoragePlace, C.StoragePlaceName TargetStoragePlaceName, A.MaterCode, D.MaterialName, A.ProcDate,
	                            A.AdjustNum, A.PieceWeight, A.AdjustWeight, A.Remark
                            from PpmRubberAdjustDetail A
                            left join BasStoragePlace B on A.SourceStoragePlace = B.StoragePlaceID
                            left join BasStoragePlace C on A.TargetStoragePlace = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "' and A.Barcode = '" + Barcode + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
