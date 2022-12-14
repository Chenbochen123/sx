using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    using System.Data;
    public class QmcStandardService : BaseService<QmcStandard>, IQmcStandardService
    {
		#region 构造方法

        public QmcStandardService() : base(){ }

        public QmcStandardService(string connectStringKey) : base(connectStringKey){ }

        public QmcStandardService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string activateFlag { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<QmcStandard> pageParams { get; set; }
        }

        public PageResult<QmcStandard> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<QmcStandard> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.StandardId, A.StandardName, 
                                  A.CreatorId,  B.Username, A.ActivateDate,A.Remark,
                                  case A.ActivateFlag when '1' then '启用' when '0' then '未启用' when '2' then '过期' end as ActivateFlag 
                                  from QmcStandard A inner join BasUser B on A.CreatorId = B.WorkBarCode where");
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" A.DeleteFlag = '" + queryParams.deleteFlag + "'");
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

        public DataSet GetStandardList()
        {
            string sql = @"select A.StandardId, A.StandardName, 
                                  A.CreatorId,  B.Username, A.ActivateDate,A.Remark,
                                  case A.ActivateFlag when '1' then '当前' when '0' then '未启用' end as ActivateFlag 
                                  from QmcStandard A inner join BasUser B on A.CreatorId = B.WorkBarCode where A.DeleteFlag = '0'";
            return this.GetBySql(sql).ToDataSet();
        }

        public string GetNextStandardId()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(StandardId) + 1 as StandardId From QmcStandard ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
