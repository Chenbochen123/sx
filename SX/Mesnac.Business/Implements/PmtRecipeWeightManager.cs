using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    /// <summary>
    /// PmtRecipeWeightManager ʵ����
    /// �ﱾǿ @ 2013-04-03 12:45:55
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeWeightManager : BaseManager<PmtRecipeWeight>, IPmtRecipeWeightManager
    {
		#region ����ע���빹�췽��

        /// <summary>
        /// 
        /// �ﱾǿ @ 2013-04-03 12:45:55
        /// </summary>
        private IPmtRecipeWeightService service;

        /// <summary>
        /// �� PmtRecipeWeightManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:45:55
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeWeightManager()
        {
            this.service = new PmtRecipeWeightService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtRecipeWeightManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:45:55
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtRecipeWeightManager(string connectStringKey)
        {
			this.service = new PmtRecipeWeightService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtRecipeWeightManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:45:55
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeWeightManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeWeightService(way);
            base.BaseService = this.service;
        }

        #endregion
    }
}
