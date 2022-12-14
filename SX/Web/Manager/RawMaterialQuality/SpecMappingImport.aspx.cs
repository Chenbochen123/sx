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

public partial class Manager_RawMaterialQuality_SpecMappingImport : BasePage
{
    #region 属性注入
    protected IQmcSpecManager specManager = new QmcSpecManager();
    protected IQmcSpecMappingManager mappingManager = new QmcSpecMappingManager();
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
    #endregion

    #region 点击增删改按钮激发的事件
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

            SetAllDataTable(dtAllData, workbook);
            SetValidDataTable(dtValidData, workbook);
            SetCorruptedDataTable(dtCorruptedData, workbook);

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
        dtWorkbook.Columns.Add(new DataColumn("specId"));
        dtWorkbook.Columns.Add(new DataColumn("specName"));
        dtWorkbook.Columns.Add(new DataColumn("deleteFlag"));
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
    private void SetAllDataTable(DataTable dtAllData, IWorkbook workbook)
    {
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
                string specId = String.Empty;
                string specName = String.Empty;
                string remark = String.Empty;
                string deleteFlag = String.Empty;
                bool integrationFlag = true;//规格完整性标记
                bool matchFlag = true;//规格一致性标记
                bool legalFlag = true;//数据合法性标记
                QmcSpec spec = new QmcSpec();

                #region 检查规格映射模板的正确性

                //规格名称不为空
                if (row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    specName = row.GetCell(1).ToString();
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "规格名称为空";
                    }
                    else
                    {
                        errMsg += "/规格名称为空";
                    }
                }
                if (row.GetCell(2, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    if (row.GetCell(2).ToString() == "否")
                    {
                        deleteFlag = "否";
                    }
                    else if (row.GetCell(2).ToString() == "是")
                    {
                        deleteFlag = "是";
                    }
                    else
                    {
                        integrationFlag = false;
                        validFlag = "0";
                        if (errMsg == String.Empty)
                        {
                            errMsg = "启用标识不正确";
                        }
                        else
                        {
                            errMsg += "/启用标识不正确";
                        }
                    }
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "启用标识为空";
                    }
                    else
                    {
                        errMsg += "/启用标识为空";
                    }
                }
                if (integrationFlag)
                {
                    //规格与数据库一致
                    if (specManager.GetListByWhere((QmcSpec._.SpecName == specName) && (QmcSpec._.SeriesId == seriesId) && (QmcSpec._.DeleteFlag == "0")).Count > 0)
                    {
                        spec = specManager.GetListByWhere((QmcSpec._.SpecName == specName) && (QmcSpec._.SeriesId == seriesId) && (QmcSpec._.DeleteFlag == "0"))[0];
                        specId = spec.SpecId.ToString();
                    }
                    else
                    {
                        matchFlag = false;
                        validFlag = "0";
                        if (errMsg == String.Empty)
                        {
                            errMsg = "规格与数据库不一致";
                        }
                        else
                        {
                            errMsg += "/规格与数据库不一致";
                        }
                    }
                }
                if (integrationFlag && matchFlag)
                {
                    //输入数据的合法性
                    if (row.GetCell(3, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                    {
                        remark = row.GetCell(3).ToString();
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
                }
                #endregion

                #region 向datatable插入此条数据
                if (validFlag == "1")
                {
                    errMsg = "正常";
                }
                DataRow drAllData = dtAllData.NewRow();
                drAllData["rowNum"] = rowNum;
                drAllData["seriesId"] = seriesId;
                drAllData["seriesName"] = seriesName;
                drAllData["materialCode"] = materialCode;
                drAllData["materialName"] = materialName;
                drAllData["materialERPCode"] = materialERPCode;
                drAllData["specId"] = specId;
                drAllData["specName"] = specName;
                drAllData["deleteFlag"] = deleteFlag;
                drAllData["remark"] = remark;
                drAllData["validFlag"] = validFlag;
                drAllData["errMsg"] = errMsg;
                dtAllData.Rows.Add(drAllData);
                #endregion

                tableRowNum++;
            }
        }
    }

    /// <summary>
    /// 填充合法导入数据表
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="sheet"></param>
    private void SetValidDataTable(DataTable dtValidData, IWorkbook workbook)
    {
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
                string specId = String.Empty;
                string specName = String.Empty;
                string remark = String.Empty;
                string deleteFlag = String.Empty;
                bool integrationFlag = true;//规格完整性标记
                bool matchFlag = true;//规格一致性标记
                bool legalFlag = true;//数据合法性标记
                QmcSpec spec = new QmcSpec();

                #region 检查规格映射模板的正确性

                //规格名称不为空
                if (row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    specName = row.GetCell(1).ToString();
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "规格名称为空";
                    }
                    else
                    {
                        errMsg += "/规格名称为空";
                    }
                }
                if (row.GetCell(2, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    if (row.GetCell(2).ToString() == "否")
                    {
                        deleteFlag = "否";
                    }
                    else if (row.GetCell(2).ToString() == "是")
                    {
                        deleteFlag = "是";
                    }
                    else
                    {
                        integrationFlag = false;
                        validFlag = "0";
                        if (errMsg == String.Empty)
                        {
                            errMsg = "启用标识不正确";
                        }
                        else
                        {
                            errMsg += "/启用标识不正确";
                        }
                    }
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "启用标识为空";
                    }
                    else
                    {
                        errMsg += "/启用标识为空";
                    }
                }
                if (integrationFlag)
                {
                    //规格与数据库一致
                    if (specManager.GetListByWhere((QmcSpec._.SpecName == specName) && (QmcSpec._.SeriesId == seriesId) && (QmcSpec._.DeleteFlag == "0")).Count > 0)
                    {
                        spec = specManager.GetListByWhere((QmcSpec._.SpecName == specName) && (QmcSpec._.SeriesId == seriesId) && (QmcSpec._.DeleteFlag == "0"))[0];
                        specId = spec.SpecId.ToString();
                    }
                    else
                    {
                        matchFlag = false;
                        validFlag = "0";
                        if (errMsg == String.Empty)
                        {
                            errMsg = "规格与数据库不一致";
                        }
                        else
                        {
                            errMsg += "/规格与数据库不一致";
                        }
                    }
                }
                if (integrationFlag && matchFlag)
                {
                    //输入数据的合法性
                    if (row.GetCell(3, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                    {
                        remark = row.GetCell(3).ToString();
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
                }
                #endregion

                #region 向datatable插入此条数据
                if (validFlag == "1")
                {
                    errMsg = "正常";
                    DataRow drValidData = dtValidData.NewRow();
                    drValidData["rowNum"] = rowNum;
                    drValidData["seriesId"] = seriesId;
                    drValidData["seriesName"] = seriesName;
                    drValidData["materialCode"] = materialCode;
                    drValidData["materialName"] = materialName;
                    drValidData["materialERPCode"] = materialERPCode;
                    drValidData["specId"] = specId;
                    drValidData["specName"] = specName;
                    drValidData["deleteFlag"] = deleteFlag;  
                    drValidData["remark"] = remark;
                    drValidData["validFlag"] = validFlag;
                    drValidData["errMsg"] = errMsg;
                    dtValidData.Rows.Add(drValidData);
                }
                #endregion

                tableRowNum++;
            }
        }
    }

    /// <summary>
    /// 填充坏数据表
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="sheet"></param>
    private void SetCorruptedDataTable(DataTable dtCorruptedData, IWorkbook workbook)
    {
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
                string specId = String.Empty;
                string specName = String.Empty;
                string deleteFlag = String.Empty;
                string remark = String.Empty;
                bool integrationFlag = true;//检测指标完整性标记
                bool matchFlag = true;//检测指标一致性标记
                bool legalFlag = true;//数据合法性标记
                QmcSpec spec = new QmcSpec();

                #region 检查规格映射模板的正确性

                //规格名称不为空
                if (row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    specName = row.GetCell(1).ToString();
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "规格名称为空";
                    }
                    else
                    {
                        errMsg += "/规格名称为空";
                    }
                }
                if (row.GetCell(2, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    if (row.GetCell(2).ToString() == "否")
                    {
                        deleteFlag = "否";
                    }
                    else if (row.GetCell(2).ToString() == "是")
                    {
                        deleteFlag = "是";
                    }
                    else
                    {
                        integrationFlag = false;
                        validFlag = "0";
                        if (errMsg == String.Empty)
                        {
                            errMsg = "启用标识不正确";
                        }
                        else
                        {
                            errMsg += "/启用标识不正确";
                        }
                    }
                }
                else
                {
                    integrationFlag = false;
                    validFlag = "0";
                    if (errMsg == String.Empty)
                    {
                        errMsg = "启用标识为空";
                    }
                    else
                    {
                        errMsg += "/启用标识为空";
                    }
                }
                if (integrationFlag)
                {
                    //规格与数据库一致
                    if (specManager.GetListByWhere((QmcSpec._.SpecName == specName) && (QmcSpec._.SeriesId == seriesId) && (QmcSpec._.DeleteFlag == "0")).Count > 0)
                    {
                        spec = specManager.GetListByWhere((QmcSpec._.SpecName == specName) && (QmcSpec._.SeriesId == seriesId) && (QmcSpec._.DeleteFlag == "0"))[0];
                        specId = spec.SpecId.ToString();
                    }
                    else
                    {
                        matchFlag = false;
                        validFlag = "0";
                        if (errMsg == String.Empty)
                        {
                            errMsg = "规格与数据库不一致";
                        }
                        else
                        {
                            errMsg += "/规格与数据库不一致";
                        }
                    }
                }
                if (integrationFlag && matchFlag)
                {
                    //输入数据的合法性
                    if (row.GetCell(3, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                    {
                        remark = row.GetCell(3).ToString();
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
                }
                #endregion

                #region 向datatable插入此条数据
                if (validFlag == "1")
                {
                    errMsg = "正常";
                }
                else if (validFlag == "0")
                {
                    DataRow drCorruptedData = dtCorruptedData.NewRow();
                    drCorruptedData["rowNum"] = rowNum;
                    drCorruptedData["seriesId"] = seriesId;
                    drCorruptedData["seriesName"] = seriesName;
                    drCorruptedData["materialCode"] = materialCode;
                    drCorruptedData["materialName"] = materialName;
                    drCorruptedData["materialERPCode"] = materialERPCode;
                    drCorruptedData["specId"] = specId;
                    drCorruptedData["specName"] = specName;
                    drCorruptedData["deleteFlag"] = deleteFlag;
                    drCorruptedData["remark"] = remark;
                    drCorruptedData["validFlag"] = validFlag;
                    drCorruptedData["errMsg"] = errMsg;
                    dtCorruptedData.Rows.Add(drCorruptedData);
                }
                #endregion

                tableRowNum++;
            }
        }
    }

    /// <summary>
    /// 点击下载模板按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_download_Click(object sender, EventArgs e)
    {
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
        EntityArrayList<QmcSpec> specList = specManager.GetListByWhere((QmcSpec._.SeriesId == cbxSeriesName.Value) && (QmcSpec._.DeleteFlag == "0"));
        if (specList.Count == 0)
        {
            X.Msg.Alert("提示", "模板生成错误，该原材料系列无可用的规格项目！").Show();
            return;
        }
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
                sheet.SetColumnWidth(2, 3000);
                sheet.SetColumnWidth(3, 8000);
                // 创建第一行    
                IRow row0 = sheet.CreateRow(0);
                // 设置行高    
                row0.Height = ((short)500);
                // 创建表头列   
                ICell cell1 = row0.CreateCell(1);
                ICell cell2 = row0.CreateCell(2);
                ICell cell3 = row0.CreateCell(3);
                cell1.SetCellValue(new HSSFRichTextString("规格名称"));
                cell2.SetCellValue(new HSSFRichTextString("是否启用"));
                cell3.SetCellValue(new HSSFRichTextString("备注"));
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
            foreach (QmcSpec spec in specList)
            {
                // 创建规格数据行
                IRow dataRow = sheet.CreateRow(starter);
                // 设置行高    
                dataRow.Height = ((short)450);
                // 创建数据列
                ICell cell0 = dataRow.CreateCell(0);
                ICell cell1 = dataRow.CreateCell(1);
                ICell cell2 = dataRow.CreateCell(2);
                ICell cell3 = dataRow.CreateCell(3);
                cell0.SetCellValue(new HSSFRichTextString(starter.ToString()));
                cell1.SetCellValue(new HSSFRichTextString(spec.SpecName));
                cell2.SetCellValue(new HSSFRichTextString("否"));
                cell3.SetCellValue(new HSSFRichTextString(spec.Remark));
                //增加是否启用下拉菜单
                CellRangeAddressList activateSwitch = new CellRangeAddressList(starter, starter, 2, 2);
                DVConstraint constraintSwitch = DVConstraint.CreateExplicitListConstraint(new string[] { "否", "是" });
                HSSFDataValidation dataValidateSwitch = new HSSFDataValidation(activateSwitch, constraintSwitch);
                sheet.AddValidationData(dataValidateSwitch);
                starter++;//行计数器自增
            }
        }
        MemoryStream excelStream = new MemoryStream();
        file.Write(excelStream);
        new Mesnac.Util.Excel.ExcelDownload().FileDown(excelStream, "规格映射导入模板-" + minorType.MinorTypeName);
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void StoreCenterValid_SubmitData(object sender, StoreSubmitDataEventArgs e)
    {
        List<JsonObject> joList = e.Object<JsonObject>();
        if (joList.Count == 0)
        {
            X.Msg.Alert("提示", "没有需要提交保存的信息").Show();
            return;
        }
        EntityArrayList<QmcSpecMapping> addList = new EntityArrayList<QmcSpecMapping>();
        EntityArrayList<QmcSpecMapping> updateList = new EntityArrayList<QmcSpecMapping>();
        int mappingIdPointer = 0;
        foreach (JsonObject jo in joList)
        {
            QmcSpecMapping mapping = null;
            EntityArrayList<QmcSpec> specList = specManager.GetListByWhere((QmcSpec._.SeriesId == jo["seriesId"].ToString()) && (QmcSpec._.DeleteFlag == "0"));
            foreach (QmcSpec spec in specList)
            {
                if ((mappingManager.GetListByWhere((QmcSpecMapping._.MaterialCode == jo["materialCode"].ToString()) && (QmcSpecMapping._.SpecId == spec.SpecId)).Count > 0) && (spec.SpecName == jo["specName"].ToString()))
                {
                    mapping = mappingManager.GetListByWhere((QmcSpecMapping._.MaterialCode == jo["materialCode"].ToString()) && (QmcSpecMapping._.SpecId == spec.SpecId))[0];
                    if (jo["deleteFlag"].ToString() == "是")
                    {
                        mapping.DeleteFlag = "0";
                    }
                    else if (jo["deleteFlag"].ToString() == "否")
                    {
                        mapping.DeleteFlag = "1";
                    }
                    else
                    {
                        mapping.DeleteFlag = "1";
                    }
                    mapping.Remark = jo["remark"].ToString();
                }
            }
            if (mapping == null)
            {
                mapping = new QmcSpecMapping();
                mapping.MappingId = Convert.ToInt32(mappingManager.GetNextMappingId()) + mappingIdPointer;
                foreach (QmcSpec spec in specList)
                {
                    if (spec.SpecName == jo["specName"].ToString())
                    {
                        mapping.SpecId = spec.SpecId;
                        mapping.SpecName = spec.SpecName;
                        break;
                    }
                }
                if (jo["deleteFlag"].ToString() == "是")
                {
                    mapping.DeleteFlag = "0";
                }
                else if (jo["deleteFlag"].ToString() == "否")
                {
                    mapping.DeleteFlag = "1";
                }
                else
                {
                    mapping.DeleteFlag = "1";
                }
                mapping.Remark = jo["remark"].ToString();
                mapping.MaterialCode = jo["materialCode"].ToString();
                mappingIdPointer++;
                addList.Add(mapping);
            }
            else
            {
                updateList.Add(mapping);
            }
        }
        int importedCount = 0;
        int failedCount = 0;
        foreach (QmcSpecMapping mapping in addList)
        {
            try
            {
                mappingManager.Insert(mapping);
            }
            catch (Exception)
            {
                failedCount++;
                continue;
            }
            importedCount++;
        }
        foreach (QmcSpecMapping mapping in updateList)
        {
            try
            {
                mappingManager.Update(mapping);
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
        this.AppendWebLog("规格映射导入", "导入数目：" + importedCount);
        X.Msg.Alert("成功", "保存成功" + "<br />成功保存规格映射: " + importedCount.ToString() + "条，失败" + failedCount.ToString() + "条！").Show();
    }
    #endregion
}