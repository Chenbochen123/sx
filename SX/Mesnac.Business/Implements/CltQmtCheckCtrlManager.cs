using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class CltQmtCheckCtrlManager : BaseManager<CltQmtCheckCtrl>, ICltQmtCheckCtrlManager
    {
		#region 属性注入与构造方法
		
        private ICltQmtCheckCtrlService service;

        public CltQmtCheckCtrlManager()
        {
            this.service = new CltQmtCheckCtrlService();
            base.BaseService = this.service;
        }

		public CltQmtCheckCtrlManager(string connectStringKey)
        {
			this.service = new CltQmtCheckCtrlService(connectStringKey);
            base.BaseService = this.service;
        }

        public CltQmtCheckCtrlManager(NBear.Data.Gateway way)
        {
			this.service = new CltQmtCheckCtrlService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetAvgTrendDataSetByQueryParams(ICltQmtCheckCtrlQueryParams paras)
        {
            return this.service.GetAvgTrendDataSetByQueryParams(paras);
        }
        public DataSet GetCheckNotHGItemCount(ICltQmtCheckCtrlQueryParams paras)
        {
            return this.service.GetCheckNotHGItemCount(paras);
        }
        public DataSet GetCheckChart(ICltQmtCheckCtrlQueryParams paras)
        {
            return this.service.GetCheckChart(paras);
        }
        public DataSet GetFormatValue(ICltQmtCheckCtrlQueryParams paras)
        {
            return this.service.GetFormatValue(paras);
        }
    }
}
