using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    /// <summary>
    /// 设备料仓信息
    /// 孙本强 @ 2013-04-03 13:03:03
    /// </summary>
    /// <remarks></remarks>
    public class PmtEquipJarStoreService : BaseService<PmtEquipJarStore>, IPmtEquipJarStoreService
    {
        #region 构造方法

        /// <summary>
        /// 类 PmtEquipJarStoreService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:03
        /// </summary>
        /// <remarks></remarks>
        public PmtEquipJarStoreService() : base() { }

        /// <summary>
        /// 类 PmtEquipJarStoreService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:03
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtEquipJarStoreService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// 类 PmtEquipJarStoreService 构造函数
        /// 孙本强 @ 2013-04-03 13:03:03
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtEquipJarStoreService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 13:58:41
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 孙本强 @ 2013-04-03 13:03:03
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<PmtEquipJarStore>();
                ObjID = null;
                EquipCode = null;
                JarType = null;
                DeleteFlag = null;
            }
            /// <summary>
            /// 唯一编号
            /// 孙本强 @ 2013-04-03 13:03:03
            /// </summary>
            /// <value>The obj ID.</value>
            /// <remarks></remarks>
            public string ObjID { get; set; }
            /// <summary>
            /// 设备编号
            /// 孙本强 @ 2013-04-03 13:03:03
            /// </summary>
            /// <value>The equip code.</value>
            /// <remarks></remarks>
            public string EquipCode { get; set; }
            /// <summary>
            /// 料仓类型
            /// 孙本强 @ 2013-04-03 13:03:03
            /// </summary>
            /// <value>The type of the jar.</value>
            /// <remarks></remarks>
            public string JarType { get; set; }
            /// <summary>
            /// 删除标志
            /// 孙本强 @ 2013-04-03 13:03:03
            /// </summary>
            /// <value>The delete flag.</value>
            /// <remarks></remarks>
            public string DeleteFlag { get; set; }
            /// <summary>
            /// 页面查询条数条件
            /// 孙本强 @ 2013-04-03 13:03:03
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtEquipJarStore> PageParams { get; set; }
        }
        #region 料仓树

        /// <summary>
        /// 数据转化为字符串
        /// 孙本强 @ 2013-04-03 13:03:04
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string ValueToString(object value)
        {
            if ((value == DBNull.Value) || (value == null))
            {
                return string.Empty;
            }
            return value.ToString();
        }
        /// <summary>
        /// 数据转化为数字
        /// 孙本强 @ 2013-04-03 13:03:04
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? ValueToInt(object value)
        {
            if ((value == DBNull.Value) || (value == null))
            {
                return null;
            }
            try
            {
                return Convert.ToInt32(value.ToString());
            }
            catch
            {
            }
            return null;
        }
        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 12:58:57
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasEquipType> GetEquipTypeHaveJar()
        {
            string sqlstr = @"SELECT DISTINCT t3.* FROM dbo.PmtEquipJarCount t1
                    INNER JOIN dbo.BasEquip t2 ON t1.EquipCode = t2.EquipCode
                    INNER JOIN dbo.BasEquipType t3 ON t2.EquipType=t3.ObjID
                    WHERE t1.JarCount>0 AND t1.DeleteFlag='0' ORDER BY t3.ObjID";
           return this.GetBySql(sqlstr.ToString()).ToArrayList<BasEquipType>();
        }

        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 12:58:57
        /// </summary>
        /// <param name="typeid">The typeid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasEquip> GetEquipHaveJar(string typeid)
        {
            string sqlstr = @"SELECT DISTINCT t2.* FROM dbo.PmtEquipJarCount t1
                    INNER JOIN dbo.BasEquip t2 ON t1.EquipCode = t2.EquipCode
                    WHERE t1.JarCount>0 AND t1.DeleteFlag='0' AND t2.EquipType='" + typeid + "' ORDER BY t2.EquipCode";
            return this.GetBySql(sqlstr.ToString()).ToArrayList<BasEquip>();
        }

        /// <summary>
        /// 获取设备具有的料仓罐
        /// 孙本强 @ 2013-04-03 12:58:57
        /// </summary>
        /// <param name="equipCode">The equip code.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<SysCode> GetEquipJarTypeHaveJar(string equipCode)
        {
            string sqlstr = @"SELECT DISTINCT t3.* FROM dbo.BasEquip t1 
            INNER JOIN dbo.PmtEquipJarStore t2 ON t1.EquipCode=t2.EquipCode
            INNER JOIN dbo.SysCode t3 ON t2.JarType=t3.ItemCode AND t3.TypeID='EquipJar'
            WHERE t1.EquipCode='" + equipCode + "'  ORDER BY t3.ItemCode";
            return this.GetBySql(sqlstr.ToString()).ToArrayList<SysCode>();
        }

        #endregion

        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 13:03:04
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtEquipJarStore> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtEquipJarStore> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.ObjID,t1.Priority,t1.JarNum,
                                t2.StoragePlaceID,t2.StoragePlaceName,
                                t3.StorageID,t3.StorageName,
                                t5.MaterialCode,t5.MaterialName,
                                t6.ObjID as MinorTypeID,t6.MinorTypeName, 
                                t1.WorkID,
                                t1.DeleteFlag,t1.SeqIdx FROM  dbo.PmtEquipJarStore t1
                                LEFT JOIN dbo.BasStoragePlace t2 ON t1.StoragePlaceCode=t2.StoragePlaceID
                                LEFT JOIN dbo.BasStorage t3 ON t2.StorageID=t3.StorageID
                                LEFT JOIN dbo.BasMaterial t5 ON t1.MaterialCode=t5.MaterialCode
                                LEFT JOIN dbo.BasMaterialMinorType t6 ON t5.MinorTypeID=t6.MinorTypeID AND t6.MajorID=t5.MajorTypeID
                                WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.ObjID))
            {
                sqlstr.AppendLine(" AND t1.ObjID = " + queryParams.ObjID);
            }
            if (!string.IsNullOrEmpty(queryParams.EquipCode))
            {
                sqlstr.AppendLine(" AND t1.EquipCode = '" + queryParams.EquipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.JarType))
            {
                sqlstr.AppendLine(" AND t1.JarType = '" + queryParams.JarType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.DeleteFlag))
            {
                sqlstr.AppendLine(" AND t1.DeleteFlag = " + queryParams.DeleteFlag);
            }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }
    }
}
