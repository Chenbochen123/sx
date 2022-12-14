using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    public class PstPlanGetMaterService : BaseService<PstPlanGetMater>, IPstPlanGetMaterService
    {
		#region 构造方法

        public PstPlanGetMaterService() : base(){ }

        public PstPlanGetMaterService(string connectStringKey) : base(connectStringKey){ }

        public PstPlanGetMaterService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法

        public class QueryParams
        {
            public DateTime planDate { get; set; }
            public string workShopCode { get; set; }
            public string storageID { get; set; }
            public string materType { get; set; }
            public PageResult<PstPlanGetMater> pageParams { get; set; }
        }

        public PageResult<PstPlanGetMater> GetTablePageDataBySql(QueryParams queryParams)
        {
            //全部Sql语句
            //select a.ObjID, a.PlanDate, a.EquipCode, e.EquipName, m.MaterialCode, m.MaterialName, a.StorageID, s.StorageName, round(a.TotalWeight,0) TotalWeight, ISNULL(r.RealGetWeight, 0) RealGetWeight
            //from PstPlanGetMater a 
            //    LEFT JOIN BasEquip e on a.EquipCode = e.EquipCode
            //    LEFT JOIN BasStorage s ON a.StorageID = s.StorageID
            //    LEFT JOIN BasMaterial m ON a.MaterialCode = m.MaterialCode
            //    LEFT JOIN (
            //        select B.MaterCode, ISNULL(SUM(Weight), 0) RealGetWeight from PstShopStorageDetail A
            //            LEFT JOIN PstShopStorage B on A.Barcode = B.Barcode and A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID
            //        where A.StoreInOut = 'I' and A.StorageID = '013' and A.RecordDate >= '2013-06-30 00:00:000' and A.RecordDate <= '2013-07-01 00:00:000' 
            //        Group by B.MaterCode
            //    ) r on a.MaterialCode = r.RealGetWeight
            //where a.PlanDate = '2013-06-30' and a.EquipCode = '01008' and s.StorageID = '013' and SUBSTRING(a.MaterialCode, 1, 3) in (select CONVERT(varchar, MajorID) + MinorTypeID from BasMaterialMinorType where Remark = '粉料类')


            PageResult<PstPlanGetMater> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();

            sqlstr.AppendLine(@"select a.ObjID, a.PlanDate, a.EquipCode, e.EquipName, m.MaterialCode, m.MaterialName, a.StorageID, s.StorageName,a.SourcePlace,a.Remark, round(a.TotalWeight,0) TotalWeight, ISNULL(r.RealGetWeight, 0) RealGetWeight, u.UserName
                                from PstPlanGetMater a 
                                    LEFT JOIN BasEquip e on a.EquipCode = e.EquipCode
	                                LEFT JOIN BasStorage s ON a.StorageID = s.StorageID
	                                LEFT JOIN BasMaterial m ON a.MaterialCode = m.MaterialCode
                                    LEFT JOIN BasUser u ON a.UserID = u.WorkBarcode
	                                LEFT JOIN (
		                                select A.StorageID, B.MaterCode, ISNULL(SUM(Weight), 0) RealGetWeight from PstShopStorageDetail A
			                                LEFT JOIN PstShopStorage B on A.Barcode = B.Barcode and A.StorageID = B.StorageID and A.StoragePlaceID = B.StoragePlaceID
		                                where A.StoreInOut = 'I'");
            if (!string.IsNullOrEmpty(queryParams.workShopCode))
                sqlstr.AppendLine(" and A.StorageID in (select StorageID from BasStorage where WorkShopCode = '" + queryParams.workShopCode + "')");
            if (!string.IsNullOrEmpty(queryParams.storageID))
                sqlstr.AppendLine(" and A.StorageID = '" + queryParams.storageID + "'");
            sqlstr.AppendLine(" and A.RecordDate >= '" + queryParams.planDate.ToString() + "' and A.RecordDate <= '" + queryParams.planDate.AddDays(1).ToString() + "' Group by A.StorageID, B.MaterCode ) r on a.MaterialCode = r.MaterCode and a.StorageID = r.StorageID");
            sqlstr.AppendLine(" where a.DeleteFlag = '0' and a.PlanDate = '" + queryParams.planDate.ToString() + "'");
            if (!string.IsNullOrEmpty(queryParams.workShopCode))
                sqlstr.AppendLine(" and a.StorageID in (select StorageID from BasStorage where WorkShopCode = '" + queryParams.workShopCode + "')");
            if (!string.IsNullOrEmpty(queryParams.storageID))
                sqlstr.AppendLine(" and a.StorageID = '" + queryParams.storageID + "'");
            if (queryParams.materType != "全部")
                sqlstr.AppendLine(" and SUBSTRING(a.MaterialCode, 1, 3) in (select CONVERT(varchar, MajorID) + MinorTypeID from BasMaterialMinorType where Remark = '" + queryParams.materType + "')");
            
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

        //判断指定日期是否有计划信息，如果没有，则执行存储过程，将指定日期下的计划写入计划表，否则提示没有数据
        public bool JudgeExistPlan(string PlanDate)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"declare @count int
                                select @count = COUNT(1) from PstPlanGetMater where PlanDate = '" + PlanDate + @"'
                                if @count = 0
                                begin exec ProcPlanGetMater '" + PlanDate + "', null, null end select COUNT(1) from PstPlanGetMater where PlanDate = '" + PlanDate + "'");
            int num = Convert.ToInt32(this.GetBySql(sqlstr.ToString()).ToScalar().ToString());
            if (num > 0)
                return true;
            else
                return false;
        }

        public DataSet GetPlanMaterInfo(string ObjID)
        {
            string sql = @"select a.PlanDate, a.EquipCode,a.SourcePlace,a.Remark,e.EquipName, m.MaterialCode, m.MaterialName, s.StorageID, s.StorageName, round(a.TotalWeight,0) TotalWeight
                            from PstPlanGetMater a 
                            LEFT JOIN BasEquip e ON a.EquipCode = e.EquipCode
                            LEFT JOIN BasStorage s ON a.StorageID = s.StorageID
                            LEFT JOIN BasMaterial m ON a.MaterialCode = m.MaterialCode
                        where a.ObjID = '" + ObjID + "'";
            return this.GetBySql(sql).ToDataSet();
        }

        public void ReSetMater(string PlanDate)
        {
            if (string.IsNullOrEmpty(PlanDate))
                PlanDate = DateTime.Now.ToString("yyyy-MM-dd");
            string EquipClass = "";
            string StorageID = "";
            NBear.Data.StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPlanGetMater");
            sps.AddInputParameter("PlanDate", this.TypeToDbType(PlanDate.GetType()), PlanDate);
            sps.AddInputParameter("EquipClass", this.TypeToDbType(EquipClass.GetType()), EquipClass);
            sps.AddInputParameter("StorageID", this.TypeToDbType(StorageID.GetType()), StorageID);
            sps.ExecuteNonQuery();
        }
    }
}
