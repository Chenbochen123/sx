using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class PptClassManager : BaseManager<PptClass>, IPptClassManager
    {
		#region ����ע���빹�췽��
		
        private IPptClassService service;

        public PptClassManager()
        {
            this.service = new PptClassService();
            base.BaseService = this.service;
        }

		public PptClassManager(string connectStringKey)
        {
			this.service = new PptClassService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptClassManager(NBear.Data.Gateway way)
        {
			this.service = new PptClassService(way);
            base.BaseService = this.service;
        }

        #endregion


        #region ��ѯ�����ඨ��
        public class QueryParams : PptClassService.QueryParams
        {
        }
        #endregion
        /// <summary>
        /// ���ݰ������Ʋ�ѯ������Ϣ
        /// ���˽�
        /// 2013-1-25
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PptClass GetClassByName(string name)
        {
            return service.GetClassByName(name);
        }

        /// <summary>
        /// ��ҳ��ʽ��ȡ�����б���Ϣ
        /// yuany
        /// 2013��1��29��
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptClass> GetTablePageDataBySql(Mesnac.Data.Implements.PptClassService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
