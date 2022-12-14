using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using System.Data;
    public interface IPptSetTimeManager : IBaseManager<PptSetTime>
    {
        /// <summary>
        /// 根据工序ID查询班次时间表
        /// 孙宜建
        /// 2013-1-29
        /// </summary>
        /// <param name="procedureID">工序ID</param>
        /// <returns></returns>
        DataSet GetDataSetByProcedureID(string procedureID);
        /// <summary>
        /// 根据工序ID获取改工序班次数量
        /// 孙宜建
        /// 2013-1-30
        /// </summary>
        /// <param name="proid">工序ID</param>
        /// <returns></returns>
        int GetShiftNumByProcedureID(int proid);

        /// <summary>
        /// 修改工序的班次时间表
        /// 孙宜建
        /// 2013-2-10
        /// </summary>
        /// <param name="setTime"></param>
        bool UpdateSetTime(PptSetTime setTime);
    }
}
