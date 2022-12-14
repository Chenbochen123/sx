using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstMaterialRubberSplitService : BaseService<PstMaterialRubberSplit>, IPstMaterialRubberSplitService
    {
		#region 构造方法

        public PstMaterialRubberSplitService() : base(){ }

        public PstMaterialRubberSplitService(string connectStringKey) : base(connectStringKey){ }

        public PstMaterialRubberSplitService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public string storageID { get; set; }
            public string storagePlaceID { get; set; }
            public string materCode { get; set; }
            public string barcode { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string isPrinted { get; set; }
            public string operPerson { get; set; }
            public string chejianCode { get; set; }
            public string shiftID { get; set; }
            public string shiftClassID { get; set; }
            public string splitbarcode { get; set; }
            public string shelfid { get; set; }
            public PageResult<PstMaterialRubberSplit> pageParams { get; set; }
        }

        public PageResult<PstMaterialRubberSplit> GetTablePageDataBySqlPrint(QueryParams queryParams)
        {
            PageResult<PstMaterialRubberSplit> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.BillNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.BarcodeSplit,
	                                A.MaterCode, D.MaterialName, A.Weight, CONVERT(varchar(10), A.PlanDate, 120) PlanDate, A.ShiftID, E.ShiftName,
	                                case when A.PrintTime is null then '0' else '1' end IsPrint, A.PrintTime, F.UserName
                                from PstMaterialRubberSplit A
	                                left join BasStorage B on A.StorageID = B.StorageID
	                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
	                                left join BasMaterial D on A.MaterCode = D.MaterialCode
	                                left join PptShift E on A.ShiftID = E.ObjID
	                                left join BasUser F on A.OperPerson = F.WorkBarcode
                                where 1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.splitbarcode))
            {
                sqlstr.AppendLine(" AND A.BarcodeSplit = '" + queryParams.splitbarcode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.operPerson))
            {
                sqlstr.AppendLine(" AND A.OperPerson = '" + queryParams.operPerson + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OperTime >= '" + queryParams.beginDate.ToString("yyyy-MM-dd") + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OperTime <= '" + queryParams.endDate.AddDays(1).ToString("yyyy-MM-dd") + "'");
            if (queryParams.isPrinted=="1")
            {
                sqlstr.AppendLine(" AND A.PrintTime is not null");
            }
            else if (queryParams.isPrinted == "0")
            {
                sqlstr.AppendLine(" AND A.PrintTime is null");
            }
            if (queryParams.chejianCode != "all")
                sqlstr.AppendLine(" AND B.WorkShopCode = '" + queryParams.chejianCode + "'");
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

        public PageResult<PstMaterialRubberSplit> GetTableSplitUnLock(QueryParams queryParams)
        {
            PageResult<PstMaterialRubberSplit> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select BarCode,LLShelfID ShelfID,case when FState='1' then '已锁定' else '未锁定' end FState from
                             BasLLShelf where 1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.barcode))
                sqlstr.AppendLine(" AND Barcode like '%" + queryParams.barcode + "%' ");
            if (!string.IsNullOrEmpty(queryParams.shelfid))
                sqlstr.AppendLine(" AND LLShelfID like '%" + queryParams.shelfid + "%'");
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


        public PageResult<PstMaterialRubberSplit> GetTableSplitReset(QueryParams queryParams)
        {
            PageResult<PstMaterialRubberSplit> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select a.BarcodeSplit,a.Barcode,b.StorageName,c.StoragePlaceName,d.MaterialName,f.ShiftName,g.ClassName,a.Weight,convert(nvarchar(30),a.OperTime,120) OperTime,(select UserName from basuser where WorkBarcode=a.OperPerson) as UserName,a.PrintTime, ISNULL(I.LLShelfID,S.LLShelfID) ShelfID,case when I.FState='1' then '已锁定' else '未锁定' end FState
                                from PstMaterialRubberSplit A
                                    left join BasLLShelf I on a.BarcodeSplit = I.BarCode
									left join PstMateiralSplitShelfCode S on a.BarcodeSplit = s.splistbarcode
	                                left join BasStorage B on A.StorageID = B.StorageID
	                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
	                                left join BasMaterial D on A.MaterCode = D.MaterialCode
	                                left join PptShift F on A.ShiftID = F.ObjID
	                                left join PptClass G on A.ShiftClassID = G.ObjID
                                where 1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.operPerson))
            {
                sqlstr.AppendLine(" AND A.OperPerson = '" + queryParams.operPerson + "'");
            }
            if (queryParams.shiftID != "all")
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            }
           
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OperTime >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OperTime <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            
            if (queryParams.chejianCode != "all")
                sqlstr.AppendLine(" AND B.WorkShopCode = '" + queryParams.chejianCode + "'");
            if (!string.IsNullOrEmpty(queryParams.barcode))
                sqlstr.AppendLine(" AND (A.Barcode like '%" + queryParams.barcode + "%' or A.BarcodeSplit like '%" + queryParams.barcode + "%') ");
            if (!string.IsNullOrEmpty(queryParams.shelfid))
                sqlstr.AppendLine(" AND ISNULL(I.LLShelfID,S.LLShelfID) = '" + queryParams.shelfid + "'");
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


        public PageResult<PstMaterialRubberSplit> GetTablePageTotalBySqlPrint(QueryParams queryParams)
        {
            PageResult<PstMaterialRubberSplit> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.MaterCode, D.MaterialName, convert(varchar(10), A.OperTime, 120) OperDate, 
	                                A.ShiftID, F.ShiftName, A.ShiftClassID, G.ClassName, count(1) Num, SUM(A.Weight) TotalWeight,Sum(isnull(A.UsedWeight,0)) as UsedWeight, SUM(A.Weight)-Sum(isnull(A.UsedWeight,0)) as LeftWeight, A.OperPerson, E.UserName
                                from PstMaterialRubberSplit A
                                    left join BasLLShelf s on a.BarcodeSplit = s.barcode
	                                left join BasStorage B on A.StorageID = B.StorageID
	                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
	                                left join BasMaterial D on A.MaterCode = D.MaterialCode
	                                left join BasUser E on A.OperPerson = E.WorkBarcode
	                                left join PptShift F on A.ShiftID = F.ObjID
	                                left join PptClass G on A.ShiftClassID = G.ObjID
                                where 1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            {
                sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.operPerson))
            {
                sqlstr.AppendLine(" AND A.OperPerson = '" + queryParams.operPerson + "'");
            }
            if (queryParams.shiftID != "all")
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            }
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OperTime >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OperTime <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (queryParams.chejianCode != "all")
                sqlstr.AppendLine(" AND B.WorkShopCode = '" + queryParams.chejianCode + "'");
            if(!string.IsNullOrEmpty(queryParams.barcode))
                sqlstr.AppendLine(" AND (A.Barcode like '%" + queryParams.barcode + "%' or A.BarcodeSplit like '%" + queryParams.barcode + "%') ");
            if (!string.IsNullOrEmpty(queryParams.shelfid))
                sqlstr.AppendLine(" AND s.llshelfid = '"+queryParams.shelfid+"'");
            sqlstr.AppendLine(@" group by A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.MaterCode, D.MaterialName, convert(varchar(10), A.OperTime, 120),
                                A.ShiftID, F.ShiftName, A.ShiftClassID, G.ClassName, A.OperPerson, E.UserName");
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

        public PageResult<PstMaterialRubberSplit> GetTablePageOastBySqlPrint(QueryParams queryParams)
        {
            PageResult<PstMaterialRubberSplit> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"select A.StorageID, E.StorageName, A.StoragePlaceID, F.StoragePlaceName, MaterCode, B.MaterialName, 
	                            COUNT(1) Num, SUM(Weight - ISNULL(UsedWeight, 0)) TotalWeight
                            from PstMaterialRubberSplit A
	                            left join BasMaterial B on A.MaterCode = B.MaterialCode
	                            left join BasStorage E on A.StorageID = E.StorageID
	                            left join BasStoragePlace F on A.StoragePlaceID = F.StoragePlaceID
                                where 1 = 1 ");
            if (!string.IsNullOrEmpty(queryParams.storageID))
            {
                sqlstr.AppendLine(" AND A.StorageID = '" + queryParams.storageID + "'");
            }
            //if (!string.IsNullOrEmpty(queryParams.storagePlaceID))
            //{
            //    sqlstr.AppendLine(" AND A.StoragePlaceID = '" + queryParams.storagePlaceID + "'");
            //}
            if (!string.IsNullOrEmpty(queryParams.materCode))
            {
                sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.barcode))
            {
                sqlstr.AppendLine(" AND (A.BarCode = '"+queryParams.barcode+"' or a.barcodesplit = '"+queryParams.barcode+"') ");
            }
            //if (!string.IsNullOrEmpty(queryParams.operPerson))
            //{
            //    sqlstr.AppendLine(" AND A.OperPerson = '" + queryParams.operPerson + "'");
            //}
            //if (queryParams.shiftID != "all")
            //{
            //    sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            //}
            if (queryParams.beginDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OperTime >= '" + queryParams.beginDate.ToString() + "'");
            if (queryParams.endDate != DateTime.MinValue)
                sqlstr.AppendLine(" AND A.OperTime <= '" + queryParams.endDate.AddDays(1).ToString() + "'");
            if (queryParams.chejianCode != "all")
                sqlstr.AppendLine(" AND E.WorkShopCode = '" + queryParams.chejianCode + "'");
            sqlstr.AppendLine(@"group by A.StorageID, E.StorageName, A.StoragePlaceID, F.StoragePlaceName, MaterCode, B.MaterialName");
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
        public DataSet ProcUnReset(string BarCodeSplit, string OperPerson)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcUnReset");
            sps.AddInputParameter("BarCodeSplit", this.TypeToDbType(BarCodeSplit.GetType()), BarCodeSplit);
            sps.AddInputParameter("OperPerson", this.TypeToDbType(OperPerson.GetType()), OperPerson);
            return sps.ToDataSet();
        }
        public DataSet GetByPrintInfo(string BarcodeSplit)
        {
            string sql = @"select A.BillNo, A.StorageID, B.StorageName, A.StoragePlaceID, C.StoragePlaceName, A.Barcode, A.BarcodeSplit,
                                A.MaterCode, D.MaterialName, Weight, CONVERT(varchar(10), PlanDate, 120) PlanDate, A.ShiftID, E.ShiftName,
                                A.ShiftClassID, G.ClassName, case when PrintTime is null then '0' else '1' end IsPrint, A.PrintTime, F.UserName,
                                SUBSTRING(CONVERT(varchar, A.OperTime, 120), 6, 2) + '月' + SUBSTRING(CONVERT(varchar, A.OperTime, 120), 9, 2) + '日' DateDay,
                                SUBSTRING(CONVERT(varchar, A.OperTime, 120), 12, 2) + '时' + SUBSTRING(CONVERT(varchar, A.OperTime, 120), 15, 2) + '分' DateMinute,
                                SUBSTRING(CONVERT(varchar, DATEADD(HOUR, D.MinParkTime, A.OperTime), 120), 6, 2) + '月' + SUBSTRING(CONVERT(varchar, DATEADD(HOUR, D.MinParkTime, A.OperTime), 120), 9, 2) + '日' DateDay1,
                                SUBSTRING(CONVERT(varchar, DATEADD(HOUR, D.MinParkTime, A.OperTime), 120), 12, 2) + '时' + SUBSTRING(CONVERT(varchar, DATEADD(HOUR, D.MinParkTime, A.OperTime), 120), 15, 2) + '分' DateMinute1,
                                H.LLProductNo
                            from PstMaterialRubberSplit A
                                left join BasStorage B on A.StorageID = B.StorageID
                                left join BasStoragePlace C on A.StoragePlaceID = C.StoragePlaceID
                                left join BasMaterial D on A.MaterCode = D.MaterialCode
                                left join PptShift E on A.ShiftID = E.ObjID
                                left join BasUser F on A.PrintPerson = F.WorkBarcode
                                left join PptClass G on A.ShiftClassID = G.ObjID
                                left join PstShopStorage H on A.StorageID = H.StorageID and A.StoragePlaceID = H.StoragePlaceID and A.Barcode = H.Barcode
                                where A.BarcodeSplit = '" + BarcodeSplit + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetByInfo(string Barcode, string StorageID, string StoragePlaceID, string OperPerson, string OperDate)
        {
            string sql = @"select A.Barcode, A.BarcodeSplit, A.MaterCode, B.MaterialName, A.Weight, A.OperTime, A.PrintTime, A.PrintPerson, C.UserName,isnull(s.LLShelfID,p.LLShelfID) LLShelfID
                            from PstMaterialRubberSplit A 
                                left join BasLLShelf s on a.barcodesplit = s.barcode
                                left join PstMateiralSplitShelfCode p on a.barcodesplit = p.splistbarcode
	                            left join BasMaterial B on A.MaterCode = B.MaterialCode
	                            left join BasUser C on A.PrintPerson = C.WorkBarcode
                            where A.Barcode ='" + Barcode + "' and a.StorageID = '" + StorageID + "' and a.StoragePlaceID = '" + StoragePlaceID + "' and a.OperPerson = '" + OperPerson + "' and CONVERT(varchar(10), a.OperTime, 120) = '" + OperDate + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetByOastInfo(string StorageID, string StoragePlaceID, string MaterCode, string BeginDate, string EndDate,string barcode)
        {
            string sql = @"select A.Barcode, A.BarcodeSplit, A.MaterCode, B.MaterialName, A.Weight, A.OperTime, A.PrintTime, PrintPerson, C.UserName, A.UsedWeight,A.Weight-isnull(A.UsedWeight,0) as LeftWeight,A.EquipName,isnull(D.llshelfid,s.llshelfid) llshelfid
                            from PstMaterialRubberSplit A 
	                            left join BasMaterial B on A.MaterCode = B.MaterialCode
	                            left join BasUser C on A.PrintPerson = C.WorkBarcode
                                left join PstMateiralSplitShelfCode D on A.barcodesplit = D.SplistBarCode
                                left join BasLLShelf s on A.barcodesplit = s.barcode
                            where A.StorageID = '" + StorageID + "' and A.StoragePlaceID = '" + StoragePlaceID + "' and A.MaterCode = '" + MaterCode + "' and A.OperTime >= '" + BeginDate + "' and A.OperTime <= '" + EndDate + "'";
            if (!string.IsNullOrEmpty(barcode))
                sql += " and (A.BarCode = '" + barcode + "' or a.barcodesplit = '" + barcode + "')";
            return this.GetBySql(sql).ToDataSet();
        }

        public DataSet GetSqlInfo(string sql)
        {
            return this.GetBySql(sql).ToDataSet();
        }

        public string CancelBarcodeSplit(string storageID, string storagePlaceID, string barcodeSplit)
        {
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPstCancelMaterSplit");
            sps.AddInputParameter("StorageID", this.TypeToDbType(storageID.GetType()), storageID);
            sps.AddInputParameter("StoragePlaceID", this.TypeToDbType(storagePlaceID.GetType()), storagePlaceID);
            sps.AddInputParameter("BarcodeSplit", this.TypeToDbType(barcodeSplit.GetType()), barcodeSplit);

            return sps.ToDataSet().Tables[0].Rows[0][0].ToString();
        }

        public DataSet GetBarcodeSplitQuery(string barcode, string storageID, string storagePlaceID)
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(storagePlaceID))
                sql = @"select A.*, ISNULL(B.ChaiWeight, 0) ChaiWeight, case when A.StoreInWeight > ISNULL(B.ChaiWeight, 0)  then A.StoreInWeight - ISNULL(B.ChaiWeight, 0)  else 0 end UnChaiWeight from 
                        (
                        select A.Barcode, A.StorageID, A.StoragePlaceID, B.MaterCode, C.MaterialName, SUM(case when StoreInOut = 'I' then Weight else 0 end) StoreInWeight, SUM(case when StoreInOut = 'O' then Weight else 0 end) StoreOutWeight
                            ,SUM((case when StoreInOut = 'I' then 1 else -1 end) * Weight) RealWeight
                        from PstShopStorageDetail A
	                        left join PstShopStorage B on A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID and A.Barcode = B.Barcode
	                        left join BasMaterial C on B.MaterCode = C.MaterialCode
                        where A.StorageID = '" + storageID + @"' and A.StoragePlaceID = '" + storagePlaceID + @"' and A.Barcode = '" + barcode + @"'
                        group by A.Barcode, A.StorageID, A.StoragePlaceID, B.MaterCode, C.MaterialName
                        ) A left join (
                        select StorageID, StoragePlaceID, Barcode, sum(Weight) ChaiWeight 
                        from PstMaterialRubberSplit ps  where StorageID = '" + storageID + @"' and StoragePlaceID = '" + storagePlaceID + @"' and Barcode = '" + barcode + @"'
                        group by StorageID, StoragePlaceID, Barcode
                        ) B on A.Barcode = B.Barcode and A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID";
            else
                sql = @"select A.*, ISNULL(B.ChaiWeight, 0) ChaiWeight, case when A.StoreInWeight > ISNULL(B.ChaiWeight, 0) then A.StoreInWeight -ISNULL(B.ChaiWeight, 0)  else 0 end UnChaiWeight from 
                        (
                        select A.Barcode, A.StorageID, B.MaterCode, C.MaterialName, SUM(case when StoreInOut = 'I' then Weight else 0 end) StoreInWeight, SUM(case when StoreInOut = 'O' then Weight else 0 end) StoreOutWeight
	                        ,SUM((case when StoreInOut = 'I' then 1 else -1 end) * Weight) RealWeight
                        from PstShopStorageDetail A
	                        left join PstShopStorage B on A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID and A.Barcode = B.Barcode
	                        left join BasMaterial C on B.MaterCode = C.MaterialCode 
                        where A.StorageID = '" + storageID + @"' and A.Barcode = '" + barcode + @"'
                        group by A.Barcode, A.StorageID, A.StoragePlaceID, B.MaterCode, C.MaterialName
                        ) A left join (
                        select StorageID, Barcode, sum(Weight) ChaiWeight 
                        from PstMaterialRubberSplit where StorageID = '" + storageID + @"' and Barcode = '" + barcode + @"'
                        group by StorageID, StoragePlaceID, Barcode
                        ) B on A.Barcode = B.Barcode and A.StorageID = B.StorageID";

            return this.GetBySql(sql).ToDataSet();
        }
    }
}
