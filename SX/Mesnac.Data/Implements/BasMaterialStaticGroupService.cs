using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class BasMaterialStaticGroupService : BaseService<BasMaterialStaticGroup>, IBasMaterialStaticGroupService
    {
		#region 构造方法

        public BasMaterialStaticGroupService() : base(){ }

        public BasMaterialStaticGroupService(string connectStringKey) : base(connectStringKey){ }

        public BasMaterialStaticGroupService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string materialCode { get; set; }
            public string materialName { get; set; }
            public string majorTypeID { get; set; }
            public string minorTypeID { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasMaterialStaticGroup> pageParams { get; set; }
        }

        public PageResult<BasMaterialStaticGroup> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasMaterialStaticGroup> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.ObjID, A.MajorTypeID, B.MajorTypeName, A.MinorTypeID, C.MinorTypeName, A.MaterialCode, A.MaterialName, A.MaterialGroup, D.MaterialName MaterialGroupName
                                from BasMaterial A
	                                left join BasMaterialMajorType B on A.MajorTypeID = B.ObjID
	                                left join BasMaterialMinorType C on A.MinorTypeID = C.MinorTypeID and A.MajorTypeID = C.MajorID
	                                left join BasMaterialStaticGroup D on A.MaterialGroup = D.MaterialCode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.materialCode))
            {
                sqlstr.AppendLine(" AND A.MaterialCode like '%" + queryParams.materialCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.materialName))
            {
                sqlstr.AppendLine(" AND A.MaterialName like '%" + queryParams.materialName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.majorTypeID))
            {
                sqlstr.AppendLine(" AND A.MajorTypeID = '" + queryParams.majorTypeID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.minorTypeID))
            {
                sqlstr.AppendLine(" AND A.MinorTypeID = " + queryParams.minorTypeID);
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND A.DeleteFlag ='" + queryParams.deleteFlag + "'");
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
