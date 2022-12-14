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

public partial class Manager_RubberQuality_Manage_CheckRubberQualityCPKDailyReport : BasePage
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

            InitControls();

            DateFieldNorthBeginDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldNorthEndDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));

            if (ComboBoxNorthStandCode.Items.Count > 0)
            {
                ComboBoxNorthStandCode.Select(0);
            }

        }

    }

    /// <summary>
    /// 加载控件
    /// </summary>
    private void InitControls()
    {
        // 加载标准分类
        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(
            QmtCheckStandType._.DeleteFlag == "0"
            & QmtCheckStandType._.CheckTypeCode.In(new object[] { 2 })
            & QmtCheckStandType._.WorkShopId != null
            , QmtCheckStandType._.ObjID.Asc);
        foreach (QmtCheckStandType mQmtCheckStandType in mQmtCheckStandTypeList)
        {
            ComboBoxNorthStandCode.Items.Add(new Ext.Net.ListItem { Text = mQmtCheckStandType.StandTypeName, Value = mQmtCheckStandType.ObjID.ToString() });
            //ComboBoxNorthStandCode.AddItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString());
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        string materCode = HiddenNorthMaterCode.Value.ToString();
        if (materCode == "")
        {
            X.Msg.Alert("提示", "请先选择胶料").Show();
            return;
        }
        string standCode = ComboBoxNorthStandCode.Value.ToString();
        if (standCode == "")
        {
            X.Msg.Alert("提示", "请先选择车间标准").Show();
            return;
        }
        string beginPlanDate = DateFieldNorthBeginDate.RawText;
        string endPlanDate = DateFieldNorthEndDate.RawText;
        string materName = TriggerFieldNorthMaterName.Value.ToString();

        HiddenMaterName.SetValue(materName);
        HiddenBeginDate.SetValue(beginPlanDate);
        HiddenEndDate.SetValue(endPlanDate);

        IQmtRubberQualityCPKDailyReportParams paras = new QmtRubberQualityCPKDailyReportParams();
        paras.BeginPlanDate = beginPlanDate;
        paras.EndPlanDate = endPlanDate;
        paras.MaterCode = materCode;
        paras.StandCode = standCode;

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        DataSet ds = bQmtCheckMasterManager.GetCheckRubberQualityCPKDailyReportByParas(paras);

        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();

        X.Msg.Alert("提示", "查询完毕").Show();

    }

    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthExport_Click(object sender, DirectEventArgs e)
    {
        string records = e.ExtraParams["records"];
        Newtonsoft.Json.JavaScriptArray jsArrayRecords = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(records) as Newtonsoft.Json.JavaScriptArray;

        if (jsArrayRecords.Count == 0)
        {
            X.Msg.Alert("提示", "请先查询出结果后再导出").Show();
            return;
        }
        string materName = HiddenMaterName.Value.ToString().Replace("\\", "").Replace("/", "");
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet(materName);

        IRow row = null;
        ICell cell = null;

        // 首行标题
        int rownum = 0;
        row = sheet.CreateRow(rownum);
        cell = row.CreateCell(0);

        ICellStyle cellStyleTitle = GetCellStyle(workbook, "", null
            , HorizontalAlignment.CENTER, VerticalAlignment.CENTER);
        IFont fontTitle = GetFontStyle(workbook, "宋体", null, 15, (short)FontBoldWeight.BOLD);
        cellStyleTitle.SetFont(fontTitle);
        string title = "胶料质检CPK日报表";
        SetCellValue(cell, title, cellStyleTitle);
        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum, 0, 31);

        // 第二行 副标题
        rownum = rownum + 1;
        string beginDate = HiddenBeginDate.Value.ToString();
        string endDate = HiddenEndDate.Value.ToString();
        row = sheet.CreateRow(rownum);
        cell = row.CreateCell(0);
        ICellStyle cellStyleInfo = GetCellStyle(workbook, "", null
            , HorizontalAlignment.CENTER, VerticalAlignment.CENTER);
        IFont fontInfo = GetFontStyle(workbook, "宋体", null, 11);
        cellStyleInfo.SetFont(fontInfo);
        string info = beginDate + "至" + endDate;
        SetCellValue(cell, info, cellStyleInfo);
        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum, 0, 31);

        // 表头
        // 第一行
        rownum = rownum + 1;
        row = sheet.CreateRow(rownum);
        SetCellValue(row.CreateCell(0), "日期", cellStyleInfo);
        SetCellValue(row.CreateCell(1), "总车数", cellStyleInfo);
        SetCellValue(row.CreateCell(2), "合格数", cellStyleInfo);
        SetCellValue(row.CreateCell(3), "不合格数", cellStyleInfo);
        SetCellValue(row.CreateCell(4), "合格率", cellStyleInfo);
        SetCellValue(row.CreateCell(5), "粘度", cellStyleInfo);
        SetCellValue(row.CreateCell(8), "焦烧", cellStyleInfo);
        SetCellValue(row.CreateCell(11), "ML", cellStyleInfo);
        SetCellValue(row.CreateCell(14), "MH", cellStyleInfo);
        SetCellValue(row.CreateCell(17), "硬度", cellStyleInfo);
        SetCellValue(row.CreateCell(20), "比重", cellStyleInfo);
        SetCellValue(row.CreateCell(23), "T30", cellStyleInfo);
        SetCellValue(row.CreateCell(26), "T60", cellStyleInfo);
        SetCellValue(row.CreateCell(29), "抽出", cellStyleInfo);
        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum + 1, 0, 0);
        SetCellRangeAddress(sheet, rownum, rownum + 1, 1, 1);
        SetCellRangeAddress(sheet, rownum, rownum + 1, 2, 2);
        SetCellRangeAddress(sheet, rownum, rownum + 1, 3, 3);
        SetCellRangeAddress(sheet, rownum, rownum + 1, 4, 4);
        SetCellRangeAddress(sheet, rownum, rownum, 5, 7);
        SetCellRangeAddress(sheet, rownum, rownum, 8, 10);
        SetCellRangeAddress(sheet, rownum, rownum, 11, 13);
        SetCellRangeAddress(sheet, rownum, rownum, 14, 16);
        SetCellRangeAddress(sheet, rownum, rownum, 17, 19);
        SetCellRangeAddress(sheet, rownum, rownum, 20, 22);
        SetCellRangeAddress(sheet, rownum, rownum, 23, 25);
        SetCellRangeAddress(sheet, rownum, rownum, 26, 28);
        SetCellRangeAddress(sheet, rownum, rownum, 29, 31);

        // 第二行
        rownum = rownum + 1;
        row = sheet.CreateRow(rownum);
        SetCellValue(row.CreateCell(5), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(6), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(7), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(8), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(9), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(10), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(11), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(12), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(13), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(14), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(15), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(16), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(17), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(18), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(19), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(20), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(21), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(22), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(23), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(24), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(25), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(26), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(27), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(28), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(29), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(30), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(31), "CPK", cellStyleInfo);

        // 数据行
        int index = 0;
        int len = jsArrayRecords.Count;
        foreach (Newtonsoft.Json.JavaScriptObject jsObjectRecord in jsArrayRecords)
        {
            index = index + 1;
            ICellStyle cellStyle = GetCellStyle(workbook);
            IFont font = font = GetFontStyle(workbook, "宋体", null, 11);
            cellStyle.SetFont(font);

            // 合格率列的格式(百分比)
            ICellStyle cellStyleRate = workbook.CreateCellStyle();
            cellStyleRate.CloneStyleFrom(cellStyle);
            cellStyleRate.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");

            rownum = rownum + 1;
            row = sheet.CreateRow(rownum);
            SetCellValue(row.CreateCell(0), jsObjectRecord["日期"], cellStyle);
            SetCellValue(row.CreateCell(1), jsObjectRecord["总车数"], cellStyle);
            SetCellValue(row.CreateCell(2), jsObjectRecord["合格数"], cellStyle);
            SetCellValue(row.CreateCell(3), jsObjectRecord["不合格数"], cellStyle);
            SetCellValue(row.CreateCell(4), jsObjectRecord["合格率2"], cellStyleRate);
            SetCellValue(row.CreateCell(5), jsObjectRecord["粘度_CA"], cellStyle);
            SetCellValue(row.CreateCell(6), jsObjectRecord["粘度_CP"], cellStyle);
            SetCellValue(row.CreateCell(7), jsObjectRecord["粘度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(8), jsObjectRecord["焦烧_CA"], cellStyle);
            SetCellValue(row.CreateCell(9), jsObjectRecord["焦烧_CP"], cellStyle);
            SetCellValue(row.CreateCell(10), jsObjectRecord["焦烧_CPK"], cellStyle);
            SetCellValue(row.CreateCell(11), jsObjectRecord["ML_CA"], cellStyle);
            SetCellValue(row.CreateCell(12), jsObjectRecord["ML_CP"], cellStyle);
            SetCellValue(row.CreateCell(13), jsObjectRecord["ML_CPK"], cellStyle);
            SetCellValue(row.CreateCell(14), jsObjectRecord["MH_CA"], cellStyle);
            SetCellValue(row.CreateCell(15), jsObjectRecord["MH_CP"], cellStyle);
            SetCellValue(row.CreateCell(16), jsObjectRecord["MH_CPK"], cellStyle);
            SetCellValue(row.CreateCell(17), jsObjectRecord["硬度_CA"], cellStyle);
            SetCellValue(row.CreateCell(18), jsObjectRecord["硬度_CP"], cellStyle);
            SetCellValue(row.CreateCell(19), jsObjectRecord["硬度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(20), jsObjectRecord["比重_CA"], cellStyle);
            SetCellValue(row.CreateCell(21), jsObjectRecord["比重_CP"], cellStyle);
            SetCellValue(row.CreateCell(22), jsObjectRecord["比重_CPK"], cellStyle);
            SetCellValue(row.CreateCell(23), jsObjectRecord["T30_CA"], cellStyle);
            SetCellValue(row.CreateCell(24), jsObjectRecord["T30_CP"], cellStyle);
            SetCellValue(row.CreateCell(25), jsObjectRecord["T30_CPK"], cellStyle);
            SetCellValue(row.CreateCell(26), jsObjectRecord["T60_CA"], cellStyle);
            SetCellValue(row.CreateCell(27), jsObjectRecord["T60_CP"], cellStyle);
            SetCellValue(row.CreateCell(28), jsObjectRecord["T60_CPK"], cellStyle);
            SetCellValue(row.CreateCell(29), jsObjectRecord["抽出_CA"], cellStyle);
            SetCellValue(row.CreateCell(30), jsObjectRecord["抽出_CP"], cellStyle);
            SetCellValue(row.CreateCell(31), jsObjectRecord["抽出_CPK"], cellStyle);

        }

        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);

        new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, "胶料质检CPK日报表");

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