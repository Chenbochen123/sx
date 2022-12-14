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
    public class PptEquipRubTimeManager : BaseManager<PptEquipRubTime>, IPptEquipRubTimeManager
    {
		#region 属性注入与构造方法
		
        private IPptEquipRubTimeService service;

        public PptEquipRubTimeManager()
        {
            this.service = new PptEquipRubTimeService();
            base.BaseService = this.service;
        }

		public PptEquipRubTimeManager(string connectStringKey)
        {
			this.service = new PptEquipRubTimeService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptEquipRubTimeManager(NBear.Data.Gateway way)
        {
			this.service = new PptEquipRubTimeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public DataSet GetEquipRubTime(string beginTime, string endTime, string materCode, string workShopCode)
        {
            return this.service.GetEquipRubTime(beginTime, endTime, materCode, workShopCode);
        }
    }
}
