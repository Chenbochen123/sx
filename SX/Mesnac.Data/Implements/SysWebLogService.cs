using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    /// <summary>
    /// SysWebLogService ʵ����
    /// �ﱾǿ @ 2013-04-03 12:52:58
    /// </summary>
    /// <remarks></remarks>
    public class SysWebLogService : BaseService<SysWebLog>, ISysWebLogService
    {
		#region ���췽��

        /// <summary>
        /// �� SysWebLogService ���캯��
        /// �ﱾǿ @ 2013-04-03 12:52:58
        /// </summary>
        /// <remarks></remarks>
        public SysWebLogService() : base(){ }

        /// <summary>
        /// �� SysWebLogService ���캯��
        /// �ﱾǿ @ 2013-04-03 12:52:58
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysWebLogService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// �� SysWebLogService ���캯��
        /// �ﱾǿ @ 2013-04-03 12:52:58
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysWebLogService(NBear.Data.Gateway way) : base(way){ }

        #endregion ���췽��

        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// �ﱾǿ @ 2013-04-03 12:52:58
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// �� QueryParams ���캯��
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<SysWebLog>();
                BeginTime = new DateTime();
                EndTime = new DateTime();
                PageName = null;
                MethodName = null;
                UserName = null;
                UserRealName = null;
                LogRemark = null;
                LogResult = null;
            }
            /// <summary>
            /// Gets or sets the page params.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<SysWebLog> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the begin time.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The begin time.</value>
            /// <remarks></remarks>
            public DateTime BeginTime { get; set; }
            /// <summary>
            /// Gets or sets the end time.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The end time.</value>
            /// <remarks></remarks>
            public DateTime EndTime { get; set; }
            /// <summary>
            /// Gets or sets the name of the page.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The name of the page.</value>
            /// <remarks></remarks>
            public string PageName { get; set; }
            /// <summary>
            /// Gets or sets the name of the method.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The name of the method.</value>
            /// <remarks></remarks>
            public string MethodName { get; set; }
            /// <summary>
            /// Gets or sets the name of the user.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The name of the user.</value>
            /// <remarks></remarks>
            public string UserName { get; set; }
            /// <summary>
            /// Gets or sets the name of the user real.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The name of the user real.</value>
            /// <remarks></remarks>
            public string UserRealName { get; set; }
            /// <summary>
            /// Gets or sets the log remark.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The log remark.</value>
            /// <remarks></remarks>
            public string LogRemark { get; set; }
            /// <summary>
            /// Gets or sets the log result.
            /// �ﱾǿ @ 2013-04-03 12:52:59
            /// </summary>
            /// <value>The log result.</value>
            /// <remarks></remarks>
            public string LogResult { get; set; }
        }
        #endregion

        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:52:59
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<SysWebLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<SysWebLog> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT t1.ObjID,t2.UserName,t2.RealName,t4.ShowName AS PageName,t3.ShowName AS MethodName,t1.Remark,t1.MethodResult,t1.UserIP,t1.RecordTime
                                FROM dbo.SysWebLog t1
                                INNER JOIN dbo.BasUser t2 ON t1.UserCode=t2.WorkBarcode
                                INNER JOIN dbo.SysPageMethod t3 ON t1.MethodID=t3.ObjID
                                INNER JOIN dbo.SysPageMenu	t4 ON t3.PageID=t4.ObjID
                                 WHERE      1 = 1 ");
            sqlstr.Append("AND t1.RecordTime>='").Append(queryParams.BeginTime.ToString()).AppendLine("'");
            sqlstr.Append("AND t1.RecordTime<='").Append(queryParams.EndTime.ToString()).AppendLine("'");
            if (!string.IsNullOrEmpty(queryParams.PageName))
            {
                sqlstr.AppendLine("AND t4.ShowName like '%" + queryParams.PageName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.MethodName))
            {
                sqlstr.AppendLine("AND t3.ShowName like '%" + queryParams.MethodName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.UserName))
            {
                sqlstr.AppendLine("AND t2.UserName like '%" + queryParams.UserName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.UserRealName))
            {
                sqlstr.AppendLine("AND t2.RealName like '%" + queryParams.UserRealName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.LogRemark))
            {
                sqlstr.AppendLine("AND t1.Remark like '%" + queryParams.LogRemark + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.LogResult))
            {
                sqlstr.AppendLine("AND t1.MethodResult like '%" + queryParams.LogResult + "%'");
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
