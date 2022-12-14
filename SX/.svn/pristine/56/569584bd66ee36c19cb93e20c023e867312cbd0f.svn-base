using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;

using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

using Ext.Net;

using NBear.Common;
using Mesnac.Entity;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Web.UI;

public partial class Manager_RubberQuality_BasicInfo_CheckStandImport : BasePage
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            保存 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthSave" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 保存 { get; private set; } //必须为 public
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
            scriptLink.Attributes.Add("src", "CheckStandImport.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            InitControls();
        }
    }
    protected void btnExport_Click(object sender, DirectEventArgs e)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        using (FileStream fs = new FileStream(Server.MapPath("CheckStandItemImport.xls"), FileMode.Open, FileAccess.Read))
        {
            try
            {
                workbook = new HSSFWorkbook(fs);
            }
            catch
            {
                X.Msg.Alert("提示", "模板不是有效的Excel文件").Show();
                return;
            }
        }

           MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            string fileName = "胶料质检标准导入模板";
            new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, fileName);
         
    }
    private void InitControls()
    {
        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(
            QmtCheckStandType._.DeleteFlag == "0"
            , QmtCheckStandType._.ObjID.Asc);
        foreach (QmtCheckStandType mQmtCheckStandType in mQmtCheckStandTypeList)
        {
            ComboBoxNorthCheckStandTypeId.AddItem(mQmtCheckStandType.StandTypeName, mQmtCheckStandType.ObjID.ToString());
        }
    }

    #region 标准文件上传

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

    /// <summary>
    /// 上传标准文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthUpload_Click(object sender, DirectEventArgs e)
    {
        if (FileUploadFieldNorthExcel.HasFile == false)
        {
            X.Msg.Alert("提示", "请选择要上传的文件").Show();

            return;
        }

        if (ComboBoxNorthExcelSheets.Value == null || ComboBoxNorthExcelSheets.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择Sheet页").Show();
            return;
        }

        if (DateFieldNorthRegDateTime.RawText == null || DateFieldNorthRegDateTime.RawText == "")
        {
            X.Msg.Alert("提示", "请选择生效日期").Show();

            return;
        }

        if (TimeFieldNorthRegDateTime.RawText == null || TimeFieldNorthRegDateTime.RawText == "")
        {
            X.Msg.Alert("提示", "请填写生效时间").Show();

            return;
        }

        if (TextFieldNorthLLStandVision.Text == null || TextFieldNorthLLStandVision.Text.Trim() == "")
        {
            X.Msg.Alert("提示", "请填写玲珑版本").Show();

            return;
        }

        if (ComboBoxNorthCheckStandTypeId.Value.ToString() == "")
        {
            X.Msg.Alert("提示", "请选择标准类型").Show();
            return;
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
                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "失败",
                    Message = "上传的文件不是有效的Excel文件"
                });
                return;
            }

            IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
            EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetAllList();

            //将原Excel文件导入到DataTable中
            DataTable dtWorkbook = CreateWorkbookTable();

            int sheetIndex = Convert.ToInt32(ComboBoxNorthExcelSheets.Value);

            SetWorkbookTable(dtWorkbook, workbook.GetSheetAt(sheetIndex));

            DataTable dtTemp = CreateTempTable();

            SetTempTable(dtWorkbook, dtTemp, mBasMaterialList);

            dtWorkbook.DefaultView.Sort = "seq";
            StoreCenterOrigin.DataSource = dtWorkbook;
            StoreCenterOrigin.DataBind();

            DataTable dtError = dtWorkbook.Copy();
            dtError.DefaultView.RowFilter = "flag='1'";
            StoreCenterError.DataSource = dtError;
            StoreCenterError.DataBind();

            dtTemp.DefaultView.Sort = "seq";
            StoreCenterSave.DataSource = dtTemp;
            StoreCenterSave.DataBind();

            //DateFieldNorthRegDateTime.Disabled = true;
            //TimeFieldNorthRegDateTime.Disabled = true;

            //if (dtTemp.Rows.Count == 0)
            //{
            //    ButtonNorthSave.Disabled = true;
            //}
            //else
            //{
            //    ButtonNorthSave.Disabled = false;
            //}
            string tpl = "文件上传完毕: {0}<br/>文件大小: {1} 字节<br/>Excel文件数据：{2} 条<br/>要保存的数据: {3} 条";
            X.Msg.Show(new MessageBoxConfig
            {
                Buttons = MessageBox.Button.OK,
                Icon = MessageBox.Icon.INFO,
                Title = "成功",
                Message = string.Format(tpl, this.FileUploadFieldNorthExcel.PostedFile.FileName
                , this.FileUploadFieldNorthExcel.PostedFile.ContentLength
                , dtWorkbook.Rows.Count.ToString()
                , dtTemp.Rows.Count.ToString())
            });
        }

    }

    /// <summary>
    /// 创建Workbook表格
    /// </summary>
    /// <returns></returns>
    private DataTable CreateWorkbookTable()
    {
        DataTable dtWorkbook = new DataTable();
        dtWorkbook.Columns.Add(new DataColumn("sheetname"));
        dtWorkbook.Columns.Add(new DataColumn("typename"));
        dtWorkbook.Columns.Add(new DataColumn("index"));
        dtWorkbook.Columns.Add(new DataColumn("matername"));
        dtWorkbook.Columns.Add(new DataColumn("min"));
        dtWorkbook.Columns.Add(new DataColumn("205")); //T30
        dtWorkbook.Columns.Add(new DataColumn("206")); //T60
        dtWorkbook.Columns.Add(new DataColumn("201")); //ML
        dtWorkbook.Columns.Add(new DataColumn("202")); //MH
        dtWorkbook.Columns.Add(new DataColumn("101")); //ML(1+4)
        dtWorkbook.Columns.Add(new DataColumn("102")); //T5
        dtWorkbook.Columns.Add(new DataColumn("301")); //硬度
        dtWorkbook.Columns.Add(new DataColumn("401")); //比重
        dtWorkbook.Columns.Add(new DataColumn("501")); //H抽出

        dtWorkbook.Columns.Add(new DataColumn("typeid"));
        dtWorkbook.Columns.Add(new DataColumn("seq", typeof(int)));
        dtWorkbook.Columns.Add(new DataColumn("flag"));
        dtWorkbook.Columns.Add(new DataColumn("errmsg"));

        return dtWorkbook;
    }

    /// <summary>
    /// 填充Workbook表格
    /// 修改标识：qusf 20131105
    /// 修改说明：1.标准类型改为用户选择
    ///           2.Excel文件改为只上传第一个sheet页
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="workbook"></param>
    private void SetWorkbookTable(DataTable dtWorkbook, ISheet sheet)
    {
        int seq = 0;
        //int numberOfSheets = workbook.NumberOfSheets;

        IQmtCheckStandTypeManager bQmtCheckStandTypeManager = new QmtCheckStandTypeManager();
        EntityArrayList<QmtCheckStandType> mQmtCheckStandTypeList = bQmtCheckStandTypeManager.GetListByWhereAndOrder(
            QmtCheckStandType._.DeleteFlag == "0"
            , QmtCheckStandType._.ObjID.Asc
            );

        //for (int sheetIndex = 0; sheetIndex < numberOfSheets; sheetIndex++)
        //{
            //ISheet sheet = workbook.GetSheetAt(0);

            string sheetName = sheet.SheetName;

            string typeId = ComboBoxNorthCheckStandTypeId.Value.ToString();
            string typeName = ComboBoxNorthCheckStandTypeId.RawText;

            int firstRowNum = sheet.FirstRowNum;
            int lastRowNum = sheet.LastRowNum;

            int index = 0;
            for (int rowNum = firstRowNum; rowNum <= lastRowNum; rowNum++)
            {
                IRow row = sheet.GetRow(rowNum);

                if (row.GetCell(0, MissingCellPolicy.RETURN_BLANK_AS_NULL) != null)
                {
                    string s = row.GetCell(0).ToString();

                    if (s != "" && int.TryParse(s, out index) == true)
                    {
                        seq++;
                        DataRow drWorkbook = dtWorkbook.NewRow();
                        drWorkbook["sheetname"] = sheetName;
                        drWorkbook["typename"] = typeName;
                        drWorkbook["index"] = row.GetCell(0).ToString();
                        drWorkbook["matername"] = row.GetCell(1).ToString();
                        drWorkbook["min"] = row.GetCell(2).ToString();
                        drWorkbook["205"] = row.GetCell(3).ToString();
                        drWorkbook["206"] = row.GetCell(4).ToString();
                        drWorkbook["201"] = row.GetCell(5).ToString();
                        drWorkbook["202"] = row.GetCell(6).ToString();
                        drWorkbook["101"] = row.GetCell(7).ToString();
                        drWorkbook["102"] = row.GetCell(8).ToString();
                        drWorkbook["301"] = row.GetCell(9).ToString();
                        drWorkbook["401"] = row.GetCell(10).ToString();
                        drWorkbook["501"] = row.GetCell(11).ToString();

                        drWorkbook["typeid"] = typeId;
                        drWorkbook["seq"] = seq.ToString();

                        drWorkbook["flag"] = "0";

                        dtWorkbook.Rows.Add(drWorkbook);

                    }
                }

            }
        //}

    }

    /// <summary>
    /// 创建要保存的临时表格
    /// </summary>
    /// <returns></returns>
    private DataTable CreateTempTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("sheetname"));
        dt.Columns.Add(new DataColumn("typename"));
        dt.Columns.Add(new DataColumn("index"));
        dt.Columns.Add(new DataColumn("matername"));

        dt.Columns.Add(new DataColumn("typeid"));
        dt.Columns.Add(new DataColumn("seq", typeof(int)));
        dt.Columns.Add(new DataColumn("matercode"));

        dt.Columns.Add(new DataColumn("min205")); //T30
        dt.Columns.Add(new DataColumn("min206")); //T60
        dt.Columns.Add(new DataColumn("min201")); //ML
        dt.Columns.Add(new DataColumn("min202")); //MH
        dt.Columns.Add(new DataColumn("min101")); //ML(1+4)
        dt.Columns.Add(new DataColumn("min102")); //T5
        dt.Columns.Add(new DataColumn("min301")); //硬度
        dt.Columns.Add(new DataColumn("min401")); //比重
        dt.Columns.Add(new DataColumn("min501")); //H抽出

        dt.Columns.Add(new DataColumn("max205")); //T30
        dt.Columns.Add(new DataColumn("max206")); //T60
        dt.Columns.Add(new DataColumn("max201")); //ML
        dt.Columns.Add(new DataColumn("max202")); //MH
        dt.Columns.Add(new DataColumn("max101")); //ML(1+4)
        dt.Columns.Add(new DataColumn("max102")); //T5
        dt.Columns.Add(new DataColumn("max301")); //硬度
        dt.Columns.Add(new DataColumn("max401")); //比重
        dt.Columns.Add(new DataColumn("max501")); //H抽出

        return dt;
    }

    /// <summary>
    /// 填充要保存的临时表格
    /// </summary>
    /// <param name="dtWorkbook"></param>
    /// <param name="dtTemp"></param>
    /// <param name="mBasMaterialList"></param>
    private void SetTempTable(DataTable dtWorkbook, DataTable dtTemp, EntityArrayList<BasMaterial> mBasMaterialList)
    {
        dtTemp.DefaultView.Sort = "matercode, typeid";
        int rowCount = dtWorkbook.Rows.Count;
        int seq = 0;
        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            bool flag = true;
            DataRow drWorkbook = dtWorkbook.Rows[rowIndex];
            string[] splitChars = { "/" };
            string materNameWorkbook = drWorkbook["matername"].ToString();
            if (materNameWorkbook == "")
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "胶料代号不能为空";
                continue;
            }
            string[] materNameTemps = materNameWorkbook.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

            string min205 = "";
            string max205 = "";
            flag = flag && GetStandValue(drWorkbook["205"].ToString(), out min205, out max205);

            string min206 = "";
            string max206 = "";
            flag = flag && GetStandValue(drWorkbook["206"].ToString(), out min206, out max206);

            string min201 = "";
            string max201 = "";
            flag = flag && GetStandValue(drWorkbook["201"].ToString(), out min201, out max201);

            string min202 = "";
            string max202 = "";
            flag = flag && GetStandValue(drWorkbook["202"].ToString(), out min202, out max202);

            string min101 = "";
            string max101 = "";
            flag = flag && GetStandValue(drWorkbook["101"].ToString(), out min101, out max101);

            string min102 = "";
            string max102 = "";
            flag = flag && GetStandValue(drWorkbook["102"].ToString(), out min102, out max102);

            string min301 = "";
            string max301 = "";
            flag = flag && GetStandValue(drWorkbook["301"].ToString(), out min301, out max301);

            string min401 = "";
            string max401 = "";
            flag = flag && GetStandValue(drWorkbook["401"].ToString(), out min401, out max401);

            string min501 = "";
            string max501 = "";
            flag = flag && GetStandValue_1(drWorkbook["501"].ToString(), out min501, out max501);

            if (flag == false)
            {
                drWorkbook["flag"] = "1";
                drWorkbook["errmsg"] = "上传的标准数据不符合要求";
                continue;
            }

            foreach (string materNameTemp in materNameTemps)
            {
                BasMaterial[] mBasMaterials = mBasMaterialList.Filter(BasMaterial._.MaterialName == materNameTemp, BasMaterial._.DeleteFlag.Asc & BasMaterial._.ObjID.Desc);

                if (mBasMaterials.Length == 0)
                {
                    drWorkbook["flag"] = "1";
                    drWorkbook["errmsg"] = "胶料代号不存在：" + materNameTemp;
                    continue;
                }
                if (dtTemp.DefaultView.Find(new object[] { mBasMaterials[0].MaterialCode, drWorkbook["typeid"].ToString() }) >= 0)
                {
                    continue;
                }

                seq++;
                DataRow drTemp = dtTemp.NewRow();
                drTemp["sheetname"] = drWorkbook["sheetname"].ToString();
                drTemp["typename"] = drWorkbook["typename"].ToString();
                drTemp["index"] = drWorkbook["index"].ToString();
                drTemp["matername"] = materNameTemp;

                drTemp["seq"] = seq;
                drTemp["typeid"] = drWorkbook["typeid"].ToString();
                drTemp["matercode"] = mBasMaterials[0].MaterialCode;

                drTemp["min205"] = min205;
                drTemp["max205"] = max205;

                drTemp["min206"] = min206;
                drTemp["max206"] = max206;

                drTemp["min201"] = min201;
                drTemp["max201"] = max201;

                drTemp["min202"] = min202;
                drTemp["max202"] = max202;

                drTemp["min101"] = min101;
                drTemp["max101"] = max101;

                drTemp["min102"] = min102;
                drTemp["max102"] = max102;

                drTemp["min301"] = min301;
                drTemp["max301"] = max301;

                drTemp["min401"] = min401;
                drTemp["max401"] = max401;

                drTemp["min501"] = min501;
                drTemp["max501"] = max501;

                dtTemp.Rows.Add(drTemp);
            }

        }

    }

    /// <summary>
    /// 获取除"H抽出"的最大最小值
    /// </summary>
    /// <param name="originValue"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    private bool GetStandValue(string originValue, out string minValue, out string maxValue)
    {
        if (originValue.Contains("("))
        {
            originValue = originValue.Substring(0, originValue.IndexOf("("));
        }
        else if (originValue.Contains("（"))
        {
            originValue = originValue.Substring(0, originValue.IndexOf("（"));
        }
        if (originValue == "-" || originValue == "/")
        {
            minValue = "";
            maxValue = "";
            return true;
        }
        else if (originValue.Contains("≥") == true)
        {
            double dValue = 0;
            if (double.TryParse(originValue.Substring(originValue.IndexOf("≥") + 1), out dValue) == true)
            {
                minValue = dValue.ToString();
                maxValue = "9999";
                return true;
            }
            else
            {
                minValue = originValue;
                maxValue = "";
                return false;
            }
        }
        else if (originValue.Contains("≤") == true)
        {

            double dValue = 0;
            if (double.TryParse(originValue.Substring(originValue.IndexOf("≤") + 1), out dValue) == true)
            {
                minValue = "0";
                maxValue = dValue.ToString();
                return true;
            }
            else
            {
                minValue = "";
                maxValue = originValue;
                return false;
            }
        }
        else if (originValue.Contains("-") == true)
        {
            double dMin = 0;
            double dMax = 0;
            minValue = originValue.Substring(0, originValue.IndexOf("-"));
            maxValue = originValue.Substring(originValue.IndexOf("-") + 1);
            if (double.TryParse(minValue, out dMin) == true && double.TryParse(maxValue, out dMax) == true)
            {
                minValue = dMin.ToString();
                maxValue = dMax.ToString();
                return true;
            }
            else
            {
                minValue = originValue;
                maxValue = "";
                return false;
            }
        }
        else if (originValue.Contains("/") == true)
        {
            double dMin = 0;
            double dMax = 0;
            minValue = originValue.Substring(0, originValue.IndexOf("/"));
            maxValue = originValue.Substring(originValue.IndexOf("/") + 1);
            if (double.TryParse(minValue, out dMin) == true && double.TryParse(maxValue, out dMax) == true)
            {
                minValue = dMin.ToString();
                maxValue = dMax.ToString();
                return true;
            }
            else
            {
                minValue = originValue;
                maxValue = "";
                return false;
            }
        }
        else
        {
            minValue = originValue;
            maxValue = "";
            return false;
        }
    }

    /// <summary>
    /// 获取"H抽出"的最大最小值
    /// </summary>
    /// <param name="originValue"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    private bool GetStandValue_1(string originValue, out string minValue, out string maxValue)
    {
        if (originValue == "" || originValue == "-" || originValue == "/")
        {
            minValue = "";
            maxValue = "";
            return true;
        }
        else
        {
            double dMin = 0;

            if (double.TryParse(originValue, out dMin) == true)
            {
                minValue = dMin.ToString();
                maxValue = "9999";
                return true;
            }
            else
            {
                minValue = originValue;
                maxValue = "";
                return false;
            }
        }
    }

    #endregion 标准文件上传

    #region 标准保存

    /// <summary>
    /// 保存标准信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void StoreCenterSave_SubmitData(object sender, StoreSubmitDataEventArgs e)
    {
        if (DateFieldNorthRegDateTime.RawText == null || DateFieldNorthRegDateTime.RawText == "")
        {
            X.Msg.Alert("提示", "生效日期不能为空").Show();
            X.Mask.Hide();
            return;
        }

        if (TimeFieldNorthRegDateTime.RawText == null || TimeFieldNorthRegDateTime.RawText == "")
        {
            X.Msg.Alert("提示", "生效时间不能为空").Show();
            X.Mask.Hide();
            return;
        }

        if (TextFieldNorthLLStandVision.Text == null || TextFieldNorthLLStandVision.Text.Trim() == "")
        {
            X.Msg.Alert("提示", "玲珑版本不能为空").Show();
            X.Mask.Hide();
            return;
        }

        List<JsonObject> joList = e.Object<JsonObject>();

        if (joList.Count() == 0)
        {
            X.Msg.Alert("提示", "没有需要提交保存的标准信息").Show();
            X.Mask.Hide();
            return;
        }

        Guid guid = Guid.NewGuid();

        EntityArrayList<QmtCheckStandMaster> mQmtCheckStandMasterList = new EntityArrayList<QmtCheckStandMaster>();
        EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList = new EntityArrayList<QmtCheckStandDetail>();

        string dealCode = "1";
        string drawMark = "";
        string cardMark2 = "";
        IQmtDealNotionManager bQmtDealNotionManager = new QmtDealNotionManager();
        QmtDealNotion mQmtDealNotion = bQmtDealNotionManager.GetById(new object[] { dealCode });
        if (mQmtDealNotion != null)
        {
            drawMark = mQmtDealNotion.DealNotion;
            cardMark2 = mQmtDealNotion.DealNotion;
        }

        int masterCount = 0;
        int detailCount = 0;
        foreach (JsonObject jo in joList)
        {
            QmtCheckStandMaster mQmtCheckStandMaster = new QmtCheckStandMaster();
            mQmtCheckStandMaster.Choiceness = "1";
            mQmtCheckStandMaster.DefineDate = DateTime.Today.ToString("yyyy-MM-dd");
            mQmtCheckStandMaster.DeleteFlag = "0";
            mQmtCheckStandMaster.GUID = guid.ToString();
            mQmtCheckStandMaster.MaterCode = jo["matercode"].ToString();
            mQmtCheckStandMaster.MemoNote = "";
            mQmtCheckStandMaster.QuaCompute = "1";
            mQmtCheckStandMaster.RegDateTime = DateTime.Parse(DateFieldNorthRegDateTime.RawText + " " + TimeFieldNorthRegDateTime.RawText);
            mQmtCheckStandMaster.StandCode = Convert.ToInt32(jo["typeid"]);
            mQmtCheckStandMaster.StandDate = DateTime.Today.ToString("yyyy-MM-dd");
            mQmtCheckStandMaster.StandVisionStat = "1";
            mQmtCheckStandMaster.WorkerBarcode = this.UserID;
            mQmtCheckStandMaster.StandId = Convert.ToInt32(jo["seq"]);

            mQmtCheckStandMaster.LLStandVision = TextFieldNorthLLStandVision.Text.Trim();

            mQmtCheckStandMasterList.Add(mQmtCheckStandMaster);
            masterCount++;


            string itemCd = "205";
            QmtCheckStandDetail mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

            itemCd = "206";
            mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

            itemCd = "201";
            mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

            itemCd = "202";
            mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

            itemCd = "101";
            mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

            itemCd = "102";
            mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

            itemCd = "301";
            mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

            itemCd = "401";
            mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

            itemCd = "501";
            mQmtCheckStandDetail = GetQmtCheckStandDetail(guid, jo, itemCd, dealCode, drawMark, cardMark2);
            if (mQmtCheckStandDetail != null)
            {
                mQmtCheckStandDetailList.Add(mQmtCheckStandDetail);
                detailCount++;
            }

        }

        IQmtCheckStandMasterManager bQmtCheckStandMasterManager = new QmtCheckStandMasterManager();
        bQmtCheckStandMasterManager.SaveImport(mQmtCheckStandMasterList, mQmtCheckStandDetailList);

        StoreCenterOrigin.RemoveAll();
        StoreCenterError.RemoveAll();
        StoreCenterSave.RemoveAll();

        ButtonNorthUpload.Disabled = false;
        //ButtonNorthSave.Disabled = true;
        //DateFieldNorthRegDateTime.Disabled = false;
        //TimeFieldNorthRegDateTime.Disabled = false;

        X.Msg.Alert("成功", "保存成功" + "<br />标准主数据: " + masterCount.ToString() + "<br />标准明细数据: " + detailCount.ToString()).Show();
        X.Mask.Hide();

    }

    private QmtCheckStandDetail GetQmtCheckStandDetail(Guid guid, JsonObject jo, string itemCd, string dealCode, string drawMark, string cardMark2)
    {
        if (jo["max" + itemCd].ToString() != "" && jo["min" + itemCd].ToString() != "")
        {
            QmtCheckStandDetail mQmtCheckStandDetail = new QmtCheckStandDetail();
            mQmtCheckStandDetail.CardMark2 = cardMark2;
            mQmtCheckStandDetail.DealCode = Convert.ToInt32(dealCode);
            mQmtCheckStandDetail.DeleteFlag = "0";
            mQmtCheckStandDetail.DrawMark = drawMark;
            mQmtCheckStandDetail.Grade = 1;
            mQmtCheckStandDetail.GUID = guid.ToString();
            mQmtCheckStandDetail.IfMax = 1;
            mQmtCheckStandDetail.IfMin = 1;
            mQmtCheckStandDetail.ItemCd = itemCd;
            mQmtCheckStandDetail.JudgeResult = 1;
            mQmtCheckStandDetail.PermMax = Convert.ToDecimal(jo["max" + itemCd]);
            mQmtCheckStandDetail.PermMin = Convert.ToDecimal(jo["min" + itemCd]);
            mQmtCheckStandDetail.WeightId = 1;
            mQmtCheckStandDetail.QuaFrequency = "100%";
            mQmtCheckStandDetail.StandId = Convert.ToInt32(jo["seq"]);

            return mQmtCheckStandDetail;
        }

        return null;
    }

    #endregion 标准保存
}