using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    /// <summary>
    /// PmtProcedureService ʵ����
    /// �ﱾǿ @ 2013-04-03 13:03:28
    /// </summary>
    /// <remarks></remarks>
    public class PmtProcedureService : BaseService<PmtProcedure>, IPmtProcedureService
    {
		#region ���췽��

        /// <summary>
        /// �� PmtProcedureService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:03:28
        /// </summary>
        /// <remarks></remarks>
        public PmtProcedureService() : base(){ }

        /// <summary>
        /// �� PmtProcedureService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:03:28
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtProcedureService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// �� PmtProcedureService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:03:28
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtProcedureService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
