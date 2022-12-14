using System;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;
    using NBear.Common;
    using NBear.Data;
    /// <summary>
    /// SysPageMenuService 实现类
    /// 孙本强 @ 2013-04-03 12:53:29
    /// </summary>
    /// <remarks></remarks>
    public class SysPageMenuService : BaseService<SysPageMenu>, ISysPageMenuService
    {
        #region 构造方法

        /// <summary>
        /// 类 SysPageMenuService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:29
        /// </summary>
        /// <remarks></remarks>
        public SysPageMenuService() : base() { }

        /// <summary>
        /// 类 SysPageMenuService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:30
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysPageMenuService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 SysPageMenuService 构造函数
        /// 孙本强 @ 2013-04-03 12:53:30
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysPageMenuService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:53:30
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 孙本强 @ 2013-04-03 12:53:30
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<SysPageMenu>();
                PageName = null;
            }
            /// <summary>
            /// Gets or sets the page params.
            /// 孙本强 @ 2013-04-03 12:53:30
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<SysPageMenu> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the name of the page.
            /// 孙本强 @ 2013-04-03 12:53:30
            /// </summary>
            /// <value>The name of the page.</value>
            /// <remarks></remarks>
            public string PageName { get; set; }
        }
        #endregion

        #region ISysPageMenuService 成员函数
        /// <summary>
        /// 获取用户操作的菜单列表
        /// 孙本强 @ 2013-04-03 12:51:25
        /// 孙本强 @ 2013-04-03 12:53:30
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="parid">The parid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<SysPageMenu> GetUserMenuPageList(string userid, string parid)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcGetUserMenuPageList");
            sps.AddInputParameter("@UserID", this.TypeToDbType(userid.GetType()), userid);
            sps.AddInputParameter("@MenuLevel", this.TypeToDbType(parid.GetType()), parid);
            return sps.ToArrayList<SysPageMenu>();
        }
        /// <summary>
        /// 判断用户是否存在操作某个页面的权限
        /// 孙本强 @ 2013-04-03 12:51:25
        /// 孙本强 @ 2013-04-03 12:53:30
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="pageurl">The pageurl.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool PagePermission(string userid, string pageurl)
        {
            bool Result = true;
            string sql = String.Empty;
            sql = @"SELECT COUNT(*) as a_count FROM dbo.SysPageMenu t1
                        INNER JOIN dbo.SysPageAction t2 ON t1.ObjID = t2.PageMenuID AND t2.DeleteFlag=0
                        WHERE t1.DeleteFlag=0 AND UPPER(t1.PageUrl)=@PageUrl";
            NBear.Data.CustomSqlSection css = this.GetBySql(sql);
            css.AddInputParameter("@PageUrl", this.TypeToDbType(pageurl.GetType()), pageurl.ToUpper());
            if (Convert.ToInt32(css.ToScalar().ToString()) > 0)
            {
                sql = @"SELECT COUNT(*) as a_count FROM dbo.SysUserAllAction  t1
                        INNER JOIN dbo.SysPageAction t2 ON t1.ActionID = t2.ObjID AND t2.DeleteFlag=0
                        INNER JOIN dbo.SysPageMenu t3 ON t2.PageMenuID=t3.ObjID AND t3.DeleteFlag=0
                        WHERE t1.UserCode=@UserID AND UPPER(t3.PageUrl)=@PageUrl";
                css = this.GetBySql(sql);
                css.AddInputParameter("@UserID", this.TypeToDbType(userid.GetType()), userid);
                css.AddInputParameter("@PageUrl", this.TypeToDbType(pageurl.GetType()), pageurl.ToUpper());
                Result = (Convert.ToInt32(css.ToScalar().ToString()) > 0);
            }
            return Result;
        }
        /// <summary>
        /// Gets the page ID.
        /// 孙本强 @ 2013-04-03 12:53:30
        /// </summary>
        /// <param name="sys_MenuPage">The sys_ menu page.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int GetPageID(SysPageMenu sys_MenuPage)
        {
            string sql = String.Empty;
            sql = @"SELECT ObjID FROM dbo.SysPageMenu WHERE UPPER(PageUrl)=@PageUrl";
            NBear.Data.CustomSqlSection css = this.GetBySql(sql);
            css.AddInputParameter("@PageUrl", this.TypeToDbType(sys_MenuPage.PageUrl.GetType()), sys_MenuPage.PageUrl.ToUpper());
            if (css.ToScalar() != null)
            {
                return Convert.ToInt32(css.ToScalar().ToString());
            }
            return 0;
        }
        /// <summary>
        /// 获取当前页面的ID
        /// 孙本强 @ 2013-04-03 12:51:25
        /// 孙本强 @ 2013-04-03 12:53:31
        /// </summary>
        /// <param name="pageurl">The pageurl.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPageID(string pageurl)
        {
            SysPageMenu sysPageMenu = new SysPageMenu();
            sysPageMenu.PageUrl = pageurl;
            int Result = GetPageID(sysPageMenu);
            if (Result <= 0)
            {
                sysPageMenu.ObjID = ((int)this.GetMaxValueByProperty(SysPageMenu._.ObjID) + 1);
                sysPageMenu.DeleteFlag = "1";
                sysPageMenu.ShowName = "未知页面";
                sysPageMenu.MenuLevel = "未知模块";
                sysPageMenu.SeqIdx = 9999;
                this.Insert(sysPageMenu);
                Result = GetPageID(sysPageMenu);
            }
            return Result;
        }


        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:53:31
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysPageMenu> GetTablePageDataBySql(QueryParams queryParams)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.* FROM dbo.SysPageMenu t1 
                                WHERE NOT EXISTS (SELECT 1 FROM dbo.SysPageMenu t2 WHERE t1.MenuLevel=SUBSTRING(t2.MenuLevel,1,LEN(t2.MenuLevel)-2)) 
                                 AND  t1.IsShow=1 ");
            if (!string.IsNullOrEmpty(queryParams.PageName))
            {
                sqlstr.AppendLine("AND t1.ShowName like '%" + queryParams.PageName + "%'");
            }
            queryParams.PageParams.QueryStr = sqlstr.ToString();
            return this.GetPageDataBySql(queryParams.PageParams);
        }

        #endregion
    }
}
