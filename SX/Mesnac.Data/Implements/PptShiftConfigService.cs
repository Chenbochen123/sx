using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public class PptShiftConfigService : BaseService<PptShiftConfig>, IPptShiftConfigService
    {
		#region 构造方法

        public PptShiftConfigService() : base(){ }

        public PptShiftConfigService(string connectStringKey) : base(connectStringKey){ }

        public PptShiftConfigService(NBear.Data.Gateway way) : base(way){ }

        #endregion 构造方法
        public class QueryParams
        {

            public string planDate { get; set; }
            public string startPlanDate { get; set; }
            public string endPlanDate { get; set; }
            public string ZJSID { get; set; }

         
            public string equipTypeName { get; set; }
            public string shiftID { get; set; }
            public string deleteFlag { get; set; }
            public string materialName { get; set; }
            public PageResult<PptShiftConfig> pageParams { get; set; }
        }

        public PageResult<PptShiftConfig> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PptShiftConfig> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT PlanDate,e.EquipName,s.ShiftName ShiftID,class.ClassName ClassID,MaterialName,Barcode
                                , CAST(BarcodeStart AS NVARCHAR(12))+'-' +CAST(barcodeEnd AS NVARCHAR(12)) BarcodeSE 
                                ,ShelfNum*TotalWeight SetTotalWeight
                                ,RealWeight
                                ,convert(varchar, RealWeight-ShelfNum*TotalWeight)  ConfigDValue
                                ,u.UserName OperCode,ReceiveDate,
                                DiffTime,MemNote, CASE WHEN UsedFlag=0 THEN '未用' WHEN UsedFlag=1 THEN '正用' WHEN UsedFlag=2 THEN '用完' END UsedFlag,
                                PrintDate,ShelfNum,UsedNum
                                ,UsedWeigh,UpdateFlag,BarcodeUse,OrgOrNot
                                FROM dbo.PptShiftConfig c LEFT JOIN dbo.BasUser u ON u.HRCode=c.OperCode
                                LEFT JOIN dbo.PptShift s ON s.ObjID=c.ShiftID LEFT JOIN dbo.PptClass class ON class.ObjID=c.ClassID
                                LEFT JOIN dbo.BasEquip e ON c.EquipCode = e.EquipCode
                                WHERE   1 = 1");
            if (!string.IsNullOrEmpty(queryParams.planDate))
            {
                sqlstr.AppendLine(" AND PlanDate = '" + queryParams.planDate+"'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipTypeName))
            {
                sqlstr.AppendLine(" AND c.EquipCode ='" + queryParams.equipTypeName + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND c.ShiftID = '" + queryParams.shiftID + "'");
            }
            //if (!string.IsNullOrEmpty(queryParams.deleteFlag))
            //{
            //    sqlstr.AppendLine(" AND DeleteFlag ='" + queryParams.deleteFlag + "'");
            //}
            if (!string.IsNullOrEmpty(queryParams.materialName))
            {
                sqlstr.AppendLine(" AND c.MaterialName ='" + queryParams.materialName + "'");
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
        public PageResult<PptShiftConfig> GetTablePageDataBySql2(QueryParams queryParams)
        {
            PageResult<PptShiftConfig> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT PlanDate
                                                  , MaterialName
                                                  ,cast(sum(RealWeight)/ 1000 as numeric(12, 3))  as RealWeight
                                                  FROM PptShiftConfig WHERE 1=1 and stockFlag<>4");
            if (queryParams.startPlanDate != "0001-01-01 0:00:00" && queryParams.startPlanDate != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine(" AND PlanDate >= '" + queryParams.startPlanDate + "'");
            }
            if (queryParams.endPlanDate != "0001-01-01 0:00:00" && queryParams.endPlanDate != "0001/1/1 0:00:00")
            {
                sqlstr.AppendLine(" AND PlanDate <= '" + queryParams.endPlanDate + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.ZJSID))
            {
                sqlstr.AppendLine(" AND ZJSID = (select MainHanderCode from BasMainHander where usercode='" + queryParams.ZJSID + "')");
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID) && queryParams.shiftID != "0")
            {
                sqlstr.AppendLine(" AND ShiftID = " + queryParams.shiftID);
            }

            sqlstr.AppendLine(" group by PlanDate,MaterialName  having COUNT(RealWeight)>0  order by PlanDate");

            pageParams.QueryStr = sqlstr.ToString();

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;

        }

        public DataSet GetMaterInfoList(string PlanDate, string ShiftID, string EquipCode)
        {
            string sql = @"select RecipeMaterialCode Id, RecipeMaterialName + '(' + B.ItemName + ')' Name, PlanID 
                            from PptPlan A
	                            left join SysCode B on B.TypeID = 'PmtType' and A.RecipeType = B.ItemCode
                            where PlanDate = '" + PlanDate + "'";
            if (!string.IsNullOrWhiteSpace(ShiftID))
                sql += " and ShiftID = '" + ShiftID + "'";
            if (!string.IsNullOrWhiteSpace(EquipCode))
                sql += " and RecipeEquipCode = '" + EquipCode + "'";

            return this.GetBySql(sql).ToDataSet();
        }
        public DataSet GetInfoByBarcode(string barcode, string userCode)
        {
            string sql = @"select A.Barcode, D.UserName, A.MaterialName, Convert(varchar, A.BarcodeStart) + '-' + Convert(varchar, A.BarcodeEnd) ShelfBarcode, B.ClassName, A.ProdDate, 
	                            case when C.MaxParkTime != null and C.MaxParkTime != 0 then DATEADD(DAY, C.MaxParkTime, A.ProdDate) else '2099-12-31 23:59:59' end ValidDate, A.BarcodeUse,
                                DATEPART(MONTH, A.ProdDate) date1, DATEPART(DAY, A.ProdDate) date2, DATEPART(HOUR, A.ProdDate) date3, DATEPART(MI, A.ProdDate) date4,
                                DATEPART(MONTH, case when C.MaxParkTime != null and C.MaxParkTime != 0 then DATEADD(DAY, C.MaxParkTime, A.ProdDate) else '2099-12-31 23:59:59' end) date5,
                                DATEPART(DAY, case when C.MaxParkTime != null and C.MaxParkTime != 0 then DATEADD(DAY, C.MaxParkTime, A.ProdDate) else '2099-12-31 23:59:59' end) date6,
                                DATEPART(HOUR, case when C.MaxParkTime != null and C.MaxParkTime != 0 then DATEADD(DAY, C.MaxParkTime, A.ProdDate) else '2099-12-31 23:59:59' end) date7,
                                DATEPART(MI, case when C.MaxParkTime != null and C.MaxParkTime != 0 then DATEADD(DAY, C.MaxParkTime, A.ProdDate) else '2099-12-31 23:59:59' end) date8
                            from PptShiftConfig A
	                            left join PptClass B on A.ClassID = B.ObjID
	                            left join BasMaterial C on A.MaterialCode = C.MaterialCode
	                            left join BasUser D on D.WorkBarcode = '" + userCode + @"'
                            where Barcode in (" + barcode + ")";
            return this.GetBySql(sql).ToDataSet();
        }
    }
}
