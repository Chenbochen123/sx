using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using Mesnac.Business.Interface;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_ProducingPlan_PlanEntering_ImportPlan : Mesnac.Web.UI.Page
{


    #region 属性注入
        IBasMaterialManager basMaterialManager = new BasMaterialManager();
        IBasEquipManager basEquipManager = new BasEquipManager();
        IPmtRecipeManager pmtRecipeManager = new PmtRecipeManager();
        IPptPlanMgrManager planMgrManager = new PptPlanMgrManager();
    #endregion

    #region 初始化方法
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            #region 加载CSS样式
            System.Web.UI.HtmlControls.HtmlGenericControl cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/ext-chinese-font.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式
            #region 加载JS文件
            System.Web.UI.HtmlControls.HtmlGenericControl scriptLink = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            scriptLink.Attributes.Add("type", "text/javascript");
            scriptLink.Attributes.Add("src", "ImportPlan.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件
        }
    }
    #endregion
   
    #region 选择Excel文件
    
    /// <summary>
    /// 选择Excel文件
    /// </summary>
    /// <returns></returns>
    [DirectMethod]
    public bool SelectExcel()
    {
        ComboBoxNorthExcelSheets.GetStore().RemoveAll();
        ComboBoxNorthExcelSheets.Clear();

        if (FileUploadFieldNorthExcel.HasFile == false
            || FileUploadFieldNorthExcel.PostedFile == null
            || FileUploadFieldNorthExcel.PostedFile.ContentLength == 0)
        {
            return false;
        }

        using (MemoryStream ms = new MemoryStream(this.FileUploadFieldNorthExcel.FileBytes))
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

            int numberOfSheets = workbook.NumberOfSheets;
            for (int sheetIndex = 0; sheetIndex < numberOfSheets; sheetIndex++)
            {
                ComboBoxNorthExcelSheets.AddItem(Convert.ToString(sheetIndex + 1) + ":" + workbook.GetSheetName(sheetIndex), sheetIndex.ToString());
            }
        }

        return true;
    }
    #endregion

    #region 上传Excel文件
    
    /// <summary>
    /// 上传Excel文件
    /// </summary>
    /// <returns></returns>
    [DirectMethod]
    public bool UploadExcel()
    {
        if (FileUploadFieldNorthExcel.HasFile == false
            || FileUploadFieldNorthExcel.PostedFile == null
            || FileUploadFieldNorthExcel.PostedFile.ContentLength == 0)
        {
            X.Msg.Alert("提示", "没有选择上传文件或文件不存在".ToString()).Show();
            return false;
        }
        if (ComboBoxNorthExcelSheets.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择Sheet页").Show();
            return false;
        }
        if (DateFieldNorthPlanDate.RawText == null || DateFieldNorthPlanDate.RawText == "")
        {
            X.Msg.Alert("提示", "请选择生产计划日期").Show();
            return false;
        }
        // Excel文件读取
        using (MemoryStream ms = new MemoryStream(this.FileUploadFieldNorthExcel.FileBytes))
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
            //对比功能页面选取的日期和Excel中的计划日期
            int sheetIndex = Convert.ToInt32(ComboBoxNorthExcelSheets.Value);//sheetIndex Sheet页序号
            string planDateExcel = "";
            planDateExcel = workbook.GetSheetAt(sheetIndex).GetRow(1).GetCell(7, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
            try
            {
                if (DateFieldNorthPlanDate.RawValue.ToString() != DateTime.Parse(planDateExcel).ToString("yyyy-MM-dd"))
                {
                    X.Msg.Alert("提示", "生产计划日期与Excel中计划日期不对应").Show();
                    return false;
                }
            }
            catch (Exception)
            {
                X.Msg.Alert("提示", "上传Excel中计划日期不是有效格式").Show();
                return false;
            }

            //将原Excel文件导入到DataTable中
            DataTable dtWorkbook = CreateWorkbookTable();
            SetWorkbookTable(dtWorkbook, workbook.GetSheetAt(sheetIndex));//填充所有数据

            DataTable dtTemp = CreateTempTable();
            SetTempTable_1(dtWorkbook, dtTemp);//填充保存数据

            dtWorkbook.DefaultView.Sort = "seq";
            StoreCenterOrigin.DataSource = dtWorkbook;
            StoreCenterOrigin.DataBind();

            StoreCenterSave.DataSource = dtTemp;
            StoreCenterSave.DataBind();

            DataTable dtError = dtWorkbook.Copy();
            dtError.DefaultView.RowFilter = "flag='1'";
            StoreCenterError.DataSource = dtError;
            StoreCenterError.DataBind();

            string guid = Guid.NewGuid().ToString();
            HiddenGUID.SetValue(guid);

            string tpl = "文件上传完毕: {0}<br/>文件大小: {1} 字节<br/>Excel文件数据：{2} 条";
            X.Msg.Alert("成功", string.Format(tpl, this.FileUploadFieldNorthExcel.PostedFile.FileName
                , this.FileUploadFieldNorthExcel.PostedFile.ContentLength
                , dtWorkbook.Rows.Count.ToString())
            ).Show();
        }
        return true;
    }
    #endregion

    #region 创建表格及填充表格
    /// <summary>
    /// 创建Excel表格
    /// </summary>
    /// <returns></returns>
    private DataTable CreateWorkbookTable()
    {
        DataTable dtWorkbook = new DataTable();
        dtWorkbook.Columns.Add(new DataColumn("EquipName"));//设备名称
        dtWorkbook.Columns.Add(new DataColumn("MaterialName"));//物料名称
        dtWorkbook.Columns.Add(new DataColumn("Morning")); //早班
        dtWorkbook.Columns.Add(new DataColumn("Noon")); //中班
        dtWorkbook.Columns.Add(new DataColumn("Night")); //夜班
        dtWorkbook.Columns.Add(new DataColumn("seq", typeof(int)));
        dtWorkbook.Columns.Add(new DataColumn("flag"));
        dtWorkbook.Columns.Add(new DataColumn("errmsg"));
        dtWorkbook.Columns.Add(new DataColumn("TypeName"));
        return dtWorkbook;
    }

    /// <summary>
    /// 填充Excel表格(标准)
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="sheet"></param>
    private void SetWorkbookTable(DataTable dtWorkbook, ISheet sheet)
    {
        int seq = 0;
        int firstRowNum = 4; //从第5行开始
        int lastRowNum = sheet.LastRowNum;
        string tempEquipName = "";
        string tempMaterialName = "";
        string tempNoon = "";
        string tempNight = "";
        string tempMorning = "";
        string TypeName = "";
        for (int rowNum = firstRowNum; rowNum <= lastRowNum; rowNum++)
        {
            IRow row = sheet.GetRow(rowNum);

            if (row != null && row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
            {
                string materialName = row.GetCell(1).ToString();
                string equipName = row.GetCell(0).ToString();
                if (materialName.Trim() != "")
                {
                    seq++;
                    if (equipName.Trim() != "")
                    {
                        tempEquipName = row.GetCell(0, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                    }
                    tempMaterialName = row.GetCell(1, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                    tempNoon = row.GetCell(2, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                    tempNight = row.GetCell(4, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                    tempMorning = row.GetCell(6, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                    TypeName = row.GetCell(9, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                    DataRow drWorkbook = dtWorkbook.NewRow();
                    drWorkbook["EquipName"] = tempEquipName;
                    drWorkbook["MaterialName"] = tempMaterialName;
                    drWorkbook["Noon"] = tempNoon.Trim() != "" ? tempNoon : "0";
                    drWorkbook["Night"] = tempNight.Trim() != "" ? tempNight : "0";
                    drWorkbook["Morning"] = tempMorning.Trim() != "" ? tempMorning : "0";
                    drWorkbook["seq"] = seq.ToString();
                    drWorkbook["TypeName"] = TypeName.ToString();
                    dtWorkbook.Rows.Add(drWorkbook);

                }
            }
        }
    }


    /// <summary>
    /// 创建提交保存数据的表格
    /// 修改标识：qusf 20131022
    ///          
    /// </summary>
    /// <returns></returns>
    private DataTable CreateTempTable()
    {
        DataTable dtTemp = new DataTable();
        dtTemp.Columns.Add(new DataColumn("PlanDate"));
        dtTemp.Columns.Add(new DataColumn("EquipCode"));
        dtTemp.Columns.Add(new DataColumn("EquipName"));
        dtTemp.Columns.Add(new DataColumn("MaterialCode"));
        dtTemp.Columns.Add(new DataColumn("MaterialName"));
        dtTemp.Columns.Add(new DataColumn("RecipeName"));

        dtTemp.Columns.Add(new DataColumn("Morning")); //早班
        dtTemp.Columns.Add(new DataColumn("Noon")); //中班
        dtTemp.Columns.Add(new DataColumn("Night")); //夜班

        dtTemp.Columns.Add(new DataColumn("seq", typeof(int)));
        dtTemp.Columns.Add(new DataColumn("TypeName"));
        return dtTemp;

    }

    /// <summary>
    /// 填充提交保存数据的表格(标准)
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="dtTemp"></param>
    /// <param name="mBasMaterialList"></param>
    /// <param name="planDate"></param>
    /// <param name="shiftId"></param>
    /// <param name="shiftName"></param>
    /// <param name="shiftClass"></param>
    /// <param name="className"></param>
    private void SetTempTable_1(DataTable dtWorkbook, DataTable dtTemp)
    {
        int rowCount = dtWorkbook.Rows.Count;
        int seq = 0;
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            #region 取值及数据校验
            bool flag = true;
            DataRow drWorkbook = dtWorkbook.Rows[rowIndex];

            string dataMorning = drWorkbook["Morning"].ToString();
            string dataNoon = drWorkbook["Noon"].ToString();
            string dataNight = drWorkbook["Night"].ToString();
            string dataEquipCode = "";
            string dataMaterialCode = "";
            string dataRecipeName = "";
            string dataEquipName = drWorkbook["EquipName"].ToString();
            string dataMaterialName = drWorkbook["MaterialName"].ToString();
            string dataTypeName = "";
            string dataTypeID = "";
            EntityArrayList<BasMaterial> materialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialName == dataMaterialName.Trim()
                    && BasMaterial._.DeleteFlag == "0");
            EntityArrayList<BasEquip> equipList = basEquipManager.GetListByWhere(BasEquip._.EquipName == dataEquipName.Trim()
                    && BasEquip._.DeleteFlag == "0");
            //校验设备名称
            if (equipList.Count == 0)
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "设备名称不符合规范";
                continue;
            }
            else
            {
                dataEquipCode = equipList[0].EquipCode;
            }
            //校验物料名称
            if (materialList.Count == 0)
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "物料名称不符合规范";
                continue;
            }
            else
            {
                dataMaterialCode = materialList[0].MaterialCode;
            }
            //校验配方名称
            if (string.IsNullOrEmpty(drWorkbook["TypeName"].ToString()))
            {
                EntityArrayList<PmtRecipe> recipeList = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeEquipCode == dataEquipCode.Trim()
                        && PmtRecipe._.RecipeMaterialCode == dataMaterialCode.Trim()
                        && PmtRecipe._.RecipeState == "1"
                        && PmtRecipe._.RecipeType != "3");
                if (recipeList.Count == 0)
                {
                    drWorkbook["flag"] = "1";
                    drWorkbook["errmsg"] = "不存在正用的正常配方信息";
                    continue;
                }
                else
                {
                    dataRecipeName = recipeList[0].RecipeName;
                }
            }
            else
            {
                String sql = "select * from syscode where typeid = 'PmtType' and itemname = '" + drWorkbook["TypeName"].ToString() + "'";
                DataSet ds = pmtRecipeManager.GetBySql(sql).ToDataSet();

                if (ds.Tables[0].Rows.Count == 0)
                {
                    drWorkbook["flag"] = "1";
                    drWorkbook["errmsg"] = "配方类型不存在";
                    continue;
                }
                else
                { dataTypeName = ds.Tables[0].Rows[0]["ItemName"].ToString();

                dataTypeID = ds.Tables[0].Rows[0]["ItemCode"].ToString();
                }

                EntityArrayList<PmtRecipe> recipeList = pmtRecipeManager.GetListByWhere(PmtRecipe._.RecipeEquipCode == dataEquipCode.Trim()
                       && PmtRecipe._.RecipeMaterialCode == dataMaterialCode.Trim()
                       && PmtRecipe._.RecipeState == "1"
                       && PmtRecipe._.RecipeType == dataTypeID);
                if (recipeList.Count == 0)
                {
                    drWorkbook["flag"] = "1";
                    drWorkbook["errmsg"] = "不存在正用的" + dataTypeName + "配方信息";
                    continue;
                }
                else
                {
                    dataRecipeName = recipeList[0].RecipeName;
                }



            }



            //校验早中夜数量格式正确性
            string pattern = @"^(0|([1-9]\d{0,2}))$";
            if (!Regex.IsMatch(dataMorning, pattern))
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "计划数量格式错误";
                continue;
            }
            if (!Regex.IsMatch(dataNoon, pattern))
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "计划数量格式错误";
                continue;
            }
            if (!Regex.IsMatch(dataNight, pattern))
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "计划数量格式错误";
                continue;
            }
            if ("0".Equals(dataMorning) && "0".Equals(dataNoon) && "0".Equals(dataNight))
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "中、夜、早班计划都为0的计划不予导入";
                continue;
            }


            if (flag == false)
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "上传的标准数据不符合要求";
                continue;
            }

            #endregion

            seq++;
            DataRow drTemp = dtTemp.NewRow();
            drTemp["PlanDate"] = DateFieldNorthPlanDate.RawValue.ToString();
            drTemp["EquipName"] = dataEquipName;
            drTemp["MaterialName"] = dataMaterialName;
            drTemp["EquipCode"] = dataEquipCode;
            drTemp["MaterialCode"] = dataMaterialCode;
            drTemp["RecipeName"] = dataRecipeName;
            drTemp["Morning"] = dataMorning;
            drTemp["Noon"] = dataNoon;
            drTemp["Night"] = dataNight;
            drTemp["seq"] = seq;
            drTemp["TypeName"] = dataTypeName;
            dtTemp.Rows.Add(drTemp);
        }
    }
    #endregion

    #region 保存可以提交保存的数据
    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void StoreCenterSave_SubmitData(object sender, StoreSubmitDataEventArgs e)
    {
        List<JsonObject> joList = e.Object<JsonObject>();

        if (joList.Count == 0)
        {
            X.Msg.Alert("提示", "没有需要提交保存的计划信息").Show();
            return;
        }
        foreach (JsonObject jo in joList)
        {
            PptPlanMgr planMgr = new PptPlanMgr();
            planMgr.PlanDate = DateTime.Parse(jo["PlanDate"].ToString());
            planMgr.EquipCode = jo["EquipCode"].ToString();
            planMgr.MaterialCode = jo["MaterialCode"].ToString();
            planMgr.ERPCode = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == jo["MaterialCode"].ToString())[0].ERPCode;
            planMgr.MaterialName = jo["MaterialName"].ToString();
            planMgr.RecipeName = jo["RecipeName"].ToString();
            planMgr.ActualOnePlan = Convert.ToInt32(jo["Noon"].ToString());
            planMgr.ActualTwoPlan = Convert.ToInt32(jo["Night"].ToString());
            planMgr.ActualThreePlan = Convert.ToInt32(jo["Morning"].ToString());
            planMgr.Auditor = this.UserID;
            planMgr.AuditDate = DateTime.Now;
            planMgr.AuditFlag = "1";
            planMgr.CreatePlanFlag = "0";
            planMgr.DeleteFlag = "0";
            planMgr.AddFlag = "1";
            planMgr.Remark = "计划导入人：" + this.UserID + " , " + "计划导入时间：" + DateTime.Now.ToString("yyyy-MM-dd");
            planMgrManager.Insert(planMgr);
        }
        StoreCenterOrigin.RemoveAll();
        StoreCenterError.RemoveAll();
        StoreCenterSave.RemoveAll();


        X.Msg.Alert("成功", "保存成功" + "<br />共保存计划数据: " + joList.Count).Show();

    }
    #endregion

}