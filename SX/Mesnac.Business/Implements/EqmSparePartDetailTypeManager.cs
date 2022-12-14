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
    public class EqmSparePartDetailTypeManager : BaseManager<EqmSparePartDetailType>, IEqmSparePartDetailTypeManager
    {
		#region 属性注入与构造方法
		
        private IEqmSparePartDetailTypeService service;

        public EqmSparePartDetailTypeManager()
        {
            this.service = new EqmSparePartDetailTypeService();
            base.BaseService = this.service;
        }

		public EqmSparePartDetailTypeManager(string connectStringKey)
        {
			this.service = new EqmSparePartDetailTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        public EqmSparePartDetailTypeManager(NBear.Data.Gateway way)
        {
			this.service = new EqmSparePartDetailTypeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetDataByParas( EqmSparePartDetailTypeParams queryParams )
        {
            return this.service.GetDataByParas( queryParams );
        }

        public int GetNextID()
        {
            return Convert.ToInt32( this.service.GetMaxValueByProperty( EqmSparePartDetailType._.ObjID ) ) + 1;
        }

        public string GetNextCode()
        {
            return ( int.Parse( this.service.GetMaxValueByProperty( EqmSparePartDetailType._.DetailTypeCode ).ToString() ) + 1 ).ToString().PadLeft( 4 , '0' );
        }
    }
}
