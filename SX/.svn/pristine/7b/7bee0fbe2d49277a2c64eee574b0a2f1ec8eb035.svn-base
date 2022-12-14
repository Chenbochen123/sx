using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class EqmRepairProtectPlanService : BaseService<EqmRepairProtectPlan>, IEqmRepairProtectPlanService
    {
		#region 构造方法

        public EqmRepairProtectPlanService() : base(){ }

        public EqmRepairProtectPlanService(string connectStringKey) : base(connectStringKey){ }

        public EqmRepairProtectPlanService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public QueryParams()
            {
                pageParams = new PageResult<EqmRepairProtectPlan>();
                equipCode = null;
                responseUser = null;
                startRepairDate = null;
                endRepairDate = null;
                repairTime = null;
                finishUser = null;
                startFinishDate = null;
                endFinishDate = null;
                startConfirmDate = null;
                endConfirmDate = null;
                confirmUser = null;
                deleteFlag = null;
                planName = null;
            }
            public string equipCode { get; set; }
            public string responseUser { get; set; }
            public string startRepairDate { get; set; }
            public string endRepairDate { get; set; }
            public string repairTime { get; set; }
            public string finishUser { get; set; }
            public string startFinishDate { get; set; }
            public string endFinishDate { get; set; }
            public string startConfirmDate { get; set; }
            public string endConfirmDate { get; set; }
            public string confirmUser { get; set; }
            public string deleteFlag { get; set; }
            public string planName { get; set; }
            public PageResult<EqmRepairProtectPlan> pageParams { get; set; }
        }

        public PageResult<EqmRepairProtectPlan> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<EqmRepairProtectPlan> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT	    p.ObjID, equip.EquipName AS EquipCode, RepairProtectPlanContent, RepairDate,  
                                            RepairTime, u1.UserName AS ResponseUser, NeedStopTime, s2.ItemName AS PlanStopTime, 
                                            FinishCondition, FinishDate, u2.UserName AS FinishUser, Verification, 
                                            u3.UserName AS ConfirmUser, ConfirmDate, case when p.DeleteFlag = '0' then '否' when p.DeleteFlag = '1' then '是' else p.DeleteFlag end DeleteFlag, p.Remark,
                                            p.PlanMonth, s1.ItemName AS PlanName, PlanMonth + '   ' + equip.EquipName + '   ' + s1.ItemName AS GroupName
                                 FROM	    EqmRepairProtectPlan p 
                                 LEFT JOIN  BasUser u1  on u1.WorkBarcode = p.ResponseUser 
                                 LEFT JOIN  BasUser u2  on u2.WorkBarcode = p.FinishUser  
                                 LEFT JOIN  BasUser u3  on u3.WorkBarcode = p.ConfirmUser  
                                 LEFT JOIN  BasEquip    equip on equip.EquipCode = p.EquipCode
                                 LEFT JOIN  SysCode s1  on s1.ItemCode = p.PlanName     and s1.TypeID = 'ProtectPlanType'
                                 LEFT JOIN  SysCode s2  on s2.ItemCode = p.PlanStopTime and s2.TypeID = 'ProtectPlanStopTime'
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND p.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.responseUser))
            {
                sqlstr.AppendLine(" AND p.ResponseUser = '" + queryParams.responseUser + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.finishUser))
            {
                sqlstr.AppendLine(" AND p.FinishUser = '" + queryParams.finishUser + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.confirmUser))
            {
                sqlstr.AppendLine(" AND p.ConfirmUser = '" + queryParams.confirmUser + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND p.DeleteFlag = '" + queryParams.deleteFlag + "'");
            } 
            if (!string.IsNullOrEmpty(queryParams.planName))
            {
                sqlstr.AppendLine(" AND p.PlanName = '" + queryParams.planName + "'");
            }
             try
            {
                if (!string.IsNullOrEmpty(queryParams.startRepairDate))
                {
                    sqlstr.AppendLine("AND p.RepairDate  >='" + Convert.ToDateTime(queryParams.startRepairDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.endRepairDate))
                {
                    sqlstr.AppendLine("AND p.RepairDate  <='" + Convert.ToDateTime(queryParams.endRepairDate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.startFinishDate))
                {
                    sqlstr.AppendLine("AND p.FinishDate  >='" + Convert.ToDateTime(queryParams.startFinishDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.endFinishDate))
                {
                    sqlstr.AppendLine("AND p.FinishDate  <='" + Convert.ToDateTime(queryParams.endFinishDate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.startConfirmDate))
                {
                    sqlstr.AppendLine("AND p.ConfirmDate  >='" + Convert.ToDateTime(queryParams.startConfirmDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.endConfirmDate))
                {
                    sqlstr.AppendLine("AND p.ConfirmDate  <='" + Convert.ToDateTime(queryParams.endConfirmDate).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            sqlstr.AppendLine(" Order by p.ObjID ");
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

        public string GetNeedStopTimeCount(string equipCode, string planName, string planMonth)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"        SELECT		Sum(NeedStopTime)       AS      NeedStopTimeCount 
                                        FROM		EqmRepairProtectPlan    ");
            sqlstr.AppendLine(" WHERE   EquipCode = '" + equipCode + "'     ");
            sqlstr.AppendLine(" AND     PlanName  = '" + planName + "'      ");
            sqlstr.AppendLine(" AND     PlanMonth = '" + planMonth + "'     ");
            return this.GetBySql(sqlstr.ToString()).ToDataSet().Tables[0].Rows[0][0].ToString();
        }
    }
}
