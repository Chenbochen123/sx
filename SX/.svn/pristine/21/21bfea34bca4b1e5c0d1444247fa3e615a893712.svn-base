using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using System.Data;
    public interface IPptShiftTimeRuleManager : IBaseManager<PptShiftTimeRule>
    {
        /// <summary>
        /// 根据工序ID查询班次规律
        /// 孙宜建 
        /// 2013-1-25
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        DataSet GetBySqlByProcedureID(string procedureID);

        /// <summary>
        /// 修改班次信息
        /// 孙宜建
        /// 2013-1-27
        /// </summary>
        /// <param name="ppt_ShiftimeRule"></param>
        /// <returns></returns>
        bool UpdateShiftTimeRule(PptShiftTimeRule pptShiftimeRule);

        /// <summary>
        /// 根据工序获取班次规律的班组数量
        /// 孙宜建
        /// 2013-1-30
        /// </summary>
        /// <param name="proid">工序ID</param>
        /// <returns></returns>
        int GetShiftClassNumByProcedureID(int proid);
    }
}
