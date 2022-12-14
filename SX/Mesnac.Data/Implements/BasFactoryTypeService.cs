using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasFactoryTypeService : BaseService<BasFactoryType>, IBasFactoryTypeService
    {
		#region 构造方法

        public BasFactoryTypeService() : base(){ }

        public BasFactoryTypeService(string connectStringKey) : base(connectStringKey){ }

        public BasFactoryTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string factoryTypeName { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasFactoryType> pageParams { get; set; }
        }

        public PageResult<BasFactoryType> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasFactoryType> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT ObjID, FactoryTypeName, Remark, DeleteFlag
                                 FROM BasFactoryType 
                                 WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.factoryTypeName))
            {
                sqlstr.AppendLine(" AND FactoryTypeName like '%" + queryParams.factoryTypeName + "%'");
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

        public string GetNextFactoryTypeCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ObjID) + 1 as ObjID From BasFactoryType ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            return temp.PadLeft(10, '0');
        }
    }
}
