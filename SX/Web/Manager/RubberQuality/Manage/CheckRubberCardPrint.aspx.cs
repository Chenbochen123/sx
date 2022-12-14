using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.HtmlControls;

using NBear;
using NBear.Common;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;

public partial class Manager_RubberQuality_Manage_CheckRubberCardPrint : BasePage
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
            打印预览 = new SysPageAction() { ActionID = 3, ActionName = "ButtonNorthPrintView" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 打印预览 { get; private set; } //必须为 public
    }
    #endregion

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            #region 加载CSS样式
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式

            #region 加载JS文件
            HtmlGenericControl scriptLink = new HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "CheckRubberCardPrint.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            InitControls();

            DateFieldNorthPlanDate.SetValue(DateTime.Today);

            //if (ComboBoxNorthShiftId.Items.Count > 0)
            //{
            //    ComboBoxNorthShiftId.SelectedItems.Add(ComboBoxNorthShiftId.Items[0]);
            //}

            //if (ComboBoxNorthEquipCode.Items.Count > 0)
            //{
            //    ComboBoxNorthEquipCode.SelectedItems.Add(ComboBoxNorthEquipCode.Items[0]);
            //}

            //if (ComboBoxNorthStandCode.Items.Count > 0)
            //{
            //    ComboBoxNorthStandCode.SelectedItems.Add(ComboBoxNorthStandCode.Items[0]);
            //}

        }

    }

    /// <summary>
    /// 初始化控件
    /// 修改标识：qusf 20131016
    /// 修改内容：1.主机手只取
    /// </summary>
    private void InitControls()
    {
        // 生产班次
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        EntityArrayList<PptShift> mPptShiftList = bPptShiftManager.GetListByWhereAndOrder(PptShift._.UseFlag == "1"
            , PptShift._.ObjID.Asc);
        foreach (PptShift mPptShift in mPptShiftList)
        {
            //ComboBoxNorthShiftId.Items.Add(new Ext.Net.ListItem(mPptShift.ShiftName, mPptShift.ObjID.ToString()));
            ComboBoxNorthShiftId.AddItem(mPptShift.ShiftName, mPptShift.ObjID.ToString());
        }

        // 主机手
        IBasMainHanderManager bBasMainHanderManager = new BasMainHanderManager();
        DataSet dsZJS = bBasMainHanderManager.GetMixMainHanderInfo();
        foreach (DataRow drZJS in dsZJS.Tables[0].Rows)
        {
            ComboBoxNorthZJSId.AddItem("[" + drZJS["MainHanderCode"].ToString() + "]" + drZJS["UserName"].ToString(), drZJS["MainHanderCode"].ToString());

        }

        // 生产机台
        IBasEquipManager bBasEquipManager = new BasEquipManager();
        EntityArrayList<BasEquip> mBasEquipList = bBasEquipManager.GetListByWhereAndOrder(BasEquip._.DeleteFlag == "0"
            & BasEquip._.EquipType == "01"
            , BasEquip._.EquipName.Asc);
        foreach (BasEquip mBasEquip in mBasEquipList)
        {
            //ComboBoxNorthEquipCode.Items.Add(new Ext.Net.ListItem(mBasEquip.EquipName, mBasEquip.EquipCode));
            ComboBoxNorthEquipCode.AddItem(mBasEquip.EquipName, mBasEquip.EquipCode);
        }

        // 标准分类
        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(
            QmtCheckStandType._.DeleteFlag == "0"
            & QmtCheckStandType._.CheckTypeCode.In(2, 3)
            , QmtCheckStandType._.ObjID.Asc);
        foreach (QmtCheckStandType mQmtCheckStandType in mQmtCheckStandTypeList)
        {
            ComboBoxNorthStandCode.AddItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString());
        }
    }

    /// <summary>
    /// 更改生产日期
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DateFieldNorthPlanDate_Change(object sender, DirectEventArgs e)
    {
        FillComboBoxNorthMaterCode();
    }

    /// <summary>
    /// 更改生产班次
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxNorthShiftId_Change(object sender, DirectEventArgs e)
    {
        FillComboBoxNorthMaterCode();
    }

    /// <summary>
    /// 更改主机手
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxNorthZJSId_Change(object sender, DirectEventArgs e)
    {
        FillComboBoxNorthMaterCode();
    }

    /// <summary>
    /// 更改生产机台
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxNorthEquipCode_Change(object sender, DirectEventArgs e)
    {
        FillComboBoxNorthMaterCode();
    }


    /// <summary>
    /// 填充物料名称
    /// </summary>
    private void FillComboBoxNorthMaterCode()
    {
        ComboBoxNorthMaterCode.GetStore().RemoveAll();

        if (DateFieldNorthPlanDate.SelectedValue != null
            && ComboBoxNorthEquipCode.Value.ToString() != ""
            && ComboBoxNorthZJSId.Value.ToString() != ""
            && ComboBoxNorthShiftId.Value.ToString() != "")
        {
            IPptShiftConfigManager bPptShiftConfigManager = new PptShiftConfigManager();
            WhereClip whereClipPptPlan = new WhereClip();
            whereClipPptPlan.And(PptShiftConfig._.StockFlag != "4");
            whereClipPptPlan.And(PptShiftConfig._.PlanDate == DateFieldNorthPlanDate.SelectedDate);
            whereClipPptPlan.And(PptShiftConfig._.EquipCode == ComboBoxNorthEquipCode.Value.ToString());
            whereClipPptPlan.And(PptShiftConfig._.ZJSID == ComboBoxNorthZJSId.Value.ToString());
            whereClipPptPlan.And(PptShiftConfig._.ShiftID == ComboBoxNorthShiftId.Value.ToString());

            EntityArrayList<PptShiftConfig> mPptShiftConfigList = bPptShiftConfigManager.GetListByWhereAndOrder(whereClipPptPlan
                , PptShiftConfig._.MaterialName.Asc);

            var list = mPptShiftConfigList.GroupBy(g => new { g.MaterialCode, g.MaterialName }).Select(s => s.First()).OrderBy(o => o.MaterialName);
            // 计划物料
            foreach (var v in list)
            {
                ComboBoxNorthMaterCode.AddItem(v.MaterialName, v.MaterialCode);
                if (!X.IsAjaxRequest)
                {
                    //ComboBoxNorthMaterCode.Items.Add(new Ext.Net.ListItem(v.RecipeMaterialName, v.RecipeMaterialCode));
                    //ComboBoxNorthMaterCode.AddItem(v.RecipeMaterialName, v.RecipeMaterialCode);
                }
                else
                {
                    //ComboBoxNorthMaterCode.AddItem(v.RecipeMaterialName, v.RecipeMaterialCode);
                }
            }
        }
        ComboBoxNorthMaterCode.SetValue("");
    }

    /// <summary>
    /// 选择或清除物料名称
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxNorthMaterCode_TriggerClick(object sender, DirectEventArgs e)
    {
        string index = e.ExtraParams["index"].ToString();
        if (index == "0")
        {
            // 查询
            X.Js.AddScript("App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();");
        }
        else if (index == "1")
        {
            // 清除
            ComboBoxNorthMaterCode.SetValue("");
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, EventArgs e)
    {
        #region 验证查询条件
        if (DateFieldNorthPlanDate.SelectedValue == null)
        {
            X.Msg.Alert("提示", "生产日期不能为空").Show();
            //GridPanelCenter.Render();
            return;
        }
        if (ComboBoxNorthZJSId.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "主机手不能为空").Show();
            //GridPanelCenter.Render();
            return;
        }

        if (ComboBoxNorthEquipCode.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "生产机台不能为空").Show();
            //GridPanelCenter.Render();
            return;
        }

        if (ComboBoxNorthMaterCode.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "物料名称不能为空").Show();
            //GridPanelCenter.Render();
            return;
        }
        if (ComboBoxNorthShiftId.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "生产班次不能为空").Show();
            //GridPanelCenter.Render();
            return;
        }
        if (ComboBoxNorthStandCode.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "标准分类不能为空").Show();
            //GridPanelCenter.Render();
            return;
        }


        #endregion 验证查询条件

        DataSet ds = ReadDataSet();

        // Clear Collections to remove old Data and Models
        StoreCenterMain.Reader.Clear();
        StoreCenterCopy.Reader.Clear();
        //GridPanelCenterMain.ColumnModel.Columns.Clear();
        ModelCenterMain.Fields.Clear();
        ModelCenterCopy.Fields.Clear();

        // Reconfigure Store & GridPanel
        //ColumnCollection ColumnCollectionMain = new ColumnCollection();
        int colCount = ds.Tables[0].Columns.Count;
        for (int colIndex = 0; colIndex < colCount - 1; colIndex++)
        {
            DataColumn dc = ds.Tables[0].Columns[colIndex];
            ModelCenterMain.Fields.Add(new ModelField { Name = dc.ColumnName, Type = ModelFieldType.String });
            ModelCenterCopy.Fields.Add(new ModelField { Name = dc.ColumnName, Type = ModelFieldType.String });

            //ColumnCollectionMain.Add(new Column { Text = dc.ColumnName, DataIndex = dc.ColumnName });
        }


        ModelCenterMain.Fields.Add(new ModelField { Name = ds.Tables[0].Columns[colCount - 1].ColumnName, Type = ModelFieldType.Boolean });
        ModelCenterCopy.Fields.Add(new ModelField { Name = ds.Tables[0].Columns[colCount - 1].ColumnName, Type = ModelFieldType.String });
        //ColumnCollectionMain.Add(new CheckColumn { Text = ds.Tables[0].Columns[colCount - 1].ColumnName, DataIndex = ds.Tables[0].Columns[colCount - 1].ColumnName });

        //ColumnCollectionMain.Insert(0, new RowNumbererColumn { Width = Unit.Parse("40px") });
        //GridPanelCenterMain.ColumnModel.Columns.Add(ColumnCollectionMain);



        //StoreCenterMain.Buffered = true;
        //StoreCenterMain.PageSize = ds.Tables[0].Rows.Count;

        //if (ds.Tables[0].Rows.Count > 0)
        //{
        StoreCenterMain.DataSource = ds;
        StoreCenterMain.DataBind();

        StoreCenterCopy.DataSource = ds;
        StoreCenterCopy.DataBind();


        //}

        // Reconfigure GridPanel
        //GridPanelCenterMain.SelectionModel.Add(new RowSelectionModel { Mode = SelectionMode.Single });

        // **Make sure to call .Render() on the GridPanel
        GridPanelCenterMain.Render();
        GridPanelCenterCopy.Render();

        X.Msg.Alert("提示", "查询完毕").Show();
    }


    /// <summary>
    /// 获取查询数据
    /// </summary>
    /// <returns></returns>
    private DataSet ReadDataSet()
    {
        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        IQmtCheckRubberCardQueryParams paras = new QmtCheckRubberCardQueryParams();
        paras.PlanDate = DateFieldNorthPlanDate.SelectedDate.ToString("yyyy-MM-dd");
        paras.ZJSID = ComboBoxNorthZJSId.Value.ToString();
        paras.EquipCode = ComboBoxNorthEquipCode.Value.ToString();
        paras.MaterCode = ComboBoxNorthMaterCode.Value.ToString();
        paras.ShiftId = ComboBoxNorthShiftId.Value.ToString();
        paras.StandCode = ComboBoxNorthStandCode.Value.ToString();
        DataSet ds = bQmtCheckMasterManager.GetCheckRubberCardQueryByParas(paras);

        return ds;
    }

    protected void StoreCenterMain_DataChanged(object sender, DirectEventArgs e)
    {

    }

    protected void ButtonNorthExport_Click(object sender, DirectEventArgs e)
    {
        string fields = e.ExtraParams["fields"];
        string records = e.ExtraParams["records"];
        Newtonsoft.Json.JavaScriptArray jsArrayFields = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(fields) as Newtonsoft.Json.JavaScriptArray;
        Newtonsoft.Json.JavaScriptArray jsArrayRecords = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(records) as Newtonsoft.Json.JavaScriptArray;

        DataTable dt = new DataTable();

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectField in jsArrayFields)
        {
            if (jsObjectField["name"].ToString().ToLower() != "id")
            {
                dt.Columns.Add(new DataColumn(jsObjectField["name"].ToString(), typeof(string)));
            }
        }

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectRecord in jsArrayRecords)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = jsObjectRecord[dc.ColumnName];
            }
            dt.Rows.Add(dr);
        }

        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }
        Mesnac.Util.Excel.ExcelDownload ed = new Mesnac.Util.Excel.ExcelDownload();
        ed.ExcelFileDown(dt, "胶料卡片打印");

    }

    protected void ButtonNorthPrintView_Click(object sender, DirectEventArgs e)
    {
        string fields = e.ExtraParams["fields"];
        string records = e.ExtraParams["records"];
        Newtonsoft.Json.JavaScriptArray jsArrayFields = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(fields) as Newtonsoft.Json.JavaScriptArray;
        Newtonsoft.Json.JavaScriptArray jsArrayRecords = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(records) as Newtonsoft.Json.JavaScriptArray;

        DataTable dt = new DataTable();

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectField in jsArrayFields)
        {
            if (jsObjectField["name"].ToString().ToLower() != "id")
            {
                dt.Columns.Add(new DataColumn(jsObjectField["name"].ToString(), typeof(string)));
            }
        }

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectRecord in jsArrayRecords)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = jsObjectRecord[dc.ColumnName];
            }
            dt.Rows.Add(dr);
        }

        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "请先查询出结果").Show();
            return;
        }

        if (RowSelectionModelCenterMain.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要预览的记录").Show();
            return;
        }

        int rowIndex = RowSelectionModelCenterMain.SelectedIndex;

        string printFlag = dt.Rows[rowIndex]["是否可打印"].ToString();

        if (printFlag == "")
        {
            X.Msg.Alert("提示", "该记录不可以打印").Show();
            return;
        }

        string barcode = dt.Rows[rowIndex]["架子条码"].ToString();

        if (barcode == "")
        {
            X.Msg.Alert("提示", "该记录没有架子条码").Show();
            return;
        }
        string planDate = DateFieldNorthPlanDate.SelectedDate.ToString("yyyy-MM-dd");

        IQmtCheckRubberCardReportParams paras = new QmtCheckRubberCardReportParams();
        paras.PlanDate = planDate;
        paras.Barcode = barcode;

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        DataSet ds = bQmtCheckMasterManager.GetCheckRubberCardReportByParas(paras);

        if (ds.Tables[0].Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到架子的信息").Show();
            return;
        }
        string materName = ds.Tables[0].Rows[0]["MaterName"].ToString();
        string equipName = ds.Tables[0].Rows[0]["EquipName"].ToString();
        string shiftName = ds.Tables[0].Rows[0]["ShiftName"].ToString();
        string serialIdStartEnd = ds.Tables[0].Rows[0]["SerialIdStartEnd"].ToString();
        string judgeResult = dt.Rows[rowIndex]["核定等级"].ToString();
        string totalWeight = ds.Tables[0].Rows[0]["TotalWeight"].ToString();
        string realWeight = ds.Tables[0].Rows[0]["RealWeight"].ToString();
        string prodDate = ds.Tables[0].Rows[0]["ProdDate"].ToString();
        string realProdDate = ds.Tables[0].Rows[0]["RealProdDate"].ToString();
        string userName = dt.Rows[0]["主机手"].ToString();
        string checkTime = dt.Rows[rowIndex]["检验时间"].ToString();
        string printTime = dt.Rows[0]["打印标志"].ToString();
        string judgeMemo = dt.Rows[0]["核定结果"].ToString();
        string imageUrl = Server.MapPath("CheckRubberCardPrint.frx").Replace("CheckRubberCardPrint.frx", judgeMemo + ".jpg");
        imageUrl = imageUrl.Replace(@"\", @"\\");
        
        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("CheckRubberCardPrint.frx"));
        //绑定数据源

        //if (ds.Tables[0].Rows.Count > 0)
        //    btnPrint.Visible = true;
        //else
        //    btnPrint.Visible = false;

        report.SetParameterValue("MaterName", materName);
        report.SetParameterValue("PlanDate", planDate);
        report.SetParameterValue("EquipName", equipName);
        report.SetParameterValue("ShiftName", shiftName);
        report.SetParameterValue("SerialIdStartEnd", serialIdStartEnd);
        report.SetParameterValue("JudgeResult", judgeResult);
        report.SetParameterValue("Barcode", barcode);
        report.SetParameterValue("TotalWeight", totalWeight);
        report.SetParameterValue("RealWeight", realWeight);
        report.SetParameterValue("ProdDate", prodDate);
        report.SetParameterValue("RealProdDate", realProdDate);
        report.SetParameterValue("ZJSName", userName);
        report.SetParameterValue("CheckTime", checkTime);
        report.SetParameterValue("PrintTime", printTime);
        report.SetParameterValue("JudgeMemo", judgeMemo);
        report.SetParameterValue("ImageUrl", imageUrl);

        int serialCount = 0;

        DataTable dtReport = new DataTable();
        dtReport.Columns.Add(new DataColumn("MNNDValue", typeof(string)));
        dtReport.Columns.Add(new DataColumn("MNNDMark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("MNJSValue", typeof(string)));
        dtReport.Columns.Add(new DataColumn("MNJSMark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("YDValue", typeof(string)));
        dtReport.Columns.Add(new DataColumn("YDMark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("BZValue", typeof(string)));
        dtReport.Columns.Add(new DataColumn("BZMark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("MLValue", typeof(string)));
        dtReport.Columns.Add(new DataColumn("MLMark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("MHValue", typeof(string)));
        dtReport.Columns.Add(new DataColumn("MHMark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("Ts1Value", typeof(string)));
        dtReport.Columns.Add(new DataColumn("Ts1Mark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("T25Value", typeof(string)));
        dtReport.Columns.Add(new DataColumn("T25Mark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("T30Value", typeof(string)));
        dtReport.Columns.Add(new DataColumn("T30Mark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("T60Value", typeof(string)));
        dtReport.Columns.Add(new DataColumn("T60Mark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("T90Value", typeof(string)));
        dtReport.Columns.Add(new DataColumn("T90Mark", typeof(string)));
        dtReport.Columns.Add(new DataColumn("Grade", typeof(string)));
        dtReport.Columns.Add(new DataColumn("JudgeResult", typeof(string)));
        dtReport.Columns.Add(new DataColumn("SerialId", typeof(string)));
        
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["架子条码"].ToString() == barcode)
            {
                serialCount = serialCount + 1;
                DataRow drReport = dtReport.NewRow();
                drReport["MNNDValue"] = dr["门尼粘度"].ToString();
                drReport["MNNDMark"] = dr["粘度标识"].ToString();
                drReport["MNJSValue"] = dr["门尼焦烧"].ToString();
                drReport["MNJSMark"] = dr["焦烧标识"].ToString();
                drReport["YDValue"] = dr["硬度"].ToString();
                drReport["YDMark"] = dr["硬度标识"].ToString();
                drReport["BZValue"] = dr["比重"].ToString();
                drReport["BZMark"] = dr["比重标识"].ToString();
                drReport["MLValue"] = dr["ML"].ToString();
                drReport["MLMark"] = dr["ML标识"].ToString();
                drReport["MHValue"] = dr["MH"].ToString();
                drReport["MHMark"] = dr["MH标识"].ToString();
                drReport["Ts1Value"] = dr["Ts1"].ToString();
                drReport["Ts1Mark"] = dr["Ts1标识"].ToString();
                drReport["T25Value"] = dr["T25"].ToString();
                drReport["T25Mark"] = dr["T25标识"].ToString();
                drReport["T30Value"] = dr["T30"].ToString();
                drReport["T30Mark"] = dr["T30标识"].ToString();
                drReport["T60Value"] = dr["T60"].ToString();
                drReport["T60Mark"] = dr["T60标识"].ToString();
                drReport["T90Value"] = dr["T90"].ToString();
                drReport["T90Mark"] = dr["T90标识"].ToString();
                drReport["Grade"] = dr["核定等级"].ToString();
                drReport["JudgeResult"] = dr["核定结果"].ToString();
                drReport["SerialId"] = dr["车次"].ToString();
                dtReport.Rows.Add(drReport);
            }
        }

        report.SetParameterValue("SerialCount", serialCount.ToString());

        //return;
        report.RegisterData(dtReport, "Table");

        report.Refresh();
        WebReport1.Update();
        WebReport1.Refresh();

        WindowPrint.Show();

    }

    protected void ButtonPrintClose_Click(object sender, DirectEventArgs e)
    {
        WindowPrint.Hide();
    }

    protected void RefreshTime(object sender, DirectEventArgs e)
    {
        this.HiddenServerTime.SetValue(DateTime.Now.ToString("HH:mm:ss"));
    }
}