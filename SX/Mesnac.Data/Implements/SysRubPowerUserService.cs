using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class SysRubPowerUserService : BaseService<SysRubPowerUser>, ISysRubPowerUserService
    {
		#region 构造方法

        public SysRubPowerUserService() : base(){ }

        public SysRubPowerUserService(string connectStringKey) : base(connectStringKey){ }

        public SysRubPowerUserService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string rubCode { get; set; }
            public string rubType { get; set; }
            public string workBarCode { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<SysRubPowerUser> pageParams { get; set; }
        }

        public PageResult<SysRubPowerUser> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<SysRubPowerUser> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT rub.RubCode , u.UserName , u.WorkBarcode , u.HRCode
                                 FROM SysRubPowerUser rub
                                 LEFT JOIN BasUser u ON rub.WorkBarcode = u.WorkBarcode
                                 WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND rub.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.rubCode))
            {
                sqlstr.AppendLine(" AND rub.RubCode like '%" + queryParams.rubCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.rubType))
            {
                sqlstr.AppendLine(" AND rub.RubType = " + queryParams.rubType);
            }
            if (!string.IsNullOrEmpty(queryParams.workBarCode))
            {
                sqlstr.AppendLine(" AND rub.WorkBarCode = " + queryParams.workBarCode);
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND rub.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND rub.DeleteFlag ='" + queryParams.deleteFlag + "'");
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
