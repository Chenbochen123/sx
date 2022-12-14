using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    /// <summary>
    /// ISysPageActionService �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 12:51:57
    /// </summary>
    /// <remarks></remarks>
    public interface ISysPageActionService : IBaseService<SysPageAction>
    {
        /// <summary>
        /// ��ȡ���е�ҳ�������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:51:58
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetAllPageMenuAction();


        /// <summary>
        /// ��ȡ���е�ҳ�������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:51:58
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        DataSet GetAllPageMenuAction(string powerName);
        /// <summary>
        /// ��ȡ��ǰҳ���û��Ĳ�����Ϣ
        /// �ﱾǿ @ 2013-04-03 12:51:58
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<SysPageAction> GetUserPageActionList(string url, string userid);

    }
}
