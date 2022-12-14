using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class PptClassManager : BaseManager<PptClass>, IPptClassManager
    {
		#region 属性注入与构造方法
		
        private IPptClassService service;

        public PptClassManager()
        {
            this.service = new PptClassService();
            base.BaseService = this.service;
        }

		public PptClassManager(string connectStringKey)
        {
			this.service = new PptClassService(connectStringKey);
            base.BaseService = this.service;
        }

        public PptClassManager(NBear.Data.Gateway way)
        {
			this.service = new PptClassService(way);
            base.BaseService = this.service;
        }

        #endregion


        #region 查询条件类定义
        public class QueryParams : PptClassService.QueryParams
        {
        }
        #endregion
        /// <summary>
        /// 根据班组名称查询班组信息
        /// 孙宜建
        /// 2013-1-25
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PptClass GetClassByName(string name)
        {
            return service.GetClassByName(name);
        }

        /// <summary>
        /// 分页方式提取班组列表信息
        /// yuany
        /// 2013年1月29日
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptClass> GetTablePageDataBySql(Mesnac.Data.Implements.PptClassService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
