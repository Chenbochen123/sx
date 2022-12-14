using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstShopAdjustService : BaseService<PstShopAdjust>, IPstShopAdjustService
    {
		#region 构造方法

        public PstShopAdjustService() : base(){ }

        public PstShopAdjustService(string connectStringKey) : base(connectStringKey){ }

        public PstShopAdjustService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string storageID { get; set; }
            public string toStorageID { get; set; }
            public string workShopCode { get; set; }
            public string barcode { get; set; }
            public string materCode { get; set; }
            public string shiftID { get; set; }
            public string operPerson { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string deleteFlag { get; set; }
            public string AdjustType { get; set; }
            public string matertype { get; set; }
            public PageResult<PstShopAdjust> pageParams { get; set; }
        }
        public DataSet GetReportBySql(QueryParams queryParams)
        {
           
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select B.StorageName,C.StoragePlaceName, D.MaterialName,
	                                  E.StorageName as ToStorageName,convert(nvarchar(10),A.RecordDate,120) RecordDate,sum(AdjustNum) AdjustNum, sum(AdjustWeight) AdjustWeight
                                from PstShopAdjust A
	                                left join BasStorage B on A.StorageID = B.StorageID
	                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
	                                left join BasMaterial D on A.MaterCode = D.MaterialCode
	                                left join BasStorage E on A.ToStorageID = E.StorageID
	                                left join BasStoragePlace F on A.ToStoragePlaceID = F.StoragePlaceID
	                                left join PptShift G on A.ShiftID = G.ObjID
	                                left join PptClass H on A.ShiftClassID = H.ObjID
	                                left join BasUser I on A.OperPerson = I.WorkBarcode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.toStorageID))
            {
                sqlstr.AppendLine(" AND A.ToStorageID = '" + queryParams.toStorageID + "'");
            }
            if (queryParams.workShopCode != "all")
            {
                sqlstr.AppendLine(" AND B.WorkShopCode = '" + queryParams.workShopCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (queryParams.shiftID != "all")
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.operPerson))
            {
                sqlstr.AppendLine(" AND A.OperPerson = '" + queryParams.operPerson + "'");
            }

            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (queryParams.matertype == "1")
            {
                sqlstr.AppendLine(" AND left(A.MaterCode,3) in ('101','102','103') ");
            }
            sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
            sqlstr.AppendLine(" group by  B.StorageName,C.StoragePlaceName, D.MaterialName, E.StorageName,convert(nvarchar(10),A.RecordDate,120)");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
                
        }


        public PageResult<PstShopAdjust> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PstShopAdjust> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, Barcode, OrderID, A.MaterCode, D.MaterialName,
	                                ProcDate, AdjustNum, AdjustWeight, A.ToStorageID, E.StorageName ToStorageName, A.ToStoragePlaceID, F.StoragePlaceName ToStoragePlaceName, InaccountDate,
	                                A.ShiftID, G.ShiftName, A.ShiftClassID, H.ClassName, A.OperPerson, I.UserName, A.RecordDate
                                from PstShopAdjust A
	                                left join BasStorage B on A.StorageID = B.StorageID
	                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
	                                left join BasMaterial D on A.MaterCode = D.MaterialCode
	                                left join BasStorage E on A.ToStorageID = E.StorageID
	                                left join BasStoragePlace F on A.ToStoragePlaceID = F.StoragePlaceID
	                                left join PptShift G on A.ShiftID = G.ObjID
	                                left join PptClass H on A.ShiftClassID = H.ObjID
	                                left join BasUser I on A.OperPerson = I.WorkBarcode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.toStorageID))
            {
                sqlstr.AppendLine(" AND A.ToStorageID = '" + queryParams.toStorageID + "'");
            }
            if (queryParams.workShopCode != "all")
            {
                sqlstr.AppendLine(" AND B.WorkShopCode = '" + queryParams.workShopCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (queryParams.shiftID != "all")
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.operPerson))
            {
                sqlstr.AppendLine(" AND A.OperPerson = '" + queryParams.operPerson + "'");
            }

            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (queryParams.matertype == "1")
            {
                sqlstr.AppendLine(" AND left(A.MaterCode,3) in ('101','102','103') ");
            }
            sqlstr.AppendLine(" AND A.DeleteFlag = '" + queryParams.deleteFlag + "'");
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

        public PageResult<PstShopAdjust> GetTablePageTotalBySql(QueryParams queryParams)
        {
            PageResult<PstShopAdjust> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select * from (
                                    select A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName, SUM(AdjustNum) TotalNum, SUM(AdjustWeight) TotalWeight, 'O' AdjustType
                                    from PstShopAdjust A
	                                    left join BasStorage B on A.StorageID = B.StorageID
	                                    left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
	                                    left join BasMaterial D on A.MaterCode = D.MaterialCode");
            sqlstr.AppendLine(" where A.DeleteFlag = '" + queryParams.deleteFlag + "'");
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (queryParams.workShopCode != "all")
            {
                sqlstr.AppendLine(" AND B.WorkShopCode = '" + queryParams.workShopCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }

            sqlstr.AppendLine(@" group by A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName
                                union all
                                select A.ToStorageID, B.StorageName, A.ToStoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName, SUM(AdjustNum) TotalNum, SUM(AdjustWeight) TotalWeight, 'I' AdjustType
                                from PstShopAdjust A
	                                left join BasStorage B on A.ToStorageID = B.StorageID
	                                left join BasStoragePlace C on A.ToStoragePlaceID = C.StoragePlaceID
	                                left join BasMaterial D on A.MaterCode = D.MaterialCode");
            sqlstr.AppendLine(" where A.DeleteFlag = '" + queryParams.deleteFlag + "'");
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (queryParams.workShopCode != "all")
            {
                sqlstr.AppendLine(" AND B.WorkShopCode = '" + queryParams.workShopCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.ToStorageID = '" + queryParams.storageID + "'");
            }

            sqlstr.AppendLine(@"group by A.ToStorageID, B.StorageName, A.ToStoragePlaceID, C.StoragePlaceName, A.MaterCode, D.MaterialName
                                ) A");
            if (queryParams.AdjustType != "all")
                sqlstr.AppendLine(" where AdjustType = '" + queryParams.AdjustType + "'");
            
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

        public DataSet GetByInfo(string workShopCode, DateTime beginTime, DateTime endTime, string storageID, string storagePlaceID, string materCode, string adjustType)
        {
            string sql = string.Empty;
            if (adjustType == "I")
            {
                sql = @"select A.Barcode, A.ToStorageID StorageID, B.StorageName, A.ToStoragePlaceID StoragePlaceID, C.StoragePlaceName, E.StorageName ToStorageName, F.StoragePlaceName ToStoragePlaceName, A.MaterCode, D.MaterialName, G.Num, G.RealWeight, SUM(AdjustNum) AdjustNum, SUM(AdjustWeight) AdjustWeight, 'I' AdjustType
                        from PstShopAdjust A
                            left join BasStorage B on A.ToStorageID = B.StorageID
                            left join BasStoragePlace C on A.ToStoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            left join BasStorage E on A.StorageID = E.StorageID
                            left join BasStoragePlace F on A.StoragePlaceID = F.StoragePlaceID
                            left join PstShopStorage G on A.StorageID = G.StorageID and A.StoragePlaceID = G.StoragePlaceID and A.Barcode = G.Barcode";
            }
            else
            {
                sql = @"select A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, E.StorageName ToStorageName, F.StoragePlaceName ToStoragePlaceName, A.MaterCode, D.MaterialName, G.Num, G.RealWeight, SUM(AdjustNum) AdjustNum, SUM(AdjustWeight) AdjustWeight, 'O' AdjustType
                        from PstShopAdjust A
                            left join BasStorage B on A.StorageID = B.StorageID
                            left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                            left join BasMaterial D on A.MaterCode = D.MaterialCode
                            left join BasStorage E on A.ToStorageID = E.StorageID
                            left join BasStoragePlace F on A.ToStoragePlaceID = F.StoragePlaceID
                            left join PstShopStorage G on A.StorageID = G.StorageID and A.StoragePlaceID = G.StoragePlaceID and A.Barcode = G.Barcode";
            }

            sql += @" where A.DeleteFlag = '0' and A.RecordDate >= '" + beginTime.ToString() + "' and A.RecordDate <= '" + endTime.AddDays(1).ToString() + "'";
            if (workShopCode != "all")
                sql += " and B.WorkShopCode = '" + workShopCode + "'";
            if (!string.IsNullOrEmpty(storageID) && adjustType == "I")
                sql += " and A.ToStorageID = '" + storageID + "'";
            if (!string.IsNullOrEmpty(storageID) && adjustType == "O")
                sql += " and A.StorageID = '" + storageID + "'";
            if (!string.IsNullOrEmpty(materCode))
                sql += " and A.MaterCode = '" + materCode + "'";
            if (adjustType == "I")
                sql += " group by A.Barcode, A.ToStorageID, B.StorageName, A.ToStoragePlaceID, C.StoragePlaceName, E.StorageName, F.StoragePlaceName, A.MaterCode, D.MaterialName, G.Num, G.RealWeight";
            else
                sql += " group by A.Barcode, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, E.StorageName, F.StoragePlaceName, A.MaterCode, D.MaterialName, G.Num, G.RealWeight";
            
            return this.GetBySql(sql).ToDataSet();
        }

        public string CancelShopAdjust(string storageID, string storagePlaceID, string barcode, int orderID, string operPerson)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPstCancelShopAdjust");
            sps.AddInputParameter("Barcode", this.TypeToDbType(barcode.GetType()), barcode);
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("OrderID", this.TypeToDbType(orderID.GetType()), orderID);
            sps.AddInputParameter("OperPerson", this.TypeToDbType(operPerson.GetType()), operPerson);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }
    }
}
