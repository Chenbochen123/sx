using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasRubTyrePartService : BaseService<BasRubTyrePart>, IBasRubTyrePartService
    {
		#region 构造方法

        public BasRubTyrePartService() : base(){ }

        public BasRubTyrePartService(string connectStringKey) : base(connectStringKey){ }

        public BasRubTyrePartService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string tyrePartName { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasRubTyrePart> pageParams { get; set; }
        }

        public PageResult<BasRubTyrePart> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasRubTyrePart> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    ObjID, TyrePartName, Remark, DeleteFlag
                                 FROM	    BasRubTyrePart 
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.tyrePartName))
            {
                sqlstr.AppendLine(" AND TyrePartName like '%" + queryParams.tyrePartName + "%'");
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

        public string GetNextTyrePartCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ObjID) + 1 as  ObjID From BasRubTyrePart");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(3, '0');
        }
    }
}
