using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class EqmMaintainRecordService : BaseService<EqmMaintainRecord>, IEqmMaintainRecordService
    {
		#region 构造方法

        public EqmMaintainRecordService() : base(){ }

        public EqmMaintainRecordService(string connectStringKey) : base(connectStringKey){ }

        public EqmMaintainRecordService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public System.Data.DataSet GetDataByParas(EqmMaintainRecordParams queryParams)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine(@"    SELECT TA.ObjID, TA.EquipCode, TB.EquipName, TA.ShiftID, TG.ShiftName, TA.ClassName, 
                                CONVERT(VARCHAR(20),TA.StartTime,120) AS StartTime, CONVERT(VARCHAR(20),TA.EndTime,120) AS EndTime, 
                                TA.StopTime, CONVERT(VARCHAR(20),TA.ReportTime,120) AS ReportTime, TA.ReportDuration, TA.RespondDuration,
                                CONVERT(VARCHAR(20),TA.MaintainStartTime,120) AS MaintainStartTime, CONVERT(VARCHAR(20),
                                TA.MaintainEndTime,120) AS MaintainEndTime, TA.FaultTypeID, TC.ItemName AS FaultTypeName, TH.ItemName AS MainTypeName,
                                TA.StopTypeID, TD.TypeName, TA.FaultID, TE.FaultName, TA.StopReason, TA.DealDesc, TA.Maintainers, TA.AffirmOper, 
                                TA.Status, TI.ItemName AS StatusName, TA.UserID, TF.RealName AS Recorder, TA.Remark, TA.SparePartOutNo");
            sb.AppendLine("FROM EqmMaintainRecord TA");
            sb.AppendLine("LEFT JOIN BasEquip TB ON TA.EquipCode=TB.EquipCode");
            sb.AppendLine("LEFT JOIN SysCode TC ON TA.FaultTypeID=TC.ItemCode AND TC.TypeID='FaultType'");
            sb.AppendLine("LEFT JOIN EqmStopType TD ON TA.StopTypeID=TD.TypeCode");
            sb.AppendLine("LEFT JOIN EqmStopFault TE ON TA.FaultID=TE.FaultCode");
            sb.AppendLine("LEFT JOIN BasUser TF ON TA.UserID=TF.WorkBarcode");
            sb.AppendLine("LEFT JOIN PptShift TG ON TA.ShiftID=TG.ObjID");
            sb.AppendLine("LEFT JOIN SysCode TH ON TD.MainTypeID=TH.ItemCode AND TH.TypeID='StopMainType'");
            sb.AppendLine("LEFT JOIN SysCode TI ON TA.Status=TI.ItemCode AND TI.TypeID='YesNo'");
            sb.AppendLine("WHERE 1=1");
            if (!string.IsNullOrEmpty(queryParams.objID))
                sb.AppendLine("AND TA.ObjID=" + queryParams.objID);
            if (!string.IsNullOrEmpty(queryParams.equipCode))
                sb.AppendLine("AND TA.EquipCode='" + queryParams.equipCode + "'");
            if (!string.IsNullOrEmpty(queryParams.shiftID))
                sb.AppendLine("AND TA.ShiftID=" + queryParams.shiftID);
            if (!string.IsNullOrEmpty(queryParams.startTime))
                sb.AppendLine("AND TA.StartTime>='" + queryParams.startTime + "'");
            if (!string.IsNullOrEmpty(queryParams.endTime))
                sb.AppendLine("AND TA.StartTime<='" + queryParams.endTime + "'");
            if (!string.IsNullOrEmpty(queryParams.stopTypeID))
                sb.AppendLine("AND TA.StopTypeID='" + queryParams.stopTypeID + "'");
            if (!string.IsNullOrEmpty(queryParams.faultID))
                sb.AppendLine("AND TA.FaultID='" + queryParams.faultID + "'");
            if (!string.IsNullOrEmpty(queryParams.status))
                sb.AppendLine("AND TA.Status=" + queryParams.status);
            if (!string.IsNullOrEmpty(queryParams.workShopCode))
                sb.AppendLine("AND TB.WorkShopCode=" + queryParams.workShopCode);
            if (!string.IsNullOrEmpty(queryParams.mainTypeID))
                sb.AppendLine("AND TD.MainTypeID='" + queryParams.mainTypeID + "'");
            if (!string.IsNullOrEmpty(queryParams.faultTypeID))
                sb.AppendLine("AND TD.FaultTypeID=" + queryParams.faultTypeID);
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }


        public int InsertRecord(EqmMaintainRecord record)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("INSERT INTO [EqmMaintainRecord] ([EquipCode],[ShiftID],[ClassName],[StartTime],[EndTime],[FaultTypeID],[StopTypeID],[FaultID],[StopReason],[DealDesc],[ReportTime],[MaintainStartTime],[MaintainEndTime],[Maintainers],[Status],[UserID],[AffirmOper],[Remark])");
            sb.AppendLine("VALUES (@EquipCode,@ShiftID,@ClassName,@StartTime,@EndTime,@FaultTypeID,@StopTypeID,@FaultID,@StopReason");
            sb.AppendLine(",@DealDesc,@ReportTime,@MaintainStartTime,@MaintainEndTime,@Maintainers,@Status,@UserID,@AffirmOper,@Remark);");
            #endregion

            NBear.Data.CustomSqlSection css = base.defaultGateway.FromCustomSql(sb.ToString());
            #region
            css.AddInputParameter(base.BuildDbParamName("EquipCode"), DbType.String, record.EquipCode);
            css.AddInputParameter(base.BuildDbParamName("ShiftID"), DbType.String, record.ShiftID);
            css.AddInputParameter(base.BuildDbParamName("ClassName"), DbType.String, record.ClassName);
            css.AddInputParameter(base.BuildDbParamName("StartTime"), DbType.String, record.StartTime);
            css.AddInputParameter(base.BuildDbParamName("EndTime"), DbType.String, record.EndTime);
            css.AddInputParameter(base.BuildDbParamName("FaultTypeID"), DbType.String, record.FaultTypeID);
            css.AddInputParameter(base.BuildDbParamName("StopTypeID"), DbType.String, record.StopTypeID);
            css.AddInputParameter(base.BuildDbParamName("FaultID"), DbType.String, record.FaultID);
            css.AddInputParameter(base.BuildDbParamName("StopReason"), DbType.String, record.StopReason);
            css.AddInputParameter(base.BuildDbParamName("DealDesc"), DbType.String, record.DealDesc);
            css.AddInputParameter(base.BuildDbParamName("ReportTime"), DbType.String, record.ReportTime);
            css.AddInputParameter(base.BuildDbParamName("MaintainStartTime"), DbType.String, record.MaintainStartTime);
            css.AddInputParameter(base.BuildDbParamName("MaintainEndTime"), DbType.String, record.MaintainEndTime);
            css.AddInputParameter(base.BuildDbParamName("Maintainers"), DbType.String, record.Maintainers);
            css.AddInputParameter(base.BuildDbParamName("Status"), DbType.String, record.Status);
            css.AddInputParameter(base.BuildDbParamName("UserID"), DbType.String, record.UserID);
            css.AddInputParameter(base.BuildDbParamName("AffirmOper"), DbType.String, record.AffirmOper);
            css.AddInputParameter(base.BuildDbParamName("Remark"), DbType.String, record.Remark);
            #endregion

            return css.ExecuteNonQuery();
        }


        public DataSet GetGroupDataByParas(List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("WITH T1 AS(");
            sb.AppendLine("SELECT TA.StopTypeID,TA.StopTime");
            sb.AppendLine("FROM EqmmaintainRecord TA left join EqmStopType ET on TA.StopTypeID = ET.TypeCode");
            sb.AppendLine(string.Format("WHERE TA.StopTime>0 AND TA.StartTime>='{0}'", list[0]));
            sb.AppendLine(string.Format("AND TA.EndTime<='{0}'", list[1]));
            sb.AppendLine(string.Format("AND CHARINDEX(TA.EquipCode,'{0}')>0", list[2]));
            if(!string.IsNullOrEmpty(list[3]))
            sb.AppendLine(string.Format("AND ET.MainTypeID = '{0}'", list[3]));
            sb.AppendLine("),T2 AS(");
            sb.AppendLine("SELECT SUM(StopTime) AS TotalDuration");
            sb.AppendLine("FROM T1");
            sb.AppendLine(")SELECT T1.StopTypeID, T3.TypeName, SUM(T1.StopTime) AS Duration,(SELECT TotalDuration FROM T2) AS TotalDuration, CAST(CAST(SUM(T1.StopTime)*100./(SELECT TotalDuration FROM T2) AS DECIMAL(5,2)) AS VARCHAR(8))+'%' AS DurationPercent");
            sb.AppendLine("FROM T1");
            sb.AppendLine("INNER JOIN EqmStopType T3 ON T1.StopTypeID=T3.TypeCode");
            sb.AppendLine("GROUP BY T1.StopTypeID, T3.TypeName");
            sb.AppendLine("ORDER BY T1.StopTypeID");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }

        public DataSet GetGroupDetailDataByParas(List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine("WITH T1 AS(");
            sb.AppendLine("SELECT TA.FaultID,TA.StopTime");
            sb.AppendLine("FROM EqmmaintainRecord TA");
            sb.AppendLine(string.Format("WHERE TA.StopTime>0 AND TA.StopTypeID='{0}'", list[3]));
            sb.AppendLine(string.Format("AND TA.StartTime>='{0}'", list[0]));
            sb.AppendLine(string.Format("AND TA.EndTime<='{0}'", list[1]));
            sb.AppendLine(string.Format("AND CHARINDEX(TA.EquipCode,'{0}')>0", list[2]));
            sb.AppendLine("),T2 AS(");
            sb.AppendLine("SELECT SUM(T1.StopTime) AS TotalDuration");
            sb.AppendLine("FROM T1");
            sb.AppendLine(")SELECT T1.FaultID, T3.FaultName, SUM(T1.StopTime) AS Duration,(SELECT TotalDuration FROM T2) AS TotalDuration, CAST(CAST(SUM(T1.StopTime)*100./(SELECT TotalDuration FROM T2) AS DECIMAL(5,2)) AS VARCHAR(8))+'%' AS DurationPercent");
            sb.AppendLine("FROM T1");
            sb.AppendLine("INNER JOIN EqmStopFault T3 ON T1.FaultID=T3.FaultCode");
            sb.AppendLine("GROUP BY T1.FaultID, T3.FaultName");
            sb.AppendLine("ORDER BY T1.FaultID");
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql(sb.ToString());
            return css.ToDataSet();
        }
    }
}
