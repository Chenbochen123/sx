using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Business.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;
    using Mesnac.Util.Cryptography;
    using NBear.Common;
    using Mesnac.Data.Components;
    /// <summary>
    /// PmtTermManager ʵ����
    /// �ﱾǿ @ 2013-04-03 12:45:43
    /// </summary>
    /// <remarks></remarks>
    public class PmtTermManager : BaseManager<PmtTerm>, IPmtTermManager
    {
		#region ����ע���빹�췽��

        /// <summary>
        /// 
        /// �ﱾǿ @ 2013-04-03 12:45:43
        /// </summary>
        private IPmtTermService service;

        /// <summary>
        /// �� PmtTermManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:45:43
        /// </summary>
        /// <remarks></remarks>
        public PmtTermManager()
        {
            this.service = new PmtTermService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtTermManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:45:43
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtTermManager(string connectStringKey)
        {
			this.service = new PmtTermService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtTermManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:45:43
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtTermManager(NBear.Data.Gateway way)
        {
			this.service = new PmtTermService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// �ﱾǿ @ 2013-04-03 12:45:43
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtTermService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:45:43
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PmtTerm> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
