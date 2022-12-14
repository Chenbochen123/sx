using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using System.Data;
    public interface IPptShiftTimeManager : IBaseManager<PptShiftTime>
    {
        /// <summary>
        /// 更加起始日期和结束日期查询对应工序的班次信息
        /// 孙宜建
        /// 2013-1-28
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        DataSet GetShiftTimeByTime(string start, string end, string dept);

        /// <summary>
        /// 调用存储过程生成排班信息
        /// 孙宜建
        /// 2013-1-30
        /// </summary>
        /// <param name="dt">日期</param>
        /// <param name="num">周期数量</param>
        /// <param name="proid">工序ID</param>
        void AddPptShiftTime(string dt, int num, int proid);


        /// <summary>
        /// 获取工序的班次信息
        /// 孙宜建
        /// 2013-2-25
        /// </summary>
        /// <param name="shiftID">班组</param>
        /// <param name="proID">工序</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        DataTable GetClassNameByPIDAndDate(string shiftID, string proID, string date);

        /// <summary>
        /// 获取当天当前时间对应的班次和班组
        /// 赵营 2013-05-31
        /// </summary>
        /// <param name="procedureID">工序</param>
        /// <param name="shiftClassID">指定班组</param>
        /// <returns></returns>
        DataSet GetShiftDS(string procedureID, string shiftClassID);
    }
}
