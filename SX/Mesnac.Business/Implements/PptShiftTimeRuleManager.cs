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
    public class PptShiftTimeRuleManager : BaseManager<PptShiftTimeRule>, IPptShiftTimeRuleManager
    {
		#region ����ע���빹�췽��
		
        private IPptShiftTimeRuleService service;

        public PptShiftTimeRuleManager()
        {
            this.service = new PptShiftTimeRuleService();
            base.BaseService = this.service;
        }

		public PptShiftTimeRuleManager(string connectStringKey)
        {
			this.service = new PptShiftTimeRuleService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptShiftTimeRuleManager(NBear.Data.Gateway way)
        {
			this.service = new PptShiftTimeRuleService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// ���ݹ���ID��ѯ��ι���
        /// ���˽� 
        /// 2013-1-25
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataSet GetBySqlByProcedureID(string procedureID)
        {
            return service.GetBySqlByProcedureID(procedureID);
        }

        /// <summary>
        /// �޸İ����Ϣ
        /// ���˽�
        /// 2013-1-27
        /// </summary>
        /// <param name="ppt_ShiftimeRule"></param>
        /// <returns></returns>
        public bool UpdateShiftTimeRule( PptShiftTimeRule pptShiftimeRule)
        {
            return service.UpdateShiftTimeRule(pptShiftimeRule);
        }
        /// <summary>
        /// ���ݹ����ȡ��ι��ɵİ�������
        /// ���˽�
        /// 2013-1-30
        /// </summary>
        /// <param name="proid">����ID</param>
        /// <returns></returns>
        public int GetShiftClassNumByProcedureID(int proid)
        {
            return service.GetShiftClassNumByProcedureID(proid);
        }
    }
}
