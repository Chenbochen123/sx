using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class BasMainHanderManager : BaseManager<BasMainHander>, IBasMainHanderManager
    {
		#region 属性注入与构造方法
		
        private IBasMainHanderService service;

        public BasMainHanderManager()
        {
            this.service = new BasMainHanderService();
            base.BaseService = this.service;
        }

		public BasMainHanderManager(string connectStringKey)
        {
			this.service = new BasMainHanderService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasMainHanderManager(NBear.Data.Gateway way)
        {
			this.service = new BasMainHanderService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : BasMainHanderService.QueryParams
        {
        }
        #endregion
        public PageResult<BasMainHander> GetTablePageDataBySql(Mesnac.Data.Implements.BasMainHanderService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public DataSet IshaveUserInfo(string MainHanderCode, string UserCode, string ObjID)
        {
            return this.service.IshaveUserInfo(MainHanderCode, UserCode, ObjID);
        }

        public DataSet GetMixMainHanderInfo()
        {
            return this.service.GetMixMainHanderInfo();
        }
    }
}
