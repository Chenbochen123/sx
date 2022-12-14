using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PmtEquipJarStoreLogManager : BaseManager<PmtEquipJarStoreLog>, IPmtEquipJarStoreLogManager
    {
		#region 属性注入与构造方法
		
        private IPmtEquipJarStoreLogService service;

        public PmtEquipJarStoreLogManager()
        {
            this.service = new PmtEquipJarStoreLogService();
            base.BaseService = this.service;
        }

		public PmtEquipJarStoreLogManager(string connectStringKey)
        {
			this.service = new PmtEquipJarStoreLogService(connectStringKey);
            base.BaseService = this.service;
        }

        public PmtEquipJarStoreLogManager(NBear.Data.Gateway way)
        {
			this.service = new PmtEquipJarStoreLogService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:58:06
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtEquipJarStoreLogService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:58:06
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtEquipJarStoreLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
    }
}
