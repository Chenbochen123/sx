using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.HtmlControls;

using Ext.Net;

using Mesnac.Web.UI;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Entity;

using Mesnac.Data.Interface;
using Mesnac.Data.Implements;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NBear.Common;

public partial class Manager_RubberQuality_Manage_QrigProductionStatistics : BasePage
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            HtmlGenericControl scriptLink = new HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "QrigProductionStatistics.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);

            InitControls();

            if (!IsPostBack)
            {
                DateFieldCheckSDate.SetValue(DateTime.Today.AddDays(-3).ToString("yyyy-MM-dd"));
                DateFieldCheckEDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            }

            //StoreMain.LoadProxy();
            //BindStoreMain();
        }
    }

    private void InitControls()
    {
        // 班组
        IPptClassManager bPptClassManager = new PptClassManager();
        EntityArrayList<PptClass> mPptClassList = bPptClassManager.GetListByWhereAndOrder(
            PptClass._.UseFlag == "1"
            , PptClass._.ObjID.Asc);
        foreach (PptClass mPptClass in mPptClassList)
        {
            ComboBoxCheckPlanClass.AddItem(mPptClass.ClassName, mPptClass.ObjID.ToString());
        }

        // 车间
        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
        EntityArrayList<BasWorkShop> mBasWorkShopList = bBasWorkShopManager.GetListByWhereAndOrder(
            BasWorkShop._.DeleteFlag == "0"
            , BasWorkShop._.ObjID.Asc);
        foreach (BasWorkShop mBasWorkShop in mBasWorkShopList)
        {
            ComboBoxWorkShopId.AddItem(mBasWorkShop.WorkShopName, mBasWorkShop.ObjID.ToString());
        }
    }

    protected void ButtonNorthQuery_Click(object sender, EventArgs e)
    {
        if (ValidateFormPanelNorth() == false)
        {
            return;
        }
        //StoreMain.LoadPage(1);
        //BindStoreMain();
        

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("QrigProductionStatistics.frx"));
        //绑定数据源
        DataSet ds = GetStoreSource();
        //if (ds.Tables[0].Rows.Count > 0)
        //    btnPrint.Visible = true;
        //else
        //    btnPrint.Visible = false;
        report.RegisterData(ds.Tables[0], "QmtQrigProdStatInfo");
        report.Refresh();
        WebReport1.Update(); 
        WebReport1.Refresh();

        X.Msg.Alert("提示", "查询完毕").Show();

    }

    /// <summary>
    /// 修改标识：qusf 20131209
    /// 修改内容：1.导出中文名
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

            Response.AddHeader("Content-Disposition", "attachment;filename=检验产量统计.xlsx");

            strm.Position = 0;

            strm.WriteTo(Response.OutputStream);

            Response.End();
        }

    }

    [DirectMethod]
    public bool SearchQrigMaster()
    {
        if (ValidateFormPanelNorth() == false)
        {
            return false;
        }

        //BindStoreMain();

        //初始化报表控件
        FastReport.Report report = this.WebReport1.Report;
        report.Load(Server.MapPath("QrigProductionStatistics.frx"));
        //绑定数据源
        DataSet ds = GetStoreSource();
        //if (ds.Tables[0].Rows.Count > 0)
        //    btnPrint.Visible = true;
        //else
        //    btnPrint.Visible = false;
        report.RegisterData(ds.Tables[0], "QmtQrigProdStatInfo");
        //report.Refresh();
        
        WebReport1.Refresh();


        X.Msg.Alert("提示", "查询完毕").Show();

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void BindStoreMain()
    {
        DataSet ds = GetStoreSource();
        StoreMain.DataSource = ds;
        StoreMain.DataBind();

    }

    private DataSet GetStoreSource()
    {
        string checkSDate = DateFieldCheckSDate.SelectedDate.ToString("yyyy-MM-dd");
        string checkEDate = DateFieldCheckEDate.SelectedDate.ToString("yyyy-MM-dd");
        string checkPlanClass = ComboBoxCheckPlanClass.Value.ToString();
        string workShopId = ComboBoxWorkShopId.Value.ToString();

        IQmtQrigMasterManager bQmtQrigMasterManager = new QmtQrigMasterManager();

        IQmtQrigMasterStaticProdAmountParams paras = new QmtQrigMasterStaticProdAmountParams();
        paras.CheckSDate = checkSDate;
        paras.CheckEDate = checkEDate;
        paras.CheckPlanClass = checkPlanClass;
        paras.WorkShopId = workShopId;
        DataSet ds = bQmtQrigMasterManager.StaticsQrigProductionAmount(paras);

        return ds;
    }

    private bool ValidateFormPanelNorth()
    {
        if (DateFieldCheckSDate.RawValue == null)
        {
            X.Msg.Alert("提示", "请选择检验起始日期").Show();
            return false;
        }
        if (DateFieldCheckEDate.RawValue == null)
        {
            X.Msg.Alert("提示", "请选择检验截止日期").Show();
            return false;
        }

        return true;
    }

    protected void ButtonNorthExcel_Click(object sender, DirectEventArgs e)
    {
        //DataSet ds = GetStoreSource();

        //if (ds.Tables[0].Rows.Count == 0)
        //{
        //    X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
        //    return;
        //}
        string rowsValues = e.ExtraParams["RowsValues"].ToString();
        DataTable dt = new DataTable();
        foreach (ModelField mf in ModelMain.Fields)
        {
            dt.Columns.Add(new DataColumn(mf.Name, typeof(string)));
        }

        Dictionary<string, string>[] dicRowsValues = JSON.Deserialize<Dictionary<string, string>[]>(rowsValues);
        foreach (Dictionary<string, string> dic in dicRowsValues)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = dic[dc.ColumnName];
            }
            dt.Rows.Add(dr);
        }

        for (int i = 0; i < dt.Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = dt.Columns[i];
            foreach (ColumnBase cb in GridPanelMain.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                dt.Columns.Remove(dc.ColumnName);
                i--;
            }
        }

        //return;

        //DataTable dt = GetStoreSourceTable();
        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }
        Mesnac.Util.Excel.ExcelDownload ed = new Mesnac.Util.Excel.ExcelDownload();
        ed.ExcelFileDown(dt, "检验数据汇总.xls");

    }

    private DataTable GetStoreSourceTable()
    {
        DataTable dt = new DataTable();
        foreach (ModelField mf in StoreMain.Model[0].Fields)
        {
            dt.Columns.Add(new DataColumn(mf.Name, typeof(string)));
        }

        Dictionary<string, string>[] dics = JSON.Deserialize<Dictionary<string, string>[]>(StoreMain.JsonData);
        foreach (Dictionary<string, string> dic in dics)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = dic[dc.ColumnName];
            }
            dt.Rows.Add(dr);
        }

        return dt;
    }
    protected void RefreshTime(object sender, DirectEventArgs e)
    {
        this.HiddenServerTime.SetValue(DateTime.Now.ToString("HH:mm:ss"));
    }
}