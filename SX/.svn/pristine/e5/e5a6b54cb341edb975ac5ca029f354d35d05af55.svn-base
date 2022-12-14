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
    public class Eqm_downikindManager : BaseManager<Eqm_downikind>, IEqm_downikindManager
    {
		#region 属性注入与构造方法
		
        private IEqm_downikindService service;

        public Eqm_downikindManager()
        {
            this.service = new Eqm_downikindService();
            base.BaseService = this.service;
        }

		public Eqm_downikindManager(string connectStringKey)
        {
			this.service = new Eqm_downikindService(connectStringKey);
            base.BaseService = this.service;
        }

        public Eqm_downikindManager(NBear.Data.Gateway way)
        {
			this.service = new Eqm_downikindService(way);
            base.BaseService = this.service;
        }

        #endregion
        public DataSet GetDataByParas(Eqm_downikind queryParams)
        {
            return this.service.GetDataByParas(queryParams);
        }
    }
}
