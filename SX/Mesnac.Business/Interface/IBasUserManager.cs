using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// ϵͳ�û� �ӿڶ���
    /// �ﱾǿ @ 2013-04-03 13:10:37
    /// </summary>
    public interface IBasUserManager : IBaseManager<BasUser>
    {

        /// <summary>
        /// ��ȡ�û�ID
        /// �ﱾǿ @ 2013-04-26 16:10:37
        /// </summary>
        /// <value>The user ID.</value>
        string UserID { get; }
        /// <summary>
        /// У���û���¼��Ϣ
        /// �ﱾǿ @ 2013-04-26 16:10:37
        /// </summary>
        /// <param name="loginUser">The login user.</param>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        bool Login(BasUser loginUser, out BasUser user);
        /// <summary>
        /// �û��ǳ�
        /// �ﱾǿ @ 2013-04-26 16:10:37
        /// </summary>
        void Logout();

        /// <summary>
        /// ��ȡ�û��б���Ϣ
        /// �ﱾǿ @ 2013-04-26 16:10:37
        /// </summary>
        /// <param name="queryParams">The query params.</param>
        /// <returns>PageResult{BasUser}.</returns>
        PageResult<BasUser> GetTablePageDataBySql(BasUserManager.QueryParams queryParams);

        /// <summary>
        /// ��ȡ��һ���û����
        /// �ﱾǿ @ 2013-04-26 16:10:37
        /// </summary>
        /// <returns>System.String.</returns>
        string GetNextWorkBarcode();

    }
}
