using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberReturnDetailService : BaseService<PpmRubberReturnDetail>, IPpmRubberReturnDetailService
    {
		#region 构造方法

        public PpmRubberReturnDetailService() : base(){ }

        public PpmRubberReturnDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberReturnDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string factoryID { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<PpmRubberReturnDetail> pageParams { get; set; }
        }

        public PageResult<PpmRubberReturnDetail> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberReturnDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.Barcode, A.OrderID, A.ProductNo, A.MaterCode, B.MaterialName, 
                                    A.ProcDate, A.StoragePlaceID, D.StoragePlaceName,
                                    A.ReturnNum, A.PieceWeight, A.ReturnWeight, A.RecordDate, A.Remark
                                from PpmRubberReturnDetail A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasStoragePlace D on A.StoragePlaceID = D.StoragePlaceID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
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
            string sql = @"select A.BillNo, A.Barcode, A.OrderID, A.ProductNo, A.MaterCode, B.MaterialName, A.ProcDate, 
	                            A.StoragePlaceID, C.StoragePlaceName, A.ReturnNum, A.PieceWeight, A.ReturnWeight, A.Remark
                            from PpmRubberReturnDetail A
                            left join BasMaterial B on A.MaterCode = B.MaterialCode
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            where A.BillNo = '" + BillNo + "' and A.DeleteFlag = '0'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetByOtherBillNo(string BillNo, string Barcode, string OrderID)
        {
            string sql = @"select A.BillNo, A.Barcode, A.OrderID, A.ProductNo, A.MaterCode, B.MaterialName, A.ProcDate, 
	                            A.StoragePlaceID, C.StoragePlaceName, A.ReturnNum, A.PieceWeight, A.ReturnWeight, A.Remark
                            from PpmRubberReturnDetail A
                            left join BasMaterial B on A.MaterCode = B.MaterialCode
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            where A.DeleteFlag = '0' and A.BillNo = '" + BillNo + "' and A.Barcode = '" + Barcode + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
