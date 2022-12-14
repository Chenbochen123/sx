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
    public class PptSetTimeManager : BaseManager<PptSetTime>, IPptSetTimeManager
    {
		#region 属性注入与构造方法
		
        private IPptSetTimeService service;

        public PptSetTimeManager()
        {
            this.service = new PptSetTimeService();
            base.BaseService = this.service;
        }

		public PptSetTimeManager(string connectStringKey)
        {
			this.service = new PptSetTimeService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptSetTimeManager(NBear.Data.Gateway way)
        {
			this.service = new PptSetTimeService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// 根据工序ID查询班次时间表
        /// 孙宜建
        /// 2013-1-29
        /// </summary>
        /// <param name="procedureID">工序ID</param>
        /// <returns></returns>
        public DataSet GetDataSetByProcedureID(string procedureID)
        {
            return service.GetDataSetByProcedureID(procedureID);
        }
        /// <summary>
        /// 根据工序ID获取改工序班次数量
        /// 孙宜建
        /// 2013-1-30
        /// </summary>
        /// <param name="proid">工序ID</param>
        /// <returns></returns>
        public int GetShiftNumByProcedureID(int proid)
        {
            return service.GetShiftNumByProcedureID(proid);
        }
        /// <summary>
        /// 修改工序的班次时间表
        /// 孙宜建
        /// 2013-2-10
        /// </summary>
        /// <param name="setTime"></param>
        public bool UpdateSetTime(PptSetTime setTime)
        {
            return service.UpdateSetTime(setTime);
        }
    }
}
