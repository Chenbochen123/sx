using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;

using NBear.Common;

using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using Mesnac.Data.Implements;
using Mesnac.Data.Interface;

using Ext.Net;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public partial class Manager_RubberQuality_Manage_CheckRubberQualityCPKReport : Mesnac.Web.UI.BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            导出 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthExport" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

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
            scriptLink.Attributes.Add("src", "CheckRubberQualityCPKReport.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            InitControls();

            DateFieldNorthMonth.SetValue(DateTime.Today.ToString("yyyy-MM"));
            DateFieldNorthBeginDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldNorthEndDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));

            ComboBoxNorthCheckType.SetValueAndFireSelect("2");
        }
    }

    private void InitControls()
    {
        IBasEquipManager bBasEquipManager = new BasEquipManager();
        EntityArrayList<BasEquip> mBasEquipList = bBasEquipManager.GetListByWhereAndOrder(
            BasEquip._.DeleteFlag == "0"
            & BasEquip._.EquipType == "01"
            , BasEquip._.EquipName.Asc);

        foreach (BasEquip mBasEquip in mBasEquipList)
        {
            ComboBoxNorthEquipCode.AddItem(mBasEquip.EquipName, mBasEquip.EquipCode);
        }

        IBasMainHanderManager bBasMainHanderManager = new BasMainHanderManager();
        DataSet ds = bBasMainHanderManager.GetMixMainHanderInfo();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ComboBoxNorthZJSID.AddItem("[" + dr["MainHanderCode"].ToString() + "]" + dr["UserName"].ToString(), dr["MainHanderCode"].ToString());
        }

    }

    [DirectMethod(Timeout = 600000)]
    public void GetRubberQualityCPKReport(string otherMaterCodes)
    {
        if (HiddenNorthMaterCode.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择胶料").Show();
            X.Mask.Hide();
            return;
        }

        HiddenNorthCheckType.SetValue(ComboBoxNorthCheckType.Value.ToString());

        string materCode = HiddenNorthMaterCode.Value.ToString();
        string materName = TriggerFieldNorthMaterName.Value.ToString();

        string beginPlanDate = "";
        string endPlanDate = "";
        switch (RadioGroupNorthDateType.CheckedItems[0].InputValue.ToString())
        {
            case "1": //月份
                string month = DateFieldNorthMonth.RawText;
                beginPlanDate = month + "-01";
                endPlanDate = Convert.ToDateTime(month + "-01").AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                break;
            case "2":
                beginPlanDate = DateFieldNorthBeginDate.RawText;
                endPlanDate = DateFieldNorthEndDate.RawText;
                break;
            default:
                break;
        }

        string zjsID = ComboBoxNorthZJSID.Value.ToString();
        string equipCode = ComboBoxNorthEquipCode.Value.ToString();

        DataSet ds = null;
        string checkType = HiddenNorthCheckType.Value.ToString();
        if (checkType == "2")
        {
            IQmtCheckRubberQualityCPKReportParams paras = new QmtCheckRubberQualityCPKReportParams();
            paras.MaterCode = materCode;
            paras.BeginPlanDate = beginPlanDate;
            paras.EndPlanDate = endPlanDate;
            paras.ZJSID = zjsID;
            paras.EquipCode = equipCode;
            paras.OtherMaterCodes = otherMaterCodes;

            IQmtCheckMasterManager bQmtCheckMasterManager = new QmtCheckMasterManager();
            ds = bQmtCheckMasterManager.GetCheckRubberQualityCPKReportByParas(paras);
        }
        else if (checkType == "1")
        {
            IQmtCheckRubberAssessQualityCPKReportParams paras = new QmtCheckRubberAssessQualityCPKReportParams();
            paras.MaterCode = materCode;
            paras.BeginPlanDate = beginPlanDate;
            paras.EndPlanDate = endPlanDate;
            paras.ZJSID = zjsID;
            paras.EquipCode = equipCode;
            paras.OtherMaterCodes = otherMaterCodes;

            IQmtCheckAssessMasterManager bQmtCheckAssessMasterManager = new QmtCheckAssessMasterManager();
            ds = bQmtCheckAssessMasterManager.GetCheckRubberAssessQualityCPKReportByParas(paras);
        }
        else if (checkType == "3")
        {
            IQmtCheckRubberAssessQualityCPKReportParams paras = new QmtCheckRubberAssessQualityCPKReportParams();
            paras.MaterCode = materCode;
            paras.BeginPlanDate = beginPlanDate;
            paras.EndPlanDate = endPlanDate;
            paras.ZJSID = zjsID;
            paras.EquipCode = equipCode;
            paras.OtherMaterCodes = otherMaterCodes;

            IQmtCheckAssessMasterManager bQmtCheckAssessMasterManager = new QmtCheckAssessMasterManager();
            ds = GetCheckRubberAssessQualityCPKReportByParas(paras);
        }
        if (ds.Tables[1].Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有符合条件的记录").Show();
            X.Mask.Hide();
            return;
        }
        IWorkbook workbook = null;
        using (FileStream fs = new FileStream(Server.MapPath("CheckRubberQualityCPKReport.xls"), FileMode.Open, FileAccess.Read))
        {
            try
            {
                workbook = new HSSFWorkbook(fs);
            }
            catch
            {
                X.Msg.Alert("提示", "上传的文件不是有效的Excel文件").Show();
                X.Mask.Hide();
                return;
            }

        }

        ISheet sheet = workbook.GetSheetAt(0);
        sheet.GetRow(0).GetCell(0).SetCellValue(materName);

        #region 填充标准信息
        DataTable dtCheckStand = ds.Tables[0];

        //ICellStyle style = workbook.CreateCellStyle(); //设置单元格的样式：水平对齐居中 style.Alignment = HorizontalAlignment.CENTER; //新建一个字体样式对象
        //IDataFormat formatNum = workbook.CreateDataFormat();
        //style.DataFormat = formatNum.GetFormat("[h]:mm");//设置单元格的格式为科学计数法

        if (dtCheckStand.Rows.Count > 0)
        {
            DataRow drCheckStand = dtCheckStand.Rows[0];

            // 设置最小值
            string permMin101 = drCheckStand["PermMin101"].ToString();
            string permMin102 = drCheckStand["PermMin102"].ToString();
            string permMin201 = drCheckStand["PermMin201"].ToString();
            string permMin202 = drCheckStand["PermMin202"].ToString();
            string permMin203 = drCheckStand["PermMin203"].ToString();
            string permMin204 = drCheckStand["PermMin204"].ToString();
            string permMin205 = drCheckStand["PermMin205"].ToString();
            string permMin206 = drCheckStand["PermMin206"].ToString();
            string permMin301 = drCheckStand["PermMin301"].ToString();
            string permMin401 = drCheckStand["PermMin401"].ToString();
            string permMin501 = drCheckStand["PermMin501"].ToString();

            if (permMin201 == "")
            {
                sheet.GetRow(3).GetCell(3).SetCellValue("");
            }
            else
            {
                sheet.GetRow(3).GetCell(3).SetCellValue(Convert.ToDouble(permMin201));
            }
            if (permMin202 == "")
            {
                sheet.GetRow(3).GetCell(4).SetCellValue("");
            }
            else
            {
                sheet.GetRow(3).GetCell(4).SetCellValue(Convert.ToDouble(permMin202));
            }
            if (permMin205 == "")
            {
                sheet.GetRow(3).GetCell(5).SetCellValue("");
            }
            else
            {
                sheet.GetRow(3).GetCell(5).SetCellValue(Convert.ToDouble(permMin205));
            }
            if (permMin206 == "")
            {
                sheet.GetRow(3).GetCell(6).SetCellValue("");
            }
            else
            {
                sheet.GetRow(3).GetCell(6).SetCellValue(Convert.ToDouble(permMin206));
            }
            if (permMin101 == "")
            {
                sheet.GetRow(3).GetCell(7).SetCellValue("");
            }
            else
            {
                sheet.GetRow(3).GetCell(7).SetCellValue(Convert.ToDouble(permMin101));
            }

            // sheet.GetRow(3).GetCell(8).CellStyle.DataFormat = formatNum.GetFormat("hh:mm");

            if (permMin102 == "")
            {
                sheet.GetRow(3).GetCell(8).SetCellValue(0);
            }
            else
            {
                double tempPermMin102 = Convert.ToDouble(permMin102);
                string permMin102Value = Math.Truncate(tempPermMin102).ToString() + ":"
                    + Math.Truncate((tempPermMin102 - Math.Truncate(tempPermMin102)) * 60).ToString();

                sheet.GetRow(3).GetCell(8).SetCellValue(tempPermMin102 / (24 * 60));
            }
            if (permMin401 == "")
            {
                sheet.GetRow(3).GetCell(9).SetCellValue("");
            }
            else
            {
                sheet.GetRow(3).GetCell(9).SetCellValue(Convert.ToDouble(permMin401));
            }
            if (permMin301 == "")
            {
                sheet.GetRow(3).GetCell(10).SetCellValue("");
            }
            else
            {
                sheet.GetRow(3).GetCell(10).SetCellValue(Convert.ToDouble(permMin301));
            }
            if (permMin501 == "")
            {
                sheet.GetRow(3).GetCell(11).SetCellValue("");
            }
            else
            {
                sheet.GetRow(3).GetCell(11).SetCellValue(Convert.ToDouble(permMin501));
            }

            //设置最大值
            string permMax101 = drCheckStand["PermMax101"].ToString();
            string permMax102 = drCheckStand["PermMax102"].ToString();
            string permMax201 = drCheckStand["PermMax201"].ToString();
            string permMax202 = drCheckStand["PermMax202"].ToString();
            string permMax203 = drCheckStand["PermMax203"].ToString();
            string permMax204 = drCheckStand["PermMax204"].ToString();
            string permMax205 = drCheckStand["PermMax205"].ToString();
            string permMax206 = drCheckStand["PermMax206"].ToString();
            string permMax301 = drCheckStand["PermMax301"].ToString();
            string permMax401 = drCheckStand["PermMax401"].ToString();
            string permMax501 = drCheckStand["PermMax501"].ToString();

            if (permMax201 == "")
            {
                sheet.GetRow(5).GetCell(3).SetCellValue("");
            }
            else
            {
                sheet.GetRow(5).GetCell(3).SetCellValue(Convert.ToDouble(permMax201));
            }
            if (permMax202 == "")
            {
                sheet.GetRow(5).GetCell(4).SetCellValue("");
            }
            else
            {
                sheet.GetRow(5).GetCell(4).SetCellValue(Convert.ToDouble(permMax202));
            }
            if (permMax205 == "")
            {
                sheet.GetRow(5).GetCell(5).SetCellValue("");
            }
            else
            {
                sheet.GetRow(5).GetCell(5).SetCellValue(Convert.ToDouble(permMax205));
            }
            if (permMax206 == "")
            {
                sheet.GetRow(5).GetCell(6).SetCellValue("");
            }
            else
            {
                sheet.GetRow(5).GetCell(6).SetCellValue(permMax206 == "" ? 0 : Convert.ToDouble(permMax206));
            }
            if (permMax101 == "")
            {
                sheet.GetRow(5).GetCell(7).SetCellValue("");
            }
            else
            {
                sheet.GetRow(5).GetCell(7).SetCellValue(Convert.ToDouble(permMax101));
            }
            if (permMax102 == "")
            {
                sheet.GetRow(5).GetCell(8).SetCellValue(0);
            }
            else
            {
                double tempPermMax102 = Convert.ToDouble(permMax102);
                string permMax102Value = Math.Truncate(tempPermMax102).ToString() + ":"
                    + Math.Truncate((tempPermMax102 - Math.Truncate(tempPermMax102)) * 60).ToString();
                sheet.GetRow(5).GetCell(8).SetCellValue(tempPermMax102 / (24 * 60));
            }
            if (permMax401 == "")
            {
                sheet.GetRow(5).GetCell(9).SetCellValue("");
            }
            else
            {
                sheet.GetRow(5).GetCell(9).SetCellValue(Convert.ToDouble(permMax401));
            }
            if (permMax301 == "")
            {
                sheet.GetRow(5).GetCell(10).SetCellValue("");
            }
            else
            {
                sheet.GetRow(5).GetCell(10).SetCellValue(Convert.ToDouble(permMax301));
            }
            if (permMax501 == "")
            {
                sheet.GetRow(5).GetCell(11).SetCellValue("");
            }
            else
            {
                sheet.GetRow(5).GetCell(11).SetCellValue(Convert.ToDouble(permMax501));
            }

            //设置中值
            if (permMax201 == "" || permMin201 == "")
            {
                sheet.GetRow(4).GetCell(3).SetCellValue("");
            }
            else
            {
                sheet.GetRow(4).GetCell(3).SetCellValue(Math.Round((Convert.ToDouble(permMax201) + Convert.ToDouble(permMin201)) / 2, 3));
            }
            if (permMax202 == "" || permMin202 == "")
            {
                sheet.GetRow(4).GetCell(4).SetCellValue("");
            }
            else
            {
                sheet.GetRow(4).GetCell(4).SetCellValue(Math.Round((Convert.ToDouble(permMax202) + Convert.ToDouble(permMin202)) / 2, 3));
            }
            if (permMax205 == "" || permMin205 == "")
            {
                sheet.GetRow(4).GetCell(5).SetCellValue("");
            }
            else
            {
                sheet.GetRow(4).GetCell(5).SetCellValue(Math.Round((Convert.ToDouble(permMax205) + Convert.ToDouble(permMin205)) / 2, 3));
            }
            if (permMax206 == "" || permMin206 == "")
            {
                sheet.GetRow(4).GetCell(6).SetCellValue("");
            }
            else
            {
                sheet.GetRow(4).GetCell(6).SetCellValue(Math.Round((Convert.ToDouble(permMax206) + Convert.ToDouble(permMin206)) / 2, 3));
            }
            if (permMax101 == "" || permMin101 == "")
            {
                sheet.GetRow(4).GetCell(7).SetCellValue("");
            }
            else
            {
                sheet.GetRow(4).GetCell(7).SetCellValue(Math.Round((Convert.ToDouble(permMax101) + Convert.ToDouble(permMin101)) / 2, 3));
            }
            if (permMax102 == "" || permMin102 == "")
            {
                sheet.GetRow(4).GetCell(8).SetCellValue(0);
            }
            else
            {
                double tempPermMid102 = (Convert.ToDouble(permMax102) + Convert.ToDouble(permMin102)) / 2;
                string permMid102 = Math.Truncate(tempPermMid102).ToString() + ":"
                    + Math.Truncate((tempPermMid102 - Math.Truncate(tempPermMid102)) * 60).ToString();
                sheet.GetRow(4).GetCell(8).SetCellValue(tempPermMid102 / (24 * 60));
            }
            if (permMax401 == "" || permMin401 == "")
            {
                sheet.GetRow(4).GetCell(9).SetCellValue("");
            }
            else
            {
                sheet.GetRow(4).GetCell(9).SetCellValue(Math.Round((Convert.ToDouble(permMax401) + Convert.ToDouble(permMin401)) / 2, 3));
            }
            if (permMax301 == "" || permMin301 == "")
            {
                sheet.GetRow(4).GetCell(10).SetCellValue("");
            }
            else
            {
                sheet.GetRow(4).GetCell(10).SetCellValue(Math.Round((Convert.ToDouble(permMax301) + Convert.ToDouble(permMin301)) / 2, 3));
            }
            if (permMax501 == "" || permMin501 == "")
            {
                sheet.GetRow(4).GetCell(11).SetCellValue("");
            }
            else
            {
                sheet.GetRow(4).GetCell(11).SetCellValue(Math.Round((Convert.ToDouble(permMax501) + Convert.ToDouble(permMin501)) / 2, 3));
            }

        }

        #endregion 填充标准信息

        #region 填充质检信息
        int rowIndex = 17;
        DataTable dtCheckRecord = ds.Tables[1];
        foreach (DataRow drCheckRecord in dtCheckRecord.Rows)
        {
            string itemCheck101 = drCheckRecord["ItemCheck101"].ToString();
            string itemCheck102 = drCheckRecord["ItemCheck102"].ToString();
            string itemCheck201 = drCheckRecord["ItemCheck201"].ToString();
            string itemCheck202 = drCheckRecord["ItemCheck202"].ToString();
            string itemCheck203 = drCheckRecord["ItemCheck203"].ToString();
            string itemCheck204 = drCheckRecord["ItemCheck204"].ToString();
            string itemCheck205 = drCheckRecord["ItemCheck205"].ToString();
            string itemCheck206 = drCheckRecord["ItemCheck206"].ToString();
            string itemCheck301 = drCheckRecord["ItemCheck301"].ToString();
            string itemCheck401 = drCheckRecord["ItemCheck401"].ToString();
            string itemCheck501 = drCheckRecord["ItemCheck501"].ToString();
            string judgeResult = drCheckRecord["JudgeResult"].ToString();
            string recipetype = "";
            try
            {

                 recipetype = drCheckRecord["recipetype"].ToString();
            }
            catch(Exception ee){}
            if (itemCheck201 == "")
            {
                continue;
            }

            rowIndex = rowIndex + 1;

            string className = drCheckRecord["PlanDate"].ToString() + drCheckRecord["ClassName"].ToString();

            sheet.GetRow(rowIndex).GetCell(0).SetCellValue(className);

            if (itemCheck201 == "")
            {
                sheet.GetRow(rowIndex).GetCell(3).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(3).SetCellValue(Convert.ToDouble(itemCheck201));
            }
            if (itemCheck202 == "")
            {
                sheet.GetRow(rowIndex).GetCell(4).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(4).SetCellValue(Convert.ToDouble(itemCheck202));
            }
            if (itemCheck205 == "")
            {
                sheet.GetRow(rowIndex).GetCell(5).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(5).SetCellValue(Convert.ToDouble(itemCheck205));
            }
            if (itemCheck206 == "")
            {
                sheet.GetRow(rowIndex).GetCell(6).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(6).SetCellValue(Convert.ToDouble(itemCheck206));
            }
            if (itemCheck101 == "")
            {
                sheet.GetRow(rowIndex).GetCell(7).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(7).SetCellValue(Convert.ToDouble(itemCheck101));
            }
            if (itemCheck102 == "")
            {
                sheet.GetRow(rowIndex).GetCell(8).SetCellValue("");
            }
            else
            {
                double tempItemCheck202 = Convert.ToDouble(itemCheck102);
                string itemCheck102Value = Math.Truncate(tempItemCheck202).ToString() + ":" 
                    + Math.Truncate((tempItemCheck202 - Math.Truncate(tempItemCheck202)) * 60).ToString() + ":00";
                //sheet.GetRow(rowIndex).GetCell(14).SetCellValue(tempItemCheck202);
                sheet.GetRow(rowIndex).GetCell(8).SetCellValue(tempItemCheck202 / (24 * 60));
            }
            if (itemCheck401 == "")
            {
                sheet.GetRow(rowIndex).GetCell(9).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(9).SetCellValue(Convert.ToDouble(itemCheck401));
            }
            if (itemCheck301 == "")
            {
                sheet.GetRow(rowIndex).GetCell(10).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(10).SetCellValue(Convert.ToDouble(itemCheck301));
            }
            if (itemCheck501 == "")
            {
                sheet.GetRow(rowIndex).GetCell(11).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(11).SetCellValue(Convert.ToDouble(itemCheck501));
            }

            if (judgeResult == "")
            {
                sheet.GetRow(rowIndex).GetCell(13).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(13).SetCellValue(Convert.ToInt32(judgeResult));
            }


            if (recipetype == "")
            {
                sheet.GetRow(rowIndex).GetCell(14).SetCellValue("");
            }
            else
            {
                sheet.GetRow(rowIndex).GetCell(14).SetCellValue(recipetype);
            }

        }
        #endregion 填充质检信息

        sheet.ForceFormulaRecalculation = true;

        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);

        X.Mask.Hide();

        new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, materName);

    }
    public DataSet GetCheckRubberAssessQualityCPKReportByParas(IQmtCheckRubberAssessQualityCPKReportParams paras)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("@MaterCode", paras.MaterCode);
        dict.Add("@BeginPlanDate", paras.BeginPlanDate);
        dict.Add("@EndPlanDate", paras.EndPlanDate);
        dict.Add("@ZJSID", paras.ZJSID);
        dict.Add("@EquipCode", paras.EquipCode);
        dict.Add("@OtherMaterCodes", paras.OtherMaterCodes);
        IQmtCheckAssessMasterManager bQmtCheckAssessMasterManager = new QmtCheckAssessMasterManager();
        return bQmtCheckAssessMasterManager.GetDataSetByStoreProcedure("ProcQmtRubberQualityCPKReportZJ", dict);

    }
}