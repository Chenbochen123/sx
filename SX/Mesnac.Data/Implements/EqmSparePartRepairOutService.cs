using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class EqmSparePartRepairOutService : BaseService<EqmSparePartRepairOut>, IEqmSparePartRepairOutService
    {
		#region 构造方法

        public EqmSparePartRepairOutService() : base(){ }

        public EqmSparePartRepairOutService(string connectStringKey) : base(connectStringKey){ }

        public EqmSparePartRepairOutService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {
            public string objID { get; set; }
            public string repairCode { get; set; }
            public string sparePartCode { get; set; }
            public string sparePartModel { get; set; }
            public string StoreOutNum { get; set; }
            public string sendUser { get; set; }
            public string beginSendDate { get; set; }
            public string endSendDate { get; set; }
            public string remark { get; set; }
            public string sendNo { get; set; }
            public string orderId { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<EqmSparePartRepairOut> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<EqmSparePartRepairOut> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<EqmSparePartRepairOut> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT     sap.ObjID, SendNo, SendDate, m.SparePartName AS SparePartCode ,SparePartModel, 
                                            StoreOutNum, u.UserName AS SendUser, RecordDate,
                                            sap.Remark, sap.DeleteFlag
                                 FROM EqmSparePartRepairOut   sap
                                 LEFT JOIN EqmSparePart m           ON  m.SparePartCode    = sap.SparePartCode 
                                 LEFT JOIN BasUser  u               ON  u.WorkBarcode      = sap.SendUser 
                                 WHERE 1=1  ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND sap.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.sendNo))
            {
                sqlstr.AppendLine(" AND sap.SendNo = '" + queryParams.sendNo + "'");
            }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.beginSendDate))
                {
                    sqlstr.AppendLine("AND sap.SendDate  >='" + Convert.ToDateTime(queryParams.beginSendDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.endSendDate))
                {
                    sqlstr.AppendLine("AND sap.SendDate  <='" + Convert.ToDateTime(queryParams.endSendDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            if (!string.IsNullOrEmpty(queryParams.sparePartCode))
            {
                sqlstr.AppendLine(" AND sap.SparePartCode = '" + queryParams.sparePartCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.sendUser))
            {
                sqlstr.AppendLine(" AND sap.SendUser ='" + queryParams.sendUser + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.orderId))
            {
                sqlstr.AppendLine(" AND sap.OrderID ='" + queryParams.orderId + "'");
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
        public string GetNextSparePartStoreOutCode(DateTime storeOutDate)
        {
            string dtStr = storeOutDate.ToString("yyMMdd");
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"    SELECT  'CK' + Convert(varchar ,(Convert(bigint ,Max(Substring(SendNo,3,11)))+1)) 
                                    AS      SendNo 
                                    FROM	dbo.EqmSparePartRepairOut ");
            sqlstr.AppendLine(@"    WHERE   SendNo Like 'CK" + dtStr + "%' ");

            string temp = this.GetBySql(sqlstr.ToString()).ToScalar().ToString();
            if (String.IsNullOrEmpty(temp))
            {
                temp = "CK" + dtStr + "00001";
            }
            return temp;
        }
    }
}
