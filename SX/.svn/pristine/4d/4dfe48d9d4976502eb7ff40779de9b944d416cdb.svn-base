using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    /// <summary>
    /// SysLoginLogService 实现类
    /// 孙本强 @ 2013-04-03 12:53:40
    /// </summary>
    /// <remarks></remarks>
    public class SysLoginLogService : BaseService<SysLoginLog>, ISysLoginLogService
    {
        #region 构造方法

        /// <summary>
        /// 类 SysLoginLogService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:40
        /// </summary>
        /// <remarks></remarks>
        public SysLoginLogService() : base() { }

        /// <summary>
        /// 类 SysLoginLogService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:40
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysLoginLogService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 SysLoginLogService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:40
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysLoginLogService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法


        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:53:40
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 孙本强 @ 2013-04-03 12:53:40
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<SysLoginLog>();
                BeginTime = new DateTime();
                EndTime = new DateTime();
                LoginState = null;
                UserName = null;
                UserRealName = null;
            }
            /// <summary>
            /// Gets or sets the page params.
            /// 孙本强 @ 2013-04-03 12:53:40
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<SysLoginLog> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the begin time.
            /// 孙本强 @ 2013-04-03 12:53:40
            /// </summary>
            /// <value>The begin time.</value>
            /// <remarks></remarks>
            public DateTime BeginTime { get; set; }
            /// <summary>
            /// Gets or sets the end time.
            /// 孙本强 @ 2013-04-03 12:53:41
            /// </summary>
            /// <value>The end time.</value>
            /// <remarks></remarks>
            public DateTime EndTime { get; set; }
            /// <summary>
            /// Gets or sets the state of the login.
            /// 孙本强 @ 2013-04-03 12:53:41
            /// </summary>
            /// <value>The state of the login.</value>
            /// <remarks></remarks>
            public string LoginState { get; set; }
            /// <summary>
            /// Gets or sets the name of the user.
            /// 孙本强 @ 2013-04-03 12:53:41
            /// </summary>
            /// <value>The name of the user.</value>
            /// <remarks></remarks>
            public string UserName { get; set; }
            /// <summary>
            /// Gets or sets the name of the user real.
            /// 孙本强 @ 2013-04-03 12:53:41
            /// </summary>
            /// <value>The name of the user real.</value>
            /// <remarks></remarks>
            public string UserRealName { get; set; }
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:53:41
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysLoginLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<SysLoginLog> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT t1.ObjID,t2.UserName,t2.RealName,t2.WorkBarcode,t1.LoginIP,t1.LoginTime,t1.LogoutIP,t1.LogoutTime
                                FROM dbo.SysLoginLog t1
                                INNER JOIN dbo.BasUser t2 ON t1.UserCode=t2.WorkBarcode
                                 WHERE     1 = 1 ");
            sqlstr.Append("AND t1.LoginTime>='").Append(queryParams.BeginTime.ToString()).AppendLine("'");
            sqlstr.Append("AND t1.LoginTime<='").Append(queryParams.EndTime.ToString()).AppendLine("'");
            if (!string.IsNullOrEmpty(queryParams.UserName))
            {
                sqlstr.AppendLine("AND t2.UserName like '%" + queryParams.UserName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.UserRealName))
            {
                sqlstr.AppendLine("AND t2.RealName like '%" + queryParams.UserRealName + "%'");
            }
            if (queryParams.LoginState=="1")
            {
                sqlstr.AppendLine("AND ISNULL(t1.LogoutIP,'')=''");
            }
            if (queryParams.LoginState == "2")
            {
                sqlstr.AppendLine("AND ISNULL(t1.LogoutIP,'')<>''");
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
