using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasFactoryInfoService : BaseService<BasFactoryInfo>, IBasFactoryInfoService
    {
		#region 构造方法

        public BasFactoryInfoService() : base(){ }

        public BasFactoryInfoService(string connectStringKey) : base(connectStringKey){ }

        public BasFactoryInfoService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string facName { get; set; }
            public string facType { get; set; }
            public string facAddress { get; set; }
            public string corporation { get; set; }
            public string contactMan { get; set; }
            public string dutyMan { get; set; }
            public string email { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasFactoryInfo> pageParams { get; set; }
        }

        public PageResult<BasFactoryInfo> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasFactoryInfo> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT info.ObjID, '' as FacType, info.FacName, 
                                        info.FacSimpleName, info.FacAddress, info.FacPostCode, 
                                        info.Corporation, info.ContactTel, info.ContactMan, 
                                        info.DutyMan, info.Email, info.Remark, info.DeleteFlag, info.DisplayId,
                                        info.HRCode , info.ERPCode
                                 FROM BasFactoryInfo info
                                 WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND info.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.facName))
            {
                sqlstr.AppendLine(" AND (info.facName like '%" + queryParams.facName + "%' or info.FacSimpleName like '%" + queryParams.facName + "%')");
            }
            if (!string.IsNullOrEmpty(queryParams.facType))
            {
                sqlstr.AppendLine(" AND info.FacType = " + queryParams.facType);
            }
            if (!string.IsNullOrEmpty(queryParams.facAddress))
            {
                sqlstr.AppendLine(" AND info.FacAddress like '%" + queryParams.facAddress + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.corporation))
            {
                sqlstr.AppendLine(" AND info.Corporation like '%" + queryParams.corporation + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.contactMan))
            {
                sqlstr.AppendLine(" AND info.ContactMan like '%" + queryParams.contactMan + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.dutyMan))
            {
                sqlstr.AppendLine(" AND info.DutyMan like '%" + queryParams.dutyMan + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.email))
            {
                sqlstr.AppendLine(" AND info.Email like '%" + queryParams.email + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND info.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND info.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        public string GetNextFactoryCode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(ObjID) + 1 as ObjID From BasFactoryInfo ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            return temp.PadLeft(10, '0');
        }
    }
}
