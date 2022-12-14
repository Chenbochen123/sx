using System;
using System.Collections.Generic;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// IPmtRecipeMixingLogManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 12:07:33
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtRecipeMixingLogManager : IBaseManager<PmtRecipeMixingLog>
    {
        /// <summary>
        /// ��ȡ������Ϣ��־��Ϣ
        /// �ﱾǿ @ 2013-04-03 12:07:33
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetPmtRecipeMixingLog(string recipe);
    }
}
