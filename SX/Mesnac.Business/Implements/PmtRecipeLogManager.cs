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
    /// PmtRecipeLogManager ʵ����
    /// �ﱾǿ @ 2013-04-03 12:47:02
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeLogManager : BaseManager<PmtRecipeLog>, IPmtRecipeLogManager
    {
		#region ����ע���빹�췽��

        /// <summary>
        /// 
        /// �ﱾǿ @ 2013-04-03 12:47:02
        /// </summary>
        private IPmtRecipeLogService service;

        /// <summary>
        /// �� PmtRecipeLogManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:02
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeLogManager()
        {
            this.service = new PmtRecipeLogService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtRecipeLogManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:02
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtRecipeLogManager(string connectStringKey)
        {
			this.service = new PmtRecipeLogService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtRecipeLogManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:47:02
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeLogManager(NBear.Data.Gateway way)
        {
			this.service = new PmtRecipeLogService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// �ﱾǿ @ 2013-04-03 12:47:02
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtRecipeLogService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:47:02
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PmtRecipeLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
