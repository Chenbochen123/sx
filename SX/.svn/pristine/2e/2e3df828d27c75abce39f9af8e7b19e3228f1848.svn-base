using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.HtmlControls;

using Mesnac.Web.UI;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;

using Ext.Net;
using System.Text;


public partial class Manager_RubberQuality_Manage_CheckRubberQualitiedRateReport : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthExport" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 加载CSS样式
        HtmlGenericControl cssLink = new HtmlGenericControl("link");
        cssLink.Attributes.Add("type", "text/css");
        cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
        this.Page.Header.Controls.Add(cssLink);
        #endregion 加载CSS样式

        if (!X.IsAjaxRequest)
        {
            this.txtBeginPlanDate.SetValue(DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"));
            this.txtEndPlanDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));

            ComboBoxNorthCheckType.SetValueAndFireSelect("2");

        }
    }

    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        if (txtBeginPlanDate.SelectedValue == null || txtEndPlanDate.SelectedValue == null)
        {
            X.Msg.Alert("提示", "生产日期不能为空").Show();
            return;
        }

        HiddenNorthCheckType.SetValue(ComboBoxNorthCheckType.Value.ToString());

        DataSet ds = null;
        string checkType = HiddenNorthCheckType.Value.ToString();
        string chejian = "";
        String shift = "";
        if (this.cbxChejian.Value != null)
        {
            if (this.cbxChejian.Value.ToString() == "全部" || this.cbxChejian.Value.ToString() == "0")
            {
                chejian = "";
            }
            else
            {
                chejian = cbxChejian.Value.ToString();
            }

        }

        if (this.cbxShift.Value != null)
        {
            if (this.cbxShift.Value.ToString() == "全部" || this.cbxShift.Value.ToString() == "0")
            {
                shift = "";
            }
            else
            {
                shift = cbxShift.Value.ToString();
            }

        }

        if (checkType == "2")
        {
            IQmtCheckRubberQualitiedRateReportParams paras = new QmtCheckRubberQualitiedRateReportParams();

            paras.BeginPlanDate = txtBeginPlanDate.RawText;
            paras.EndPlanDate = txtEndPlanDate.RawText;
            paras.WorkBar = chejian;
            paras.ShiftID = shift;
            IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();

            ds = GetCheckRubberQualitiedRateReportByParas(paras);
        }
        else if (checkType == "1")
        {
            IQmtCheckRubberAssessQualitiedRateReportParams paras = new QmtCheckRubberAssessQualitiedRateReportParams();

            paras.BeginPlanDate = txtBeginPlanDate.RawText;
            paras.EndPlanDate = txtEndPlanDate.RawText;
            paras.WorkBar = chejian;
            paras.ShiftID = shift;
            IQmtCheckAssessMasterManager bQmtCheckAssessMasterManager = new QmtCheckAssessMasterManager();

            ds =GetCheckRubberAssessQualitiedRateReportByParas(paras);
        }

        if (ds.Tables[0].Rows.Count == 0)
        {
            WebReport1.Report.Clear();
            WebReport1.Report.Refresh();
            WebReport1.Update();
            WebReport1.Refresh();

            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("CheckRubberQualitiedRateReport.frx"));
        //绑定数据源

        //if (ds.Tables[0].Rows.Count > 0)
        //    btnPrint.Visible = true;
        //else
        //    btnPrint.Visible = false;
        report.RegisterData(ds.Tables[0], "QmtCheckQualitiedRateReport");
        report.SetParameterValue("PlanMonth", txtBeginPlanDate.RawText.ToString() + "至" + txtEndPlanDate.RawText.ToString());
        report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

        X.Msg.Alert("提示", "查询完毕").Show();

    }


    public DataSet GetCheckRubberQualitiedRateReportByParas(IQmtCheckRubberQualitiedRateReportParams paras)
    {
        StringBuilder sql = new StringBuilder();
        sql.AppendLine("SELECT A.MaterCode, C.MaterialName+dbo.FuncGetTypeNameByTypeID(RecipeType) as MaterialName, A.PlanDate");
        sql.AppendLine("    , COUNT(*) SerialCount");
        sql.AppendLine("    , SUM(CASE WHEN A.Grade = 1 THEN 0 ELSE 1 END) UnqualitiedCount");
        sql.AppendLine("    , SUM(CASE WHEN A.Grade = 1 THEN 1 ELSE 0 END) QualitiedCount");
        sql.AppendLine("    , CONVERT(NUMERIC(9, 4), ROUND(CONVERT(NUMERIC(9, 0), SUM(CASE WHEN A.Grade = 1 THEN 1 ELSE 0 END)) / COUNT(*), 4)) QualitiedRate");
        sql.AppendLine("FROM (");
        sql.AppendLine("    SELECT A.PlanDate, A.MaterCode,RecipeType");
        sql.AppendLine("        , Case When ISNULL(B.NotQuaCompute, '0') = '1' Then 1 Else B.Grade End Grade");
        sql.AppendLine("        , RANK() OVER(PARTITION BY A.CheckCode, B.SerialId, B.LLSerialID");
        sql.AppendLine("            ORDER BY B.IfCheckNum DESC) RANK1");
        sql.AppendLine("    FROM QmtCheckMaster A LEFT JOIN BasEquip F ON A.EquipCode = F.EquipCode");
        sql.AppendLine("    INNER JOIN QmtCheckLot B ON A.CheckCode = B.CheckCode");
        sql.AppendLine("    INNER JOIN QmtCheckStandType C ON A.StandCode = C.ObjID");
        sql.AppendLine("    WHERE 1 = 1 AND C.CheckTypeCode IN (2)");
        if (paras.BeginPlanDate != "")
        {
            sql.AppendFormat("    AND LEFT(A.PlanDate, 10) >= '{0}'", paras.BeginPlanDate);
            sql.AppendLine();
        }
        if (paras.EndPlanDate != "")
        {
            sql.AppendFormat("    AND LEFT(A.PlanDate, 10) <= '{0}'", paras.EndPlanDate);
            sql.AppendLine();
        }
        if (paras.WorkBar != "")
        {
            sql.AppendFormat("    AND F.WorkShopCode = '{0}'", paras.WorkBar);
            sql.AppendLine();
        }
        if (paras.ShiftID != "")
        {
            sql.AppendFormat("    AND A.ShiftID = '{0}'", paras.ShiftID);
            sql.AppendLine();
        }
        sql.AppendLine("    ) A");
        sql.AppendLine("    LEFT JOIN BasMaterial C ON A.MaterCode = C.MaterialCode");

        sql.AppendLine("WHERE 1 = 1 AND A.Grade IS NOT NULL AND A.RANK1 = 1");
        sql.AppendLine("GROUP BY  A.MaterCode , C.MaterialName+dbo.FuncGetTypeNameByTypeID(RecipeType) , A.PlanDate");
        sql.AppendLine("ORDER BY  C.MaterialName+dbo.FuncGetTypeNameByTypeID(RecipeType) , A.PlanDate");
        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();

        return bQmtCheckMasterManager.GetBySql(sql.ToString()).ToDataSet();

    }

    public DataSet GetCheckRubberAssessQualitiedRateReportByParas(IQmtCheckRubberAssessQualitiedRateReportParams paras)
    {
        StringBuilder sql = new StringBuilder();
        sql.AppendLine("SELECT A.MaterCode, C.MaterialName, A.PlanDate");
        sql.AppendLine("    , COUNT(*) SerialCount");
        sql.AppendLine("    , SUM(CASE WHEN A.Grade = 1 THEN 0 ELSE 1 END) UnqualitiedCount");
        sql.AppendLine("    , SUM(CASE WHEN A.Grade = 1 THEN 1 ELSE 0 END) QualitiedCount");
        sql.AppendLine("    , CONVERT(NUMERIC(9, 4), ROUND(CONVERT(NUMERIC(9, 0), SUM(CASE WHEN A.Grade = 1 THEN 1 ELSE 0 END)) / COUNT(*), 4)) QualitiedRate");
        sql.AppendLine("FROM (");
        sql.AppendLine("    SELECT A.PlanDate, A.MaterCode");
        sql.AppendLine("        , Case When ISNULL(B.NotQuaCompute, '0') = '1' Then 1 Else B.Grade End Grade");
        sql.AppendLine("        , RANK() OVER(PARTITION BY A.CheckCode, B.SerialId, B.LLSerialID");
        sql.AppendLine("            ORDER BY B.IfCheckNum DESC) RANK1");
        sql.AppendLine("    FROM QmtCheckAssessMaster A LEFT JOIN BasEquip F ON A.EquipCode = F.EquipCode");
        sql.AppendLine("    INNER JOIN QmtCheckAssessLot B ON A.CheckCode = B.CheckCode");
        sql.AppendLine("    INNER JOIN QmtCheckStandType C ON A.StandCode = C.ObjID");
        sql.AppendLine("    WHERE 1 = 1 AND C.CheckTypeCode IN (1)");
        if (paras.BeginPlanDate != "")
        {
            sql.AppendFormat("    AND LEFT(A.PlanDate, 10) >= '{0}'", paras.BeginPlanDate);
            sql.AppendLine();
        }
        if (paras.EndPlanDate != "")
        {
            sql.AppendFormat("    AND LEFT(A.PlanDate, 10) <= '{0}'", paras.EndPlanDate);
            sql.AppendLine();
        }
        if (paras.WorkBar != "")
        {
            sql.AppendFormat("    AND F.WorkShopCode = '{0}'", paras.WorkBar);
            sql.AppendLine();
        }
        if (paras.ShiftID != "")
        {
            sql.AppendFormat("    AND A.ShiftID = '{0}'", paras.ShiftID);
            sql.AppendLine();
        }
        sql.AppendLine("    ) A");
        sql.AppendLine("    LEFT JOIN BasMaterial C ON A.MaterCode = C.MaterialCode");

        sql.AppendLine("WHERE 1 = 1 AND A.Grade IS NOT NULL AND A.RANK1 = 1");
        sql.AppendLine("GROUP BY A.MaterCode, C.MaterialName, A.PlanDate");
        sql.AppendLine("ORDER BY C.MaterialName, A.PlanDate");
        IQmtCheckAssessMasterManager bQmtCheckAssessMasterManager = new QmtCheckAssessMasterManager();
        return bQmtCheckAssessMasterManager.GetBySql(sql.ToString()).ToDataSet();

    }

    /// <summary>
    /// 导出Report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthExport_Click(object sender, EventArgs e)
    {
        if (WebReport1.Report.Pages.Count == 0)
        {
            X.Msg.Alert("提示", "请先查询出结果后再导出").Show();
            return;
        }

        //String planMonth = DateFieldNorthPlanMonth.RawText;
        //WebReport1.Report.FileName = "JLHGL-" + planMonth;
        //WebReport1.ExportExcel2007();


        // FastReport.Utils.Config.WebMode = true;

        WebReport1.Report.Prepare();

        // Export report to Excel stream

        FastReport.Export.OoXML.Excel2007Export excelExport = new FastReport.Export.OoXML.Excel2007Export();

        using (System.IO.MemoryStream strm = new System.IO.MemoryStream())
        {
            WebReport1.Report.Export(excelExport, strm);

            // Stream the Excel back to the client as an attachment

            Response.ClearContent();

            Response.ClearHeaders();

            Response.Buffer = true;

            Response.ContentType = "Application/Excel";

            Response.AddHeader("Content-Disposition", "attachment;filename=胶料合格率统计.xlsx");

            strm.Position = 0;

            strm.WriteTo(Response.OutputStream);

            Response.End();
        }

    }
    protected void RefreshTime(object sender, DirectEventArgs e)
    {
        this.HiddenServerTime.SetValue(DateTime.Now.ToString("HH:mm:ss"));
    }

}