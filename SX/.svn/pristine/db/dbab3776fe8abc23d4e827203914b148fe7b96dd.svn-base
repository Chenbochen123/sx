using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class QmcCheckItemService : BaseService<QmcCheckItem>, IQmcCheckItemService
    {
		#region 构造方法

        public QmcCheckItemService() : base(){ }

        public QmcCheckItemService(string connectStringKey) : base(connectStringKey){ }

        public QmcCheckItemService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string standardId { get; set; }
            public string seriesId { get; set; }
            public string valueType { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<QmcCheckItem> pageParams { get; set; }
        }

        public PageResult<QmcCheckItem> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<QmcCheckItem> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT * FROM QmcCheckItem where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.standardId))
            {
                sqlstr.AppendLine(" AND StandardId = '" + queryParams.standardId + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.seriesId))
            {
                sqlstr.AppendLine(" AND SeriesId = '" + queryParams.seriesId + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.valueType))
            {
                sqlstr.AppendLine(" AND ValueType = '" + queryParams.valueType + "'");
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

        public string GetNextItemId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ItemId) + 1 as ItemId From QmcCheckItem ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
