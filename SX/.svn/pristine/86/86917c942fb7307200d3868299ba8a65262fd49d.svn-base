using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    public class EqmMaintainRecordManager : BaseManager<EqmMaintainRecord>, IEqmMaintainRecordManager
    {
		#region 属性注入与构造方法
		
        private IEqmMaintainRecordService service;

        public EqmMaintainRecordManager()
        {
            this.service = new EqmMaintainRecordService();
            base.BaseService = this.service;
        }

		public EqmMaintainRecordManager(string connectStringKey)
        {
			this.service = new EqmMaintainRecordService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmMaintainRecordManager(NBear.Data.Gateway way)
        {
			this.service = new EqmMaintainRecordService(way);
            base.BaseService = this.service;
        }

        #endregion

        //获取维修记录信息
        public System.Data.DataSet GetDataByParas(EqmMaintainRecordParams queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }

        //新增维修记录信息
        public int InsertRecord(EqmMaintainRecord record)
        {
            return this.service.InsertRecord(record);
        }

        //获取维修记录统计信息
        public System.Data.DataSet GetGroupDataByParas(List<string> list)
        {
            return this.service.GetGroupDataByParas(list);
        }

        //获取维修记录统计明细信息
        public System.Data.DataSet GetGroupDetailDataByParas(List<string> list)
        {
            return this.service.GetGroupDetailDataByParas(list);
        }
    }
}
