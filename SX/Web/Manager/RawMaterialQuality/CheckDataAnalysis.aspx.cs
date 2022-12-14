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
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;

public partial class Manager_RawMaterialQuality_CheckDataAnalysis : BasePage
{
    #region 属性注入
    protected IQmcSampleLedgerManager sampleLedgerManager = new QmcSampleLedgerManager();
    protected IQmcSpecMappingManager specMappingManager = new QmcSpecMappingManager();
    protected IQmcSpecManager specManager = new QmcSpecManager();
    protected IQmcLedgerManager ledgerManager = new QmcLedgerManager();
    protected IQmcCheckDataManager checkDataManager = new QmcCheckDataManager();
    protected IQmcCheckDataDetailManager checkDataDetailManager = new QmcCheckDataDetailManager();
    protected IQmcCheckItemManager itemManager = new QmcCheckItemManager();
    protected IQmcCheckItemDetailManager itemDetailManager = new QmcCheckItemDetailManager();
    protected IQmcStandardManager standardManager = new QmcStandardManager();
    protected IPstMaterialChkManager materialChkManager = new PstMaterialChkManager();
    protected IPstMaterialChkDetailManager materialChkDetailManager = new PstMaterialChkDetailManager();
    protected IBasFactoryInfoManager factoryManager = new BasFactoryInfoManager();
    protected IBasUserManager userManager = new BasUserManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    static bool inhibitor = false;
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExport" };
            导出SPC报表 = new SysPageAction() { ActionID = 3, ActionName = "btnExportSPC" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 导出SPC报表 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            if (inhibitor == false)
            {
                //初始化查询起止日期
                this.txtBeginTime.Value = DateTime.Now.Date;
                this.txtEndTime.Value = DateTime.Now.Date.AddDays(1);
                inhibitor = true;
            }
            //初始化原材料系列下拉菜单
            InitSeries();
            //初始化执行标准
            InitStandard();
        }
    }
    #endregion

    #region 页面方法
    /// <summary>
    /// 初始化原材料下拉菜单
    /// </summary>
    protected void InitSeries()
    {
        EntityArrayList<BasMaterialMinorType> lst = new EntityArrayList<BasMaterialMinorType>();
        lst = minorTypeManager.GetListByWhere(BasMaterialMinorType._.MajorID == 1 && BasMaterialMinorType._.DeleteFlag == "0");
        foreach (BasMaterialMinorType type in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = type.MinorTypeName;
            item.Value = type.MinorTypeID;
            cbxSeries.Items.Add(item);
        }
    }
    /// <summary>
    /// 初始化执行标准
    /// </summary>
    private void InitStandard()
    {
        EntityArrayList<QmcStandard> standardList = standardManager.GetListByWhereAndOrder((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag != "0"), QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> activeList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "1")), QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> readyList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "0")), QmcStandard._.ActivateFlag.Asc);
        EntityArrayList<QmcStandard> historyList = standardManager.GetListByWhereAndOrder(((QmcStandard._.DeleteFlag == "0") && (QmcStandard._.ActivateFlag == "2")), QmcStandard._.ActivateFlag.Asc);
        if (standardList.Count == 0)
        {
            btnExport.Disable();
            btnSearch.Disable();
            btnExportSPC.Disable();
            msg.Alert("提示", "没有可统计的执行标准！");
            msg.Show();
        }
        else
        {
            foreach (QmcStandard standard in standardList)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = standard.StandardName;
                item.Value = standard.StandardId.ToString();
                if (standard.ActivateFlag == "2")
                {
                    item.Text = item.Text + "(过期)";
                }
                else if (standard.ActivateFlag == "0")
                {
                    item.Text = item.Text + "(未生效)";
                }
                else if (standard.ActivateFlag == "1")
                {
                    item.Text = item.Text + "(当前)";
                    cbxStandard.Text = item.Text;
                    cbxStandard.Value = item.Value;
                }
                cbxStandard.Items.Add(item);
            }
        }
    }

    /// <summary>
    /// 加载合格率统计报告
    /// </summary>
    [DirectMethod]
    protected void LoadReport()
    {
        if (Convert.ToDateTime(txtBeginTime.Text) >= Convert.ToDateTime(txtEndTime.Text))
        {
            msg.Alert("操作", "结束时间必须晚于起始时间！");
            msg.Show();
            return;
        }
        QmcLedgerManager.QueryParams param = new QmcLedgerManager.QueryParams();
        param.seriesId = (string)cbxSeries.Value;
        param.standardId = (string)cbxStandard.Value;
        param.specId = (string)cbxSpec.Value;
        param.supplierId = (string)txtSupplierId.Value;
        param.manufacturerId = (string)txtManufacturerId.Value;
        param.beginDate = Convert.ToDateTime(txtBeginTime.Value).Date;
        param.endDate = Convert.ToDateTime(txtEndTime.Value).Date;
        param.materialCode = (string)cbxMaterial.Value;
        DataTable dt = ledgerManager.GetReport(param);

        pnlAnalysis.GetStore().DataSource = dt;
        pnlAnalysis.GetStore().DataBind();
    }
    #endregion

    #region 增删改按钮激发的方法
    /// <summary>
    /// 查询按钮点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadReport();
    }

    /// <summary>
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        QmcLedgerManager.QueryParams param = new QmcLedgerManager.QueryParams();
        param.seriesId = (string)cbxSeries.Value;
        param.standardId = (string)cbxStandard.Value;
        param.specId = (string)cbxSpec.Value;
        param.supplierId = (string)txtSupplierId.Value;
        param.manufacturerId = (string)txtManufacturerId.Value;
        param.beginDate = Convert.ToDateTime(txtBeginTime.Value);
        param.endDate = Convert.ToDateTime(txtEndTime.Value);
        param.materialCode = (string)cbxMaterial.Value;
        DataTable dt = ledgerManager.GetReport(param);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = dt.Columns[i];
            foreach (ColumnBase cb in this.pnlAnalysis.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()) && (cb.Visible == true))
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
        if (dt.Rows.Count > 0)
        {
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "合格率统计报表");
        }
        else
        {
            msg.Alert("操作", "没有可以导出的内容！");
            msg.Show();
        }
    }

    /// <summary>
    /// 点击导出SPC报表按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSPCSubmit_Click(object sender, EventArgs e)
    {
        if (((string)cbxSeries.Value == "") || ((string)cbxMaterial.Value == ""))
        {
            msg.Alert("操作", "请选择原材料系列和型号！");
            msg.Show();
            return;
        }

        #region 加载报告模板
        string xlsPath = "CheckDataSPCReport.xls";
        HSSFWorkbook workbook = new HSSFWorkbook();
        using (FileStream fs = new FileStream(Server.MapPath(xlsPath), FileMode.Open, FileAccess.Read))
        {
            try
            {
                workbook = new HSSFWorkbook(fs);
            }
            catch
            {
                X.Msg.Alert("提示", "报告模板不是有效的Excel文件").Show();
                return;
            }
        }
        #endregion

        #region 获取检测项目
        EntityArrayList<QmcCheckItemDetail> sheetList = new EntityArrayList<QmcCheckItemDetail>();
        EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.SeriesId == (string)cbxSeries.Value)
            && ((QmcCheckItem._.StandardId == (string)cbxStandard.Value))
            && ((QmcCheckItem._.DeleteFlag == "0"))
            && ((QmcCheckItem._.ValueType == "数字")));
        EntityArrayList<QmcCheckItemDetail> itemDetailList = itemDetailManager.GetListByWhere((QmcCheckItemDetail._.MaterialCode == (string)cbxMaterial.Value) 
            && (QmcCheckItemDetail._.DeleteFlag == "0")
            && (QmcCheckItemDetail._.LatestFlag == "1"));
        if (itemList.Count == 0)
        {
            msg.Alert("警告", "该原材料系列没有检测项目！");
            msg.Show();
            return;
        }
        else if (itemDetailList.Count == 0)
        {
            msg.Alert("警告", "该原材料型号没有检测指标！");
            msg.Show();
            return;
        }
        else
        {
            foreach (QmcCheckItem item in itemList)
            {
                foreach (QmcCheckItemDetail detail in itemDetailList)
                {
                    if (item.ItemId == detail.ItemId)
                    {
                        sheetList.Add(detail);
                    }
                }
            }
        }
        if (sheetList.Count == 0)
        {
            msg.Alert("警告", "该原材料型号没有检测指标！");
            msg.Show();
            return;
        }
        #endregion

        int counter = 1;
        foreach (QmcCheckItemDetail detail in sheetList)
        {
            #region 获取数据
            QmcCheckDataDetailManager.QueryParams param = new QmcCheckDataDetailManager.QueryParams();
            param.seriesId = (string)cbxSeries.Value;
            param.standardId = (string)cbxStandard.Value;
            param.specId = (string)cbxSpec.Value;
            param.supplierId = (string)txtSupplierId.Value;
            param.manufacturerId = (string)txtManufacturerId.Value;
            param.beginDate = Convert.ToDateTime(txtBeginTime.Value);
            param.endDate = Convert.ToDateTime(txtEndTime.Value);
            param.materialCode = (string)cbxMaterial.Value;
            param.itemId = detail.ItemId.ToString();

            DataTable dt = checkDataDetailManager.GetSPCReport(param);
            #endregion

            #region 写入数据
            HSSFSheet modelSheet = (HSSFSheet)workbook.GetSheetAt(0);
            ISheet sheet = workbook.CloneSheet(0);
            QmcCheckItem item = itemManager.GetById(detail.ItemId);
            workbook.SetSheetName(counter, item.ItemName.Replace("/", "∕").Replace("*", "x"));
            //写入标准
            string goodOperator = detail.GoodOperator;
            switch (goodOperator)
            {
                case "－":
                    sheet.GetRow(1).GetCell(2).SetCellValue(detail.GoodMinValue.ToString());
                    sheet.GetRow(2).GetCell(2).SetCellValue(((detail.GoodMinValue + detail.GoodMaxValue) / 2).ToString());
                    sheet.GetRow(3).GetCell(2).SetCellValue(detail.GoodMaxValue.ToString());
                    break;
                case ">":
                    sheet.GetRow(18).GetCell(2).SetCellValue(detail.GoodMaxValue.ToString());
                    break;
                case "<":
                    sheet.GetRow(11).GetCell(2).SetCellValue(detail.GoodMaxValue.ToString());
                    break;
                default:
                    break;
            }
            if (dt.Rows.Count > 0)
            {
                int indexPointer = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //写入数据
                    switch (goodOperator)
                    {
                        case "－":
                            sheet.GetRow(1 + indexPointer).GetCell(8).SetCellValue(Convert.ToDouble(dr["CheckValue"]));
                            break;
                        case ">":
                            sheet.GetRow(1 + indexPointer).GetCell(10).SetCellValue(Convert.ToDouble(dr["CheckValue"]));
                            break;
                        case "<":
                            sheet.GetRow(1 + indexPointer).GetCell(9).SetCellValue(Convert.ToDouble(dr["CheckValue"]));
                            break;
                        default:
                            break;
                    }
                    indexPointer++;
                }
            }
            #endregion
            counter++;
        }

        #region 生成报告下载
        workbook.RemoveSheetAt(0);
        workbook.ForceFormulaRecalculation = true;
        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);
        string fileName = "SPC统计报表_" + cbxMaterial.SelectedItem.Text;
        new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, fileName);
        #endregion
    }

    /// <summary>
    /// 更换原材料分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxSeries_Change(object sender, DirectEventArgs e)
    {
        cbxMaterial.GetStore().RemoveAll();
        cbxSpec.GetStore().RemoveAll();

        cbxMaterial.SetValue("");
        cbxSpec.SetValue("");

        string minorTypeId = cbxSeries.Value.ToString();
        if (minorTypeId == "")
        {
            return;
        }
        string standardId = cbxStandard.Value.ToString();
        if (standardId == "")
        {
            return;
        }
        EntityArrayList<BasMaterial> mBasMaterialList = materialManager.GetListByWhereAndOrder(
            BasMaterial._.DeleteFlag == "0"
            & BasMaterial._.MajorTypeID == 1
            & BasMaterial._.MinorTypeID == minorTypeId
            , BasMaterial._.ObjID.Asc);
        foreach (BasMaterial mBasMaterial in mBasMaterialList)
        {
            cbxMaterial.AddItem(mBasMaterial.MaterialName, mBasMaterial.MaterialCode);
        }
        LoadReport();
    }

    /// <summary>
    /// 更换原材料型号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxMaterial_Change(object sender, DirectEventArgs e)
    {
        cbxSpec.GetStore().RemoveAll();
        cbxSpec.SetValue("");

        string materialCode = cbxMaterial.Value.ToString();
        if (materialCode == "")
        {
            return;
        }
        EntityArrayList<QmcSpecMapping> specMappingList = specMappingManager.GetListByWhere((QmcSpecMapping._.MaterialCode == materialCode) && (QmcSpecMapping._.DeleteFlag == "0"));
        foreach (QmcSpecMapping mapping in specMappingList)
        {
            cbxSpec.AddItem(mapping.SpecName, mapping.SpecId);
        }
        LoadReport();
    }

    /// <summary>
    /// 更换规格
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxSpec_Change(object sender, DirectEventArgs e)
    {
        string specId = cbxSpec.Value.ToString();
        if (specId == "")
        {
            return;
        }
        LoadReport();
    }
    #endregion
}