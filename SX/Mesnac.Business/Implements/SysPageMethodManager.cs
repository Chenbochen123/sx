using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Business.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;
    using Mesnac.Util.Cryptography;
    using NBear.Common;
    using Mesnac.Data.Components;
    /// <summary>
    /// SysPageMethodManager 实现类
    /// 孙本强 @ 2013-04-03 11:35:44
    /// </summary>
    /// <remarks></remarks>
    public class SysPageMethodManager : BaseManager<SysPageMethod>, ISysPageMethodManager
    {
        #region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:35:45
        /// </summary>
        private ISysPageMethodService service;

        /// <summary>
        /// 类 SysPageMethodManager 构造函数
        /// 孙本强 @ 2013-04-03 11:35:45
        /// </summary>
        /// <remarks></remarks>
        public SysPageMethodManager()
        {
            this.service = new SysPageMethodService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysPageMethodManager 构造函数
        /// 孙本强 @ 2013-04-03 11:35:45
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public SysPageMethodManager(string connectStringKey)
        {
            this.service = new SysPageMethodService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 SysPageMethodManager 构造函数
        /// 孙本强 @ 2013-04-03 11:35:45
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public SysPageMethodManager(NBear.Data.Gateway way)
        {
            this.service = new SysPageMethodService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:35:45
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : SysPageMethodService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 获取当前页面操作方法的ID
        /// 孙本强 @ 2013-04-03 11:35:45
        /// </summary>
        /// <param name="sysPageMethod">The sys page method.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int GetPageMethodID(SysPageMethod sysPageMethod)
        {
            WhereClip where = new WhereClip();
            where.And(SysPageMethod._.PageID == sysPageMethod.PageID);
            where.And(SysPageMethod._.MethodName == sysPageMethod.MethodName);
            EntityArrayList<SysPageMethod> lst = this.GetListByWhere(where);
            if (lst.Count > 0)
            {
                return lst[0].ObjID;
            }
            return 0;
        }
        /// <summary>
        /// 添加当前页面操作方法
        /// 孙本强 @ 2013-04-03 11:35:45
        /// </summary>
        /// <param name="sysPageMethod">The sys page method.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int Append(SysPageMethod sysPageMethod)
        {
            int Result = GetPageMethodID(sysPageMethod);
            if (Result <= 0)
            {
                sysPageMethod.ObjID = (int)this.GetMaxValueByProperty(SysPageMethod._.ObjID) + 1;
                sysPageMethod.SeqIdx = sysPageMethod.ObjID;
                sysPageMethod.ShowName = "未知操作";
                this.Insert(sysPageMethod);
                Result = GetPageMethodID(sysPageMethod);
            }
            return Result;
        }

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:35:45
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<SysPageMethod> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

    }
}
