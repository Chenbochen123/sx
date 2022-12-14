using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberAdjustingService : BaseService<PpmRubberAdjusting>, IPpmRubberAdjustingService
    {
		#region 构造方法

        public PpmRubberAdjustingService() : base(){ }

        public PpmRubberAdjustingService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberAdjustingService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string storageID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string chkResultFlag { get; set; }
            public PageResult<PpmRubberAdjusting> pageParams { get; set; }
        }

        public PageResult<PpmRubberAdjusting> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberAdjusting> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.InventoryNo, A.StorageID, B.StorageName, A.AdjustingDate, A.InaccountDuration,
	                                A.MakerPerson, C.UserName, A.ChkResultFlag, A.DeleteFlag, A.Remark
                                from PpmRubberAdjusting A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasUser C on A.MakerPerson = C.WorkBarcode
                                where A.DeleteFlag = '0'");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo = '" + queryParams.billNo + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.AdjustingDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.AdjustingDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
            {
                sqlstr.AppendLine(" AND A.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
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
            int rows = this.GetBySql("select BillNo from PpmRubberAdjusting where DeleteFlag = '0' and SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'TZ' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PpmRubberAdjusting where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'TZ' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string ChkPerson)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PpmRubberAdjusting set ChkPerson = '" + ChkPerson + "', ChkResultFlag = '1', ChkDate = GETDATE() where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }
}
