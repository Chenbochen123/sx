using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmRubberCarryoverService : BaseService<PpmRubberCarryover>, IPpmRubberCarryoverService
    {
		#region 构造方法

        public PpmRubberCarryoverService() : base(){ }

        public PpmRubberCarryoverService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberCarryoverService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        //获取库房已经结转的期间
        public string GetInaccountDuration(string StorageID)
        {
            string sql = "select MAX(InaccountDuration) from PpmRubberCarryover where StorageID = '" + StorageID + "'";

            return this.GetBySql(sql).ToScalar().ToString();
        }

        //获取库房应该结转到的期间
        public string GetStorageDuration(string StorageID)
        {
            string sql = "select case when day(GETDATE()) > DurationEndDate then CONVERT(varchar(6), GETDATE(), 112) else CONVERT(varchar(6), DATEADD(month, -1, getdate()), 112) end from BasStorage where StorageID = '" + StorageID + "'";

            return this.GetBySql(sql).ToScalar().ToString();
        }

        //获取库存信息没有结转的期间，按照日期大小依次进行结转
        public List<string> GetDurationFromPpmStorage(string StorageID)
        {
            List<string> arrList = new List<string>();
            string sql = @"select distinct InaccountDuration from PpmStorageDetail where StorageID = '" + StorageID + @"' 
                        and InaccountDuration != (select case when day(GETDATE()) > DurationEndDate then CONVERT(varchar(6), GETDATE(), 112) else CONVERT(varchar(6), DATEADD(month, -1, getdate()), 112) end from BasStorage where StorageID = '" + StorageID + @"') 
                        order by InaccountDuration";
            for (int i = 0; i < GetBySql(sql).ToDataSet().Tables[0].Rows.Count; i++)
            {
                arrList.Add(GetBySql(sql).ToDataSet().Tables[0].Rows[0][0].ToString());
            }

            return arrList;
        }

        //查询库存指定期间和上一期间结余的数据
        public bool CarryoverStorageDetail(string StorageID, string InaccountDuration)
        {
            string sql = "update BasStorage set LockFlag = '1' where StorageID = '" + StorageID + "';";
            sql += @"insert into PpmRubberCarryoverDetail
                        select '" + InaccountDuration + "', Barcode, StorageID, StoragePlaceID, OrderID, StoreInOut, RecordDate, Num, PieceWeight, Weight, InaccountDate,BillType, SourceBillNo, SourceOrderID from PpmStorageDetail where StorageID = '" + StorageID + "' and InaccountDuration <= '" + InaccountDuration + "';";
            sql += @"insert into PpmRubberCarryover
                        select '" + InaccountDuration + @"', A.Barcode, A.StorageID, A.StoragePlaceID, A.Barcode, B.MaterCode, B.ProcDate, SUM(A.Num * (case when A.StoreInOut = 'I' then 1 else -1 end)), 
                        SUM(A.Weight * (case when A.StoreInOut = 'I' then 1 else -1 end))/SUM(A.Num * (case when A.StoreInOut = 'I' then 1 else -1 end)), 
                        SUM(A.Weight * (case when A.StoreInOut = 'I' then 1 else -1 end)), B.RecordDate, B.FactoryID
                        from PpmStorageDetail A left join PpmStorage B on A.Barcode = B.Barcode and A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID
                        where A.StorageID = '" + StorageID + @"' and A.InaccountDuration <= '" + InaccountDuration + @"'
                        group by A.Barcode, A.StorageID, A.StoragePlaceID, A.Barcode, B.MaterCode, B.ProcDate, B.RecordDate, B.FactoryID;";
            sql += "delete from PpmStorageDetail where StorageID = '" + StorageID + "' and InaccountDuration <= '" + InaccountDuration + "';";
            sql += @"insert into PpmStorageDetail
                        select Barcode, StorageID, StoragePlaceID, 1, 'I', GETDATE(), Num, PieceWeight, RealWeight, '" + InaccountDuration + @"', GETDATE(), '1007', null, null 
                        from PpmRubberCarryover
                        where InaccountDuration = '" + InaccountDuration + "' and StorageID = '" + StorageID + "';";
            sql += "update BasStorage set LockFlag = '0' where StorageID = '" + StorageID + "';";
            try
            {
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
