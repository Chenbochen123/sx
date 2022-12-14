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
    /// <summary>
    /// PmtEquipAbilityManager 实现类
    /// 孙本强 @ 2013-04-03 11:54:47
    /// </summary>
    /// <remarks></remarks>
    public class PmtEquipAbilityManager : BaseManager<PmtEquipAbility>, IPmtEquipAbilityManager
    {
		#region 属性注入与构造方法

        /// <summary>
        /// 数据库操作类
        /// 孙本强 @ 2013-04-03 11:54:47
        /// </summary>
        private IPmtEquipAbilityService service;

        /// <summary>
        /// 类 PmtEquipAbilityManager 构造函数
        /// 孙本强 @ 2013-04-03 11:54:48
        /// </summary>
        /// <remarks></remarks>
        public PmtEquipAbilityManager()
        {
            this.service = new PmtEquipAbilityService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtEquipAbilityManager 构造函数
        /// 孙本强 @ 2013-04-03 11:54:48
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
		public PmtEquipAbilityManager(string connectStringKey)
        {
			this.service = new PmtEquipAbilityService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtEquipAbilityManager 构造函数
        /// 孙本强 @ 2013-04-03 11:54:48
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtEquipAbilityManager(NBear.Data.Gateway way)
        {
			this.service = new PmtEquipAbilityService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region IPmtEquipAbilityManager 成员

        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:54:48
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtEquipAbilityService.QueryParams
        {
        }
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:54:48
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtEquipAbility> GetTablePageDataBySql(Mesnac.Data.Implements.PmtEquipAbilityService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        /// <summary>
        /// 执行存储过程进行汇总
        /// 孙本强 @ 2013-04-03 11:49:37
        /// </summary>
        /// <param name="startDate">汇总开始日期</param>
        /// <param name="endDate">汇总结束日期</param>
        /// <param name="shiftID">当班次为“全部”时，传入参数为“0”</param>
        /// <remarks></remarks>
        public void ExecProcEquipAbility(string startDate, string endDate, string shiftID , string workbarcode)
        {
            this.service.ExecProcEquipAbility(startDate, endDate, shiftID, workbarcode);
        }

        #endregion
    }
}
