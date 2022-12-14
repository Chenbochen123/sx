using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstMaterialInventoryService : BaseService<PstMaterialInventory>, IPstMaterialInventoryService
    {
		#region 构造方法

        public PstMaterialInventoryService() : base(){ }

        public PstMaterialInventoryService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialInventoryService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string inventoryType { get; set; }
            public string storageID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string hrCode { get; set; }
            public PageResult<PstMaterialInventory> pageParams { get; set; }
        }

        public PageResult<PstMaterialInventory> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialInventory> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, A.InventoryType, A.StorageID, C.StorageName, B.UserName, InventoryDate, ChkResultFlag, ChkPerson, A.RecordDate, A.Remark 
                                from PstMaterialInventory A
                                left join BasUser B on A.MakerPerson = B.WorkBarcode
                                left join BasStorage C on A.StorageID = c.StorageID
                                where A.DeleteFlag = '0'");
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
            if (!string.IsNullOrEmpty(queryParams.hrCode))
            {
                sqlstr.AppendLine(" AND B.HRCode = '" + queryParams.hrCode + "'");
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

        public string GetBillNo()
        {
            int rows = this.GetBySql("select BillNo from PstMaterialInventory where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'PD' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PstMaterialInventory where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'PD' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string UserID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PstMaterialInventory set ChkResultFlag = '1', ChkPerson = '" + UserID + "', ChkDate = getdate() where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }
}
