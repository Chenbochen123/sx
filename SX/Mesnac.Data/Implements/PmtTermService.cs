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
    /// PmtTermService 实现类
    /// 孙本强 @ 2013-04-03 13:02:14
    /// </summary>
    /// <remarks></remarks>
    public class PmtTermService : BaseService<PmtTerm>, IPmtTermService
    {
		#region 构造方法

        /// <summary>
        /// 类 PmtTermService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:14
        /// </summary>
        /// <remarks></remarks>
        public PmtTermService() : base(){ }

        /// <summary>
        /// 类 PmtTermService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:14
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtTermService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// 类 PmtTermService 构造函数
        /// 孙本强 @ 2013-04-03 13:02:14
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtTermService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 13:02:14
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 孙本强 @ 2013-04-03 13:02:14
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
            /// 孙本强 @ 2013-04-03 13:02:14
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtTerm> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the code.
            /// 孙本强 @ 2013-04-03 13:02:14
            /// </summary>
            /// <value>The code.</value>
            /// <remarks></remarks>
            public string Code { get; set; }
            /// <summary>
            /// Gets or sets the address.
            /// 孙本强 @ 2013-04-03 13:02:14
            /// </summary>
            /// <value>The address.</value>
            /// <remarks></remarks>
            public string Address { get; set; }
            /// <summary>
            /// Gets or sets the name of the show.
            /// 孙本强 @ 2013-04-03 13:02:15
            /// </summary>
            /// <value>The name of the show.</value>
            /// <remarks></remarks>
            public string ShowName { get; set; }
            /// <summary>
            /// Gets or sets the remark.
            /// 孙本强 @ 2013-04-03 13:02:15
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string Remark { get; set; }
            /// <summary>
            /// Gets or sets the delete flag.
            /// 孙本强 @ 2013-04-03 13:02:15
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
        }
        #endregion
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 13:02:15
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
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

