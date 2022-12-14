using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class QmcPropertyService : BaseService<QmcProperty>, IQmcPropertyService
    {
		#region 构造方法

        public QmcPropertyService() : base(){ }

        public QmcPropertyService(string connectStringKey) : base(connectStringKey){ }

        public QmcPropertyService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string seriesId { get; set; }
            public string valueType { get; set; }
            public string hasSelection { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<QmcProperty> pageParams { get; set; }
        }

        public PageResult<QmcProperty> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<QmcProperty> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT * FROM QmcProperty where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.seriesId))
            {
                sqlstr.AppendLine(" AND SeriesId = '" + queryParams.seriesId + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.valueType))
            {
                sqlstr.AppendLine(" AND ValueType = '" + queryParams.valueType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.hasSelection))
            {
                sqlstr.AppendLine(" AND HasSelection = '" + queryParams.hasSelection + "'");
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

        public string GetNextPropertyId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(PropertyId) + 1 as PropertyId From QmcProperty ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
