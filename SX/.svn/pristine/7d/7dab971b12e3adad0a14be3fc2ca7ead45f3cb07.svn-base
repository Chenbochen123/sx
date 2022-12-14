using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class QmtCheckAssessMasterService : BaseService<QmtCheckAssessMaster>, IQmtCheckAssessMasterService
    {
		#region 构造方法

        public QmtCheckAssessMasterService() : base(){ }

        public QmtCheckAssessMasterService(string connectStringKey) : base(connectStringKey){ }

        public QmtCheckAssessMasterService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetCheckRubberAssessQSQueryByParas(IQmtCheckRubberAssessQSQueryParams paras)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.CheckPlanDate, A.ShiftCheckId, B.ShiftName ShiftCheckName, A.ShiftCheckGroupID, C.ClassName ShiftCheckGroupName");
            sql.AppendLine(", D.WorkShopCode, E.WorkShopName");
            sql.AppendLine("FROM QmtCheckAssessMaster A");
            sql.AppendLine("LEFT JOIN PptShift B ON A.ShiftCheckId = B.ObjID");
            sql.AppendLine("LEFT JOIN PptClass C ON A.ShiftCheckGroupID = C.ObjID");
            sql.AppendLine("LEFT JOIN BasEquip D ON A.EquipCode = D.EquipCode  ");
            sql.AppendLine("LEFT JOIN BasWorkShop E ON D.WorkShopCode = E.ObjID  ");
            sql.AppendLine("WHERE ISNULL(A.CheckPlanDate, '') <> ''");

            if (paras.CheckPlanSDate != "")
            {
                sql.AppendFormat("    AND A.CheckPlanDate >= '{0}'", paras.CheckPlanSDate);
                sql.AppendLine();
            }
            if (paras.CheckPlanEDate != "")
            {
                sql.AppendFormat("    AND A.CheckPlanDate <= '{0}'", paras.CheckPlanEDate);
                sql.AppendLine();
            }
            if (paras.CheckShiftClass != "")
            {
                sql.AppendFormat("    AND A.ShiftCheckGroupID = '{0}'", paras.CheckShiftClass);
                sql.AppendLine();
            }
            if (paras.CheckShiftId != "")
            {
                sql.AppendFormat("    AND A.ShiftCheckId = '{0}'", paras.CheckShiftId);
                sql.AppendLine();
            }
            if (paras.WorkShopCode != "")
            {
                sql.AppendFormat("    AND D.WorkShopCode = '{0}'", paras.WorkShopCode);
                sql.AppendLine();
            }

            sql.AppendLine("GROUP BY A.CheckPlanDate, A.ShiftCheckId, B.ShiftName, A.ShiftCheckGroupID, C.ClassName, D.WorkShopCode, E.WorkShopName");
            sql.AppendLine("ORDER BY A.CheckPlanDate DESC, A.ShiftCheckId DESC, A.ShiftCheckGroupID DESC, E.WorkShopName");


            return GetBySql(sql.ToString()).ToDataSet();
        }

        public DataSet GetCheckRubberAssessQSReportByParas(IQmtCheckRubberAssessQSReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@CheckPlanDate", paras.CheckPlanDate);
            dict.Add("@CheckShiftClass", paras.CheckShiftClass);
            dict.Add("@CheckShiftId", paras.CheckShiftId);
            dict.Add("@WorkShopCode", paras.WorkShopCode);
            return GetDataSetByStoreProcedure("ProcQmtCheckRubberAssessQSReport", dict);

        }

        public DataSet GetCheckRubberAssessQualityCPKReportByParas(IQmtCheckRubberAssessQualityCPKReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);
            dict.Add("@ZJSID", paras.ZJSID);
            dict.Add("@EquipCode", paras.EquipCode);
            dict.Add("@OtherMaterCodes", paras.OtherMaterCodes);
            return GetDataSetByStoreProcedure("ProcQmtRubberAssessQualityCPKReport", dict);

        }

        /// <summary>
        /// 修改标识：qusf 20140414
        /// 修改说明：1.不再统计Grade IS NULL的车次
        ///           2.多次判级以最后一次为准
        ///           3.不再统计专检(实验胶)的数据
        /// 修改标识：qusf 20140104
        /// 修改说明：1.NotQuaCompute = '1'时，默认为合格
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberAssessQualitiedRateReportByParas(IQmtCheckRubberAssessQualitiedRateReportParams paras)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.MaterCode, C.MaterialName, A.PlanDate");
            sql.AppendLine("    , COUNT(*) SerialCount");
            sql.AppendLine("    , SUM(CASE WHEN A.Grade = 1 THEN 0 ELSE 1 END) UnqualitiedCount");
            sql.AppendLine("    , SUM(CASE WHEN A.Grade = 1 THEN 1 ELSE 0 END) QualitiedCount");
            sql.AppendLine("    , CONVERT(NUMERIC(9, 4), ROUND(CONVERT(NUMERIC(9, 0), SUM(CASE WHEN A.Grade = 1 THEN 1 ELSE 0 END)) / COUNT(*), 4)) QualitiedRate");
            sql.AppendLine("FROM (");
            sql.AppendLine("    SELECT A.PlanDate, A.MaterCode");
            sql.AppendLine("        , Case When ISNULL(B.NotQuaCompute, '0') = '1' Then 1 Else B.Grade End Grade");
            sql.AppendLine("        , RANK() OVER(PARTITION BY A.CheckCode, B.SerialId, B.LLSerialID");
            sql.AppendLine("            ORDER BY B.IfCheckNum DESC) RANK1");
            sql.AppendLine("    FROM QmtCheckAssessMaster A LEFT JOIN BasEquip F ON A.EquipCode = F.EquipCode");
            sql.AppendLine("    INNER JOIN QmtCheckAssessLot B ON A.CheckCode = B.CheckCode");
            sql.AppendLine("    INNER JOIN QmtCheckStandType C ON A.StandCode = C.ObjID");
            sql.AppendLine("    WHERE 1 = 1 AND C.CheckTypeCode IN (1)");
            if (paras.BeginPlanDate != "")
            {
                sql.AppendFormat("    AND LEFT(A.PlanDate, 10) >= '{0}'", paras.BeginPlanDate);
                sql.AppendLine();
            }
            if (paras.EndPlanDate != "")
            {
                sql.AppendFormat("    AND LEFT(A.PlanDate, 10) <= '{0}'", paras.EndPlanDate);
                sql.AppendLine();
            }
            if (paras.WorkBar != "")
            {
                sql.AppendFormat("    AND F.WorkShopCode = '{0}'", paras.WorkBar);
                sql.AppendLine();
            }
            if (paras.ShiftID != "")
            {
                sql.AppendFormat("    AND A.ShiftID = '{0}'", paras.ShiftID);
                sql.AppendLine();
            }
            sql.AppendLine("    ) A");
            sql.AppendLine("    LEFT JOIN BasMaterial C ON A.MaterCode = C.MaterialCode");

            sql.AppendLine("WHERE 1 = 1 AND A.Grade IS NOT NULL AND A.RANK1 = 1");
            sql.AppendLine("GROUP BY A.MaterCode, C.MaterialName, A.PlanDate");
            sql.AppendLine("ORDER BY C.MaterialName, A.PlanDate");

            return GetBySql(sql.ToString()).ToDataSet();

        }

    }

    public class QmtCheckRubberAssessQSQueryParams : IQmtCheckRubberAssessQSQueryParams
    {
        public string CheckPlanSDate { get; set; }
        public string CheckPlanEDate { get; set; }
        public string CheckShiftClass { get; set; }
        public string CheckShiftId { get; set; }
        public string WorkShopCode { get; set; }
    }

    public class QmtCheckRubberAssessQSReportParams : IQmtCheckRubberAssessQSReportParams
    {
        public string CheckPlanDate { get; set; }
        public string CheckShiftClass { get; set; }
        public string CheckShiftId { get; set; }
        public string WorkShopCode { get; set; }
    }

    public class QmtCheckRubberAssessQualityCPKReportParams : IQmtCheckRubberAssessQualityCPKReportParams
    {
        public string MaterCode { get; set; }
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string ZJSID { get; set; }
        public string EquipCode { get; set; }
        public string OtherMaterCodes { get; set; }
    }

    public class QmtCheckRubberAssessQualitiedRateReportParams : IQmtCheckRubberAssessQualitiedRateReportParams
    {
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string WorkBar { get; set; }
        public string ShiftID { get; set; }
    }


}
