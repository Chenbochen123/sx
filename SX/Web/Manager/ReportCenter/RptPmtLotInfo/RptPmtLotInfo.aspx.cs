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
using Ext.Net;

public partial class Manager_ReportCenter_RptPmtLotInfo_RptPmtLotInfo : Mesnac.Web.UI.Page
{
    private string barCode = "";
    private IPpt_curvedataManager curveManager = new Ppt_curvedataManager("Curve");
    private IPptOpenMixDataManager openMixManager = new PptOpenMixDataManager();
    private IPptLotDataManager lotManager = new PptLotDataManager();
    private IPptMixingDataManager mixDataManager = new PptMixingDataManager();
    private IPptWeighDataManager weighManager = new PptWeighDataManager();
    private IPptAlarmDataManager alramManager = new PptAlarmDataManager();
    private IPmtRecipeManager recipeManager = new PmtRecipeManager();
    private IPmtRecipeMixingManager recipeMixManager = new PmtRecipeMixingManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            barCode = Request.QueryString["BarCode"].ToString().Trim();
        }
        catch (Exception)
        {
            barCode = "";
        }
    }

    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        //加载模板
        FastReport.Report report = this.WebReport1.Report;
       
        //绑定车报表信息
        DataSet PptLot = lotManager.GetLotInfoByBarcode(barCode);
        //X.Js.Alert(PptLot.Tables[0].Rows[0]["EquipName"].ToString());
        //return; 

       report.Load(Server.MapPath("RptPmtLotInfo.frx")); 
        //report.Load(Server.MapPath("RptPmtLotInfo2.frx"));
        report.RegisterData(PptLot.Tables[0], "PptLotData");
        //绑定混炼信息
        DataSet mixData = mixDataManager.GetMixDataByBarCode(barCode);
        report.RegisterData(mixData.Tables[0], "PptMixingData");
    
        //绑定称重信息
        EntityArrayList<PptWeighData> weighList = weighManager.GetListByWhere(PptWeighData._.Barcode == barCode );

      



            report.RegisterData(weighList, "PptWeighData");
        //绑定预分散信息
        //            EntityArrayList<PptWeighData> advanceSeparateList = weighManager.GetListByWhere(PptWeighData._.Barcode == barCode && PptWeighData._.WeighType == "预分散");
        //            report.RegisterData(advanceSeparateList, "PptAdvanceSeparate");
        ////        //绑定开炼信息

        //            string sqlopen = @"SELECT		m.mixBarcode as Barcode,klequipid , m.id as OpenMixId, m.step_name as OpenActionCode,
        // m.stepMixTime as MixTime , m.stepmixcoolspeed as CoolMixSpeed, 
        //			                                    m.stepOpenMixSpeed as OpenMixSpeed, m.steprollor as MixRollor, m.stepWaterTemp as WaterTemp, m.stepRubTemp as RubberTemp, 
        //			                                    m.steppower
        //                                    FROM	    PptMixDataOne m     
        //                                    where         m.mixBarcode='" + barCode + @"'
        //                                    order by klequipid,OpenMixId";
        //            DataSet openMixData = mixDataManager.GetBySql(sqlopen).ToDataSet();
        //            report.RegisterData(openMixData.Tables[0], "PptOpenMixingData");

        ////        //绑定报警信息
        //            EntityArrayList<PptAlarmData> alarmList = alramManager.GetListByWhere(PptAlarmData._.Barcode == barCode);
        //            report.RegisterData(alarmList, "PptAlarmData");
        //密炼曲线图

     
   

            EntityArrayList<Ppt_curvedata> curveArrayList = curveManager.GetTopNListWhereOrder(1, Ppt_curvedata._.Barcode == barCode, Ppt_curvedata._.Barcode.Asc);
            List<PptCurve> curveList = new List<PptCurve>();
            if (curveArrayList.Count > 0)
            {
                curveList = IniPptCurveList(curveArrayList[0]);
            }
            report.RegisterData(curveList, "PptCurve");
    }
    public List<PptCurve> IniPptCurveList(Ppt_curvedata data)
    {
        List<PptCurve> Result = new List<PptCurve>();
        string ss = data.Mixing_Time;
        int count = ss.Split(':').Length;

        for (int i = 0; i < count; i++)
        {
            try
            {
                PptCurve p = new PptCurve();
                p.Barcode = data.Barcode;
                p.CurveData = data.Curve_data;
                p.PlanDate = data.Plan_date;
                p.PlanID = data.Plan_id;
                p.SerialID = data.Serial_id == null ? string.Empty : ((int)data.Serial_id).ToString();
                p.IfSubed = data.If_Subed == null ? string.Empty : data.If_Subed;
                p.SecondSpan = Convert.ToInt32(data.Mixing_Time.Split(':')[i]);
                p.MixingTime = ((DateTime)data.Start_datetime).AddSeconds(p.SecondSpan);
                if (!String.IsNullOrEmpty(data.Mixing_Temp))
                {
                    p.MixingTemp = Convert.ToDecimal(data.Mixing_Temp.Split(':')[i]);
                }
                else
                {
                    p.MixingTemp = 0;
                }
                if (!String.IsNullOrEmpty(data.Mixing_Power))
                {
                    p.MixingPower = Convert.ToDecimal(data.Mixing_Power.Split(':')[i]);
                }
                else
                {
                    p.MixingPower = 0;
                }
                if (!String.IsNullOrEmpty(data.Mixing_Energy))
                {
                    p.MixingEnergy = Convert.ToDecimal(data.Mixing_Energy.Split(':')[i]);
                }
                else
                {
                    p.MixingEnergy = 0;
                }
                if (!String.IsNullOrEmpty(data.Mixing_Press))
                {
                    p.MixingPress = Convert.ToDecimal(data.Mixing_Press.Split(':')[i]);
                }
                else
                {
                    p.MixingPress = 0;
                }
                if (!String.IsNullOrEmpty(data.Mixing_Speed))
                {
                    p.MixingSpeed = Convert.ToDecimal(data.Mixing_Speed.Split(':')[i]);
                }
                else
                {
                    p.MixingSpeed = 0;
                }
                if (!String.IsNullOrEmpty(data.SDS_postion))
                {
                    p.MixingPosition = Convert.ToDecimal(data.SDS_postion.Split(':')[i]);
                }
                else
                {
                    p.MixingPosition = 0;
                }

                //if (!String.IsNullOrEmpty(data.Mixing_MixT))
                //{
                //    p.L1 = Convert.ToDecimal(data.Mixing_MixT.Split(':')[i]);
                //}
                //else
                //{
                //    p.L1 = 0;
                //}
                //if (!String.IsNullOrEmpty(data.Mixing_RotorT))
                //{
                //    p.L2 = Convert.ToDecimal(data.Mixing_RotorT.Split(':')[i]);
                //}
                //else
                //{
                //    p.L2 = 0;
                //}
                //if (!String.IsNullOrEmpty(data.Mixing_DumpT))
                //{
                //    p.L3 = Convert.ToDecimal(data.Mixing_DumpT.Split(':')[i]);
                //}
                //else
                //{
                //    p.L3 = 0;
                //}
                //if (!String.IsNullOrEmpty(data.Mixing_Blend))
                //{
                //    p.L4 = Convert.ToDecimal(data.Mixing_Blend.Split(':')[i]);
                //}
                //else
                //{
                //    p.L4 = 0;
                //}
                //if (!String.IsNullOrEmpty(data.Mixing_Blend2))
                //{
                //    p.L5 = Convert.ToDecimal(data.Mixing_Blend2.Split(':')[i]);
                //}
                //else
                //{
                //    p.L5 = 0;
                //}
                Result.Add(p);
            }
            catch
            (Exception ex)
            {
                //X.Js.Alert(ex.Message.ToString()); 

            }
        }
        return Result;
    }
}