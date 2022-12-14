using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class IncSapOrderReloadService : BaseService<IncSapOrderReload>, IIncSapOrderReloadService
    {
		#region 构造方法

        public IncSapOrderReloadService() : base(){ }

        public IncSapOrderReloadService(string connectStringKey) : base(connectStringKey){ }

        public IncSapOrderReloadService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public QueryParams()
            {
                pageParams = new PageResult<IncSapOrderReload>();
                objID = null;
                mesOrderCode = null;
                mesOrderType = null;
                sendBeginDate = null;
                sendEndDate = null;
                dealState = null;
                sapOrderCode = null;
                sapOrderType = null;
                ext_1 = null;
                ext_2 = null;
                deleteFlag = null;
                isUpload = null;
                operType = null;
            }
            public string objID { get; set; }
            public string mesOrderCode { get; set; }
            public string mesOrderType { get; set; }
            public string sendBeginDate { get; set; }
            public string sendEndDate { get; set; }
            public string dealState { get; set; }
            public string sapOrderCode { get; set; }
            public string sapOrderType { get; set; }
            public string ext_1 { get; set; }
            public string ext_2 { get; set; }
            public string deleteFlag { get; set; }
            public string isUpload { get; set; }
            public string operType { get; set; }
            public PageResult<IncSapOrderReload> pageParams { get; set; }
        }

        public PageResult<IncSapOrderReload> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<IncSapOrderReload> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    MesOrderCode,sys1.ItemName AS MesOrderType, 
                                            CONVERT(varchar, SendDate, 20) SendDate, SendSystem , CONVERT(varchar, UploadDate, 20) UploadDate ,sys2.ItemName AS IsUpload, DealState=dbo.FuncStrUnion(MesOrderCode) ,
                                            case when inc.Ext_2 = '0' then '上行正向传输' when inc.Ext_2 = '1' then '上行反向撤销' else '下行传输数据' end Ext_2, Ext_1
                                 FROM	    IncSapOrderReload inc 
                                 LEFT JOIN  SysCode sys1 on inc.MesOrderType = sys1.ItemCode and sys1.TypeID = 'SapOrder'
                                 LEFT JOIN  SysCode sys2 on inc.IsUpload = sys2.ItemCode and sys2.TypeID = 'YesNo'
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND inc.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.mesOrderCode))
            {
                sqlstr.AppendLine(" AND inc.MesOrderCode like '%" + queryParams.mesOrderCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.mesOrderType))
            {
                sqlstr.AppendLine(" AND inc.MesOrderType like '%" + queryParams.mesOrderType + "%'");
            }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.sendBeginDate))
                {
                    sqlstr.AppendLine("AND inc.SendDate  >='" + Convert.ToDateTime(queryParams.sendBeginDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.sendEndDate))
                {
                    sqlstr.AppendLine("AND inc.SendDate  <='" + Convert.ToDateTime(queryParams.sendEndDate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            if (!string.IsNullOrEmpty(queryParams.sapOrderCode))
            {
                sqlstr.AppendLine(" AND inc.SAPOrderCode like '%" + queryParams.sapOrderCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.sapOrderType))
            {
                sqlstr.AppendLine(" AND inc.SAPOrderType like '%" + queryParams.sapOrderType + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.ext_1))
            {
                sqlstr.AppendLine(" AND inc.Ext_1 like '%" + queryParams.ext_1 + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.ext_2))
            {
                sqlstr.AppendLine(" AND inc.Ext_2 like '%" + queryParams.ext_2 + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND inc.DeleteFlag ='" + queryParams.deleteFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.isUpload))
            {
                sqlstr.AppendLine(" AND inc.IsUpload ='" + queryParams.isUpload + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.operType))
            {
                sqlstr.AppendLine(" AND inc.MesOrderType like '" + queryParams.operType + "%'");
            }

            sqlstr.Append(" Group by MesOrderCode , sys1.ItemName , SendDate , SendSystem , UploadDate , sys2.ItemName, inc.Ext_2, Ext_1");
            if (!string.IsNullOrEmpty(queryParams.dealState))//是否成功 传E则查询成功，传S则代表错误，传空则忽略此方法。
            {
                sqlstr.AppendLine(" Having CHARINDEX('E' , dbo.FuncStrUnion(MesOrderCode)) " + ("E".Equals(queryParams.dealState)?"=":"!=") + " 0");
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
                return this.GetPageDataByReader(pageParams);
            }
        }

        public bool IsExistData(string MesOrderCode, string MesOrderType)
        {
            string sql = "select * from IncSapOrderReload where MesOrderCode like '" + MesOrderCode + "%' and MesOrderType = '" + MesOrderType + "'";

            int num = this.GetBySql(sql).ToDataSet().Tables[0].Rows.Count;

            if (num > 0)
                return true;
            else
                return false;
        }
    }
}
