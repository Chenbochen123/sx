using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasStoragePlaceService : BaseService<BasStoragePlace>, IBasStoragePlaceService
    {
		#region 构造方法

        public BasStoragePlaceService() : base(){ }

        public BasStoragePlaceService(string connectStringKey) : base(connectStringKey){ }

        public BasStoragePlaceService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string storageID { get; set; }
            public string storagePlaceName { get; set; }
            public string defaultFlag { get; set; }
            public string storageType { get; set; }
            public string lockFlag { get; set; }
            public string cancelFlag { get; set; }
            public string deleteFlag { get; set; }
            public PageResult<BasStoragePlace> pageParams { get; set; }
        }

        public PageResult<BasStoragePlace> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasStoragePlace> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT A.ObjID, A.StorageID, B.StorageName, StoragePlaceID, StoragePlaceName, AutoGenFlag, CONVERT(BIT, DefaultFlag) DefaultFlag, CONVERT(BIT, A.LockFlag) LockFlag, A.CancelFlag, A.Remark
                                FROM BasStoragePlace A
                                LEFT JOIN BasStorage B ON A.StorageID = B.StorageID WHERE 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND B.StorageID in (" + queryParams.storageID + ")");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceName))
            {
                sqlstr.AppendLine(" AND StoragePlaceName LIKE '%" + queryParams.storagePlaceName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.defaultFlag))
            {
                sqlstr.AppendLine(" AND DefaultFlag = '" + queryParams.defaultFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageType))
            {
                sqlstr.AppendLine(" AND B.StorageType = '" + queryParams.storageType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.lockFlag))
            {
                sqlstr.AppendLine(" AND A.LockFlag = '" + queryParams.lockFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.cancelFlag))
            {
                sqlstr.AppendLine(" AND A.CancelFlag = '" + queryParams.cancelFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
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

        public bool UpdateLocked(string IDS)
        {
            string sql = @"update BasStoragePlace set LockFlag = '1' where ObjID in (" + IDS + ")";
            if (!string.IsNullOrEmpty(IDS))
            {
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public bool UpdateUsingByStorageID(string IDS)
        {
            string sql = @"with a as(
                            select ObjID, StorageID, StorageName, StorageHigherLevel from BasStorage where ObjID in (" + IDS + @")
                            union all
                            select b.ObjID, b.StorageID, b.StorageName, b.StorageHigherLevel from BasStorage b, a where b.StorageHigherLevel = a.StorageID) 
                            update BasstoragePlace set LockFlag = '1' where StorageID in (select StorageID from a)";
            if (!string.IsNullOrEmpty(IDS))
            {
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public void SetDefaultStoragePlace(string ObjID, string StorageID)
        {
            this.GetBySql("update BasStoragePlace set DefaultFlag = '0' where DefaultFlag = '1' and StorageID = '" + StorageID + "'").ExecuteNonQuery();
            this.GetBySql("update BasStoragePlace set DefaultFlag = '1' where ObjID = '" + ObjID + "'").ExecuteNonQuery();
        }

        public void SetAutoGenDefault(string ObjID, string StorageID)
        {
            this.GetBySql("update BasStoragePlace set DefaultFlag = '1' where AutoGenFlag = '1' and StorageID = '" + StorageID + "'").ExecuteNonQuery();
            this.GetBySql("update BasStoragePlace set DefaultFlag = '0' where ObjID = '" + ObjID + "'").ExecuteNonQuery();
        }

        /// <summary>
        /// 根据库房编号生成库位编号
        /// </summary>
        /// <param name="StorageID">库房编号</param>
        /// <returns>新生成的库位编号</returns>
        public DataSet GetStoragePlaceID(string StorageID)
        {
            DataSet ds = this.GetBySql("select ObjID from BasStoragePlace where StorageID = '" + StorageID + "'").ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return this.GetBySql("select StorageID + RIGHT('000' + CONVERT(VARCHAR, CONVERT(INT, RIGHT(StoragePlaceID, 3)) + 1), 3) from BasStoragePlace where ObjID = (select max(ObjID) from BasStoragePlace where DeleteFlag = '0' and StorageID = '" + StorageID + "')").ToDataSet();
            else
                return this.GetBySql("SELECT '" + StorageID + "' + '001'").ToDataSet();
        }

        public string GetStoragePlaceName(string StorageID, string StoragePlaceID)
        {
            DataSet ds = this.GetBySql("select StoragePlaceName from BasStoragePlace where StorageID = '" + StorageID + "' and StoragePlaceID = '" + StoragePlaceID + "'").ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }

        public string GetStorageType(string StoragePlaceID, string StorageID)
        {
            DataSet ds;
            if (!string.IsNullOrEmpty(StoragePlaceID))
                ds = this.GetBySql("select B.StorageType from BasStoragePlace A left join BasStorage B on A.StorageID = B.StorageID where StoragePlaceID = '" + StoragePlaceID + "'").ToDataSet();
            else
                ds = this.GetBySql("select StorageType from BasStorage where StorageID = '" + StorageID + "'").ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();
            else
                return "";
        }

        public DataSet GetStoragePlaceInfo(string storageID)
        {
            string sql = "select '0' StoragePlaceID, '----全部----' StoragePlaceName union select StoragePlaceID, StoragePlaceName from BasStoragePlace where StorageID = '" + storageID + "'";

            return this.GetBySql(sql).ToDataSet();
        }
    }
}
