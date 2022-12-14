using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PmtOpenActionModelMainService : BaseService<PmtOpenActionModelMain>, IPmtOpenActionModelMainService
    {
		#region ���췽��

        public PmtOpenActionModelMainService() : base(){ }

        public PmtOpenActionModelMainService(string connectStringKey) : base(connectStringKey){ }

        public PmtOpenActionModelMainService(NBear.Data.Gateway way) : base(way){ }

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
                PageParams = new PageResult<PmtOpenActionModelMain>();
                ModelName = null;
                ModelCreateUser = null;
                DeleteFlag = null;
            }
            /// <summary>
            /// ҳ���ѯ��������
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtOpenActionModelMain> PageParams { get; set; }
            /// <summary>
            /// ģ������
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The code.</value>
            /// <remarks></remarks>
            public string ModelName { get; set; }
            /// <summary>
            /// ģ�崴������
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The address.</value>
            /// <remarks></remarks>
            public string ModelCreateDate { get; set; }
            /// <summary>
            /// ģ�崴����
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The name of the show.</value>
            /// <remarks></remarks>
            public string ModelCreateUser { get; set; }
            /// <summary>
            /// ģ����Ч����
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string ModelValidDate { get; set; }
            /// <summary>
            /// ģ������
            /// Ԭ�� @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string ModelDetail { get; set; }
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
        public PageResult<PmtOpenActionModelMain> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtOpenActionModelMain> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT t1.* , u.UserName FROM PmtOpenActionModelMain t1
                                 LEFT JOIN  BasUser u  ON u.WorkBarcode = t1.ModelCreateUser
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.ModelName))
            {
                sqlstr.AppendLine("AND t1.ModelName like '%" + queryParams.ModelName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.ModelCreateUser))
            {
                sqlstr.AppendLine("AND u.WorkBarcode like '%" + queryParams.ModelCreateUser + "%'");
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
