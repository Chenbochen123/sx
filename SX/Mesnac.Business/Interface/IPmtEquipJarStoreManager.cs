using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Interface
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Business.Implements;
    /// <summary>
    /// IPmtEquipJarStoreManager 接口定义
    /// 孙本强 @ 2013-04-03 11:49:51
    /// </summary>
    /// <remarks></remarks>
    public interface IPmtEquipJarStoreManager : IBaseManager<PmtEquipJarStore>
    {
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        PageResult<PmtEquipJarStore> GetTablePageDataBySql(PmtEquipJarStoreManager.QueryParams queryParams);
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<BasEquipType> GetEquipTypeHaveJar();
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <param name="typeid">料仓类型</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<BasEquip> GetEquipHaveJar(string typeid);
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <param name="equipCode">设备编号</param>
        /// <returns></returns>
        /// <remarks></remarks>
        EntityArrayList<SysCode> GetEquipJarTypeHaveJar(string equipCode);


        /// <summary>
        /// 通过料仓数初始化表
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <param name="equipCode">The equip code.</param>
        /// <remarks></remarks>
        void IniPmtEquipJarStoreByCount(string equipCode);
        /// <summary>
        /// 通过料仓数初始化表
        /// 孙本强 @ 2013-04-03 11:49:52
        /// </summary>
        /// <param name="equip">The equip.</param>
        /// <remarks></remarks>
        void IniPmtEquipJarStoreByCount(BasEquip equip);
        /// <summary>
        /// 通过料仓数初始化表
        /// 孙本强 @ 2013-04-03 11:49:52
        /// </summary>
        /// <param name="ObjID">The obj ID.</param>
        /// <remarks></remarks>
        void IniPmtEquipJarStoreByCount(int ObjID);
    }
}
