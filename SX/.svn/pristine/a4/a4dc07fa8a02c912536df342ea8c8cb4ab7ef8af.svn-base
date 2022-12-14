using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class BasMainHanderService : BaseService<BasMainHander>, IBasMainHanderService
    {
		#region 构造方法

        public BasMainHanderService() : base(){ }

        public BasMainHanderService(string connectStringKey) : base(connectStringKey){ }

        public BasMainHanderService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string mainHanderCode { get; set; }
            public string userName { get; set; }
            public string remark { get; set; }
            public string shiftID { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasMainHander> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<BasMainHander> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasMainHander> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT      a.ObjID, MainHanderCode,b.HRCode As UserCode, 
                                                b.UserName AS UserName, a.Remark, a.DeleteFlag,
                                                c.WorkShopName AS WorkShopCode , d.ClassName AS ClassID
                                    FROM        BasMainHander a 
                                    LEFT JOIN   BasUser b       ON  a.UserCode = b.HRCode
                                    LEFT JOIN   BasWorkShop c   ON  a.WorkShopCode = c.ObjID
                                    LEFT JOIN   PptClass d   ON  a.ClassID = d.ObjID
                                    WHERE 1=1 
                             ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND a.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.mainHanderCode))
            {
                sqlstr.AppendLine(" AND a.MainHanderCode = " + queryParams.mainHanderCode);
            }
            if (!string.IsNullOrEmpty(queryParams.userName))
            {
                sqlstr.AppendLine(" AND b.UserName like '%" + queryParams.userName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.remark))
            {
                sqlstr.AppendLine(" AND a.Remark like '%" + queryParams.remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND a.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        public DataSet IshaveUserInfo(string MainHanderCode, string UserCode, string ObjID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(MainHanderCode))
            {
                sql = "select '1' from BasMainHander where MainHanderCode = '" + MainHanderCode + "'";
                DataSet ds = this.GetBySql(sql).ToDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                    return ds;
            }
            if (string.IsNullOrEmpty(ObjID))
                sql = "select MainHanderCode from BasMainHander where DeleteFlag = '0' and UserCode = '" + UserCode + "'";
            else
                sql = "select MainHanderCode from BasMainHander where ObjID != '" + ObjID + "' and DeleteFlag = '0' and UserCode = '" + UserCode + "'";

            return this.GetBySql(sql).ToDataSet();
        }

        /// <summary>
        /// 获取密炼车间主机手信息(2-5)
        /// </summary>
        /// <returns></returns>
        public DataSet GetMixMainHanderInfo()
        {
            StringBuilder sb = new StringBuilder();
            #region

            sb.AppendLine("SELECT TA.MainHanderCode, TB.UserName, TB.WorkBarcode");
            sb.AppendLine("FROM BasMainHander TA");
            sb.AppendLine("LEFT JOIN BasUser TB ON TA.UserCode = TB.HRCode");
            sb.AppendLine("WHERE TA.WorkShopCode IN ('2', '3', '4', '5')");
            sb.AppendLine("ORDER BY TA.MainHanderCode");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();

        }
    }
}
