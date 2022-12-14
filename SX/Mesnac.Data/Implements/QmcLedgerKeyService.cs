using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class QmcLedgerKeyService : BaseService<QmcLedgerKey>, IQmcLedgerKeyService
    {
		#region 构造方法

        public QmcLedgerKeyService() : base(){ }

        public QmcLedgerKeyService(string connectStringKey) : base(connectStringKey){ }

        public QmcLedgerKeyService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string valueType { get; set; }
            public string hasSelection { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<QmcLedgerKey> pageParams { get; set; }
        }

        public PageResult<QmcLedgerKey> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<QmcLedgerKey> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT * FROM QmcLedgerKey where 1 = 1");
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

        public string GetNextKeyId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(KeyId) + 1 as KeyId From QmcLedgerKey ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
