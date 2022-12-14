using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using System.Data;
    public class PptSetTimeManager : BaseManager<PptSetTime>, IPptSetTimeManager
    {
		#region ����ע���빹�췽��
		
        private IPptSetTimeService service;

        public PptSetTimeManager()
        {
            this.service = new PptSetTimeService();
            base.BaseService = this.service;
        }

		public PptSetTimeManager(string connectStringKey)
        {
			this.service = new PptSetTimeService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptSetTimeManager(NBear.Data.Gateway way)
        {
			this.service = new PptSetTimeService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// ���ݹ���ID��ѯ���ʱ���
        /// ���˽�
        /// 2013-1-29
        /// </summary>
        /// <param name="procedureID">����ID</param>
        /// <returns></returns>
        public DataSet GetDataSetByProcedureID(string procedureID)
        {
            return service.GetDataSetByProcedureID(procedureID);
        }
        /// <summary>
        /// ���ݹ���ID��ȡ�Ĺ���������
        /// ���˽�
        /// 2013-1-30
        /// </summary>
        /// <param name="proid">����ID</param>
        /// <returns></returns>
        public int GetShiftNumByProcedureID(int proid)
        {
            return service.GetShiftNumByProcedureID(proid);
        }
        /// <summary>
        /// �޸Ĺ���İ��ʱ���
        /// ���˽�
        /// 2013-2-10
        /// </summary>
        /// <param name="setTime"></param>
        public bool UpdateSetTime(PptSetTime setTime)
        {
            return service.UpdateSetTime(setTime);
        }
    }
}
