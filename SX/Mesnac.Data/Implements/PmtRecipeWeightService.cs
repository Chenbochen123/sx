using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    /// <summary>
    /// PmtRecipeWeightService ʵ����
    /// �ﱾǿ @ 2013-04-03 13:02:20
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeWeightService : BaseService<PmtRecipeWeight>, IPmtRecipeWeightService
    {
		#region ���췽��

        /// <summary>
        /// �� PmtRecipeWeightService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:20
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeWeightService() : base(){ }

        /// <summary>
        /// �� PmtRecipeWeightService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:20
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtRecipeWeightService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// �� PmtRecipeWeightService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:20
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeWeightService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��
    }
}
