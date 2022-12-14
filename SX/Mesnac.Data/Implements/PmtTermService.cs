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
    /// PmtTermService ʵ����
    /// �ﱾǿ @ 2013-04-03 13:02:14
    /// </summary>
    /// <remarks></remarks>
    public class PmtTermService : BaseService<PmtTerm>, IPmtTermService
    {
		#region ���췽��

        /// <summary>
        /// �� PmtTermService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:14
        /// </summary>
        /// <remarks></remarks>
        public PmtTermService() : base(){ }

        /// <summary>
        /// �� PmtTermService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:14
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtTermService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// �� PmtTermService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:14
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtTermService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��

        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// �ﱾǿ @ 2013-04-03 13:02:14
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// �� QueryParams ���캯��
            /// �ﱾǿ @ 2013-04-03 13:02:14
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<PmtTerm>();
                Code = null;
                Address = null;
                ShowName = null;
                Remark = null;
                DeleteFlag = null;
            }
            /// <summary>
            /// Gets or sets the page params.
            /// �ﱾǿ @ 2013-04-03 13:02:14
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtTerm> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the code.
            /// �ﱾǿ @ 2013-04-03 13:02:14
            /// </summary>
            /// <value>The code.</value>
            /// <remarks></remarks>
            public string Code { get; set; }
            /// <summary>
            /// Gets or sets the address.
            /// �ﱾǿ @ 2013-04-03 13:02:14
            /// </summary>
            /// <value>The address.</value>
            /// <remarks></remarks>
            public string Address { get; set; }
            /// <summary>
            /// Gets or sets the name of the show.
            /// �ﱾǿ @ 2013-04-03 13:02:15
            /// </summary>
            /// <value>The name of the show.</value>
            /// <remarks></remarks>
            public string ShowName { get; set; }
            /// <summary>
            /// Gets or sets the remark.
            /// �ﱾǿ @ 2013-04-03 13:02:15
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string Remark { get; set; }
            /// <summary>
            /// Gets or sets the delete flag.
            /// �ﱾǿ @ 2013-04-03 13:02:15
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
        }
        #endregion
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 13:02:15
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PmtTerm> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtTerm> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT * FROM PmtTerm t1
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.Code))
            {
                sqlstr.AppendLine("AND t1.TermCode = '" + queryParams.Code + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.Address))
            {
                sqlstr.AppendLine("AND t1.TermAddress = '" + queryParams.Address + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.ShowName))
            {
                sqlstr.AppendLine("AND t1.ShowName like '%" + queryParams.ShowName + "%'");
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

