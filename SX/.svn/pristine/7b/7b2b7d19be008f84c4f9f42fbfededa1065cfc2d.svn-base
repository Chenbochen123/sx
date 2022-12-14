using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using System.Data;
    public class PptShiftTimeRuleService : BaseService<PptShiftTimeRule>, IPptShiftTimeRuleService
    {
		#region 构造方法

        public PptShiftTimeRuleService() : base(){ }

        public PptShiftTimeRuleService(string connectStringKey) : base(connectStringKey){ }

        public PptShiftTimeRuleService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法




        #region IPptShiftTimeRuleService 成员

        public DataSet GetBySqlByProcedureID(string procedureID)
        {
            string sql = @"SELECT ObjID,ProcedureID,SerialID,
ShiftClass1ID=(SELECT ClassName FROM dbo.PptClass WHERE ObjID=ShiftClass1ID),
ShiftClass2ID=(SELECT ClassName FROM dbo.PptClass WHERE ObjID=ShiftClass2ID),
ShiftClass3ID=(SELECT ClassName FROM dbo.PptClass WHERE ObjID=ShiftClass3ID)
FROM dbo.PptShiftTimeRule where ProcedureID='" + procedureID + "' ORDER BY SerialID";
            DataSet ds = new DataSet();
            ds = this.GetBySql(sql).ToDataSet();
            return ds;
        }

        public bool UpdateShiftTimeRule(PptShiftTimeRule pptShiftimeRule)
        {
            bool flag = false;
            string sql = @"UPDATE [dbo].[PptShiftTimeRule]
                        SET [SerialID] ='{0}'
                        ,[ShiftClass1ID] = '{1}'
                        ,[ShiftClass2ID] = '{2}'
                        ,[ShiftClass3ID] = '{3}'
                    WHERE ObjID='{4}'";
            sql = string.Format(sql, pptShiftimeRule.SerialID, pptShiftimeRule.ShiftClass1ID, pptShiftimeRule.ShiftClass2ID, pptShiftimeRule.ShiftClass3ID, pptShiftimeRule.ObjID);
            if (pptShiftimeRule != null)
            {
                this.GetBySql(sql).ExecuteNonQuery();
            }
            return flag;
        }

        /// <summary>
        /// 根据工序获取班次规律的班组数量
        /// 孙宜建
        /// 2013-1-30
        /// </summary>
        /// <param name="proid">工序ID</param>
        /// <returns></returns>
        public int GetShiftClassNumByProcedureID(int proid)
        {
            int num=0;
            string sql = @"SELECT DISTINCT ShiftClass1ID ID FROM PptShiftTimeRule WHERE ProcedureID ={0} AND ShiftClass1ID<>0
UNION 
SELECT DISTINCT ShiftClass2ID ID FROM PptShiftTimeRule WHERE ProcedureID ={0} AND ShiftClass2ID<>0
UNION 
SELECT DISTINCT ShiftClass3ID ID FROM PptShiftTimeRule WHERE ProcedureID ={0} AND ShiftClass3ID<>0";
            sql=string.Format(sql,proid);

            num =this.GetBySql(sql).ToDataSet().Tables[0].Rows.Count;
            return num;
        }

        #endregion

    }
}
