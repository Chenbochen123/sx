using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class PstMaterialAdjustDetailService : BaseService<PstMaterialAdjustDetail>, IPstMaterialAdjustDetailService
    {
		#region 构造方法

        public PstMaterialAdjustDetailService() : base(){ }

        public PstMaterialAdjustDetailService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialAdjustDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public PageResult<PstMaterialAdjustDetail> pageParams { get; set; }
        }

        public PageResult<PstMaterialAdjustDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialAdjustDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.Barcode, A.OrderID, A.ProductNo, A.SourceStoragePlace, B.StoragePlaceName SourceStoragePlaceName,
	                                A.TargetStoragePlace, C.StoragePlaceName TargetStoragePlaceName, A.MaterCode, D.MaterialName, A.ProcDate,
	                                A.AdjustNum, A.PieceWeight, A.AdjustWeight, A.DeleteFlag, A.Remark, A.TypeID, A.ShiftClassID, A.ShiftID
                                from PstMaterialAdjustDetail A
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
                                    from PstMaterialAdjustDetail A
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
                            from PstMaterialAdjustDetail A
                            left join BasStoragePlace B on A.SourceStoragePlace = B.StoragePlaceID
                            left join BasStoragePlace C on A.TargetStoragePlace = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "' and A.Barcode = '" + Barcode + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetDetailInfo(string PlanDate)
        {
            string sql = @"select '" + PlanDate + @"' 领料日期, E.StorageName 原材料库房, D.StoragePlaceName 原材料库位, G.StorageName 目的库房, H.WorkShopName 车间, C.MaterialName 物料名称, '''' + A.Barcode 条码号, SUM(A.AdjustWeight) 领取重量
                            from PstMaterialAdjustDetail A 
	                            left join PstMaterialAdjust B on A.BillNo = B.BillNo
	                            left join BasMaterial C on A.MaterCode = C.MaterialCode
	                            left join BasStoragePlace D on A.SourceStoragePlace = D.StoragePlaceID
	                            left join BasStorage E on D.StorageID = E.StorageID
	                            left join BasStoragePlace F on A.TargetStoragePlace = F.StoragePlaceID
	                            left join BasStorage G on F.StorageID = G.StorageID
	                            left join BasWorkShop H on G.WorkShopCode = H.ObjID
                            where LEN(A.BillNo) = 11 and B.ChkResultFlag = '1' and B.DeleteFlag = '0' and G.StorageType = '1'
	                            and CONVERT(varchar(10), A.RecordDate, 120) = '" + PlanDate + @"' 
                            group by E.StorageName, D.StoragePlaceName, G.StorageName, G.StorageName, H.WorkShopName, C.MaterialName, A.Barcode";

            return this.GetBySql(sql).ToDataSet();
        }
    }
}
