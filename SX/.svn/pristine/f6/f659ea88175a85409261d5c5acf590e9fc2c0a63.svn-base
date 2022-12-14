using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    using Mesnac.Data.Interface;
    using Mesnac.Entity;
    using NBear.Common;
    /// <summary>
    /// PmtEquipJarStoreManager 实现类
    /// 孙本强 @ 2013-04-03 11:58:06
    /// </summary>
    /// <remarks></remarks>
    public class PmtEquipJarStoreManager : BaseManager<PmtEquipJarStore>, IPmtEquipJarStoreManager
    {
        #region 属性注入与构造方法

        /// <summary>
        /// 
        /// 孙本强 @ 2013-04-03 11:58:06
        /// </summary>
        private IPmtEquipJarStoreService service;

        /// <summary>
        /// 类 PmtEquipJarStoreManager 构造函数
        /// 孙本强 @ 2013-04-03 11:58:06
        /// </summary>
        /// <remarks></remarks>
        public PmtEquipJarStoreManager()
        {
            this.service = new PmtEquipJarStoreService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtEquipJarStoreManager 构造函数
        /// 孙本强 @ 2013-04-03 11:58:06
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtEquipJarStoreManager(string connectStringKey)
        {
            this.service = new PmtEquipJarStoreService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtEquipJarStoreManager 构造函数
        /// 孙本强 @ 2013-04-03 11:58:06
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtEquipJarStoreManager(NBear.Data.Gateway way)
        {
            this.service = new PmtEquipJarStoreService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 11:58:06
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtEquipJarStoreService.QueryParams
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
        public PageResult<PmtEquipJarStore> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        #region 料仓树
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasEquipType> GetEquipTypeHaveJar()
        {
            return this.service.GetEquipTypeHaveJar();
        }
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <param name="typeid">料仓类型</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasEquip> GetEquipHaveJar(string typeid)
        {
            return this.service.GetEquipHaveJar(typeid);
        }
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <param name="equipCode">设备编号</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<SysCode> GetEquipJarTypeHaveJar(string equipCode)
        {
            return this.service.GetEquipJarTypeHaveJar(equipCode);
        }

        #endregion
        #region 料仓初始化
        /// <summary>
        /// 料仓罐数量类
        /// 孙本强 @ 2013-04-03 11:58:07
        /// </summary>
        IPmtEquipJarCountManager pmtEquipJarCountManager = new PmtEquipJarCountManager();
        /// <summary>
        /// 通过料仓数初始化表
        /// 孙本强 @ 2013-04-03 11:49:51
        /// </summary>
        /// <param name="equipCode">The equip code.</param>
        /// <remarks></remarks>
        public void IniPmtEquipJarStoreByCount(string equipCode)
        {
            WhereClip where = new WhereClip();
            where.And(PmtEquipJarCount._.EquipCode == equipCode);
            EntityArrayList<PmtEquipJarCount> lst = pmtEquipJarCountManager.GetListByWhere(where);
            foreach (PmtEquipJarCount m in lst)
            {
                #region 获取料仓信息
                where = new WhereClip();
                where.And(PmtEquipJarStore._.EquipCode == m.EquipCode);
                where.And(PmtEquipJarStore._.JarType == m.JarType);
                EntityArrayList<PmtEquipJarStore> jarlst = this.GetListByWhere(where);
                int MaxJarNum = 0;
                foreach (PmtEquipJarStore jar in jarlst)
                {
                    if (MaxJarNum < jar.JarNum)
                    {
                        MaxJarNum = jar.JarNum;
                    }
                }
                #endregion
                #region 初始化料仓
                int count = jarlst.Count;
                if ((m.JarCount != null) && (count < m.JarCount))
                {
                    count = (int)m.JarCount - count;
                    List<PmtEquipJarStore> inserts = new List<PmtEquipJarStore>();
                    for (int i = 0; i < count; i++)
                    {
                        PmtEquipJarStore jar = new PmtEquipJarStore();
                        jar.EquipCode = m.EquipCode;
                        jar.JarType = m.JarType;
                        MaxJarNum++;
                        jar.JarNum = MaxJarNum;
                        jar.Priority = 0;
                        jar.DeleteFlag = "0";
                        inserts.Add(jar);
                    }
                    this.BatchInsert(inserts);
                }
                #endregion
            }
        }
        /// <summary>
        /// 通过料仓数初始化表
        /// 孙本强 @ 2013-04-03 11:49:52
        /// </summary>
        /// <param name="equip">The equip.</param>
        /// <remarks></remarks>
        public void IniPmtEquipJarStoreByCount(BasEquip equip)
        {
            IniPmtEquipJarStoreByCount(equip.EquipCode);
        }
        /// <summary>
        /// 通过料仓数初始化表
        /// 孙本强 @ 2013-04-03 11:49:52
        /// </summary>
        /// <param name="ObjID">The obj ID.</param>
        /// <remarks></remarks>
        public void IniPmtEquipJarStoreByCount(int ObjID)
        {
            EntityArrayList<BasEquip> lst = new BasEquipManager().GetListByWhere(BasEquip._.ObjID == ObjID);
            foreach (BasEquip m in lst)
            {
                IniPmtEquipJarStoreByCount(m);
            }
        }
        #endregion
    }
}
