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
		#region 构造方法

        public PmtOpenActionModelMainService() : base(){ }

        public PmtOpenActionModelMainService(string connectStringKey) : base(connectStringKey){ }

        public PmtOpenActionModelMainService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 袁洋 @ 2013-04-03 13:58:25
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 袁洋 @ 2013-04-03 13:03:23
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
            /// 页面查询条数条件
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtOpenActionModelMain> PageParams { get; set; }
            /// <summary>
            /// 模板名称
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The code.</value>
            /// <remarks></remarks>
            public string ModelName { get; set; }
            /// <summary>
            /// 模板创建日期
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The address.</value>
            /// <remarks></remarks>
            public string ModelCreateDate { get; set; }
            /// <summary>
            /// 模板创建人
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The name of the show.</value>
            /// <remarks></remarks>
            public string ModelCreateUser { get; set; }
            /// <summary>
            /// 模板有效日期
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string ModelValidDate { get; set; }
            /// <summary>
            /// 模板描述
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The remark.</value>
            /// <remarks></remarks>
            public string ModelDetail { get; set; }
            /// <summary>
            /// 删除标志
            /// 袁洋 @ 2013-04-03 13:03:23
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
        }
        #endregion
        /// <summary>
        /// 获取分页数据集
        /// 袁洋 @ 2013-04-03 13:03:23
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
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
