using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasEquipTypeService : BaseService<BasEquipType>, IBasEquipTypeService
    {
		#region 构造方法

        public BasEquipTypeService() : base(){ }

        public BasEquipTypeService(string connectStringKey) : base(connectStringKey){ }

        public BasEquipTypeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string equipTypeName { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasEquipType> pageParams { get; set; }
        }

        public PageResult<BasEquipType> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasEquipType> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    ObjID, EquipTypeName, Remark, DeleteFlag
                                 FROM	    BasEquipType
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.equipTypeName))
            {
                sqlstr.AppendLine(" AND EquipTypeName    like '%" + queryParams.equipTypeName + "%'");
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

        public string GetNextEquipTypeCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" SELECT MAX(ObjID) + 1 AS ObjID FROM BasEquipType ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(2, '0');
        }
    }
}
