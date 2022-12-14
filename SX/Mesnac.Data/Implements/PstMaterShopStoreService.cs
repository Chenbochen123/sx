using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstMaterShopStoreService : BaseService<PstMaterShopStore>, IPstMaterShopStoreService
    {
		#region 构造方法

        public PstMaterShopStoreService() : base(){ }

        public PstMaterShopStoreService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterShopStoreService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string workShopCode { get; set; }
            public DateTime planDate { get; set; }
            public string shiftID { get; set; }
            public string minorType { get; set; }
            public PageResult<PstMaterShopStore> pageParams { get; set; }
        }

        public PageResult<PstMaterShopStore> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstMaterShopStore> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.*, B.MaterialName, C.WorkShopName, D.ShiftName from PstMaterShopStore A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                                left join BasWorkShop C on A.WorkShopCode = C.ObjID
                                left join PptShift D on A.ShiftID = D.ObjID where 1 = 1");
            if (queryParams.planDate != DateTime.MinValue)
                sqlstr.AppendLine(" and PlanDate = '" + queryParams.planDate.ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.workShopCode))
                sqlstr.AppendLine(" and WorkShopCode = '" + queryParams.workShopCode + "'");
            if (!string.IsNullOrEmpty(queryParams.shiftID))
                sqlstr.AppendLine(" and ShiftID = '" + queryParams.shiftID + "'");
            if (!string.IsNullOrEmpty(queryParams.minorType))
            {
                sqlstr.AppendLine(@" and MaterCode in (
	                                select MaterialCode from BasMaterial A 
		                                left join BasMaterialMinorType B on A.MajorTypeID = B.MajorID and A.MinorTypeID = B.MinorTypeID
	                                where B.Remark = '" + queryParams.minorType + "')");
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

        public string UpdateAudit(string planDate)
        {
            string sql = "update PstMaterShopStore set AuditFlag = '1' where PlanDate = '" + planDate + "' and AuditFlag = '0'";
            int count = this.GetBySql(sql).ExecuteNonQuery();

            if (count > 0)
                return "true";
            else
                return "false";
        }

        public string UpdateCancelAudit(string planDate)
        {
            string sql = "update PstMaterShopStore set AuditFlag = '0' where PlanDate = '" + planDate + "' and AuditFlag = '1'";
            int count = this.GetBySql(sql).ExecuteNonQuery();

            if (count > 0)
                return "true";
            else
                return "false";
        }

        public string DataAllowAddJZ(string PlanDate, string WorkShopCode, string ShiftID, string MaterCode, string IsAdd)
        {
            string sql = "select top 1 PlanDate, ShiftID from PstMaterShopStore where WorkShopCode = '" + WorkShopCode + "' order by FID desc";
            DataSet ds = this.GetBySql(sql).ToDataSet();
            if (ds.Tables[0].Rows.Count == 0)
                return "结转表中不存在任何数据，请检查！";

            string getPlanDate = ds.Tables[0].Rows[0]["PlanDate"].ToString();
            string getShiftID = ds.Tables[0].Rows[0]["ShiftID"].ToString();

            if (getPlanDate != PlanDate || getShiftID != ShiftID)
                return "您选择的调整日期、车间、班次已经超出了结转范围，请检查！";

            if (IsAdd == "1")
            {
                string sqlCount = "select * from PstMaterShopStore  where PlanDate = '" + PlanDate + "' and ShiftID = '" + ShiftID + "' and WorkShopCode = '" + WorkShopCode + "' and MaterCode = '" + MaterCode + "'";
                DataSet dsCount = this.GetBySql(sqlCount).ToDataSet();
                if (dsCount.Tables[0].Rows.Count > 0)
                    return "您选择的调整日期、车间、班次下已经有该物料的结存数，请检查！";
            }

            return "OK";
        }
    }
}
