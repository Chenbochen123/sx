using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasMaterialMajorTypeService : BaseService<BasMaterialMajorType>, IBasMaterialMajorTypeService
    {
		#region 构造方法

        public BasMaterialMajorTypeService() : base(){ }

        public BasMaterialMajorTypeService(string connectStringKey) : base(connectStringKey){ }

        public BasMaterialMajorTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string majorTypeName { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasMaterialMajorType> pageParams { get; set; }
        }

        public PageResult<BasMaterialMajorType> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasMaterialMajorType> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    ObjID, MajorTypeName, Remark, DeleteFlag 
                                 FROM	    BasMaterialMajorType
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.majorTypeName))
            {
                sqlstr.AppendLine(" AND MajorTypeName like '%" + queryParams.majorTypeName + "%'");
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

        public string GetNextMaterialMajorTypeCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" SELECT MAX(ObjID) + 1 AS ObjID FROM BasMaterialMajorType ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(10, '0');
        }
    }
}
