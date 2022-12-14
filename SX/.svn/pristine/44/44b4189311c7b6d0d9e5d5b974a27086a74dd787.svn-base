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
    /// PmtActionService 实现类
    /// 孙本强 @ 2013-04-03 13:03:22
    /// </summary>
    /// <remarks></remarks>
    public class PmtActionService : BaseService<PmtAction>, IPmtActionService
    {
		#region 构造方法

        /// <summary>
        /// 类 PmtActionService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:22
        /// </summary>
        /// <remarks></remarks>
        public PmtActionService() : base(){ }

        /// <summary>
        /// 类 PmtActionService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:22
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtActionService(string connectStringKey) : base(connectStringKey){ }

        /// <summary>
        /// 类 PmtActionService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:22
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtActionService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 13:58:25
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 孙本强 @ 2013-04-03 13:03:23
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<PmtAction>();
                Code = null;
                Address = null;
                ShowName = null;
                Remark = null;
                DeleteFlag = null;
            }
            /// <summary>
            /// 页面查询条数条件
            /// 孙本强 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtAction> PageParams { get; set; }
            /// <summary>
            /// 编号
            /// 孙本强 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The code.</value>
            /// <remarks></remarks>
            public string Code { get; set; }
            /// <summary>
            /// 地址
            /// 孙本强 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The address.</value>
            /// <remarks></remarks>
            public string Address { get; set; }
            /// <summary>
            /// 显示名称
            /// 孙本强 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The name of the show.</value>
            /// <remarks></remarks>
            public string ShowName { get; set; }
            /// <summary>
            /// 备注
            /// 孙本强 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string Remark { get; set; }
            /// <summary>
            /// 删除标志
            /// 孙本强 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
        }
        #endregion
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 13:03:23
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtAction> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtAction> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT * FROM PmtAction t1
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
