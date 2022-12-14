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

public partial class Manager_RubberQuality_Manage_CheckMasterBatchReport : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthExcel" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
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
            scriptLink.Attributes.Add("src", "CheckMasterBatchReport.js?" + DateTime.Now.Ticks.ToString());
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
    /// </summary>
    private void InitControls()
    {
        // 生产班次
        IPptShiftManager bPptShiftManager = new PptShiftManager();
        EntityArrayList<PptShift> mPptShiftList = bPptShiftManager.GetAllListOrder(PptShift._.ObjID.Asc);
        foreach (PptShift mPptShift in mPptShiftList)
        {
            ComboBoxNorthShiftId.AddItem(mPptShift.ShiftName, mPptShift.ObjID.ToString());
            //ComboBoxNorthShiftId.Items.Add(new Ext.Net.ListItem(mPptShift.ShiftName, mPptShift.ObjID.ToString()));
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

        // 标准类型 
        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(QmtCheckStandType._.DeleteFlag == "0"
            , QmtCheckStandType._.ObjID.Asc);
        foreach (QmtCheckStandType mQmtCheckStandType in mQmtCheckStandTypeList)
        {
            //ComboBoxNorthStandCode.Items.Add(new Ext.Net.ListItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString()));
            ComboBoxNorthStandCode.AddItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString());
        }

    }

    /// <summary>
    /// 填充物料名称
    /// </summary>
    private void FillComboBoxNorthMaterCode()
    {
        ComboBoxNorthMaterCode.GetStore().RemoveAll();

        if (DateFieldNorthPlanDate.SelectedValue != null
            && ComboBoxNorthEquipCode.Value.ToString() != ""
            && ComboBoxNorthShiftId.Value.ToString() != "")
        {
            IPptPlanManager bPptPlanManager = new PptPlanManager();
            WhereClip whereClipPptPlan = new WhereClip();
            whereClipPptPlan.And(PptPlan._.RealNum > 0);
            whereClipPptPlan.And(PptPlan._.PlanDate == DateFieldNorthPlanDate.SelectedDate);
            whereClipPptPlan.And(PptPlan._.RecipeEquipCode == ComboBoxNorthEquipCode.Value.ToString());
            whereClipPptPlan.And(PptPlan._.ShiftID == ComboBoxNorthShiftId.Value.ToString());

            EntityArrayList<PptPlan> mPptPlanList = bPptPlanManager.GetListByWhereAndOrder(whereClipPptPlan
                , PptPlan._.RecipeMaterialName.Asc);


            var list = mPptPlanList.GroupBy(g => new { g.RecipeMaterialCode, g.RecipeMaterialName }).Select(s => s.First()).OrderBy(o => o.RecipeMaterialName);
            // 计划物料
            foreach (var v in list)
            {
                ComboBoxNorthMaterCode.AddItem(v.RecipeMaterialName, v.RecipeMaterialCode);
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
        if (ComboBoxNorthShiftId.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "生产班次不能为空").Show();
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

        if (NumberFieldNorthSSerialId.RawValue.ToString() == "")
        {
            X.Msg.Alert("提示", "起始车次不能为空").Show();
            //GridPanelCenter.Render();
            return;
        }

        if (NumberFieldNorthESerialId.RawValue.ToString() == "")
        {
            X.Msg.Alert("提示", "结束车次不能为空").Show();
            //GridPanelCenter.Render();
            return;
        }
        #endregion 验证查询条件

        DataSet ds = ReadDataSet();

        // Clear Collections to remove old Data and Models
        StoreCenterMain.Reader.Clear();
        StoreCenterCopy.Reader.Clear();
        //GridPanelCenterMain.SelectionModel.Clear();
        GridPanelCenterMain.ColumnModel.Columns.Clear();
        //GridPanelCenterCopy.ColumnModel.Columns.Clear();
        //GridPanelCenterMain.Features.Clear();
        ModelCenterMain.Fields.Clear();
        ModelCenterCopy.Fields.Clear();

        // Reconfigure Store & GridPanel
        ColumnCollection ColumnCollectionMain = new ColumnCollection();
        //ColumnCollection ColumnCollectionCopy = new ColumnCollection();
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            ModelCenterMain.Fields.Add(new ModelField { Name = dc.ColumnName, Type = ModelFieldType.String });
            ModelCenterCopy.Fields.Add(new ModelField { Name = dc.ColumnName, Type = ModelFieldType.String });
            ColumnCollectionMain.Add(new Column { Text = dc.ColumnName, DataIndex = dc.ColumnName });
            //ColumnCollectionCopy.Add(new Column { Text = dc.ColumnName, DataIndex = dc.ColumnName });
        }
        ColumnCollectionMain.Insert(0, new RowNumbererColumn { Width = Unit.Parse("40px") });
        //ColumnCollectionCopy.Insert(0, new RowNumbererColumn { Width = Unit.Parse("40px") });
        GridPanelCenterMain.ColumnModel.Columns.Add(ColumnCollectionMain);
        //GridPanelCenterCopy.ColumnModel.Columns.Add(ColumnCollectionCopy);

        

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
        IQmtCheckMasterBatchReportParams paras = new QmtCheckMasterBatchReportParams();
        paras.PlanDate = DateFieldNorthPlanDate.SelectedDate.ToString("yyyy-MM-dd");
        paras.ShiftId = ComboBoxNorthShiftId.Value.ToString();
        paras.EquipCode = ComboBoxNorthEquipCode.Value.ToString();
        paras.MaterCode = ComboBoxNorthMaterCode.Value.ToString();
        paras.StartSerialId = NumberFieldNorthSSerialId.Value.ToString();
        paras.EndSerialId = NumberFieldNorthESerialId.Value.ToString();
        paras.StandCode = ComboBoxNorthStandCode.Value.ToString();
        DataSet ds = bQmtCheckMasterManager.GetMasterBatchReportByParas(paras);

        return ds;
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
    /// 更改生产机台
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxNorthEquipCode_Change(object sender, DirectEventArgs e)
    {
        FillComboBoxNorthMaterCode();
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
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthExcel_Click(object sender, DirectEventArgs e)
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
        ed.ExcelFileDown(dt, "母胶测试报表");

    }

}