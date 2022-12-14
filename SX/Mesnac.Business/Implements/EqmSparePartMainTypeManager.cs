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
    public class EqmSparePartMainTypeManager : BaseManager<EqmSparePartMainType>, IEqmSparePartMainTypeManager
    {
		#region 属性注入与构造方法
		
        private IEqmSparePartMainTypeService service;

        public EqmSparePartMainTypeManager()
        {
            this.service = new EqmSparePartMainTypeService();
            base.BaseService = this.service;
        }

		public EqmSparePartMainTypeManager(string connectStringKey)
        {
			this.service = new EqmSparePartMainTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmSparePartMainTypeManager(NBear.Data.Gateway way)
        {
			this.service = new EqmSparePartMainTypeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetDataByParas( EqmSparePartMainTypeParams queryParams )
        {
            return this.service.GetDataByParas( queryParams );
        }

    }
}
