using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class QmtCheckMasterService : BaseService<QmtCheckMaster>, IQmtCheckMasterService
    {
        #region 构造方法

        public QmtCheckMasterService() : base() { }

        public QmtCheckMasterService(string connectStringKey) : base(connectStringKey) { }

        public QmtCheckMasterService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        public PageResult<QmtCheckMaster> GetCheckSummaryQueryByParas(IQmtCheckMasterSummaryQueryParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@PlanSDate", paras.PlanSDate);
            dict.Add("@PlanEDate", paras.PlanEDate);
            dict.Add("@WorkShopId", paras.WorkShopId);
            dict.Add("@ShiftId", paras.ShiftId);
            dict.Add("@ShiftClass", paras.ShiftClass);
            dict.Add("@ZJSID", paras.ZJSID);
            dict.Add("@StandCode", paras.StandCode);
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@JudgeResult", paras.JudgeResult);
            dict.Add("@EquipCode", paras.EquipCode);

            DataSet ds = GetDataSetByStoreProcedure("ProcQmtSummaryCheckInfo", dict);

            PageResult<QmtCheckMaster> pageParams = paras.PageParams;

            if (pageParams != null && pageParams.PageSize > 0)
            {
                DataTable dt = ds.Tables[0];
                pageParams.RecordCount = dt.Rows.Count;

                if (pageParams.RecordCount > 0)
                {
                    ds = new DataSet();
                    DataTable dtr = null;
                    if (pageParams.Orderfld == ""
                        || pageParams.Orderfld == (new QmtCheckMaster()).GetPropertyMappingColumnNames()[0])
                    {
                        dtr = dt.AsEnumerable()
                            .Skip((pageParams.PageIndex - 1) * pageParams.PageSize)
                            .Take(pageParams.PageSize).CopyToDataTable();
                    }
                    else
                    {
                        if (pageParams.OrderType == 1)
                        {
                            dtr = dt.AsEnumerable()
                                .OrderByDescending(x => x.Field<string>(pageParams.Orderfld))
                                .Skip((pageParams.PageIndex - 1) * pageParams.PageSize)
                                .Take(pageParams.PageSize).CopyToDataTable();
                        }
                        else
                        {
                            dtr = dt.AsEnumerable()
                                .OrderBy(x => x.Field<string>(pageParams.Orderfld))
                                .Skip((pageParams.PageIndex - 1) * pageParams.PageSize)
                                .Take(pageParams.PageSize).CopyToDataTable();
                        }
                    }
                    ds.Tables.Add(dtr);
                }

                pageParams.DataSet = ds;

                return pageParams;

            }
            else
            {
                if (pageParams == null)
                {
                    pageParams = new PageResult<QmtCheckMaster>();
                }

                pageParams.DataSet = ds;

                return pageParams;
            }

        }

        public DataSet GetMasterBatchReportByParas(IQmtCheckMasterBatchReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@PlanDate", paras.PlanDate);
            dict.Add("@ShiftId", paras.ShiftId);
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@EquipCode", paras.EquipCode);
            dict.Add("@StartSerialId", paras.StartSerialId);
            dict.Add("@EndSerialId", paras.EndSerialId);
            dict.Add("@StandCode", paras.StandCode);
            return GetDataSetByStoreProcedure("ProcQmtCheckMasterBatchReport", dict);

        }

        public DataSet GetCheckRubberQSQueryByParas(IQmtCheckRubberQSQueryParams paras)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.CheckPlanDate, A.ShiftCheckId, B.ShiftName ShiftCheckName, A.ShiftCheckGroupID, C.ClassName ShiftCheckGroupName");
            sql.AppendLine(", D.WorkShopCode, E.WorkShopName");
            sql.AppendLine("FROM QmtCheckMaster A");
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

        public DataSet GetCheckRubberQSReportByParas(IQmtCheckRubberQSReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@CheckPlanDate", paras.CheckPlanDate);
            dict.Add("@CheckShiftClass", paras.CheckShiftClass);
            dict.Add("@CheckShiftId", paras.CheckShiftId);
            dict.Add("@WorkShopCode", paras.WorkShopCode);
            return GetDataSetByStoreProcedure("ProcQmtCheckRubberQSReport", dict);

        }

        /// <summary>
        /// 修改标识：qusf 20131120
        /// 修改说明：1.不再统计Grade IS NULL的车次
        ///           2.多次判级以最后一次为准
        ///           3.不再统计专检(实验胶)的数据
        /// 修改标识：qusf 20140104
        /// 修改说明：1.NotQuaCompute = '1'时，默认为合格
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualitiedRateReportByParas(IQmtCheckRubberQualitiedRateReportParams paras)
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
            sql.AppendLine("    FROM QmtCheckMaster A LEFT JOIN BasEquip F ON A.EquipCode = F.EquipCode");
            sql.AppendLine("    INNER JOIN QmtCheckLot B ON A.CheckCode = B.CheckCode");
            sql.AppendLine("    INNER JOIN QmtCheckStandType C ON A.StandCode = C.ObjID");
            sql.AppendLine("    WHERE 1 = 1 AND C.CheckTypeCode IN (2)");
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

        /// <summary>
        /// 修改标识：qusf 20131017
        /// 修改内容：1.使用ZJSID代替WorkbarCode
        /// 修改标识：qusf 20131105
        /// 修改内容：1.将月份改为起始日期
        /// 修改标识：qusf 20140104
        /// 修改内容：1.当NotQuaCompute = '1'时，默认为合格
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualityMonthReportByParas(IQmtCheckRubberQualityMonthReportParams paras)
        {
            if (string.IsNullOrEmpty(paras.CheckTypeCode))
            {
                paras.CheckTypeCode = "2";
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.PlanDate, A.ShiftClass, A.ClassName, A.ShiftCheckGroupID");
            sql.AppendLine("    , A.CheckClassName, A.ZJSID Workerbarcode, '' as UserName, A.RubTypeId, A.RubTypeName");
            sql.AppendLine("    , A.SerialCount, A.QualitiedCount, A.UnqualitiedCount");
            sql.AppendLine("FROM (");
            sql.AppendLine("    SELECT A.PlanDate, A.ShiftClass, A.ClassName, A.ShiftCheckGroupID");
            sql.AppendLine("        , A.CheckClassName, A.ZJSID,  B.ObjID RubTypeId, A.RubTypeName");
            sql.AppendLine("        , CASE WHEN ISNULL(A.RubTypeName, B.RubTypeName) = B.RubTypeName THEN A.SerialCount ELSE NULL END SerialCount");
            sql.AppendLine("        , CASE WHEN ISNULL(A.RubTypeName, B.RubTypeName) = B.RubTypeName THEN A.UnqualitiedCount ELSE NULL END UnqualitiedCount");
            sql.AppendLine("        , CASE WHEN ISNULL(A.RubTypeName, B.RubTypeName) = B.RubTypeName THEN A.QualitiedCount ELSE NULL END QualitiedCount");
            sql.AppendLine("    FROM (");
            sql.AppendLine("        SELECT A.PlanDate, A.ShiftClass, H.ClassName, A.ShiftCheckGroupID");
            sql.AppendLine("            , I.ClassName CheckClassName, A.ZJSID,  F.RubTypeName, COUNT(*) SerialCount");
            sql.AppendLine("            , SUM(CASE WHEN A.Grade = 1 THEN 0 ELSE 1 END) UnqualitiedCount");
            sql.AppendLine("            , SUM(CASE WHEN A.Grade = 1 THEN 1 ELSE 0 END) QualitiedCount");
            sql.AppendLine("        FROM (");
            sql.AppendLine("            SELECT A.PlanDate, A.ShiftClass, A.ShiftCheckGroupID, A.ZJSID, A.MaterCode");
            sql.AppendLine("                , Case When ISNULL(B.NotQuaCompute, '0') = '1' Then 1 Else B.Grade End Grade");
            sql.AppendLine("                , RANK() OVER(PARTITION BY A.CheckCode, B.SerialId, B.LLSerialID ORDER BY B.IfCheckNum DESC) RANK1");
            
            if (paras.CheckTypeCode == "2") //检验标准
            {
                sql.AppendLine("            FROM QmtCheckMaster A");
                sql.AppendLine("            INNER JOIN QmtCheckLot B ON A.CheckCode = B.CheckCode");
                sql.AppendLine("            INNER JOIN QmtCheckStandType C ON A.StandCode = C.ObjID");
                sql.AppendLine("            INNER JOIN basequip E ON A.Equipcode =  e.Equipcode");
            }
            else if (paras.CheckTypeCode == "1")
            {
                sql.AppendLine("            FROM QmtCheckAssessMaster A");
                sql.AppendLine("            INNER JOIN QmtCheckAssessLot B ON A.CheckCode = B.CheckCode");
                sql.AppendLine("            INNER JOIN basequip E ON A.Equipcode =  e.Equipcode");
            }
            sql.AppendLine("            WHERE 1 = 1 AND B.Grade IS NOT NULL");
            if (paras.CheckTypeCode == "2") //检验标准
            {
                //sql.AppendLine("            AND C.CheckTypeCode IN (2)");
            }


            //if (paras.PlanMonth != null && paras.PlanMonth != "")
            //{
            //    sql.AppendFormat("            AND LEFT(A.PlanDate, 7) = '{0}'", paras.PlanMonth);
            //    sql.AppendLine();
            //}
            if (paras.BeginPlanDate != null && paras.BeginPlanDate != "")
            {
                sql.AppendFormat("            AND A.PlanDate >= '{0}'", paras.BeginPlanDate);
                sql.AppendLine();
            }
            if (paras.EndPlanDate != null && paras.EndPlanDate != "")
            {
                sql.AppendFormat("            AND A.PlanDate <= '{0}'", paras.EndPlanDate);
                sql.AppendLine();
            }
            if (paras.ShiftID != null && paras.ShiftID != "")
            {
                sql.AppendFormat("            AND A.ShiftClass = '{0}'", paras.ShiftID);
                sql.AppendLine();
            }
            if (paras.WorkBar != null && paras.WorkBar != "")
            {
                sql.AppendFormat("            AND E.workshopcode = '{0}'", paras.WorkBar);
                sql.AppendLine();
            }
            sql.AppendLine("        ) A");
            sql.AppendLine("        LEFT JOIN BasMaterial D ON A.MaterCode = D.MaterialCode");
            sql.AppendLine("        LEFT JOIN BasRubInfo E ON D.RubCode = E.ObjID");
            sql.AppendLine("        LEFT JOIN BasRubType F ON E.RubTypeCode = F.ObjID");
            sql.AppendLine("        LEFT JOIN PptClass H ON A.ShiftClass = H.ObjID");
            sql.AppendLine("        LEFT JOIN PptClass I ON A.ShiftCheckGroupID = I.ObjID");
            //sql.AppendLine("        LEFT JOIN BasMainHander J ON A.ZJSID = J.MainHanderCode");
            //sql.AppendLine("        LEFT JOIN BasUser K ON J.UserCode = K.HRCode");
            sql.AppendLine("        WHERE 1 = 1 AND A.RANK1 = 1");
            sql.AppendLine("        GROUP BY A.PlanDate, A.ShiftClass, H.ClassName, A.ShiftCheckGroupID, I.ClassName, A.ZJSID,  F.RubTypeName");
            sql.AppendLine("    ) A");
            sql.AppendLine("    CROSS JOIN BasRubType B");
            sql.AppendLine("    WHERE B.DeleteFlag = '0'");
            sql.AppendLine(") A");
            sql.AppendLine("ORDER BY A.ZJSID, A.RubTypeId, A.ShiftClass, A.ShiftCheckGroupID");

            return GetBySql(sql.ToString()).ToDataSet();

        }

        public DataSet GetCheckRubberCardQueryByParas(IQmtCheckRubberCardQueryParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@PlanDate", paras.PlanDate);
            dict.Add("@ZJSID", paras.ZJSID);
            dict.Add("@EquipCode", paras.EquipCode);
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@ShiftId", paras.ShiftId);
            dict.Add("@StandCode", paras.StandCode);

            return GetDataSetByStoreProcedure("ProcQmtShowCheckResultDataByStandCode", dict);

        }

        public DataSet GetCheckRubberCardReportByParas(IQmtCheckRubberCardReportParams paras)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.Barcode, A.PlanDate, A.MaterialCode MaterCode, B.MaterialName MaterName, A.EquipCode");
            sql.AppendLine("    , C.EquipName, D.ShiftName + '->' + F.ClassName AS ShiftName");
            sql.AppendLine("    , B.MaxParkTime MaxTime, A.MemNote AS SerialIdStartEnd, A.TotalWeight * A.ShelfNum AS TotalWeight, A.RealWeight");
            sql.AppendLine("    , A.ProdDate AS ProdDate, A.ProdDate AS RealProdDate, E.UserName ");
            sql.AppendLine("FROM PptShiftConfig A ");
            sql.AppendLine("    LEFT JOIN BasMaterial B ON A.MaterialCode = B.MaterialCode ");
            sql.AppendLine("    LEFT JOIN BasEquip C ON A.EquipCode = C.EquipCode ");
            sql.AppendLine("    LEFT JOIN PptShift D ON A.ShiftID = D.ObjID ");
            sql.AppendLine("    LEFT JOIN BasUser E ON A.OperCode = E.WorkBarcode ");
            sql.AppendLine("    LEFT JOIN PptClass F ON A.ClassID = F.ObjID ");
            sql.AppendLine("WHERE 1 = 1");

            if (paras.PlanDate != null && paras.PlanDate != "")
            {
                sql.AppendFormat("    AND A.PLANDATE = '{0}'", paras.PlanDate);
                sql.AppendLine();
            }

            if (paras.Barcode != null && paras.Barcode != "")
            {
                sql.AppendFormat("    AND A.Barcode = '{0}'", paras.Barcode);
                sql.AppendLine();
            }

            return GetBySql(sql.ToString()).ToDataSet();

        }

        public DataSet GetCheckRubberQualityReportByParas(IQmtCheckRubberQualityReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginCheckPlanDate", paras.BeginCheckPlanDate);
            dict.Add("@EndCheckPlanDate", paras.EndCheckPlanDate);
            dict.Add("@WorkShopCode", paras.WorkShopCode);
            string spName = "";
            if (paras.CheckTypeCode == "2")
            {
                spName = "ProcQmtRubberQualityReport";
            }
            else if (paras.CheckTypeCode == "1")
            {
                spName = "ProcQmtRubberAssessQualityReport";
            }
            return GetDataSetByStoreProcedure(spName, dict);

        }

        public DataSet GetCheckRubberQualityReportViewByParas(IQmtCheckRubberQualityReportViewParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@RubTypeCode", paras.RubTypeCode);
            dict.Add("@CheckPlanDate", paras.CheckPlanDate);
            dict.Add("@ShiftCheckId", paras.ShiftCheckId);
            dict.Add("@WorkShopCode", paras.WorkShopCode);
            dict.Add("@JudgeResult", paras.JudgeResult);
            string spName = "";
            if (paras.CheckTypeCode == "2")
            {
                spName = "ProcQmtRubberQualityReportView";
            }
            else if (paras.CheckTypeCode == "1")
            {
                spName = "ProcQmtRubberAssessQualityReportView";
            }
            return GetDataSetByStoreProcedure(spName, dict);

        }

        public DataSet GetCheckRubberQualityCPKReportByParas(IQmtCheckRubberQualityCPKReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);
            dict.Add("@ZJSID", paras.ZJSID);
            dict.Add("@EquipCode", paras.EquipCode);
            dict.Add("@OtherMaterCodes", paras.OtherMaterCodes);
            return GetDataSetByStoreProcedure("ProcQmtRubberQualityCPKReport", dict);

        }

        /// <summary>
        /// 胶料合格率月报表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualifiedRateMonthReportByParas(IQmtRubberQualifiedRateMonthReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);
            dict.Add("@RubTypeCode", paras.RubTypeCode);
            dict.Add("@StandCode", paras.StandCode);
            dict.Add("@ZJSID", paras.ZJSID);
            return GetDataSetByStoreProcedure("ProcQmtRubberQualifiedRateMonthReport", dict);

        }

        /// <summary>
        /// 胶料CPK统计日报表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualityCPKDailyReportByParas(IQmtRubberQualityCPKDailyReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@StandCode", paras.StandCode);
            return GetDataSetByStoreProcedure("ProcQmtRubberQualityCPKDailyReport", dict);
        }


        /// <summary>
        /// 胶料质检均值统计日报表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualityAvgDailyReportByParas(IQmtRubberQualityAvgDailyReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);
            dict.Add("@MaterCode", paras.MaterCode);
            return GetDataSetByStoreProcedure("ProcQmtRubberQualityAvgDailyReport", dict);
        }

        /// <summary>
        /// 胶料质检车间CPK统计日报表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualityWorkshopCPKReportByParas(IQmtRubberQualityWorkshopCPKReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);
            return GetDataSetByStoreProcedure("ProcQmtRubberQualityWorkshopCPKReport", dict);
        }

        /// <summary>
        /// 胶料质检机台CPK统计日报表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualityEquipCPKReportByParas(IQmtRubberQualityEquipCPKReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);
            return GetDataSetByStoreProcedure("ProcQmtRubberQualityEquipCPKReport", dict);
        }

        /// <summary>
        /// CPK合格率统计日报表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualityCPKRateReportByParas(IQmtRubberQualityCPKRateReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);
            dict.Add("@MaterCode", paras.MaterCode);
            dict.Add("@StandCode", paras.StandCode);
            return GetDataSetByStoreProcedure("ProcQmtRubberQualityCPKRateReport", dict);
        }

        /// <summary>
        /// 胶料硫变仪稳定性统计日报表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberLBEquipDataReportByParas(IQmtRubberLBEquipDataReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@WorkShopCode", paras.WorkShopCode);
            dict.Add("@ItemType", paras.ItemType);
            return GetDataSetByStoreProcedure("ProcQmtRubberLBEquipDataReport", dict);
        }

        /// <summary>
        /// 胶料质检主机手CPK统计日报表
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public DataSet GetCheckRubberQualityZJSCPKReportByParas(IQmtRubberQualityZJSCPKReportParams paras)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("@BeginPlanDate", paras.BeginPlanDate);
            dict.Add("@EndPlanDate", paras.EndPlanDate);

            return GetDataSetByStoreProcedure("ProcQmtRubberQualityZJSCPKReport", dict);
        }


    }

    /// <summary>
    /// 胶料质检判级汇总数据查询用
    /// </summary>
    public class QmtCheckMasterSummaryQueryParams : IQmtCheckMasterSummaryQueryParams
    {
        public string PlanSDate { get; set; }
        public string PlanEDate { get; set; }
        public string WorkShopId { get; set; }
        public string ShiftId { get; set; }
        public string ShiftClass { get; set; }
        public string ZJSID { get; set; }
        public string MaterCode { get; set; }
        public string StandCode { get; set; }
        public string JudgeResult { get; set; }
        public string EquipCode { get; set; }

        public PageResult<QmtCheckMaster> PageParams { get; set; }

    }

    public class QmtCheckMasterBatchReportParams : IQmtCheckMasterBatchReportParams
    {
        public string PlanDate { get; set; }
        public string ShiftId { get; set; }
        public string MaterCode { get; set; }
        public string EquipCode { get; set; }
        public string StartSerialId { get; set; }
        public string EndSerialId { get; set; }
        public string StandCode { get; set; }
    }

    public class QmtCheckRubberQSQueryParams : IQmtCheckRubberQSQueryParams
    {
        public string CheckPlanSDate { get; set; }
        public string CheckPlanEDate { get; set; }
        public string CheckShiftClass { get; set; }
        public string CheckShiftId { get; set; }
        public string WorkShopCode { get; set; }
    }


    public class QmtCheckRubberQSReportParams : IQmtCheckRubberQSReportParams
    {
        public string CheckPlanDate { get; set; }
        public string CheckShiftClass { get; set; }
        public string CheckShiftId { get; set; }
        public string WorkShopCode { get; set; }
    }

    public class QmtCheckRubberQualitiedRateReportParams : IQmtCheckRubberQualitiedRateReportParams
    {
       public  string BeginPlanDate { get; set; }
       public string EndPlanDate { get; set; }
       public string WorkBar { get; set; }
       public string ShiftID { get; set; }
    }

    public class QmtCheckRubberQualityMonthReportParams : IQmtCheckRubberQualityMonthReportParams
    {
        //public string PlanMonth { get; set; }
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string CheckTypeCode { get; set; }
        public string WorkBar { get; set; }
        public string ShiftID { get; set; }
    }

    public class QmtCheckRubberCardQueryParams : IQmtCheckRubberCardQueryParams
    {
        public string PlanDate { get; set; }
        public string ZJSID { get; set; }
        public string MaterCode { get; set; }
        public string EquipCode { get; set; }
        public string ShiftId { get; set; }
        public string StandCode { get; set; }
    }

    public class QmtCheckRubberCardReportParams : IQmtCheckRubberCardReportParams
    {
        public string PlanDate { get; set; }
        public string Barcode { get; set; }
    }

    public class QmtCheckRubberQualityReportParams : IQmtCheckRubberQualityReportParams
    {
        public string BeginCheckPlanDate { get; set; }
        public string EndCheckPlanDate { get; set; }
        public string WorkShopCode { get; set; }
        public string CheckTypeCode { get; set; }
    }

    public class QmtCheckRubberQualityReportViewParams : IQmtCheckRubberQualityReportViewParams
    {
        public string RubTypeCode { get; set; }
        public string CheckPlanDate { get; set; }
        public string ShiftCheckId { get; set; }
        public string WorkShopCode { get; set; }
        public string JudgeResult { get; set; }
        public string CheckTypeCode { get; set; }
    }


    public class QmtCheckRubberQualityCPKReportParams : IQmtCheckRubberQualityCPKReportParams
    {
        public string MaterCode { get; set; }
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string ZJSID { get; set; }
        public string EquipCode { get; set; }
        public string OtherMaterCodes { get; set; }
    }

    /// <summary>
    /// 胶料合格率月报表查询参数类
    /// </summary>
    public class QmtRubberQualifiedRateMonthReportParams : IQmtRubberQualifiedRateMonthReportParams
    {
        // public string PlanMonth { get; set; }
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string RubTypeCode { get; set; }
        public string StandCode { get; set; }
        public string ZJSID { get; set; }
    }

    /// <summary>
    /// 胶料CPK统计日报表查询参数类
    /// </summary>
    public class QmtRubberQualityCPKDailyReportParams : IQmtRubberQualityCPKDailyReportParams
    {
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string MaterCode { get; set; }
        public string StandCode { get; set; }
    }

    /// <summary>
    /// 胶料质检均值统计日报表查询参数类
    /// </summary>
    public class QmtRubberQualityAvgDailyReportParams : IQmtRubberQualityAvgDailyReportParams
    {
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string MaterCode { get; set; }
    }

    /// <summary>
    /// 胶料质检车间CPK统计日报表查询参数类
    /// </summary>
    public class QmtRubberQualityWorkshopCPKReportParams : IQmtRubberQualityWorkshopCPKReportParams
    {
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
    }

    /// <summary>
    /// 胶料质检机台CPK统计日报表查询参数类
    /// </summary>
    public class QmtRubberQualityEquipCPKReportParams : IQmtRubberQualityEquipCPKReportParams
    {
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
    }

    /// <summary>
    /// CPK合格率统计日报表查询参数类
    /// </summary>
    public class QmtRubberQualityCPKRateReportParams : IQmtRubberQualityCPKRateReportParams
    {
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
        public string MaterCode { get; set; }
        public string StandCode { get; set; }
    }


    /// <summary>
    /// 胶料硫变仪稳定性统计日报表查询参数类
    /// </summary>
    public class QmtRubberLBEquipDataReportParams : IQmtRubberLBEquipDataReportParams
    {
        public string BeginPlanDate { get; set; }
        public string WorkShopCode { get; set; }
        public string ItemType { get; set; }
    }

    /// <summary>
    /// 胶料质检主机手CPK统计日报表查询参数类
    /// </summary>
    public class QmtRubberQualityZJSCPKReportParams : IQmtRubberQualityZJSCPKReportParams
    {
        public string BeginPlanDate { get; set; }
        public string EndPlanDate { get; set; }
    }

}
