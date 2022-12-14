using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PmtNonAuditMaterialService : BaseService<PmtNonAuditMaterial>, IPmtNonAuditMaterialService
    {
		#region 构造方法

        public PmtNonAuditMaterialService() : base(){ }

        public PmtNonAuditMaterialService(string connectStringKey) : base(connectStringKey){ }

        public PmtNonAuditMaterialService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string materialCode { get; set; }
            public string deleteFlag { get; set; }
            public string remark { get; set; }
            public PageResult<PmtNonAuditMaterial> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PmtNonAuditMaterial> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtNonAuditMaterial> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      non.ObjId AS ObjID, mater.MaterialName AS MaterialName , non.MaterialCode AS MaterialCode , non.DeleteFlag, non.Remark
                                    FROM        PmtNonAuditMaterial non 
                                    LEFT JOIN   BasMaterial mater   ON  mater.MaterialCode = non.MaterialCode
                                    WHERE       1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND non.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.materialCode))
            {
                sqlstr.AppendLine(" AND non.MaterialCode = " + queryParams.materialCode);
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND non.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND non.DeleteFlag ='" + queryParams.deleteFlag + "'");
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
