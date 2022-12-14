using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class EqmSapSparePartService : BaseService<EqmSapSparePart>, IEqmSapSparePartService
    {
		#region 构造方法

        public EqmSapSparePartService() : base(){ }

        public EqmSapSparePartService(string connectStringKey) : base(connectStringKey){ }

        public EqmSapSparePartService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string beginReceiveDate { get; set; }
            public string endReceiveDate { get; set; }
            public string sparepartCode { get; set; }
            public string remark { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<EqmSapSparePart> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<EqmSapSparePart> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<EqmSapSparePart> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT     sap.ObjID, ReceiveNo, ReceiveDate, m.SparePartName AS SparePartCode ,SparePartModel, 
                                            StoreInNum, u.UserName AS ReceiveUser, RecordDate,
                                            sap.Remark, sap.DeleteFlag
                                 FROM EqmSapSparePart   sap
                                 LEFT JOIN EqmSparePart m           ON  m.SparePartCode    = sap.SparePartCode 
                                 LEFT JOIN BasUser  u               ON  u.WorkBarcode      = sap.ReceiveUser 
                                 WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND sap.ObjID = " + queryParams.objID);
            }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.beginReceiveDate))
                {
                    sqlstr.AppendLine("AND sap.ReceiveDate  >='" + Convert.ToDateTime(queryParams.beginReceiveDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.endReceiveDate))
                {
                    sqlstr.AppendLine("AND sap.ReceiveDate  <='" + Convert.ToDateTime(queryParams.endReceiveDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            if (!string.IsNullOrEmpty(queryParams.sparepartCode))
            {
                sqlstr.AppendLine(" AND sap.SparePartCode = '" + queryParams.sparepartCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND sap.DeleteFlag ='" + queryParams.deleteFlag + "'");
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

        /// <summary>
        /// 取流水号方法
        /// </summary>
        /// <param name="MajorTypeID"></param>
        /// <param name="MinorTypeID"></param>
        /// <returns></returns>
        public string GetNextSparePartStoreInCode(DateTime storeInDate)
        {
            string dtStr = storeInDate.ToString("yyMMdd");
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT  'RK' + Convert(varchar ,(Convert(bigint ,Max(Substring(ReceiveNo,3,11)))+1)) 
                                    AS      ReceiveNo 
                                    FROM	dbo.EqmSapSparePart ");
            sqlstr.AppendLine(@"    WHERE   ReceiveNo Like 'RK" + dtStr + "%' ");

            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (String.IsNullOrEmpty(temp))
            {
                temp = "RK" + dtStr + "00001";
            }
            return temp;
        }
    }
}
