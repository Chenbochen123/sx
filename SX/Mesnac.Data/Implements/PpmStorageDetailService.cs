using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PpmStorageDetailService : BaseService<PpmStorageDetail>, IPpmStorageDetailService
    {
		#region 构造方法

        public PpmStorageDetailService() : base(){ }

        public PpmStorageDetailService(string connectStringKey) : base(connectStringKey){ }

        public PpmStorageDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID)
        {
            string sql = @"select Barcode, OrderID, StoreInOut, Num, PieceWeight, Weight, RecordDate, InaccountDuration, BillType, SourceBillNo, SourceOrderID
                            from PpmStorageDetail
                            where 1 = 1 and Barcode ='" + Barcode + "' and StorageID = '" + StorageID + "' and StoragePlaceID = '" + StoragePlaceID + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public int GetOrderID(string Barcode)
        {
            DataSet ds = this.GetBySql("select MAX(OrderID) from PpmStorageDetail where Barcode = '" + Barcode + "'").ToDataSet();
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            else
                return 0;
        }
    }
}
