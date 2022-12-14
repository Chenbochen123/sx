using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;

using Ext.Net;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;

using NBear.Common;

public partial class Manager_RubberQuality_Manage_CheckRubberQualityReport : Mesnac.Web.UI.BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            导出 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthExport" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            #region 加载CSS样式
            System.Web.UI.HtmlControls.HtmlGenericControl cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/ext-chinese-font.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式

            #region 加载JS文件
            System.Web.UI.HtmlControls.HtmlGenericControl scriptLink = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "CheckRubberQualityReport.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            // 生产车间
            IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
            EntityArrayList<BasWorkShop> mBasWorkShopList = bBasWorkShopManager.GetListByWhereAndOrder(
                BasWorkShop._.DeleteFlag == "0"
                , BasWorkShop._.ObjID.Asc);
            foreach (BasWorkShop mBasWorkShop in mBasWorkShopList)
            {
                ComboBoxNorthWorkShop.AddItem(mBasWorkShop.WorkShopName, mBasWorkShop.ObjID.ToString());
            }

            DateFieldNorthMonth.SetValue(DateTime.Today.ToString("yyyy-MM"));
            ComboBoxNorthCheckTypeCode.SetValue("2");
        }
    }

    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        //var t = new Ext.Net.Panel()
        //{
        //    Title = "New tab",
        //    ID = "NewTabID",
        //    Html = "Tab1's content"
        //};
        //t.AddTo(this.TabPanelCenter);
        //this.TabPanelCenter.SetActiveTab(t.ClientID);

        //return;
        //PanelCenter.Items.Clear();

        //PanelCenter.RemoveAll();

        //PanelCenter.LoadContent();

        HiddenMonth.SetValue(DateFieldNorthMonth.RawText);
        HiddenWorkShopCode.SetValue(ComboBoxNorthWorkShop.Value.ToString());
        HiddenWorkShopName.SetValue(ComboBoxNorthWorkShop.RawText.Trim());
        HiddenCheckTypeCode.SetValue(ComboBoxNorthCheckTypeCode.Value.ToString());

        PanelCenter.LoadContent();
    }

    [DirectMethod]
    public string GetTabPanelCenterContent(Dictionary<string, string> parameters)
    {
        IQmtCheckRubberQualityReportParams paras = new QmtCheckRubberQualityReportParams();
        paras.BeginCheckPlanDate = DateFieldNorthMonth.RawText + "-01";
        paras.EndCheckPlanDate = Convert.ToDateTime(DateFieldNorthMonth.RawText + "-01")
            .AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
        paras.WorkShopCode = ComboBoxNorthWorkShop.Value.ToString();
        paras.CheckTypeCode = ComboBoxNorthCheckTypeCode.Value.ToString();

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        DataSet ds = bQmtCheckMasterManager.GetCheckRubberQualityReportByParas(paras);

        var tabPanel = new TabPanel()
        {
            ID = "TabPanelCenter"
        };

        foreach (DataTable dt in ds.Tables)
        {
            string rubTypeCode = dt.Rows[0]["RubTypeCode"].ToString();
            string rubTypeName = dt.Rows[0]["RubTypeName"].ToString();
            GridPanel gridPanel = new GridPanel
            {
                ID = "GridPanelCenter" + rubTypeCode,
                Title = rubTypeName + "胶料",
                Store = 
                {
                    new Store 
                    { 
                        ID="StoreCenter" + rubTypeCode,
                        Model = {
                            new Model {
                                ID = "ModelCenter" + rubTypeCode,
                                Fields = 
                                {
                                      new ModelField { Name = "CheckPlanDate", },
                                      new ModelField { Name = "ShiftCheckId", },
                                      new ModelField { Name = "RubTypeCode", },
                                      new ModelField { Name = "RubTypeName", },
                                      new ModelField { Name = "Amount", },
                                      new ModelField { Name = "QuaAmount", },
                                      new ModelField { Name = "UnQuaAmount", },
                                      new ModelField { Name = "DetailAmount", },
                                      new ModelField { Name = "Amount_LB", },
                                      new ModelField { Name = "Amount_BZ", },
                                      new ModelField { Name = "Amount_YD", },
                                      new ModelField { Name = "Amount_CC", },
                                      new ModelField { Name = "Amount_MN", },
                                      new ModelField { Name = "Amount_JS", },
                                      new ModelField { Name = "QuaRate", },
                                },
                            },
                        },
                        DataSource = dt,
                    }
                },
                ColumnModel =
                {
                    ID = "ColumnModelCenter" + rubTypeCode,
                    Columns =
                    {
                        new Column { DataIndex = "CheckPlanDate", Text = "检验日期", Width = 90, },
                        new Column { DataIndex = "ShiftCheckId", Text = "班次", Width = 50, },
                        new Column { DataIndex = "Amount", Text = "受检数量", Width = 70, },
                        new Column { DataIndex = "QuaAmount", Text = "合格", Width = 50, },
                        new Column { DataIndex = "UnQuaAmount", Text = "不合格", Width = 60, },
                        new Column { DataIndex = "QuaRate", Text = "合格率%", Width = 70, },
                        new Column { DataIndex = "Amount_LB", Text = "硫变仪", Width = 60, TdCls = "x-grid-cell-Cost", Renderer = { Fn = "pctChange" , }, },
                        new Column { DataIndex = "Amount_YD", Text = "硬度", Width = 50, TdCls = "x-grid-cell-Cost", Renderer = { Fn = "pctChange" , }, },
                        new Column { DataIndex = "Amount_BZ", Text = "比重", Width = 50, TdCls = "x-grid-cell-Cost", Renderer = { Fn = "pctChange" , }, },
                        new Column { DataIndex = "Amount_CC", Text = "抽出", Width = 50, TdCls = "x-grid-cell-Cost", Renderer = { Fn = "pctChange" , }, },
                        new Column { DataIndex = "Amount_MN", Text = "粘度", Width = 50, TdCls = "x-grid-cell-Cost", Renderer = { Fn = "pctChange" , }, },
                        new Column { DataIndex = "Amount_JS", Text = "焦烧", Width = 50, TdCls = "x-grid-cell-Cost", Renderer = { Fn = "pctChange" , }, },
                        new Column { DataIndex = "DetailAmount", Text = "合计", Width = 50, TdCls = "x-grid-cell-Cost", },
                    },
                },
            };
            gridPanel.Listeners.ItemDblClick.Fn = "GridPanelCenter_ItemDbClick";
            tabPanel.Items.Add(gridPanel);
        }
        return ComponentLoader.ToConfig(tabPanel);
    }

    protected void ButtonViewShowAll_Click(object sender, DirectEventArgs e)
    {
        CheckboxViewNorthJudgeResult.SetValue(false);
        GetRubberQualityReportView("");
    }

    protected void ButtonViewShowUnQuality_Click(object sender, DirectEventArgs e)
    {
        CheckboxViewNorthJudgeResult.SetValue(true);
        GetRubberQualityReportView("0");
    }

    [DirectMethod]
    public bool CheckBoxViewJudgeResult_Change()
    {
        CheckboxViewNorthJudgeResult.SetValue(true);
        GetRubberQualityReportView("0");
        WindowView.Show();
        return true;
    }

    private void GetRubberQualityReportView(string judgeResult)
    {
        StoreViewDetail.RemoveAll();

        string rubTypeCode = HiddenRubTypeCode.Value.ToString();
        string checkPlanDate = HiddenCheckPlanDate.Value.ToString();
        string shiftCheckId = HiddenShiftCheckId.Value.ToString();
        string workShopCode = HiddenWorkShopCode.Value.ToString();
        string checkTypeCode = HiddenCheckTypeCode.Value.ToString();

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();

        IQmtCheckRubberQualityReportViewParams paras = new QmtCheckRubberQualityReportViewParams();
        paras.RubTypeCode = rubTypeCode;
        paras.CheckPlanDate = checkPlanDate;
        paras.ShiftCheckId = shiftCheckId;
        paras.WorkShopCode = workShopCode;
        paras.JudgeResult = judgeResult;
        paras.CheckTypeCode = checkTypeCode;

        DataSet ds = bQmtCheckMasterManager.GetCheckRubberQualityReportViewByParas(paras);

        StoreViewLot.DataSource = ds.Tables[0];
        StoreViewLot.DataBind();
    }

    protected void RowSelectionModelViewLot_SelectionChange(object sender, DirectEventArgs e)
    {
        string checkCode = e.ExtraParams["CheckCode"];
        string serialId = e.ExtraParams["SerialId"];
        string llSerialID = e.ExtraParams["LLSerialID"];
        string ifCheckNum = e.ExtraParams["IfCheckNum"];

        IQmtCheckDetailManager bQmtCheckDetailManager = new QmtCheckDetailManager();

        IQmtCheckRubberQualityReportDetailParams paras = new QmtCheckRubberQualityReportDetailParams();
        paras.CheckCode = checkCode;
        paras.SerialId = serialId;
        paras.LLSerialID = llSerialID;
        paras.IfCheckNum = ifCheckNum;

        DataSet ds = bQmtCheckDetailManager.GetCheckRubberQualityReportDetailByParas(paras);

        StoreViewDetail.DataSource = ds.Tables[0];
        StoreViewDetail.DataBind();

    }


    [DirectMethod]
    public void GetRubberQualityReport()
    {
        string month = HiddenMonth.Value.ToString();
        string workShopCode = HiddenWorkShopCode.Value.ToString();
        string workShopName = HiddenWorkShopName.Value.ToString();
        string checkTypeCode = HiddenCheckTypeCode.Value.ToString();
        if (month == "")
        {
            X.Msg.Alert("提示", "请先查询结果后再导出").Show();
            return;
        }

        IQmtCheckRubberQualityReportParams paras = new QmtCheckRubberQualityReportParams();
        paras.BeginCheckPlanDate = month + "-01";
        paras.EndCheckPlanDate = Convert.ToDateTime(month + "-01").AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
        paras.WorkShopCode = workShopCode;
        paras.CheckTypeCode = checkTypeCode;

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        DataSet ds = bQmtCheckMasterManager.GetCheckRubberQualityReportByParas(paras);

        IWorkbook workbook = null;
        using (FileStream fs = new FileStream(Server.MapPath("CheckRubberQualityReport.xls"), FileMode.Open, FileAccess.Read))
        {
            try
            {
                workbook = new HSSFWorkbook(fs);
            }
            catch
            {
                X.Msg.Alert("提示", "上传的文件不是有效的Excel文件").Show();
                return;
            }

        }

        int sheetIndex = 0;
        foreach (DataTable dt in ds.Tables)
        {
            sheetIndex = sheetIndex + 1;

            ISheet sheet = workbook.CloneSheet(0);
            workbook.SetSheetName(sheetIndex, dt.Rows[0]["RubTypeName"].ToString() + "胶料");

            sheet.GetRow(2).GetCell(0).SetCellValue(dt.Rows[0]["CheckPlanDate"].ToString().Substring(5, 2) + ".1");

            int rowIndex = 2; //第三行起
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["QuaAmount"].ToString() != "" && dr["QuaAmount"].ToString() != "0")
                    sheet.GetRow(rowIndex).GetCell(4).SetCellValue(Convert.ToInt32(dr["QuaAmount"])); //合格数

                if (dr["UnQuaAmount"].ToString() != "" && dr["UnQuaAmount"].ToString() != "0")
                    sheet.GetRow(rowIndex).GetCell(6).SetCellValue(Convert.ToInt32(dr["UnQuaAmount"])); //不合格数

                if (dr["Amount_LB"].ToString() != "" && dr["Amount_LB"].ToString() != "0")
                    sheet.GetRow(rowIndex).GetCell(10).SetCellValue(Convert.ToInt32(dr["Amount_LB"])); //流变仪数

                if (dr["Amount_YD"].ToString() != "" && dr["Amount_YD"].ToString() != "0")
                    sheet.GetRow(rowIndex).GetCell(11).SetCellValue(Convert.ToInt32(dr["Amount_YD"])); //硬度数

                if (dr["Amount_BZ"].ToString() != "" && dr["Amount_BZ"].ToString() != "0")
                    sheet.GetRow(rowIndex).GetCell(12).SetCellValue(Convert.ToInt32(dr["Amount_BZ"])); //比重数

                if (dr["Amount_CC"].ToString() != "" && dr["Amount_CC"].ToString() != "0")
                    sheet.GetRow(rowIndex).GetCell(13).SetCellValue(Convert.ToInt32(dr["Amount_CC"])); //抽出数

                if (dr["Amount_MN"].ToString() != "" && dr["Amount_MN"].ToString() != "0")
                    sheet.GetRow(rowIndex).GetCell(14).SetCellValue(Convert.ToInt32(dr["Amount_MN"])); //粘度数

                if (dr["Amount_JS"].ToString() != "" && dr["Amount_JS"].ToString() != "0")
                    sheet.GetRow(rowIndex).GetCell(15).SetCellValue(Convert.ToInt32(dr["Amount_JS"])); //焦烧数


                rowIndex = rowIndex + 1;
            }
            sheet.ForceFormulaRecalculation = true;
        }
        workbook.RemoveSheetAt(0);

        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);

        X.Mask.Hide();

        string fileName = "胶料质量分析" + month;
        if (workShopName != "")
        {
            fileName = fileName + "(" + workShopName + ")";
        }
        new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, fileName);


        //X.Msg.Alert("提示", "导出成功").Show();
        //X.Msg.Notify("", sheet.SheetName).Show();

    }

}