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
    /// PmtMixTypeManager ʵ����
    /// �ﱾǿ @ 2013-04-03 12:47:13
    /// </summary>
    /// <remarks></remarks>
    public class PmtMixTypeManager : BaseManager<PmtMixType>, IPmtMixTypeManager
    { 
		#region ����ע���빹�췽��

        /// <summary>
        /// 
        /// �ﱾǿ @ 2013-04-03 12:47:13
        /// </summary>
        private IPmtMixTypeService service;

        /// <summary>
        /// �� PmtMixTypeManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:13
        /// </summary>
        /// <remarks></remarks>
        public PmtMixTypeManager()
        {
            this.service = new PmtMixTypeService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtMixTypeManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:13
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtMixTypeManager(string connectStringKey)
        {
			this.service = new PmtMixTypeService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtMixTypeManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:13
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtMixTypeManager(NBear.Data.Gateway way)
        {
			this.service = new PmtMixTypeService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// �ﱾǿ @ 2013-04-03 12:47:13
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtMixTypeService.QueryParams
        {
        }
        #endregion
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:47:14
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PmtMixType> GetTablePageDataBySql(Mesnac.Data.Implements.PmtMixTypeService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }


        /// <summary>
        /// ��ȡ��һ������ֵ
        /// �ﱾǿ @ 2013-04-03 12:47:14
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPmtMixTypeNextPrimaryKeyValue()
        {
            return this.service.GetPmtMixTypeNextPrimaryKeyValue();
        }
    }
}
