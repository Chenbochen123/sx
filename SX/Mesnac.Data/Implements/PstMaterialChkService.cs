using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class PstMaterialChkService : BaseService<PstMaterialChk>, IPstMaterialChkService
    {
		#region 构造方法

        public PstMaterialChkService() : base(){ }

        public PstMaterialChkService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialChkService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string noticeNo { get; set; }
            public string factoryID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string sendChkFlag { get; set; }
            public string chkResultFlag { get; set; }
            public string NoPassFlag { get; set; }
            public string stockInFlag { get; set; }
            public string hrCode { get; set; }
            public string filedFlag { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<PstMaterialChk> pageParams { get; set; }
        }

        public PageResult<PstMaterialChk> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterialChk> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select distinct A.BillNo, A.NoticeNo, A.FactoryID, B.FacName, convert(bit, A.SendChkFlag) SendChkFlag, A.MakerPerson, C.UserName, convert(bit, A.FiledFlag) FiledFlag, A.Remark 
                                from PstMaterialChk A
                                left join BasFactoryInfo B on A.FactoryID = B.ObjID
                                left join BasUser C on A.MakerPerson = C.WorkBarcode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.noticeNo))
            {
                sqlstr.AppendLine(" AND A.NoticeNo like '%" + queryParams.noticeNo + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InStockDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InStockDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.sendChkFlag))
            {
                sqlstr.AppendLine(" AND A.SendChkFlag = '" + queryParams.sendChkFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.stockInFlag))
            {
                sqlstr.AppendLine(" AND A.StockInFlag = '" + queryParams.stockInFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.filedFlag))
            {
                sqlstr.AppendLine(" AND A.FiledFlag = '" + queryParams.filedFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.chkResultFlag))
            {
                sqlstr.AppendLine(" AND D.ChkResultFlag = '" + queryParams.chkResultFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.hrCode))
            {
                sqlstr.AppendLine(" AND C.HRCode = '" + queryParams.hrCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.NoPassFlag))
            {
                sqlstr.AppendLine(" AND D.SendNum - isnull(D.PassNum, 0) > 0");
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

        /// <summary>
        /// 生成送检单编号 规则：SJ+年月日+三位随机号   暂定
        /// </summary>
        /// <returns>送检单号</returns>
        public string GetBillNo()
        {
            int rows = this.GetBySql("select BillNo from PstMaterialChk where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'SJ' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PstMaterialChk where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'SJ' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateSendChkFlag(string StrBillNo)
        {
            //using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required))
            //{
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PstMaterialChk set SendChkFlag = '1' where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            //    scope.Complete();
            //    scope.Dispose();
            //}
            return false;
        }

        public bool CancelSendChk(string StrBillNo)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                sql = @"update PstMaterialChk set SendChkFlag = '0' where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public bool UpdateStockInFlag(string BillNo)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(BillNo))
            {
                sql = @"update PstMaterialChk set StockInFlag = '1' where BillNo = '" + BillNo + "'";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public string GetFactoryID(string BillNo)
        {
            string sql = "select * from PstMaterialChk where BillNo = '" + BillNo + "'";
            DataSet ds = this.GetBySql(sql).ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["FactoryID"].ToString();
            else
                return "";
        }
    }
}
