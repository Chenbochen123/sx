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
public partial class Manager_Equipment_EquipRepairProtectPlan_ImportRepairProtectPlan : Mesnac.Web.UI.Page
{
    #region 属性注入
    IBasEquipManager basEquipManager = new BasEquipManager();
    ISysCodeManager syscodeManager = new SysCodeManager();
    IBasUserManager userManager = new BasUserManager();
    IEqmRepairProtectPlanManager planManager = new EqmRepairProtectPlanManager();
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
            scriptLink.Attributes.Add("src", "ImportRepairProtectPlan.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件
            #region 加载计划类型下拉框数据
            EntityArrayList<SysCode> typelist = syscodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanType");
            foreach (SysCode sysCode in typelist)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem(sysCode.ItemName, sysCode.ItemCode);
                ComboBoxPlanType.Items.Add(item);
            }
            #endregion
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
            X.Msg.Alert("提示", "请选择计划月份").Show();
            return false;
        }
        if (ComboBoxPlanType.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择计划类型").Show();
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
            string planMonthExcel = "";
            planMonthExcel = workbook.GetSheetAt(sheetIndex).GetRow(1).GetCell(5, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
            try
            {
                if (DateFieldNorthPlanDate.RawValue.ToString() != DateTime.Parse(planMonthExcel).ToString("yyyy-MM"))
                {
                    X.Msg.Alert("提示", "计划月份与Excel中计划月份不对应").Show();
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
        dtWorkbook.Columns.Add(new DataColumn("RepairProtectPlanContent"));//计划内容
        dtWorkbook.Columns.Add(new DataColumn("RepairTime")); //检修时间
        dtWorkbook.Columns.Add(new DataColumn("RepairDate")); //检修日期
        dtWorkbook.Columns.Add(new DataColumn("NeedStopTime")); //需要停机时间
        dtWorkbook.Columns.Add(new DataColumn("PlanStopTime")); //计划停机日期
        dtWorkbook.Columns.Add(new DataColumn("ResponseUser")); //负责人
        dtWorkbook.Columns.Add(new DataColumn("seq", typeof(int)));
        dtWorkbook.Columns.Add(new DataColumn("flag"));
        dtWorkbook.Columns.Add(new DataColumn("errmsg"));

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
        string tempRepairProtectPlanContente = "";
        string tempRepairTime = "";
        string tempRepairDate = "";
        string tempResponseUser = "";
        string tempNeedStopTime = "";
        string tempPlanStopTime = "";
        for (int rowNum = firstRowNum; rowNum <= lastRowNum; rowNum++)
        {
            IRow row = sheet.GetRow(rowNum);
            seq++;
            if (row != null && row.GetCell(1, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
            {
                string equipName = row.GetCell(0).ToString();
                if (equipName.Trim() != "")
                {
                    tempEquipName = row.GetCell(0, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                }
                tempRepairDate = row.GetCell(1, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                tempRepairTime = row.GetCell(2, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                tempNeedStopTime = row.GetCell(3, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                tempPlanStopTime = row.GetCell(4, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                tempResponseUser = row.GetCell(5, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                tempRepairProtectPlanContente = row.GetCell(6, MissingCellPolicy.CREATE_NULL_AS_BLANK).ToString();
                DataRow drWorkbook = dtWorkbook.NewRow();
                drWorkbook["EquipName"] = tempEquipName;
                drWorkbook["RepairProtectPlanContent"] = tempRepairProtectPlanContente;
                drWorkbook["RepairTime"] = tempRepairTime.Trim() != "" ? tempRepairTime : "0";
                drWorkbook["RepairDate"] = tempRepairDate;
                drWorkbook["ResponseUser"] = tempResponseUser;
                drWorkbook["NeedStopTime"] = tempNeedStopTime.Trim() != "" ? tempNeedStopTime : "0";
                drWorkbook["PlanStopTime"] = tempPlanStopTime;
                drWorkbook["seq"] = seq.ToString();
                dtWorkbook.Rows.Add(drWorkbook);
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
        dtTemp.Columns.Add(new DataColumn("EquipName"));//设备名称
        dtTemp.Columns.Add(new DataColumn("RepairProtectPlanContent"));//计划内容
        dtTemp.Columns.Add(new DataColumn("RepairTime"));//检修时间
        dtTemp.Columns.Add(new DataColumn("RepairDate"));//检修日期

        dtTemp.Columns.Add(new DataColumn("ResponseUser")); //负责人
        dtTemp.Columns.Add(new DataColumn("NeedStopTime")); //需要停机时间
        dtTemp.Columns.Add(new DataColumn("PlanStopTime")); //计划停机日期

        dtTemp.Columns.Add(new DataColumn("seq", typeof(int)));

        return dtTemp;

    }

    /// <summary>
    /// 填充提交保存数据的表格(标准)
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="dtTemp"></param>
    private void SetTempTable_1(DataTable dtWorkbook, DataTable dtTemp)
    {
        int rowCount = dtWorkbook.Rows.Count;
        int seq = 0;
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            #region 取值及数据校验
            bool flag = true;
            DataRow drWorkbook = dtWorkbook.Rows[rowIndex];

            string dataEquipName = drWorkbook["EquipName"].ToString();
            string dataRepairProtectPlanContent = drWorkbook["RepairProtectPlanContent"].ToString();
            string dataRepairTime = drWorkbook["RepairTime"].ToString();
            string dataRepairDate = drWorkbook["RepairDate"].ToString();
            string dataResponseUser = drWorkbook["ResponseUser"].ToString();
            string dataNeedStopTime = drWorkbook["NeedStopTime"].ToString();
            string dataPlanStopTime = drWorkbook["PlanStopTime"].ToString();
            //校验设备名称
            EntityArrayList<BasEquip> equipList = basEquipManager.GetListByWhere(BasEquip._.EquipName == dataEquipName.Trim()
                    && BasEquip._.DeleteFlag == "0");
            if (equipList.Count == 0)
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "设备名称不符合规范";
                continue;
            }
            //校验检修日期
            try
            {
                DateTime tempDt = Convert.ToDateTime(dataRepairDate);
                DateTime setDt = Convert.ToDateTime(DateFieldNorthPlanDate.Value);
                if (tempDt.Month != setDt.Month)
                {
                    drWorkbook["flag"] = "1";
                    drWorkbook["errmsg"] = "检修日期月份与计划月份不符";
                    continue;
                }
            }
            catch
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "检修日期不符合规范";
                continue;
            }
            //校验负责人
            if (dataResponseUser.Length < 6)
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "负责人不符合规范";
                continue;
            }else
            {
                string workBarcode = dataResponseUser.Substring(dataResponseUser.Length - 6, 6);
                string username = dataResponseUser.Substring(0, dataResponseUser.Length - 6);
                EntityArrayList<BasUser> userList = userManager.GetListByWhere(BasUser._.UserName == username && BasUser._.WorkBarcode == workBarcode);
                if (userList.Count < 1) {
                    drWorkbook["flag"] = "1";
                    drWorkbook["errmsg"] = "负责人在人员信息中不存在";
                    continue;
                }
            }
            //校验计划停机日期
            EntityArrayList<SysCode> sysCodeList = syscodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanStopTime" && SysCode._.ItemName == dataPlanStopTime);
            if (sysCodeList.Count < 1)
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "计划停车日期不符合规范";
                continue;
            }
            //校验检修时间和需要停机时间的数字格式的正确性
            string pattern = @"^(0|([1-9]\d{0,2}))$";
            if (!Regex.IsMatch(dataRepairTime, pattern))
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "检修时间格式错误";
                continue;
            }
            if (!Regex.IsMatch(dataNeedStopTime, pattern))
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "需要停机时间格式错误";
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
            drTemp["EquipName"] = dataEquipName;
            drTemp["RepairProtectPlanContent"] = dataRepairProtectPlanContent;
            drTemp["RepairTime"] = dataRepairTime;
            drTemp["RepairDate"] = dataRepairDate;
            drTemp["ResponseUser"] = dataResponseUser;
            drTemp["NeedStopTime"] = dataNeedStopTime;
            drTemp["PlanStopTime"] = dataPlanStopTime;
            drTemp["seq"] = seq;

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
            EqmRepairProtectPlan plan = new EqmRepairProtectPlan();
            plan.EquipCode = basEquipManager.GetListByWhere(BasEquip._.EquipName == jo["EquipName"].ToString())[0].EquipCode;
            plan.RepairProtectPlanContent = jo["RepairProtectPlanContent"].ToString();
            plan.RepairDate = Convert.ToDateTime(jo["RepairDate"].ToString());
            plan.RepairTime = Convert.ToInt32(jo["RepairTime"].ToString());
            plan.ResponseUser = jo["ResponseUser"].ToString().Substring(jo["ResponseUser"].ToString().Length - 6, 6);
            plan.NeedStopTime = Convert.ToInt32(jo["NeedStopTime"].ToString());

            EntityArrayList<SysCode> planStopTimeList = syscodeManager.GetListByWhere(SysCode._.TypeID == "ProtectPlanStopTime" && SysCode._.ItemName == jo["PlanStopTime"]);
            plan.PlanStopTime = planStopTimeList[0].ItemCode;

            plan.PlanName = ComboBoxPlanType.SelectedItem.Value.ToString();
            plan.PlanMonth = DateFieldNorthPlanDate.Value.ToString();
            plan.DeleteFlag = "0";

            plan.Remark = "设备维护保养计划导入人：" + this.UserID + " , " + "设备维护保养计划导入时间：" + DateTime.Now.ToString("yyyy-MM-dd");
            planManager.Insert(plan);
        }
        StoreCenterOrigin.RemoveAll();
        StoreCenterError.RemoveAll();
        StoreCenterSave.RemoveAll();


        X.Msg.Alert("成功", "保存成功" + "<br />共保存计划数据: " + joList.Count).Show();

    }
    #endregion
}