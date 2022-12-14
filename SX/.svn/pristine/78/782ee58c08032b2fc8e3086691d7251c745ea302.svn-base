using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class BasStorageService : BaseService<BasStorage>, IBasStorageService
    {
		#region 构造方法

        public BasStorageService() : base(){ }

        public BasStorageService(string connectStringKey) : base(connectStringKey){ }

        public BasStorageService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string objID { get; set; }
            public string noStorageID { get; set; }
            public string storageID { get; set; }
            public string storageName { get; set; }
            public string storageHigherLevel { get; set; }
            public string usedFlag { get; set; }
            public string storageType { get; set; }
            public string erpCode { get; set; }
            public string deleteFlag { get; set; }
            public string cancelFlag { get; set; }
            public string lastStorageFlag { get; set; }
            public PageResult<BasStorage> pageParams { get; set; }
        }

        public PageResult<BasStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<BasStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            //sqlstr.AppendLine(@"SELECT A.ObjID, A.StorageID, A.StorageName, CONVERT(bit, A.UsedFlag) UsedFlag, A.StorageLevel, B.StorageName StorageHigherLevel, A.LastStorageFlag, A.UsedDuration, A.UsingDuration, 
            //                 case when A.DurationEndDate = '99' then convert(varchar(10), convert(datetime, convert(nvarchar(8),CONVERT(datetime, substring(A.UsingDuration, 1, 4)+substring(A.UsingDuration, 5, 2)+'01'),120)+ '01'), 120)
            //                 else convert(varchar(10), dateadd(MONTH, -1, CONVERT(datetime, SUBSTRING(A.UsingDuration, 1, 4) + '-'+SUBSTRING(A.UsingDuration, 5, 2)+'-'+RIGHT('00' + CONVERT(VARCHAR, A.DurationBeginDate), 2))), 120) end DurationBeginDate,
            //                 case when A.DurationEndDate = '99' then convert(varchar(10), Dateadd(day, -1, Dateadd(MONTH, 1, CONVERT(datetime, SUBSTRING(A.UsingDuration, 1, 4) + '-'+SUBSTRING(A.UsingDuration, 5, 2)+'-01 23:59:59'))), 120)
            //                 else convert(varchar(10), dateadd(SECOND, -1, CONVERT(datetime, SUBSTRING(A.UsingDuration, 1, 4) + '-'+SUBSTRING(A.UsingDuration, 5, 2)+'-'+RIGHT('00' + CONVERT(VARCHAR, A.DurationBeginDate), 2))), 120) end DurationEndDate,
            //                 case when A.DurationEndDate = '99' and A.UsingDuration is not null then '自然月' when A.DurationEndDate != '99' and A.UsingDuration is not null then '自定义' else '' end DurationSet, 
            //                    A.ERPCode, A.CreatePerson, A.CreateDate, A.ModifyPerson, A.ModifyDate, A.ResponsiblePerson, A.SeqIdx, CONVERT(bit, A.CancelFlag) CancelFlag, A.DeleteFlag, A.Remark
            //                FROM BasStorage A
            //                left join BasStorage B on A.StorageHigherLevel = B.StorageID and B.DeleteFlag = '0' WHERE 1 = 1");
            sqlstr.AppendLine(@"select * from BasStorage A where 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.objID))
            {
                sqlstr.AppendLine(" AND A.ObjID = " + queryParams.objID);
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID like '%" + queryParams.storageID + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageName))
            {
                sqlstr.AppendLine(" AND A.StorageName like '%" + queryParams.storageName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageHigherLevel))
            {
                sqlstr.AppendLine(" AND A.StorageHigherLevel = '" + queryParams.storageHigherLevel + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.usedFlag))
            {
                sqlstr.AppendLine(" AND A.UsedFlag = '" + queryParams.usedFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageType))
            {
                sqlstr.AppendLine(" AND A.StorageType = '" + queryParams.storageType + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.erpCode))
            {
                sqlstr.AppendLine(" AND A.ERPCode like '%" + queryParams.erpCode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.lastStorageFlag))
            {
                sqlstr.AppendLine(" AND A.LastStorageFlag = '" + queryParams.lastStorageFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            {
                sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.noStorageID))
            {
                sqlstr.AppendLine(" AND A.StorageID != '" + queryParams.noStorageID + "' AND ISNULL(A.StorageHigherLevel, '') != '" + queryParams.noStorageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.cancelFlag))
            {
                sqlstr.AppendLine(" AND A.CancelFlag = '" + queryParams.cancelFlag + "'");
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

        public bool UpdateUsing(string IDS)
        {
            string sql = @"with a as(
                            select ObjID, StorageID, StorageName, StorageHigherLevel from BasStorage where ObjID in (" + IDS + @")
                            union all
                            select b.ObjID, b.StorageID, b.StorageName, b.StorageHigherLevel from BasStorage b, a where b.StorageHigherLevel = a.StorageID) 
                            update BasStorage set UsedFlag = '1', UsedDuration = convert(varchar(6), GETDATE(), 112), UsingDuration = convert(varchar(6), GETDATE(), 112) where ObjID in (select ObjID from a)";
            if (!string.IsNullOrEmpty(IDS))
            {
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public string IsStoreIn(string StorageID)
        {
            return this.GetBySql(@"select case when DurationEndDate = '99' then CONVERT(datetime, SUBSTRING(UsingDuration, 1, 4) + '-'+SUBSTRING(UsingDuration, 5, 2)+'-'+RIGHT('00' + CONVERT(VARCHAR, DurationBeginDate), 2)) 
                                   else dateadd(MONTH, -1, CONVERT(datetime, SUBSTRING(UsingDuration, 1, 4) + '-'+SUBSTRING(UsingDuration, 5, 2)+'-'+RIGHT('00' + CONVERT(VARCHAR, DurationBeginDate), 2))) end
                                   from BasStorage where StorageID = '" + StorageID + "'").ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 根据上级库房编号生成新的下级库房编号
        /// </summary>
        /// <param name="StorageID">上级库房编号</param>
        /// <returns>新生成的下级库房编号以及库房层级</returns>
        public DataSet GetStorageID(string StorageID)
        {
            DataSet ds = new DataSet();
            //if (string.IsNullOrEmpty(StorageID))
            //{
            //    ds = this.GetBySql("select * from BasStorage").ToDataSet();
            //    if (ds.Tables[0].Rows.Count > 0)
            //        return this.GetBySql("SELECT RIGHT('000' + CONVERT(VARCHAR, CONVERT(INT, RIGHT(StorageID, 3)) + 1), 3), isnull(StorageLevel, 1) FROM BasStorage where ObjID = (select max(ObjID) from BasStorage where len(StorageHigherLevel) = 2)").ToDataSet();
            //    else
            //        return this.GetBySql("select '001', '1'").ToDataSet();
            //}
            //else
            //{
            //    ds = this.GetBySql("select ObjID from BasStorage where StorageHigherLevel = '" + StorageID + "'").ToDataSet();
            //    if (ds.Tables[0].Rows.Count > 0)
            //        return this.GetBySql("SELECT StorageHigherLevel + RIGHT('000' + CONVERT(VARCHAR, CONVERT(INT, RIGHT(StorageID, 3)) + 1), 3), isnull(StorageLevel, 1)+1 FROM BasStorage where ObjID = (select max(ObjID) from BasStorage where StorageHigherLevel = '" + StorageID + "')").ToDataSet();
            //    else
            //        return this.GetBySql("SELECT '" + StorageID + "' + '001', 2").ToDataSet();
            //}
            if (StorageID.Length < 3)
            {
                ds = this.GetBySql("select ObjID from BasStorage where StorageHigherLevel = '" + StorageID + "'").ToDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return this.GetBySql("SELECT RIGHT('000' + CONVERT(VARCHAR, CONVERT(INT, RIGHT(StorageID, 3)) + 1), 3), LEN(RIGHT('000' + CONVERT(VARCHAR, CONVERT(INT, RIGHT(StorageID, 3)) + 1), 3))/3 FROM BasStorage where ObjID = (select max(ObjID) from BasStorage where StorageLevel = '1')").ToDataSet();
                }
                else
                {
                    return this.GetBySql("SELECT RIGHT('000' + CONVERT(VARCHAR, CONVERT(INT, RIGHT(StorageID, 3)) + 1), 3), '1' FROM BasStorage where ObjID = (select max(ObjID) from BasStorage where LEN(StorageID) = 3)").ToDataSet();
                }
            }
            else
            {
                ds = this.GetBySql("select ObjID from BasStorage where StorageHigherLevel = '" + StorageID + "'").ToDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                    return this.GetBySql("SELECT StorageHigherLevel + RIGHT('000' + CONVERT(VARCHAR, CONVERT(INT, RIGHT(StorageID, 3)) + 1), 3), isnull(StorageLevel, 1)+1 FROM BasStorage where ObjID = (select max(ObjID) from BasStorage where StorageHigherLevel = '" + StorageID + "')").ToDataSet();
                else
                    return this.GetBySql("SELECT '" + StorageID + "' + '001', 2").ToDataSet();
            }
        }

        public void UpdateLastStorageFlag(string ObjID)
        {
            string sql = "update BasStorage set LastStorageFlag = '0' where ObjID = '" + ObjID + "'";
            this.GetBySql(sql).ExecuteNonQuery();
        }

        public string GetStorageName(string StorageID)
        {
            DataSet ds = this.GetBySql("select StorageName from BasStorage where StorageID = '" + StorageID + "'").ToDataSet();
            if (ds.Tables[0].Rows.Count > 0)
                return this.GetBySql("select StorageName from BasStorage where StorageID = '" + StorageID + "'").ToDataSet().Tables[0].Rows[0][0].ToString();
            else
                return "";
        }

        public DataSet GetDuration(string StorageID)
        {
            DataSet ds = this.GetBySql(@"SELECT A.ObjID, A.StorageID, A.StorageName, CONVERT(bit, A.UsedFlag) UsedFlag, A.UsingDuration, 
                                            case when A.DurationEndDate = '99' then convert(varchar(10), convert(datetime, convert(nvarchar(8),CONVERT(datetime, substring(A.UsingDuration, 1, 4)+substring(A.UsingDuration, 5, 2)+'01'),120)+ '01'), 120)
                                            else convert(varchar(10), dateadd(MONTH, -1, CONVERT(datetime, SUBSTRING(A.UsingDuration, 1, 4) + '-'+SUBSTRING(A.UsingDuration, 5, 2)+'-'+RIGHT('00' + CONVERT(VARCHAR, A.DurationBeginDate), 2))), 120) end DurationBeginDate,
                                            case when A.DurationEndDate = '99' then convert(varchar(10), Dateadd(day, -1, Dateadd(MONTH, 1, CONVERT(datetime, SUBSTRING(A.UsingDuration, 1, 4) + '-'+SUBSTRING(A.UsingDuration, 5, 2)+'-01 23:59:59'))), 120)
                                            else convert(varchar(10), dateadd(SECOND, -1, CONVERT(datetime, SUBSTRING(A.UsingDuration, 1, 4) + '-'+SUBSTRING(A.UsingDuration, 5, 2)+'-'+RIGHT('00' + CONVERT(VARCHAR, A.DurationBeginDate), 2))), 120) end DurationEndDate,
                                            case when A.DurationEndDate = '99' and A.UsingDuration is not null then '自然月' when A.DurationEndDate != '99' and A.UsingDuration is not null then '自定义' else '' end DurationSet
                                        FROM BasStorage A left join BasStorage B on A.StorageHigherLevel = B.StorageID
                                        where A.StorageID = '" + StorageID + "'").ToDataSet();
            return ds;
        }

        public DataSet GetStorageInfo(string storageType, string lastStorageFlag)
        {
            string sql = "select StorageID, StorageName from BasStorage where StorageType = '" + storageType + "'";
            if (!string.IsNullOrEmpty(lastStorageFlag))
                sql += " and LastStorageFlag = '" + lastStorageFlag + "'";

            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetStorageStr(string storageID)
        {
            string sql = @"with a as(
                            select StorageID from BasStorage where StorageID = '" + storageID + @"'
                            union all
                            select b.StorageID from BasStorage b, a where b.StorageHigherLevel = a.StorageID)
                            select * from a";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
