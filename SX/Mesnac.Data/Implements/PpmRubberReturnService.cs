using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberReturnService : BaseService<PpmRubberReturn>, IPpmRubberReturnService
    {
		#region 构造方法

        public PpmRubberReturnService() : base(){ }

        public PpmRubberReturnService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberReturnService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string billNo { get; set; }
            public string factoryID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<PpmRubberReturn> pageParams { get; set; }
        }

        public PageResult<PpmRubberReturn> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberReturn> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.AppendLine(@"select A.BillNo, A.ReturnDate, A.ReturnReason, A.ReturnFactory, B.FacName, C.UserName, 
	                                A.StorageID, D.StorageName,	A.InaccountDuration, A.ChkResultFlag, A.ChkDate, A.Remark
                                from PpmRubberReturn A
                                left join BasFactoryInfo B on A.ReturnFactory = B.ObjID
                                left join BasUser C on A.MakerPerson = C.WorkBarcode
                                left join BasStorage D on A.StorageID = D.StorageID
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
                sqlstr.AppendLine(" AND A.ReturnDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.ReturnDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
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
            int rows = this.GetBySql("select BillNo from PpmRubberReturn where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows.Count;
            if (rows > 0)
                return this.GetBySql("select 'TH' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + RIGHT('000' + CONVERT(VARCHAR, MAX(CONVERT(int, RIGHT(BillNo, 3))) + 1), 3) from PpmRubberReturn where SUBSTRING(BillNo, 3, 6) = SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6)").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return this.GetBySql("select 'TH' + SUBSTRING(CONVERT(varchar(10),GETDATE(), 112), 3, 6) + '001'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }
    }
}
