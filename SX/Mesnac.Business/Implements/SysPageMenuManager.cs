
namespace Mesnac.Business.Implements
{
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;
    using NBear.Common;
    /// <summary>
    /// SysPageMenuManager 实现类
    /// 孙本强 @ 2013-04-03 11:35:01
    /// </summary>
    /// <remarks></remarks>
    public class SysPageMenuManager : BaseManager<SysPageMenu>, ISysPageMenuManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:35:01
        /// </summary>
        private ISysPageMenuService service;

        /// <summary>
        /// 类 SysPageMenuManager 构造函数
        /// 孙本强 @ 2013-04-03 11:35:01
        /// </summary>
        /// <remarks></remarks>
        public SysPageMenuManager()
        {
            this.service = new SysPageMenuService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysPageMenuManager 构造函数
        /// 孙本强 @ 2013-04-03 11:35:01
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public SysPageMenuManager(string connectStringKey)
        {
			this.service = new SysPageMenuService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysPageMenuManager 构造函数
        /// 孙本强 @ 2013-04-03 11:35:01
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysPageMenuManager(NBear.Data.Gateway way)
        {
			this.service = new SysPageMenuService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:35:01
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : SysPageMenuService.QueryParams
        {
        }
        #endregion

        #region ISysPageMenuManager 成员函数
        /// <summary>
        /// 获取用户操作的菜单列表
        /// 孙本强 @ 2013-04-03 11:35:01
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="parid">上次菜单ID</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<SysPageMenu> GetUserMenuPageList(string userid, string parid)
        {
            return this.service.GetUserMenuPageList(userid, parid);
        }

        /// <summary>
        /// 校验页面权限
        /// 孙本强 @ 2013-04-03 11:35:01
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool PagePermission(string userid, string url)
        {
            return this.service.PagePermission(userid, url);
        }


        /// <summary>
        /// 获取当前页面的ID
        /// 孙本强 @ 2013-04-03 11:35:02
        /// </summary>
        /// <param name="pageurl">The pageurl.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetPageID(string pageurl)
        {
            return this.service.GetPageID(pageurl);
        }


        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:35:02
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysPageMenu> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        #endregion
    }
}
