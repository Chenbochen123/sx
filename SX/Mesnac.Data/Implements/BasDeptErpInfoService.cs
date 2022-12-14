using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasDeptErpInfoService : BaseService<BasDeptErpInfo>, IBasDeptErpInfoService
    {
		#region 构造方法

        public BasDeptErpInfoService() : base(){ }

        public BasDeptErpInfoService(string connectStringKey) : base(connectStringKey){ }

        public BasDeptErpInfoService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string depName { get; set; }
            public string remark { get; set; }
            public string erpCode { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasDeptErpInfo> pageParams { get; set; }
        }

        public PageResult<BasDeptErpInfo> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasDeptErpInfo> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT     ObjID,DepName,ERPCode,Remark,Deleteflag
                                 FROM       BasDeptErpInfo  
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.depName))
            {
                sqlstr.AppendLine(" AND DepName like '%" + queryParams.depName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.erpCode))
            {
                sqlstr.AppendLine(" AND ERPCode like '%" + queryParams.erpCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND DeleteFlag ='" + queryParams.deleteFlag + "'");
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
