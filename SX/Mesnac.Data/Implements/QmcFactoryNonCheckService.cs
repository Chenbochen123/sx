using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class QmcFactoryNonCheckService : BaseService<QmcFactoryNonCheck>, IQmcFactoryNonCheckService
    {
		#region 构造方法

        public QmcFactoryNonCheckService() : base(){ }

        public QmcFactoryNonCheckService(string connectStringKey) : base(connectStringKey){ }

        public QmcFactoryNonCheckService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string factoryCode { get; set; }
            public string materialCode { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<QmcFactoryNonCheck> pageParams { get; set; }
        }

        public PageResult<QmcFactoryNonCheck> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<QmcFactoryNonCheck> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT 
                                t1.ObjID , t2.FacName AS FactoryCode , t3.MaterialName AS MaterialCode , 
                                t1.NonCheckNum , t1.DeleteFlag , t1.Remark,TotalNum,NonCheckWeight,TotalWeight ,t1.MSetNum,t1.NSetNum 
                                FROM QmcFactoryNonCheck t1
                                LEFT JOIN BasFactoryInfo t2 ON t1.FactoryCode  = t2.ObjID
                                LEFT JOIN BasMaterial   t3  ON t1.MaterialCode = t3.MaterialCode
                                WHERE 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.factoryCode))
            {
                sqlstr.AppendFormat(" AND t1.factoryCode  = '{0}'", queryParams.factoryCode);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.materialCode))
            {
                sqlstr.AppendFormat(" AND t1.materialCode = '{0}'", queryParams.materialCode);
                sqlstr.AppendLine();
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND t1.DeleteFlag = '" + queryParams.deleteFlag + "'");
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
