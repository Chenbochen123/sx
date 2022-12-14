using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasUserService : BaseService<BasUser>, IBasUserService
    {
        #region 构造方法

        public BasUserService() : base() { }

        public BasUserService(string connectStringKey) : base(connectStringKey) { }

        public BasUserService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public class QueryParams
        {
            public QueryParams()
            {
                pageParams = new PageResult<BasUser>();
                objID = null;
                userName = null;
                deptCode = null;
                workID = null;
                shiftID = null;
                workshopID = null;
                deleteFlag = null;
                realname = null;
                workbarcode=null;
                hrcode = null;
            }
            public string objID { get; set; }
            public string userName { get; set; }
            public string deptCode { get; set; }
            public string workID { get; set; }
            public string shiftID { get; set; }
            public string workshopID { get; set; }
            public string deleteFlag { get; set; }
            public string realname { get; set; }
            public string workbarcode { get; set; }
            public string hrcode { get; set; }
            public PageResult<BasUser> pageParams { get; set; }
        }

        public PageResult<BasUser> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasUser> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    u.ObjID as ObjID, u.UserName as UserName, u.UserPWD as UserPWD, u.RealName as RealName, 
                                            '' as Sex, u.Telephone as Telephone, u.WorkBarcode as WorkBarcode, dep.DepName as DeptCode, 
                                            work.WorkName as WorkID, shift.ClassName as ShiftID, workshop.WorkShopName as WorkShopID,  
                                            u.Remark , u.HRCode , u.ERPCode , u.DeleteFlag
                                 FROM	    BasUser u 
                                 LEFT JOIN  BasDept dep  on u.DeptCode = dep.DepCode 
                                 LEFT JOIN  BasWork work on u.WorkID = work.ObjID  
                                 LEFT JOIN  PptClass shift on u.ShiftID = shift.ObjID  
                                 LEFT JOIN  BasWorkShop workshop on u.WorkShopID = workshop.ObjID  
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND u.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.realname))
            {
                sqlstr.AppendLine(" AND u.RealName like '%" + queryParams.realname + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.userName))
            {
                sqlstr.AppendLine(" AND u.UserName like '%" + queryParams.userName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.workbarcode))
            {
                sqlstr.AppendLine(" AND u.WorkBarcode like '%" + queryParams.workbarcode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deptCode))
            {
                sqlstr.AppendLine(" AND u.DeptCode = " + queryParams.deptCode);
            }
            if (!string.IsNullOrEmpty(queryParams.workID))
            {
                sqlstr.AppendLine(" AND u.WorkID = " + queryParams.workID);
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND u.ShiftID = " + queryParams.shiftID);
            }
            if (!string.IsNullOrEmpty(queryParams.workshopID))
            {
                sqlstr.AppendLine(" AND u.WorkShopID = " + queryParams.workshopID);
            }
            if (!string.IsNullOrEmpty(queryParams.hrcode))
            {
                sqlstr.AppendLine(" AND u.HRCode = '" + queryParams.hrcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND u.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            sqlstr.AppendLine(" Order by u.WorkBarCode ");
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                //return this.GetPageDataBySql(pageParams);
                //return this.GetPageDataByReader(pageParams);
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
        }

        public string GetNextWorkBarcode()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(" Select MAX(WorkBarcode) + 1 as WorkBarcode From BasUser ");
            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (temp == "")
            {
                temp = "1";
            }
            return temp.PadLeft(6, '0');
        }
    }
}
