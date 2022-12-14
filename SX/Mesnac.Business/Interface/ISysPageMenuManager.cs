using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using NBear.Common;
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ISysPageMenuManager �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 11:46:20
    /// </summary>
    /// <remarks></remarks>
    public interface ISysPageMenuManager : IBaseManager<SysPageMenu>
    {
        /// <summary>
        /// ��ȡ�û������Ĳ˵��б�
        /// �ﱾǿ @ 2013-04-03 11:46:20
        /// </summary>
        /// <param name="userid">�û�ID</param>
        /// <param name="parid">�ϴβ˵�ID</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<SysPageMenu> GetUserMenuPageList(string userid, string parid);


        /// <summary>
        /// �ж��û��Ƿ���ڲ���ĳ��ҳ���Ȩ��
        /// �ﱾǿ @ 2013-04-03 11:46:20
        /// </summary>
        /// <param name="userid">�û�ID</param>
        /// <param name="pageurl">ҳ���ַ</param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool PagePermission(string userid, string pageurl);

        /// <summary>
        /// ��ȡ��ǰҳ���ID
        /// �ﱾǿ @ 2013-04-03 11:46:20
        /// </summary>
        /// <param name="pageurl">The pageurl.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        int GetPageID(string pageurl);

        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 11:46:20
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        PageResult<SysPageMenu> GetTablePageDataBySql(SysPageMenuManager.QueryParams queryParams);
    }
}
