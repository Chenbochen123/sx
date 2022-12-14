using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using System.Data;
    public class PptSetTimeService : BaseService<PptSetTime>, IPptSetTimeService
    {
		#region ���췽��

        public PptSetTimeService() : base(){ }

        public PptSetTimeService(string connectStringKey) : base(connectStringKey){ }

        public PptSetTimeService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��

        #region IPptSetTimeService ��Ա
        /// <summary>
        /// ���ݹ���ID��ѯ���ʱ���
        /// ���˽�
        /// 2013-1-29
        /// </summary>
        /// <param name="procedureID">����ID</param>
        /// <returns></returns>
        public DataSet GetDataSetByProcedureID(string procedureID)
        {
            string sql = @"SELECT ObjID=PptSetTime.ObjID,ProcedureID,ShiftID,ShiftName,StartTime,StopTime,dbo.PptSetTime.UseFlag,DayFlag FROM dbo.PptSetTime LEFT JOIN PptShift ON dbo.PptSetTime.ShiftID = dbo.PptShift.ObjID
 Where ProcedureID='" +procedureID+"'";
            DataSet ds = new DataSet();
            ds = this.GetBySql(sql).ToDataSet();
            return ds;
        }

        #endregion

        #region IPptSetTimeService ��Ա

        /// <summary>
        /// ���ݹ���ID��ȡ�Ĺ���������
        /// ���˽�
        /// 2013-1-30
        /// </summary>
        /// <param name="proid">����ID</param>
        /// <returns></returns>
        public int GetShiftNumByProcedureID(int proid)
        {
            string sql = @"SELECT COUNT(*) FROM dbo.PptSetTime
WHERE ProcedureID={0} AND UseFlag<>0";
            sql = string.Format(sql,proid);
            int num = 0;
            num =Convert.ToInt32(this.GetBySql(sql).ToScalar());
            return num;

        }
        /// <summary>
        /// �޸Ĺ���İ��ʱ���
        /// ���˽�
        /// 2013-2-10
        /// </summary>
        /// <param name="setTime"></param>
        public bool UpdateSetTime(PptSetTime setTime)
        {
            string sql = @"UPDATE [dbo].[PptSetTime]
                            SET [StartTime] ='{0}'
                                ,[StopTime] = '{1}'
                                ,[UseFlag] ='{2}'
,[DayFlag]='{5}'
                            WHERE ProcedureID='{3}' AND ShiftID='{4}'";
            sql = string.Format(sql,setTime.StartTime,setTime.StopTime,setTime.UseFlag,setTime.ProcedureID,setTime.ShiftID,setTime.DayFlag);
            int i=this.GetBySql(sql).ExecuteNonQuery();
            if (i <= 0)
            {
                sql = @"INSERT INTO dbo.PptSetTime
                        ( ProcedureID ,
                            ShiftID ,
                            StartTime ,
                            StopTime ,
                            UseFlag ,
                        )
                    VALUES  ( '{0}' , -- ProcedureID - int
                            {1} , -- ShiftID - int
                            '{2}' , -- StartTime - char(8)
                            '{3}' , -- StopTime - char(8)
                            '{4}'  -- UseFlag - char(1)
,'{5}'  -- DayFlag - char(1)
                        )";
                sql = string.Format(sql, setTime.ProcedureID, setTime.ShiftID, setTime.StartTime, setTime.StopTime, setTime.UseFlag, setTime.DayFlag);
                i = this.GetBySql(sql).ExecuteNonQuery();
            }
            return true;

        }
        #endregion
    }
}