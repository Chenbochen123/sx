using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class EqmStopTypeManager : BaseManager<EqmStopType>, IEqmStopTypeManager
    {
		#region 属性注入与构造方法
		
        private IEqmStopTypeService service;

        public EqmStopTypeManager()
        {
            this.service = new EqmStopTypeService();
            base.BaseService = this.service;
        }

		public EqmStopTypeManager(string connectStringKey)
        {
			this.service = new EqmStopTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmStopTypeManager(NBear.Data.Gateway way)
        {
			this.service = new EqmStopTypeService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        public class QueryParams : EqmStopTypeService.QueryParams
        {
        }
        #endregion
        public DataSet GetDataByParas( EqmStopTypeParams queryParams )
        {
            return this.service.GetDataByParas( queryParams );
        }


        public string GetNextTypeCodeByParas( EqmStopType eqmStopType )
        {
            return this.service.GetNextTypeCodeByParas( eqmStopType );
        }

        public PageResult<EqmStopType> GetEqmStopTypeBySearchKey(Mesnac.Data.Implements.EqmStopTypeService.QueryParams queryParams)
        {
            return this.service.GetEqmStopTypeBySearchKey(queryParams);
        }
    }
}
