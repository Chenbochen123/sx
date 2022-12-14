using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasMaterialMinorTypeService : BaseService<BasMaterialMinorType>, IBasMaterialMinorTypeService
    {
        #region 构造方法

        public BasMaterialMinorTypeService() : base() { }

        public BasMaterialMinorTypeService(string connectStringKey) : base(connectStringKey) { }

        public BasMaterialMinorTypeService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string minorTypeID { get; set; }
            public string minorTypeName { get; set; }
            public string sectName { get; set; }
            public string majorID { get; set; }
            public string remark { get; set; }
            public double? minStore { get; set; }
            public double? maxStore { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasMaterialMinorType> pageParams { get; set; }
        }

        public PageResult<BasMaterialMinorType> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasMaterialMinorType> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    minor.ObjID, minor.MinorTypeID, minor.MinorTypeName, 
                                            major.MajorTypeName as MajorID, minor.Remark,minor.MinStore,minor.MaxStore,minor.DeleteFlag
                                 FROM	    BasMaterialMinorType minor  
                                 LEFT JOIN  BasMaterialMajorType major on major.ObjID = minor.MajorID 
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND minor.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.minorTypeID))
            {
                sqlstr.AppendLine(" AND minor.MinorTypeID like '%" + queryParams.minorTypeID + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.minorTypeName))
            {
                sqlstr.AppendLine(" AND minor.MinorTypeName like '%" + queryParams.minorTypeName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.majorID))
            {
                sqlstr.AppendLine(" AND minor.MajorID = " + queryParams.majorID);
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND minor.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND minor.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        public PageResult<BasMaterialMinorType> GetQueryRubSectDataPageBySql(QueryParams queryParams)
        {
            PageResult<BasMaterialMinorType> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    Convert(varchar ,major.ObjID) + Convert(varchar ,minor.MinorTypeID) as SectCode,  
                                            major.MajorTypeName + minor.MinorTypeName as SectName, minor.Remark,minor.MinStore,minor.MaxStore, minor.DeleteFlag
                                 FROM	    BasMaterialMinorType minor  
                                 LEFT JOIN  BasMaterialMajorType major on major.ObjID = minor.MajorID 
                                 WHERE      major.ObjID > 2     ");
            if (!string.IsNullOrEmpty(queryParams.sectName))
            {
                sqlstr.AppendLine(" AND (major.MajorTypeName + minor.MinorTypeName) like '%" + queryParams.sectName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND minor.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND minor.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        public string GetNextMaterialMinorTypeCode(string majorid)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(MinorTypeID) + 1 as MinorTypeID From BasMaterialMinorType WHERE MajorID = '" + majorid + "'");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(2, '0');
        }

        public string GetNextMaterialMinorObjIDCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ObjID) + 1 as ObjID From BasMaterialMinorType ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp;
        }
    }
}
