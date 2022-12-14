using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastReport;
using FastReport.Utils;
using FastReport.Web;
using System.IO;
using FastReport.Data;
using System.Data;
using Mesnac.Entity;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using System.Configuration;
using System.Data.SqlClient;
using NBear.Common;

public partial class Manager_ReportCenter_RptPlanLotInfo_RptPlanLotInfo : System.Web.UI.Page
{
    private string planID = "";
    protected PptLotDataManager lotManager = new PptLotDataManager();
    protected PptWeighDataManager weighManager = new PptWeighDataManager();
    protected PptPlanManager planManager = new PptPlanManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if( Request.QueryString["PlanID"] != null)
        {
            planID = Request.QueryString["PlanID"].ToString().Trim();
        }
        else
        {
            planID = "111228J51N02";
        }
    }


    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        //加载模板
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("RptPlanLotInfo.frx"));

        //绑定数据源
        DataSet planMain = GetRptPlanLotMain(planID);
        report.RegisterData(planMain.Tables[0], "planMain");


        DataSet planAvgAndSumMain = GetRptPlanLotMainAvgAndSum(planID);
        report.RegisterData(planAvgAndSumMain.Tables[0], "planAvgAndSumMain");


        DataSet planLotMaterialDetailInfo = planManager.GetRptPlanLotSumDetailInfo(planID);
        report.RegisterData(planLotMaterialDetailInfo.Tables[0], "MaterialDetailInfo");


        DataSet planLotRubsDetailInfo = GetRptPlanLotMaterialDetailInfo(planID);

        DataRow[] drs = new DataRow[999]; int i = 0;
        foreach (DataRow dr in planLotRubsDetailInfo.Tables[0].Rows)
        {

          //  String sqll = "select * from PmtCheckMaterSet where materialcode = '" + dr["MaterCode"].ToString() + "'";
          //  DataSet das = planManager.GetBySql(sqll).ToDataSet();
          //  if (dr["MaterCode"].ToString().Substring(0, 1) == "2" || das.Tables[0].Rows.Count>0)
          //  {
               
       



          //      if (String.IsNullOrEmpty(dr["check_weight"].ToString()) || Double.Parse(dr["check_weight"].ToString()) == 0)
          //{ continue; }
          //dr["SetWeight"] = dr["check_setweight"].ToString();
          //dr["RealWeight"] = dr["check_weight"].ToString();
          //dr["WeighType"] = "检量称";


          //  }
        }
        //for (int t = 0; t < i;t++ )
        //{ planLotRubsDetailInfo.Tables[0].Rows.Add(drs[t]); }




        report.RegisterData(planLotRubsDetailInfo.Tables[0], "RubsDetailInfo");
        report.SetParameterValue("PlanID", planID);

    }
    public DataSet GetRptPlanLotMaterialDetailInfo(string planID)
    {

        string sql = "select top 1 * from PptWeighData where planid =  '" + planID + "'";
                DataSet ds= planManager.GetBySql(sql).ToDataSet();

        if (ds.Tables[0].Rows.Count == 0)
        {
            sql = @"  SELECT		a.Barcode,a.SetWeight,a.RealWeight,
                                CASE		a.WeighType WHEN '1' THEN '炭黑' WHEN '3' THEN '胶料' WHEN '2' THEN '油' WHEN '4' THEN '小料' 
                                ELSE		a.WeighType 
                                END AS		WeighType ,a.WeightID,a.MaterCode,a.MaterName,b.MaterName  AS RecipeName, 
			                                b.SerialID AS SerialID,b.StartDatetime AS StartDatetime,b.DoneRtime AS DoneRtime,
			                                b.DoneAllRtime AS DoneAllRtime,BwbTime AS BwbTime,PjTemp AS PjTemp,PolyDisTime AS PolyDisTime,
			                                CBDisTime AS CBDisTime,OilDisTime AS OilDisTime,PjPower AS PjPower,LotEnergy AS LotEnergy,
a.check_weight,a.check_setweight,a.check_setError
                                FROM		dbo.PptWeighHisData a 
                                LEFT JOIN	PptLotData  b ON a.Barcode=b.Barcode 
                                WHERE       a.PlanID = '" + planID + "'";
            sql += "   ORDER BY	Barcode,WeighType,WeightID,a.Matercode ";
        }
        else
        { sql = @"  SELECT		a.Barcode,a.SetWeight,a.RealWeight,
                                CASE		a.WeighType WHEN '1' THEN '炭黑' WHEN '3' THEN '胶料' WHEN '2' THEN '油' WHEN '4' THEN '小料' 
                                ELSE		a.WeighType 
                                END AS		WeighType ,a.WeightID,a.MaterCode,a.MaterName,b.MaterName  AS RecipeName, 
			                                b.SerialID AS SerialID,b.StartDatetime AS StartDatetime,b.DoneRtime AS DoneRtime,
			                                b.DoneAllRtime AS DoneAllRtime,BwbTime AS BwbTime,PjTemp AS PjTemp,PolyDisTime AS PolyDisTime,
			                                CBDisTime AS CBDisTime,OilDisTime AS OilDisTime,PjPower AS PjPower,LotEnergy AS LotEnergy,
a.check_weight,a.check_setweight,a.check_setError
                                FROM		dbo.PptWeighData a 
                                LEFT JOIN	PptLotData  b ON a.Barcode=b.Barcode 
                                WHERE       a.PlanID = '" + planID + "'";
        sql += "   ORDER BY	Barcode,WeighType,WeightID,a.Matercode ";


      }











        ds = planManager.GetBySql(sql).ToDataSet();
        //}
        
        return ds;
    }
    public DataSet GetRptPlanLotMainAvgAndSum(string planID)
    {
        string sqlstr = @"  SELECT  sum(DoneAllRtime) as sumDoneAllRtime,avg(DoneAllRtime) as avgDoneAllRtime,
		                                sum(DoneRtime) as sumDoneRtime,avg(DoneRtime) as avgDoneRtime,
		                                sum(BwbTime) as sumBwbTime ,avg(BwbTime) as avgBwbTime,
		                                sum(PolyDisTime) as sumPolyDistime,avg(PolyDisTime) as avgPolydistime,
		                                sum(CBDisTime) as sumCBDistime,avg(CBDisTime) as avgCBdistime,
		                                sum(PjTemp) as sumPjTemp,avg(PjTemp) as avgPjTemp,
                                        sum(OilDisTime) as sumOil,avg(OilDisTime) as avgOil,
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

        DataSet ds = planManager.GetBySql(sqlstr).ToDataSet();
        return ds;
    }

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
                                LEFT JOIN	SysCode	c			ON		c.ItemCode = p.RecipeType 
								                                AND		c.TypeID = 'PmtType'
                                LEFT JOIN	PptShift shift		ON		shift.ObjID = p.ShiftID
                                LEFT JOIN	PptClass class		ON		class.ObjID = p.ClassID
                                LEFT JOIN	BasEquip equip		ON		equip.EquipCode = lot.EquipCode
                                WHERE      	p.PlanID = '" + planID + "'";

        DataSet ds = planManager.GetBySql(sqlstr).ToDataSet();
        return ds;
    }
}