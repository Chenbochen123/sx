using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;

using NBear.Common;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.Util;
using NPOI.HSSF.Util;
using NPOI.SS.Util;

public partial class Manager_RubberQuality_Manage_CheckRubberQualityZJSCPKReport : BasePage
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

            DateFieldNorthBeginDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldNorthEndDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));

        }

    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        string beginPlanDate = DateFieldNorthBeginDate.RawText;
        string endPlanDate = DateFieldNorthEndDate.RawText;

        HiddenBeginDate.SetValue(beginPlanDate);
        HiddenEndDate.SetValue(endPlanDate);

        IQmtRubberQualityZJSCPKReportParams paras = new QmtRubberQualityZJSCPKReportParams();
        paras.BeginPlanDate = beginPlanDate;
        paras.EndPlanDate = endPlanDate;

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();

        DataSet ds = bQmtCheckMasterManager.GetCheckRubberQualityZJSCPKReportByParas(paras);

        ModelCenter.Fields.Clear();

        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            ModelCenter.Fields.Add(new ModelField { Name = dc.ColumnName });
        }

        GridPanelCenter.ColumnModel.Columns.Clear();
        foreach (DataColumn dc in ds.Tables[0].Columns)
        {
            GridPanelCenter.ColumnModel.Columns.Add(new Column { DataIndex = dc.ColumnName, Text = dc.ColumnName });
        }


        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();

        //GridPanelCenter.Reconfigure();
        GridPanelCenter.Render();

        X.Msg.Alert("提示", "查询完毕").Show();

    }

    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "胶料质检主机手CPK日报表");

    }


    /// <summary>
    /// 设置单元格内容及格式
    /// </summary>
    /// <param name="cell">单元格</param>
    /// <param name="value">内容</param>
    /// <param name="cellStyle">格式</param>
    private void SetCellValue(ICell cell, object value, ICellStyle cellStyle = null)
    {
        if (value == null)
        {
            cell.SetCellValue("");
        }
        else if (value is string)
        {
            cell.SetCellValue(Convert.ToString(value));
        }
        else if (value is int)
        {
            cell.SetCellValue(Convert.ToDouble(value));
        }
        else if (value is long)
        {
            cell.SetCellValue(Convert.ToDouble(value));
        }
        else if (value is double)
        {
            cell.SetCellValue(Convert.ToDouble(value));
        }
        else
        {
            cell.SetCellValue(Convert.ToString(value.GetType()));
        }

        if (cellStyle != null)
        {
            cell.CellStyle = cellStyle;
        }

    }

    /// <summary>
    /// 合并单元格
    /// </summary>
    /// <param name="sheet">要合并单元格所在的sheet</param>
    /// <param name="rowStart">开始行的索引</param>
    /// <param name="rowEnd">结束行的索引</param>
    /// <param name="colStart">开始列的索引</param>
    /// <param name="colEnd">结束列的索引</param>
    private void SetCellRangeAddress(ISheet sheet, int rowStart, int rowEnd, int colStart, int colEnd)
    {
        CellRangeAddress cellRangeAddress = new CellRangeAddress(rowStart, rowEnd, colStart, colEnd);
        sheet.AddMergedRegion(cellRangeAddress);
    }

    /// <summary>
    /// 获取单元格格式
    /// </summary>
    /// <param name="workbook">Excel工作簿</param>
    /// <param name="cellDataFormat">单元格格式</param>
    /// <param name="cellFillForegroundColor">前景色</param>
    /// <param name="cellHorizontalAlignment">左右对齐方式</param>
    /// <param name="cellVerticalAlignment">上下对齐方式</param>
    /// <returns></returns>
    private ICellStyle GetCellStyle(IWorkbook workbook
        , string cellDataFormat = ""
        , Nullable<short> cellFillForegroundColor = null
        , Nullable<HorizontalAlignment> cellHorizontalAlignment = null
        , Nullable<VerticalAlignment> cellVerticalAlignment = null
        )
    {
        ICellStyle cellStyle = workbook.CreateCellStyle();

        if (string.IsNullOrEmpty(cellDataFormat) == false)
        {
            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat(cellDataFormat);
        }
        if (cellHorizontalAlignment != null)
        {
            cellStyle.Alignment = cellHorizontalAlignment.Value;
        }
        if (cellVerticalAlignment != null)
        {
            cellStyle.VerticalAlignment = cellVerticalAlignment.Value;
        }
        if (cellFillForegroundColor != null)
        {
            cellStyle.FillForegroundColor = cellFillForegroundColor.Value;
            cellStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
        }

        return cellStyle;
    }

    /// <summary>
    /// 获取字体样式
    /// </summary>
    /// <param name="workbook">Excel工作簿</param>
    /// <param name="fontname">字体名</param>
    /// <param name="fontcolor">字体颜色</param>
    /// <param name="fontsize">字体大小</param>
    /// <returns></returns>
    private IFont GetFontStyle(IWorkbook workbook
        , string fontFamily = "", HSSFColor fontColor = null, Nullable<short> fontSize = null
        , Nullable<short> fontBoldweight = null, Nullable<bool> fontIsItalic = null, Nullable<bool> fontStrikeout = null
        , FontUnderline fontUnderline = null)
    {
        IFont font = workbook.CreateFont();
        if (string.IsNullOrEmpty(fontFamily) == false)
        {
            font.FontName = fontFamily;
        }
        if (fontColor != null)
        {
            font.Color = fontColor.GetIndex();
        }
        if (fontSize != null)
        {
            font.FontHeightInPoints = (short)fontSize;
        }

        if (fontBoldweight != null)
        {
            font.Boldweight = fontBoldweight.Value;
        }
        if (fontIsItalic != null)
        {
            font.IsItalic = fontIsItalic.Value;
        }
        if (fontStrikeout != null)
        {
            font.IsStrikeout = fontStrikeout.Value;
        }
        if (fontUnderline != null)
        {
            font.Underline = fontUnderline.ByteValue;
        }

        return font;
    }
}