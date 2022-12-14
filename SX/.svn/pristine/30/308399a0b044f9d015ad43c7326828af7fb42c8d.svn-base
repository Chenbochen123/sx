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

public partial class Manager_RawMaterialQuality_FactoryMappingImport : BasePage
{
    #region 属性注入
    protected IQmcFactoryMappingManager mappingManager = new QmcFactoryMappingManager();
    protected IBasFactoryInfoManager factoryManager = new BasFactoryInfoManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
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
        dtWorkbook.Columns.Add(new DataColumn("supplierId"));
        dtWorkbook.Columns.Add(new DataColumn("supplierName"));
        dtWorkbook.Columns.Add(new DataColumn("supplierERPCode"));
        dtWorkbook.Columns.Add(new DataColumn("manufacturerId"));
        dtWorkbook.Columns.Add(new DataColumn("manufacturerName"));
        dtWorkbook.Columns.Add(new DataColumn("manufacturerERPCode"));
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
        EntityArrayList<BasMaterialMinorType> seriesList = minorTypeManager.GetListByWhere((BasMaterialMinorType._.MajorID == "1") && (BasMaterialMinorType._.DeleteFlag == "0"));//获取所有系列
        EntityArrayList<QmcFactoryMapping> mappingList = mappingManager.GetListByWhere(QmcFactoryMapping._.DeleteFlag == "0");//获取所有对应关系
        int sheetIndex = 0;//Id自增
        foreach (BasMaterialMinorType series in seriesList)
        {
            //为每个系列建立一个Sheet页
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            int firstRowNum = 1; //从第2行开始
            int lastRowNum = sheet.LastRowNum;
            for (int rowNum = firstRowNum; rowNum <= lastRowNum; rowNum++)
            {
                //建立行
                IRow row = sheet.GetRow(rowNum);
                if (row != null && row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null && row.GetCell(2, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    string seriesId = series.MinorTypeID;
                    string seriesName = series.MinorTypeName;
                    string supplierId = String.Empty;
                    string supplierName = String.Empty;
                    string supplierERPCode = String.Empty;
                    string manufacturerId = String.Empty;
                    string manufacturerName = String.Empty;
                    string manufacturerERPCode = String.Empty;
                    string remark = String.Empty;
                    string validFlag = "1";
                    string errorMsg = String.Empty;
                    supplierERPCode = row.GetCell(1).ToString();
                    manufacturerERPCode = row.GetCell(2).ToString();
                    if (row.GetCell(3, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                    {
                        remark = row.GetCell(3).ToString();
                    }
                    else
                    {
                        remark = String.Empty;
                    }

                    #region 校验数据合法性
                    EntityArrayList<BasFactoryInfo> supplierList = factoryManager.GetListByWhere(BasFactoryInfo._.ERPCode == supplierERPCode && BasFactoryInfo._.DeleteFlag == "0");
                    EntityArrayList<BasFactoryInfo> manufacturerList = factoryManager.GetListByWhere(BasFactoryInfo._.ERPCode == manufacturerERPCode && BasFactoryInfo._.DeleteFlag == "0");
                    if (supplierList.Count > 0 && manufacturerList.Count > 0)
                    {
                        supplierId = supplierList[0].ObjID.ToString();
                        supplierName = supplierList[0].FacName;
                        manufacturerId = manufacturerList[0].ObjID.ToString();
                        manufacturerName = manufacturerList[0].FacName;
                        foreach (QmcFactoryMapping mapping in mappingList)
                        {
                            if (mapping.SeriesId == seriesId && mapping.SupplierERPCode == supplierERPCode && mapping.ManufacturerERPCode == manufacturerERPCode)
                            {
                                validFlag = "0";//之前数据库的数据中必须没有重复记录
                                errorMsg = "有重复的记录";
                                break;
                            }
                        }
                        if (dtAllData.Rows.Count > 0 && validFlag == "1")
                        {
                            foreach (DataRow tempRow in dtAllData.Rows)
                            {
                                if (tempRow["seriesId"].ToString() == seriesId && tempRow["supplierERPCode"].ToString() == supplierERPCode && tempRow["manufacturerERPCode"].ToString() == manufacturerERPCode)
                                {
                                    validFlag = "0";//之前的Excel数据中必须没有重复记录
                                    errorMsg = "有重复的记录";
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        validFlag = "0";//ERP代码对应的厂商必须存在
                        errorMsg = "厂商不存在";
                    }

                    if (remark.Length > 50)
                    {
                        validFlag = "0";//备注不能超过50字
                        if (errorMsg == String.Empty)
                        {
                            errorMsg = "备注超过50字";
                        }
                        else
                        {
                            errorMsg += "/备注超过50字";
                        }
                    }
                    #endregion

                    if (validFlag == "1")
                    {
                        errorMsg = "正常";
                    }
                    DataRow drAllData = dtAllData.NewRow();
                    drAllData["rowNum"] = rowNum;
                    drAllData["seriesId"] = seriesId;
                    drAllData["seriesName"] = seriesName;
                    drAllData["supplierId"] = supplierId;
                    drAllData["supplierName"] = supplierName;
                    drAllData["supplierERPCode"] = supplierERPCode;
                    drAllData["manufacturerId"] = manufacturerId;
                    drAllData["manufacturerName"] = manufacturerName;
                    drAllData["manufacturerERPCode"] = manufacturerERPCode;
                    drAllData["remark"] = remark;
                    drAllData["errMsg"] = errorMsg;
                    dtAllData.Rows.Add(drAllData);
                }
            }
            sheetIndex++;
        }
    }

    /// <summary>
    /// 填充合法导入数据表
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="sheet"></param>
    private void SetValidDataTable(DataTable dtValidData, IWorkbook workbook)
    {
        EntityArrayList<BasMaterialMinorType> seriesList = minorTypeManager.GetListByWhere((BasMaterialMinorType._.MajorID == "1") && (BasMaterialMinorType._.DeleteFlag == "0"));//获取所有系列
        EntityArrayList<QmcFactoryMapping> mappingList = mappingManager.GetListByWhere(QmcFactoryMapping._.DeleteFlag == "0");//获取所有对应关系
        int sheetIndex = 0;//Id自增
        foreach (BasMaterialMinorType series in seriesList)
        {
            //为每个系列建立一个Sheet页
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            int firstRowNum = 1; //从第2行开始
            int lastRowNum = sheet.LastRowNum;
            for (int rowNum = firstRowNum; rowNum <= lastRowNum; rowNum++)
            {
                //建立行
                IRow row = sheet.GetRow(rowNum);
                if (row != null && row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null && row.GetCell(2, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    string seriesId = series.MinorTypeID;
                    string seriesName = series.MinorTypeName;
                    string supplierId = String.Empty;
                    string supplierName = String.Empty;
                    string supplierERPCode = String.Empty;
                    string manufacturerId = String.Empty;
                    string manufacturerName = String.Empty;
                    string manufacturerERPCode = String.Empty;
                    string remark = String.Empty;
                    string validFlag = "1";
                    supplierERPCode = row.GetCell(1).ToString();
                    manufacturerERPCode = row.GetCell(2).ToString();
                    if (row.GetCell(3, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                    {
                        remark = row.GetCell(3).ToString();
                    }
                    else
                    {
                        remark = String.Empty;
                    }

                    #region 校验数据合法性
                    EntityArrayList<BasFactoryInfo> supplierList = factoryManager.GetListByWhere(BasFactoryInfo._.ERPCode == supplierERPCode && BasFactoryInfo._.DeleteFlag == "0");
                    EntityArrayList<BasFactoryInfo> manufacturerList = factoryManager.GetListByWhere(BasFactoryInfo._.ERPCode == manufacturerERPCode && BasFactoryInfo._.DeleteFlag == "0");
                    if (supplierList.Count > 0 && manufacturerList.Count > 0)
                    {
                        supplierId = supplierList[0].ObjID.ToString();
                        supplierName = supplierList[0].FacName;
                        manufacturerId = manufacturerList[0].ObjID.ToString();
                        manufacturerName = manufacturerList[0].FacName;
                        foreach (QmcFactoryMapping mapping in mappingList)
                        {
                            if (mapping.SeriesId == seriesId && mapping.SupplierERPCode == supplierERPCode && mapping.ManufacturerERPCode == manufacturerERPCode)
                            {
                                validFlag = "0";//之前数据库的数据中必须没有重复记录
                                break;
                            }
                        }
                        if (dtValidData.Rows.Count > 0 && validFlag == "1")
                        {
                            foreach (DataRow tempRow in dtValidData.Rows)
                            {
                                if (tempRow["seriesId"].ToString() == seriesId && tempRow["supplierERPCode"].ToString() == supplierERPCode && tempRow["manufacturerERPCode"].ToString() == manufacturerERPCode)
                                {
                                    validFlag = "0";//之前的Excel数据中必须没有重复记录
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        validFlag = "0";//ERP代码对应的厂商必须存在
                    }

                    if (remark.Length > 50)
                    {
                        validFlag = "0";//备注不能超过50字
                    }
                    #endregion

                    if (validFlag == "1")
                    {
                        DataRow drValidData = dtValidData.NewRow();
                        drValidData["rowNum"] = rowNum;
                        drValidData["seriesId"] = seriesId;
                        drValidData["seriesName"] = seriesName;
                        drValidData["supplierId"] = supplierId;
                        drValidData["supplierName"] = supplierName;
                        drValidData["supplierERPCode"] = supplierERPCode;
                        drValidData["manufacturerId"] = manufacturerId;
                        drValidData["manufacturerName"] = manufacturerName;
                        drValidData["manufacturerERPCode"] = manufacturerERPCode;
                        drValidData["remark"] = remark;
                        dtValidData.Rows.Add(drValidData);
                    }
                }
            }
            sheetIndex++;
        }
    }

    /// <summary>
    /// 填充坏数据表
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="sheet"></param>
    private void SetCorruptedDataTable(DataTable dtCorruptedData, IWorkbook workbook)
    {
        EntityArrayList<BasMaterialMinorType> seriesList = minorTypeManager.GetListByWhere((BasMaterialMinorType._.MajorID == "1") && (BasMaterialMinorType._.DeleteFlag == "0"));//获取所有系列
        EntityArrayList<QmcFactoryMapping> mappingList = mappingManager.GetListByWhere(QmcFactoryMapping._.DeleteFlag == "0");//获取所有对应关系
        int sheetIndex = 0;//Id自增
        foreach (BasMaterialMinorType series in seriesList)
        {
            //为每个系列建立一个Sheet页
            ISheet sheet = workbook.GetSheetAt(sheetIndex);
            int firstRowNum = 1; //从第2行开始
            int lastRowNum = sheet.LastRowNum;
            for (int rowNum = firstRowNum; rowNum <= lastRowNum; rowNum++)
            {
                IRow row = sheet.GetRow(rowNum);
                if (row != null && row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null && row.GetCell(2, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    string seriesId = series.MinorTypeID;
                    string seriesName = series.MinorTypeName;
                    string supplierId = String.Empty;
                    string supplierName = String.Empty;
                    string supplierERPCode = String.Empty;
                    string manufacturerId = String.Empty;
                    string manufacturerName = String.Empty;
                    string manufacturerERPCode = String.Empty;
                    string remark = String.Empty;
                    string validFlag = "1";
                    string errorMsg = String.Empty;
                    supplierERPCode = row.GetCell(1).ToString();
                    manufacturerERPCode = row.GetCell(2).ToString();
                    if (row.GetCell(3, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                    {
                        remark = row.GetCell(3).ToString();
                    }
                    else
                    {
                        remark = String.Empty;
                    }

                    #region 校验数据合法性
                    EntityArrayList<BasFactoryInfo> supplierList = factoryManager.GetListByWhere(BasFactoryInfo._.ERPCode == supplierERPCode && BasFactoryInfo._.DeleteFlag == "0");
                    EntityArrayList<BasFactoryInfo> manufacturerList = factoryManager.GetListByWhere(BasFactoryInfo._.ERPCode == manufacturerERPCode && BasFactoryInfo._.DeleteFlag == "0");
                    if (supplierList.Count > 0 && manufacturerList.Count > 0)
                    {
                        supplierId = supplierList[0].ObjID.ToString();
                        supplierName = supplierList[0].FacName;
                        manufacturerId = manufacturerList[0].ObjID.ToString();
                        manufacturerName = manufacturerList[0].FacName;
                        foreach (QmcFactoryMapping mapping in mappingList)
                        {
                            if (mapping.SeriesId == seriesId && mapping.SupplierERPCode == supplierERPCode && mapping.ManufacturerERPCode == manufacturerERPCode)
                            {
                                validFlag = "0";//之前数据库的数据中必须没有重复记录
                                errorMsg = "有重复的记录";
                                break;
                            }
                        }
                        if (dtCorruptedData.Rows.Count > 0 && validFlag == "1")
                        {
                            foreach (DataRow tempRow in dtCorruptedData.Rows)
                            {
                                if (tempRow["seriesId"].ToString() == seriesId && tempRow["supplierERPCode"].ToString() == supplierERPCode && tempRow["manufacturerERPCode"].ToString() == manufacturerERPCode)
                                {
                                    validFlag = "0";//之前的Excel数据中必须没有重复记录
                                    errorMsg = "有重复的记录";
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        validFlag = "0";//ERP代码对应的厂商必须存在
                        errorMsg = "厂商不存在";
                    }

                    if (remark.Length > 50)
                    {
                        validFlag = "0";//备注不能超过50字
                        if (errorMsg == String.Empty)
                        {
                            errorMsg = "备注超过50字";
                        }
                        else
                        {
                            errorMsg += "/备注超过50字";
                        }
                    }
                    #endregion

                    if (validFlag == "0")
                    {
                        DataRow drCorruptedData = dtCorruptedData.NewRow();
                        drCorruptedData["rowNum"] = rowNum;
                        drCorruptedData["seriesId"] = seriesId;
                        drCorruptedData["seriesName"] = seriesName;
                        drCorruptedData["supplierId"] = supplierId;
                        drCorruptedData["supplierName"] = supplierName;
                        drCorruptedData["supplierERPCode"] = supplierERPCode;
                        drCorruptedData["manufacturerId"] = manufacturerId;
                        drCorruptedData["manufacturerName"] = manufacturerName;
                        drCorruptedData["manufacturerERPCode"] = manufacturerERPCode;
                        drCorruptedData["remark"] = remark;
                        drCorruptedData["errMsg"] = errorMsg;
                        dtCorruptedData.Rows.Add(drCorruptedData);
                    }
                }
            }
            sheetIndex++;
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
        EntityArrayList<BasMaterialMinorType> seriesList = minorTypeManager.GetListByWhere((BasMaterialMinorType._.MajorID == "1") && (BasMaterialMinorType._.DeleteFlag == "0"));
        foreach (BasMaterialMinorType minorType in seriesList)
        {
            ISheet sheet = file.CreateSheet(minorType.MinorTypeID + "_" + minorType.MinorTypeName);
            sheet.CreateFreezePane(1, 1);// 冻结    
            // 设置列宽    
            sheet.SetColumnWidth(0, 1000);
            sheet.SetColumnWidth(1, 7000);
            sheet.SetColumnWidth(2, 7000);
            sheet.SetColumnWidth(3, 9000);
            // 创建第一行    
            IRow row0 = sheet.CreateRow(0);
            // 设置行高    
            row0.Height = ((short)400);
            // 创建第一列    
            ICell cell1 = row0.CreateCell(1);
            cell1.SetCellValue(new HSSFRichTextString("供应商ERP代码-必填"));
            ICell cell2 = row0.CreateCell(2);
            cell2.SetCellValue(new HSSFRichTextString("生产商ERP代码-必填"));
            ICell cell3 = row0.CreateCell(3);
            cell3.SetCellValue(new HSSFRichTextString("备注"));
        }
        MemoryStream excelStream = new MemoryStream();
        file.Write(excelStream);
        new Mesnac.Util.Excel.ExcelDownload().FileDown(excelStream, "厂商关系导入模板");
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
        EntityArrayList<QmcFactoryMapping> submitList = new EntityArrayList<QmcFactoryMapping>();
        int mappingIdPointer = 0;
        foreach (JsonObject jo in joList)
        {
            QmcFactoryMapping mapping = new QmcFactoryMapping();
            mapping.MappingId = Convert.ToInt32(mappingManager.GetNextMappingId()) + mappingIdPointer;
            mapping.SeriesId = jo["seriesId"].ToString();
            mapping.SeriesName = jo["seriesName"].ToString();
            mapping.SupplierId = Convert.ToInt32(jo["supplierId"]);
            mapping.SupplierName = jo["supplierName"].ToString();
            mapping.SupplierERPCode = jo["supplierERPCode"].ToString();
            mapping.ManufacturerId = Convert.ToInt32(jo["manufacturerId"]);
            mapping.ManufacturerName = jo["manufacturerName"].ToString();
            mapping.ManufacturerERPCode = jo["manufacturerERPCode"].ToString();
            mapping.Remark = jo["remark"].ToString();
            mapping.DeleteFlag = "0";
            submitList.Add(mapping);
            mappingIdPointer++;
        }
        int importedCount = 0;
        int failedCount = 0;
        foreach (QmcFactoryMapping mapping in submitList)
        {
            try
            {
                mappingManager.Insert(mapping);
            }
            catch(Exception ex)
            {
                failedCount++;
                continue;
            }
            importedCount++;
        }
        StoreCenterAll.RemoveAll();
        StoreCenterCorrupted.RemoveAll();
        StoreCenterValid.RemoveAll();
        this.AppendWebLog("厂商对应关系导入", "导入数目：" + importedCount);
        X.Msg.Alert("成功", "保存成功" + "<br />成功保存对应关系: " + importedCount.ToString() + "条，失败" + failedCount.ToString() + "条！").Show();
    }

    #endregion
}