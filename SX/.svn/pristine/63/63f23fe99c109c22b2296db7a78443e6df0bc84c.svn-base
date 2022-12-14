using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using System.Data;
    public class PpmRubberStorageService : BaseService<PpmRubberStorage>, IPpmRubberStorageService
    {
		#region 构造方法

        public PpmRubberStorageService() : base(){ }

        public PpmRubberStorageService(string connectStringKey) : base(connectStringKey){ }

        public PpmRubberStorageService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string barcode { get; set; }
            public string storageID { get; set; }
            public string storagePlaceID { get; set; }
            public string equipCode { get; set; }
            public string materCode { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string shiftID { get; set; }
            public string shiftClassID { get; set; }
            public string stockFlag { get; set; }
            public string isEmptyWeight { get; set; }
            public string ShlefBarCode { get; set; }
            public string Oper { get; set; }
            public int Limit { get; set; }
            public int page { get; set; }
            public int pagenum { get; set; } 
            public PageResult<PpmRubberStorage> pageParams { get; set; }
        }
        public DataTable GetTableStoreOutDetailReport(QueryParams queryParams)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BarCode,B.StorageName,'' as StoragePlaceName,S.StorageName as ToStorageName,A.memnote,
	                                ShelfNum, PlanDate,  D.ShiftName,  E.ClassName,  F.EquipName, 
	                                 G.MaterialName, A.Weight, A.RecordDate, A.OperPerson,A.shelfnum,a.llbarcode,a.llmennote
                                from PpmRubberStoreout A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStorage S on A.ToStorageID = S.StorageID
                                left join PptShift D on A.ShiftID = D.ObjID
                                left join PptClass E on A.ShiftClassID = E.ObjID
                                left join BasEquip F on A.EquipCode = F.EquipCode
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.ToStorageID = '" + queryParams.storagePlaceID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate >= Convert( datetime,'"+ queryParams.beginDate.ToString() + "',120)"); 
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate <= Convert( datetime,'" + queryParams.endDate.ToString() + "',120)"); 
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }

            if (queryParams.shiftID != "all" && !string.IsNullOrEmpty(queryParams.shiftID))
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            if (queryParams.shiftClassID != "all" && !string.IsNullOrEmpty(queryParams.shiftClassID))
                sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet().Tables[0];

        }
        public DataTable GetTableStoreOutReport(QueryParams queryParams)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select B.StorageName,'' as StoragePlaceName,S.StorageName as ToStorageName,
	                                convert(nvarchar(10),a.recorddate,120) recorddate,
	                                 G.MaterialName, sum(A.Weight) weight,sum(A.shelfnum) shelfnum
                                from PpmRubberStoreout A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStorage S on A.ToStorageID = S.StorageID
                                left join PptShift D on A.ShiftID = D.ObjID
                                left join PptClass E on A.ShiftClassID = E.ObjID
                                left join BasEquip F on A.EquipCode = F.EquipCode
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.ToStorageID = '" + queryParams.storagePlaceID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate >= Convert(datetime, '" + queryParams.beginDate.ToString() + "', 120)"); 
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate <= Convert(datetime, '" + queryParams.endDate.ToString() + "', 120)"); 
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }

            if (queryParams.shiftID != "all" && !string.IsNullOrEmpty(queryParams.shiftID))
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            if (queryParams.shiftClassID != "all" && !string.IsNullOrEmpty(queryParams.shiftClassID))
                sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");
            sqlstr.AppendLine(" group by B.StorageName,S.StorageName, convert(nvarchar(10),a.recorddate,120),G.MaterialName");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet().Tables[0];

        }

        public DataTable GetTableStoreBackDetailReport(QueryParams queryParams)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BarCode,B.StorageName,C.StoragePlaceName,S.StorageName as ToStorageName,G.MaterialName,
	                                 A.BackWeight,A.RecordDate, case when OperType='1' then '正常' else '不合格' end opertype
                                from PpmRubberBackNormalLog A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasStorage S on A.SourceStorageID = S.StorageID
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.SourceStorageID = '" + queryParams.storagePlaceID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.ToString() + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if ((!string.IsNullOrEmpty(queryParams.Oper)) && queryParams.Oper != "全部" && queryParams.Oper != "all")
            {
                sqlstr.AppendLine(" AND A.OperType = " + queryParams.Oper);
            }
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet().Tables[0];

        }

        public DataTable GetTableStoreBackReport(QueryParams queryParams)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select B.StorageName,C.StoragePlaceName,S.StorageName as ToStorageName,
	                                convert(nvarchar(10),a.recorddate,120) recorddate,
	                                 G.MaterialName, sum(A.BackWeight) weight
                                from PpmRubberBackNormalLog A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasStorage S on A.SourceStorageID = S.StorageID
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.SourceStorageID = '" + queryParams.storagePlaceID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.RecordDate <= '" + queryParams.endDate.ToString() + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if ((!string.IsNullOrEmpty(queryParams.Oper))&&queryParams.Oper!="全部"&&queryParams.Oper!="all")
            {
                sqlstr.AppendLine(" AND A.OperType = " + queryParams.Oper);
            }
            sqlstr.AppendLine(" group by B.StorageName,C.StoragePlaceName,S.StorageName, convert(nvarchar(10),a.recorddate,120),G.MaterialName");
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet().Tables[0];

        }

        public PageResult<PpmRubberStorage> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, Barcode, ShelfBarcode, BarcodeStart,BarcodeEnd,Convert(nvarchar(10),BarcodeStart)+'-'+Convert(nvarchar(10),BarcodeEnd) as 'Checi',
	                                ShelfNum, MemNote, PlanDate, A.ShiftID, D.ShiftName, A.ShiftClassID, E.ClassName, A.EquipCode, F.EquipName, 
	                                A.MaterCode, G.MaterialName, A.ValidDate, A.ProductWeight, A.StockFlag, A.CheckFlag, A.TecDealFlag, A.TecDealIdea,
	                                A.ConsumeWeight, A.RealWeight, A.RecordDate, A.OperPerson,A.RealNum
                                from PpmRubberStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join PptShift D on A.ShiftID = D.ObjID
                                left join PptClass E on A.ShiftClassID = E.ObjID
                                left join BasEquip F on A.EquipCode = F.EquipCode
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
                                where 1 = 1");
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode like '%" + queryParams.barcode + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }

            if (queryParams.shiftID != "all")
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            
            if (queryParams.shiftClassID != "all")
                sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");

            if (queryParams.stockFlag != "all")
                sqlstr.AppendLine(" AND A.StockFlag = '" + queryParams.stockFlag + "'");

            if (queryParams.isEmptyWeight == "0")
                sqlstr.AppendLine(" AND A.RealWeight > 0");

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

        public PageResult<PpmRubberStorage> GetTablePageStoreoutBySql(QueryParams queryParams)
        {
            PageResult<PpmRubberStorage> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();


            sqlstr.AppendLine(@"select A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.ShelfBarcode, BarcodeStart,BarcodeEnd,
                                    ShelfNum, MemNote, A.PlanDate, A.ShiftID, D.ShiftName, A.ShiftClassID, E.ClassName, A.EquipCode, F.EquipName, 
                                    A.MaterCode, G.MaterialName, A.ValidDate, A.ProductWeight, A.StockFlag, A.CheckFlag, A.TecDealFlag, A.TecDealIdea,
                                    A.ConsumeWeight, A.RealWeight");
            if (queryParams.stockFlag == "1")
            {
                sqlstr.AppendLine(@",H.RecordDate, H.Weight,BS.StorageName as  ToStorageName,CS.StoragePlaceName as ToStoragePlaceName, u.UserName as OperPerson");
            }
            else
            {
                sqlstr.AppendLine(@",'' as RecordDate, '' as Weight,'' as  ToStorageName,'' as ToStoragePlaceName, '' as OperPerson");
            }
             sqlstr.AppendLine(@"from PpmRubberStorage A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join PptShift D on A.ShiftID = D.ObjID
                                left join PptClass E on A.ShiftClassID = E.ObjID
                                left join BasEquip F on A.EquipCode = F.EquipCode
                                left join BasMaterial G on A.MaterCode = G.MaterialCode
                                
                                ");
             if (queryParams.stockFlag == "1")
             {
                 sqlstr.AppendLine(@"left join PpmRubberStorageDetail H on H.OperType = '003' and A.StorageID = H.StorageID and A.StoragePlaceID = H.StoragePlaceID and A.Barcode = H.Barcode
                                left join BasStorage BS on H.ToStorageID = BS.StorageID
                                left join BasStoragePlace CS on H.ToStoragePlaceID = CS.StoragePlaceID
                                left join BasUser u on H.OperPerson = u.WorkBarcode  where 1 = 1 ");
             }
            else
            {
                 sqlstr.AppendLine(@" where 1 = 1 ");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.Oper))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                if (queryParams.stockFlag == "1")
                {
                    sqlstr.AppendLine(" AND H.ToStorageID = '" + queryParams.storagePlaceID + "'");
                }
            }
            if (!string.IsNullOrEmpty(queryParams.Oper))
            {
                if (queryParams.stockFlag == "1")
                {
                    sqlstr.AppendLine(" AND H.OperPerson = '" + queryParams.Oper + "'");
                }
            }
            if (queryParams.stockFlag == "0")
            {
                if (queryParams.beginDate != DateTime.MinValue)
                {
                    sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
                }
                if (queryParams.endDate != DateTime.MinValue)
                {
                    sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
                }
            }
            else
            {
                if (queryParams.beginDate != DateTime.MinValue)
                {
                    sqlstr.AppendLine(" AND H.RecordDate >= '" + queryParams.beginDate.ToString() + "'");
                }
                if (queryParams.endDate != DateTime.MinValue)
                {
                    sqlstr.AppendLine(" AND H.RecordDate <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
                }
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (queryParams.stockFlag == "1")
            {
                sqlstr.AppendLine(" and H.OperType = '003'");
            }
            if (queryParams.shiftID != "all")
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");

            if ((!string.IsNullOrEmpty(queryParams.shiftClassID)) && queryParams.shiftClassID != "all")
                sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");

            //if (queryParams.stockFlag != "all")
            if ((!string.IsNullOrEmpty(queryParams.stockFlag)) && queryParams.stockFlag != "all")
            sqlstr.AppendLine(" AND A.StockFlag = '" + queryParams.stockFlag + "'");

            if (queryParams.isEmptyWeight == "0")
                sqlstr.AppendLine(" AND A.RealWeight > 0");

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

        public string SubmitRubberStoreOut(string storageID, string storagePlaceID, string barcode, string shiftID, string shiftClassID, string operPerson, string toStorageID, string toStoragePlaceID)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmSubmitRubberStoreout");
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("Barcode", this.TypeToDbType(barcode.GetType()), barcode);
            sps.AddInputParameter("ShiftID", this.TypeToDbType(shiftID.GetType()), shiftID);
            sps.AddInputParameter("ShiftClassID", this.TypeToDbType(shiftClassID.GetType()), shiftClassID);
            sps.AddInputParameter("OperPerson", this.TypeToDbType(operPerson.GetType()), operPerson);
            sps.AddInputParameter("ToStorageID", this.TypeToDbType(toStorageID.GetType()), toStorageID);
            sps.AddInputParameter("ToStoragePlaceID", this.TypeToDbType(toStoragePlaceID.GetType()), toStoragePlaceID);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public string CancelReturnRubber(string storageID, string storagePlaceID, string barcode)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPpmCancelRubberStoreout");
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("Barcode", this.TypeToDbType(barcode.GetType()), barcode);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }
        public PageResult<PpmRubberStorage> ProcPPMOutDateQuery(QueryParams queryParams,string startDate, string endDate, string workShop, string storageID, string storagePlaceID, int limit, string barCode, string shlefBarCode, int page, int pagenum,string matercode)
        {
            PageResult<PpmRubberStorage> pageParams = queryParams.pageParams;
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPPMOutDateQuery2");
            sps.AddInputParameter("StartDate", this.TypeToDbType(startDate.GetType()), startDate);
            sps.AddInputParameter("EndDate", this.TypeToDbType(endDate.GetType()), endDate);
            sps.AddInputParameter("WorkShop", this.TypeToDbType(workShop.GetType()), workShop);
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("Limit", this.TypeToDbType(limit.GetType()), limit);
            sps.AddInputParameter("BarCode", this.TypeToDbType(barCode.GetType()), barCode);
            sps.AddInputParameter("ShlefBarCode", this.TypeToDbType(shlefBarCode.GetType()), shlefBarCode);
            sps.AddInputParameter("page", this.TypeToDbType(page.GetType()), page);
            sps.AddInputParameter("pagenum", this.TypeToDbType(pagenum.GetType()), pagenum);
            sps.AddInputParameter("matercode", this.TypeToDbType(matercode.GetType()), matercode);
            string sqlstr = sps.ToDataSet().Tables[0].Rows[0][0].ToString();
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
        public PageResult<PpmRubberStorage> ProcPPMOutDateQueryDeal(QueryParams queryParams,string matercode, string startDate, string endDate, string workShop, string storageID, string storagePlaceID, string barCode, string shlefBarCode, int type, string orderway, int page, int pagenum)
        {
            PageResult<PpmRubberStorage> pageParams = queryParams.pageParams;
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPPMOutDateQueryDeal2");
            sps.AddInputParameter("StartDate", this.TypeToDbType(startDate.GetType()), startDate);
            sps.AddInputParameter("EndDate", this.TypeToDbType(endDate.GetType()), endDate);
            sps.AddInputParameter("WorkShop", this.TypeToDbType(workShop.GetType()), workShop);
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("matercode", this.TypeToDbType(matercode.GetType()), matercode);
            sps.AddInputParameter("equipcode", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("BarCode", this.TypeToDbType(barCode.GetType()), barCode);
            sps.AddInputParameter("ShlefBarCode", this.TypeToDbType(shlefBarCode.GetType()), shlefBarCode);
            sps.AddInputParameter("Type", this.TypeToDbType(type.GetType()), type);
            sps.AddInputParameter("orderway", this.TypeToDbType(orderway.GetType()), orderway);
            sps.AddInputParameter("page", this.TypeToDbType(page.GetType()), page);
            sps.AddInputParameter("pagenum", this.TypeToDbType(pagenum.GetType()), pagenum);
            
            string sqlstr = sps.ToDataSet().Tables[0].Rows[0][0].ToString();
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
        public DataSet ProcPPMOutDateTotalQuery(string startDate, string endDate, string workShop, string storageID, string storagePlaceID, int limit, string barCode, string shlefBarCode)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPPMOutDateTotalQuery");
            sps.AddInputParameter("StartDate", this.TypeToDbType(startDate.GetType()), startDate);
            sps.AddInputParameter("EndDate", this.TypeToDbType(endDate.GetType()), endDate);
            sps.AddInputParameter("WorkShop", this.TypeToDbType(workShop.GetType()), workShop);
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("Limit", this.TypeToDbType(limit.GetType()), limit);
            sps.AddInputParameter("BarCode", this.TypeToDbType(barCode.GetType()), barCode);
            sps.AddInputParameter("ShlefBarCode", this.TypeToDbType(shlefBarCode.GetType()), shlefBarCode);
            return sps.ToDataSet();
        }
    }
   
}
