using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using System.Data;
    using NBear.Data;
    using NBear.Common;
    using Mesnac.Data.Components;
    public class PptPlanService : BaseService<PptPlan>, IPptPlanService
    {
        #region 构造方法

        public PptPlanService() : base() { }

        public PptPlanService(string connectStringKey) : base(connectStringKey) { }

        public PptPlanService(NBear.Data.Gateway way) : base(way) { }

        #endregion 构造方法

        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 13:57:18
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            public QueryParams()
            {
                this.pageParams = new PageResult<PptPlan>();
            }
            public string planStartDate { get; set; }
            public string planID { get; set; }
            public string planEndDate { get; set; }
            public string equipTypeName { get; set; }
            public string shiftID { get; set; }
            public string deleteFlag { get; set; }
            public string materialCode { get; set; }
            public string classID { get; set; }
            public string equipCode { get; set; }
            public string recipeID { get; set; }
            public DateTime beginDate { get; set; }
            public DateTime endDate { get; set; }
            public string byGroup { get; set; }
            public string workShopCode { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string WorkShopCode { get; set; }
            public string StorageID { get; set; }
            public string StoragePlaceID { get; set; }
            public string ClassID { get; set; }
            public string EquipCode { get; set; }
            public string BasMaterial { get; set; }
            public string BasUser { get; set; }
            public string ToStorageID { get; set; }
            public string ToStoragePlaceID { get; set; }
            public string type { get; set; }
            public int page { get; set; }
            public int pagenum { get; set; }
            public string isout { get; set; }
            public PageResult<PptPlan> pageParams { get; set; }
        }

        public PageResult<PptPlan> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PptPlan> pageParams = queryParams.pageParams;
            //if (pageParams.PageSize < 0)
            //{
            //NBear.Data.CustomSqlSection css = this.GetBySql("ProcPlanRanRate");
            NBear.Data.StoredProcedureSection css = base.defaultGateway.FromStoredProcedure("ProcPlanRanRate");
            css.AddInputParameter("@PlanDate1", DbType.String, queryParams.planStartDate);
            css.AddInputParameter("@planDate2", DbType.String, queryParams.planEndDate);
            css.AddInputParameter("@WorkShopCode", DbType.String, queryParams.workShopCode);
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                css.AddInputParameter("@EquipCode", base.TypeToDbType(queryParams.equipCode.GetType()), queryParams.equipCode);
            }
            else
            {
                css.AddInputParameter("@EquipCode", base.TypeToDbType(queryParams.equipCode.GetType()), "");
            }
            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                css.AddInputParameter("@shiftId", base.TypeToDbType(queryParams.shiftID.GetType()), queryParams.shiftID);
            }
            else
            {
                css.AddInputParameter("@shiftId", DbType.String, "");
            }

            if (!string.IsNullOrEmpty(queryParams.classID))
            {
                css.AddInputParameter("@classId", base.TypeToDbType(queryParams.classID.GetType()), queryParams.classID);
            }
            else
            {
                css.AddInputParameter("@classId", DbType.String, "");
            }

            if (!string.IsNullOrEmpty(queryParams.materialCode))
            {
                css.AddInputParameter("@materCode", base.TypeToDbType(queryParams.materialCode.GetType()), queryParams.materialCode);
            }
            else
            {
                css.AddInputParameter("@materCode", DbType.String, "");
            }
            if (!string.IsNullOrEmpty(queryParams.equipTypeName))
            {
                css.AddInputParameter("@equipType", base.TypeToDbType(queryParams.equipTypeName.GetType()), queryParams.equipTypeName);
            }
            else
            {
                css.AddInputParameter("@equipType", DbType.String, "");
            }
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
            //}
            //else
            //{
            //    return null;// this.GetPageDataBySql(pageParams);
            //}
        }

        /// <summary>
        /// 返回指定车间的设备单位时间产量
        /// 王锴
        /// 2014-3-25
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="workShopCode"></param>
        /// <returns></returns>
        public DataSet GetEquipmentPruductionSummary(DateTime beginTime, DateTime endTime, string workShopCode, string shiftId, string equipmentCode)
        {
            String BeginTime = beginTime.ToString("yyyy-MM-dd");
            String EndTime = endTime.ToString("yyyy-MM-dd");
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendFormat(@"EXEC dbo.Proc_EquipCLRPT 
                                       @PSdate = '{0}',
                                       @PEdate = '{1}',
                                       @WorkShopCode = '{2}',
                                       @UseShiftID = '{3}',
                                       @EquipCode = '{4}'",BeginTime, EndTime, workShopCode, shiftId, equipmentCode);
            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToDataSet();
        }

        /// <summary>
        /// 返回指定机台计划信息
        /// 孙宜建
        /// 2013-2-20
        /// </summary>
        /// <param name="group">机台编码</param>
        /// <param name="date">计划日期</param>
        /// <returns></returns>
        public DataSet GetEquipPlan(string equipCode, string date)
        {
            DataSet ds = new DataSet();
            //早班信息
            string sql = @"SELECT ID=ROW_NUMBER() OVER(ORDER BY PriLevel),PlanID,PlanState,p.ShiftID,RecipeMaterialName,RecipeMaterialCode,RecipeEquipCode,RecipeName,PlanNum,PriLevel,p.ShiftID,ShiftName,OperDatetime,UserName,SerialNum
FROM dbo.PptPlan p LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID LEFT JOIN dbo.BasUser u ON u.WorkBarcode=p.OperCode
WHERE PlanDate = convert(datetime, '" + date + "', 120) AND p.ShiftID=1 AND RecipeEquipCode='" + equipCode + "' AND p.DeleteFlag=0 ORDER BY u.ObjID ;";

            //中班信息
            sql += @"SELECT ID=ROW_NUMBER() OVER(ORDER BY PriLevel),PlanID,PlanState,p.ShiftID,RecipeMaterialName,RecipeMaterialCode,RecipeEquipCode,RecipeName,PlanNum,PriLevel,p.ShiftID,ShiftName,OperDatetime,UserName,SerialNum
FROM dbo.PptPlan p LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID LEFT JOIN dbo.BasUser u ON u.WorkBarcode=p.OperCode
WHERE PlanDate = convert(datetime, '" + date + "', 120) AND  p.ShiftID=2 AND RecipeEquipCode='" + equipCode + "' AND p.DeleteFlag=0 ORDER BY u.ObjID ;";
            //夜班信息
            sql += @"SELECT ID=ROW_NUMBER() OVER(ORDER BY PriLevel), PlanID,PlanState,p.ShiftID,RecipeMaterialName,RecipeMaterialCode,RecipeEquipCode,RecipeName,PlanNum,PriLevel,p.ShiftID,ShiftName,OperDatetime,UserName,SerialNum
FROM dbo.PptPlan p LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID LEFT JOIN dbo.BasUser u ON u.WorkBarcode=p.OperCode
WHERE PlanDate = convert(datetime, '" + date + "', 120) AND  p.ShiftID=3 AND RecipeEquipCode='" + equipCode + "' AND p.DeleteFlag=0 ORDER BY u.ObjID ;";
            sql += @"
SELECT '888' ID,'' PlanID,'' PlanState,'' ShiftID,'' RecipeMaterialName,'' RecipeMaterialCode,'' RecipeEquipCode,'车数汇总' RecipeName,(select isnull(sum(PlanNum),0) from PptPlan 
where PlanDate = '" + date + @"' 
AND ShiftID = 1 
AND RecipeEquipCode = '" + equipCode + @"'
AND DeleteFlag=0) 
 PlanNum,'' PriLevel,'' ShiftID,'' ShiftName,'' OperDatetime,'' UserName,'' SerialNum 


select CONVERT(DECIMAL(18,1),sum(plan_num * (avg_allrtime+Min_PolyTime+Min_Bwbtime))/60.0) as PlanNum  from ppt_plan a left join Pmt_equipability b on a.Equip_code=b.Equip_code and a.Mater_code=b.Mater_code
where plan_date= '" + date + @"'  and a.equip_Code= '" + equipCode + @"' and shift_id=1
 ";
            sql += @"
SELECT '888' ID,'' PlanID,'' PlanState,'' ShiftID,'' RecipeMaterialName,'' RecipeMaterialCode,'' RecipeEquipCode,'车数汇总' RecipeName,(select isnull(sum(PlanNum),0) from PptPlan 
where PlanDate = '" + date + @"' 
AND ShiftID = 3 
AND RecipeEquipCode = '" + equipCode + @"'
AND DeleteFlag=0) 
 PlanNum,'' PriLevel,'' ShiftID,'' ShiftName,'' OperDatetime,'' UserName,'' SerialNum 


select CONVERT(DECIMAL(18,1),sum(plan_num * (avg_allrtime+Min_PolyTime+Min_Bwbtime))/60.0) as PlanNum  from ppt_plan a left join Pmt_equipability b on a.Equip_code=b.Equip_code and a.Mater_code=b.Mater_code
where plan_date= '" + date + @"'  and a.equip_Code= '" + equipCode + @"' and shift_id=3
 "; 
            ds = this.GetBySql(sql).ToDataSet();

            return ds;
        }

        /// <summary>
        /// 带条件查询返回指定机台计划信息
        /// 袁洋
        /// 2013-3-21
        /// </summary>
        /// <param name="equipCode">机台编码</param>
        /// <param name="date">计划日期</param>
        /// <param name="shiftid">班次号</param>
        /// <param name="recipematerialcode">配方物料代码</param>
        /// <param name="recipename">配方名称</param>
        /// <returns></returns>
        public DataSet GetEquipPlan(string equipCode, string date, string shiftid, string recipematerialcode, string recipename)
        {
            DataSet ds = new DataSet();
            string sql = "";
            switch (shiftid)
            {
                case "1":
                    //早班信息
                    sql = @"SELECT ID=ROW_NUMBER() OVER(ORDER BY PriLevel), PlanID,PlanState, p.ShiftID, RecipeMaterialName, 
                            RecipeMaterialCode, RecipeEquipCode, RecipeName, PlanNum, PriLevel, p.ShiftID, ShiftName, OperDatetime, 
                            UserName,SerialNum
                            FROM dbo.PptPlan p  LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID 
                                                LEFT JOIN dbo.BasUser u ON u.WorkBarcode=p.OperCode
                            WHERE PlanDate = '" + date + @"' 
                                  AND p.ShiftID = 1 
                                  AND RecipeEquipCode = '" + equipCode + @"' 
                                  AND p.DeleteFlag=0 ";
                    if (recipematerialcode != null && recipematerialcode != "")
                    {
                        sql += " AND RecipeMaterialCode = '" + recipematerialcode + "' ";
                    }
                    if (recipename != null && recipename != "")
                    {
                        sql += " AND RecipeName = '" + recipename + "' ";
                    }
                    
            sql += @"
SELECT '888' ID,'' PlanID,'' PlanState,'' ShiftID,'' RecipeMaterialName,'' RecipeMaterialCode,'' RecipeEquipCode,'车数汇总' RecipeName,(select isnull(sum(PlanNum),0) from PptPlan 
where PlanDate = '" + date + @"' 
AND ShiftID = 1 
AND RecipeEquipCode = '" + equipCode + @"'
AND DeleteFlag=0) 
 PlanNum,'' PriLevel,'' ShiftID,'' ShiftName,'' OperDatetime,'' UserName,'' SerialNum 


select CONVERT(DECIMAL(18,1),sum(plan_num * (avg_allrtime+Min_PolyTime+Min_Bwbtime))/60.0) as PlanNum  from ppt_plan a left join Pmt_equipability b on a.Equip_code=b.Equip_code and a.Mater_code=b.Mater_code
where plan_date= '" + date + @"'  and a.equip_Code= '" + equipCode + @"' and shift_id=1
 "; 

                    break;
                case "2":
                    //中班信息
                    sql = @"SELECT ID=ROW_NUMBER() OVER(ORDER BY PriLevel), PlanID,PlanState, p.ShiftID, RecipeMaterialName, 
                            RecipeMaterialCode, RecipeEquipCode, RecipeName, PlanNum, PriLevel, p.ShiftID, ShiftName, OperDatetime, 
                            UserName,SerialNum
                            FROM dbo.PptPlan p  LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID 
                                                LEFT JOIN dbo.BasUser u ON u.WorkBarcode=p.OperCode
                            WHERE PlanDate = '" + date + @"' 
                                  AND p.ShiftID = 2 
                                  AND RecipeEquipCode = '" + equipCode + @"' 
                                  AND p.DeleteFlag=0 ";
                    if (recipematerialcode != null && recipematerialcode != "")
                    {
                        sql += " AND RecipeMaterialCode = '" + recipematerialcode + "' ";
                    }
                    if (recipename != null && recipename != "")
                    {
                        sql += " AND RecipeName = '" + recipename + "' ";
                    }
                    sql += " ORDER BY u.ObjID ;";
                    break;
                case "3":
                    //夜班信息
                    sql = @"SELECT ID=ROW_NUMBER() OVER(ORDER BY PriLevel), PlanID,PlanState, p.ShiftID, RecipeMaterialName, 
                            RecipeMaterialCode, RecipeEquipCode, RecipeName, PlanNum, PriLevel, p.ShiftID, ShiftName, OperDatetime, 
                            UserName,SerialNum
                            FROM dbo.PptPlan p  LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID 
                                                LEFT JOIN dbo.BasUser u ON u.WorkBarcode=p.OperCode
                            WHERE PlanDate = '" + date + @"' 
                                  AND p.ShiftID = 3 
                                  AND RecipeEquipCode = '" + equipCode + @"' 
                                  AND p.DeleteFlag=0 ";
                    if (recipematerialcode != null && recipematerialcode != "")
                    {
                        sql += " AND RecipeMaterialCode = '" + recipematerialcode + "' ";
                    }
                    if (recipename != null && recipename != "")
                    {
                        sql += " AND RecipeName = '" + recipename + "' ";
                    }
                    
    sql += @"
SELECT '888' ID,'' PlanID,'' PlanState,'' ShiftID,'' RecipeMaterialName,'' RecipeMaterialCode,'' RecipeEquipCode,'车数汇总' RecipeName,(select isnull(sum(PlanNum),0) from PptPlan 
where PlanDate = '" + date + @"' 
AND ShiftID = 3 
AND RecipeEquipCode = '" + equipCode + @"'
AND DeleteFlag=0) 
 PlanNum,'' PriLevel,'' ShiftID,'' ShiftName,'' OperDatetime,'' UserName,'' SerialNum 


select CONVERT(DECIMAL(18,1),sum(plan_num * (avg_allrtime+Min_PolyTime+Min_Bwbtime))/60.0) as PlanNum from ppt_plan a left join Pmt_equipability b on a.Equip_code=b.Equip_code and a.Mater_code=b.Mater_code
where plan_date= '" + date + @"'  and a.equip_Code= '" + equipCode + @"' and shift_id=3 ";             break;
                default:
                    break;
            }
            ds = this.GetBySql(sql).ToDataSet();

            return ds;
        }

        /// <summary>
        /// 计划执行监控
        /// 2013-03-7
        /// 孙宜建
        /// </summary>
        /// <param name="equipCode">机台</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public DataSet GetPlanMonitor(string equipCode, string date)
        {
            DataSet ds = new DataSet();
            //早班信息
            string sql = @"SELECT PlanID,RecipeMaterialName+'('+case RecipeType when 0 then '正常' when 1 then '试制' when 2 then '返炼' when 3 then '试验' when 4 then '欧盟' when 5 then '掺胶' else '' end+')' RecipeMaterialName,PlanDate,RecipeEquipCode,CONVERT(VARCHAR(5),RealNum)+'/'+CONVERT(VARCHAR(5),PlanNum) RealPlanNum,CAST(ROUND(RealNum*1.0/PlanNum,4) AS numeric(5,2)) AS Num,CONVERT(VARCHAR(5),CAST(ROUND((RealNum*1.0/PlanNum)*100,3) AS numeric(5,0)))+'%' AS Per,RealEndtime 
 FROM dbo.PptPlan WHERE PlanDate = '" + date + "' AND ShiftID=1 AND PlanNum!=0 AND RecipeEquipCode='" + equipCode + "' AND DeleteFlag=0 ORDER BY PriLevel ;";

            //中班信息
            sql += @"SELECT PlanID,RecipeMaterialName+'('+case RecipeType when 0 then '正常' when 1 then '试制' when 2 then '返炼' when 3 then '试验' when 4 then '欧盟' when 5 then '掺胶' else '' end+')' RecipeMaterialName,PlanDate,RecipeEquipCode,CONVERT(VARCHAR(5),RealNum)+'/'+CONVERT(VARCHAR(5),PlanNum) RealPlanNum,CAST(ROUND(RealNum*1.0/PlanNum,4) AS numeric(5,2)) AS Num,CONVERT(VARCHAR(5),CAST(ROUND((RealNum*1.0/PlanNum)*100,3) AS numeric(5,0)))+'%' AS Per,RealEndtime 
 FROM dbo.PptPlan WHERE PlanDate = '" + date + "' AND ShiftID=2  AND PlanNum!=0 AND RecipeEquipCode='" + equipCode + "' AND DeleteFlag=0 ORDER BY PriLevel ;";
            //夜班信息
            sql += @"SELECT PlanID,RecipeMaterialName+'('+case RecipeType when 0 then '正常' when 1 then '试制' when 2 then '返炼' when 3 then '试验' when 4 then '欧盟' when 5 then '掺胶' else '' end+')' RecipeMaterialName,PlanDate,RecipeEquipCode,CONVERT(VARCHAR(5),RealNum)+'/'+CONVERT(VARCHAR(5),PlanNum) RealPlanNum,CAST(ROUND(RealNum*1.0/PlanNum,4) AS numeric(5,2)) AS Num,CONVERT(VARCHAR(5),CAST(ROUND((RealNum*1.0/PlanNum)*100,3) AS numeric(5,0)))+'%' AS Per,RealEndtime 
 FROM dbo.PptPlan WHERE PlanDate = '" + date + "' AND ShiftID=3 AND PlanNum!=0 AND RecipeEquipCode='" + equipCode + "' AND DeleteFlag=0 ORDER BY PriLevel ;";
            ds = this.GetBySql(sql).ToDataSet();

            return ds;
        }

        /// <summary>
        /// 获取药品的计划排产信息
        /// 孙宜建
        /// 2013-3-2
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="equipCode">机台</param>
        /// <param name="shiftId">班次</param>
        /// <returns></returns>
        public DataSet GetXLPlanCreate(string date, string equipCode, string shiftId)
        {

            string[] paramNames ={
                this.defaultGateway.BuildDbParamName("Plandate"),
                this.defaultGateway.BuildDbParamName("Equip_code"),
                this.defaultGateway.BuildDbParamName("Shift_id")
            };
            object[] paramValues = {
                date,
                equipCode,
                shiftId
            };
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcXLPlanCreate");
            for (int i = 0; i < paramNames.Length; i++)
            {
                sps.AddInputParameter(paramNames[i], this.TypeToDbType(paramValues[i].GetType()), paramValues[i]);
            }
            return sps.ToDataSet();
        }

        /// <summary>
        /// 批量下达计划
        /// 孙宜建 2013-3-1
        /// </summary>
        /// <param name="equipCode">机台</param>
        /// <param name="date">日期</param>
        /// <param name="shiftid">班次</param>
        /// <returns></returns>
        public bool AllUpdatePlanState(string equipCode, string date, string shiftid)
        {
            string sql = @"UPDATE dbo.PptPlan SET PlanState=2 
                           WHERE PlanDate = '" + date + "' AND ShiftID='" + shiftid + "' AND RecipeEquipCode='" + equipCode + "' AND DeleteFlag=0 AND PlanState=1";
            int i = 0;
            i = this.GetBySql(sql).ExecuteNonQuery();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region IPptPlanService 成员

        /// <summary>
        /// 返回新增计划号
        /// 孙宜建
        /// 2013-2-26
        /// </summary>
        /// <param name="date">日期XXXX-XX-XX</param>
        /// <param name="equipCode">机台编码</param>
        /// <param name="shiftid">班次</param>
        /// <returns></returns>
        public string GetGetMaxPlanId(string date, string equipCode, string shiftid)
        {
            string[] paramNames ={
                this.defaultGateway.BuildDbParamName("plan_date"),
                this.defaultGateway.BuildDbParamName("equipcode"),
                this.defaultGateway.BuildDbParamName("ban")
            };
            object[] paramValues = {
                date,
                equipCode,
                shiftid
            };
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcPptGetMaxPlanId");
            for (int i = 0; i < paramNames.Length; i++)
            {
                sps.AddInputParameter(paramNames[i], this.TypeToDbType(paramValues[i].GetType()), paramValues[i]);
            }
            return sps.ToScalar().ToString();
        }

        /// <summary>
        /// 插入计划时更改插入后的优先级
        /// sunyj
        /// 2013-2-27
        /// </summary>
        /// <param name="date">计划日期</param>
        /// <param name="equipCode">机台编号</param>
        /// <param name="shiftid">班次</param>
        /// <param name="priLevel">插入的优先级</param>
        public bool UpdatePriLevel(string date, string equipCode, string shiftid, int priLevel)
        {

            string sql = @"UPDATE dbo.PptPlan SET PriLevel=PriLevel+1
                           WHERE PlanDate='{0}'  AND RecipeEquipCode='{1}' AND ShiftID={2} AND PriLevel>={3}";

            sql = string.Format(sql, date, equipCode, shiftid, priLevel);
            try
            {
                this.GetBySql(sql).ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取生产计划的领料单
        /// 2013-4-1
        /// 孙宜建
        /// </summary>
        /// <param name="date">计划日期</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetSumDayMater(string date, string type,string storeid)
        {
            string[] paramNames ={
                this.defaultGateway.BuildDbParamName("PlanDate"),
                this.defaultGateway.BuildDbParamName("EquipClass"),
                this.defaultGateway.BuildDbParamName("storID")
                //huiw,2013-10-24注释
                //this.defaultGateway.BuildDbParamName("storID"),
                //this.defaultGateway.BuildDbParamName("storID")

            };
            object[] paramValues = {
                date,
                type,
                storeid
                //huiw,2013-10-24注释
                //"",
                //""
            };
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("ProcSumDayMater");
            for (int i = 0; i < paramNames.Length; i++)
            {
                sps.AddInputParameter(paramNames[i], this.TypeToDbType(paramValues[i].GetType()), paramValues[i]);
            }
            return sps.ToDataSet().Tables[0];
        }
        /// <summary>
        /// 获取小料称量查询中的计划信息
        /// 孙宜建
        /// 2013-4-2
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public PageResult<PptPlan> GetSmallPlanTablePageDataBySql(PptPlanService.QueryParams queryParams)
        {
            PageResult<PptPlan> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@" SELECT distinct p.PlanID,p.PlanDate,s.ShiftName,c.ClassName,RecipeMaterialCode,RecipeMaterialName,PlanNum,RealNum,RealStartTime,RealEndtime 
                                    FROM dbo.PptPlan p
                                    LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID
                                    LEFT JOIN dbo.PptClass c ON c.ObjID=p.ClassID
                                    LEFT JOIN dbo.PptWeighData w ON p.PlanID=w.PlanID   
                                 WHERE      1 = 1 and LEFT(RecipeMaterialCode,1)=2  ");
            if (!string.IsNullOrEmpty(queryParams.planID))
            {
                sqlstr.AppendLine(" AND p.PlanID='" + queryParams.planID + "'");
            }
            else
            {
                if (!string.IsNullOrEmpty(queryParams.equipCode))
                {
                    sqlstr.AppendLine(" AND w.EquipCode= '" + queryParams.equipCode + "'");
                }
                if (!string.IsNullOrEmpty(queryParams.planStartDate))
                {
                    sqlstr.AppendLine(" AND p.PlanDate='" + queryParams.planStartDate + "'");
                }
                if (!string.IsNullOrEmpty(queryParams.shiftID))
                {
                    sqlstr.AppendLine(" AND p.ShiftID= " + queryParams.shiftID);
                }

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

        #endregion


        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 13:33:01
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PptPlan> GetPlanLotReportPageDataBySql(QueryParams queryParams)
        {
            PageResult<PptPlan> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            if (string.IsNullOrWhiteSpace(queryParams.materialCode))
            {
                sqlstr.AppendLine(@"SELECT * FROM (SELECT  t1.RecipeMaterialCode,'' AS RecipeMaterialName,SUM(t1.PlanNum) AS PlanNum,SUM(t1.RealNum) AS RealNum  FROM  dbo.PptPlan t1");
            }
            else
            {
                sqlstr.AppendLine(@"SELECT  t1.*,tp1.ItemName AS PlanStateName,tp2.ClassName,tp3.ShiftName FROM  dbo.PptPlan t1
                                    LEFT JOIN dbo.SysCode tp1 ON t1.PlanState=tp1.ItemCode AND tp1.TypeID='PlanState'
                                    LEFT JOIN dbo.PptClass tp2 ON t1.ClassID=tp2.ObjID
                                    LEFT JOIN dbo.PptShift tp3 ON t1.ShiftID=tp3.ObjID");
            }
            sqlstr.AppendLine(@"LEFT JOIN dbo.PmtRecipe t2 ON t1.RecipeEquipCode=t2.RecipeEquipCode AND t1.RecipeMaterialCode=t2.RecipeMaterialCode AND t1.RecipeVersionID=t2.RecipeVersionID
                             WHERE 1=1");

            if (!string.IsNullOrWhiteSpace(queryParams.materialCode))
            {
                sqlstr.AppendLine(" AND t1.RecipeMaterialCode= '" + queryParams.materialCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND t1.RecipeEquipCode= '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.planStartDate))
            {
                sqlstr.AppendLine(" AND t1.PlanDate>= convert(datetime, '" + queryParams.planStartDate + "', 120)"); 
            }
            if (!string.IsNullOrWhiteSpace(queryParams.planEndDate))
            {
                sqlstr.AppendLine(" AND t1.PlanDate <= convert(datetime, '" + queryParams.planEndDate + "', 120)"); 
            }
            if (!string.IsNullOrWhiteSpace(queryParams.classID))
            {
                sqlstr.AppendLine(" AND t1.ClassID= '" + queryParams.classID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND t1.ShiftID= '" + queryParams.shiftID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.recipeID))
            {
                sqlstr.AppendLine(" AND t2.ObjID= '" + queryParams.recipeID + "'");
            }


            if (string.IsNullOrWhiteSpace(queryParams.materialCode))
            {
                sqlstr.AppendLine(" GROUP BY t1.RecipeMaterialCode) t1");
            }
            sqlstr.AppendLine("ORDER BY t1.RecipeMaterialCode");

            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataByReader(pageParams);
            }
        }

        public EntityArrayList<BasMaterial> GetPlanPptMaterial(QueryParams queryParams)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine(@"SELECT t1.* FROM dbo.BasMaterial t1
                    INNER JOIN 
                    (SELECT DISTINCT t1.RecipeMaterialCode FROM dbo.PptPlan t1 WHERE 1=1");
            if (!string.IsNullOrWhiteSpace(queryParams.materialCode)&&queryParams.materialCode!="全部")
            {
                sqlstr.AppendLine(" AND t1.RecipeMaterialCode= '" + queryParams.materialCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND t1.RecipeEquipCode= '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.planStartDate))
            {
                sqlstr.AppendLine(" AND t1.PlanDate>= '" + queryParams.planStartDate + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.planEndDate))
            {
                sqlstr.AppendLine(" AND t1.PlanDate <= '" + queryParams.planEndDate + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.classID))
            {
                sqlstr.AppendLine(" AND t1.ClassID= '" + queryParams.classID + "'");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND t1.ShiftID= '" + queryParams.shiftID + "'");
            }
            sqlstr.AppendLine(@"
                        ) t2 ON t1.MaterialCode=t2.RecipeMaterialCode
                        ORDER BY t1.MaterialName");

            NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
            return css.ToArrayList<BasMaterial>();
        }
        /// <summary>
        /// 主表头信息
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataSet GetRptPlanLotMain(string planID)
        {
            string sqlstr = @"  SELECT		p.RecipeName , equip.EquipName as equipCode , p.RealStartTime , p.RealEndtime , 
			                                c.ItemName , shift.ShiftName + '—' + class.ClassName as ShiftClassName , p.RealNum ,
			                                p.PlanNum , p.RecipeMaterialName
						
                                FROM		PptLotData lot 
                                LEFT JOIN	PptPlan p			ON		P.PlanID = lot.PlanID
                                LEFT JOIN	PmtRecipe re		ON		re.RecipeMaterialCode = p.RecipeMaterialCode
								                                AND		re.RecipeMaterialName = p.RecipeMaterialName
								                                AND		re.RecipeEquipCode = p.RecipeEquipCode
                                LEFT JOIN	SysCode	c			ON		c.ItemCode = re.RecipeType 
								                                AND		c.TypeID = 'PmtType'
                                LEFT JOIN	PptShift shift		ON		shift.ObjID = p.ShiftID
                                LEFT JOIN	PptClass class		ON		class.ObjID = p.ClassID
                                LEFT JOIN	BasEquip equip		ON		equip.EquipCode = lot.EquipCode
                                WHERE      	p.PlanID = '" + planID + "'";

            DataSet ds = this.GetBySql(sqlstr).ToDataSet();
            return ds;
        }
        /// <summary>
        /// 主表平均值和总和信息
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataSet GetRptPlanLotMainAvgAndSum(string planID)
        {
            string sqlstr = @"  SELECT  sum(DoneAllRtime) as sumDoneAllRtime,avg(DoneAllRtime) as avgDoneAllRtime,
		                                sum(DoneRtime) as sumDoneRtime,avg(DoneRtime) as avgDoneRtime,
		                                sum(BwbTime) as sumBwbTime ,avg(BwbTime) as avgBwbTime,
		                                sum(PolyDisTime) as sumPolyDistime,avg(PolyDisTime) as avgPolydistime,
		                                sum(CBDisTime) as sumCBDistime,avg(CBDisTime) as avgCBdistime,
		                                sum(PjTemp) as sumPjTemp,avg(PjTemp) as avgPjTemp,
		                                sum(PjPower) as sumPjPower,round(avg(PjPower),2) as avgPjPower,
		                                sum(LotEnergy) as sumLotEnergy,round(avg(LotEnergy),2) as avgLotEnergy,
		                                sum(RealWeight) as sumRealweight,avg(RealWeight) as avgRealweight,
		                                max(DoneAllRtime) as maxDoneAllRtime,min(DoneAllRtime) as minDoneAllRtime,
		                                max(DoneRtime) as maxDoneRtime,min(DoneRtime) as minDoneRtime,
		                                max(BwbTime) as maxBwbTime ,min(BwbTime) as minBwbTime,
		                                max(PjTemp) as maxPjTemp,min(PjTemp) as minPjTemp,
		                                max(PjPower) as maxPjPower,min(PjPower) as minPjPower,
		                                max(LotEnergy) as maxLotEnergy,min(LotEnergy) as minLotEnergy,
		                                STDEV(DoneAllRtime) as STDDoneAllRtime,STDEV(DoneRtime) as STDDoneRtime,
		                                STDEV(BwbTime) as STDBwbTime,STDEV(PjTemp) as STDPjTemp,
		                                STDEV(PjPower) as STDPjPower,STDEV(LotEnergy) as STDLotEnergy 
                                FROM    PptLotData 
                                WHERE   PlanID = '" + planID + "'";

            DataSet ds = this.GetBySql(sqlstr).ToDataSet();
            return ds;
        }

        /// <summary>
        /// 从表对应称量信息
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataSet GetRptPlanLotMaterialDetailInfo(string planID)
        {
            string sqlstr = @"  SELECT		a.Barcode,a.SetWeight,a.RealWeight,
                                CASE		a.WeighType WHEN '1' THEN '炭黑' WHEN '3' THEN '胶料' WHEN '2' THEN '油' WHEN '4' THEN '小料' 
                                ELSE		a.WeighType 
                                END AS		WeighType ,a.WeightID,a.MaterCode,a.MaterName,b.MaterName  AS RecipeName, 
			                                b.SerialID AS SerialID,b.StartDatetime AS StartDatetime,b.DoneRtime AS DoneRtime,
			                                b.DoneAllRtime AS DoneAllRtime,BwbTime AS BwbTime,PjTemp AS PjTemp,PolyDisTime AS PolyDisTime,
			                                CBDisTime AS CBDisTime,OilDisTime AS OilDisTime,PjPower AS PjPower,LotEnergy AS LotEnergy
                                FROM		dbo.PptWeighData a 
                                LEFT JOIN	PptLotData  b ON a.Barcode=b.Barcode 
                                WHERE       a.PlanID = '" + planID + "'";
            sqlstr+=        "   ORDER BY	Barcode,WeighType,WeightID,a.Matercode ";
            DataSet ds = this.GetBySql(sqlstr).ToDataSet();
            return ds;
        }

        /// <summary>
        /// 从表矩阵胶料名称信息
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataSet GetRptPlanLotRubsDetailInfo(string planID)
        {
            string sqlstr = @"  SELECT		distinct rtrim(a.WeighType) as WeighType, a.SetWeight, 
			                                MaterName, rtrim(a.MaterCode) as MaterCode, a.WeightID,
			                                rtrim(a.MaterCode)+convert(nvarchar(2),a.WeightID) as MaterCodeTemp  
                                FROM		PptWeighData as a 
                                WHERE		a.Matercode>='0' 
                                AND         a.PlanID = '" + planID + "'";
            sqlstr += "         ORDER BY	WeighType,a.WeightID,MaterCode ";
            DataSet ds = this.GetBySql(sqlstr).ToDataSet();
            return ds;
        }

        /// <summary>
        /// 从表总计和信息
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataSet GetRptPlanLotSumDetailInfo(string planID)
        {
            string sqlstr = @" SELECT	SumWeight = (Select Sum(RealWeight) From PptWeighData Where Barcode = A.Barcode),

        a.barcode,A.serialid 
        ,Convert(nvarchar(20),A.startdatetime,114) as startdatetime,A.startdatetime as startdatetime1,
		A.doneallrtime, A.donertime,A.bwbtime,A.PolyDisTime, A.CBDisTime, A.OilDisTime, [PjTemp] as PjTemp,A.pjpower ,
        A.PjEner as PjEner
FROM	PptLotData a 
      WHERE	    barcode like '" + planID+"%' ORDER BY	barcode " ;
       
            DataSet ds = this.GetBySql(sqlstr).ToDataSet();
            return ds;
        }

        public DataSet GetPlanTotalReport(string byGroup, DateTime beginDate, DateTime endDate, string shiftID, string classID, string equipCode, string materCode)
        {
            string sql = "";
            sql += "select " + byGroup;
            sql += @"F.UserName, SUM(RealNum) TotalNum, SUM(RealNum*TotalWeight) TotalWeight
                        from PptPlan A
	                        left join PptShift B on A.ShiftID = B.ObjID
	                        left join PptClass C on A.ClassID = C.ObjID
	                        left join BasEquip D on A.RecipeEquipCode = D.EquipCode
	                        left join BasMainHander E on A.UserID = E.UserCode
	                        left join BasUser F on E.UserCode = F.HRCode 
                        where 1 = 1";
            if (beginDate != DateTime.MinValue)
                sql += " and PlanDate >= '" + beginDate.ToString() + "'";
            if (endDate != DateTime.MinValue)
                sql += " and PlanDate <= '" + endDate.AddDays(1).ToString() + "'";
            if (!string.IsNullOrEmpty(shiftID))
                sql += " and A.ShiftID = '" + shiftID + "'";
            if (!string.IsNullOrEmpty(classID))
                sql += " and ClassID = '" + classID + "'";
            if (!string.IsNullOrEmpty(equipCode))
                sql += " and RecipeEquipCode = '" + equipCode + "'";
            if (!string.IsNullOrEmpty(materCode))
                sql += " and RecipeMaterialCode = '" + materCode + "'";
            sql += " group by " + byGroup.Replace(" ProdDate", "");
            if (!string.IsNullOrEmpty(byGroup))
                sql += " , F.UserName";
            else
                sql += " F.UserName";

            return this.GetBySql(sql).ToDataSet();
        }

        public PageResult<PptPlan> GetTableTotalDataBySql(QueryParams queryParams)
        {
            PageResult<PptPlan> pageParams = queryParams.pageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.AppendLine("select " + queryParams.byGroup);
            sqlstr.AppendLine(@"F.UserName, SUM(RealNum) TotalNum, SUM(RealNum*TotalWeight) TotalWeight
                        from PptPlan A
	                        left join PptShift B on A.ShiftID = B.ObjID
	                        left join PptClass C on A.ClassID = C.ObjID
	                        left join BasEquip D on A.RecipeEquipCode = D.EquipCode
	                        left join BasUser F on A.UserID = F.HRCode 
                        where A.DeleteFlag = '0'");
            if (queryParams.beginDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
            }
            if (queryParams.endDate != DateTime.MinValue)
            {
                sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.ToString() + "'");
            }
            if (queryParams.shiftID != "all")
            {
                sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
            }
            if (queryParams.classID != "all")
            {
                sqlstr.AppendLine(" AND A.ClassID = '" + queryParams.classID + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND A.RecipeEquipCode = '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.materialCode))
            {
                sqlstr.AppendLine(" AND A.RecipeMaterialCode = '" + queryParams.materialCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.BasUser))
            {
                sqlstr.AppendLine(" AND A.UserID = '" + queryParams.BasUser + "'");
            }
            sqlstr.AppendLine(" AND SUBSTRING(D.EquipName, 2, 1) = '" + queryParams.workShopCode + "'");
            sqlstr.AppendLine(" group by " + queryParams.byGroup.Replace(" ProdDate", ""));

            sqlstr.AppendLine(" F.UserName");
            sqlstr.AppendLine(" having  SUM(RealNum*TotalWeight)<>0 ");
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
        public DataSet CompoundQuery(QueryParams queryParams)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("Proc_CompoundQueryAll");
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StartDate"), this.TypeToDbType(queryParams.StartDate.GetType()), queryParams.StartDate);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EndDate"), this.TypeToDbType(queryParams.EndDate.GetType()), queryParams.EndDate);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("WorkShopCode"), this.TypeToDbType(queryParams.WorkShopCode.GetType()), queryParams.WorkShopCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StorageID"), this.TypeToDbType(queryParams.StorageID.GetType()), queryParams.StorageID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StoragePlaceID"), this.TypeToDbType(queryParams.StoragePlaceID.GetType()), queryParams.StoragePlaceID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("GroupID"), this.TypeToDbType(queryParams.shiftID.GetType()), queryParams.shiftID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ClassID"), this.TypeToDbType(queryParams.ClassID.GetType()), queryParams.ClassID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipCode"), this.TypeToDbType(queryParams.EquipCode.GetType()), queryParams.EquipCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("BasMaterial"), this.TypeToDbType(queryParams.BasMaterial.GetType()), queryParams.BasMaterial);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("IsOut"), this.TypeToDbType(queryParams.isout.GetType()), queryParams.isout);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ToStorageID"), this.TypeToDbType(queryParams.ToStorageID.GetType()), queryParams.ToStorageID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ToStoragePlaceID"), this.TypeToDbType(queryParams.ToStoragePlaceID.GetType()), queryParams.ToStoragePlaceID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("Type"), this.TypeToDbType(queryParams.type.GetType()), queryParams.type);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("page"), this.TypeToDbType(queryParams.page.GetType()), queryParams.page);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("pagenum"), this.TypeToDbType(queryParams.pagenum.GetType()), queryParams.pagenum);

            return sps.ToDataSet();
            //return queryParams.pageParams;
        }
        public DataSet CompoundQueryMonth(QueryParams queryParams)
        {
            StoredProcedureSection sps = this.defaultGateway.FromStoredProcedure("Proc_CompoundQuery");
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StartDate"), this.TypeToDbType(queryParams.StartDate.GetType()), queryParams.StartDate);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EndDate"), this.TypeToDbType(queryParams.EndDate.GetType()), queryParams.EndDate);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("WorkShopCode"), this.TypeToDbType(queryParams.WorkShopCode.GetType()), queryParams.WorkShopCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StorageID"), this.TypeToDbType(queryParams.StorageID.GetType()), queryParams.StorageID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("StoragePlaceID"), this.TypeToDbType(queryParams.StoragePlaceID.GetType()), queryParams.StoragePlaceID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("GroupID"), this.TypeToDbType(queryParams.shiftID.GetType()), queryParams.shiftID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ClassID"), this.TypeToDbType(queryParams.ClassID.GetType()), queryParams.ClassID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("EquipCode"), this.TypeToDbType(queryParams.EquipCode.GetType()), queryParams.EquipCode);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("BasMaterial"), this.TypeToDbType(queryParams.BasMaterial.GetType()), queryParams.BasMaterial);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("IsOut"), this.TypeToDbType(queryParams.isout.GetType()), queryParams.isout);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ToStorageID"), this.TypeToDbType(queryParams.ToStorageID.GetType()), queryParams.ToStorageID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("ToStoragePlaceID"), this.TypeToDbType(queryParams.ToStoragePlaceID.GetType()), queryParams.ToStoragePlaceID);
            sps.AddInputParameter(this.defaultGateway.BuildDbParamName("Type"), this.TypeToDbType(queryParams.type.GetType()), queryParams.type);
          
            return sps.ToDataSet();
        }


    }
}
