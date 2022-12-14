using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    public class EqmStopRecordService : BaseService<EqmStopRecord>, IEqmStopRecordService
    {
		#region 构造方法

        public EqmStopRecordService() : base(){ }

        public EqmStopRecordService(string connectStringKey) : base(connectStringKey){ }

        public EqmStopRecordService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region IEqmStopRecordService 成员

        public System.Data.DataSet GetDataByParas( EqmStopRecordParams queryParams )
        {
            StringBuilder sb =new StringBuilder();
            #region
            sb.AppendLine(@"SELECT TA.ObjID, TA.EquipCode, TB.EquipName, TA.ShiftID, TG.ShiftName, TA.ClassName, 
                            TA.StartTime, TA.EndTime, CONVERT(VARCHAR(20),TA.StartTime,120) AS BeginTime, case CONVERT(varchar(4), 
                            TA.EndTime, 120) when '1900' then '' else CONVERT(varchar(19), TA.EndTime, 120) end OverTime, 
                           cast( case CONVERT(varchar(4), TA.EndTime, 120) when '1900' then '' else CONVERT(varchar, DATEDIFF(MI,TA.StartTime,TA.EndTime)) end  as int )AS DurationMi,
                            CONVERT(VARCHAR(20),TA.ReportTime,120) AS ReportTime, CONVERT(VARCHAR(20),TA.MaintainStartTime,120) AS MaintainStartTime, 
                            CONVERT(VARCHAR(20),MaintainEndTime,120) AS MaintainEndTime, TA.FaultTypeID, TC.ItemName AS FaultTypeName, TH.ItemName AS MainTypeName, 
                            TA.StopTypeID, TD.TypeName, TA.FaultID, TE.FaultName, TA.StopReason, TA.DealDesc, TA.Maintainers, TA.UserID, TF.RealName AS Recorder,
                            TA.Remark, TA.IsReportMaintain");
            sb.AppendLine( "FROM EqmStopRecord TA" );
            sb.AppendLine( "LEFT JOIN BasEquip TB ON TA.EquipCode=TB.EquipCode" );
            sb.AppendLine( "LEFT JOIN SysCode TC ON TA.FaultTypeID=TC.ItemCode AND TC.TypeID='FaultType'" );
            sb.AppendLine( "LEFT JOIN EqmStopType TD ON TA.StopTypeID=TD.TypeCode" );
            sb.AppendLine( "LEFT JOIN EqmStopFault TE ON TA.FaultID=TE.FaultCode" );
            sb.AppendLine( "LEFT JOIN BasUser TF ON TA.UserID=TF.WorkBarcode" );
            sb.AppendLine( "LEFT JOIN PptShift TG ON TA.ShiftID=TG.ObjID" );
            sb.AppendLine( "LEFT JOIN SysCode TH ON TD.MainTypeID=TH.ItemCode AND TH.TypeID='StopMainType'" );
            sb.AppendLine( "WHERE 1=1" );
            if ( !string.IsNullOrEmpty( queryParams.objID ) )
                sb.AppendLine( "AND TA.ObjID=" + queryParams.objID );
            if ( !string.IsNullOrEmpty( queryParams.equipCode ) )
                sb.AppendLine( "AND TA.EquipCode='" + queryParams.equipCode + "'" );
            if ( !string.IsNullOrEmpty( queryParams.shiftID ) )
                sb.AppendLine( "AND TA.ShiftID=" + queryParams.shiftID );
            if ( !string.IsNullOrEmpty( queryParams.startTime ) )
                sb.AppendLine( "AND TA.StartTime>='" + queryParams.startTime + "'" );
            if ( !string.IsNullOrEmpty( queryParams.endTime ) )
                sb.AppendLine( "AND TA.StartTime<='" + queryParams.endTime + "'" );
            if ( !string.IsNullOrEmpty( queryParams.stopTypeID ) )
                sb.AppendLine( "AND TA.StopTypeID='" + queryParams.stopTypeID + "'" );
            if ( !string.IsNullOrEmpty( queryParams.faultID ) )
                sb.AppendLine( "AND TA.FaultID='" + queryParams.faultID + "'" );
            if ( !string.IsNullOrEmpty( queryParams.workShopCode ) )
                sb.AppendLine( "AND TB.WorkShopCode=" + queryParams.workShopCode );
            if ( !string.IsNullOrEmpty( queryParams.mainTypeID ) )
                sb.AppendLine( "AND TD.MainTypeID='" + queryParams.mainTypeID + "'" );
            if ( !string.IsNullOrEmpty( queryParams.faultTypeID ) )
                sb.AppendLine( "AND TD.FaultTypeID=" + queryParams.faultTypeID );
            #endregion

            NBear.Data.CustomSqlSection css = this.GetBySql( sb.ToString() );
            return css.ToDataSet();
        }

        #endregion
    }
}
