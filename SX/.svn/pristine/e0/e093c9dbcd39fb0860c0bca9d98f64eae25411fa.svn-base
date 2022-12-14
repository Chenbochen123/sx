using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberStoreinService : BaseService<PpmRubberStorein>, IPpmRubberStoreinService
    {
		#region 构造方法

        public PpmRubberStoreinService() : base(){ }

        public PpmRubberStoreinService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberStoreinService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string billType { get; set; }
            public string factoryID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string filedFlag { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<PpmRubberStorein> pageParams { get; set; }
        }

        public PageResult<PpmRubberStorein> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberStorein> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BillNo, BillType, C.FacName FactoryName, B.UserName, InputDate, convert(bit, LockedFlag) LockedFlag, convert(bit, FiledFlag) FiledFlag, ChkPerson, A.Remark 
                                from PpmRubberStorein A
                                left join BasUser B on A.MakerPerson = B.WorkBarcode
                                left join BasFactoryInfo C on A.FactoryID = C.ObjID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.billNo))
            {
                sqlstr.AppendLine(" AND A.BillNo like '%" + queryParams.billNo + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.billType))
            {
                sqlstr.AppendLine(" AND A.BillType = '" + queryParams.billType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND A.FactoryID = '" + queryParams.factoryID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InputDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.InputDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
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

        /// <summary>
        /// 生成入库单编号 规则：SJ+年月日+三位随机号   暂定
        /// </summary>
        /// <returns>送检单号</returns>
        public string GetBillNo()
        {
            int rows = this.GetBySql("select BillNo from PpmRubberStorein where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'RK' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PpmRubberStorein where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'RK' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public bool UpdateChkResultFlag(string StrBillNo, string UserID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(StrBillNo))
            {
                //首先修改审批合格的标志位，然后将数据插入到库存明细数据表和主表中
                sql = @"update PpmRubberStorein set ChkResultFlag = '1', LockedFlag='1', ChkPerson = '" + UserID + "', ChkDate = getdate() where BillNo in (" + StrBillNo + ")";
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
                sql = @"update PpmRubberStorein set ChkResultFlag = '0', LockedFlag='0', ChkPerson = '" + UserID + "', ChkDate = null where BillNo in (" + StrBillNo + ")";
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }
}
