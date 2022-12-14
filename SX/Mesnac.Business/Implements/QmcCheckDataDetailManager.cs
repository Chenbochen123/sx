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
    public class QmcCheckDataDetailManager : BaseManager<QmcCheckDataDetail>, IQmcCheckDataDetailManager
    {
		#region ����ע���빹�췽��
		
        private IQmcCheckDataDetailService service;

        public QmcCheckDataDetailManager()
        {
            this.service = new QmcCheckDataDetailService();
            base.BaseService = this.service;
        }

		public QmcCheckDataDetailManager(string connectStringKey)
        {
			this.service = new QmcCheckDataDetailService(connectStringKey);
            base.BaseService = this.service;
        }

        public QmcCheckDataDetailManager(NBear.Data.Gateway way)
        {
			this.service = new QmcCheckDataDetailService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        public class QueryParams : QmcCheckDataDetailService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// ���ݼ���¼ID��ȡ����¼�����Ŀ��Ϣ
        /// </summary>
        /// <param name="checkId"></param>
        /// <returns></returns>
        public DataSet GetDataSetByCheckId(string checkId)
        {
            return this.service.GetDataSetByCheckId(checkId);
        }

        public DataTable GetSPCReport(QmcCheckDataDetailService.QueryParams param)
        {
            return this.service.GetSPCReport(param);
        }
    }
}
