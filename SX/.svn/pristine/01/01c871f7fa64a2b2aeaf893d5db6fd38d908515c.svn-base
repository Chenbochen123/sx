using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberReturninService : BaseService<PpmRubberReturnin>, IPpmRubberReturninService
    {
		#region 构造方法

        public PpmRubberReturninService() : base(){ }

        public PpmRubberReturninService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberReturninService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string factoryID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string filedFlag { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<PpmRubberReturnin> pageParams { get; set; }
        }

        public PageResult<PpmRubberReturnin> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberReturnin> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, C.FacName FactoryName, B.UserName, ReturninDate, convert(bit, LockedFlag) LockedFlag, convert(bit, FiledFlag) FiledFlag, ChkPerson, A.Remark 
                                from PpmRubberReturnin A
                                left join BasUser B on A.MakerPerson = B.WorkBarcode
                                left join BasFactoryInfo C on A.FactoryID = C.ObjID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.ReturninDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.ReturninDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.filedFlag))
            {
                sqlstr.AppendLine(" AND A.FiledFlag = '" + queryParams.filedFlag + "'");
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

        public string GetBillNo()
        {
            int rows = this.GetBySql("select BillNo from PpmRubberReturnin where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'TK' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PpmRubberReturnin where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'TK' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string UserID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                //首先修改审批合格的标志位，然后将数据插入到库存明细数据表和主表中
                sql = @"update PpmRubberReturnin set ChkResultFlag = '1', LockedFlag='1', ChkPerson = '" + UserID + "', ChkDate = getdate() where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public bool CancelChkResult(string StrBillNo, string UserID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PpmRubberReturnin set ChkResultFlag = '0', LockedFlag='0', ChkPerson = null, ChkDate = null where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }
}
