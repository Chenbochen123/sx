using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class PstMaterialStoreinDetailService : BaseService<PstMaterialStoreinDetail>, IPstMaterialStoreinDetailService
    {
		#region 构造方法

        public PstMaterialStoreinDetailService() : base(){ }

        public PstMaterialStoreinDetailService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialStoreinDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string barcode { get; set; }
            public PageResult<PstMaterialStoreinDetail> pageParams { get; set; }
        }

        public PageResult<PstMaterialStoreinDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialStoreinDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, Barcode, OrderID, ProductNo, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName, ProcDate, 
                                InputNum, InputWeight, RecordDate, A.Remark, A.PieceWeight, A.ProductPlace
                            from PstMaterialStoreinDetail A
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
                            A.MaterCode, D.MaterialName, ProcDate, InputNum, InputWeight, A.RecordDate, A.Remark, A.PieceWeight, A.SourceBillNo, A.SourceOrderID, A.NoticeNo
                            from PstMaterialStoreinDetail A
                            left join PstMaterialStorein E on A.BillNo = E.BillNo
                            left join BasStorage B on E.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            string sql = @"select A.BillNo, Barcode, ProductNo, E.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, 
                            A.MaterCode, D.MaterialName, ProcDate, InputNum, InputWeight, A.RecordDate, A.Remark, A.PieceWeight, A.SourceBillNo, A.SourceOrderID, A.NoticeNo
                            from PstMaterialStoreinDetail A
                            left join PstMaterialStorein E on A.BillNo = E.BillNo
                            left join BasStorage B on E.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "' and A.Barcode = '" + Barcode + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetFromChkdetail(string BillNo)
        {
            return this.GetBySql("select * from PstMaterialChkDetail where DeleteFlag = '0' and ChkResultFlag = '1' and BillNo = '" + BillNo + "'").ToDataSet();
        }

        public bool UpdateStorage(string BillNo, string StorageID, string StoragePlaceID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StorageID) && !string.IsNullOrEmpty(StoragePlaceID))
            {
                sql = @"update PstMaterialStoreinDetail set StorageID = '" + StorageID + "', StoragePlaceID = '" + StoragePlaceID + "' where DeleteFlag = '0' and BillNo = '" + BillNo + "'";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public PstMaterialStoreinDetail GetStoreinDetail(string BillNo, string Barcode)
        {
            PstMaterialStoreinDetail storeinDetail = this.GetBySql("select * from PstMaterialStoreinDetail where BillNo = '" + BillNo + "' and Barcode = '" + Barcode + "'").ToFirst<PstMaterialStoreinDetail>();

            return storeinDetail;
        }
    }
}
