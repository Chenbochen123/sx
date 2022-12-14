using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasDeptService : BaseService<BasDept>, IBasDeptService
    {
		#region 构造方法

        public BasDeptService() : base(){ }

        public BasDeptService(string connectStringKey) : base(connectStringKey){ }

        public BasDeptService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string depCode { get; set; }
            public string depName { get; set; }
            public string remark { get; set; }
            public string parentNum { get; set; }
            public string erpCode { get; set; }
            public string depLevel { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasDept> pageParams { get; set; }
        }

        public PageResult<BasDept> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasDept> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    dep1.ObjID as ObjID , dep1.DepCode as DepCode , dep1.DepName as DepName, '' as DepLevel, 
                                            dep2.DepName as ParentNum ,dep1.ERPCode as ERPCode, dep1.HRCode as HRCode, dep1.DisplayId as DisplayId,
                                            dep1.Remark as Remark, dep1.DeleteFlag as DeleteFlag 
                                 FROM	    basdept dep1  
                                 LEFT JOIN  basdept dep2 on dep1.ParentNum = dep2.DepCode  
                                 WHERE      1 = 1");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND dep1.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.depCode))
            {
                sqlstr.AppendLine(" AND dep1.DepCode like '%" + queryParams.depCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.depName))
            {
                sqlstr.AppendLine(" AND dep1.DepName like '%" + queryParams.depName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.depLevel))
            {
                sqlstr.AppendLine(" AND dep1.DepLevel = " + queryParams.depLevel);
            }

            //if (!string.IsNullOrEmpty(queryParams.parentNum.Trim()))
            //{
            //    sqlstr.AppendLine(" AND dep1.ParentNum = " + queryParams.parentNum);
            //}
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND dep1.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.erpCode))
            {
                sqlstr.AppendLine(" AND dep1.ERPCode like '%" + queryParams.erpCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND dep1.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND dep1.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        public string GetNextDepCode() {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(DepCode) + 1 as DepCode From BasDept ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString(); 
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(5 , '0');
        }
    }
}
