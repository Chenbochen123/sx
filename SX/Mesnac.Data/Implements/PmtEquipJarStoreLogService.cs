using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PmtEquipJarStoreLogService : BaseService<PmtEquipJarStoreLog>, IPmtEquipJarStoreLogService
    {
		#region 构造方法

        public PmtEquipJarStoreLogService() : base(){ }

        public PmtEquipJarStoreLogService(string connectStringKey) : base(connectStringKey){ }

        public PmtEquipJarStoreLogService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        /// <summary>
        /// 查询条件定义类
        /// 袁洋 @ 2014年4月2日
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// 类 QueryParams 构造函数
            /// 袁洋 @ 2014年4月2日
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<PmtEquipJarStoreLog>();

                ObjID = null;
                EquipCode = null;
                JarType = null;
                DeleteFlag = null;
            }
            public string ObjID { get; set; }
            public string BeginTime { get; set; }
            public string EndTime { get; set; }
            public string MaterialCode { get; set; }
            public string EquipCode { get; set; }
            public string StorePlaceCode { get; set; }
            public string JarType { get; set; }
            public string DeleteFlag { get; set; }
            public PageResult<PmtEquipJarStoreLog> PageParams { get; set; }
        }

        /// <summary>
        /// 获取分页数据集
        /// 袁洋 @ 2014年4月2日
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtEquipJarStoreLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtEquipJarStoreLog> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.ObjID , t1.Priority , equip.EquipName , syscode.ItemName AS JarType , equipType.EquipTypeName ,
	                                place1.StoragePlaceID AS StoragePlaceCodeBefore , place1.StoragePlaceName AS  StoragePlaceNameBefore ,
	                                place2.StoragePlaceID AS StoragePlaceCodeAfter  ,place2.StoragePlaceName AS StoragePlaceNameAfter ,
	                                store1.StorageID AS StorageIDBefore , store1.StorageName AS StorageNameBefore,
	                                store2.StorageID AS StorageIDAfter , store2.StorageName AS StorageNameAfter,
	                                mater1.MaterialCode AS MaterialCodeBefore , mater1.MaterialName AS MaterialNameBefore ,
	                                mater2.MaterialCode AS MaterialCodeAfter , mater2.MaterialName  AS MaterialNameAfter ,
	                                t1.WorkIDBefore , t1.WorkIDAfter , t1.OperDate , u.UserName as OperCode ,
	                                t1.DeleteFlag , t1.Remark , t1.Ext_1 , t1.Ext_2 , t1.Ext_3 , t1.Ext_4 FROM  dbo.PmtEquipJarStoreLog t1
	                                LEFT JOIN dbo.BasStoragePlace place1 ON t1.StoragePlaceCodeBefore=place1.StoragePlaceID
	                                LEFT JOIN dbo.BasStoragePlace place2 ON t1.StoragePlaceCodeBefore=place2.StoragePlaceID
	                                LEFT JOIN dbo.BasStorage store1 ON place1.StorageID=store1.StorageID
	                                LEFT JOIN dbo.BasStorage store2 ON place2.StorageID=store2.StorageID
	                                LEFT JOIN dbo.BasMaterial mater1 ON t1.MaterialCodeBefore=mater1.MaterialCode
	                                LEFT JOIN dbo.BasMaterial mater2 ON t1.MaterialCodeAfter=mater2.MaterialCode
                                    LEFT JOIN dbo.BasEquip equip ON t1.EquipCode=equip.EquipCode
                                    LEFT JOIN dbo.SysCode syscode ON t1.JarType=syscode.ItemCode and TypeID = 'EquipJar'
	                                LEFT JOIN dbo.BasEquipType equipType ON equip.EquipType = equipType.ObjID
	                                LEFT JOIN dbo.BasUser u ON u.WorkBarcode = t1.OperCode
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
            try
            {
                if (!string.IsNullOrEmpty(queryParams.BeginTime))
                {
                    sqlstr.AppendLine("AND t1.OperDate  >='" + Convert.ToDateTime(queryParams.BeginTime).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.EndTime))
                {
                    sqlstr.AppendLine("AND t1.OperDate  <='" + Convert.ToDateTime(queryParams.EndTime).AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
                }
            }
            catch { }
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
