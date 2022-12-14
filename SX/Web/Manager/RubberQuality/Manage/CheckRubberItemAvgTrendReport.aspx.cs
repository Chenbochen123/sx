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

public partial class Manager_RubberQuality_Manage_CheckRubberItemAvgTrendReport : BasePage
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

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            ButtonNorthExportImage.Hidden = this._.导出.SeqIdx == 0;

            #region 加载CSS样式
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/ext-chinese-font.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式

            InitControls();

            DateFieldNorthBeginDate.SetValue(DateTime.Today.ToString("yyyy-MM-01"));
            DateFieldNorthEndDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));

            ComboBoxNorthStatisticType.Select(0);
        }
    }

    /// <summary>
    /// 初始化控件
    /// </summary>
    private void InitControls()
    {
        // 车间
        IBasWorkShopManager bBasWorkShopManager = new BasWorkShopManager();
        EntityArrayList<BasWorkShop> mBasWorkShopList = bBasWorkShopManager.GetListByWhereAndOrder(
            BasWorkShop._.DeleteFlag == "0"
            , BasWorkShop._.ObjID.Asc);
        foreach (BasWorkShop mBasWorkShop in mBasWorkShopList)
        {
            ComboBoxNorthWorkShop.AddItem(mBasWorkShop.WorkShopName, mBasWorkShop.ObjID.ToString());
        }

        // 主机手
        IBasMainHanderManager bBasMainHanderManager = new BasMainHanderManager();
        EntityArrayList<BasMainHander> mBasMainHanderList = bBasMainHanderManager.GetListByWhereAndOrder(
            BasMainHander._.DeleteFlag == "0"
            & BasMainHander._.WorkShopCode.In(new object[] { 2, 3, 4, 5 })
            , BasMainHander._.MainHanderCode.Asc);
        foreach (BasMainHander mBasMainHander in mBasMainHanderList)
        {
            ComboBoxNorthZJS.AddItem(mBasMainHander.MainHanderCode, mBasMainHander.MainHanderCode);
        }

        // 项目
        IQmtCheckItemManager bQmtCheckItemManager = new QmtCheckItemManager();
        EntityArrayList<QmtCheckItem> mQmtCheckItemList = bQmtCheckItemManager.GetListByWhereAndOrder(
            QmtCheckItem._.DeleteFlag == "0"
            , QmtCheckItem._.ItemCode.Asc);
        foreach (QmtCheckItem mQmtCheckItem in mQmtCheckItemList)
        {
            ComboBoxNorthCheckItem.AddItem(mQmtCheckItem.ItemName, mQmtCheckItem.ItemCode);
        }

    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        if (HiddenNorthMaterCode.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择胶号").Show();
            return;
        }
        if (ComboBoxNorthCheckItem.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择项目").Show();
            return;
        }

        string beginDate = DateFieldNorthBeginDate.RawText;
        string endDate = DateFieldNorthEndDate.RawText;
        string statisticType = ComboBoxNorthStatisticType.Value.ToString();
        string materCode = HiddenNorthMaterCode.Value.ToString();
        string itemCd = ComboBoxNorthCheckItem.Value.ToString();
        string itemName = ComboBoxNorthCheckItem.RawText;
        string workShopCode = ComboBoxNorthWorkShop.Value.ToString();
        string zjsID = ComboBoxNorthZJS.Value.ToString();

        ICltQmtCheckCtrlQueryParams paras = new CltQmtCheckCtrlQueryParams();
        paras.BeginDate = beginDate;
        paras.EndDate = endDate;
        paras.StatisticType = statisticType;
        paras.MaterCode = materCode;
        paras.ItemCd = itemCd;
        paras.WorkShopCode = workShopCode;
        paras.ZJSID = zjsID;
        ICltQmtCheckCtrlManager bCltQmtCheckCtrlManager = new CltQmtCheckCtrlManager();

        DataSet ds = bCltQmtCheckCtrlManager.GetAvgTrendDataSetByQueryParams(paras);

        #region 设置图表
        string permMax = "";
        string permMin = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            permMax = ds.Tables[0].Rows[1]["PermMax"].ToString().TrimEnd(new char[] { '0' });
            permMin = ds.Tables[0].Rows[1]["permMin"].ToString().TrimEnd(new char[] { '0' });
        }

        ModelCenter.Fields.Clear();
        ModelCenter.Fields.Add(new ModelField("SimplePlanDate"));
        ModelCenter.Fields.Add(new ModelField("PermMax"));
        ModelCenter.Fields.Add(new ModelField("PermMin"));
        ModelCenter.Fields.Add(new ModelField("PermAvg"));

        ChartCenter.LegendConfig = new ChartLegend
        {
            Position = LegendPosition.Right,
        };

        ChartCenter.Axes.Clear();
        ChartCenter.Axes.Add(new NumericAxis
        {
            Position = Position.Left,
            Title = itemName + "(" + permMin + "-" + permMax + ")",
        });
        ChartCenter.Axes.Add(new CategoryAxis
        {
            Position = Position.Bottom,
            Title = beginDate + "至" + endDate + " 数据项目：" + itemName + " 标准范围：(" + permMin + "-" + permMax + ")",
            Fields = new string[] { "SimplePlanDate" },
        });

        ChartCenter.Series.Clear();
        ChartCenter.Series.Add(new LineSeries
        {
            Axis = Position.Left,
            Title = "上限",
            XField = new string[] { "SimplePlanDate" },
            YField = new string[] { "PermMax" },
            ShowMarkers = true,
            MarkerConfig = new SpriteAttributes
            {
                Type = SpriteType.Circle,
            },
        });
        ChartCenter.Series.Add(new LineSeries
        {
            Axis = Position.Left,
            Title = "下限",
            XField = new string[] { "SimplePlanDate" },
            YField = new string[] { "PermMin" },
            MarkerConfig = new SpriteAttributes
            {
                Type = SpriteType.Circle,
            },
        });
        ChartCenter.Series.Add(new LineSeries
        {
            Axis = Position.Left,
            Title = "中值",
            XField = new string[] { "SimplePlanDate" },
            YField = new string[] { "PermAvg" },
            MarkerConfig = new SpriteAttributes
            {
                Type = SpriteType.Circle,
            },
        });

        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            ModelCenter.Fields.Add(new ModelField
            {
                Name = dr["TypeName"].ToString(),
                Convert =
                {
                    Handler = @"if (value === null) { value = undefined; } return value;",
                },
            });

            ChartCenter.Series.Add(new LineSeries
            {
                Axis = Position.Left,
                Title = dr["TypeName"].ToString(),
                XField = new string[] { "SimplePlanDate" },
                YField = new string[] { dr["TypeName"].ToString() },
                MarkerConfig = new SpriteAttributes
                {
                    Type = SpriteType.Cross,
                },
            });
        }

        StoreCenter.DataSource = ds.Tables[0];
        StoreCenter.DataBind();

        ChartCenter.Render();

        #endregion 设置图表


        #region 设置列表

        ModelEast.Fields.Clear();
        ModelEast.Fields.Add(new ModelField("日期"));

        GridPanelEast.ColumnModel.Columns.Clear();
        GridPanelEast.ColumnModel.Columns.Add(new Column { DataIndex = "日期", Text = "日期" });

        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            ModelEast.Fields.Add(new ModelField(dr["TypeName"].ToString()));
            GridPanelEast.ColumnModel.Columns.Add(new Column { DataIndex = dr["TypeName"].ToString(), Text = dr["TypeName"].ToString() });
        }

        StoreEast.DataSource = ds.Tables[0];
        StoreEast.DataBind();

        GridPanelEast.Render();

        #endregion 设置列表

        X.Msg.Alert("提示", "查询完毕").Show();
    }

    /// <summary>
    /// 导出表格
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ButtonNorthExport_Click(object sender, DirectEventArgs e)
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
        ed.ExcelFileDown(dt, "胶料质检项目均值趋势");
    }
}