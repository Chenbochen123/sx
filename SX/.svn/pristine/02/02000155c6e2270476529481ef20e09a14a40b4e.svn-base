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

public partial class Manager_RubberQuality_Manage_CheckRubberQualityWorkshopCPKReport : BasePage
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

        IQmtRubberQualityWorkshopCPKReportParams paras = new QmtRubberQualityWorkshopCPKReportParams();
        paras.BeginPlanDate = beginPlanDate;
        paras.EndPlanDate = endPlanDate;

        IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
        DataSet ds = bQmtCheckMasterManager.GetCheckRubberQualityWorkshopCPKReportByParas(paras);

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
        row = sheet.CreateRow(rownum);
        cell = row.CreateCell(0);

        ICellStyle cellStyleTitle = GetCellStyle(workbook, "", null
            , HorizontalAlignment.CENTER, VerticalAlignment.CENTER);
        IFont fontTitle = GetFontStyle(workbook, "宋体", null, 15, (short)FontBoldWeight.BOLD);
        cellStyleTitle.SetFont(fontTitle);
        string title = "胶料质检车间CPK日报表";
        SetCellValue(cell, title, cellStyleTitle);
        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum, 0, 135);

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
        SetCellRangeAddress(sheet, rownum, rownum, 0, 135);

        // 表头
        // 第一行
        rownum = rownum + 1;
        row = sheet.CreateRow(rownum);
        SetCellValue(row.CreateCell(0), "生产日期", cellStyleInfo);
        SetCellValue(row.CreateCell(1), "M2", cellStyleInfo);
        SetCellValue(row.CreateCell(28), "M3", cellStyleInfo);
        SetCellValue(row.CreateCell(55), "M4", cellStyleInfo);
        SetCellValue(row.CreateCell(82), "M5", cellStyleInfo);
        SetCellValue(row.CreateCell(109), "综合", cellStyleInfo);
        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum + 2, 0, 0);
        SetCellRangeAddress(sheet, rownum, rownum, 1, 27);
        SetCellRangeAddress(sheet, rownum, rownum, 28, 54);
        SetCellRangeAddress(sheet, rownum, rownum, 55, 81);
        SetCellRangeAddress(sheet, rownum, rownum, 82, 108);
        SetCellRangeAddress(sheet, rownum, rownum, 109, 135);

        // 第二行
        rownum = rownum + 1;
        row = sheet.CreateRow(rownum);
        SetCellValue(row.CreateCell(1), "粘度", cellStyleInfo);
        SetCellValue(row.CreateCell(4), "焦烧", cellStyleInfo);
        SetCellValue(row.CreateCell(7), "硬度", cellStyleInfo);
        SetCellValue(row.CreateCell(10), "比重", cellStyleInfo);
        SetCellValue(row.CreateCell(13), "ML", cellStyleInfo);
        SetCellValue(row.CreateCell(16), "MH", cellStyleInfo);
        SetCellValue(row.CreateCell(19), "T30", cellStyleInfo);
        SetCellValue(row.CreateCell(22), "T60", cellStyleInfo);
        SetCellValue(row.CreateCell(25), "抽出", cellStyleInfo);

        SetCellValue(row.CreateCell(28), "粘度", cellStyleInfo);
        SetCellValue(row.CreateCell(31), "焦烧", cellStyleInfo);
        SetCellValue(row.CreateCell(34), "硬度", cellStyleInfo);
        SetCellValue(row.CreateCell(37), "比重", cellStyleInfo);
        SetCellValue(row.CreateCell(40), "ML", cellStyleInfo);
        SetCellValue(row.CreateCell(43), "MH", cellStyleInfo);
        SetCellValue(row.CreateCell(46), "T30", cellStyleInfo);
        SetCellValue(row.CreateCell(49), "T60", cellStyleInfo);
        SetCellValue(row.CreateCell(52), "抽出", cellStyleInfo);

        SetCellValue(row.CreateCell(55), "粘度", cellStyleInfo);
        SetCellValue(row.CreateCell(58), "焦烧", cellStyleInfo);
        SetCellValue(row.CreateCell(61), "硬度", cellStyleInfo);
        SetCellValue(row.CreateCell(64), "比重", cellStyleInfo);
        SetCellValue(row.CreateCell(67), "ML", cellStyleInfo);
        SetCellValue(row.CreateCell(70), "MH", cellStyleInfo);
        SetCellValue(row.CreateCell(73), "T30", cellStyleInfo);
        SetCellValue(row.CreateCell(76), "T60", cellStyleInfo);
        SetCellValue(row.CreateCell(79), "抽出", cellStyleInfo);

        SetCellValue(row.CreateCell(82), "粘度", cellStyleInfo);
        SetCellValue(row.CreateCell(85), "焦烧", cellStyleInfo);
        SetCellValue(row.CreateCell(88), "硬度", cellStyleInfo);
        SetCellValue(row.CreateCell(91), "比重", cellStyleInfo);
        SetCellValue(row.CreateCell(94), "ML", cellStyleInfo);
        SetCellValue(row.CreateCell(97), "MH", cellStyleInfo);
        SetCellValue(row.CreateCell(100), "T30", cellStyleInfo);
        SetCellValue(row.CreateCell(103), "T60", cellStyleInfo);
        SetCellValue(row.CreateCell(106), "抽出", cellStyleInfo);

        SetCellValue(row.CreateCell(109), "粘度", cellStyleInfo);
        SetCellValue(row.CreateCell(112), "焦烧", cellStyleInfo);
        SetCellValue(row.CreateCell(115), "硬度", cellStyleInfo);
        SetCellValue(row.CreateCell(118), "比重", cellStyleInfo);
        SetCellValue(row.CreateCell(121), "ML", cellStyleInfo);
        SetCellValue(row.CreateCell(124), "MH", cellStyleInfo);
        SetCellValue(row.CreateCell(127), "T30", cellStyleInfo);
        SetCellValue(row.CreateCell(130), "T60", cellStyleInfo);
        SetCellValue(row.CreateCell(133), "抽出", cellStyleInfo);

        // 合并单元格
        SetCellRangeAddress(sheet, rownum, rownum, 1, 3);
        SetCellRangeAddress(sheet, rownum, rownum, 4, 6);
        SetCellRangeAddress(sheet, rownum, rownum, 7, 9);
        SetCellRangeAddress(sheet, rownum, rownum, 10, 12);
        SetCellRangeAddress(sheet, rownum, rownum, 13, 15);
        SetCellRangeAddress(sheet, rownum, rownum, 16, 18);
        SetCellRangeAddress(sheet, rownum, rownum, 19, 21);
        SetCellRangeAddress(sheet, rownum, rownum, 22, 24);
        SetCellRangeAddress(sheet, rownum, rownum, 25, 27);

        SetCellRangeAddress(sheet, rownum, rownum, 28, 30);
        SetCellRangeAddress(sheet, rownum, rownum, 31, 33);
        SetCellRangeAddress(sheet, rownum, rownum, 34, 36);
        SetCellRangeAddress(sheet, rownum, rownum, 37, 39);
        SetCellRangeAddress(sheet, rownum, rownum, 40, 42);
        SetCellRangeAddress(sheet, rownum, rownum, 43, 45);
        SetCellRangeAddress(sheet, rownum, rownum, 46, 48);
        SetCellRangeAddress(sheet, rownum, rownum, 49, 51);
        SetCellRangeAddress(sheet, rownum, rownum, 52, 54);

        SetCellRangeAddress(sheet, rownum, rownum,55, 57);
        SetCellRangeAddress(sheet, rownum, rownum, 58, 60);
        SetCellRangeAddress(sheet, rownum, rownum, 61, 63);
        SetCellRangeAddress(sheet, rownum, rownum, 64, 66);
        SetCellRangeAddress(sheet, rownum, rownum, 67, 69);
        SetCellRangeAddress(sheet, rownum, rownum, 70, 72);
        SetCellRangeAddress(sheet, rownum, rownum, 73, 75);
        SetCellRangeAddress(sheet, rownum, rownum, 76, 78);
        SetCellRangeAddress(sheet, rownum, rownum, 79, 81);

        SetCellRangeAddress(sheet, rownum, rownum, 82, 84);
        SetCellRangeAddress(sheet, rownum, rownum, 85, 87);
        SetCellRangeAddress(sheet, rownum, rownum, 88, 90);
        SetCellRangeAddress(sheet, rownum, rownum, 91, 93);
        SetCellRangeAddress(sheet, rownum, rownum, 94, 96);
        SetCellRangeAddress(sheet, rownum, rownum, 97, 99);
        SetCellRangeAddress(sheet, rownum, rownum, 100, 102);
        SetCellRangeAddress(sheet, rownum, rownum, 103, 105);
        SetCellRangeAddress(sheet, rownum, rownum, 106, 108);

        SetCellRangeAddress(sheet, rownum, rownum, 109, 111);
        SetCellRangeAddress(sheet, rownum, rownum, 112, 114);
        SetCellRangeAddress(sheet, rownum, rownum, 115, 117);
        SetCellRangeAddress(sheet, rownum, rownum, 118, 120);
        SetCellRangeAddress(sheet, rownum, rownum, 121, 123);
        SetCellRangeAddress(sheet, rownum, rownum, 124, 126);
        SetCellRangeAddress(sheet, rownum, rownum, 127, 129);
        SetCellRangeAddress(sheet, rownum, rownum, 130, 132);
        SetCellRangeAddress(sheet, rownum, rownum, 133, 135);

        // 第三行
        rownum = rownum + 1;
        row = sheet.CreateRow(rownum);
        SetCellValue(row.CreateCell(1), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(2), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(3), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(4), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(5), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(6), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(7), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(8), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(9), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(10), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(11), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(12), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(13), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(14), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(15), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(16), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(17), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(18), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(19), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(20), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(21), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(22), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(23), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(24), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(25), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(26), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(27), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(28), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(29), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(30), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(31), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(32), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(33), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(34), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(35), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(36), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(37), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(38), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(39), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(40), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(41), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(42), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(43), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(44), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(45), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(46), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(47), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(48), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(49), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(50), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(51), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(52), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(53), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(54), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(55), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(56), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(57), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(58), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(59), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(60), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(61), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(62), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(63), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(64), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(65), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(66), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(67), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(68), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(69), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(70), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(71), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(72), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(73), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(74), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(75), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(76), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(77), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(78), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(79), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(80), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(81), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(82), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(83), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(84), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(85), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(86), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(87), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(88), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(89), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(90), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(91), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(92), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(93), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(94), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(95), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(96), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(97), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(98), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(99), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(100), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(101), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(102), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(103), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(104), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(105), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(106), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(107), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(108), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(109), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(110), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(111), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(112), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(113), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(114), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(115), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(116), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(117), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(118), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(119), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(120), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(121), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(122), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(123), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(124), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(125), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(126), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(127), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(128), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(129), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(130), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(131), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(132), "CPK", cellStyleInfo);
        SetCellValue(row.CreateCell(133), "CA", cellStyleInfo);
        SetCellValue(row.CreateCell(134), "CP", cellStyleInfo);
        SetCellValue(row.CreateCell(135), "CPK", cellStyleInfo);

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
            SetCellValue(row.CreateCell(0), jsObjectRecord["生产日期"], cellStyle);

            SetCellValue(row.CreateCell(1), jsObjectRecord["M2_粘度_CA"], cellStyle);
            SetCellValue(row.CreateCell(2), jsObjectRecord["M2_粘度_CP"], cellStyle);
            SetCellValue(row.CreateCell(3), jsObjectRecord["M2_粘度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(4), jsObjectRecord["M2_焦烧_CA"], cellStyle);
            SetCellValue(row.CreateCell(5), jsObjectRecord["M2_焦烧_CP"], cellStyle);
            SetCellValue(row.CreateCell(6), jsObjectRecord["M2_焦烧_CPK"], cellStyle);
            SetCellValue(row.CreateCell(7), jsObjectRecord["M2_硬度_CA"], cellStyle);
            SetCellValue(row.CreateCell(8), jsObjectRecord["M2_硬度_CP"], cellStyle);
            SetCellValue(row.CreateCell(9), jsObjectRecord["M2_硬度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(10), jsObjectRecord["M2_比重_CA"], cellStyle);
            SetCellValue(row.CreateCell(11), jsObjectRecord["M2_比重_CP"], cellStyle);
            SetCellValue(row.CreateCell(12), jsObjectRecord["M2_比重_CPK"], cellStyle);
            SetCellValue(row.CreateCell(13), jsObjectRecord["M2_ML_CA"], cellStyle);
            SetCellValue(row.CreateCell(14), jsObjectRecord["M2_ML_CP"], cellStyle);
            SetCellValue(row.CreateCell(15), jsObjectRecord["M2_ML_CPK"], cellStyle);
            SetCellValue(row.CreateCell(16), jsObjectRecord["M2_MH_CA"], cellStyle);
            SetCellValue(row.CreateCell(17), jsObjectRecord["M2_MH_CP"], cellStyle);
            SetCellValue(row.CreateCell(18), jsObjectRecord["M2_MH_CPK"], cellStyle);
            SetCellValue(row.CreateCell(19), jsObjectRecord["M2_T30_CA"], cellStyle);
            SetCellValue(row.CreateCell(20), jsObjectRecord["M2_T30_CP"], cellStyle);
            SetCellValue(row.CreateCell(21), jsObjectRecord["M2_T30_CPK"], cellStyle);
            SetCellValue(row.CreateCell(22), jsObjectRecord["M2_T60_CA"], cellStyle);
            SetCellValue(row.CreateCell(23), jsObjectRecord["M2_T60_CP"], cellStyle);
            SetCellValue(row.CreateCell(24), jsObjectRecord["M2_T60_CPK"], cellStyle);
            SetCellValue(row.CreateCell(25), jsObjectRecord["M2_抽出_CA"], cellStyle);
            SetCellValue(row.CreateCell(26), jsObjectRecord["M2_抽出_CP"], cellStyle);
            SetCellValue(row.CreateCell(27), jsObjectRecord["M2_抽出_CPK"], cellStyle);

            SetCellValue(row.CreateCell(28), jsObjectRecord["M3_粘度_CA"], cellStyle);
            SetCellValue(row.CreateCell(29), jsObjectRecord["M3_粘度_CP"], cellStyle);
            SetCellValue(row.CreateCell(30), jsObjectRecord["M3_粘度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(31), jsObjectRecord["M3_焦烧_CA"], cellStyle);
            SetCellValue(row.CreateCell(32), jsObjectRecord["M3_焦烧_CP"], cellStyle);
            SetCellValue(row.CreateCell(33), jsObjectRecord["M3_焦烧_CPK"], cellStyle);
            SetCellValue(row.CreateCell(34), jsObjectRecord["M3_硬度_CA"], cellStyle);
            SetCellValue(row.CreateCell(35), jsObjectRecord["M3_硬度_CP"], cellStyle);
            SetCellValue(row.CreateCell(36), jsObjectRecord["M3_硬度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(37), jsObjectRecord["M3_比重_CA"], cellStyle);
            SetCellValue(row.CreateCell(38), jsObjectRecord["M3_比重_CP"], cellStyle);
            SetCellValue(row.CreateCell(39), jsObjectRecord["M3_比重_CPK"], cellStyle);
            SetCellValue(row.CreateCell(40), jsObjectRecord["M3_ML_CA"], cellStyle);
            SetCellValue(row.CreateCell(41), jsObjectRecord["M3_ML_CP"], cellStyle);
            SetCellValue(row.CreateCell(42), jsObjectRecord["M3_ML_CPK"], cellStyle);
            SetCellValue(row.CreateCell(43), jsObjectRecord["M3_MH_CA"], cellStyle);
            SetCellValue(row.CreateCell(44), jsObjectRecord["M3_MH_CP"], cellStyle);
            SetCellValue(row.CreateCell(45), jsObjectRecord["M3_MH_CPK"], cellStyle);
            SetCellValue(row.CreateCell(46), jsObjectRecord["M3_T30_CA"], cellStyle);
            SetCellValue(row.CreateCell(47), jsObjectRecord["M3_T30_CP"], cellStyle);
            SetCellValue(row.CreateCell(48), jsObjectRecord["M3_T30_CPK"], cellStyle);
            SetCellValue(row.CreateCell(49), jsObjectRecord["M3_T60_CA"], cellStyle);
            SetCellValue(row.CreateCell(50), jsObjectRecord["M3_T60_CP"], cellStyle);
            SetCellValue(row.CreateCell(51), jsObjectRecord["M3_T60_CPK"], cellStyle);
            SetCellValue(row.CreateCell(52), jsObjectRecord["M3_抽出_CA"], cellStyle);
            SetCellValue(row.CreateCell(53), jsObjectRecord["M3_抽出_CP"], cellStyle);
            SetCellValue(row.CreateCell(54), jsObjectRecord["M3_抽出_CPK"], cellStyle);

            SetCellValue(row.CreateCell(55), jsObjectRecord["M4_粘度_CA"], cellStyle);
            SetCellValue(row.CreateCell(56), jsObjectRecord["M4_粘度_CP"], cellStyle);
            SetCellValue(row.CreateCell(57), jsObjectRecord["M4_粘度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(58), jsObjectRecord["M4_焦烧_CA"], cellStyle);
            SetCellValue(row.CreateCell(59), jsObjectRecord["M4_焦烧_CP"], cellStyle);
            SetCellValue(row.CreateCell(60), jsObjectRecord["M4_焦烧_CPK"], cellStyle);
            SetCellValue(row.CreateCell(61), jsObjectRecord["M4_硬度_CA"], cellStyle);
            SetCellValue(row.CreateCell(62), jsObjectRecord["M4_硬度_CP"], cellStyle);
            SetCellValue(row.CreateCell(63), jsObjectRecord["M4_硬度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(64), jsObjectRecord["M4_比重_CA"], cellStyle);
            SetCellValue(row.CreateCell(65), jsObjectRecord["M4_比重_CP"], cellStyle);
            SetCellValue(row.CreateCell(66), jsObjectRecord["M4_比重_CPK"], cellStyle);
            SetCellValue(row.CreateCell(67), jsObjectRecord["M4_ML_CA"], cellStyle);
            SetCellValue(row.CreateCell(68), jsObjectRecord["M4_ML_CP"], cellStyle);
            SetCellValue(row.CreateCell(69), jsObjectRecord["M4_ML_CPK"], cellStyle);
            SetCellValue(row.CreateCell(70), jsObjectRecord["M4_MH_CA"], cellStyle);
            SetCellValue(row.CreateCell(71), jsObjectRecord["M4_MH_CP"], cellStyle);
            SetCellValue(row.CreateCell(72), jsObjectRecord["M4_MH_CPK"], cellStyle);
            SetCellValue(row.CreateCell(73), jsObjectRecord["M4_T30_CA"], cellStyle);
            SetCellValue(row.CreateCell(74), jsObjectRecord["M4_T30_CP"], cellStyle);
            SetCellValue(row.CreateCell(75), jsObjectRecord["M4_T30_CPK"], cellStyle);
            SetCellValue(row.CreateCell(76), jsObjectRecord["M4_T60_CA"], cellStyle);
            SetCellValue(row.CreateCell(77), jsObjectRecord["M4_T60_CP"], cellStyle);
            SetCellValue(row.CreateCell(78), jsObjectRecord["M4_T60_CPK"], cellStyle);
            SetCellValue(row.CreateCell(79), jsObjectRecord["M4_抽出_CA"], cellStyle);
            SetCellValue(row.CreateCell(80), jsObjectRecord["M4_抽出_CP"], cellStyle);
            SetCellValue(row.CreateCell(81), jsObjectRecord["M4_抽出_CPK"], cellStyle);

            SetCellValue(row.CreateCell(82), jsObjectRecord["M5_粘度_CA"], cellStyle);
            SetCellValue(row.CreateCell(83), jsObjectRecord["M5_粘度_CP"], cellStyle);
            SetCellValue(row.CreateCell(84), jsObjectRecord["M5_粘度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(85), jsObjectRecord["M5_焦烧_CA"], cellStyle);
            SetCellValue(row.CreateCell(86), jsObjectRecord["M5_焦烧_CP"], cellStyle);
            SetCellValue(row.CreateCell(87), jsObjectRecord["M5_焦烧_CPK"], cellStyle);
            SetCellValue(row.CreateCell(88), jsObjectRecord["M5_硬度_CA"], cellStyle);
            SetCellValue(row.CreateCell(89), jsObjectRecord["M5_硬度_CP"], cellStyle);
            SetCellValue(row.CreateCell(90), jsObjectRecord["M5_硬度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(91), jsObjectRecord["M5_比重_CA"], cellStyle);
            SetCellValue(row.CreateCell(92), jsObjectRecord["M5_比重_CP"], cellStyle);
            SetCellValue(row.CreateCell(93), jsObjectRecord["M5_比重_CPK"], cellStyle);
            SetCellValue(row.CreateCell(94), jsObjectRecord["M5_ML_CA"], cellStyle);
            SetCellValue(row.CreateCell(95), jsObjectRecord["M5_ML_CP"], cellStyle);
            SetCellValue(row.CreateCell(96), jsObjectRecord["M5_ML_CPK"], cellStyle);
            SetCellValue(row.CreateCell(97), jsObjectRecord["M5_MH_CA"], cellStyle);
            SetCellValue(row.CreateCell(98), jsObjectRecord["M5_MH_CP"], cellStyle);
            SetCellValue(row.CreateCell(99), jsObjectRecord["M5_MH_CPK"], cellStyle);
            SetCellValue(row.CreateCell(100), jsObjectRecord["M5_T30_CA"], cellStyle);
            SetCellValue(row.CreateCell(101), jsObjectRecord["M5_T30_CP"], cellStyle);
            SetCellValue(row.CreateCell(102), jsObjectRecord["M5_T30_CPK"], cellStyle);
            SetCellValue(row.CreateCell(103), jsObjectRecord["M5_T60_CA"], cellStyle);
            SetCellValue(row.CreateCell(104), jsObjectRecord["M5_T60_CP"], cellStyle);
            SetCellValue(row.CreateCell(105), jsObjectRecord["M5_T60_CPK"], cellStyle);
            SetCellValue(row.CreateCell(106), jsObjectRecord["M5_抽出_CA"], cellStyle);
            SetCellValue(row.CreateCell(107), jsObjectRecord["M5_抽出_CP"], cellStyle);
            SetCellValue(row.CreateCell(108), jsObjectRecord["M5_抽出_CPK"], cellStyle);

            SetCellValue(row.CreateCell(109), jsObjectRecord["综合_粘度_CA"], cellStyle);
            SetCellValue(row.CreateCell(110), jsObjectRecord["综合_粘度_CP"], cellStyle);
            SetCellValue(row.CreateCell(111), jsObjectRecord["综合_粘度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(112), jsObjectRecord["综合_焦烧_CA"], cellStyle);
            SetCellValue(row.CreateCell(113), jsObjectRecord["综合_焦烧_CP"], cellStyle);
            SetCellValue(row.CreateCell(114), jsObjectRecord["综合_焦烧_CPK"], cellStyle);
            SetCellValue(row.CreateCell(115), jsObjectRecord["综合_硬度_CA"], cellStyle);
            SetCellValue(row.CreateCell(116), jsObjectRecord["综合_硬度_CP"], cellStyle);
            SetCellValue(row.CreateCell(117), jsObjectRecord["综合_硬度_CPK"], cellStyle);
            SetCellValue(row.CreateCell(118), jsObjectRecord["综合_比重_CA"], cellStyle);
            SetCellValue(row.CreateCell(119), jsObjectRecord["综合_比重_CP"], cellStyle);
            SetCellValue(row.CreateCell(120), jsObjectRecord["综合_比重_CPK"], cellStyle);
            SetCellValue(row.CreateCell(121), jsObjectRecord["综合_ML_CA"], cellStyle);
            SetCellValue(row.CreateCell(122), jsObjectRecord["综合_ML_CP"], cellStyle);
            SetCellValue(row.CreateCell(123), jsObjectRecord["综合_ML_CPK"], cellStyle);
            SetCellValue(row.CreateCell(124), jsObjectRecord["综合_MH_CA"], cellStyle);
            SetCellValue(row.CreateCell(125), jsObjectRecord["综合_MH_CP"], cellStyle);
            SetCellValue(row.CreateCell(126), jsObjectRecord["综合_MH_CPK"], cellStyle);
            SetCellValue(row.CreateCell(127), jsObjectRecord["综合_T30_CA"], cellStyle);
            SetCellValue(row.CreateCell(128), jsObjectRecord["综合_T30_CP"], cellStyle);
            SetCellValue(row.CreateCell(129), jsObjectRecord["综合_T30_CPK"], cellStyle);
            SetCellValue(row.CreateCell(130), jsObjectRecord["综合_T60_CA"], cellStyle);
            SetCellValue(row.CreateCell(131), jsObjectRecord["综合_T60_CP"], cellStyle);
            SetCellValue(row.CreateCell(132), jsObjectRecord["综合_T60_CPK"], cellStyle);
            SetCellValue(row.CreateCell(133), jsObjectRecord["综合_抽出_CA"], cellStyle);
            SetCellValue(row.CreateCell(134), jsObjectRecord["综合_抽出_CP"], cellStyle);
            SetCellValue(row.CreateCell(135), jsObjectRecord["综合_抽出_CPK"], cellStyle);

        }

        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);

        new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, "胶料质检车间CPK日报表");

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