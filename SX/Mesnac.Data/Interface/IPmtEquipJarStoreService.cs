using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using NBear.Common;
    using Mesnac.Data.Implements;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    /// <summary>
    /// IPmtEquipJarStoreService 接口定义
    /// 孙本强 @ 2013-04-03 12:58:57
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtEquipJarStoreService : IBaseService<PmtEquipJarStore>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:58:57
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtEquipJarStore> GetTablePageDataBySql(PmtEquipJarStoreService.QueryParams queryParams);

        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 12:58:57
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<BasEquipType> GetEquipTypeHaveJar();
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 12:58:57
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<BasEquip> GetEquipHaveJar(string typeid);
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 12:58:57
        /// </summary>
        /// <param name="equipCode">The equip code.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<SysCode> GetEquipJarTypeHaveJar(string equipCode);
    }
}
