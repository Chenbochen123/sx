using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasRubInfoService : BaseService<BasRubInfo>, IBasRubInfoService
    {
		#region 构造方法

        public BasRubInfoService() : base(){ }

        public BasRubInfoService(string connectStringKey) : base(connectStringKey){ }

        public BasRubInfoService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string rubName { get; set; }
            public string rubTypeCode { get; set; }
            public string tyrePartID { get; set; }
            public string rubPurpose { get; set; }
            public string factoryID { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasRubInfo> pageParams { get; set; }
        }

        public PageResult<BasRubInfo> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasRubInfo> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT rub.ObjID, rub.RubCode, rub.RubName, rub.RubOtherName, 
                                        type.RubTypeName as RubTypeCode, rubpurpose as RubPurpose, RubRate , 
                                        '' as FactoryID, rub.Remark, rub.DeleteFlag ,'' AS RubPowerUser
                                 FROM BasRubInfo rub
                                 LEFT JOIN BasRubType type ON rub.RubTypeCode = type.ObjID
                                 WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND rub.RubCode = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.rubName))
            {
                sqlstr.AppendLine(" AND rub.RubName like '%" + queryParams.rubName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.rubTypeCode))
            {
                sqlstr.AppendLine(" AND rub.rubTypeCode = " + queryParams.rubTypeCode);
            }
            if (!string.IsNullOrEmpty(queryParams.tyrePartID))
            {
                sqlstr.AppendLine(" AND rub.TyrePartID = " + queryParams.tyrePartID);
            }
            if (!string.IsNullOrEmpty(queryParams.rubPurpose))
            {
                sqlstr.AppendLine(" AND rub.RubPurpose = " + queryParams.rubPurpose);
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND rub.FactoryID = " + queryParams.factoryID);
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

        public string GetNextRubInfoCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(RubCode) + 1 as RubCode From BasRubInfo ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(4, '0');
        }
    }
}
