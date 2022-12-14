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
    /// SysPageMethodService 实现类
    /// 孙本强 @ 2013-04-03 12:53:24
    /// </summary>
    /// <remarks></remarks>
    public class SysPageMethodService : BaseService<SysPageMethod>, ISysPageMethodService
    {
		#region 构造方法

        /// <summary>
        /// 类 SysPageMethodService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:25
        /// </summary>
        /// <remarks></remarks>
        public SysPageMethodService() : base(){ }

        /// <summary>
        /// 类 SysPageMethodService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:25
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysPageMethodService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// 类 SysPageMethodService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:25
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysPageMethodService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:53:25
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 孙本强 @ 2013-04-03 12:53:25
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<SysPageMethod>();
                MethodName = null;
            }
            /// <summary>
            /// Gets or sets the page params.
            /// 孙本强 @ 2013-04-03 12:53:25
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<SysPageMethod> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the name of the method.
            /// 孙本强 @ 2013-04-03 12:53:25
            /// </summary>
            /// <value>The name of the method.</value>
            /// <remarks></remarks>
            public string MethodName { get; set; }
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:53:25
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysPageMethod> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<SysPageMethod> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT t1.*,t2.ShowName AS PageName FROM SysPageMethod t1
                                 INNER JOIN dbo.SysPageMenu t2 ON t1.PageID=t2.ObjID
                                 WHERE      1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.MethodName))
            {
                sqlstr.AppendLine("AND t1.ShowName like '%" + queryParams.MethodName + "%'");
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
