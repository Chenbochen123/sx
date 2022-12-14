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
    /// SysRoleService 实现类
    /// 孙本强 @ 2013-04-03 12:53:15
    /// </summary>
    /// <remarks></remarks>
    public class SysRoleService : BaseService<SysRole>, ISysRoleService
    {
        #region 构造方法

        /// <summary>
        /// 类 SysRoleService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:16
        /// </summary>
        /// <remarks></remarks>
        public SysRoleService() : base() { }

        /// <summary>
        /// 类 SysRoleService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:16
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysRoleService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 SysRoleService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:16
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysRoleService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:53:16
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 孙本强 @ 2013-04-03 12:53:16
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
            /// 孙本强 @ 2013-04-03 12:53:16
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<SysRole> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the name of the role.
            /// 孙本强 @ 2013-04-03 12:53:16
            /// </summary>
            /// <value>The name of the role.</value>
            /// <remarks></remarks>
            public string RoleName { get; set; }
            /// <summary>
            /// Gets or sets the remark.
            /// 孙本强 @ 2013-04-03 12:53:16
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string Remark { get; set; }
            /// <summary>
            /// Gets or sets the delete flag.
            /// 孙本强 @ 2013-04-03 12:53:16
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
        }
        #endregion
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:53:16
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
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
