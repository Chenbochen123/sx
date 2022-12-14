using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    /// <summary>
    /// PmtMixingModelManager ʵ����
    /// �ﱾǿ @ 2013-04-03 12:47:20
    /// </summary>
    /// <remarks></remarks>
    public class PmtMixingModelManager : BaseManager<PmtMixingModel>, IPmtMixingModelManager
    {
		#region ����ע���빹�췽��

        /// <summary>
        /// 
        /// �ﱾǿ @ 2013-04-03 12:47:20
        /// </summary>
        private IPmtMixingModelService service;

        /// <summary>
        /// �� PmtMixingModelManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:20
        /// </summary>
        /// <remarks></remarks>
        public PmtMixingModelManager()
        {
            this.service = new PmtMixingModelService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtMixingModelManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:20
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtMixingModelManager(string connectStringKey)
        {
			this.service = new PmtMixingModelService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtMixingModelManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:20
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtMixingModelManager(NBear.Data.Gateway way)
        {
			this.service = new PmtMixingModelService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// У��ģ��
        /// �ﱾǿ @ 2013-04-03 12:47:20
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string ValidityModel(List<PmtMixingModel> lst)
        {
            string Result = string.Empty;
            return Result;
        }
        /// <summary>
        /// ��������ģ��
        /// �ﱾǿ @ 2013-04-03 12:44:33
        /// �ﱾǿ @ 2013-04-03 12:47:21
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SaveModel(List<PmtMixingModel> lst)
        {
            string Result = ValidityModel(lst);
            if (Result.Length > 0)
            {
                return Result;
            }
            foreach (PmtMixingModel m in lst)
            {
                this.Insert(m);
            }
            return Result;
        }
    }
}
