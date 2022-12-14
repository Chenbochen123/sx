using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PmtOpenActionService : BaseService<PmtOpenAction>, IPmtOpenActionService
    {
		#region ���췽��

        public PmtOpenActionService() : base(){ }

        public PmtOpenActionService(string connectStringKey) : base(connectStringKey){ }

        public PmtOpenActionService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��

        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// Ԭ�� @ 2013-04-03 13:58:25
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// �� QueryParams ���캯��
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<PmtOpenAction>();
                Code = null;
                Address = null;
                ShowName = null;
                Remark = null;
                DeleteFlag = null;
            }
            /// <summary>
            /// ҳ���ѯ��������
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtOpenAction> PageParams { get; set; }
            /// <summary>
            /// ���
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The code.</value>
            /// <remarks></remarks>
            public string Code { get; set; }
            /// <summary>
            /// ��ַ
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The address.</value>
            /// <remarks></remarks>
            public string Address { get; set; }
            /// <summary>
            /// ��ʾ����
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The name of the show.</value>
            /// <remarks></remarks>
            public string ShowName { get; set; }
            /// <summary>
            /// ��ע
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string Remark { get; set; }
            /// <summary>
            /// ɾ����־
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
        }
        #endregion
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// Ԭ�� @ 2013-04-03 13:03:23
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PmtOpenAction> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtOpenAction> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT * FROM PmtOpenAction t1
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.Code))
            {
                sqlstr.AppendLine("AND t1.ActionCode = '" + queryParams.Code + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.Address))
            {
                sqlstr.AppendLine("AND t1.ActionAddress = '" + queryParams.Address + "'");
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
