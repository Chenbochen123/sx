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
    public class QmcCheckDataPropertyManager : BaseManager<QmcCheckDataProperty>, IQmcCheckDataPropertyManager
    {
		#region 属性注入与构造方法
		
        private IQmcCheckDataPropertyService service;

        public QmcCheckDataPropertyManager()
        {
            this.service = new QmcCheckDataPropertyService();
            base.BaseService = this.service;
        }

		public QmcCheckDataPropertyManager(string connectStringKey)
        {
			this.service = new QmcCheckDataPropertyService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcCheckDataPropertyManager(NBear.Data.Gateway way)
        {
			this.service = new QmcCheckDataPropertyService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// 根据检测记录ID获取检测记录检测属性信息
        /// </summary>
        /// <param name="checkId"></param>
        /// <returns></returns>
        public DataSet GetDataSetByCheckId(string checkId)
        {
            return this.service.GetDataSetByCheckId(checkId);
        }
    }
}
