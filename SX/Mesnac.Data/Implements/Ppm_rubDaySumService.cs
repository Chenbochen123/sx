using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using System.Data;
    public class Ppm_rubDaySumService : BaseService<Ppm_rubDaySum>, IPpm_rubDaySumService
    {
		#region 构造方法

        public Ppm_rubDaySumService() : base(){ }

        public Ppm_rubDaySumService(string connectStringKey) : base(connectStringKey){ }

        public Ppm_rubDaySumService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string plan_date { get; set; }
            public PageResult<Ppm_rubDaySum> pageParams { get; set; }
        }

        public DataTable GetTableStoreDaySum(QueryParams queryParams)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select * From ppm_rubdaysum
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.plan_date))
            {
                sqlstr.AppendLine(" AND plan_date = '" + queryParams.plan_date + "'");
            }

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet().Tables[0];

        }
    }
}
