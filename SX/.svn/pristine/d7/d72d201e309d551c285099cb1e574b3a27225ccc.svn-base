using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasStorageFlowService : BaseService<BasStorageFlow>, IBasStorageFlowService
    {
		#region 构造方法

        public BasStorageFlowService() : base(){ }

        public BasStorageFlowService(string connectStringKey) : base(connectStringKey){ }

        public BasStorageFlowService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string flowID { get; set; }
            public string sourceStorage { get; set; }
            public string targetStorage { get; set; }
            public string forbiddenFlag { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasStorageFlow> pageParams { get; set; }
        }

        public PageResult<BasStorageFlow> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasStorageFlow> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT ObjID, FlowID, SourceStorage, TargetStorage, LimitTimes, ForbiddenFlag, Remark 
                                FROM BasStorageFlow WHERE 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.flowID))
            {
                sqlstr.AppendLine(" AND FlowID = " + queryParams.flowID);
            }
            if (!string.IsNullOrEmpty(queryParams.sourceStorage))
            {
                sqlstr.AppendLine(" AND SourceStorage LIKE '%" + queryParams.sourceStorage + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.targetStorage))
            {
                sqlstr.AppendLine(" AND TargetStorage LIKE '%" + queryParams.targetStorage + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.forbiddenFlag))
            {
                sqlstr.AppendLine(" AND ForbiddenFlag = '" + queryParams.forbiddenFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND DeleteFlag = '" + queryParams.deleteFlag + "'");
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
    }
}
