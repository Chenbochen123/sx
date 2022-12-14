using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SysProblemRecordService : BaseService<SysProblemRecord>, ISysProblemRecordService
    {
		#region 构造方法

        public SysProblemRecordService() : base(){ }

        public SysProblemRecordService(string connectStringKey) : base(connectStringKey){ }

        public SysProblemRecordService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public QueryParams()
            {
                pageParams = new PageResult<SysProblemRecord>();
                createDept = null;
                createUser = null;
                dealDept = null;
                dealUser = null;
                validateDept = null;
                validateUser = null;
                startCreateDate = DateTime.MinValue;
                endCreateDate = DateTime.MinValue;
                startDealDate = DateTime.MinValue;
                endDealDate = DateTime.MinValue;
                startValidateDate = DateTime.MinValue;
                endValidateDate = DateTime.MinValue;
                deleteFlag = null;
            }
            public string createDept { get; set; }
            public string createUser { get; set; }
            public string dealDept { get; set; }
            public string dealUser { get; set; }
            public string validateDept { get; set; }
            public string validateUser { get; set; }
            public DateTime startCreateDate { get; set; }
            public DateTime endCreateDate { get; set; }
            public DateTime startDealDate { get; set; }
            public DateTime endDealDate { get; set; }
            public DateTime startValidateDate { get; set; }
            public DateTime endValidateDate { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<SysProblemRecord> pageParams { get; set; }
        }

        public PageResult<SysProblemRecord> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<SysProblemRecord> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    problem.ObjID, CreateDate, dep1.ItemName AS CreateDept, u1.UserName AS CreateUser, ProblemDesc, 
                                            proType.ItemName AS ProblemType,level.ItemName AS ErrorLevel, ProblemReason, Solution, DealResult, 
                                            dep2.ItemName AS DealDept,u2.UserName AS DealUser, DealDate,u3.UserName AS ValidateUser,
                                            dep3.ItemName AS ValidateDept,ValidateDate, problem.DeleteFlag, problem.Remark
                                 FROM	    SysProblemRecord problem 
                                 LEFT JOIN  BasUser u1  on u1.WorkBarcode = problem.CreateUser 
                                 LEFT JOIN  BasUser u2  on u2.WorkBarcode = problem.DealUser  
                                 LEFT JOIN  BasUser u3  on u3.WorkBarcode = problem.ValidateUser  
                                 LEFT JOIN  SysCode dep1 on dep1.ItemCode = problem.CreateDept    and dep1.TypeID = 'ProblemDept'  
                                 LEFT JOIN  SysCode dep2 on dep2.ItemCode = problem.DealDept      and dep2.TypeID = 'ProblemDept'
                                 LEFT JOIN  SysCode dep3 on dep3.ItemCode = problem.ValidateDept  and dep3.TypeID = 'ProblemDept'  
                                 LEFT JOIN  SysCode level on level.ItemCode = problem.ErrorLevel  and level.TypeID = 'ProblemLevel'  
                                 LEFT JOIN  SysCode proType on proType.ItemCode = problem.ProblemType  and proType.TypeID = 'ProblemType'  
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.createUser))
            {
                sqlstr.AppendLine(" AND problem.CreateUser = '" + queryParams.createUser + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.dealUser))
            {
                sqlstr.AppendLine(" AND problem.DealUser = '" + queryParams.dealUser + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.validateUser))
            {
                sqlstr.AppendLine(" AND problem.ValidateUser = '" + queryParams.validateUser + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.createDept))
            {
                sqlstr.AppendLine(" AND problem.CreateDept = '" + queryParams.createDept + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.dealDept))
            {
                sqlstr.AppendLine(" AND problem.DealDept = '" + queryParams.dealDept + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.validateDept))
            {
                sqlstr.AppendLine(" AND problem.ValidateDept = '" + queryParams.validateDept + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND problem.DeleteFlag = '" + queryParams.deleteFlag + "'");
            }
            if (queryParams.startCreateDate != DateTime.MinValue)
            {
                sqlstr.AppendLine("AND problem.CreateDate  >='" + queryParams.startCreateDate.ToString() + "'");
            }
            if (queryParams.endCreateDate != DateTime.MinValue)
            {
                sqlstr.AppendLine("AND problem.CreateDate  <='" + queryParams.startCreateDate.AddDays(1).ToString() + "'");
            }
            if (queryParams.startDealDate != DateTime.MinValue)
            {
                sqlstr.AppendLine("AND problem.DealDate  >='" + queryParams.startDealDate.ToString() + "'");
            }
            if (queryParams.endDealDate != DateTime.MinValue)
            {
                sqlstr.AppendLine("AND problem.DealDate  <='" + queryParams.endDealDate.AddDays(1).ToString() + "'");
            }
            if (queryParams.startValidateDate != DateTime.MinValue)
            {
                sqlstr.AppendLine("AND problem.ValidateDate  >='" + queryParams.startValidateDate.ToString() + "'");
            }
            if (queryParams.endValidateDate != DateTime.MinValue)
            {
                sqlstr.AppendLine("AND problem.ValidateDate  <='" + queryParams.endValidateDate.AddDays(1).ToString() + "'");
            }
            //try
            //{
            //    if (!string.IsNullOrEmpty(queryParams.startCreateDate) && !"0001-01-01 0:00:00".Equals(queryParams.startCreateDate))
            //    {
            //        sqlstr.AppendLine("AND problem.CreateDate  >='" + Convert.ToDateTime(queryParams.startCreateDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //    }
            //}
            //catch { }
            //try
            //{
            //    if (!string.IsNullOrEmpty(queryParams.endCreateDate) && !"0001-01-01 0:00:00".Equals(queryParams.endCreateDate))
            //    {
            //        sqlstr.AppendLine("AND problem.CreateDate  <='" + Convert.ToDateTime(queryParams.endCreateDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //    }
            //}
            //catch { }
            //try
            //{
            //    if (!string.IsNullOrEmpty(queryParams.startDealDate) && !"0001-01-01 0:00:00".Equals(queryParams.startDealDate))
            //    {
            //        sqlstr.AppendLine("AND problem.DealDate  >='" + Convert.ToDateTime(queryParams.startDealDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //    }
            //}
            //catch { }
            //try
            //{
            //    if (!string.IsNullOrEmpty(queryParams.endDealDate) && !"0001-01-01 0:00:00".Equals(queryParams.endDealDate))
            //    {
            //        sqlstr.AppendLine("AND problem.DealDate  <='" + Convert.ToDateTime(queryParams.endDealDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //    }
            //}
            //catch { }
            //try
            //{
            //    if (!string.IsNullOrEmpty(queryParams.startValidateDate) && !"0001-01-01 0:00:00".Equals(queryParams.startValidateDate))
            //    {
            //        sqlstr.AppendLine("AND problem.ValidateDate  >='" + Convert.ToDateTime(queryParams.startValidateDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //    }
            //}
            //catch { }
            //try
            //{
            //    if (!string.IsNullOrEmpty(queryParams.endValidateDate) && !"0001-01-01 0:00:00".Equals(queryParams.endValidateDate))
            //    {
            //        sqlstr.AppendLine("AND problem.ValidateDate  <='" + Convert.ToDateTime(queryParams.endValidateDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            //    }
            //}
            //catch { }
            sqlstr.AppendLine(" Order by problem.ObjID ");
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataByReader(pageParams);
            }
        }
    }
}
