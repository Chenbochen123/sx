using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    /// <summary>
    /// SysUserActionService 实现类
    /// 孙本强 @ 2013-04-03 12:53:10
    /// </summary>
    /// <remarks></remarks>
    public class SysUserActionService : BaseService<SysUserAction>, ISysUserActionService
    {
        #region 构造方法

        /// <summary>
        /// 类 SysUserActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:10
        /// </summary>
        /// <remarks></remarks>
        public SysUserActionService() : base() { }

        /// <summary>
        /// 类 SysUserActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:10
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysUserActionService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 SysUserActionService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:11
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysUserActionService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:53:11
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 孙本强 @ 2013-04-03 12:53:11
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<SysUserAction>();
                PageActionID = null;
            }
            /// <summary>
            /// Gets or sets the page params.
            /// 孙本强 @ 2013-04-03 12:53:11
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<SysUserAction> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the page action ID.
            /// 孙本强 @ 2013-04-03 12:53:11
            /// </summary>
            /// <value>The page action ID.</value>
            /// <remarks></remarks>
            public string PageActionID { get; set; }
        }
        #endregion


        /// <summary>
        /// 用户权限拷贝
        /// 孙本强 @ 2013-04-03 12:50:11
        /// 孙本强 @ 2013-04-03 12:53:11
        /// </summary>
        /// <param name="sourceUserID">The source user ID.</param>
        /// <param name="targetUserID">The target user ID.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int CopyForm(string sourceUserID, string targetUserID)
        {
            string sql = @"INSERT INTO SysUserAction(UserCode,ActionID) SELECT '" + targetUserID
                  + "',ActionID FROM SysUserAction WHERE UserCode='" + sourceUserID + "'";
            NBear.Data.CustomSqlSection css = this.GetBySql(sql);
            return css.ExecuteNonQuery();
        }

        /// <summary>
        /// 通过角色设置用户权限
        /// 孙本强 @ 2013-04-03 12:50:11
        /// 孙本强 @ 2013-04-03 12:53:11
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int SetUserActionByRole(string userid)
        {
            string sql = @"INSERT INTO SysUserAction(UserCode,ActionID) 
                    SELECT DISTINCT t1.UserCode,t2.ActionID FROM dbo.SysUserRole t1 
                    INNER JOIN dbo.SysRoleAction t2 ON t1.RoleID=t2.RoleID
                    WHERE t1.UserCode='" + userid + "'";
            NBear.Data.CustomSqlSection css = this.GetBySql(sql);
            return css.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:53:11
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysUserAction> GetUserTablePageDataByAction(QueryParams queryParams)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t2.* FROM dbo.SysUserAction t1 
            INNER JOIN dbo.BasUser t2 ON t1.UserCode = t2.WorkBarcode AND t2.DeleteFlag=0");
            if (!string.IsNullOrWhiteSpace(queryParams.PageActionID))
            {
                sqlstr.AppendLine("AND t1.ActionID=" + queryParams.PageActionID);
            }
            queryParams.PageParams.QueryStr = sqlstr.ToString();
            return this.GetPageDataBySql(queryParams.PageParams);
        }
    }
}
