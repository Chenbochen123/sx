using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class QmcSpecService : BaseService<QmcSpec>, IQmcSpecService
    {
		#region 构造方法

        public QmcSpecService() : base(){ }

        public QmcSpecService(string connectStringKey) : base(connectStringKey){ }

        public QmcSpecService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string seriesId { get; set; }
            public string specName { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<QmcSpec> pageParams { get; set; }
        }

        public PageResult<QmcSpec> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<QmcSpec> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT * FROM QmcSpec where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.seriesId))
            {
                sqlstr.AppendLine(" AND SeriesId = '" + queryParams.seriesId + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.specName))
            {
                sqlstr.AppendLine(" AND SpecName LIKE '%" + queryParams.specName + "%'");
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

        public string GetNextSpecId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(SpecId) + 1 as SpecId From QmcSpec ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
