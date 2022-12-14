using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SysMesActionService : BaseService<SysMesAction>, ISysMesActionService
    {
		#region 构造方法

        public SysMesActionService() : base(){ }

        public SysMesActionService(string connectStringKey) : base(connectStringKey){ }

        public SysMesActionService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string workName { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<SysMesAction> pageParams { get; set; }
        }

        public PageResult<SysMesAction> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<SysMesAction> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT * 
                                 FROM SysMesAction WHERE 1=1 ");
        
            pageParams.QueryStr = sqlstr.ToString();
            //if (pageParams.PageSize < 0)
            //{
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            //}
            //else
            //{
            //    return this.GetPageDataBySql(pageParams);
            //}
        }
    }
}
