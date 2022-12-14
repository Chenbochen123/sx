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
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI.HPSF;
using System.Globalization;

public partial class Manager_RawMaterialQuality_CheckItemDetailImport : BasePage
{
    #region 属性注入
    protected IQmcStandardManager standardManager = new QmcStandardManager();
    protected IQmcCheckItemManager itemManager = new QmcCheckItemManager();
    protected IQmcCheckItemDetailManager detailManager = new QmcCheckItemDetailManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            下载模板 = new SysPageAction() { ActionID = 1, ActionName = "btnDownload" };
            导入 = new SysPageAction() { ActionID = 2, ActionName = "btnImport" };
            保存 = new SysPageAction() { ActionID = 3, ActionName = "btnSave" };
        }
        public SysPageAction 下载模板 { get; private set; } //必须为 public
        public SysPageAction 导入 { get; private set; } //必须为 public
        public SysPageAction 保存 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitSeries();//初始化原材料下拉菜单
            InitStandard();//初始化执行标准下拉菜单
            this.btnDownload.Disable(true);
        }
    }

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
            cbxSeriesName.Items.Add(item);
        }
    }

    /// <summary>
    /// 初始化执行标准
    /// </summary>
    private void InitStandard()
    {
        EntityArrayList<QmcStandard> readyList = standardManager.GetListByWhere(QmcStandard._.ActivateFlag == "0" && QmcStandard._.DeleteFlag == "0");
        if (readyList.Count == 0)
        {
            btnDownload.Disable();
            btnImport.Disable();
            btnSave.Disable();
            cbxSeriesName.Disable();
            cbxStandard.Disable();
            fulExcel.Disable();
            btnClear.Disable();
            cbxIsBlankTemplate.Disable();
            cbxDoCover.Disable();
            msg.Alert("提示", "当前没有未启用的执行标准，请新建执行标准！");
            msg.Show();
        }
        else
        {
            int count = readyList.Count;
            Ext.Net.ListItem selectedItem = new Ext.Net.ListItem();
            selectedItem.Text = readyList[0].StandardName;
            selectedItem.Value = readyList[0].StandardId.ToString();
            cbxStandard.Items.Add(selectedItem);
            cbxStandard.Text = selectedItem.Text;
            cbxStandard.Value = selectedItem.Value;
            if (count > 1)
            {
                for (int i = 1; i < count; i++)
                {
                    Ext.Net.ListItem item = new Ext.Net.ListItem();
                    item.Text = readyList[i].StandardName;
                    item.Value = readyList[i].StandardId.ToString();
                    cbxStandard.Items.Add(item);
                }
            }
        }
    }
    #endregion

    #region 页面方法

    /// <summary>
    /// 选择Excel文件
    /// </summary>
    /// <returns></returns>
    [DirectMethod]
    public bool SelectExcel()
    {
        if ((fulExcel.HasFile == false) || (fulExcel.PostedFile == null) || (fulExcel.PostedFile.ContentLength == 0))
        {
            return false;
        }
        using (MemoryStream ms = new MemoryStream(this.fulExcel.FileBytes))
        {
            IWorkbook workbook = null;
            try
            {
                workbook = new HSSFWorkbook(ms);
            }
            catch
            {
                X.Msg.Alert("提示", "上传的文件不是有效的Excel文件").Show();
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 上传Excel文件
    /// </summary>
    /// <returns></returns>
    [DirectMethod]
    public bool UploadExcel()
    {
        if ((fulExcel.HasFile == false) || (fulExcel.PostedFile == null) || (fulExcel.PostedFile.ContentLength == 0))
        {
            X.Msg.Alert("提示", "没有选择上传文件或文件不存在".ToString()).Show();
            return false;
        }
        if (cbxSeriesName.SelectedItem.Text == null)
        {
            X.Msg.Alert("提示", "请选择原材料系列！".ToString()).Show();
            return false;
        }
        if (!fulExcel.PostedFile.FileName.Contains(cbxSeriesName.SelectedItem.Text))
        {
            X.Msg.Alert("提示", "模板类型与所选原材料系列不一致！".ToString()).Show();
            return false;
        }
        if (!fulExcel.PostedFile.FileName.Contains(cbxStandard.SelectedItem.Text))
        {
            X.Msg.Alert("提示", "模板对应的执行标准与所选执行标准不一致！".ToString()).Show();
            return false;
        }
        // Excel文件读取
        using (MemoryStream ms = new MemoryStream(this.fulExcel.FileBytes))
        {
            IWorkbook workbook = null;
            try
            {
                workbook = new HSSFWorkbook(ms);
            }
            catch
            {
                X.Msg.Alert("提示", "上传的文件不是有效的Excel文件").Show();
                return false;
            }

            DataTable dtAllData = CreateDataTable();
            DataTable dtValidData = CreateDataTable();
            DataTable dtCorruptedData = CreateDataTable();

            SetAllDataTable(dtAllData, dtValidData, dtCorruptedData, workbook);

            dtAllData.DefaultView.Sort = "rowNum";
            dtValidData.DefaultView.Sort = "rowNum";
            dtCorruptedData.DefaultView.Sort = "rowNum";

            StoreCenterAll.DataSource = dtAllData;
            StoreCenterAll.DataBind();

            StoreCenterValid.DataSource = dtValidData;
            StoreCenterValid.DataBind();

            StoreCenterCorrupted.DataSource = dtCorruptedData;
            StoreCenterCorrupted.DataBind();

            string tpl = "文件上传完毕: {0}<br/>文件大小: {1} 字节<br/>Excel文件数据：{2} 条";
            X.Msg.Alert("成功", string.Format(tpl, this.fulExcel.PostedFile.FileName, this.fulExcel.PostedFile.ContentLength, dtAllData.Rows.Count.ToString())).Show();
        }
        return true;
    }

    /// <summary>
    /// 创建Excel表格
    /// </summary>
    /// <returns></returns>
    private DataTable CreateDataTable()
    {
        DataTable dtWorkbook = new DataTable();
        dtWorkbook.Columns.Add(new DataColumn("rowNum", typeof(int)));
        dtWorkbook.Columns.Add(new DataColumn("seriesId"));
        dtWorkbook.Columns.Add(new DataColumn("seriesName"));
        dtWorkbook.Columns.Add(new DataColumn("materialCode"));
        dtWorkbook.Columns.Add(new DataColumn("materialName"));
        dtWorkbook.Columns.Add(new DataColumn("materialERPCode"));
        dtWorkbook.Columns.Add(new DataColumn("detailName"));
        dtWorkbook.Columns.Add(new DataColumn("detailType"));
        dtWorkbook.Columns.Add(new DataColumn("frequency"));
        dtWorkbook.Columns.Add(new DataColumn("primeOperator"));
        dtWorkbook.Columns.Add(new DataColumn("primeMinValue"));
        dtWorkbook.Columns.Add(new DataColumn("primeMaxValue"));
        dtWorkbook.Columns.Add(new DataColumn("primeTextValue"));
        dtWorkbook.Columns.Add(new DataColumn("primeDisplayValue"));
        dtWorkbook.Columns.Add(new DataColumn("primeIncludeMinBorder"));
        dtWorkbook.Columns.Add(new DataColumn("primeIncludeMaxBorder"));
        dtWorkbook.Columns.Add(new DataColumn("goodOperator"));
        dtWorkbook.Columns.Add(new DataColumn("goodMinValue"));
        dtWorkbook.Columns.Add(new DataColumn("goodMaxValue"));
        dtWorkbook.Columns.Add(new DataColumn("goodTextValue"));
        dtWorkbook.Columns.Add(new DataColumn("goodDisplayValue"));
        dtWorkbook.Columns.Add(new DataColumn("goodIncludeMinBorder"));
        dtWorkbook.Columns.Add(new DataColumn("goodIncludeMaxBorder"));
        dtWorkbook.Columns.Add(new DataColumn("checkMethod"));
        dtWorkbook.Columns.Add(new DataColumn("remark"));
        dtWorkbook.Columns.Add(new DataColumn("validFlag"));
        dtWorkbook.Columns.Add(new DataColumn("errMsg"));

        return dtWorkbook;
    }

    /// <summary>
    /// 填充所有导入数据表
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="sheet"></param>
    private void SetAllDataTable(DataTable dtAllData, DataTable dtValidData, DataTable dtCorruptedData, IWorkbook workbook)
    {
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        int tableRowNum = 1;
        //获取所有的sheet页
        List<ISheet> sheetList = new List<ISheet>();
        for (int i = 0; i < workbook.NumberOfSheets; i++)
        {
            ISheet sheet = workbook.GetSheetAt(i);
            sheetList.Add(sheet);
        }
        //遍历sheet页
        foreach (ISheet sheet in sheetList)
        {
            string masterErrMsg = String.Empty;
            string masterValidFlag = "1";
            int firstRowNum = 1;
            int lastRowNum = sheet.LastRowNum;
            if (lastRowNum == 0)
            {
                continue;//sheet页无内容则跳过
            }
            string materialCode = sheet.SheetName.Substring(sheet.SheetName.Length - 13);
            //获取所属物料型号
            BasMaterial material = new BasMaterial();
            if (materialManager.GetListByWhere((BasMaterial._.MaterialCode == materialCode) && (BasMaterial._.DeleteFlag == "0")).Count > 0)
            {
                material = materialManager.GetListByWhere((BasMaterial._.MaterialCode == materialCode) && (BasMaterial._.DeleteFlag == "0"))[0];
            }
            else
            {
                masterErrMsg = "物料代码不正确";
                masterValidFlag = "0";
            }
            //遍历行
            for (int num = firstRowNum; num <= lastRowNum; num++)
            {
                string errMsg = masterErrMsg;
                string validFlag = masterValidFlag;
                IRow row = sheet.GetRow(num);
                string rowNum = tableRowNum.ToString();
                string seriesId = cbxSeriesName.Value.ToString();
                string seriesName = cbxSeriesName.SelectedItem.Text;
                string materialName = material.MaterialName;
                string materialERPCode = material.ERPCode;
                string detailName = String.Empty;
                string detailType = String.Empty;
                string frequency = String.Empty;
                string primeOperator = String.Empty;
                string primeMinValue = String.Empty;
                string primeMaxValue = String.Empty;
                string primeTextValue = String.Empty;
                string primeDisplayValue = String.Empty;
                string goodOperator = String.Empty;
                string goodMinValue = String.Empty;
                string goodMaxValue = String.Empty;
                string goodTextValue = String.Empty;
                string goodDisplayValue = String.Empty;
                string checkMethod = String.Empty;
                string remark = String.Empty;
                string primeIncludeMinBorder = String.Empty;
                string primeIncludeMaxBorder = String.Empty;
                string goodIncludeMinBorder = String.Empty;
                string goodIncludeMaxBorder = String.Empty;
                bool integrationFlag = true;//检测指标完整性标记
                bool matchFlag = true;//检测指标一致性标记
                bool coverFlag = true;//覆盖原有指标标记
                bool legalFlag = true;//数据合法性标记

                #region 检查检测指标模板的正确性

                //检测项目&指标不为空
                if (row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    detailName = row.GetCell(1).ToString();
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "检测项目为空";
                    }
                    else
                    {
                        errMsg += "/检测项目为空";
                    }
                }
                if (row.GetCell(2, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    detailType = row.GetCell(2).ToString();
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "检测值类型为空";
                    }
                    else
                    {
                        errMsg += "/检测值类型为空";
                    }
                }
                if (row.GetCell(3, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    frequency = row.GetCell(3).ToString();
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "检测频次为空";
                    }
                    else
                    {
                        errMsg += "/检测频次为空";
                    }
                }
                if (integrationFlag)
                {
                    //检测项目&检测值类型与数据库一致
                    if (itemManager.GetListByWhere((QmcCheckItem._.ItemName == detailName) && (QmcCheckItem._.ValueType == detailType) && (QmcCheckItem._.SeriesId == seriesId) && (QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0")).Count == 0)
                    {
                        matchFlag = false;
                        validFlag = "0";
                        if (errMsg == String.Empty)
                        {
                            errMsg = "检测项目或检测值类型与数据库不一致";
                        }
                        else
                        {
                            errMsg += "/检测项目或检测值类型与数据库不一致";
                        }
                    }
                }
                if (integrationFlag && matchFlag)
                {
                    //会覆盖已有的指标
                    bool particalCoverFlag = true;
                    EntityArrayList<QmcCheckItemDetail> detailList = detailManager.GetListByWhere((QmcCheckItemDetail._.MaterialCode == material.MaterialCode) && (QmcCheckItemDetail._.DeleteFlag == "0") && (QmcCheckItemDetail._.LatestFlag == "1"));
                    foreach (QmcCheckItemDetail detail in detailList)
                    {
                        EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere(QmcCheckItem._.SeriesId == seriesId);
                        foreach (QmcCheckItem item in itemList)
                        {
                            if (detail.ItemId == item.ItemId)
                            {
                                particalCoverFlag = false;
                                break;
                            }
                        }
                    }
                    if (particalCoverFlag == false)
                    {
                        if (!cbxDoCover.Checked)
                        {
                            coverFlag = false;
                            validFlag = "0";
                        }
                        if (errMsg == String.Empty)
                        {
                            errMsg = "会覆盖原有的检测指标";
                        }
                        else
                        {
                            errMsg += "/会覆盖原有的检测指标";
                        }
                    }
                    //输入数据的合法性
                    if (detailType == "文字")
                    {
                        if (row.GetCell(15, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null)
                        {
                            legalFlag = false;
                            validFlag = "0";
                            if (errMsg == String.Empty)
                            {
                                errMsg = "文字指标为空";
                            }
                            else
                            {
                                errMsg += "/文字指标为空";
                            }
                        }
                        else
                        {
                            if (row.GetCell(15, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString().Length > 50)
                            {
                                legalFlag = false;
                                validFlag = "0";
                                if (errMsg == String.Empty)
                                {
                                    errMsg = "合格品文字指标过长";
                                }
                                else
                                {
                                    errMsg += "/合格品文字指标过长";
                                }
                            }
                            if (row.GetCell(14, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                            {
                                if (row.GetCell(14, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString().Length > 50)
                                {
                                    legalFlag = false;
                                    validFlag = "0";
                                    if (errMsg == String.Empty)
                                    {
                                        errMsg = "一级品文字指标过长";
                                    }
                                    else
                                    {
                                        errMsg += "/一级品文字指标过长";
                                    }
                                }
                                primeTextValue = row.GetCell(14).ToString();
                            }
                            goodTextValue = row.GetCell(15).ToString();
                            goodDisplayValue = row.GetCell(15).ToString();
                        }
                    }
                    //合格品数字标准
                    else if (detailType == "数字")
                    {
                        //包含边界判断
                        if (row.GetCell(5, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                        {
                            if (row.GetCell(5, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                            {
                                primeIncludeMinBorder = "1";
                            }
                            else
                            {
                                primeIncludeMinBorder = "0";
                            }
                        }
                        else
                        {
                            primeIncludeMinBorder = "0";
                        }
                        if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                        {
                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                            {
                                primeIncludeMaxBorder = "1";
                            }
                            else
                            {
                                primeIncludeMaxBorder = "0";
                            }
                        }
                        else
                        {
                            primeIncludeMaxBorder = "0";
                        }
                        if (row.GetCell(10, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                        {
                            if (row.GetCell(10, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                            {
                                goodIncludeMinBorder = "1";
                            }
                            else
                            {
                                goodIncludeMinBorder = "0";
                            }
                        }
                        else
                        {
                            goodIncludeMinBorder = "0";
                        }
                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                        {
                            if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                            {
                                goodIncludeMaxBorder = "1";
                            }
                            else
                            {
                                goodIncludeMaxBorder = "0";
                            }
                        }
                        else
                        {
                            goodIncludeMaxBorder = "0";
                        }
                        //逻辑判断
                        if (row.GetCell(11, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null)
                        {
                            legalFlag = false;
                            validFlag = "0";
                            if (errMsg == String.Empty)
                            {
                                errMsg = "合格品运算符为空";
                            }
                            else
                            {
                                errMsg += "/合格品运算符为空";
                            }
                        }
                        else if (row.GetCell(11, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "")
                        {
                            legalFlag = false;
                            validFlag = "0";
                            if (errMsg == String.Empty)
                            {
                                errMsg = "合格品运算符为空";
                            }
                            else
                            {
                                errMsg += "/合格品运算符为空";
                            }
                        }
                        else
                        {
                            goodOperator = row.GetCell(11).ToString();
                            if (row.GetCell(11).ToString() == "－")
                            {
                                if ((row.GetCell(9, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null) || (row.GetCell(12, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null))
                                {
                                    legalFlag = false;
                                    validFlag = "0";
                                    if (errMsg == String.Empty)
                                    {
                                        errMsg = "合格品数字标准不完整";
                                    }
                                    else
                                    {
                                        errMsg += "/合格品数字标准不完整";
                                    }
                                }
                                else
                                {
                                    goodMinValue = row.GetCell(9).ToString();
                                    goodMaxValue = row.GetCell(12).ToString();
                                    if (goodMinValue.ToCharArray()[0] == '.')
                                    {
                                        goodMinValue = "0" + goodMinValue;
                                    }
                                    if (goodMaxValue.ToCharArray()[0] == '.')
                                    {
                                        goodMaxValue = "0" + goodMaxValue;
                                    }
                                }
                                if (legalFlag && (!Regex.Match(goodMinValue, @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success) || ((!Regex.Match(goodMaxValue, @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)))
                                {
                                    legalFlag = false;
                                    validFlag = "0";
                                    if (errMsg == String.Empty)
                                    {
                                        errMsg = "合格品数字格式不正确";
                                    }
                                    else
                                    {
                                        errMsg += "/合格品数字格式不正确";
                                    }
                                }
                                else if (Convert.ToDecimal(goodMinValue) > Convert.ToDecimal(goodMaxValue))
                                {
                                    legalFlag = false;
                                    validFlag = "0";
                                    if (errMsg == String.Empty)
                                    {
                                        errMsg = "合格品起始值大于结束值";
                                    }
                                    else
                                    {
                                        errMsg += "/合格品起始值大于结束值";
                                    }
                                }
                                else if (Convert.ToDecimal(goodMinValue) == Convert.ToDecimal(goodMaxValue))
                                {
                                    if ((row.GetCell(10, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null) && (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null))
                                    {
                                        if ((row.GetCell(10, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是") || (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是"))
                                        {
                                            legalFlag = false;
                                            validFlag = "0";
                                            if (errMsg == String.Empty)
                                            {
                                                errMsg = "合格品起始值等于结束值但不包括边界";
                                            }
                                            else
                                            {
                                                errMsg += "/合格品起始值等于结束值但不包括边界";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        legalFlag = false;
                                        validFlag = "0";
                                        if (errMsg == String.Empty)
                                        {
                                            errMsg = "合格品起始值等于结束值但不包括边界";
                                        }
                                        else
                                        {
                                            errMsg += "/合格品起始值等于结束值但不包括边界";
                                        }
                                    }
                                }
                                goodDisplayValue = goodMinValue + "" + goodOperator + "" + goodMaxValue;
                            }
                            else
                            {
                                if (row.GetCell(12, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null)
                                {
                                    legalFlag = false;
                                    validFlag = "0";
                                    if (errMsg == String.Empty)
                                    {
                                        errMsg = "合格品数字标准不完整";
                                    }
                                    else
                                    {
                                        errMsg += "/合格品数字标准不完整";
                                    }
                                }
                                else
                                {
                                    goodMaxValue = row.GetCell(12).ToString();
                                    if (goodMaxValue.ToCharArray()[0] == '.')
                                    {
                                        goodMaxValue = "0" + goodMaxValue;
                                    }
                                }
                                if (!Regex.Match(goodMaxValue, @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
                                {
                                    legalFlag = false;
                                    validFlag = "0";
                                    if (errMsg == String.Empty)
                                    {
                                        errMsg = "合格品数字格式不正确";
                                    }
                                    else
                                    {
                                        errMsg += "/合格品数字格式不正确";
                                    }
                                }
                                else
                                {
                                    goodDisplayValue = goodOperator + "" + goodMaxValue;
                                }
                            }
                        }
                        //一级品数字标准
                        if (legalFlag)
                        {
                            if (row.GetCell(6, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                            {
                                if (row.GetCell(6, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "")
                                {
                                    primeOperator = row.GetCell(6).ToString();
                                    if (row.GetCell(6).ToString() == "－")
                                    {
                                        if ((row.GetCell(4, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null) || (row.GetCell(7, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null))
                                        {
                                            legalFlag = false;
                                            validFlag = "0";
                                            if (errMsg == String.Empty)
                                            {
                                                errMsg = "一级品数字标准不完整";
                                            }
                                            else
                                            {
                                                errMsg += "/一级品数字标准不完整";
                                            }
                                        }
                                        else
                                        {
                                            primeMinValue = row.GetCell(4).ToString();
                                            primeMaxValue = row.GetCell(7).ToString();
                                        }
                                        if (legalFlag && (!Regex.Match(row.GetCell(4).ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success) || ((!Regex.Match(row.GetCell(7).ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)))
                                        {
                                            legalFlag = false;
                                            validFlag = "0";
                                            if (errMsg == String.Empty)
                                            {
                                                errMsg = "一级品数字格式不正确";
                                            }
                                            else
                                            {
                                                errMsg += "/一级品数字格式不正确";
                                            }
                                        }
                                        else if (Convert.ToDecimal(row.GetCell(4).ToString()) > Convert.ToDecimal(row.GetCell(7).ToString()))
                                        {
                                            legalFlag = false;
                                            validFlag = "0";
                                            if (errMsg == String.Empty)
                                            {
                                                errMsg = "一级品起始值大于结束值";
                                            }
                                            else
                                            {
                                                errMsg += "/一级品起始值大于结束值";
                                            }
                                        }
                                        else if (Convert.ToDecimal(row.GetCell(4).ToString()) == Convert.ToDecimal(row.GetCell(7).ToString()))
                                        {
                                            if ((row.GetCell(5, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null) && (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null))
                                            {
                                                if ((row.GetCell(5, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是") || (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是"))
                                                {
                                                    legalFlag = false;
                                                    validFlag = "0";
                                                    if (errMsg == String.Empty)
                                                    {
                                                        errMsg = "一级品起始值等于结束值但不包括边界";
                                                    }
                                                    else
                                                    {
                                                        errMsg += "/一级品起始值等于结束值但不包括边界";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                legalFlag = false;
                                                validFlag = "0";
                                                if (errMsg == String.Empty)
                                                {
                                                    errMsg = "一级品起始值等于结束值但不包括边界";
                                                }
                                                else
                                                {
                                                    errMsg += "/一级品起始值等于结束值但不包括边界";
                                                }
                                            }
                                        }
                                        primeDisplayValue = primeMinValue + "" + primeOperator + "" + primeMaxValue;
                                    }
                                    else
                                    {
                                        if (row.GetCell(7, MissingCellPolicy.RETURN_BLANK_AS_NULL) == null)
                                        {
                                            legalFlag = false;
                                            validFlag = "0";
                                            if (errMsg == String.Empty)
                                            {
                                                errMsg = "一级品数字标准不完整";
                                            }
                                            else
                                            {
                                                errMsg += "/一级品数字标准不完整";
                                            }
                                        }
                                        else
                                        {
                                            primeMaxValue = row.GetCell(7).ToString();
                                        }
                                        if (legalFlag && !Regex.Match(row.GetCell(7).ToString(), @"^-?(0|[1-9]\d{0,4})(\.\d{0,3})?$").Success)
                                        {
                                            legalFlag = false;
                                            validFlag = "0";
                                            if (errMsg == String.Empty)
                                            {
                                                errMsg = "一级品数字格式不正确";
                                            }
                                            else
                                            {
                                                errMsg += "/一级品数字格式不正确";
                                            }
                                        }
                                        else
                                        {
                                            primeDisplayValue = primeOperator + "" + primeMaxValue;
                                        }
                                    }
                                    if (legalFlag)
                                    {
                                        switch (goodOperator)
                                        {
                                            case "－":
                                                switch (primeOperator)
                                                {
                                                    case "－":
                                                        if ((Convert.ToDecimal(primeMaxValue) > Convert.ToDecimal(goodMaxValue)) || (Convert.ToDecimal(primeMinValue) < Convert.ToDecimal(goodMinValue)))
                                                        {
                                                            legalFlag = false;
                                                            validFlag = "0";
                                                            if (errMsg == String.Empty)
                                                            {
                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                            }
                                                            else
                                                            {
                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                            }
                                                            break;
                                                        }
                                                        else if ((Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue)) || (Convert.ToDecimal(primeMinValue) == Convert.ToDecimal(goodMinValue)))
                                                        {
                                                            if ((Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue)))
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                {
                                                                    if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                        {
                                                                            if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                            {
                                                                                legalFlag = false;
                                                                                validFlag = "0";
                                                                                if (errMsg == String.Empty)
                                                                                {
                                                                                    errMsg = "一级品指标范围在合格品范围之外";
                                                                                }
                                                                                else
                                                                                {
                                                                                    errMsg += "/一级品指标范围在合格品范围之外";
                                                                                }
                                                                                break;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if ((Convert.ToDecimal(primeMinValue) == Convert.ToDecimal(goodMinValue)))
                                                            {
                                                                if (row.GetCell(5, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                {
                                                                    if (row.GetCell(5, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                    {
                                                                        if (row.GetCell(10, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                        {
                                                                            if (row.GetCell(10, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                            {
                                                                                legalFlag = false;
                                                                                validFlag = "0";
                                                                                if (errMsg == String.Empty)
                                                                                {
                                                                                    errMsg = "一级品指标范围在合格品范围之外";
                                                                                }
                                                                                else
                                                                                {
                                                                                    errMsg += "/一级品指标范围在合格品范围之外";
                                                                                }
                                                                                break;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case ">":
                                                        if (Convert.ToDecimal(primeMaxValue) > Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            legalFlag = false;
                                                            validFlag = "0";
                                                            if (errMsg == String.Empty)
                                                            {
                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                            }
                                                            else
                                                            {
                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                            }
                                                            break;
                                                        }
                                                        else if (Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case "<":
                                                        if (Convert.ToDecimal(primeMaxValue) < Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            legalFlag = false;
                                                            validFlag = "0";
                                                            if (errMsg == String.Empty)
                                                            {
                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                            }
                                                            else
                                                            {
                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                            }
                                                            break;
                                                        }
                                                        else if (Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                break;
                                            case ">":
                                                switch (primeOperator)
                                                {
                                                    case "－":
                                                        if (Convert.ToDecimal(primeMinValue) < Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            legalFlag = false;
                                                            validFlag = "0";
                                                            if (errMsg == String.Empty)
                                                            {
                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                            }
                                                            else
                                                            {
                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                            }
                                                            break;
                                                        }
                                                        else if (Convert.ToDecimal(primeMinValue) == Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(5, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(5, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    legalFlag = false;
                                                                    validFlag = "0";
                                                                    if (errMsg == String.Empty)
                                                                    {
                                                                        errMsg = "一级品指标范围在合格品范围之外";
                                                                    }
                                                                    else
                                                                    {
                                                                        errMsg += "/一级品指标范围在合格品范围之外";
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case ">":
                                                        if (Convert.ToDecimal(primeMaxValue) < Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            legalFlag = false;
                                                            validFlag = "0";
                                                            if (errMsg == String.Empty)
                                                            {
                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                            }
                                                            else
                                                            {
                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                            }
                                                            break;
                                                        }
                                                        else if (Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case "<":
                                                        if (Convert.ToDecimal(primeMaxValue) < Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    legalFlag = false;
                                                                    validFlag = "0";
                                                                    if (errMsg == String.Empty)
                                                                    {
                                                                        errMsg = "一级品指标范围在合格品范围之外";
                                                                    }
                                                                    else
                                                                    {
                                                                        errMsg += "/一级品指标范围在合格品范围之外";
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        else if (Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    legalFlag = false;
                                                                    validFlag = "0";
                                                                    if (errMsg == String.Empty)
                                                                    {
                                                                        errMsg = "一级品指标范围在合格品范围之外";
                                                                    }
                                                                    else
                                                                    {
                                                                        errMsg += "/一级品指标范围在合格品范围之外";
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                legalFlag = false;
                                                                validFlag = "0";
                                                                if (errMsg == String.Empty)
                                                                {
                                                                    errMsg = "一级品指标范围在合格品范围之外";
                                                                }
                                                                else
                                                                {
                                                                    errMsg += "/一级品指标范围在合格品范围之外";
                                                                }
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                break;
                                            case "<":
                                                switch (primeOperator)
                                                {
                                                    case "－":
                                                        if (Convert.ToDecimal(primeMaxValue) > Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            legalFlag = false;
                                                            validFlag = "0";
                                                            if (errMsg == String.Empty)
                                                            {
                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                            }
                                                            else
                                                            {
                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                            }
                                                            break;
                                                        }
                                                        else if (Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    case ">":
                                                        if (Convert.ToDecimal(primeMaxValue) > Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            legalFlag = false;
                                                            validFlag = "0";
                                                            if (errMsg == String.Empty)
                                                            {
                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                            }
                                                            else
                                                            {
                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                            }
                                                            break;
                                                        }
                                                        else if (Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    legalFlag = false;
                                                                    validFlag = "0";
                                                                    if (errMsg == String.Empty)
                                                                    {
                                                                        errMsg = "一级品指标范围在合格品范围之外";
                                                                    }
                                                                    else
                                                                    {
                                                                        errMsg += "/一级品指标范围在合格品范围之外";
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                legalFlag = false;
                                                                validFlag = "0";
                                                                if (errMsg == String.Empty)
                                                                {
                                                                    errMsg = "一级品指标范围在合格品范围之外";
                                                                }
                                                                else
                                                                {
                                                                    errMsg += "/一级品指标范围在合格品范围之外";
                                                                }
                                                                break;
                                                            }
                                                        }
                                                        break;
                                                    case "<":
                                                        if (Convert.ToDecimal(primeMaxValue) > Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            legalFlag = false;
                                                            validFlag = "0";
                                                            if (errMsg == String.Empty)
                                                            {
                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                            }
                                                            else
                                                            {
                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                            }
                                                            break;
                                                        }
                                                        else if (Convert.ToDecimal(primeMaxValue) == Convert.ToDecimal(goodMaxValue))
                                                        {
                                                            if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                            {
                                                                if (row.GetCell(8, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() == "是")
                                                                {
                                                                    if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                                                                    {
                                                                        if (row.GetCell(13, MissingCellPolicy.RETURN_BLANK_AS_NULL).ToString() != "是")
                                                                        {
                                                                            legalFlag = false;
                                                                            validFlag = "0";
                                                                            if (errMsg == String.Empty)
                                                                            {
                                                                                errMsg = "一级品指标范围在合格品范围之外";
                                                                            }
                                                                            else
                                                                            {
                                                                                errMsg += "/一级品指标范围在合格品范围之外";
                                                                            }
                                                                            break;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        legalFlag = false;
                                                                        validFlag = "0";
                                                                        if (errMsg == String.Empty)
                                                                        {
                                                                            errMsg = "一级品指标范围在合格品范围之外";
                                                                        }
                                                                        else
                                                                        {
                                                                            errMsg += "/一级品指标范围在合格品范围之外";
                                                                        }
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        break;
                                                    default:
                                                        break;
                                                }
                                                break;
                                            default:
                                                legalFlag = false;
                                                validFlag = "0";
                                                if (errMsg == String.Empty)
                                                {
                                                    errMsg = "合格品运算符不正确";
                                                }
                                                else
                                                {
                                                    errMsg += "/合格品运算符不正确";
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (row.GetCell(16, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    checkMethod = row.GetCell(16).ToString();
                }
                if (row.GetCell(17, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    remark = row.GetCell(17).ToString();
                }
                if (checkMethod.Length > 50)
                {
                    legalFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "检测方法过长";
                    }
                    else
                    {
                        errMsg += "/检测方法过长";
                    }
                }
                if (remark.Length > 50)
                {
                    legalFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "备注过长";
                    }
                    else
                    {
                        errMsg += "/备注过长";
                    }
                }
                #endregion

                #region 向datatable插入此条数据
                if (validFlag == "1")
                {
                    if (coverFlag)
                    {
                        errMsg = "正常";
                    }
                    DataRow drValidData = dtValidData.NewRow();
                    drValidData["rowNum"] = rowNum;
                    drValidData["seriesId"] = seriesId;
                    drValidData["seriesName"] = seriesName;
                    drValidData["materialCode"] = materialCode;
                    drValidData["materialName"] = materialName;
                    drValidData["materialERPCode"] = materialERPCode;
                    drValidData["detailName"] = detailName;
                    drValidData["detailType"] = detailType;
                    drValidData["frequency"] = frequency;
                    drValidData["goodOperator"] = goodOperator;
                    drValidData["goodMinValue"] = goodMinValue;
                    drValidData["goodMaxValue"] = goodMaxValue;
                    drValidData["goodTextValue"] = goodTextValue;
                    drValidData["goodDisplayValue"] = goodDisplayValue;
                    drValidData["goodIncludeMinBorder"] = goodIncludeMinBorder;
                    drValidData["goodIncludeMaxBorder"] = goodIncludeMaxBorder;
                    drValidData["primeOperator"] = primeOperator;
                    drValidData["primeMinValue"] = primeMinValue;
                    drValidData["primeMaxValue"] = primeMaxValue;
                    drValidData["primeTextValue"] = primeTextValue;
                    drValidData["primeDisplayValue"] = primeDisplayValue;
                    drValidData["primeIncludeMinBorder"] = primeIncludeMinBorder;
                    drValidData["primeIncludeMaxBorder"] = primeIncludeMaxBorder;
                    drValidData["checkMethod"] = checkMethod;
                    drValidData["remark"] = remark;
                    drValidData["validFlag"] = validFlag;
                    drValidData["errMsg"] = errMsg;
                    dtValidData.Rows.Add(drValidData);
                }
                else if (validFlag == "0")
                {
                    if (coverFlag)
                    {
                        errMsg = errMsg.Replace("/会覆盖原有的检测指标/", "").Replace("/会覆盖原有的检测指标", "").Replace("会覆盖原有的检测指标/", "").Replace("会覆盖原有的检测指标", "");
                    }
                    DataRow drCorruptedData = dtCorruptedData.NewRow();
                    drCorruptedData["rowNum"] = rowNum;
                    drCorruptedData["seriesId"] = seriesId;
                    drCorruptedData["seriesName"] = seriesName;
                    drCorruptedData["materialCode"] = materialCode;
                    drCorruptedData["materialName"] = materialName;
                    drCorruptedData["materialERPCode"] = materialERPCode;
                    drCorruptedData["detailName"] = detailName;
                    drCorruptedData["detailType"] = detailType;
                    drCorruptedData["frequency"] = frequency;
                    drCorruptedData["goodOperator"] = goodOperator;
                    drCorruptedData["goodMinValue"] = goodMinValue;
                    drCorruptedData["goodMaxValue"] = goodMaxValue;
                    drCorruptedData["goodTextValue"] = goodTextValue;
                    drCorruptedData["goodDisplayValue"] = goodDisplayValue;
                    drCorruptedData["goodIncludeMinBorder"] = goodIncludeMinBorder;
                    drCorruptedData["goodIncludeMaxBorder"] = goodIncludeMaxBorder;
                    drCorruptedData["primeOperator"] = primeOperator;
                    drCorruptedData["primeMinValue"] = primeMinValue;
                    drCorruptedData["primeMaxValue"] = primeMaxValue;
                    drCorruptedData["primeTextValue"] = primeTextValue;
                    drCorruptedData["primeDisplayValue"] = primeDisplayValue;
                    drCorruptedData["primeIncludeMinBorder"] = primeIncludeMinBorder;
                    drCorruptedData["primeIncludeMaxBorder"] = primeIncludeMaxBorder;
                    drCorruptedData["checkMethod"] = checkMethod;
                    drCorruptedData["remark"] = remark;
                    drCorruptedData["validFlag"] = validFlag;
                    drCorruptedData["errMsg"] = errMsg;
                    dtCorruptedData.Rows.Add(drCorruptedData);
                }
                DataRow drAllData = dtAllData.NewRow();
                drAllData["rowNum"] = rowNum;
                drAllData["seriesId"] = seriesId;
                drAllData["seriesName"] = seriesName;
                drAllData["materialCode"] = materialCode;
                drAllData["materialName"] = materialName;
                drAllData["materialERPCode"] = materialERPCode;
                drAllData["detailName"] = detailName;
                drAllData["detailType"] = detailType;
                drAllData["frequency"] = frequency;
                drAllData["goodOperator"] = goodOperator;
                drAllData["goodMinValue"] = goodMinValue;
                drAllData["goodMaxValue"] = goodMaxValue;
                drAllData["goodTextValue"] = goodTextValue;
                drAllData["goodDisplayValue"] = goodDisplayValue;
                drAllData["goodIncludeMinBorder"] = goodIncludeMinBorder;
                drAllData["goodIncludeMaxBorder"] = goodIncludeMaxBorder;
                drAllData["primeOperator"] = primeOperator;
                drAllData["primeMinValue"] = primeMinValue;
                drAllData["primeMaxValue"] = primeMaxValue;
                drAllData["primeTextValue"] = primeTextValue;
                drAllData["primeDisplayValue"] = primeDisplayValue;
                drAllData["primeIncludeMinBorder"] = primeIncludeMinBorder;
                drAllData["primeIncludeMaxBorder"] = primeIncludeMaxBorder;
                drAllData["checkMethod"] = checkMethod;
                drAllData["remark"] = remark;
                drAllData["validFlag"] = validFlag;
                drAllData["errMsg"] = errMsg;
                dtAllData.Rows.Add(drAllData);
                #endregion

                tableRowNum++;
            }
        }
    }

    #endregion

    #region 点击增删改按钮激发的事件

    /// <summary>
    /// 点击下载模板按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_download_Click(object sender, EventArgs e)
    {
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        HSSFWorkbook file = new HSSFWorkbook();
        BasMaterialMinorType minorType = new BasMaterialMinorType();
        //校验请求合法性并获取相关数据
        EntityArrayList<BasMaterial> materialList = materialManager.GetListByWhere((BasMaterial._.MinorTypeID == cbxSeriesName.Value) && (BasMaterial._.MajorTypeID == "1") && (BasMaterial._.DeleteFlag == "0"));
        if (minorTypeManager.GetListByWhere((BasMaterialMinorType._.MinorTypeID == cbxSeriesName.Value) && (BasMaterialMinorType._.DeleteFlag == "0")).Count > 0)
        {
            minorType = minorTypeManager.GetListByWhere((BasMaterialMinorType._.MinorTypeID == cbxSeriesName.Value) && (BasMaterialMinorType._.DeleteFlag == "0"))[0];
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Category = minorType.MinorTypeID;
            file.DocumentSummaryInformation = dsi;
        }
        else
        {
            X.Msg.Alert("提示", "模板生成错误，没有选择正确的原材料系列！").Show();
            return;
        }
        EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.SeriesId == cbxSeriesName.Value) && (QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0"));
        if (itemList.Count == 0)
        {
            X.Msg.Alert("提示", "模板生成错误，选中的执行标准下的该原材料系列无可用的检测项目！").Show();
            return;
        }
        if (cbxIsBlankTemplate.Checked)
        {
            //为该系列原材料的每种物料生成sheet页
            foreach (BasMaterial material in materialList)
            {
                ISheet sheet = new HSSFSheet(file);
                try
                {
                    sheet = file.CreateSheet(material.MaterialName.Replace("/", "∕").Replace("*", "x") + "_" + material.MaterialCode);//sheet页名字中某些字符不可使用，用类似字符替代
                    sheet.CreateFreezePane(1, 1);// 冻结    
                    // 设置列宽    
                    sheet.SetColumnWidth(0, 1000);
                    sheet.SetColumnWidth(1, 4500);
                    sheet.SetColumnWidth(2, 2500);
                    sheet.SetColumnWidth(3, 2500);
                    sheet.SetColumnWidth(4, 3500);
                    sheet.SetColumnWidth(5, 2000);
                    sheet.SetColumnWidth(6, 2000);
                    sheet.SetColumnWidth(7, 3500);
                    sheet.SetColumnWidth(8, 2000);
                    sheet.SetColumnWidth(9, 3500);
                    sheet.SetColumnWidth(10, 2000);
                    sheet.SetColumnWidth(11, 2000);
                    sheet.SetColumnWidth(12, 3500);
                    sheet.SetColumnWidth(13, 2000);
                    sheet.SetColumnWidth(14, 6000);
                    sheet.SetColumnWidth(15, 6000);
                    sheet.SetColumnWidth(16, 3500);
                    sheet.SetColumnWidth(17, 7000);
                    // 创建第一行    
                    IRow row0 = sheet.CreateRow(0);
                    // 设置行高    
                    row0.Height = ((short)500);
                    // 创建表头列   
                    ICell cell1 = row0.CreateCell(1);
                    ICell cell2 = row0.CreateCell(2);
                    ICell cell3 = row0.CreateCell(3);
                    ICell cell4 = row0.CreateCell(4);
                    ICell cell5 = row0.CreateCell(5);
                    ICell cell6 = row0.CreateCell(6);
                    ICell cell7 = row0.CreateCell(7);
                    ICell cell8 = row0.CreateCell(8);
                    ICell cell9 = row0.CreateCell(9);
                    ICell cell10 = row0.CreateCell(10);
                    ICell cell11 = row0.CreateCell(11);
                    ICell cell12 = row0.CreateCell(12);
                    ICell cell13 = row0.CreateCell(13);
                    ICell cell14 = row0.CreateCell(14);
                    ICell cell15 = row0.CreateCell(15);
                    ICell cell16 = row0.CreateCell(16);
                    ICell cell17 = row0.CreateCell(17);
                    cell1.SetCellValue(new HSSFRichTextString("检测项目"));
                    cell2.SetCellValue(new HSSFRichTextString("项目类型"));
                    cell3.SetCellValue(new HSSFRichTextString("检测频次"));
                    cell4.SetCellValue(new HSSFRichTextString("一级品起始值"));
                    cell5.SetCellValue(new HSSFRichTextString("包含边界"));
                    cell6.SetCellValue(new HSSFRichTextString("关系符"));
                    cell7.SetCellValue(new HSSFRichTextString("一级品结束值"));
                    cell8.SetCellValue(new HSSFRichTextString("包含边界"));
                    cell9.SetCellValue(new HSSFRichTextString("合格品起始值"));
                    cell10.SetCellValue(new HSSFRichTextString("包含边界"));
                    cell11.SetCellValue(new HSSFRichTextString("关系符"));
                    cell12.SetCellValue(new HSSFRichTextString("合格品结束值"));
                    cell13.SetCellValue(new HSSFRichTextString("包含边界"));
                    cell14.SetCellValue(new HSSFRichTextString("一级品文字标准"));
                    cell15.SetCellValue(new HSSFRichTextString("合格品文字标准"));
                    cell16.SetCellValue(new HSSFRichTextString("检测方法"));
                    cell17.SetCellValue(new HSSFRichTextString("备注"));
                    foreach (ICell cell in row0)
                    {
                        cell.CellStyle.Alignment = HorizontalAlignment.CENTER;
                        cell.CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
                    }
                }
                catch (Exception)
                {
                    X.Msg.Alert("提示", "模板生成错误，原材料信息有误！可能的原因：有名字重复的原材料").Show();//名字重复的型号提示错误（物料管理问题，不应该存在重复的物料）
                    return;
                }
                int starter = 1;//行计数器
                foreach (QmcCheckItem item in itemList)
                {
                    // 创建检测项目数据行
                    IRow dataRow = sheet.CreateRow(starter);
                    // 设置行高    
                    dataRow.Height = ((short)450);
                    // 创建数据列
                    ICell cell0 = dataRow.CreateCell(0);
                    ICell cell1 = dataRow.CreateCell(1);
                    ICell cell2 = dataRow.CreateCell(2);
                    ICell cell3 = dataRow.CreateCell(3);
                    ICell cell4 = dataRow.CreateCell(4);
                    ICell cell5 = dataRow.CreateCell(5);
                    ICell cell6 = dataRow.CreateCell(6);
                    ICell cell7 = dataRow.CreateCell(7);
                    ICell cell8 = dataRow.CreateCell(8);
                    ICell cell9 = dataRow.CreateCell(9);
                    ICell cell10 = dataRow.CreateCell(10);
                    ICell cell11 = dataRow.CreateCell(11);
                    ICell cell12 = dataRow.CreateCell(12);
                    ICell cell13 = dataRow.CreateCell(13);
                    ICell cell14 = dataRow.CreateCell(14);
                    ICell cell15 = dataRow.CreateCell(15);
                    ICell cell16 = dataRow.CreateCell(16);
                    ICell cell17 = dataRow.CreateCell(17);
                    cell0.SetCellValue(new HSSFRichTextString(starter.ToString()));
                    cell1.SetCellValue(new HSSFRichTextString(item.ItemName));
                    cell2.SetCellValue(new HSSFRichTextString(item.ValueType));
                    //增加检测频次下拉菜单
                    CellRangeAddressList regionsFrequency = new CellRangeAddressList(starter, starter, 3, 3);
                    DVConstraint constraintFrequency = DVConstraint.CreateExplicitListConstraint(new string[] { "C", "M", "N", "F" });
                    HSSFDataValidation dataValidateFrequency = new HSSFDataValidation(regionsFrequency, constraintFrequency);
                    sheet.AddValidationData(dataValidateFrequency);
                    if (item.ValueType == "文字")
                    {
                        //检测项值类型为文字的情况，关闭数字相关单元格
                        cell4.SetCellValue(new HSSFRichTextString("N/A"));
                        cell5.SetCellValue(new HSSFRichTextString("N/A"));
                        cell6.SetCellValue(new HSSFRichTextString("N/A"));
                        cell7.SetCellValue(new HSSFRichTextString("N/A"));
                        cell8.SetCellValue(new HSSFRichTextString("N/A"));
                        cell9.SetCellValue(new HSSFRichTextString("N/A"));
                        cell10.SetCellValue(new HSSFRichTextString("N/A"));
                        cell11.SetCellValue(new HSSFRichTextString("N/A"));
                        cell12.SetCellValue(new HSSFRichTextString("N/A"));
                        cell13.SetCellValue(new HSSFRichTextString("N/A"));
                    }
                    else if (item.ValueType == "数字")
                    {
                        //检测项值类型为数字的情况，关闭文字相关单元格
                        cell14.SetCellValue(new HSSFRichTextString("N/A"));
                        cell15.SetCellValue(new HSSFRichTextString("N/A"));
                        //增加关系符下拉菜单
                        CellRangeAddressList regionsPrimeOperator = new CellRangeAddressList(starter, starter, 6, 6);
                        DVConstraint constraintPrimeOperator = DVConstraint.CreateExplicitListConstraint(new string[] { "－", ">", "<" });
                        HSSFDataValidation dataValidatePrimeOperator = new HSSFDataValidation(regionsPrimeOperator, constraintPrimeOperator);
                        sheet.AddValidationData(dataValidatePrimeOperator);
                        CellRangeAddressList regionsGoodOperator = new CellRangeAddressList(starter, starter, 11, 11);
                        DVConstraint constraintGoodOperator = DVConstraint.CreateExplicitListConstraint(new string[] { "－", ">", "<" });
                        HSSFDataValidation dataValidateGoodOperator = new HSSFDataValidation(regionsGoodOperator, constraintGoodOperator);
                        sheet.AddValidationData(dataValidateGoodOperator);
                        //增加包含边界下拉菜单
                        cell5.SetCellValue(new HSSFRichTextString("是"));
                        cell8.SetCellValue(new HSSFRichTextString("是"));
                        cell10.SetCellValue(new HSSFRichTextString("是"));
                        cell13.SetCellValue(new HSSFRichTextString("是"));
                        CellRangeAddressList regionsPrimeMinBorder = new CellRangeAddressList(starter, starter, 5, 5);
                        DVConstraint constraintPrimeMinBorder = DVConstraint.CreateExplicitListConstraint(new string[] { "是", "否" });
                        HSSFDataValidation dataValidatePrimeMinBorder = new HSSFDataValidation(regionsPrimeMinBorder, constraintPrimeMinBorder);
                        sheet.AddValidationData(dataValidatePrimeMinBorder);
                        CellRangeAddressList regionsPrimeMaxBorder = new CellRangeAddressList(starter, starter, 8, 8);
                        DVConstraint constraintPrimeMaxBorder = DVConstraint.CreateExplicitListConstraint(new string[] { "是", "否" });
                        HSSFDataValidation dataValidatePrimeMaxBorder = new HSSFDataValidation(regionsPrimeMaxBorder, constraintPrimeMaxBorder);
                        sheet.AddValidationData(dataValidatePrimeMaxBorder);
                        CellRangeAddressList regionsGoodMinBorder = new CellRangeAddressList(starter, starter, 10, 10);
                        DVConstraint constraintGoodMinBorder = DVConstraint.CreateExplicitListConstraint(new string[] { "是", "否" });
                        HSSFDataValidation dataValidateGoodMinBorder = new HSSFDataValidation(regionsGoodMinBorder, constraintGoodMinBorder);
                        sheet.AddValidationData(dataValidateGoodMinBorder);
                        CellRangeAddressList regionsGoodMaxBorder = new CellRangeAddressList(starter, starter, 13, 13);
                        DVConstraint constraintGoodMaxBorder = DVConstraint.CreateExplicitListConstraint(new string[] { "是", "否" });
                        HSSFDataValidation dataValidateGoodMaxBorder = new HSSFDataValidation(regionsGoodMaxBorder, constraintGoodMaxBorder);
                        sheet.AddValidationData(dataValidateGoodMaxBorder);
                    }
                    starter++;//行计数器自增
                }
            }
            MemoryStream excelStream = new MemoryStream();
            file.Write(excelStream);
            new Mesnac.Util.Excel.ExcelDownload().FileDown(excelStream, cbxStandard.SelectedItem.Text.ToString() + "~检测指标导入模板-" + minorType.MinorTypeName);
        }
        else
        {
            //为该系列原材料的每种物料生成sheet页
            foreach (BasMaterial material in materialList)
            {
                EntityArrayList<QmcCheckItemDetail> detailFullList = detailManager.GetListByWhere((QmcCheckItemDetail._.MaterialCode == material.MaterialCode) && (QmcCheckItemDetail._.DeleteFlag == "0") && (QmcCheckItemDetail._.LatestFlag == "1"));
                EntityArrayList<QmcCheckItemDetail> detailList = new EntityArrayList<QmcCheckItemDetail>();
                foreach (QmcCheckItemDetail detail in detailFullList)
                {
                    foreach (QmcCheckItem item in itemList)
                    {
                        if (detail.ItemId == item.ItemId)
                        {
                            detailList.Add(detail);
                        }
                    }
                }
                ISheet sheet = new HSSFSheet(file);
                try
                {
                    sheet = file.CreateSheet(material.MaterialName.Replace("/", "∕").Replace("*", "x") + "_" + material.MaterialCode);//sheet页名字中某些字符不可使用，用类似字符替代
                    sheet.CreateFreezePane(1, 1);// 冻结    
                    // 设置列宽    
                    sheet.SetColumnWidth(0, 1000);
                    sheet.SetColumnWidth(1, 4500);
                    sheet.SetColumnWidth(2, 2500);
                    sheet.SetColumnWidth(3, 2500);
                    sheet.SetColumnWidth(4, 3500);
                    sheet.SetColumnWidth(5, 2000);
                    sheet.SetColumnWidth(6, 2000);
                    sheet.SetColumnWidth(7, 3500);
                    sheet.SetColumnWidth(8, 2000);
                    sheet.SetColumnWidth(9, 3500);
                    sheet.SetColumnWidth(10, 2000);
                    sheet.SetColumnWidth(11, 2000);
                    sheet.SetColumnWidth(12, 3500);
                    sheet.SetColumnWidth(13, 2000);
                    sheet.SetColumnWidth(14, 6000);
                    sheet.SetColumnWidth(15, 6000);
                    sheet.SetColumnWidth(16, 3500);
                    sheet.SetColumnWidth(17, 7000);
                    // 创建第一行    
                    IRow row0 = sheet.CreateRow(0);
                    // 设置行高    
                    row0.Height = ((short)500);
                    // 创建表头列   
                    ICell cell1 = row0.CreateCell(1);
                    ICell cell2 = row0.CreateCell(2);
                    ICell cell3 = row0.CreateCell(3);
                    ICell cell4 = row0.CreateCell(4);
                    ICell cell5 = row0.CreateCell(5);
                    ICell cell6 = row0.CreateCell(6);
                    ICell cell7 = row0.CreateCell(7);
                    ICell cell8 = row0.CreateCell(8);
                    ICell cell9 = row0.CreateCell(9);
                    ICell cell10 = row0.CreateCell(10);
                    ICell cell11 = row0.CreateCell(11);
                    ICell cell12 = row0.CreateCell(12);
                    ICell cell13 = row0.CreateCell(13);
                    ICell cell14 = row0.CreateCell(14);
                    ICell cell15 = row0.CreateCell(15);
                    ICell cell16 = row0.CreateCell(16);
                    ICell cell17 = row0.CreateCell(17);
                    cell1.SetCellValue(new HSSFRichTextString("检测项目"));
                    cell2.SetCellValue(new HSSFRichTextString("项目类型"));
                    cell3.SetCellValue(new HSSFRichTextString("检测频次"));
                    cell4.SetCellValue(new HSSFRichTextString("一级品起始值"));
                    cell5.SetCellValue(new HSSFRichTextString("包含边界"));
                    cell6.SetCellValue(new HSSFRichTextString("关系符"));
                    cell7.SetCellValue(new HSSFRichTextString("一级品结束值"));
                    cell8.SetCellValue(new HSSFRichTextString("包含边界"));
                    cell9.SetCellValue(new HSSFRichTextString("合格品起始值"));
                    cell10.SetCellValue(new HSSFRichTextString("包含边界"));
                    cell11.SetCellValue(new HSSFRichTextString("关系符"));
                    cell12.SetCellValue(new HSSFRichTextString("合格品结束值"));
                    cell13.SetCellValue(new HSSFRichTextString("包含边界"));
                    cell14.SetCellValue(new HSSFRichTextString("一级品文字标准"));
                    cell15.SetCellValue(new HSSFRichTextString("合格品文字标准"));
                    cell16.SetCellValue(new HSSFRichTextString("检测方法"));
                    cell17.SetCellValue(new HSSFRichTextString("备注"));
                    foreach (ICell cell in row0)
                    {
                        cell.CellStyle.Alignment = HorizontalAlignment.CENTER;
                        cell.CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
                    }
                }
                catch (Exception)
                {
                    X.Msg.Alert("提示", "模板生成错误，原材料信息有误！可能的原因：有名字重复的原材料").Show();//名字重复的型号提示错误（物料管理问题，不应该存在重复的物料）
                    return;
                }
                int starter = 1;//行计数器
                foreach (QmcCheckItemDetail detail in detailList)
                {
                    QmcCheckItem item = itemManager.GetById(detail.ItemId);
                    // 创建检测项目数据行
                    IRow dataRow = sheet.CreateRow(starter);
                    // 设置行高    
                    dataRow.Height = ((short)450);
                    // 创建数据列
                    ICell cell0 = dataRow.CreateCell(0);
                    ICell cell1 = dataRow.CreateCell(1);
                    ICell cell2 = dataRow.CreateCell(2);
                    ICell cell3 = dataRow.CreateCell(3);
                    ICell cell4 = dataRow.CreateCell(4);
                    ICell cell5 = dataRow.CreateCell(5);
                    ICell cell6 = dataRow.CreateCell(6);
                    ICell cell7 = dataRow.CreateCell(7);
                    ICell cell8 = dataRow.CreateCell(8);
                    ICell cell9 = dataRow.CreateCell(9);
                    ICell cell10 = dataRow.CreateCell(10);
                    ICell cell11 = dataRow.CreateCell(11);
                    ICell cell12 = dataRow.CreateCell(12);
                    ICell cell13 = dataRow.CreateCell(13);
                    ICell cell14 = dataRow.CreateCell(14);
                    ICell cell15 = dataRow.CreateCell(15);
                    ICell cell16 = dataRow.CreateCell(16);
                    ICell cell17 = dataRow.CreateCell(17);
                    cell0.SetCellValue(new HSSFRichTextString(starter.ToString()));
                    cell1.SetCellValue(new HSSFRichTextString(item.ItemName));
                    cell2.SetCellValue(new HSSFRichTextString(item.ValueType));
                    //增加检测频次下拉菜单
                    CellRangeAddressList regionsFrequency = new CellRangeAddressList(starter, starter, 3, 3);
                    DVConstraint constraintFrequency = DVConstraint.CreateExplicitListConstraint(new string[] { "C", "M", "N", "F" });
                    HSSFDataValidation dataValidateFrequency = new HSSFDataValidation(regionsFrequency, constraintFrequency);
                    sheet.AddValidationData(dataValidateFrequency);
                    cell3.SetCellValue(new HSSFRichTextString(detail.Frequency));
                    if (item.ValueType == "文字")
                    {
                        //检测项值类型为文字的情况，关闭数字相关单元格
                        cell4.SetCellValue(new HSSFRichTextString("N/A"));
                        cell5.SetCellValue(new HSSFRichTextString("N/A"));
                        cell6.SetCellValue(new HSSFRichTextString("N/A"));
                        cell7.SetCellValue(new HSSFRichTextString("N/A"));
                        cell8.SetCellValue(new HSSFRichTextString("N/A"));
                        cell9.SetCellValue(new HSSFRichTextString("N/A"));
                        cell10.SetCellValue(new HSSFRichTextString("N/A"));
                        cell11.SetCellValue(new HSSFRichTextString("N/A"));
                        cell12.SetCellValue(new HSSFRichTextString("N/A"));
                        cell13.SetCellValue(new HSSFRichTextString("N/A"));
                        cell14.SetCellValue(new HSSFRichTextString(detail.PrimeTextValue));
                        cell15.SetCellValue(new HSSFRichTextString(detail.GoodTextValue));
                    }
                    else if (item.ValueType == "数字")
                    {
                        //检测项值类型为数字的情况，关闭文字相关单元格
                        cell14.SetCellValue(new HSSFRichTextString("N/A"));
                        cell15.SetCellValue(new HSSFRichTextString("N/A"));
                        //增加关系符下拉菜单
                        CellRangeAddressList regionsPrimeOperator = new CellRangeAddressList(starter, starter, 6, 6);
                        DVConstraint constraintPrimeOperator = DVConstraint.CreateExplicitListConstraint(new string[] { "－", ">", "<" });
                        HSSFDataValidation dataValidatePrimeOperator = new HSSFDataValidation(regionsPrimeOperator, constraintPrimeOperator);
                        sheet.AddValidationData(dataValidatePrimeOperator);
                        cell6.SetCellValue(new HSSFRichTextString(detail.PrimeOperator));
                        CellRangeAddressList regionsGoodOperator = new CellRangeAddressList(starter, starter, 11, 11);
                        DVConstraint constraintGoodOperator = DVConstraint.CreateExplicitListConstraint(new string[] { "－", ">", "<" });
                        HSSFDataValidation dataValidateGoodOperator = new HSSFDataValidation(regionsGoodOperator, constraintGoodOperator);
                        sheet.AddValidationData(dataValidateGoodOperator);
                        cell11.SetCellValue(new HSSFRichTextString(detail.GoodOperator));
                        //写入数值
                        cell4.SetCellValue(new HSSFRichTextString(detail.PrimeMinValue.ToString()));
                        cell7.SetCellValue(new HSSFRichTextString(detail.PrimeMaxValue.ToString()));
                        cell9.SetCellValue(new HSSFRichTextString(detail.GoodMinValue.ToString()));
                        cell12.SetCellValue(new HSSFRichTextString(detail.GoodMaxValue.ToString()));
                        //增加包含边界下拉菜单
                        CellRangeAddressList regionsPrimeMinBorder = new CellRangeAddressList(starter, starter, 5, 5);
                        DVConstraint constraintPrimeMinBorder = DVConstraint.CreateExplicitListConstraint(new string[] { "是", "否" });
                        HSSFDataValidation dataValidatePrimeMinBorder = new HSSFDataValidation(regionsPrimeMinBorder, constraintPrimeMinBorder);
                        sheet.AddValidationData(dataValidatePrimeMinBorder);
                        CellRangeAddressList regionsPrimeMaxBorder = new CellRangeAddressList(starter, starter, 8, 8);
                        DVConstraint constraintPrimeMaxBorder = DVConstraint.CreateExplicitListConstraint(new string[] { "是", "否" });
                        HSSFDataValidation dataValidatePrimeMaxBorder = new HSSFDataValidation(regionsPrimeMaxBorder, constraintPrimeMaxBorder);
                        sheet.AddValidationData(dataValidatePrimeMaxBorder);
                        CellRangeAddressList regionsGoodMinBorder = new CellRangeAddressList(starter, starter, 10, 10);
                        DVConstraint constraintGoodMinBorder = DVConstraint.CreateExplicitListConstraint(new string[] { "是", "否" });
                        HSSFDataValidation dataValidateGoodMinBorder = new HSSFDataValidation(regionsGoodMinBorder, constraintGoodMinBorder);
                        sheet.AddValidationData(dataValidateGoodMinBorder);
                        CellRangeAddressList regionsGoodMaxBorder = new CellRangeAddressList(starter, starter, 13, 13);
                        DVConstraint constraintGoodMaxBorder = DVConstraint.CreateExplicitListConstraint(new string[] { "是", "否" });
                        HSSFDataValidation dataValidateGoodMaxBorder = new HSSFDataValidation(regionsGoodMaxBorder, constraintGoodMaxBorder);
                        sheet.AddValidationData(dataValidateGoodMaxBorder);
                        if (detail.PrimeIncludeMinBorder == "1")
                        {
                            cell5.SetCellValue(new HSSFRichTextString("是"));
                        }
                        else
                        {
                            cell5.SetCellValue(new HSSFRichTextString("否"));
                        }
                        if (detail.PrimeIncludeMaxBorder == "1")
                        {
                            cell8.SetCellValue(new HSSFRichTextString("是"));
                        }
                        else
                        {
                            cell8.SetCellValue(new HSSFRichTextString("否"));
                        }
                        if (detail.GoodIncludeMinBorder == "1")
                        {
                            cell10.SetCellValue(new HSSFRichTextString("是"));
                        }
                        else
                        {
                            cell10.SetCellValue(new HSSFRichTextString("否"));
                        }
                        if (detail.GoodIncludeMaxBorder == "1")
                        {
                            cell13.SetCellValue(new HSSFRichTextString("是"));
                        }
                        else
                        {
                            cell13.SetCellValue(new HSSFRichTextString("否"));
                        }
                    }
                    starter++;//行计数器自增
                }
            }
            MemoryStream excelStream = new MemoryStream();
            file.Write(excelStream);
            new Mesnac.Util.Excel.ExcelDownload().FileDown(excelStream, cbxStandard.SelectedItem.Text.ToString() + "~检测指标导入模板-" + minorType.MinorTypeName);
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void StoreCenterValid_SubmitData(object sender, StoreSubmitDataEventArgs e)
    {
        string standardId = String.Empty;
        if (cbxStandard.Value != null)
        {
            standardId = cbxStandard.Value.ToString();
        }
        else
        {
            msg.Alert("操作", "没有选择执行标准！");
            msg.Show();
            return;
        }
        List<JsonObject> joList = e.Object<JsonObject>();
        if (joList.Count == 0)
        {
            X.Msg.Alert("提示", "没有需要提交保存的信息").Show();
            return;
        }
        EntityArrayList<QmcCheckItemDetail> addList = new EntityArrayList<QmcCheckItemDetail>();
        EntityArrayList<QmcCheckItemDetail> updateList = new EntityArrayList<QmcCheckItemDetail>();
        int detailIdPointer = 0;
        foreach (JsonObject jo in joList)
        {
            //导入文件中的数据写入原有指标
            QmcCheckItemDetail detail = null;
            EntityArrayList<QmcCheckItem> itemList = itemManager.GetListByWhere((QmcCheckItem._.SeriesId == jo["seriesId"].ToString()) && (QmcCheckItem._.StandardId == standardId) && (QmcCheckItem._.DeleteFlag == "0"));
            foreach (QmcCheckItem item in itemList)
            {
                if ((detailManager.GetListByWhere((QmcCheckItemDetail._.MaterialCode == jo["materialCode"].ToString()) && (QmcCheckItemDetail._.ItemId == item.ItemId) && (QmcCheckItemDetail._.Frequency == jo["frequency"].ToString()) && (QmcCheckItemDetail._.DeleteFlag == "0") && (QmcCheckItemDetail._.LatestFlag == "1")).Count > 0) && (item.ItemName == jo["detailName"].ToString()))
                {
                    detail = detailManager.GetListByWhere((QmcCheckItemDetail._.MaterialCode == jo["materialCode"].ToString()) && (QmcCheckItemDetail._.ItemId == item.ItemId) && (QmcCheckItemDetail._.Frequency == jo["frequency"].ToString()) && (QmcCheckItemDetail._.DeleteFlag == "0") && (QmcCheckItemDetail._.LatestFlag == "1"))[0];
                    detail.CheckMethod = jo["checkMethod"].ToString();
                    detail.DeleteFlag = "0";
                    detail.Remark = jo["remark"].ToString();
                    detail.LastDate = DateTime.Now;
                    switch (item.ValueType)
                    {
                        case "文字":
                            detail.GoodTextValue = jo["goodTextValue"].ToString();
                            detail.GoodDisplayValue = jo["goodTextValue"].ToString();
                            if (jo["primeTextValue"].ToString() != String.Empty)
                            {
                                detail.PrimeTextValue = jo["primeTextValue"].ToString();
                                detail.PrimeDisplayValue = jo["primeTextValue"].ToString();
                            }
                            break;
                        case "数字":
                            detail.GoodOperator = jo["goodOperator"].ToString();
                            if (detail.GoodOperator == "－")
                            {
                                detail.GoodMinValue = Convert.ToDecimal(jo["goodMinValue"].ToString());
                                detail.InputGoodMinValue = jo["goodMinValue"].ToString();
                                detail.GoodIncludeMinBorder = jo["goodIncludeMinBorder"].ToString();
                            }
                            detail.GoodMaxValue = Convert.ToDecimal(jo["goodMaxValue"].ToString());
                            detail.InputGoodMaxValue = jo["goodMaxValue"].ToString();
                            detail.GoodIncludeMaxBorder = jo["goodIncludeMaxBorder"].ToString();
                            if (jo["primeOperator"].ToString() != String.Empty)
                            {
                                detail.PrimeOperator = jo["primeOperator"].ToString();
                                if (detail.PrimeOperator == "－")
                                {
                                    detail.PrimeMinValue = Convert.ToDecimal(jo["primeMinValue"].ToString());
                                    detail.InputPrimeMinValue = jo["primeMinValue"].ToString();
                                    detail.PrimeIncludeMinBorder = jo["primeIncludeMinBorder"].ToString();
                                }
                                detail.PrimeMaxValue = Convert.ToDecimal(jo["primeMaxValue"].ToString());
                                detail.InputPrimeMaxValue = jo["primeMaxValue"].ToString();
                                detail.PrimeIncludeMaxBorder = jo["primeIncludeMaxBorder"].ToString();
                            }
                            #region 写入数字显示值
                            //合格品
                            if (detail.GoodOperator == "－")
                            {
                                detail.GoodDisplayValue = detail.InputGoodMinValue + detail.GoodOperator + detail.InputGoodMaxValue;
                            }
                            if (detail.GoodOperator == ">")
                            {
                                if (detail.GoodIncludeMaxBorder == "1")
                                {
                                    detail.GoodDisplayValue = "≥" + detail.InputGoodMaxValue;
                                }
                                else
                                {
                                    detail.GoodDisplayValue = detail.GoodOperator + detail.InputGoodMaxValue;
                                }
                            }
                            if (detail.GoodOperator == "<")
                            {
                                if (detail.GoodIncludeMaxBorder == "1")
                                {
                                    detail.GoodDisplayValue = "≤" + detail.InputGoodMaxValue;
                                }
                                else
                                {
                                    detail.GoodDisplayValue = detail.GoodOperator + detail.InputGoodMaxValue;
                                }
                            }
                            //一级品
                            if (detail.PrimeOperator == "－")
                            {
                                detail.PrimeDisplayValue = detail.InputPrimeMinValue + detail.PrimeOperator + detail.InputPrimeMaxValue;
                            }
                            if (detail.PrimeOperator == ">")
                            {
                                if (detail.PrimeIncludeMaxBorder == "1")
                                {
                                    detail.PrimeDisplayValue = "≥" + detail.InputPrimeMaxValue;
                                }
                                else
                                {
                                    detail.PrimeDisplayValue = detail.PrimeOperator + detail.InputPrimeMaxValue;
                                }
                            }
                            if (detail.PrimeOperator == "<")
                            {
                                if (detail.PrimeIncludeMaxBorder == "1")
                                {
                                    detail.PrimeDisplayValue = "≤" + detail.InputPrimeMaxValue;
                                }
                                else
                                {
                                    detail.PrimeDisplayValue = detail.PrimeOperator + detail.InputPrimeMaxValue;
                                }
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }
                }
            }
            //若无原有指标则新建指标
            if (detail == null)
            {
                detail = new QmcCheckItemDetail();
                detail.ItemDetailId = Convert.ToInt32(detailManager.GetNextDetailId()) + detailIdPointer;
                foreach (QmcCheckItem item in itemList)
                {
                    if (item.ItemName == jo["detailName"].ToString())
                    {
                        detail.ItemId = item.ItemId;
                        break;
                    }
                }
                detail.CheckMethod = jo["checkMethod"].ToString();
                detail.DeleteFlag = "0";
                detail.LatestFlag = "1";
                detail.ActivateFlag = "1";
                detail.Version = 1;
                detail.LastDate = DateTime.Now;
                detail.Frequency = jo["frequency"].ToString();
                detail.Remark = jo["remark"].ToString();
                detail.MaterialCode = jo["materialCode"].ToString();
                switch (jo["detailType"].ToString())
                {
                    case "文字":
                        detail.GoodTextValue = jo["goodTextValue"].ToString();
                        detail.GoodDisplayValue = jo["goodTextValue"].ToString();
                        if (jo["primeTextValue"].ToString() != String.Empty)
                        {
                            detail.PrimeTextValue = jo["primeTextValue"].ToString();
                            detail.PrimeDisplayValue = jo["primeTextValue"].ToString();
                        }
                        break;
                    case "数字":
                        detail.GoodOperator = jo["goodOperator"].ToString();
                        if (detail.GoodOperator == "－")
                        {
                            detail.GoodMinValue = Convert.ToDecimal(jo["goodMinValue"].ToString());
                            detail.InputGoodMinValue = jo["goodMinValue"].ToString();
                            detail.GoodIncludeMinBorder = jo["goodIncludeMinBorder"].ToString();
                        }
                        detail.GoodMaxValue = Convert.ToDecimal(jo["goodMaxValue"].ToString());
                        detail.InputGoodMaxValue = jo["goodMaxValue"].ToString();
                        detail.GoodIncludeMaxBorder = jo["goodIncludeMaxBorder"].ToString();
                        if (jo["primeOperator"].ToString() != String.Empty)
                        {
                            detail.PrimeOperator = jo["primeOperator"].ToString();
                            if (detail.PrimeOperator == "－")
                            {
                                detail.PrimeMinValue = Convert.ToDecimal(jo["primeMinValue"].ToString());
                                detail.InputPrimeMinValue = jo["primeMinValue"].ToString();
                                detail.PrimeIncludeMinBorder = jo["primeIncludeMinBorder"].ToString();
                            }
                            detail.PrimeMaxValue = Convert.ToDecimal(jo["primeMaxValue"].ToString());
                            detail.InputPrimeMaxValue = jo["primeMaxValue"].ToString();
                            detail.PrimeIncludeMaxBorder = jo["primeIncludeMaxBorder"].ToString();
                        }
                        #region 写入数字显示值
                        //合格品
                        if (detail.GoodOperator == "－")
                        {
                            detail.GoodDisplayValue = detail.InputGoodMinValue + detail.GoodOperator + detail.InputGoodMaxValue;
                        }
                        if (detail.GoodOperator == ">")
                        {
                            if (detail.GoodIncludeMaxBorder == "1")
                            {
                                detail.GoodDisplayValue = "≥" + detail.InputGoodMaxValue;
                            }
                            else
                            {
                                detail.GoodDisplayValue = detail.GoodOperator + detail.InputGoodMaxValue;
                            }
                        }
                        if (detail.GoodOperator == "<")
                        {
                            if (detail.GoodIncludeMaxBorder == "1")
                            {
                                detail.GoodDisplayValue = "≤" + detail.InputGoodMaxValue;
                            }
                            else
                            {
                                detail.GoodDisplayValue = detail.GoodOperator + detail.InputGoodMaxValue;
                            }
                        }
                        //一级品
                        if (detail.PrimeOperator == "－")
                        {
                            detail.PrimeDisplayValue = detail.InputPrimeMinValue + detail.PrimeOperator + detail.InputPrimeMaxValue;
                        }
                        if (detail.PrimeOperator == ">")
                        {
                            if (detail.PrimeIncludeMaxBorder == "1")
                            {
                                detail.PrimeDisplayValue = "≥" + detail.InputPrimeMaxValue;
                            }
                            else
                            {
                                detail.PrimeDisplayValue = detail.PrimeOperator + detail.InputPrimeMaxValue;
                            }
                        }
                        if (detail.PrimeOperator == "<")
                        {
                            if (detail.PrimeIncludeMaxBorder == "1")
                            {
                                detail.PrimeDisplayValue = "≤" + detail.InputPrimeMaxValue;
                            }
                            else
                            {
                                detail.PrimeDisplayValue = detail.PrimeOperator + detail.InputPrimeMaxValue;
                            }
                        }
                        #endregion
                        break;
                    default:
                        break;
                }
                detailIdPointer++;
                addList.Add(detail);
            }
            else
            {
                updateList.Add(detail);
            }
        }
        int importedCount = 0;
        int failedCount = 0;
        //插入新建的指标
        foreach (QmcCheckItemDetail detail in addList)
        {
            try
            {
                detailManager.Insert(detail);
            }
            catch (Exception)
            {
                failedCount++;
                continue;
            }
            importedCount++;
        }
        //更新原有的指标
        foreach (QmcCheckItemDetail detail in updateList)
        {
            try
            {
                detailManager.Update(detail);
            }
            catch (Exception)
            {
                failedCount++;
                continue;
            }
            importedCount++;
        }
        StoreCenterAll.RemoveAll();
        StoreCenterCorrupted.RemoveAll();
        StoreCenterValid.RemoveAll();
        this.AppendWebLog("检测指标导入", "导入数目：" + importedCount);
        X.Msg.Alert("成功", "保存成功" + "<br />成功保存检测指标: " + importedCount.ToString() + "条，失败" + failedCount.ToString() + "条！").Show();
    }

    #endregion
}