using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using System.Data;
    public class PptPlanMgrService : BaseService<PptPlanMgr>, IPptPlanMgrService
    {
		#region 构造方法

        public PptPlanMgrService() : base(){ }

        public PptPlanMgrService(string connectStringKey) : base(connectStringKey){ }

        public PptPlanMgrService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string materialCode { get; set; }
            public string startPlanDate { get; set; }
            public string endPlanDate { get; set; }
            public string equipCode { get; set; }
            public string deleteFlag { get; set; }
            public string WorkShopCode { get; set; }
            public PageResult<PptPlanMgr> pageParams { get; set; }
        }

        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptPlanMgr> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PptPlanMgr> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT     mgr.ObjID, mgr.PlanDate, q.EquipName as EquipCode,q.EquipCode EquipName, mgr.ERPCode, m.MaterialName as MaterialCode, 
                                             mgr.MaterialCode MaterialName, mgr.RecipeVersionID, mgr.RecipeName, mgr.RecommendTotalCount, 
                                            mgr.PlanTotalCount, mgr.ActualOnePlan, mgr.ActualOneProNum, mgr.ActualOneRemark,
                                            s3.ItemName as ActualOnePlanID , s4.ItemName as ActualTwoPlanID , s5.ItemName as ActualThreePlanID,
                                            mgr.ActualTwoPlan, mgr.ActualTwoProNum, mgr.ActualTwoRemark, mgr.ActualThreePlan, 
                                            mgr.ActualThreeProNum, mgr.ActualThreeRemark, mgr.ExecSheet, mgr.ExecSheetDate, 
                                            u.UserName as Auditor, mgr.AuditDate, s1.ItemName as AuditFlag, s2.ItemName as CreatePlanFlag, 
                                            mgr.DeleteFlag , mgr.Remark
                                 FROM       PptPlanMgr mgr
                                 LEFT JOIN  BasEquip q      ON q.EquipCode = mgr.EquipCode  
                                 LEFT JOIN  BasMaterial m   ON m.MaterialCode = mgr.MaterialCode
                                 LEFT JOIN  SysCode s1      ON s1.ItemCode = mgr.AuditFlag       AND     s1.TypeID = 'Audit'
                                 LEFT JOIN  SysCode s2      ON s2.ItemCode = mgr.CreatePlanFlag  AND     s2.TypeID = 'CreateState'
                                 LEFT JOIN  BasUser u       ON u.WorkBarCode = mgr.Auditor
                                 LEFT JOIN  PptPlan one     On one.PlanID = mgr.ActualOnePlanID
                                 LEFT JOIN  PptPlan two     On two.PlanID = mgr.ActualTwoPlanID
                                 LEFT JOIN  PptPlan three   On three.PlanID = mgr.ActualThreePlanID
                                 LEFT JOIN  SysCode s3      On one.PlanState = s3.ItemCode    AND   s3.TypeID = 'PlanState'
                                 LEFT JOIN  SysCode s4      On two.PlanState = s4.ItemCode    AND   s4.TypeID = 'PlanState'
                                 LEFT JOIN  SysCode s5      On three.PlanState = s5.ItemCode  AND   s5.TypeID = 'PlanState'
                                 WHERE  1=1 AND mgr.Addflag=1 ");
            if (!string.IsNullOrEmpty(queryParams.materialCode))
            {
                sqlstr.AppendLine(" AND mgr.MaterialCode = '" + queryParams.materialCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND mgr.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (queryParams.startPlanDate != "0001-01-01 0:00:00" && queryParams.startPlanDate != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine(" AND mgr.PlanDate >= '" + queryParams.startPlanDate + "'");
            }
            if (queryParams.endPlanDate != "0001-01-01 0:00:00" && queryParams.endPlanDate != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine(" AND mgr.PlanDate <= '" + queryParams.endPlanDate + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND mgr.DeleteFlag = '" + queryParams.deleteFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.WorkShopCode))
            {
                sqlstr.AppendLine("  AND q.WorkShopCode='" + queryParams.WorkShopCode + "'");
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
        /// 分页方法
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptPlanMgr> GetTablePageAddPlanInfoBySql(QueryParams queryParams)
        {
            PageResult<PptPlanMgr> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT     mgr.ObjID, mgr.PlanDate, q.EquipName ,q.EquipCode, mgr.ERPCode, m.MaterialName , 
                                    mgr.MaterialCode, mgr.RecipeVersionID, mgr.RecipeName, mgr.RecommendTotalCount, 
                                    mgr.PlanTotalCount, mgr.ActualOnePlan, mgr.ActualOneProNum, mgr.ActualOneRemark, 
                                    mgr.ActualTwoPlan, mgr.ActualTwoProNum, mgr.ActualTwoRemark, mgr.ActualThreePlan, 
                                    mgr.ActualThreeProNum, mgr.ActualThreeRemark, mgr.ExecSheet, mgr.ExecSheetDate, 
                                    mgr.DeleteFlag , mgr.Remark
                                    FROM  PptPlanMgr mgr LEFT JOIN  BasEquip q ON q.EquipCode = mgr.EquipCode  
                                    LEFT JOIN  BasMaterial m   ON m.MaterialCode = mgr.MaterialCode
                                    WHERE  1=1 AND mgr.Addflag=0 AND mgr.DeleteFlag=0  ");
            if (!string.IsNullOrEmpty(queryParams.materialCode))
            {
                sqlstr.AppendLine(" AND mgr.MaterialCode = '" + queryParams.materialCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND mgr.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (queryParams.startPlanDate != "0001-01-01 0:00:00" && queryParams.startPlanDate != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine(" AND mgr.PlanDate = '" + queryParams.startPlanDate + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.WorkShopCode))
            {
                sqlstr.AppendLine("  AND q.WorkShopCode='" + queryParams.WorkShopCode + "'");
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


        #region IPptPlanMgrService 成员


        public DataSet GetListAddPlanInfoByWhere(string planDate, string equipcode, string materCode)
        {
            DataSet ds = new DataSet();
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT     mgr.ObjID, mgr.PlanDate, q.EquipName ,q.EquipCode, mgr.ERPCode, m.MaterialName , 
                                    mgr.MaterialCode, mgr.RecipeVersionID, mgr.RecipeName, mgr.RecommendTotalCount, 
                                    mgr.PlanTotalCount, mgr.ActualOnePlan, mgr.ActualOneProNum, mgr.ActualOneRemark, 
                                    mgr.ActualTwoPlan, mgr.ActualTwoProNum, mgr.ActualTwoRemark, mgr.ActualThreePlan, 
                                    mgr.ActualThreeProNum, mgr.ActualThreeRemark, mgr.ExecSheet, mgr.ExecSheetDate, 
                                    mgr.DeleteFlag , mgr.Remark
                                    FROM  PptPlanMgr mgr LEFT JOIN  BasEquip q ON q.EquipCode = mgr.EquipCode  
                                    LEFT JOIN  BasMaterial m   ON m.MaterialCode = mgr.MaterialCode
                                    WHERE  1=1 AND mgr.Addflag=0 AND mgr.DeleteFlag=0 ");
            if (!string.IsNullOrEmpty(materCode))
            {
                sqlstr.AppendLine(" AND mgr.MaterialCode = '" + materCode + "'");
            }
            if (!string.IsNullOrEmpty(equipcode))
            {
                sqlstr.AppendLine(" AND mgr.EquipCode = '" + equipcode + "'");
            }
            if (planDate != "0001-01-01 0:00:00" && planDate != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine(" AND mgr.PlanDate = '" + planDate + "'");
            }
            ds = GetBySql(sqlstr.ToString()).ToDataSet();
            return ds;
        }

        #endregion
    }
}
