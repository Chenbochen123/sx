using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberStoreinDetailService : BaseService<PpmRubberStoreinDetail>, IPpmRubberStoreinDetailService
    {
		#region 构造方法

        public PpmRubberStoreinDetailService() : base(){ }

        public PpmRubberStoreinDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberStoreinDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string barcode { get; set; }
            public PageResult<PpmRubberStoreinDetail> pageParams { get; set; }
        }

        public PageResult<PpmRubberStoreinDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberStoreinDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, Barcode, OrderID, ProductNo, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName, ProcDate, 
                                InputNum, InputWeight, RecordDate, A.Remark, A.PieceWeight
                            from PpmRubberStoreinDetail A
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
                            from PpmRubberStoreinDetail A
                            left join PpmRubberStorein E on A.BillNo = E.BillNo
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
                            from PpmRubberStoreinDetail A
                            left join PpmRubberStorein E on A.BillNo = E.BillNo
                            left join BasStorage B on E.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "' and A.Barcode = '" + Barcode + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetFromChkdetail(string BillNo)
        {
            return this.GetBySql("select * from PpmRubberChkDetail where DeleteFlag = '0' and ChkResultFlag = '1' and BillNo = '" + BillNo + "'").ToDataSet();
        }

        public bool UpdateStorage(string BillNo, string StorageID, string StoragePlaceID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StorageID) && !string.IsNullOrEmpty(StoragePlaceID))
            {
                sql = @"update PpmRubberStoreinDetail set StorageID = '" + StorageID + "', StoragePlaceID = '" + StoragePlaceID + "' where DeleteFlag = '0' and BillNo = '" + BillNo + "'";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public PpmRubberStoreinDetail GetStoreinDetail(string BillNo, string Barcode)
        {
            PpmRubberStoreinDetail storeinDetail = this.GetBySql("select * from PpmRubberStoreinDetail where BillNo = '" + BillNo + "' and Barcode = '" + Barcode + "'").ToFirst<PpmRubberStoreinDetail>();

            return storeinDetail;
        }
    }
}
