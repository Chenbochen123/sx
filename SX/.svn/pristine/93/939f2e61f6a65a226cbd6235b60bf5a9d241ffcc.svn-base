using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberStoreoutDetailService : BaseService<PpmRubberStoreoutDetail>, IPpmRubberStoreoutDetailService
    {
		#region 构造方法

        public PpmRubberStoreoutDetailService() : base(){ }

        public PpmRubberStoreoutDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberStoreoutDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public PageResult<PpmRubberStoreoutDetail> pageParams { get; set; }
        }

        public PageResult<PpmRubberStoreoutDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberStoreoutDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.Barcode, A.ProductNo, A.StoragePlaceID, B.StoragePlaceName, A.MaterCode, C.MaterialName, 
	                                A.ProcDate, A.OutputNum, A.PieceWeight, A.OutputWeight, A.RecordDate, A.Remark 
                                from PpmRubberStoreoutDetail A
                                left join BasStoragePlace B on A.StoragePlaceID = B.StoragePlaceID
                                left join BasMaterial C on A.MaterCode = C.MaterialCode
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

        public int GetOrderID(string Barcode)
        {
            DataSet ds = this.GetBySql("select OrderID from PpmRubberStoreoutDetail where Barcode = '" + Barcode + "'").ToDataSet();
            DataSet dsNum = this.GetBySql("select MAX(OrderID) from PpmRubberStoreoutDetail where Barcode = '" + Barcode + "'").ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return Convert.ToInt32(dsNum.Tables[0].Rows[0][0].ToString());
            else
                return 0;
        }

        public DataSet GetByBillNo(string BillNo)
        {
            return this.GetBySql(@"select A.BillNo, A.Barcode, A.OrderID, A.MaterCode, D.MaterialName, A.ProcDate, A.OutputNum, A.PieceWeight, A.OutputWeight, A.RecordDate, A.Remark 
                                    from PpmRubberStoreoutDetail A
                                    left join BasMaterial D on A.MaterCode = D.MaterialCode
                                    where BillNo = '" + BillNo + "'").ToDataSet();
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            return this.GetBySql(@"select A.BillNo, A.Barcode, A.OrderID, A.MaterCode, D.MaterialName, A.ProcDate, A.OutputNum, A.PieceWeight, A.OutputWeight, A.RecordDate, A.Remark 
                                    from PpmRubberStoreoutDetail A
                                    left join BasMaterial D on A.MaterCode = D.MaterialCode
                                    where BillNo = '" + BillNo + "' and Barcode = '" + Barcode + "' and OrderID = '" + OrderID + "'").ToDataSet();
        }
    }
}
