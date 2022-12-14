using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysPageMethodManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 11:46:24
    /// </summary>
    /// <remarks></remarks>
    public interface ISysPageMethodManager : IBaseManager<SysPageMethod>
    {
        /// <summary>
        /// Appends the specified sys page method.
        /// �ﱾǿ @ 2013-04-03 11:46:24
        /// </summary>
        /// <param name="sysPageMethod">The sys page method.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int Append(SysPageMethod sysPageMethod);

        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 11:46:24
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<SysPageMethod> GetTablePageDataBySql(SysPageMethodManager.QueryParams queryParams);
    }
}
