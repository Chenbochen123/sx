using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberInventoryService : BaseService<PpmRubberInventory>, IPpmRubberInventoryService
    {
		#region 构造方法

        public PpmRubberInventoryService() : base(){ }

        public PpmRubberInventoryService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberInventoryService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string inventoryType { get; set; }
            public string storageID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public PageResult<PpmRubberInventory> pageParams { get; set; }
        }

        public PageResult<PpmRubberInventory> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberInventory> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, A.InventoryType, A.StorageID, C.StorageName, B.UserName, InventoryDate, ChkResultFlag, ChkPerson, A.RecordDate, A.Remark 
                                from PpmRubberInventory A
                                left join BasUser B on A.MakerPerson = B.WorkBarcode
                                left join BasStorage C on A.StorageID = c.StorageID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.inventoryType))
            {
                sqlstr.AppendLine(" AND A.InventoryType = '" + queryParams.inventoryType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InventoryDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InventoryDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
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

        public string GetBillNo()
        {
            int rows = this.GetBySql("select BillNo from PpmRubberInventory where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'PD' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PpmRubberInventory where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'PD' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string UserID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PpmRubberInventory set ChkResultFlag = '1', ChkPerson = '" + UserID + "', ChkDate = getdate() where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }
}
