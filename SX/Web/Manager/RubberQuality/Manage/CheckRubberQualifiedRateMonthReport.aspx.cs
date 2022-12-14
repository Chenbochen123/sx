using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;

using NBear.Common;

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

public partial class Manager_RubberQuality_Manage_CheckRubberQualifiedRateMonthReport : BasePage
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
    /// 加载页面
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

            if (ComboBoxNorthRubTypeCode.Items.Count > 0)
            {
                ComboBoxNorthRubTypeCode.Select(0);
            }
        }
    }

    /// <summary>
    /// 加载控件
    /// </summary>
    private void InitControls()
    {
        // 加载胶料分类
        IBasRubTypeManager bBasRubTypeManager = new BasRubTypeManager();
        EntityArrayList<BasRubType> mBasRubTypeList = bBasRubTypeManager.GetListByWhereAndOrder(
            BasRubType._.DeleteFlag == "0"
            , BasRubType._.ObjID.Asc);
        foreach (BasRubType mBasRubType in mBasRubTypeList)
        {
            ComboBoxNorthRubTypeCode.Items.Add(new Ext.Net.ListItem { Text = mBasRubType.RubTypeName, Value = mBasRubType.ObjID.ToString() });
            //ComboBoxNorthRubTypeCode.AddItem(mBasRubType.RubTypeName, mBasRubType.ObjID.ToString());
        }

        // 加载主机手
        IBasMainHanderManager bBasMainHanderManager = new BasMainHanderManager();
        DataSet ds = bBasMainHanderManager.GetMixMainHanderInfo();
        EntityArrayList<BasMainHander> mBasMainHanderList = bBasMainHanderManager.GetListByWhereAndOrder(
            BasMainHander._.DeleteFlag == "0"
            & BasMainHander._.WorkShopCode.In(new object[] { 2, 3, 4, 5 })
            , BasMainHander._.MainHanderCode.Asc);
        foreach (BasMainHander mBasMainHander in mBasMainHanderList)
        {
            ComboBoxNorthZJSID.Items.Add(new Ext.Net.ListItem { Text = mBasMainHander.MainHanderCode, Value = mBasMainHander.MainHanderCode });
            //ComboBoxNorthZJSID.AddItem(mBasMainHander.MainHanderCode, mBasMainHander.MainHanderCode);
        }

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
        string beginPlanDate = DateFieldNorthBeginDate.RawText;
        string endPlanDate = DateFieldNorthEndDate.RawText;
        string rubTypeCode = ComboBoxNorthRubTypeCode.Value.ToString();
        string rubTypeName = ComboBoxNorthRubTypeCode.RawText;
        string standCode = ComboBoxNorthStandCode.Value.ToString();
        string zjsID = ComboBoxNorthZJSID.Value.ToString();

        HiddenRubTypeName.SetValue(rubTypeName);
        HiddenBeginDate.SetValue(beginPlanDate);
        HiddenEndDate.SetValue(endPlanDate);

        IQmtRubberQualifiedRateMonthReportParams paras = new QmtRubberQualifiedRateMonthReportParams();
        paras.BeginPlanDate = beginPlanDate;
        paras.EndPlanDate = endPlanDate;
        paras.RubTypeCode = rubTypeCode;
        paras.StandCode = standCode;
        paras.ZJSID = zjsID;

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        DataSet ds = bQmtCheckMasterManager.GetCheckRubberQualifiedRateMonthReportByParas(paras);

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
        else if (jsArrayRecords.Count == 1)
        {
            X.Msg.Alert("提示", "未查询到符合条件的结果，不能导出").Show();
            return;
        }

        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("Sheet1");

        IRow row = null;
        ICell cell = null;

        // 首行标题
        int rownum = 0;
        string rubTypeName = HiddenRubTypeName.Value.ToString();
        row = sheet.CreateRow(rownum);
        cell = row.CreateCell(0);

        ICellStyle cellStyleTitle = GetCellStyle(workbook, "", null
            , HorizontalAlignment.CENTER, VerticalAlignment.CENTER);
        IFont fontTitle = GetFontStyle(workbook, "宋体", null, 15, (short)FontBoldWeight.BOLD);
        cellStyleTitle.SetFont(fontTitle);
        string title = rubTypeName + "胶料月合格率报表";
        SetCellValue(cell, title, cellStyleTitle);
        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum, 0, 22);

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
        string info = "生产日期:" + beginDate + "至" + endDate + "    打印时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        SetCellValue(cell, info, cellStyleInfo);
        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum, 0, 22);

        // 表头
        // 第一行
        rownum = rownum + 1;
        row = sheet.CreateRow(rownum);
        SetCellValue(row.CreateCell(0), "胶名", cellStyleInfo);
        SetCellValue(row.CreateCell(1), "总车数", cellStyleInfo);
        SetCellValue(row.CreateCell(2), "合格数", cellStyleInfo);
        SetCellValue(row.CreateCell(3), "不合格数", cellStyleInfo);
        SetCellValue(row.CreateCell(4), "合格率", cellStyleInfo);
        SetCellValue(row.CreateCell(5), "粘度", cellStyleInfo);
        SetCellValue(row.CreateCell(7), "焦烧", cellStyleInfo);
        SetCellValue(row.CreateCell(9), "硬度", cellStyleInfo);
        SetCellValue(row.CreateCell(11), "比重", cellStyleInfo);
        SetCellValue(row.CreateCell(13), "ML", cellStyleInfo);
        SetCellValue(row.CreateCell(15), "MH", cellStyleInfo);
        SetCellValue(row.CreateCell(17), "T30", cellStyleInfo);
        SetCellValue(row.CreateCell(19), "T60", cellStyleInfo);
        SetCellValue(row.CreateCell(21), "抽出", cellStyleInfo);
        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum + 1, 0, 0);
        SetCellRangeAddress(sheet, rownum, rownum + 1, 1, 1);
        SetCellRangeAddress(sheet, rownum, rownum + 1, 2, 2);
        SetCellRangeAddress(sheet, rownum, rownum + 1, 3, 3);
        SetCellRangeAddress(sheet, rownum, rownum + 1, 4, 4);
        SetCellRangeAddress(sheet, rownum, rownum, 5, 6);
        SetCellRangeAddress(sheet, rownum, rownum, 7, 8);
        SetCellRangeAddress(sheet, rownum, rownum, 9, 10);
        SetCellRangeAddress(sheet, rownum, rownum, 11, 12);
        SetCellRangeAddress(sheet, rownum, rownum, 13, 14);
        SetCellRangeAddress(sheet, rownum, rownum, 15, 16);
        SetCellRangeAddress(sheet, rownum, rownum, 17, 18);
        SetCellRangeAddress(sheet, rownum, rownum, 19, 20);
        SetCellRangeAddress(sheet, rownum, rownum, 21, 22);

        // 第二行
        rownum = rownum + 1;
        row = sheet.CreateRow(rownum);
        SetCellValue(row.CreateCell(5), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(6), "-", cellStyleInfo);
        SetCellValue(row.CreateCell(7), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(8), "-", cellStyleInfo);
        SetCellValue(row.CreateCell(9), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(10), "-", cellStyleInfo);
        SetCellValue(row.CreateCell(11), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(12), "-", cellStyleInfo);
        SetCellValue(row.CreateCell(13), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(14), "-", cellStyleInfo);
        SetCellValue(row.CreateCell(15), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(16), "-", cellStyleInfo);
        SetCellValue(row.CreateCell(17), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(18), "-", cellStyleInfo);
        SetCellValue(row.CreateCell(19), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(20), "-", cellStyleInfo);
        SetCellValue(row.CreateCell(21), "+", cellStyleInfo);
        SetCellValue(row.CreateCell(22), "-", cellStyleInfo);

        // 数据行
        int index = 0;
        int len = jsArrayRecords.Count;
        foreach (Newtonsoft.Json.JavaScriptObject jsObjectRecord in jsArrayRecords)
        {
            index = index + 1;
            ICellStyle cellStyle = null;
            IFont font = null;
            if (index == len)
            {
                // 总计行格式
                cellStyle = GetCellStyle(workbook, "", HSSFColor.YELLOW.index);
            }
            else
            {
                cellStyle = GetCellStyle(workbook);
            }
            // 根据合格率设置字体颜色
            if (Convert.ToDouble(jsObjectRecord["QuaRate"]) == 1)
            {
                font = GetFontStyle(workbook, "宋体", new HSSFColor.BLUE(), 11);
            }
            else if (Convert.ToDouble(jsObjectRecord["QuaRate"]) >= 0.9)
            {
                font = GetFontStyle(workbook, "宋体", new HSSFColor.GREEN(), 11);
            }
            else
            {
                font = GetFontStyle(workbook, "宋体", new HSSFColor.RED(), 11);
            }
            cellStyle.SetFont(font);

            // 合格率列的格式(百分比)
            ICellStyle cellStyleRate = workbook.CreateCellStyle();
            cellStyleRate.CloneStyleFrom(cellStyle);
            cellStyleRate.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");

            rownum = rownum + 1;
            row = sheet.CreateRow(rownum);
            SetCellValue(row.CreateCell(0), jsObjectRecord["MaterName"], cellStyle);
            SetCellValue(row.CreateCell(1), jsObjectRecord["Amount"], cellStyle);
            SetCellValue(row.CreateCell(2), jsObjectRecord["QuaAmount"], cellStyle);
            SetCellValue(row.CreateCell(3), jsObjectRecord["UnquaAmount"], cellStyle);
            SetCellValue(row.CreateCell(4), jsObjectRecord["QuaRate"], cellStyleRate);
            SetCellValue(row.CreateCell(5), jsObjectRecord["Amount_MN_Up"], cellStyle);
            SetCellValue(row.CreateCell(6), jsObjectRecord["Amount_MN_Down"], cellStyle);
            SetCellValue(row.CreateCell(7), jsObjectRecord["Amount_JS_Up"], cellStyle);
            SetCellValue(row.CreateCell(8), jsObjectRecord["Amount_JS_Down"], cellStyle);
            SetCellValue(row.CreateCell(9), jsObjectRecord["Amount_YD_Up"], cellStyle);
            SetCellValue(row.CreateCell(10), jsObjectRecord["Amount_YD_Down"], cellStyle);
            SetCellValue(row.CreateCell(11), jsObjectRecord["Amount_BZ_Up"], cellStyle);
            SetCellValue(row.CreateCell(12), jsObjectRecord["Amount_BZ_Down"], cellStyle);
            SetCellValue(row.CreateCell(13), jsObjectRecord["Amount_ML_Up"], cellStyle);
            SetCellValue(row.CreateCell(14), jsObjectRecord["Amount_ML_Down"], cellStyle);
            SetCellValue(row.CreateCell(15), jsObjectRecord["Amount_MH_Up"], cellStyle);
            SetCellValue(row.CreateCell(16), jsObjectRecord["Amount_MH_Down"], cellStyle);
            SetCellValue(row.CreateCell(17), jsObjectRecord["Amount_T30_Up"], cellStyle);
            SetCellValue(row.CreateCell(18), jsObjectRecord["Amount_T30_Down"], cellStyle);
            SetCellValue(row.CreateCell(19), jsObjectRecord["Amount_T60_Up"], cellStyle);
            SetCellValue(row.CreateCell(20), jsObjectRecord["Amount_T60_Down"], cellStyle);
            SetCellValue(row.CreateCell(21), jsObjectRecord["Amount_CC_Up"], cellStyle);
            SetCellValue(row.CreateCell(22), jsObjectRecord["Amount_CC_Down"], cellStyle);

        }

        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);

        new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, title);

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