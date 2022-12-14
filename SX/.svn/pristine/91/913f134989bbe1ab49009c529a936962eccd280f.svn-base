using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    /// <summary>
    /// SysPageActionManager 实现类
    /// 孙本强 @ 2013-04-03 11:33:41
    /// </summary>
    /// <remarks></remarks>
    public class SysPageActionManager : BaseManager<SysPageAction>, ISysPageActionManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 数据操作类
        /// 孙本强 @ 2013-04-03 11:33:41
        /// </summary>
        private ISysPageActionService service;

        /// <summary>
        /// 类 SysPageActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:33:41
        /// </summary>
        /// <remarks></remarks>
        public SysPageActionManager()
        {
            this.service = new SysPageActionService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysPageActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:33:42
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public SysPageActionManager(string connectStringKey)
        {
			this.service = new SysPageActionService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysPageActionManager 构造函数
        /// 孙本强 @ 2013-04-03 11:33:42
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysPageActionManager(NBear.Data.Gateway way)
        {
			this.service = new SysPageActionService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ISysPageActionManager 成员函数
        /// <summary>
        /// 获取所有的页面操作信息
        /// 孙本强 @ 2013-04-03 11:33:42
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetAllPageMenuAction()
        {
            return this.service.GetAllPageMenuAction();
        }

        /// <summary>
        /// 获取所有的页面操作信息
        /// 孙本强 @ 2013-04-03 11:33:42
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetAllPageMenuAction(string powerName)
        {
            return this.service.GetAllPageMenuAction(powerName);
        }
        /// <summary>
        /// 获取当前页面用户的操作信息
        /// 孙本强 @ 2013-04-03 11:33:42
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<SysPageAction> GetUserPageActionList(string url, string userid)
        {
            return this.service.GetUserPageActionList(url, userid);
        }

        #endregion
    }
}
