using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using System.Data;
    public class PptShiftTimeManager : BaseManager<PptShiftTime>, IPptShiftTimeManager
    {
		#region 属性注入与构造方法
		
        private IPptShiftTimeService service;

        public PptShiftTimeManager()
        {
            this.service = new PptShiftTimeService();
            base.BaseService = this.service;
        }

		public PptShiftTimeManager(string connectStringKey)
        {
			this.service = new PptShiftTimeService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptShiftTimeManager(NBear.Data.Gateway way)
        {
			this.service = new PptShiftTimeService(way);
            base.BaseService = this.service;
        }

        #endregion

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
            DataSet ds = new DataSet();
            ds = service.GetShiftTimeByTime(start, end, dept);
            return ds;
        }

        /// <summary>
        /// 调用存储过程生成排班信息
        /// 孙宜建
        /// 2013-1-30
        /// </summary>
        /// <param name="dt">日期</param>
        /// <param name="num">周期数量</param>
        /// <param name="proid">工序ID</param>
        public void AddPptShiftTime(string dt, int num, int proid)
        {
            service.AddPptShiftTime(dt, num, proid);
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
        public DataTable GetClassNameByPIDAndDate(string shiftID, string proID, string date)
        {
            return service.GetClassNameByPIDAndDate(shiftID,proID,date);
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
            return this.service.GetShiftDS(procedureID, shiftClassID);
        }
    }
}
