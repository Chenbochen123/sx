using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstShopStorageDetailService : BaseService<PstShopStorageDetail>, IPstShopStorageDetailService
    {
		#region 构造方法

        public PstShopStorageDetailService() : base(){ }

        public PstShopStorageDetailService(string connectStringKey) : base(connectStringKey){ }

        public PstShopStorageDetailService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string storageID { get; set; }
            public string storagePlaceID { get; set; }
            public string materCode { get; set; }
            public string barcode { get; set; }
            public string factoryID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public bool isScreenPrinted { get; set; }
            public PageResult<PstShopStorageDetail> pageParams { get; set; }
        }

        public PageResult<PstShopStorageDetail> GetTablePageDataBySqlPrint(QueryParams queryParams)
        {
            PageResult<PstShopStorageDetail> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.Barcode, E.MaterialCode, E.MaterialName, Num, PieceWeight, Weight, InaccountDate, A.StorageID, 
	                                B.StorageName, A.StoragePlaceID, C.StoragePlaceName, ShiftClassID, D.ClassName, F.ObjID, F.FacName, 
	                                A.OrderID, case when G.MaterCode is null then '0' else '1' end IsPrint, G.InTime
                                from PstShopStorageDetail A
                                    left join BasStorage B on A.StorageID = B.StorageID
                                    left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                    left join PptClass D on A.ShiftClassID = D.ObjID
                                    left join BasMaterial E on SUBSTRING(Barcode, 1, 9) = E.ERPCode
                                    left join BasFactoryInfo F on SUBSTRING(Barcode, 15, 4) = F.ERPCode
                                    left join PstMaterialInOast G on A.Barcode = G.Barcode and A.StorageID = G.StorageID and A.StoragePlaceID = G.StoragePlaceID
                                where StoreInOut = 'I' and LEN(A.Barcode) = 22");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.factoryID))
            {
                sqlstr.AppendLine(" AND F.ObjID = '" + queryParams.factoryID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND E.MaterialCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!queryParams.isScreenPrinted)
            {
                sqlstr.AppendLine(" AND G.MaterCode is null");
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

        public DataSet GetByPrintInfo(string Barcode, string StorageID, string StoragePlaceID, string OrderID)
        {
            string sql = @"select A.Barcode, E.MaterialCode, E.MaterialName, Num, PieceWeight, Weight, InaccountDate, 
	                            A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, ShiftID, G.ShiftName, 
	                            ShiftClassID, D.ClassName, F.ObjID, F.FacName, A.OrderID,
	                            case when H.MaterCode is null then '0' else '1' end IsPrint,
                                SUBSTRING(CONVERT(varchar, H.InTime, 120), 6, 2) + '月' + SUBSTRING(CONVERT(varchar, H.InTime, 120), 9, 2) + '日' DateDay,
	                            SUBSTRING(CONVERT(varchar, H.InTime, 120), 12, 2) + '时' + SUBSTRING(CONVERT(varchar, H.InTime, 120), 15, 2) + '分' DateMinute,
	                            SUBSTRING(CONVERT(varchar, DATEADD(HOUR, E.MinParkTime, H.InTime), 120), 6, 2) + '月' + SUBSTRING(CONVERT(varchar, DATEADD(HOUR, E.MinParkTime, H.InTime), 120), 9, 2) + '日' DateDay1,
	                            SUBSTRING(CONVERT(varchar, DATEADD(HOUR, E.MinParkTime, H.InTime), 120), 12, 2) + '时' + SUBSTRING(CONVERT(varchar, DATEADD(HOUR, E.MinParkTime, H.InTime), 120), 15, 2) + '分' DateMinute1
                            from PstShopStorageDetail A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join PptClass D on A.ShiftClassID = D.ObjID
                                left join BasMaterial E on SUBSTRING(Barcode, 1, 9) = E.ERPCode
                                left join BasFactoryInfo F on SUBSTRING(Barcode, 15, 4) = F.ERPCode
                                left join PptShift G on A.ShiftID = G.ObjID
                                left join PstMaterialInOast H on A.Barcode = H.Barcode and A.StorageID = H.StorageID and A.StoragePlaceID = H.StoragePlaceID
                            where StoreInOut = 'I' and LEN(A.Barcode) = 22 and A.Barcode ='" + Barcode + "' and A.StorageID = '" + StorageID + "' and A.StoragePlaceID = '" + StoragePlaceID + "' and A.OrderID = '" + OrderID + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID)
        {
            string sql = @"select Barcode, OrderID, StoreInOut, Num, PieceWeight, Weight, RecordDate, InaccountDuration, BillType, SourceBillNo, SourceOrderID,isnull(b.ShiftName,'')+'-'+ISNULL(c.ClassName,'') as shiftname,
BoxCode,d.EquipName,e.RealName
 from PstShopStorageDetail a left join pptshift b on a.ShiftID = b.ObjID
 left join PptClass c on a.ShiftClassID = c.ObjID
 left join BasEquip d on a.EquipCode = d.EquipCode
 left join BasUser e on a.OperCode = e.WorkBarcode
                            where 1 = 1 and Barcode ='" + Barcode + "' and StorageID = '" + StorageID + "' and StoragePlaceID = '" + StoragePlaceID + "'";
            return this.GetBySql(sql).ToDataSet();
        }
        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID,string boxcode)
        {
            string sql = @"select Barcode, OrderID, StoreInOut, Num, PieceWeight, Weight, RecordDate, InaccountDuration, BillType, SourceBillNo, SourceOrderID,isnull(b.ShiftName,'')+'-'+ISNULL(c.ClassName,'') as shiftname,
BoxCode,d.EquipName,e.RealName
 from PstShopStorageDetail a left join pptshift b on a.ShiftID = b.ObjID
 left join PptClass c on a.ShiftClassID = c.ObjID
 left join BasEquip d on a.EquipCode = d.EquipCode
 left join BasUser e on a.OperCode = e.WorkBarcode
                            where 1 = 1 and Barcode ='" + Barcode + "' and StorageID = '" + StorageID + "' and StoragePlaceID = '" + StoragePlaceID + "'";
            if (!string.IsNullOrEmpty(boxcode))
            {
                sql += " and boxcode like '%"+boxcode+"%'";
            }
            return this.GetBySql(sql).ToDataSet();
        }
        public int GetOrderID(string Barcode)
        {
            DataSet ds = this.GetBySql("select MAX(OrderID) from PstShopStorageDetail where Barcode = '" + Barcode + "'").ToDataSet();
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            else
                return 0;
        }

        public DataSet GetSqlInfo(string sql)
        {
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
