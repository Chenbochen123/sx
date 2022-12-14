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
    /// PmtConfigManager ʵ����
    /// �ﱾǿ @ 2013-04-03 11:52:54
    /// </summary>
    /// <remarks></remarks>
    public class PmtConfigManager : BaseManager<PmtConfig>, IPmtConfigManager
    {
        #region ����ע���빹�췽��

        /// <summary>
        /// ���ݿ������
        /// �ﱾǿ @ 2013-04-03 11:52:54
        /// </summary>
        private IPmtConfigService service;

        /// <summary>
        /// �� PmtConfigManager ���캯��
        /// �ﱾǿ @ 2013-04-03 11:52:54
        /// </summary>
        /// <remarks></remarks>
        public PmtConfigManager()
        {
            this.service = new PmtConfigService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtConfigManager ���캯��
        /// �ﱾǿ @ 2013-04-03 11:52:54
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtConfigManager(string connectStringKey)
        {
            this.service = new PmtConfigService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtConfigManager ���캯��
        /// �ﱾǿ @ 2013-04-03 11:52:54
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtConfigManager(NBear.Data.Gateway way)
        {
            this.service = new PmtConfigService(way);
            base.BaseService = this.service;
        }

        #endregion

        /// <summary>
        /// ����
        /// �ﱾǿ @ 2013-04-03 11:52:55
        /// </summary>
        /// <remarks></remarks>
        public enum TypeCode
        {
            /// <summary>
            /// ��������
            /// </summary>
            WeightMaterial,
            /// <summary>
            /// ��������
            /// </summary>
            WeightAction,
            /// <summary>
            /// �����
            /// </summary>
            AuditUser,
            /// <summary>
            /// �豸
            /// </summary>
            Equip,
            /// <summary>
            /// �䷽����
            /// </summary>
            RecipeType,
            /// <summary>
            /// �䷽״̬
            /// </summary>
            RecipeState,
            /// <summary>
            /// ̿�ڻ�������
            /// </summary>
            CarbonRecycleType,
            /// <summary>
            /// �ͣ�2�������ж�
            /// </summary>
            WeightTypey,
        }
    }
}
