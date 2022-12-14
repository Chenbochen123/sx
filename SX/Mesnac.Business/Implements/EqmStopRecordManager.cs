using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class EqmStopRecordManager : BaseManager<EqmStopRecord>, IEqmStopRecordManager
    {
		#region 属性注入与构造方法
		
        private IEqmStopRecordService service;

        public EqmStopRecordManager()
        {
            this.service = new EqmStopRecordService();
            base.BaseService = this.service;
        }

		public EqmStopRecordManager(string connectStringKey)
        {
			this.service = new EqmStopRecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmStopRecordManager(NBear.Data.Gateway way)
        {
			this.service = new EqmStopRecordService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region IEqmStopRecordManager 成员

        public System.Data.DataSet GetDataByParas( EqmStopRecordParams queryParams )
        {
            return this.service.GetDataByParas( queryParams );
        }

        #endregion
    }
}
