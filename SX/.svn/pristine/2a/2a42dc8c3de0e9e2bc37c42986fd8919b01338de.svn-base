using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    using NBear.Data;
    public class PptMonthlyEnteringService : BaseService<PptMonthlyEntering>, IPptMonthlyEnteringService
    {
		#region 构造方法

        public PptMonthlyEnteringService() : base(){ }

        public PptMonthlyEnteringService(string connectStringKey) : base(connectStringKey){ }

        public PptMonthlyEnteringService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public PageResult<PptMonthlyEntering> PageParams { get; set; }
            public string MaterialCode { get; set; }
            public string MaterialName { get; set; }
            public string YearMonth { get; set; }
            public int TypeID { get; set; }
        }

        #region IPptMonthlyEnteringService 成员

        public PageResult<PptMonthlyEntering> GetPptMonthlyEnteringPageDataBySql(PptMonthlyEnteringService.QueryParams queryParams)
        {
            PageResult<PptMonthlyEntering> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendFormat(@"EXEC dbo.ProcGetRubMonthPlan @YearMonth = '{0}',
    @RubCode = '{1}',
    @TypeID = '{2}' ", queryParams.YearMonth, queryParams.MaterialCode, queryParams.TypeID);
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                pageParams.QueryStr = sqlstr.ToString();
                return this.GetPageDataByReader(pageParams);
            }
        }

        #endregion
    }
}
