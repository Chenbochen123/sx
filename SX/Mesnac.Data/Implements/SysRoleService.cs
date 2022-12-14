using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using NBear.Common;
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    /// <summary>
    /// SysRoleService ʵ����
    /// �ﱾǿ @ 2013-04-03 12:53:15
    /// </summary>
    /// <remarks></remarks>
    public class SysRoleService : BaseService<SysRole>, ISysRoleService
    {
        #region ���췽��

        /// <summary>
        /// �� SysRoleService ���캯��
        /// �ﱾǿ @ 2013-04-03 12:53:16
        /// </summary>
        /// <remarks></remarks>
        public SysRoleService() : base() { }

        /// <summary>
        /// �� SysRoleService ���캯��
        /// �ﱾǿ @ 2013-04-03 12:53:16
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysRoleService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// �� SysRoleService ���캯��
        /// �ﱾǿ @ 2013-04-03 12:53:16
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysRoleService(NBear.Data.Gateway way) : base(way) { }

        #endregion ���췽��

        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// �ﱾǿ @ 2013-04-03 12:53:16
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// �� QueryParams ���캯��
            /// �ﱾǿ @ 2013-04-03 12:53:16
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<SysRole>();
                RoleName = null;
                Remark = null;
                DeleteFlag = null;
            }
            /// <summary>
            /// Gets or sets the page params.
            /// �ﱾǿ @ 2013-04-03 12:53:16
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<SysRole> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the name of the role.
            /// �ﱾǿ @ 2013-04-03 12:53:16
            /// </summary>
            /// <value>The name of the role.</value>
            /// <remarks></remarks>
            public string RoleName { get; set; }
            /// <summary>
            /// Gets or sets the remark.
            /// �ﱾǿ @ 2013-04-03 12:53:16
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string Remark { get; set; }
            /// <summary>
            /// Gets or sets the delete flag.
            /// �ﱾǿ @ 2013-04-03 12:53:16
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
        }
        #endregion
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:53:16
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<SysRole> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<SysRole> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT * FROM SysRole t1
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.RoleName))
            {
                sqlstr.AppendLine("AND t1.RoleName like '%" + queryParams.RoleName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.Remark))
            {
                sqlstr.AppendLine("AND t1.Remark like '%" + queryParams.Remark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
            {
                sqlstr.AppendLine("AND t1.DeleteFlag = '" + queryParams.DeleteFlag + "'");
            }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }
    }
}
