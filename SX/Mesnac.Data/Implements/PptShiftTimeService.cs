using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using System.Data;
    using NBear.Data;
    public class PptShiftTimeService : BaseService<PptShiftTime>, IPptShiftTimeService
    {
		#region 构造方法

        public PptShiftTimeService() : base(){ }

        public PptShiftTimeService(string connectStringKey) : base(connectStringKey){ }

        public PptShiftTimeService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region IPptShiftTimeService 成员
        /// <summary>
        /// 更加起始日期和结束日期查询对应工序的班次信息
        /// 孙宜建
        /// 2013-1-28
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <param name="dept">工序ID 全部查询 设置工序ID=0</param>
        /// <returns></returns>
        public DataSet GetShiftTimeByTime(string start, string end, string dept)
        {
//            string sql = @"SELECT PptShiftTime.ObjID,ProcedureName,ShiftDT,RIGHT(DATEName(dw,ShiftDT),1) AS ShiftWeek,ShiftName,ClassName,ShiftStart,ShiftEnd FROM dbo.PptShiftTime LEFT JOIN dbo.PptShift ON dbo.PptShiftTime.ShiftID = dbo.PptShift.ObjID
//LEFT JOIN dbo.PptClass ON dbo.PptShiftTime.ShiftClassID = dbo.PptClass.ObjID
//LEFT JOIN PptProcedure ON dbo.PptShiftTime.ProcedureID = dbo.PptProcedure.ObjID ";

            string sql = @"SELECT PptShiftTime.ObjID,ProcedureName,ShiftDT,RIGHT(DATEName(dw,ShiftDT),1) AS ShiftWeek,ShiftName,ClassName,ShiftStart,
ShiftEnd,PptShiftTime.facid,PptShiftTime.ProcedureID,PptShiftTime.ShiftID FROM dbo.PptShiftTime 
LEFT JOIN dbo.PptShift ON dbo.PptShiftTime.ShiftID = dbo.PptShift.ObjID
LEFT JOIN dbo.PptClass ON dbo.PptShiftTime.ShiftClassID = dbo.PptClass.ObjID
LEFT JOIN PptProcedure ON dbo.PptShiftTime.ProcedureID = dbo.PptProcedure.ObjID ";
            if (Convert.ToInt32(dept) > 0)
            {
                sql += "WHERE ShiftDT BETWEEN '" + start + "' AND '" + end + "'   AND ProcedureID='" + dept + "'";
            }
            else
            {
                sql += "WHERE ShiftDT BETWEEN '" + start + "' AND '" + end + "'";
            }
            DataSet ds = this.GetBySql(sql).ToDataSet();
            return ds;
        }

        #endregion

        #region IPptShiftTimeService 成员


        public void AddPptShiftTime(string dt, int num, int proid)
        {
            string[] paramNames ={
                this.defaultGateway.BuildDbParamName("deptid"),
                this.defaultGateway.BuildDbParamName("st"),
                this.defaultGateway.BuildDbParamName("num")
            };
            object[] paramValues = {
               proid,
               dt,
               num
            };
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPptCreateFacdate");
            for (int i = 0; i < paramNames.Length; i++)
            {
                sps.AddInputParameter(paramNames[i], this.TypeToDbType(paramValues[i].GetType()), paramValues[i]);
            }
            sps.ExecuteNonQuery();
         }

        /// <summary>
        /// 获取工序的班组信息
        /// 孙宜建
        /// 2013-2-25
        /// </summary>
        /// <param name="shiftID">班次</param>
        /// <param name="proID">工序</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public DataTable GetClassNameByPIDAndDate(string shiftID,string proID, string date)
        {
            string sql = @"SELECT ClassName,ShiftClassID,ShiftName,ShiftID FROM dbo.PptShiftTime p INNER JOIN dbo.PptClass c ON c.ObjID=p.ShiftClassID INNER JOIN dbo.PptShift s ON p.ShiftID=s.ObjID
 WHERE ProcedureID=" +proID+" AND ShiftID="+shiftID+" AND ShiftDT='"+date+"'";

            if (shiftID == "0")
            {
                sql = @" SELECT COUNT(*) Num FROM dbo.PptShiftTime 
                         WHERE ProcedureID=" + proID + " AND ShiftDT='" + date + "' AND ShiftClassID!=0";
            }

           return this.GetBySql(sql).ToDataSet().Tables[0];
        }

        /// <summary>
        /// 获取当天当前时间对应的班次和班组
        /// 赵营 2013-05-31
        /// </summary>
        /// <param name="procedureID">工序</param>
        /// <param name="shiftClassID">指定班组</param>
        /// <returns></returns>
        public DataSet GetShiftDS(string procedureID, string shiftClassID)
        {
            string sql = "";
            if (string.IsNullOrEmpty(shiftClassID))
                sql = "select ShiftID, ShiftClassID from PptShiftTime where ProcedureID = '" + procedureID + "' and ShiftDT = CONVERT(varchar(10), GETDATE(), 120) and ShiftStart <= (case when DATEPART(HOUR, GETDATE()) > 16 then DATEADD(DAY, -1, GETDATE()) else GETDATE() end) and ShiftEnd >= (case when DATEPART(HOUR, GETDATE()) > 16 then DATEADD(DAY, -1, GETDATE()) else GETDATE() end)";
            else
                sql = "select ShiftID, ShiftClassID from PptShiftTime where ProcedureID = '" + procedureID + "' and ShiftDT = CONVERT(varchar(10), GETDATE(), 120) and ShiftClassID = '" + shiftClassID + "'";

            return this.GetBySql(sql).ToDataSet();
        }

        #endregion
    }
}
