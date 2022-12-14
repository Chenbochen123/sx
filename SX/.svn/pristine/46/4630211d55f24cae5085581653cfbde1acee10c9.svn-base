using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberJZByShiftService : BaseService<PpmRubberJZByShift>, IPpmRubberJZByShiftService
    {
		#region 构造方法

        public PpmRubberJZByShiftService() : base(){ }

        public PpmRubberJZByShiftService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberJZByShiftService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string workShopCode { get; set; }
            public DateTime planDate { get; set; }
            public string shiftID { get; set; }
            public string rubType { get; set; }
            public PageResult<PpmRubberJZByShift> pageParams { get; set; }
        }

        public PageResult<PpmRubberJZByShift> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberJZByShift> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.FID, CONVERT(varchar(10), A.PlanDate, 120) PlanDate, A.ShiftID, D.ShiftName,
	                                A.WorkShopCode, B.WorkShopName, A.MaterCode, C.MaterialName, A.RubCode,
	                                A.MaterType, A.LastJZ, A.CurrentCL, A.CurrentXH, A.CurrentTZ, A.CurrentJZ, F.RubTypeName,
                                    A.CurrentIn, A.CurrentOut, A.CurrentFP
                                from PpmRubberJZByShift A
	                                left join BasWorkShop B on A.WorkShopCode = B.ObjID
	                                left join BasMaterial C on A.MaterCode = C.MaterialCode
	                                left join PptShift D on A.ShiftID = D.ObjID
                                    left join BasRubInfo E on A.RubCode = E.RubCode
	                                left join BasRubType F on E.RubTypeCode = F.ObjID
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.workShopCode))
            {
                sqlstr.AppendLine(" AND A.WorkShopCode = '" + queryParams.workShopCode + "'");
            }
            if (queryParams.planDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.PlanDate = '" + queryParams.planDate.ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            }
            if (queryParams.rubType != "all")
            {
                if (queryParams.rubType != "6")
                    sqlstr.AppendLine(" AND E.RubTypeCode = '" + queryParams.rubType + "'");
                else
                    sqlstr.AppendLine(" AND E.RubTypeCode not in ('1','2','3','4','5')");
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

        public string DataAllowAddJZ(string PlanDate, string WorkShopCode, string ShiftID, string MaterCode, string IsAdd)
        {
            string sql = "select top 1 PlanDate, ShiftID from PpmRubberJZByShift where WorkShopCode = '" + WorkShopCode + "' order by FID desc";
            DataSet ds = this.GetBySql(sql).ToDataSet();
            if (ds.Tables[0].Rows.Count == 0)
                return "结转表中不存在任何数据，请检查！";

            string getPlanDate = ds.Tables[0].Rows[0]["PlanDate"].ToString();
            string getShiftID = ds.Tables[0].Rows[0]["ShiftID"].ToString();

            if (getPlanDate != PlanDate || getShiftID != ShiftID)
                return "您选择的调整日期、车间、班次已经超出了结转范围，请检查！";

            if (IsAdd == "1")
            {
                string sqlCount = "select * from PpmRubberJZByShift  where PlanDate = '" + PlanDate + "' and ShiftID = '" + ShiftID + "' and WorkShopCode = '" + WorkShopCode + "' and MaterCode = '" + MaterCode + "'";
                DataSet dsCount = this.GetBySql(sqlCount).ToDataSet();
                if (dsCount.Tables[0].Rows.Count > 0)
                    return "您选择的调整日期、车间、班次下已经有该物料的结存数，请检查！";
            }

            return "OK";
        }
    }
}
