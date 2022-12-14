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
using System.Text;

public partial class Manager_RawMaterialQuality_CheckData : BasePage
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            添加 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthAdd" };
            修改 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthEdit" };
            删除 = new SysPageAction() { ActionID = 3, ActionName = "ButtonNorthDelete" };
            确认后修改 = new SysPageAction() { ActionID = 4, ActionName = "ButtonNorthSpecEdit" };
            确认后删除 = new SysPageAction() { ActionID = 5, ActionName = "ButtonNorthSpecDelete" };
            审核 = new SysPageAction() { ActionID = 6, ActionName = "ButtonNorthApprove" };
            放行 = new SysPageAction() { ActionID = 7, ActionName = "ButtonFX" };
            取消放行 = new SysPageAction() { ActionID = 8, ActionName = "ButtonQXFX" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 确认后修改 { get; private set; } //必须为 public
        public SysPageAction 确认后删除 { get; private set; } //必须为 public
        public SysPageAction 审核 { get; private set; } //必须为 public
        public SysPageAction 放行 { get; private set; } //必须为 public
        public SysPageAction 取消放行 { get; private set; } //必须为 public
    }
    #endregion

    #region 品级 枚举
    private enum EnumCheckGrade
    {
        /// <summary>
        /// 合格品
        /// </summary>
        Good,

        /// <summary>
        /// 一级品
        /// </summary>
        Prime,
        /// <summary>
        /// 最小值
        /// </summary>
        Min,
        /// <summary>
        /// 最大值
        /// </summary>
        Max,
    }
    #endregion

    #region 页面初始化
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
            scriptLink.Attributes.Add("src", "CheckData.js?" + DateTime.Now.Ticks.ToString());
            this.Page.Header.Controls.Add(scriptLink);
            #endregion 加载JS文件

            InitControls();

            DateFieldNorthCheckDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));

            ComboBoxNorthRecorder.Select(this.UserID);

            HiddenUserId.SetValue(this.UserID);
            if (this._.确认后修改.SeqIdx == 0)
            {
                HiddenSpecEditFlag.SetValue("0");
            }
            else
            {
                HiddenSpecEditFlag.SetValue("1");
            }
        }
    }

    /// <summary>
    /// 初始化控件
    /// </summary>
    private void InitControls()
    {
        // 原材料分类 
        IBasMaterialMinorTypeManager bBasMaterialMinorTypeManager = new BasMaterialMinorTypeManager();
        EntityArrayList<BasMaterialMinorType> mBasMaterialMinorTypeList = bBasMaterialMinorTypeManager.GetListByWhereAndOrder(
            BasMaterialMinorType._.DeleteFlag == "0"
            & BasMaterialMinorType._.MajorID == 1
            , BasMaterialMinorType._.ObjID.Asc);
        foreach (BasMaterialMinorType mBasMaterialMinorType in mBasMaterialMinorTypeList)
        {
            ComboBoxNorthMaterMinorType.AddItem(mBasMaterialMinorType.MinorTypeName, mBasMaterialMinorType.MinorTypeID);
        }

        // 录入人
        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        DataTable dtRecorder = bQmcCheckDataManager.GetAllRecorderInfo().Tables[0];
        foreach (DataRow drRecorder in dtRecorder.Rows)
        {
            ComboBoxNorthRecorder.Items.Add(new Ext.Net.ListItem { Text = drRecorder["UserName"].ToString(), Value = drRecorder["WorkBarcode"].ToString() });
        }
        IBasUserManager bBasUserManager = new BasUserManager();
        BasUser mBasUser = bBasUserManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0];

        if (ComboBoxNorthRecorder.Items.Count(p => p.Value == this.UserID) == 0)
        {
            ComboBoxNorthRecorder.Items.Insert(0, new Ext.Net.ListItem { Text = mBasUser.UserName, Value = this.UserID });
        }

    }
    #endregion

    #region 页面方法
    /// <summary>
    /// 清空编辑窗口主数据
    /// </summary>
    private void ClearCheckData()
    {
        HiddenMasterBillNo.SetValue("");
        HiddenMasterBarcode.SetValue("");
        HiddenMasterOrderID.SetValue("");
        HiddenMasterMaterCode.SetValue("");
        HiddenMasterSeriesId.SetValue("");

        TriggerFieldMasterSampleName.SetValue("");

        TextFieldMasterSupplierName.SetValue("");
        TextFieldMasterManufacturerName.SetValue("");
        TextFieldMasterFactoryCode.SetValue("");
        TextFieldMasterBarcode.SetValue("");
        TextFieldMasterSampleNum.SetValue("");
        TextFieldMasterBatchCode.SetValue("");
        TextFieldMasterSampleCode.SetValue("");
        TextFieldMasterSampleStatus.SetValue("");
        TextFieldMasterExtractorName.SetValue("");
        TextFieldMasterReceiverName.SetValue("");
        TextFieldMasterSendDate.SetValue("");
        TextFieldMasterReceiveDate.SetValue("");
        TextFieldMasterFetcherName.SetValue("");
        TextFieldMasterSampleRemark.SetValue("");

        DateFieldMasterCheckDate.SetValue(DateTime.Now.Date);
        ComboBoxMasterCheckResult.SetValue("");
        TextFieldMasterRemark.SetValue("");
        CheckboxMasterRecordStat.Checked = false;
        CheckboxMasterRecordStat.Enable();
        ComboBoxMasterCheckResult.Hidden = true;
        ComboBoxMasterCheckResult.AllowBlank = true;

        FieldSetMasterSampleInfo.Collapse();
        FieldSetMasterProperty.Height = Unit.Pixel(100);
        FieldSetMasterDetail.Height = Unit.Pixel(100);

        TextFieldMasterFrequency.SetValue("");
        HiddenMasterSpecId.SetValue("");
        TextFieldMasterSpec.SetValue("");

        ComboBoxMasterStandard.GetStore().RemoveAll();
    }

    /// <summary>
    /// 清空审核窗口主数据
    /// </summary>
    private void ClearApproveData()
    {
        txtSampleNameApprove.SetValue("");

        txtSupplierApprove.SetValue("");
        txtManufacturerApprove.SetValue("");
        txtFactoryNumApprove.SetValue("");
        txtBarcodeApprove.SetValue("");
        txtSampleNumApprove.SetValue("");
        txtBatchCodeApprove.SetValue("");
        txtSampleCodeApprove.SetValue("");
        txtSampleStatusApprove.SetValue("");
        txtExtractorApporve.SetValue("");
        txtReceiverApprove.SetValue("");
        txtSendDateApprove.SetValue("");
        txtReceiveDateApprove.SetValue("");
        txtFetcherApprove.SetValue("");
        txtSampleRemarkApprove.SetValue("");

        txtCheckDateApprove.SetValue("");
        txtCheckResultApprove.SetValue("");
        txtRemarkApprove.SetValue("");

        FieldApproveSlave.Collapse();
        FieldApproveMain.Height = Unit.Pixel(100);

        txtFrequencyApprove.SetValue("");
        txtSpecApprove.SetValue("");

        txtStandardApprove.SetValue("");
    }

    /// <summary>
    /// 查询数据
    /// </summary>
    private void QueryCheckData()
    {
        StoreCenterProperty.RemoveAll();
        StoreCenterDetail.RemoveAll();
        StoreCenterMaster.RemoveAll();

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        IQmcCheckDataQueryParams paras = new QmcCheckDataQueryParams();
        if (ComboBoxNorthMaterMinorType.Value.ToString() != "")
        {
            paras.MinorTypeID = ComboBoxNorthMaterMinorType.Value.ToString();
        }
        if (ComboBoxNorthMater.Value.ToString() != "")
        {
            paras.MaterCode = ComboBoxNorthMater.Value.ToString();
        }
        if (DateFieldNorthCheckDate.RawText != null && DateFieldNorthCheckDate.RawText != "")
        {
            paras.BeginCheckDate = DateFieldNorthCheckDate.RawText;
            paras.EndCheckDate = DateFieldNorthCheckDate.RawText;
        }
        if (ComboBoxNorthSupplyFac.Value.ToString() != "")
        {
            paras.SupplyFacId = ComboBoxNorthSupplyFac.Value.ToString();
        }
        if (ComboBoxNorthCheckResult.Value.ToString() != "")
        {
            paras.CheckResult = ComboBoxNorthCheckResult.Value.ToString();
        }
        if (TextFieldNorthBarcode.Text.Trim() != "")
        {
            paras.Barcode = TextFieldNorthBarcode.Text.Trim();
        }
        if (ComboBoxNorthRecordStat.Value.ToString() != "")
        {
            paras.RecordStat = ComboBoxNorthRecordStat.Value.ToString();
        }
        if (ComboBoxNorthRecorder.Value.ToString() != "")
        {
            paras.RecorderId = ComboBoxNorthRecorder.Value.ToString();
        }

        DataSet ds = GetDataSetByParams(paras);

        StoreCenterMaster.DataSource = ds;
        StoreCenterMaster.DataBind();

    }
    public DataSet GetDataSetByParams(IQmcCheckDataQueryParams paras)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT A.*");
        sb.AppendLine(", CASE A.CheckResult WHEN '1' THEN '合格' WHEN '0' THEN '不合格' ELSE '' END CheckResultDes");
        sb.AppendLine(", CASE ISNULL(A.RecordStat, 0) WHEN '0' THEN '未提交' WHEN '1' THEN '已提交' ELSE '' END RecordStatDes");
        sb.AppendLine(", CASE ISNULL(A.ApproveFlag, 0) WHEN '0' THEN '未审核' WHEN '1' THEN '已审核' ELSE '' END ApproveStatDes");
        sb.AppendLine(", B.MaterialName MaterName");
        sb.AppendLine(", C.FacName SupplyFacName");
        sb.AppendLine(", D.FacName ProductFacName");
        sb.AppendLine(", E.UserName RecorderName");
        sb.AppendLine(", F.UserName LastModifierName");
        sb.AppendLine(", G.SpecName");
        sb.AppendLine(", H.StandardName");
        sb.AppendLine(", CASE A.FXFlag WHEN '1' THEN '放行'  ELSE '' END FXFlag2");

        sb.AppendLine("FROM QmcCheckData A");
        sb.AppendLine("LEFT JOIN BasMaterial B ON A.MaterCode = B.MaterialCode");
        sb.AppendLine("LEFT JOIN BasFactoryInfo C ON A.SupplyFac = C.ObjID");
        sb.AppendLine("LEFT JOIN BasFactoryInfo D ON A.ProductFac = D.ObjID");
        sb.AppendLine("LEFT JOIN BasUser E ON A.RecorderId = E.WorkBarcode");
        sb.AppendLine("LEFT JOIN BasUser F ON A.LastModifierId = F.WorkBarcode");
        sb.AppendLine("Left Join QmcSpec G On A.SpecId = G.SpecId");
        sb.AppendLine("Left Join QmcStandard H On A.StandardId = H.StandardId");
        sb.AppendLine("WHERE A.DeleteFlag = '0'");
        if (paras.BillNo != null && paras.BillNo != "")
        {
            sb.AppendFormat("AND A.BillNo LIKE '%{0}%'", paras.BillNo);
            sb.AppendLine();
        }
        if (paras.Barcode != null && paras.Barcode != "")
        {
            sb.AppendFormat("AND A.BatchCode LIKE '%{0}%'", paras.Barcode);
            sb.AppendLine();
        }
        if (!string.IsNullOrEmpty(TextField2.Text))
        {
            sb.AppendFormat("AND A.BarCode LIKE '%{0}%'", TextField2.Text);
            sb.AppendLine();
        }
        if (paras.MaterCode != null && paras.MaterCode != "")
        {
            sb.AppendFormat("AND A.MaterCode = '{0}'", paras.MaterCode);
            sb.AppendLine();
        }
        else if (paras.MinorTypeID != null && paras.MinorTypeID != "")
        {
            sb.AppendFormat("AND B.MajorTypeID = 1 AND B.MinorTypeID = '{0}'", paras.MinorTypeID);
            sb.AppendLine();
        }
        if (paras.BeginCheckDate != null && paras.BeginCheckDate != "")
        {
            sb.AppendFormat("AND A.CheckDate >= '{0}'", paras.BeginCheckDate);
            sb.AppendLine();
        }
        if (paras.EndCheckDate != null && paras.EndCheckDate != "")
        {
            sb.AppendFormat("AND A.CheckDate <= '{0}'", paras.EndCheckDate);
            sb.AppendLine();
        }
        if (paras.SupplyFacId != null && paras.SupplyFacId != "")
        {
            sb.AppendFormat("AND A.SupplyFac = {0}", paras.SupplyFacId);
            sb.AppendLine();
        }
        if (paras.ProductFacId != null && paras.ProductFacId != "")
        {
            sb.AppendFormat("AND A.ProductFac = {0}", paras.ProductFacId);
            sb.AppendLine();
        }
        if (paras.CheckResult != null && paras.CheckResult != "")
        {
            sb.AppendFormat("AND A.CheckResult = '{0}'", paras.CheckResult);
            sb.AppendLine();
        }
        if (paras.RecordStat != null && paras.RecordStat != "")
        {
            sb.AppendFormat("AND ISNULL(A.RecordStat, 0) = {0}", paras.RecordStat);
            sb.AppendLine();
        }
        if (paras.RecorderId != null && paras.RecorderId != "")
        {
            sb.AppendFormat("AND ISNULL(A.RecorderId, '') = {0}", paras.RecorderId);
            sb.AppendLine();
        }

        sb.AppendLine("ORDER BY A.RecordTime DESC");
        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        return bQmcCheckDataManager.GetBySql(sb.ToString()).ToDataSet();
    }

    /// <summary>
    /// 填充属性和检测项的控件集合
    /// </summary>
    /// <param name="materCode"></param>
    /// <param name="frequency"></param>
    /// <returns></returns>
    [DirectMethod]
    public bool DMFillCheckDataSet(string materCode)
    {
        IQmcSpecManager specManager = new QmcSpecManager();
        QmcSpec spec = new QmcSpec();
        string specId = (string)HiddenMasterSpecId.Value;
        if ((specId != String.Empty) && (specId != ""))
        {
            spec = specManager.GetById(specId);
            TextFieldMasterSpec.SetValue(spec.SpecName);
        }
        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
            BasMaterial._.MaterialCode == materCode
            , BasMaterial._.ObjID.Asc);

        IBasMaterialMinorTypeManager bBasMaterialMinorTypeManager = new BasMaterialMinorTypeManager();
        EntityArrayList<BasMaterialMinorType> mBasMaterialMinorTypeList = bBasMaterialMinorTypeManager.GetListByWhereAndOrder(
            BasMaterialMinorType._.MajorID == mBasMaterialList[0].MajorTypeID
            & BasMaterialMinorType._.MinorTypeID == mBasMaterialList[0].MinorTypeID
            , BasMaterialMinorType._.ObjID.Asc);

        string seriesId = mBasMaterialMinorTypeList[0].ObjID.ToString();

        HiddenMasterSeriesId.SetValue(seriesId);

        FillStandardList();

        FillCheckDataProperty(seriesId);

        string standardId = ComboBoxMasterStandard.Value.ToString();
        if (standardId != "" && materCode != "")
        {
            FillCheckDataDetail(standardId, materCode, "");
        }
        else
        {
            FieldSetMasterDetail.Items.Clear();
        }

        return true;
    }

    /// <summary>
    /// 填充执行标准下拉列表
    /// </summary>
    private void FillStandardList(string standardId = "")
    {
        ComboBoxMasterStandard.GetStore().RemoveAll();

        IQmcStandardManager bQmcStandardManager = new QmcStandardManager();
        standardId = standardId == "" ? "0" : standardId;
        EntityArrayList<QmcStandard> mQmcStandardList = bQmcStandardManager.GetListByWhereAndOrder(
            (QmcStandard._.StandardId == standardId)
            | (QmcStandard._.DeleteFlag == "0"
            & (QmcStandard._.ActivateFlag == "1" | QmcStandard._.ActivateFlag == "2"))
            , QmcStandard._.ActivateDate.Desc);
        foreach (QmcStandard mQmcStandard in mQmcStandardList)
        {
            ComboBoxMasterStandard.AddItem(mQmcStandard.StandardName, mQmcStandard.StandardId.ToString());
        }

    }

    /// <summary>
    /// 填充属性的控件集合
    /// </summary>
    /// <param name="seriesId"></param>
    /// <param name="mQmcCheckDataPropertyList"></param>
    private void FillCheckDataProperty(string seriesId, EntityArrayList<QmcCheckDataProperty> mQmcCheckDataPropertyList = null)
    {
        // 处理属性
        FieldSetMasterProperty.Items.Clear();

        IQmcPropertyManager bQmcPropertyManager = new QmcPropertyManager();
        EntityArrayList<QmcProperty> mQmcPropertyList = bQmcPropertyManager.GetListByWhereAndOrder(
            QmcProperty._.DeleteFlag == "0"
            & QmcProperty._.SeriesId == seriesId
            , QmcProperty._.PropertyId.Asc);

        foreach (QmcProperty mQmcProperty in mQmcPropertyList)
        {
            string propertyValue = "";
            if (mQmcCheckDataPropertyList != null)
            {
                foreach (QmcCheckDataProperty mQmcCheckDataProperty in mQmcCheckDataPropertyList)
                {
                    if (mQmcCheckDataProperty.ItemPropertyId.HasValue
                        && mQmcCheckDataProperty.ItemPropertyId.Value == mQmcProperty.PropertyId)
                    {
                        propertyValue = mQmcCheckDataProperty.PropertyValue;
                    }
                }
            }

            ComponentBase control = null;

            if (mQmcProperty.HasSelection == "1")
            {
                control = new ComboBox();
                (control as ComboBox).FieldLabel = mQmcProperty.PropertyName;
                (control as ComboBox).Editable = false;
                (control as ComboBox).Triggers.Add(new FieldTrigger { Icon = TriggerIcon.Clear, Qtip = "清空" });
                (control as ComboBox).Listeners.TriggerClick.Handler = "this.setValue('');";

                IQmcPropertyValueManager bQmcPropertyValueManager = new QmcPropertyValueManager();
                EntityArrayList<QmcPropertyValue> mQmcPropertyValueList = bQmcPropertyValueManager.GetListByWhereAndOrder(
                    QmcPropertyValue._.DeleteFlag == "0"
                    & QmcPropertyValue._.PropertyId == mQmcProperty.PropertyId
                    , QmcPropertyValue._.ValueId.Asc);
                foreach (QmcPropertyValue mQmcPropertyValue in mQmcPropertyValueList)
                {
                    (control as ComboBox).Items.Add(new Ext.Net.ListItem(mQmcPropertyValue.PropertyValue, mQmcPropertyValue.ValueId.ToString()));
                }
                (control as ComboBox).SetValue(propertyValue);
            }
            else
            {
                switch (mQmcProperty.ValueType)
                {
                    case "文字":
                        control = new TextField();
                        (control as TextField).FieldLabel = mQmcProperty.PropertyName;
                        (control as TextField).SetValue(propertyValue);
                        break;
                    case "数字":
                        control = new NumberField();
                        (control as NumberField).FieldLabel = mQmcProperty.PropertyName;
                        (control as NumberField).SetValue(propertyValue);
                        break;
                    case "日期":
                        control = new DateField();
                        (control as DateField).FieldLabel = mQmcProperty.PropertyName;
                        (control as DateField).Editable = false;
                        (control as DateField).Format = "yyyy-MM-dd";

                        (control as DateField).Triggers.Add(new FieldTrigger { Icon = TriggerIcon.Clear, Qtip = "清空" });
                        (control as DateField).Listeners.TriggerClick.Handler = "this.setValue('');";
                        (control as DateField).SetValue(propertyValue);
                        break;
                    default:
                        control = new TextField();
                        (control as TextField).FieldLabel = mQmcProperty.PropertyName;
                        (control as TextField).SetValue(propertyValue);
                        break;
                }
            }
            control.ColumnWidth = 0.5;
            control.Padding = 2;
            control.ID = "MasterProperty_" + mQmcProperty.PropertyId.ToString();
            FieldSetMasterProperty.Items.Add(control);
        }
        if (mQmcPropertyList.Count > 0)
        {
            int rowNum = mQmcPropertyList.Count % 2 == 0 ? mQmcPropertyList.Count / 2 : (mQmcPropertyList.Count + 1) / 2;
            FieldSetMasterProperty.Height = Unit.Pixel(rowNum * 45);
        }
        else
        {
            FieldSetMasterProperty.Height = Unit.Pixel(100);
        }
        FieldSetMasterProperty.UpdateContent();

    }

    /// <summary>
    /// 填充检测项的控件集合
    /// </summary>
    /// <param name="standardId"></param>
    /// <param name="materCode"></param>
    /// <param name="frequency"></param>
    /// <param name="mQmcCheckDataDetailList"></param>
    private void FillCheckDataDetail(string standardId, string materCode, string checkId)
    {
        // 处理测试项
        FieldSetMasterDetail.Items.Clear();

        if (standardId == "" || materCode == "")
        {
            FieldSetMasterDetail.Height = Unit.Pixel(100);
            FieldSetMasterDetail.UpdateContent();
            return;
        }

        // 查找原材料检测指标
        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        IQmcCheckItemDetailManager bQmcCheckItemDetailManager = new QmcCheckItemDetailManager();
        IQmcCheckDataQueryItemDetailParams paras = new QmcCheckDataQueryItemDetailParams();
        paras.StandardId = standardId;
        paras.MaterCode = materCode;
        EntityArrayList<QmcCheckItemDetail> mQmcCheckItemDetailList = bQmcCheckDataManager.GetCheckItemDetailByParams(paras);

        EntityArrayList<QmcCheckDataDetail> mQmcCheckDataDetailList = null;
        if (checkId != "")
        {
            // 查找检测记录明细
            IQmcCheckDataDetailManager bQmcCheckDataDetailManager = new QmcCheckDataDetailManager();
            mQmcCheckDataDetailList = bQmcCheckDataDetailManager.GetListByWhereAndOrder(
                QmcCheckDataDetail._.CheckId == checkId
                , QmcCheckDataDetail._.DetailId.Asc);
        }

        IQmcCheckItemManager bQmcCheckItemManager = new QmcCheckItemManager();

        foreach (QmcCheckItemDetail mQmcCheckItemDetail in mQmcCheckItemDetailList)
        {
            if (mQmcCheckItemDetail.ItemId.ToString() == "")
            {
                continue;
            }

            QmcCheckItem mQmcCheckItem = bQmcCheckItemManager.GetById(mQmcCheckItemDetail.ItemId);
            //过滤掉已逻辑删除的检测项
            if (mQmcCheckItem.DeleteFlag == "1")
            {
                continue;
            }

            string checkValue = "";
            string MinValue = "";
            string MaxValue = "";
            string autoCheckResult = "";
            string isPrime = "";
            if (mQmcCheckDataDetailList != null)
            {
                foreach (QmcCheckDataDetail mQmcCheckDataDetail in mQmcCheckDataDetailList)
                {
                    if (mQmcCheckDataDetail.ItemDetailId.HasValue == true
                        && mQmcCheckDataDetail.ItemDetailId.Value == mQmcCheckItemDetail.ItemDetailId)
                    {
                        checkValue = mQmcCheckDataDetail.CheckValue;
                        MinValue = mQmcCheckDataDetail.MinValue;
                        MaxValue = mQmcCheckDataDetail.MaxValue;
                        autoCheckResult = mQmcCheckDataDetail.AutoCheckResult.ToString();
                        isPrime = mQmcCheckDataDetail.IsPrime.ToString();
                    }
                    else
                    {
                        ///对于版本号不一样的检测标准的处理
                        QmcCheckItemDetail itemDetailFromData = bQmcCheckItemDetailManager.GetById(mQmcCheckDataDetail.ItemDetailId.Value);
                        QmcCheckItem cItem = bQmcCheckItemManager.GetById(itemDetailFromData.ItemId);
                        QmcCheckItem cItemFromItem = bQmcCheckItemManager.GetById(mQmcCheckItemDetail.ItemId);
                        if ((mQmcCheckDataDetail.ItemDetailId.HasValue == true) && (cItem.ItemName == cItemFromItem.ItemName))
                        {
                            checkValue = mQmcCheckDataDetail.CheckValue;
                            MinValue = mQmcCheckDataDetail.MinValue;
                            MaxValue = mQmcCheckDataDetail.MaxValue;
                            autoCheckResult = mQmcCheckDataDetail.AutoCheckResult.ToString();
                            isPrime = mQmcCheckDataDetail.IsPrime.ToString();
                        }
                    }
                }
            }

            ComponentBase control = null;
            ComponentBase controlMin = null;
            ComponentBase controlMax = null;
            ComponentBase subcontrol = new ComboBox();//用于选择文字类型的单项检测结论的子控件
            Ext.Net.ListItem goodItem = new Ext.Net.ListItem();
            goodItem.Text = "合格";
            goodItem.Value = "1";
            Ext.Net.ListItem badItem = new Ext.Net.ListItem();
            badItem.Text = "不合格";
            badItem.Value = "0";
            Ext.Net.ListItem primeItem = new Ext.Net.ListItem();
            primeItem.Text = "一级品";
            primeItem.Value = "2";
            (subcontrol as ComboBox).Items.Add(goodItem);
            (subcontrol as ComboBox).Items.Add(badItem);
            (subcontrol as ComboBox).Items.Add(primeItem);
            (subcontrol as ComboBox).Editable = false;
            (subcontrol as ComboBox).SetValue("1");
            if (mQmcCheckItem.ValueType == "数字")
            {
                //2014年10月15日10:09:08 修改
                control = new TextField();
                (control as TextField).SetValue(checkValue);
                controlMin = new TextField();
                (controlMin as TextField).SetValue(MinValue);
                controlMax = new TextField();
                (controlMax as TextField).SetValue(MaxValue);
            }
            else
            {
                control = new TextField();
                (control as TextField).SetValue(checkValue);
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(mQmcCheckItem.ItemName);
            if (mQmcCheckItemDetail.Frequency != "")
            {
                sb.AppendFormat("({0})", mQmcCheckItemDetail.Frequency);
            }
            //sb.Append("<br>");
            string goodCheckRange = "";
            if (mQmcCheckItem.ValueType == "数字")
            {
                goodCheckRange = GetCheckRange(mQmcCheckItemDetail, EnumCheckGrade.Good);
            }
            else
            {
                goodCheckRange = mQmcCheckItemDetail.GoodDisplayValue;
                if (goodCheckRange.Trim() == "")
                {
                    goodCheckRange = "　";
                }
            }
            sb.AppendFormat("{0}", goodCheckRange);
            if (mQmcCheckItem.ValueType == "数字")
            {
                (control as TextFieldBase).FieldLabel = sb.ToString();
                (control as TextFieldBase).LabelSeparator = "";
                control.ColumnWidth = 0.5;
                (control as TextFieldBase).LabelWidth = 250;
                (control as TextFieldBase).InputWidth = 100;
                (control as TextFieldBase).LabelAlign = LabelAlign.Right;
                control.Padding = 1;
                control.ID = "MasterCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString();
                FieldSetMasterDetail.Items.Add(control);
                //<br> 
                (controlMin as TextFieldBase).FieldLabel = "最小值"; 
                (controlMin as TextFieldBase).LabelSeparator = "";
                controlMin.ColumnWidth = 0.25;
                (controlMin as TextFieldBase).LabelWidth = 50;
                (controlMin as TextFieldBase).InputWidth = 100;
                (controlMin as TextFieldBase).LabelAlign = LabelAlign.Right;
                controlMin.Padding = 1;
                controlMin.ID = "MasterCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString() + "_MIN";
                FieldSetMasterDetail.Items.Add(controlMin);


                (controlMax as TextFieldBase).FieldLabel = "最大值";
                (controlMax as TextFieldBase).LabelSeparator = "";
                controlMax.ColumnWidth = 0.25;
                (controlMax as TextFieldBase).LabelWidth = 50;
                (controlMax as TextFieldBase).InputWidth = 100;
                (controlMax as TextFieldBase).LabelAlign = LabelAlign.Right;
                controlMax.Padding = 1;
                controlMax.ID = "MasterCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString() + "_MAX";
                FieldSetMasterDetail.Items.Add(controlMax);
            }
            else
            {
                (control as TextFieldBase).FieldLabel = sb.ToString();
                (control as TextFieldBase).LabelSeparator = "";
                control.ColumnWidth = 0.5;
                (control as TextFieldBase).LabelWidth = 250;
                (control as TextFieldBase).InputWidth = 100;
                (control as TextFieldBase).LabelAlign = LabelAlign.Right;
                control.Padding = 1;
                control.ID = "MasterCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString();
                FieldSetMasterDetail.Items.Add(control);
                (subcontrol as ComboBox).FieldLabel = "";
                (subcontrol as ComboBox).LabelSeparator = "";
                subcontrol.ColumnWidth = 0.4;
                (subcontrol as ComboBox).LabelWidth = 0;
                (subcontrol as ComboBox).InputWidth = 72;
                subcontrol.ID = "MasterCheckItemDetailSub_" + mQmcCheckItemDetail.ItemDetailId.ToString();
                (subcontrol as ComboBox).LabelAlign = LabelAlign.Right;
                if (autoCheckResult != "")
                {
                    if (isPrime != "1")
                    {
                        (subcontrol as ComboBox).SetValue(autoCheckResult);
                    }
                    else
                    {
                        (subcontrol as ComboBox).SetValue("2");
                    }
                }
                subcontrol.Padding = 1;
                //subcontrol.PaddingSpec = "5 0 0 5";
                FieldSetMasterDetail.Items.Add(subcontrol);
            }
        }
        if (mQmcCheckItemDetailList.Count > 0)
        {
            int rowNum = mQmcCheckItemDetailList.Count % 2 == 0 ? mQmcCheckItemDetailList.Count / 2 : (mQmcCheckItemDetailList.Count + 1) / 2;
            FieldSetMasterDetail.Height = Unit.Pixel(rowNum * 65);
        }
        else
        {
            FieldSetMasterDetail.Height = Unit.Pixel(100);
        }

        FieldSetMasterDetail.UpdateContent();
    }

    /// <summary>
    /// 填充检测项的控件集合（审批窗口）
    /// </summary>
    /// <param name="standardId"></param>
    /// <param name="materCode"></param>
    /// <param name="frequency"></param>
    /// <param name="mQmcCheckDataDetailList"></param>
    private void FillApproveDataDetail(string standardId, string materCode, string checkId)
    {
        // 处理测试项
        FieldApproveMain.Items.Clear();

        if (standardId == "" || materCode == "")
        {
            FieldApproveMain.Height = Unit.Pixel(100);
            FieldApproveMain.UpdateContent();
            return;
        }

        // 查找原材料检测指标
        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        IQmcCheckItemDetailManager bQmcCheckItemDetailManager = new QmcCheckItemDetailManager();
        IQmcCheckDataQueryItemDetailParams paras = new QmcCheckDataQueryItemDetailParams();
        paras.StandardId = standardId;
        paras.MaterCode = materCode;
        EntityArrayList<QmcCheckItemDetail> mQmcCheckItemDetailList = bQmcCheckDataManager.GetCheckItemDetailByParams(paras);

        EntityArrayList<QmcCheckDataDetail> mQmcCheckDataDetailList = null;
        if (checkId != "")
        {
            // 查找检测记录明细
            IQmcCheckDataDetailManager bQmcCheckDataDetailManager = new QmcCheckDataDetailManager();
            mQmcCheckDataDetailList = bQmcCheckDataDetailManager.GetListByWhereAndOrder(
                QmcCheckDataDetail._.CheckId == checkId
                , QmcCheckDataDetail._.DetailId.Asc);
        }

        IQmcCheckItemManager bQmcCheckItemManager = new QmcCheckItemManager();

        foreach (QmcCheckItemDetail mQmcCheckItemDetail in mQmcCheckItemDetailList)
        {
            if (mQmcCheckItemDetail.ItemId.ToString() == "")
            {
                continue;
            }

            QmcCheckItem mQmcCheckItem = bQmcCheckItemManager.GetById(mQmcCheckItemDetail.ItemId);
            //过滤掉已逻辑删除的检测项
            if (mQmcCheckItem.DeleteFlag == "1")
            {
                continue;
            }

            string checkValue = "";
            string autoCheckResult = "";
            string isPrime = "";
            if (mQmcCheckDataDetailList != null)
            {
                foreach (QmcCheckDataDetail mQmcCheckDataDetail in mQmcCheckDataDetailList)
                {
                    if (mQmcCheckDataDetail.ItemDetailId.HasValue == true
                        && mQmcCheckDataDetail.ItemDetailId.Value == mQmcCheckItemDetail.ItemDetailId)
                    {
                        checkValue = mQmcCheckDataDetail.CheckValue;
                        autoCheckResult = mQmcCheckDataDetail.AutoCheckResult.ToString();
                        isPrime = mQmcCheckDataDetail.IsPrime.ToString();
                    }
                    else
                    {
                        ///对于版本号不一样的检测标准的处理
                        QmcCheckItemDetail itemDetailFromData = bQmcCheckItemDetailManager.GetById(mQmcCheckDataDetail.ItemDetailId.Value);
                        QmcCheckItem cItem = bQmcCheckItemManager.GetById(itemDetailFromData.ItemId);
                        QmcCheckItem cItemFromItem = bQmcCheckItemManager.GetById(mQmcCheckItemDetail.ItemId);
                        if ((mQmcCheckDataDetail.ItemDetailId.HasValue == true) && (cItem.ItemName == cItemFromItem.ItemName))
                        {
                            checkValue = mQmcCheckDataDetail.CheckValue;
                            autoCheckResult = mQmcCheckDataDetail.AutoCheckResult.ToString();
                            isPrime = mQmcCheckDataDetail.IsPrime.ToString();
                        }
                    }
                }
            }

            ComponentBase control = null;
            ComponentBase subcontrol = new TextField();//用于选择文字类型的单项检测结论的子控件
            control = new TextField();
            (control as TextFieldBase).SetValue(checkValue);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(mQmcCheckItem.ItemName);
            if (mQmcCheckItemDetail.Frequency != "")
            {
                sb.AppendFormat("({0})", mQmcCheckItemDetail.Frequency);
            }
            sb.Append("<br>");
            string goodCheckRange = "";
            if (mQmcCheckItem.ValueType == "数字")
            {
                goodCheckRange = GetCheckRange(mQmcCheckItemDetail, EnumCheckGrade.Good);
            }
            else
            {
                goodCheckRange = mQmcCheckItemDetail.GoodDisplayValue;
                if (goodCheckRange.Trim() == "")
                {
                    goodCheckRange = "　";
                }
            }
            sb.AppendFormat("{0}", goodCheckRange);
            (control as TextFieldBase).FieldLabel = sb.ToString();
            (control as TextFieldBase).LabelSeparator = "";
            control.ColumnWidth = 0.3;
            (control as TextFieldBase).LabelWidth = 150;
            (control as TextFieldBase).InputWidth = 120;
            (control as TextFieldBase).LabelAlign = LabelAlign.Right;
            control.Padding = 1;
            control.ID = "ApprovalCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString();
            (control as TextField).ReadOnly = true;
            FieldApproveMain.Items.Add(control);
            (subcontrol as TextFieldBase).FieldLabel = "";
            (subcontrol as TextFieldBase).LabelSeparator = "";
            subcontrol.ColumnWidth = 0.2;
            (subcontrol as TextFieldBase).LabelWidth = 0;
            (subcontrol as TextFieldBase).InputWidth = 72;
            subcontrol.ID = "ApprovalCheckItemDetailSub_" + mQmcCheckItemDetail.ItemDetailId.ToString();
            (subcontrol as TextFieldBase).LabelAlign = LabelAlign.Right;
            if (autoCheckResult != "")
            {
                if (isPrime == "1")
                {
                    (subcontrol as TextFieldBase).SetValue("一级品");
                }
                else if(autoCheckResult == "0")
                {
                    (subcontrol as TextFieldBase).SetValue("不合格");
                }
                else if (autoCheckResult == "1")
                {
                    (subcontrol as TextFieldBase).SetValue("合格");
                }
            }
            subcontrol.PaddingSpec = "5 0 0 65";
            (subcontrol as TextField).ReadOnly = true;
            FieldApproveMain.Items.Add(subcontrol);
        }
        if (mQmcCheckItemDetailList.Count > 0)
        {
            int rowNum = mQmcCheckItemDetailList.Count % 2 == 0 ? mQmcCheckItemDetailList.Count / 2 : (mQmcCheckItemDetailList.Count + 1) / 2;
            FieldApproveMain.Height = Unit.Pixel(rowNum * 65);
        }
        else
        {
            FieldApproveMain.Height = Unit.Pixel(100);
        }

        FieldApproveMain.UpdateContent();
    }

    /// <summary>
    /// 获取检测指标范围
    /// </summary>
    /// <param name="mQmcCheckItemDetail"></param>
    /// <param name="enumCheckGrade"></param>
    /// <returns></returns>
    private string GetCheckRange(QmcCheckItemDetail mQmcCheckItemDetail, EnumCheckGrade enumCheckGrade)
    {
        string CheckRange = "";
        if (enumCheckGrade == EnumCheckGrade.Good)
        {
            CheckRange = mQmcCheckItemDetail.GoodDisplayValue;
        }
        else if (enumCheckGrade == EnumCheckGrade.Prime)
        {
            CheckRange = mQmcCheckItemDetail.PrimeDisplayValue;
        }
        return CheckRange;
    }

    /// <summary>
    /// 保存前校验数据
    /// </summary>
    /// <returns></returns>
    [DirectMethod]
    public bool DMValidateCheckData()
    {
        return ValidateCheckData();
    }

    /// <summary>
    /// 校验检测频次
    /// </summary>
    /// <returns></returns>
    [DirectMethod]
    public string DMValidFrequency(Newtonsoft.Json.Linq.JObject jObjectDetail)
    {
        string materCode = (string)HiddenMasterMaterCode.Value;
        string frequency = (string)TextFieldMasterFrequency.Value;
        IQmcCheckItemDetailManager bQmcCheckItemDetailManager = new QmcCheckItemDetailManager();
        EntityArrayList<QmcCheckItemDetail> mQmcCheckItemDetailList = bQmcCheckItemDetailManager.GetListByWhereAndOrder(
            QmcCheckItemDetail._.DeleteFlag == "0"
            & QmcCheckItemDetail._.MaterialCode == materCode
            , QmcCheckItemDetail._.ItemDetailId.Asc);
        int nullCount = 0;
        foreach (QmcCheckItemDetail mQmcCheckItemDetail in mQmcCheckItemDetailList)
        {
            Newtonsoft.Json.Linq.JToken jTokenDetail = null;
            if (jObjectDetail.TryGetValue("MasterCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString(), out jTokenDetail) == true)
            {
                string checkValue = jTokenDetail.ToString().Trim();
                if (frequency.Contains(mQmcCheckItemDetail.Frequency))
                {
                    if (checkValue == "")
                    {
                        nullCount++;
                    }
                }
            }
        }
        if (nullCount > 0)
        {
            return "有" + nullCount + "条此原材料必检的检测项还未输入，";
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 保存前校验数据
    /// </summary>
    /// <returns></returns>
    private bool ValidateCheckData()
    {
        string commandName = HiddenMasterCommandName.Value.ToString();
        if (HiddenMasterLedgerId.Value.ToString().Trim() == "")
        {
            X.Msg.Alert("提示", "请选择样品台账").Show();
            return false;
        }

        if (DateFieldMasterCheckDate.RawText == null || DateFieldMasterCheckDate.RawText == "")
        {
            X.Msg.Alert("提示", "请选择检验日期").Show();
            return false;
        }
        if ((ComboBoxMasterCheckResult.Value.ToString()) == "" && (commandName == "SpecUpdate"))
        {
            X.Msg.Alert("提示", "请选择检验结果").Show();
            return false;
        }

        return true;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    /// <param name="jObjectProperty"></param>
    /// <param name="jObjectDetail"></param>
    /// <returns></returns>
    [DirectMethod]
    public bool DMSaveCheckData(Newtonsoft.Json.Linq.JObject jObjectProperty, Newtonsoft.Json.Linq.JObject jObjectDetail)
    {
        if (ValidateCheckData() == false)
        {
            return false;
        }

        string ledgerId = HiddenMasterLedgerId.Value.ToString();
        string billNo = HiddenMasterBillNo.Value.ToString().Trim();
        string barcode = HiddenMasterBarcode.Value.ToString().Trim();
        string batchCode = HiddenMasterBatchCode.Value.ToString().Trim();
        string orderID = HiddenMasterOrderID.Value.ToString();
        string materCode = HiddenMasterMaterCode.Value.ToString();
        string supplyFacId = HiddenMasterSupplyFacId.Value.ToString();
        string productFacId = HiddenMasterProductFacId.Value.ToString();
        string checkDate = DateFieldMasterCheckDate.RawText;
        string checkResult = ComboBoxMasterCheckResult.Value.ToString();
        string remark = TextFieldMasterRemark.Text.Trim();
        string recordStat = CheckboxMasterRecordStat.Checked == true ? "1" : "0";
        string specId = HiddenMasterSpecId.Value.ToString();
        string frequency = TextFieldMasterFrequency.Text;
        string standardId = ComboBoxMasterStandard.Value.ToString();

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();

        string commandName = HiddenMasterCommandName.Value.ToString();
        QmcCheckData mQmcCheckData;
        string checkId = "";
        if (commandName == "Insert")
        {
            mQmcCheckData = new QmcCheckData();
            mQmcCheckData.ApproveFlag = "0";
        }
        else
        {
            checkId = HiddenMasterCheckId.Value.ToString();
            if (checkId == "")
            {
                X.Msg.Alert("提示", "未找到要修改的记录").Show();
                return false;
            }
            mQmcCheckData = bQmcCheckDataManager.GetById(checkId);
            if (commandName == "Update")
            {
                if (mQmcCheckData.RecordStat.HasValue == true
                    && mQmcCheckData.RecordStat.Value == 1)
                {
                    X.Msg.Alert("提示", "记录已提交，不允许修改").Show();
                    return false;
                }
            }
            else if (commandName == "SpecUpdate")
            {
                if (mQmcCheckData.RecordStat.HasValue == false
                    || mQmcCheckData.RecordStat.Value == 0)
                {
                    X.Msg.Alert("提示", "记录未提交，不允许修改").Show();
                    return false;
                }
            }
            else
            {
                X.Msg.Alert("提示", "未知操作").Show();
                return false;
            }
        }
        mQmcCheckData.LedgerId = Convert.ToInt32(ledgerId);
        mQmcCheckData.BillNo = billNo;
        mQmcCheckData.Barcode = barcode;
        mQmcCheckData.BatchCode = batchCode;
        mQmcCheckData.Frequency = frequency;
        mQmcCheckData.OrderID = Convert.ToInt32(orderID);
        mQmcCheckData.MaterCode = materCode;
        if (supplyFacId != "")
        {
            mQmcCheckData.SupplyFac = Convert.ToInt32(supplyFacId);
        }
        else
        {
            mQmcCheckData.SupplyFac = null;
        }
        if (productFacId != "")
        {
            mQmcCheckData.ProductFac = Convert.ToInt32(productFacId);
        }
        else
        {
            mQmcCheckData.ProductFac = null;
        }
        mQmcCheckData.CheckDate = DateTime.Parse(checkDate);
        mQmcCheckData.Remark = remark;
        mQmcCheckData.Remark = TextFieldMasterBatchCode.Text;
        if (specId != "")
        {
            mQmcCheckData.SpecId = Convert.ToInt32(specId);
        }
        else
        {
            mQmcCheckData.SpecId = null;
        }
        if (commandName == "Insert")
        {
            mQmcCheckData.RecorderId = this.UserID;
            mQmcCheckData.RecordTime = DateTime.Now;
        }
        mQmcCheckData.LastModifierId = this.UserID;
        mQmcCheckData.LastModifyTime = DateTime.Now;
        mQmcCheckData.DeleteFlag = "0";
        mQmcCheckData.RecordStat = Convert.ToInt32(recordStat);
        mQmcCheckData.StandardId = Convert.ToInt32(standardId);

        string seriesId = HiddenMasterSeriesId.Value.ToString();

        // 属性
        EntityArrayList<QmcCheckDataProperty> mQmcCheckDataPropertyList = new EntityArrayList<QmcCheckDataProperty>();

        IQmcPropertyManager bQmcPropertyManager = new QmcPropertyManager();
        EntityArrayList<QmcProperty> mQmcPropertyList = bQmcPropertyManager.GetListByWhereAndOrder(
            QmcProperty._.DeleteFlag == "0"
            & QmcProperty._.SeriesId == seriesId
            , QmcProperty._.PropertyId.Asc);
        foreach (QmcProperty mQmcProperty in mQmcPropertyList)
        {
            Newtonsoft.Json.Linq.JToken jTokenProperty = null;
            if (jObjectProperty.TryGetValue("MasterProperty_" + mQmcProperty.PropertyId.ToString(), out jTokenProperty) == true)
            {
                QmcCheckDataProperty mQmcCheckDataProperty = new QmcCheckDataProperty();
                mQmcCheckDataProperty.ItemPropertyId = mQmcProperty.PropertyId;
                mQmcCheckDataProperty.PropertyValue = jTokenProperty.ToString().Trim();
                mQmcCheckDataPropertyList.Add(mQmcCheckDataProperty);
            }

        }

        bool CheckResultFlag = true;
        // 测试项
        EntityArrayList<QmcCheckDataDetail> mQmcCheckDataDetailList = new EntityArrayList<QmcCheckDataDetail>();
        IQmcCheckItemDetailManager bQmcCheckItemDetailManager = new QmcCheckItemDetailManager();
        EntityArrayList<QmcCheckItemDetail> mQmcCheckItemDetailList = bQmcCheckItemDetailManager.GetListByWhereAndOrder(
            QmcCheckItemDetail._.DeleteFlag == "0"
            & QmcCheckItemDetail._.MaterialCode == materCode
            , QmcCheckItemDetail._.ItemDetailId.Asc);
        foreach (QmcCheckItemDetail mQmcCheckItemDetail in mQmcCheckItemDetailList)
        {
            Newtonsoft.Json.Linq.JToken jTokenDetail = null;
            Newtonsoft.Json.Linq.JToken jTokenSub = null;
            if (jObjectDetail.TryGetValue("MasterCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString(), out jTokenDetail) == true)
            {
                QmcCheckDataDetail mQmcCheckDataDetail = new QmcCheckDataDetail();
                mQmcCheckDataDetail.ItemDetailId = mQmcCheckItemDetail.ItemDetailId;
                string checkValue = jTokenDetail.ToString().Trim();
                mQmcCheckDataDetail.CheckValue = checkValue;
                string goodCheckRange = "";
                int? autoCheckResult = null;
                if (mQmcCheckItemDetail.GoodOperator == null || mQmcCheckItemDetail.GoodOperator.Trim() == "")
                {
                    goodCheckRange = mQmcCheckItemDetail.GoodTextValue;
                    autoCheckResult = 1;
                }
                else
                {
                    goodCheckRange = GetCheckRange(mQmcCheckItemDetail, EnumCheckGrade.Good);
                    if (checkValue != "")
                    {
                        try
                        {
                            Convert.ToDecimal(checkValue);
                        }
                        catch (Exception)
                        {

                            IQmcCheckItemManager itemManager = new QmcCheckItemManager();
                            QmcCheckItem item = itemManager.GetById(mQmcCheckItemDetail.ItemId);
                            new Ext.Net.MessageBox().Alert("提示", item == null ? "" : item.ItemName + "请填写有效数字!").Show();
                            return false;
                        }
                        autoCheckResult = GetCheckResult(Convert.ToDecimal(checkValue)
                            , mQmcCheckItemDetail, EnumCheckGrade.Good);
                    }
                }
                string primeCheckRange = "";
                int? isPrime = null;
                if (mQmcCheckItemDetail.PrimeOperator == null || mQmcCheckItemDetail.PrimeOperator.Trim() == "")
                {
                    primeCheckRange = mQmcCheckItemDetail.PrimeTextValue;
                }
                else
                {
                    
                    primeCheckRange = GetCheckRange(mQmcCheckItemDetail, EnumCheckGrade.Prime);
                    if (autoCheckResult == 1)
                    {
                        try
                        {
                            Convert.ToDecimal(checkValue);
                        }
                        catch (Exception)
                        {

                            IQmcCheckItemManager itemManager = new QmcCheckItemManager();
                            QmcCheckItem item = itemManager.GetById(mQmcCheckItemDetail.ItemId);
                            new Ext.Net.MessageBox().Alert("提示", item == null ? "" : item.ItemName + "请填写有效数字!").Show();
                            return false;
                        }
                        isPrime = GetCheckResult(Convert.ToDecimal(checkValue)
                            , mQmcCheckItemDetail, EnumCheckGrade.Prime);
                    }
                }
                //文字手动判级的结果
                if (jObjectDetail.TryGetValue("MasterCheckItemDetailSub_" + mQmcCheckItemDetail.ItemDetailId.ToString(), out jTokenSub) == true)
                {
                    if (jTokenSub.ToString().Trim() != "")
                    {
                        if (jTokenSub.ToString().Trim() != "2")
                        {
                            autoCheckResult = Convert.ToInt32(jTokenSub.ToString().Trim());
                        }
                        else
                        {
                            autoCheckResult = 1;
                            isPrime = 1;
                        }
                    }
                }
                mQmcCheckDataDetail.GoodCheckRange = goodCheckRange;
              
                mQmcCheckDataDetail.PrimeCheckRange = primeCheckRange;
                mQmcCheckDataDetail.IsPrime = isPrime;
                if (jObjectDetail.TryGetValue("MasterCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString() + "_MIN", out jTokenDetail) == true)
                {
                    mQmcCheckDataDetail.MinValue = jTokenDetail.ToString().Trim();
                    if (autoCheckResult == 1 && (!string.IsNullOrEmpty(mQmcCheckDataDetail.MinValue)))
                    {
                        autoCheckResult = GetCheckResult(Convert.ToDecimal(mQmcCheckDataDetail.MinValue)
                              , mQmcCheckItemDetail, EnumCheckGrade.Min);

                    }
                }
                if (jObjectDetail.TryGetValue("MasterCheckItemDetail_" + mQmcCheckItemDetail.ItemDetailId.ToString() + "_MAX", out jTokenDetail) == true)
                { mQmcCheckDataDetail.MaxValue = jTokenDetail.ToString().Trim();
                if (autoCheckResult == 1 && (!string.IsNullOrEmpty(mQmcCheckDataDetail.MaxValue)))
                {
                    autoCheckResult = GetCheckResult(Convert.ToDecimal(mQmcCheckDataDetail.MaxValue)
                          , mQmcCheckItemDetail, EnumCheckGrade.Max);

                }
                }
                mQmcCheckDataDetail.AutoCheckResult = autoCheckResult;
                if (autoCheckResult == null)
                    mQmcCheckDataDetail.AutoCheckResult = 1;


                mQmcCheckDataDetailList.Add(mQmcCheckDataDetail);
                if (autoCheckResult == 0)
                {
                    CheckResultFlag = false;
                }
            }

        }
        if (CheckResultFlag)
        {
            checkResult = "1";
        }
        else
        {
            checkResult = "0";
        }
        //高级修改时手动判级
        if (commandName == "SpecUpdate")
        {
            checkResult = ComboBoxMasterCheckResult.Value.ToString();
        }
        mQmcCheckData.CheckResult = checkResult;
        QmcCheckData mOriginQmcCheckData = null;
        EntityArrayList<QmcCheckDataProperty> mOriginQmcCheckDataPropertyList = null;
        EntityArrayList<QmcCheckDataDetail> mOriginQmcCheckDataDetailList = null;
        if (commandName == "Update" || commandName == "SpecUpdate")
        {
            mOriginQmcCheckData = bQmcCheckDataManager.GetById(checkId);

            IQmcCheckDataPropertyManager bQmcCheckDataPropertyManager = new QmcCheckDataPropertyManager();
            mOriginQmcCheckDataPropertyList = bQmcCheckDataPropertyManager.GetListByWhereAndOrder(
                QmcCheckDataProperty._.CheckId == checkId
                , QmcCheckDataProperty._.PropertyId.Asc);

            IQmcCheckDataDetailManager bQmcCheckDataDetailManager = new QmcCheckDataDetailManager();
            mOriginQmcCheckDataDetailList = bQmcCheckDataDetailManager.GetListByWhereAndOrder(
                QmcCheckDataDetail._.CheckId == checkId
                , QmcCheckDataDetail._.DetailId.Asc);
        }

        if (commandName == "Insert")
        {
            bQmcCheckDataManager.Insert(mQmcCheckData, mQmcCheckDataPropertyList, mQmcCheckDataDetailList);
        
            X.Msg.Alert("提示", "添加成功").Show();
        }
        else if (commandName == "Update" || commandName == "SpecUpdate")
        {
            bQmcCheckDataManager.Update(mQmcCheckData, mQmcCheckDataPropertyList, mQmcCheckDataDetailList);
            X.Msg.Alert("提示", "修改成功").Show();
        }

        System.Text.StringBuilder originCheckDataInfo = new System.Text.StringBuilder();
        System.Text.StringBuilder modifyCheckDataInfo = new System.Text.StringBuilder();
        System.Text.StringBuilder originCheckDataPropertyInfo = new System.Text.StringBuilder();
        System.Text.StringBuilder modifyCheckDataPropertyInfo = new System.Text.StringBuilder();
        System.Text.StringBuilder originCheckDataDetailInfo = new System.Text.StringBuilder();
        System.Text.StringBuilder modifyCheckDataDetailInfo = new System.Text.StringBuilder();

        if (commandName == "Insert")
        {
        }
        else
        {
            originCheckDataInfo.AppendFormat("CheckId={0}", mOriginQmcCheckData.CheckId.ToString());
            originCheckDataInfo.AppendFormat(",BillNo={0},Barcode={1},OrderID={2}"
                , new string[] { mOriginQmcCheckData.BillNo, mOriginQmcCheckData.Barcode, mOriginQmcCheckData.OrderID.ToString() });
            originCheckDataInfo.AppendFormat(",Frequency={0},SpecId={1}"
                , new string[] { mOriginQmcCheckData.Frequency, mOriginQmcCheckData.SpecId.ToString() });
            originCheckDataInfo.AppendFormat(",LedgerId={0},MaterCode={1},SupplyFacId={2},ProductFacId={3}"
                , new string[] { mOriginQmcCheckData.LedgerId.ToString(), mOriginQmcCheckData.MaterCode
                    , mOriginQmcCheckData.SupplyFac.ToString(), mOriginQmcCheckData.ProductFac.ToString() });
            originCheckDataInfo.AppendFormat(",CheckDate={0},CheckResult={1},RecordStat={2},LastModifierId={3},LastModifyTime={4}"
                , new string[] { mOriginQmcCheckData.CheckDate.HasValue == true ? mOriginQmcCheckData.CheckDate.Value.ToString("yyyy-MM-dd") : ""
                    , mOriginQmcCheckData.CheckResult, mOriginQmcCheckData.RecordStat.ToString(), mOriginQmcCheckData.LastModifierId
                    , mOriginQmcCheckData.LastModifyTime.HasValue == true ? mOriginQmcCheckData.LastModifyTime.Value.ToString("yyyy-MM-dd") : "" });
            foreach (QmcCheckDataProperty mOriginQmcCheckDataProperty in mOriginQmcCheckDataPropertyList)
            {
                originCheckDataPropertyInfo.AppendFormat("PropertyId={0},CheckId={1},ItemPropertyId={2},PropertyValue={3};"
                    , new string[] {mOriginQmcCheckDataProperty.PropertyId.ToString(), mOriginQmcCheckDataProperty.CheckId.HasValue.ToString()
                        , mOriginQmcCheckDataProperty.ItemPropertyId.ToString(), mOriginQmcCheckDataProperty.PropertyValue });
            }
            foreach (QmcCheckDataDetail mOriginQmcCheckDataDetail in mOriginQmcCheckDataDetailList)
            {
                originCheckDataDetailInfo.AppendFormat("DetailId={0},CheckId={1},ItemPropertyId={2},PropertyValue={3}"
                    , new string[] {mOriginQmcCheckDataDetail.DetailId.ToString(), mOriginQmcCheckDataDetail.CheckId.HasValue.ToString()
                        , mOriginQmcCheckDataDetail.ItemDetailId.ToString(), mOriginQmcCheckDataDetail.CheckValue });
                originCheckDataDetailInfo.AppendFormat(",GoodCheckRange={0},AutoCheckResult={1},PrimeCheckRange={2},IsPrime={3}"
                    , new string[] { mOriginQmcCheckDataDetail.GoodCheckRange, mOriginQmcCheckDataDetail.AutoCheckResult.ToString()
                        , mOriginQmcCheckDataDetail.PrimeCheckRange, mOriginQmcCheckDataDetail.IsPrime.ToString() });
            }
        }
        modifyCheckDataInfo.AppendFormat(",BillNo={0},Barcode={1},OrderID={2}"
            , new string[] { mQmcCheckData.BillNo, mQmcCheckData.Barcode, mQmcCheckData.OrderID.ToString() });
        modifyCheckDataInfo.AppendFormat(",Frequency={0},SpecId={1}"
            , new string[] { mQmcCheckData.Frequency, mQmcCheckData.SpecId.ToString() });
        modifyCheckDataInfo.AppendFormat(",LedgerId={0},MaterCode={1},SupplyFacId={2},ProductFacId={3}"
            , new string[] { mQmcCheckData.LedgerId.ToString(), mQmcCheckData.MaterCode
                , mQmcCheckData.SupplyFac.ToString(), mQmcCheckData.ProductFac.ToString() });
        modifyCheckDataInfo.AppendFormat(",CheckDate={0},CheckResult={1},RecordStat={2},LastModifierId={3},LastModifyTime={4}"
            , new string[] { mQmcCheckData.CheckDate.HasValue == true ? mQmcCheckData.CheckDate.Value.ToString("yyyy-MM-dd") : ""
                    , mQmcCheckData.CheckResult, mQmcCheckData.RecordStat.ToString(), mQmcCheckData.LastModifierId
                    , mQmcCheckData.LastModifyTime.HasValue == true ? mQmcCheckData.LastModifyTime.Value.ToString("yyyy-MM-dd") : "" });
        foreach (QmcCheckDataProperty mQmcCheckDataProperty in mQmcCheckDataPropertyList)
        {
            modifyCheckDataPropertyInfo.AppendFormat("PropertyId={0},CheckId={1},ItemPropertyId={2},PropertyValue={3};"
                , new string[] {mQmcCheckDataProperty.PropertyId.ToString(), mQmcCheckDataProperty.CheckId.HasValue.ToString()
                        , mQmcCheckDataProperty.ItemPropertyId.ToString(), mQmcCheckDataProperty.PropertyValue });
        }
        foreach (QmcCheckDataDetail mQmcCheckDataDetail in mQmcCheckDataDetailList)
        {
            modifyCheckDataDetailInfo.AppendFormat("DetailId={0},CheckId={1},ItemPropertyId={2},PropertyValue={3}"
                , new string[] {mQmcCheckDataDetail.DetailId.ToString(), mQmcCheckDataDetail.CheckId.HasValue.ToString()
                        , mQmcCheckDataDetail.ItemDetailId.ToString(), mQmcCheckDataDetail.CheckValue });
            modifyCheckDataDetailInfo.AppendFormat(",GoodCheckRange={0},AutoCheckResult={1},PrimeCheckRange={2},IsPrime={3}"
                , new string[] { mQmcCheckDataDetail.GoodCheckRange, mQmcCheckDataDetail.AutoCheckResult.ToString()
                        , mQmcCheckDataDetail.PrimeCheckRange, mQmcCheckDataDetail.IsPrime.ToString() });
        }

        #region 日志记录
        try
        {
            if (commandName == "Insert")
            {
                modifyCheckDataInfo.Insert(0, "主数据:CheckId=" + mQmcCheckData.CheckId.ToString());
                this.AppendWebLog("原材料质检数据添加", modifyCheckDataInfo.ToString());

                modifyCheckDataPropertyInfo.Insert(0, "属性数据:");
                this.AppendWebLog("原材料质检数据添加", modifyCheckDataPropertyInfo.ToString());

                modifyCheckDataDetailInfo.Insert(0, "质检数据:");
                this.AppendWebLog("原材料质检数据添加", modifyCheckDataDetailInfo.ToString());
            }
            else if (commandName == "Update")
            {
                originCheckDataInfo.Insert(0, "主数据(修改前):");
                this.AppendWebLog("原材料质检数据修改(提交前)", originCheckDataInfo.ToString());

                originCheckDataPropertyInfo.Insert(0, "属性数据(修改前):");
                this.AppendWebLog("原材料质检数据修改(提交前)", originCheckDataPropertyInfo.ToString());

                originCheckDataDetailInfo.Insert(0, "质检数据(修改前):");
                this.AppendWebLog("原材料质检数据修改(提交前)", originCheckDataDetailInfo.ToString());

                modifyCheckDataInfo.Insert(0, "主数据(修改后):CheckId=" + mQmcCheckData.CheckId.ToString());
                this.AppendWebLog("原材料质检数据修改(提交前)", modifyCheckDataInfo.ToString());

                modifyCheckDataPropertyInfo.Insert(0, "属性数据(修改后):");
                this.AppendWebLog("原材料质检数据修改(提交前)", modifyCheckDataPropertyInfo.ToString());

                modifyCheckDataDetailInfo.Insert(0, "质检数据(修改后):");
                this.AppendWebLog("原材料质检数据修改(提交前)", modifyCheckDataDetailInfo.ToString());

            }
            else if (commandName == "SpecUpdate")
            {
                originCheckDataInfo.Insert(0, "主数据(修改前):");
                this.AppendWebLog("原材料质检数据修改(提交后)", originCheckDataInfo.ToString());

                originCheckDataPropertyInfo.Insert(0, "属性数据(修改前):");
                this.AppendWebLog("原材料质检数据修改(提交后)", originCheckDataPropertyInfo.ToString());

                originCheckDataDetailInfo.Insert(0, "质检数据(修改前):");
                this.AppendWebLog("原材料质检数据修改(提交后)", originCheckDataDetailInfo.ToString());

                modifyCheckDataInfo.Insert(0, "主数据(修改后):CheckId=" + mQmcCheckData.CheckId.ToString());
                this.AppendWebLog("原材料质检数据修改(提交后)", modifyCheckDataInfo.ToString());

                modifyCheckDataPropertyInfo.Insert(0, "属性数据(修改后):");
                this.AppendWebLog("原材料质检数据修改(提交后)", modifyCheckDataPropertyInfo.ToString());

                modifyCheckDataDetailInfo.Insert(0, "质检数据(修改后):");
                this.AppendWebLog("原材料质检数据修改(提交后)", modifyCheckDataDetailInfo.ToString());
            }
        }
        catch (Exception ex)
        {
            if (commandName == "Insert")
            {
                this.AppendWebLog("原材料质检数据添加", "日志记录导常:" + ex.Message);
            }
            else if (commandName == "Update")
            {
                this.AppendWebLog("原材料质检数据修改(提交前)", "日志记录导常:" + ex.Message);
            }
            else if (commandName == "SpecUpdate")
            {
                this.AppendWebLog("原材料质检数据修改(提交后)", "日志记录导常:" + ex.Message);
            }
        }
        #endregion  

        HiddenMasterCheckId.SetValue("");
        WindowMaster.Close();

        QueryCheckData();

        return true;
    }



    /// <summary>
    /// 检测结果判定
    /// </summary>
    /// <param name="checkValue"></param>
    /// <param name="mQmcCheckItemDetail"></param>
    /// <param name="enumCheckGrade"></param>
    /// <returns></returns>
    private int? GetCheckResult(decimal checkValue, QmcCheckItemDetail mQmcCheckItemDetail, EnumCheckGrade enumCheckGrade)
    {
        if (checkValue == 0 || checkValue == null)
            return 1;
        string _operator = "";
        decimal? minValue = null;
        decimal? maxValue = null;
        string includeMinBorder = "";
        string includeMaxBorder = "";
        if (enumCheckGrade == EnumCheckGrade.Good)
        {
            _operator = mQmcCheckItemDetail.GoodOperator;
            minValue = mQmcCheckItemDetail.GoodMinValue;
            maxValue = mQmcCheckItemDetail.GoodMaxValue;
            includeMinBorder = mQmcCheckItemDetail.GoodIncludeMinBorder;
            includeMaxBorder = mQmcCheckItemDetail.GoodIncludeMaxBorder;
        }
        else if (enumCheckGrade == EnumCheckGrade.Prime)
        {
            _operator = mQmcCheckItemDetail.PrimeOperator;
            minValue = mQmcCheckItemDetail.PrimeMinValue;
            maxValue = mQmcCheckItemDetail.PrimeMaxValue;
            includeMinBorder = mQmcCheckItemDetail.PrimeIncludeMinBorder;
            includeMaxBorder = mQmcCheckItemDetail.PrimeIncludeMaxBorder;
        }

        if (enumCheckGrade == EnumCheckGrade.Min)
        {
            _operator = mQmcCheckItemDetail.MinOperator;
            minValue = mQmcCheckItemDetail.MinMinValue;
            maxValue = mQmcCheckItemDetail.MinMaxValue;
            includeMinBorder = mQmcCheckItemDetail.MinIncludeMinBorder;
            includeMaxBorder = mQmcCheckItemDetail.MinIncludeMaxBorder;
        }

        if (enumCheckGrade == EnumCheckGrade.Max)
        {
            _operator = mQmcCheckItemDetail.MaxOperator;
            minValue = mQmcCheckItemDetail.MaxMinValue;
            maxValue = mQmcCheckItemDetail.MaxMaxValue;
            includeMinBorder = mQmcCheckItemDetail.MaxIncludeMinBorder;
            includeMaxBorder = mQmcCheckItemDetail.MaxIncludeMaxBorder;
        }

        if(string.IsNullOrEmpty(_operator))
            return 1;

        int? checkResult = null;
        switch (_operator)
        {
            case "-":
            case "－":
                if (includeMinBorder == "1" && includeMaxBorder == "1")
                {
                    checkResult = checkValue >= minValue && checkValue <= maxValue ? 1 : 0;
                }
                else if (includeMinBorder == "1")
                {
                    checkResult = checkValue >= minValue && checkValue < maxValue ? 1 : 0;
                }
                else if (includeMaxBorder == "1")
                {
                    checkResult = checkValue > minValue && checkValue <= maxValue ? 1 : 0;
                }
                else
                {
                    checkResult = checkValue > minValue && checkValue < maxValue ? 1 : 0;
                }
                break;
            case ">":
                if (includeMaxBorder == "1")
                {
                    checkResult = checkValue >= maxValue ? 1 : 0;
                }
                else
                {
                    checkResult = checkValue > maxValue ? 1 : 0;
                }
                break;
            case "<":
                if (includeMaxBorder == "1")
                {
                    checkResult = checkValue <= maxValue ? 1 : 0;
                }
                else
                {
                    checkResult = checkValue < maxValue ? 1 : 0;
                }
                break;
            case "A":
                if (includeMaxBorder == "1")
                {
                    checkResult = checkValue <= maxValue ? 1 : 0;
                }
                else
                {
                    checkResult = checkValue < maxValue ? 1 : 0;
                }
                break;
            case "I":
                if (includeMinBorder == "1")
                {
                    checkResult = checkValue >= maxValue ? 1 : 0;
                }
                else
                {
                    checkResult = checkValue > maxValue ? 1 : 0;
                }
                break;
            case "≥":
                checkResult = checkValue >= maxValue ? 1 : 0;
                break;
            case "≤":
                checkResult = checkValue <= maxValue ? 1 : 0;
                break;
            case "P":
                checkResult = 1;
                break;
            default:
                break;
        }
        return checkResult;
    }

    /// <summary>
    /// 审核选中信息
    /// </summary>
    private void ApproveCheckData()
    {
        string checkId = CheckboxSelectionModelCenterMaster.SelectedRow.RecordID;
        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        QmcCheckData mQmcCheckData = bQmcCheckDataManager.GetById(checkId);
        string ledgerId = mQmcCheckData.LedgerId.ToString();
        string billNo = mQmcCheckData.BillNo;
        string barcode = mQmcCheckData.Barcode;
        string orderID = mQmcCheckData.OrderID.ToString();
        string materCode = mQmcCheckData.MaterCode;

        IQmcSampleLedgerManager bQmcSampleLedgerManager = new QmcSampleLedgerManager();
        EntityArrayList<QmcSampleLedger> mQmcSampleLedgerList = bQmcSampleLedgerManager.GetListByWhereAndOrder(
            QmcSampleLedger._.LedgerId == ledgerId
            , QmcSampleLedger._.DeleteFlag.Asc);
        if (mQmcSampleLedgerList.Count == 0)
        {
            X.Msg.Alert("提示", "未找到样品台账信息").Show();
            return;
        }
        QmcSampleLedger mQmcSampleLedger = mQmcSampleLedgerList[0];

        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
            BasMaterial._.MaterialCode == materCode
            , BasMaterial._.DeleteFlag.Asc);
        if (mBasMaterialList.Count == 0)
        {
            X.Msg.Alert("提示", "未找到原材料信息").Show();
            return;
        }
        string majorTypeID = mBasMaterialList[0].MajorTypeID.ToString();
        string minorTypeID = mBasMaterialList[0].MinorTypeID;

        IBasMaterialMinorTypeManager bBasMaterialMinorTypeManager = new BasMaterialMinorTypeManager();
        EntityArrayList<BasMaterialMinorType> mBasMaterialMinorTypeList = bBasMaterialMinorTypeManager.GetListByWhereAndOrder(
            BasMaterialMinorType._.MajorID == majorTypeID
            & BasMaterialMinorType._.MinorTypeID == minorTypeID
            , BasMaterialMinorType._.DeleteFlag.Asc);
        if (mBasMaterialMinorTypeList.Count == 0)
        {
            X.Msg.Alert("提示", "未找到原材料分类信息").Show();
            return;
        }
        string seriesId = mBasMaterialMinorTypeList[0].ObjID.ToString();
        string standardId = mQmcCheckData.StandardId.ToString();
        IBasFactoryInfoManager bBasFactoryInfoManager = new BasFactoryInfoManager();
        string supplyFacId = mQmcCheckData.SupplyFac.ToString();
        string supplyFacName = "";
        if (supplyFacId != "")
        {
            BasFactoryInfo mBasFactoryInfo = bBasFactoryInfoManager.GetById(supplyFacId);
            supplyFacName = mBasFactoryInfo == null ? "" : mBasFactoryInfo.FacName;
        }
        string productFacId = mQmcCheckData.ProductFac.ToString();
        string productFacName = "";
        if (productFacId != "")
        {
            BasFactoryInfo mBasFactoryInfo = bBasFactoryInfoManager.GetById(productFacId);
            productFacName = mBasFactoryInfo == null ? "" : mBasFactoryInfo.FacName;
        }

        string factoryCode = mQmcSampleLedger.FactoryCode;
        string sampleNum = mQmcSampleLedger.SampleNum.ToString();
        if ((sampleNum == "null") || (sampleNum == null))
        {
            sampleNum = "0";
        }
        string sampleUnit = mQmcSampleLedger.SampleUnit;
        string sampleCode = mQmcSampleLedger.SampleCode;
        string batchCode = mQmcSampleLedger.BatchCode;
        string sampleStatus = mQmcSampleLedger.SampleStatus;

        IBasUserManager bBasUserManager = new BasUserManager();
        string extractorId = mQmcSampleLedger.ExtractorId;
        string extractorName = "";
        if (extractorId != "")
        {
            EntityArrayList<BasUser> mBasUserList = bBasUserManager.GetListByWhereAndOrder(
                BasUser._.HRCode == extractorId
                , BasUser._.DeleteFlag.Asc);
            extractorName = mBasUserList.Count == 0 ? "" : mBasUserList[0].UserName;
        }
        string receiverId = mQmcSampleLedger.ReceiverId;
        string receiverName = "";
        if (receiverId != "")
        {
            EntityArrayList<BasUser> mBasUserList = bBasUserManager.GetListByWhereAndOrder(
                BasUser._.HRCode == receiverId
                , BasUser._.DeleteFlag.Asc);
            receiverName = mBasUserList.Count == 0 ? "" : mBasUserList[0].UserName;
        }
        string receiveDate = mQmcSampleLedger.RecordDate.HasValue == true ? mQmcSampleLedger.RecordDate.Value.ToString("yyyy-MM-dd") : "";
        string sendDate = mQmcSampleLedger.SendDate.HasValue == true ? mQmcSampleLedger.SendDate.Value.ToString("yyyy-MM-dd") : "";
        string fetcherId = mQmcSampleLedger.FetcherId;
        string fetcherName = "";
        if (fetcherId != "")
        {
            EntityArrayList<BasUser> mBasUserList = bBasUserManager.GetListByWhereAndOrder(
                BasUser._.HRCode == fetcherId
                , BasUser._.DeleteFlag.Asc);
            fetcherName = mBasUserList.Count == 0 ? "" : mBasUserList[0].UserName;
        }
       
        string sampleRemark = "";
        if (!String.IsNullOrEmpty(mQmcSampleLedger.Remark)) sampleRemark = mQmcSampleLedger.Remark.Trim();
        string checkDate = mQmcCheckData.CheckDate.Value.ToString("yyyy-MM-dd");
        string checkResult = mQmcCheckData.CheckResult;
        string remark = mQmcCheckData.Remark;
        string recordStat = mQmcCheckData.RecordStat.ToString();
        string frequency = mQmcCheckData.Frequency;
        string specId = mQmcCheckData.SpecId.ToString();

        ClearApproveData();

        txtSampleNameApprove.SetValue(mQmcSampleLedger.SampleName.Trim());
        txtSupplierApprove.SetValue(supplyFacName);
        txtManufacturerApprove.SetValue(productFacName);
        txtFactoryNumApprove.SetValue(factoryCode);
        txtBarcodeApprove.SetValue(barcode);
        txtSampleNumApprove.SetValue(sampleNum + sampleUnit);
        txtBatchCodeApprove.SetValue(batchCode);
        txtSampleCodeApprove.SetValue(sampleCode);
        txtSampleStatusApprove.SetValue(sampleStatus);
        txtExtractorApporve.SetValue(extractorName);
        txtReceiverApprove.SetValue(receiverName);
        txtReceiveDateApprove.SetValue(receiveDate);
        txtSendDateApprove.SetValue(sendDate);
        txtFetcherApprove.SetValue(fetcherName);
        txtSampleRemarkApprove.SetValue(sampleRemark);

        if (specId != "")
        {
            IQmcSpecManager specManager = new QmcSpecManager();
            QmcSpec spec = specManager.GetById(specId);
            txtSpecApprove.SetValue(spec.SpecName);
        }

        txtCheckDateApprove.SetValue(checkDate);
        txtCheckResultApprove.SetValue(checkResult);
        txtRemarkApprove.SetValue(remark);
        txtFrequencyApprove.SetValue(frequency);

        FillApproveDataDetail(standardId, materCode, checkId);

        WindowApprove.Show();
    }

    /// <summary>
    /// 修改填充信息
    /// </summary>
    private void EditCheckData()
    {
        string commandName = HiddenMasterCommandName.Value.ToString();

        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择一条记录").Show();
            return;
        }

        string checkId = CheckboxSelectionModelCenterMaster.SelectedRow.RecordID;

        if (checkId == "")
        {
            X.Msg.Alert("提示", "检验数据记录ID为空").Show();
            return;
        }

        HiddenMasterCheckId.SetValue(checkId);

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        QmcCheckData mQmcCheckData = bQmcCheckDataManager.GetById(checkId);

        if (commandName == "Update")
        {
            if (mQmcCheckData.RecordStat.HasValue == true
            && mQmcCheckData.RecordStat.Value == 1)
            {
                X.Msg.Alert("提示", "记录已提交，不允许修改").Show();
                return;
            }
            if (mQmcCheckData.RecorderId != this.UserID)
            {
                X.Msg.Alert("提示", "您不是记录的录入人，不允许修改").Show();
                return;
            }
            CheckboxMasterRecordStat.Enable();
            WindowMaster.Title = "检测数据提交前修改";
        }
        else if (commandName == "SpecUpdate")
        {
            if (mQmcCheckData.RecordStat.HasValue == false || mQmcCheckData.RecordStat.Value == 0)
            {
                X.Msg.Alert("提示", "记录未提交，不允许修改").Show();
                return;
            }
            CheckboxMasterRecordStat.Disable();
            WindowMaster.Title = "检测数据提交后修改";
        }

        ClearCheckData();
        if (commandName == "SpecUpdate")
        {
            ComboBoxMasterCheckResult.Hidden = false;
            ComboBoxMasterCheckResult.AllowBlank = false;
        }
        string ledgerId = mQmcCheckData.LedgerId.ToString();
        string billNo = mQmcCheckData.BillNo;
        string barcode = mQmcCheckData.Barcode;
        string orderID = mQmcCheckData.OrderID.ToString();
        string materCode = mQmcCheckData.MaterCode;

        IQmcSampleLedgerManager bQmcSampleLedgerManager = new QmcSampleLedgerManager();
        EntityArrayList<QmcSampleLedger> mQmcSampleLedgerList = bQmcSampleLedgerManager.GetListByWhereAndOrder(
            QmcSampleLedger._.LedgerId == ledgerId
            , QmcSampleLedger._.DeleteFlag.Asc);
        if (mQmcSampleLedgerList.Count == 0)
        {
            X.Msg.Alert("提示", "未找到样品台账信息").Show();
            return;
        }
        QmcSampleLedger mQmcSampleLedger = mQmcSampleLedgerList[0];

        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
            BasMaterial._.MaterialCode == materCode
            , BasMaterial._.DeleteFlag.Asc);
        if (mBasMaterialList.Count == 0)
        {
            X.Msg.Alert("提示", "未找到原材料信息").Show();
            return;
        }
        string majorTypeID = mBasMaterialList[0].MajorTypeID.ToString();
        string minorTypeID = mBasMaterialList[0].MinorTypeID;

        IBasMaterialMinorTypeManager bBasMaterialMinorTypeManager = new BasMaterialMinorTypeManager();
        EntityArrayList<BasMaterialMinorType> mBasMaterialMinorTypeList = bBasMaterialMinorTypeManager.GetListByWhereAndOrder(
            BasMaterialMinorType._.MajorID == majorTypeID
            & BasMaterialMinorType._.MinorTypeID == minorTypeID
            , BasMaterialMinorType._.DeleteFlag.Asc);
        if (mBasMaterialMinorTypeList.Count == 0)
        {
            X.Msg.Alert("提示", "未找到原材料分类信息").Show();
            return;
        }
        string seriesId = mBasMaterialMinorTypeList[0].ObjID.ToString();
        IBasFactoryInfoManager bBasFactoryInfoManager = new BasFactoryInfoManager();
        string supplyFacId = mQmcCheckData.SupplyFac.ToString();
        string supplyFacName = "";
        if (supplyFacId != "")
        {
            BasFactoryInfo mBasFactoryInfo = bBasFactoryInfoManager.GetById(supplyFacId);
            supplyFacName = mBasFactoryInfo == null ? "" : mBasFactoryInfo.FacName;
        }
        string productFacId = mQmcCheckData.ProductFac.ToString();
        string productFacName = "";
        if (productFacId != "")
        {
            BasFactoryInfo mBasFactoryInfo = bBasFactoryInfoManager.GetById(productFacId);
            productFacName = mBasFactoryInfo == null ? "" : mBasFactoryInfo.FacName;
        }

        string factoryCode = mQmcSampleLedger.FactoryCode;
        string sampleNum = mQmcSampleLedger.SampleNum.ToString();
        if ((sampleNum == "null") || (sampleNum == null))
        {
            sampleNum = "0";
        }
        string sampleUnit = mQmcSampleLedger.SampleUnit;
        string sampleCode = mQmcSampleLedger.SampleCode;
        string batchCode = mQmcSampleLedger.BatchCode;
        string sampleStatus = mQmcSampleLedger.SampleStatus;

        IBasUserManager bBasUserManager = new BasUserManager();
        string extractorId = mQmcSampleLedger.ExtractorId;
        string extractorName = "";
        if (extractorId != "")
        {
            EntityArrayList<BasUser> mBasUserList = bBasUserManager.GetListByWhereAndOrder(
                BasUser._.HRCode == extractorId
                , BasUser._.DeleteFlag.Asc);
            extractorName = mBasUserList.Count == 0 ? "" : mBasUserList[0].UserName;
        }
        string receiverId = mQmcSampleLedger.ReceiverId;
        string receiverName = "";
        if (receiverId != "")
        {
            EntityArrayList<BasUser> mBasUserList = bBasUserManager.GetListByWhereAndOrder(
                BasUser._.HRCode == receiverId
                , BasUser._.DeleteFlag.Asc);
            receiverName = mBasUserList.Count == 0 ? "" : mBasUserList[0].UserName;
        }
        string receiveDate = mQmcSampleLedger.RecordDate.HasValue == true ? mQmcSampleLedger.RecordDate.Value.ToString("yyyy-MM-dd") : "";
        string sendDate = mQmcSampleLedger.SendDate.HasValue == true ? mQmcSampleLedger.SendDate.Value.ToString("yyyy-MM-dd") : "";
        string fetcherId = mQmcSampleLedger.FetcherId;
        string fetcherName = "";
        if (fetcherId != "")
        {
            EntityArrayList<BasUser> mBasUserList = bBasUserManager.GetListByWhereAndOrder(
                BasUser._.HRCode == fetcherId
                , BasUser._.DeleteFlag.Asc);
            fetcherName = mBasUserList.Count == 0 ? "" : mBasUserList[0].UserName;
        }
        string sampleRemark = "";
        if (!String.IsNullOrEmpty(mQmcSampleLedger.Remark)) sampleRemark=mQmcSampleLedger.Remark.Trim();

        string checkDate = mQmcCheckData.CheckDate.Value.ToString("yyyy-MM-dd");
        string checkResult = mQmcCheckData.CheckResult;
        string remark = mQmcCheckData.Remark;
        string recordStat = mQmcCheckData.RecordStat.ToString();
        string frequency = mQmcCheckData.Frequency;
        string specId = mQmcCheckData.SpecId.ToString();
        string standardId = mQmcCheckData.StandardId.ToString();

        HiddenMasterLedgerId.SetValue(ledgerId);
        HiddenMasterBillNo.SetValue(billNo);
        HiddenMasterBarcode.SetValue(barcode);
        HiddenMasterBatchCode.SetValue(batchCode);
        HiddenMasterSpecId.SetValue(specId);
        HiddenMasterOrderID.SetValue(orderID);
        HiddenMasterMaterCode.SetValue(materCode);
        HiddenMasterSeriesId.SetValue(seriesId);
        HiddenMasterSupplyFacId.SetValue(supplyFacId);
        HiddenMasterProductFacId.SetValue(productFacId);

        TriggerFieldMasterSampleName.SetValue(mQmcSampleLedger.SampleName.Trim());
        TextFieldMasterSupplierName.SetValue(supplyFacName);
        TextFieldMasterManufacturerName.SetValue(productFacName);
        TextFieldMasterFactoryCode.SetValue(factoryCode);
        TextFieldMasterBarcode.SetValue(barcode);
        TextFieldMasterSampleNum.SetValue(sampleNum + sampleUnit);
        TextFieldMasterBatchCode.SetValue(remark);
        TextFieldMasterSampleCode.SetValue(sampleCode);
        TextFieldMasterSampleStatus.SetValue(sampleStatus);
        TextFieldMasterExtractorName.SetValue(extractorName);
        TextFieldMasterReceiverName.SetValue(receiverName);
        TextFieldMasterReceiveDate.SetValue(receiveDate);
        TextFieldMasterSendDate.SetValue(sendDate);
        TextFieldMasterFetcherName.SetValue(fetcherName);
        TextFieldMasterSampleRemark.SetValue(sampleRemark);

        if (specId != "")
        {
            IQmcSpecManager specManager = new QmcSpecManager();
            QmcSpec spec = specManager.GetById(specId);
            TextFieldMasterSpec.SetValue(spec.SpecName);
        }

        DateFieldMasterCheckDate.SetValue(checkDate);
        ComboBoxMasterCheckResult.SetValue(checkResult);
        TextFieldMasterRemark.SetValue(remark);
        TextFieldMasterFrequency.SetValue(frequency);

        FillStandardList(standardId);

        ComboBoxMasterStandard.SetValueAndFireSelect(standardId);

        CheckboxMasterRecordStat.Checked = recordStat == "1" ? true : false;

        IQmcCheckDataPropertyManager bQmcCheckDataPropertyManager = new QmcCheckDataPropertyManager();
        EntityArrayList<QmcCheckDataProperty> mQmcCheckDataPropertyList = bQmcCheckDataPropertyManager.GetListByWhereAndOrder(
            QmcCheckDataProperty._.CheckId == checkId
            , QmcCheckDataProperty._.PropertyId.Asc);

        FillCheckDataProperty(seriesId, mQmcCheckDataPropertyList);

        //FillCheckDataDetail(standardId, materCode, frequency, checkId);

        //FieldSetMasterProperty.UpdateContent();
        //FieldSetMasterDetail.UpdateContent();

        TriggerFieldMasterSampleName.ConcealTrigger(0);

        WindowMaster.Show();

    }

    /// <summary>
    /// 删除
    /// </summary>
    private void DeleteCheckData()
    {
        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择一条记录").Show();
            return;
        }

        string checkId = CheckboxSelectionModelCenterMaster.SelectedRow.RecordID;
        if (checkId == "")
        {
            X.Msg.Alert("提示", "检验数据记录ID为空").Show();
            return;
        }

        string commandName = HiddenMasterCommandName.Value.ToString();

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        QmcCheckData mQmcCheckData = bQmcCheckDataManager.GetById(checkId);

        QmcCheckData mOriginQmcCheckData = new QmcCheckData();
        mOriginQmcCheckData.CheckId = mQmcCheckData.CheckId;
        mOriginQmcCheckData.LastModifierId = mQmcCheckData.LastModifierId;
        mOriginQmcCheckData.LastModifyTime = mQmcCheckData.LastModifyTime;

        if (commandName == "Delete")
        {
            if (mQmcCheckData.RecordStat.HasValue == true && mQmcCheckData.RecordStat.Value == 1)
            {
                X.Msg.Alert("提示", "该记录已提交，不允许删除").Show();
                return;
            }
            if (mQmcCheckData.RecorderId != this.UserID)
            {
                X.Msg.Alert("提示", "您不是该记录的录入人，不允许删除").Show();
                return;
            }
        }
        else if (commandName == "SpecDelete")
        {
            if (mQmcCheckData.RecordStat.HasValue == false || mQmcCheckData.RecordStat.Value == 0)
            {
                X.Msg.Alert("提示", "该记录未提交，不允许删除").Show();
                return;
            }
        }
        else
        {
            X.Msg.Alert("提示", "未知操作").Show();
            return;
        }
        mQmcCheckData.LastModifierId = this.UserID;
        mQmcCheckData.LastModifyTime = DateTime.Now;
        mQmcCheckData.DeleteFlag = "1";
        bQmcCheckDataManager.Update(mQmcCheckData);

        // 日志
        System.Text.StringBuilder originCheckDataInfo = new System.Text.StringBuilder();
        originCheckDataInfo.AppendFormat("主数据：CheckId={0},LastModifierId={1},LastModifyTime={2}"
            , new string[] { mOriginQmcCheckData.CheckId.ToString(), mOriginQmcCheckData.LastModifierId
                , mOriginQmcCheckData.LastModifyTime.HasValue == true ? mOriginQmcCheckData.LastModifyTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "" });

        try
        {
            if (commandName == "Delete")
            {
                this.AppendWebLog("原材料质检数据删除(提交前)", originCheckDataInfo.ToString());
            }
            else if (commandName == "SpecDelete")
            {
                this.AppendWebLog("原材料质检数据删除(提交后)", originCheckDataInfo.ToString());
            }
        }
        catch (Exception ex)
        {
            if (commandName == "Delete")
            {
                this.AppendWebLog("原材料质检数据删除(提交前)", "日志记录导常:" + ex.Message);
            }
            else if (commandName == "SpecDelete")
            {
                this.AppendWebLog("原材料质检数据删除(提交后)", "日志记录导常:" + ex.Message);
            }
        }

        X.Msg.Alert("提示", "删除成功").Show();

        QueryCheckData();
    }
    #endregion

    #region 事件处理
    /// <summary>
    /// 更换原材料分类
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxNorthMaterMinorType_Change(object sender, DirectEventArgs e)
    {
        ComboBoxNorthMater.GetStore().RemoveAll();
        ComboBoxNorthSupplyFac.GetStore().RemoveAll();

        ComboBoxNorthMater.SetValue("");
        ComboBoxNorthSupplyFac.SetValue("");

        string minorTypeId = ComboBoxNorthMaterMinorType.Value.ToString();
        if (minorTypeId == "")
        {
            return;
        }

        IBasMaterialManager bBasMaterialManager = new BasMaterialManager();
        EntityArrayList<BasMaterial> mBasMaterialList = bBasMaterialManager.GetListByWhereAndOrder(
            BasMaterial._.DeleteFlag == "0"
            & BasMaterial._.MajorTypeID == 1
            & BasMaterial._.MinorTypeID == minorTypeId
            , BasMaterial._.MaterialName.Asc);
        foreach (BasMaterial mBasMaterial in mBasMaterialList)
        {
            ComboBoxNorthMater.AddItem(mBasMaterial.MaterialName, mBasMaterial.MaterialCode);
        }

        IQmcFactoryMappingManager bQmcFactoryMappingManager = new QmcFactoryMappingManager();
        EntityArrayList<QmcFactoryMapping> mQmcFactoryMappingList = bQmcFactoryMappingManager.GetListByWhereAndOrder(
            QmcFactoryMapping._.DeleteFlag == "0"
            & QmcFactoryMapping._.SeriesId == minorTypeId
            , QmcFactoryMapping._.MappingId.Asc);

        var mQmcFactoryMappingGroup = mQmcFactoryMappingList.GroupBy(x => new { x.SupplierId, x.SupplierName })
            .Select(group => new { SupplierId = group.Key.SupplierId, SupplierName = group.Key.SupplierName });
        foreach (var mQmcFactoryMapping in mQmcFactoryMappingGroup)
        {
            ComboBoxNorthSupplyFac.AddItem(mQmcFactoryMapping.SupplierName.Trim(), mQmcFactoryMapping.SupplierId.ToString());
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        QueryCheckData();
    }

    /// <summary>
    /// 打开新增窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthAdd_Click(object sender, DirectEventArgs e)
    {
        ClearCheckData();

        FillStandardList();

        ComboBoxMasterStandard.Select(0);

        FieldSetMasterProperty.Items.Clear();
        FieldSetMasterDetail.Items.Clear();

        FieldSetMasterProperty.UpdateContent();
        FieldSetMasterDetail.UpdateContent();

        TriggerFieldMasterSampleName.ShowTrigger(0);

        HiddenMasterCommandName.SetValue("Insert");
        WindowMaster.Title = "检测数据新增";
        WindowMaster.Show();
    }

    /// <summary>
    /// 选中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckboxSelectionModelCenterMaster_SelectionChange(object sender, DirectEventArgs e)
    {
        string checkId = e.ExtraParams["CheckId"].ToString();

        StoreCenterProperty.RemoveAll();
        StoreCenterDetail.RemoveAll();

        IQmcCheckDataPropertyManager bQmcCheckDataPropertyManager = new QmcCheckDataPropertyManager();
        DataSet dsQmcCheckDataProperty = bQmcCheckDataPropertyManager.GetDataSetByCheckId(checkId);
        StoreCenterProperty.DataSource = dsQmcCheckDataProperty;
        StoreCenterProperty.DataBind();

        IQmcCheckDataDetailManager bQmcCheckDataDetailManager = new QmcCheckDataDetailManager();
        DataSet dsQmcCheckDataDetail = bQmcCheckDataDetailManager.GetDataSetByCheckId(checkId);
        if (dsQmcCheckDataDetail.Tables[0].Rows.Count > 0)
        {
            DataColumn dcTextCheckResult = new DataColumn("TextCheckResult");
            DataColumn dcTextIsPrime = new DataColumn("TextIsPrime");
            dsQmcCheckDataDetail.Tables[0].Columns.Add(dcTextCheckResult);
            dsQmcCheckDataDetail.Tables[0].Columns.Add(dcTextIsPrime);
            foreach (DataRow dr in dsQmcCheckDataDetail.Tables[0].Rows)
            {
                if (dr["AutoCheckResult"].ToString() == "0")
                {
                    dr["TextCheckResult"] = "不合格";
                    dr["TextIsPrime"] = "N/A";
                }
                else if (dr["AutoCheckResult"].ToString() == "1")
                {
                    dr["TextCheckResult"] = "合格";
                    if (dr["IsPrime"].ToString() == "1")
                    {
                        dr["TextIsPrime"] = "是";
                    }
                    else if (dr["IsPrime"].ToString() == "0")
                    {
                        dr["TextIsPrime"] = "否";
                    }
                    else
                    {
                        dr["TextIsPrime"] = "无标准";
                    }
                }
            }
        }
        StoreCenterDetail.DataSource = dsQmcCheckDataDetail;
        StoreCenterDetail.DataBind();
    }

    /// <summary>
    /// 打开修改窗口(未提交)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthEdit_Click(object sender, DirectEventArgs e)
    {
        HiddenMasterCommandName.SetValue("Update");

        EditCheckData();
    }

    /// <summary>
    /// 打开修改窗口(已提交)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthSpecEdit_Click(object sender, DirectEventArgs e)
    {
        HiddenMasterCommandName.SetValue("SpecUpdate");

        EditCheckData();
    }
    protected void ButtonFX_Click(object sender, DirectEventArgs e)
    {

        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择一条记录").Show();
            return;
        }

        string checkId = CheckboxSelectionModelCenterMaster.SelectedRow.RecordID;

        if (checkId == "")
        {
            X.Msg.Alert("提示", "检验数据记录ID为空").Show();
            return;
        }

        HiddenMasterCheckId.SetValue(checkId);

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        QmcCheckData mQmcCheckData = bQmcCheckDataManager.GetById(checkId);
        mQmcCheckData.FXFlag = "1";
        bQmcCheckDataManager.Update(mQmcCheckData);
        try
        {
            this.AppendWebLog("原材料质检数据放行，放行人编号：" + this.UserID + "，质检主数据编号：" + mQmcCheckData.CheckId.ToString());
        }
        catch (Exception ex)
        {
            this.AppendWebLog("原材料质检数据审核", "日志记录导常:" + ex.Message);
        }
        X.Msg.Alert("提示", "放行成功").Show();
        QueryCheckData();
    }
    protected void ButtonQXFX_Click(object sender, DirectEventArgs e)
    {

        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择一条记录").Show();
            return;
        }

        string checkId = CheckboxSelectionModelCenterMaster.SelectedRow.RecordID;

        if (checkId == "")
        {
            X.Msg.Alert("提示", "检验数据记录ID为空").Show();
            return;
        }

        HiddenMasterCheckId.SetValue(checkId);

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        QmcCheckData mQmcCheckData = bQmcCheckDataManager.GetById(checkId);
        mQmcCheckData.FXFlag = "0";
        bQmcCheckDataManager.Update(mQmcCheckData);
        try
        {
            this.AppendWebLog("原材料质检数据取消放行，放行人编号：" + this.UserID + "，质检主数据编号：" + mQmcCheckData.CheckId.ToString());
        }
        catch (Exception ex)
        {
            this.AppendWebLog("原材料质检数据审核", "日志记录导常:" + ex.Message);
        }
        X.Msg.Alert("提示", "取消放行成功").Show();
        QueryCheckData();
    }
    /// <summary>
    /// 选择执行标准
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ComboBoxMasterStandard_Click(object sender, DirectEventArgs e)
    {
        string standardId = ComboBoxMasterStandard.Value.ToString();
        string materCode = HiddenMasterMaterCode.Value.ToString();
        string frequency = TextFieldMasterFrequency.Text.Trim();

        EntityArrayList<QmcCheckDataDetail> mQmcCheckDataDetailList = null;
        string checkId = HiddenMasterCheckId.Value.ToString();
        if (checkId != "")
        {
            IQmcCheckDataDetailManager bQmcCheckDataDetailManager = new QmcCheckDataDetailManager();
            mQmcCheckDataDetailList = bQmcCheckDataDetailManager.GetListByWhereAndOrder(
                QmcCheckDataDetail._.CheckId == checkId
                , QmcCheckDataDetail._.DetailId.Asc);
        }

        FillCheckDataDetail(standardId, materCode, checkId);
        if (standardId != "" && materCode != "")
        {
        }
        else
        {
            FieldSetMasterDetail.Items.Clear();
        }
    }

    /// <summary>
    /// 删除选中的记录(未提交)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthDelete_Click(object sender, DirectEventArgs e)
    {
        HiddenMasterCommandName.SetValue("Delete");

        DeleteCheckData();
    }

    /// <summary>
    /// 删除选中的记录(已提交)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthSpecDelete_Click(object sender, DirectEventArgs e)
    {
        HiddenMasterCommandName.SetValue("SpecDelete");

        DeleteCheckData();
    }

    /// <summary>
    /// 审核选中的记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthApprove_Click(object sender, DirectEventArgs e)
    {
        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择一条记录").Show();
            return;
        }
        string checkId = CheckboxSelectionModelCenterMaster.SelectedRow.RecordID;
        if (checkId == "")
        {
            X.Msg.Alert("提示", "检验数据记录ID为空").Show();
            return;
        }
        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        QmcCheckData mQmcCheckData = bQmcCheckDataManager.GetById(checkId);
        if (mQmcCheckData.RecordStat.HasValue == false || mQmcCheckData.RecordStat.Value == 0)
        {
            X.Msg.Alert("提示", "该记录未提交，不允许审核").Show();
            return;
        }
        if (mQmcCheckData.ApproveFlag == "1")
        {
            X.Msg.Alert("提示", "该记录已审核，请勿重复审核").Show();
            return;
        }
        ApproveCheckData();
    }

    /// <summary>
    /// 审核确认
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConfirmApprove_Click(object sender, DirectEventArgs e)
    {
        string checkId = CheckboxSelectionModelCenterMaster.SelectedRow.RecordID;
        if (checkId == "")
        {
            X.Msg.Alert("提示", "检验数据记录ID为空").Show();
            return;
        }
        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        QmcCheckData mQmcCheckData = bQmcCheckDataManager.GetById(checkId);
        if (mQmcCheckData.RecordStat.HasValue == false || mQmcCheckData.RecordStat.Value == 0)
        {
            X.Msg.Alert("提示", "该记录未提交，不允许审核").Show();
            return;
        }
        if (mQmcCheckData.ApproveFlag == "1")
        {
            X.Msg.Alert("提示", "该记录已审核，请勿重复审核").Show();
            return;
        }
        mQmcCheckData.ApproverId = this.UserID;
        mQmcCheckData.ApproveFlag = "1";
        bQmcCheckDataManager.Update(mQmcCheckData);
        //审核后初始化累积数量和重量 闫志旭 2015.3.6
        IPstMaterialChkDetailManager materialChkDetailManager = new PstMaterialChkDetailManager();//送检
        IQmcFactoryNonCheckManager nonCheckManager = new QmcFactoryNonCheckManager();
        IBasFactoryInfoManager factoryManager = new BasFactoryInfoManager();
        IQmcSampleLedgerManager ledgerManager = new QmcSampleLedgerManager();
        //
        EntityArrayList<QmcSampleLedger> ledgerList = ledgerManager.GetListByWhere(QmcSampleLedger._.LedgerId == mQmcCheckData.LedgerId);
        EntityArrayList<PstMaterialChkDetail> materialChkDetailList = materialChkDetailManager.GetListByWhere(PstMaterialChkDetail._.ObjID == ledgerList[0].BillDetailId);
        EntityArrayList<BasFactoryInfo> facList = factoryManager.GetListByWhere(BasFactoryInfo._.ERPCode == ledgerList[0].FactoryCode);

        int i = 0;//那种免检方式
        EntityArrayList<QmcFactoryNonCheck> nonChkList = nonCheckManager.GetListByWhere(
                       QmcFactoryNonCheck._.FactoryCode == facList[0].ObjID &&
                       QmcFactoryNonCheck._.MaterialCode == ledgerList[0].MaterialCode &&
                       QmcFactoryNonCheck._.DeleteFlag == '0'
                      );

        QmcFactoryNonCheck nonChk = new QmcFactoryNonCheck();
        //X.Msg.Alert("aaaa", nonChkList[0].FactoryCode + ledgerList[0].MaterialCode).Show(); return;
        if (nonChkList.Count == 0)
        {
            nonChkList = nonCheckManager.GetListByWhere(
                         QmcFactoryNonCheck._.FactoryCode == "" &&
                         QmcFactoryNonCheck._.MaterialCode == ledgerList[0].MaterialCode &&
                         QmcFactoryNonCheck._.DeleteFlag == '0'
                        );
        }
        else { i = 1; }
        if (nonChkList.Count > 0)
        {

            nonChk = nonChkList[0];
            if (mQmcCheckData.CheckResult == "1")
            {
                if (nonChk.ErrorNum == 0)
                {
                    nonChk.TotalNum = 1; nonChk.TotalWeight = materialChkDetailList[0].SendWeight;
                }
                else
                { nonChk.ErrorNum = nonChk.ErrorNum - 1;
                if (nonChk.ErrorNum == 0)
                {    nonChk.TotalNum =1;
                nonChk.TotalWeight = materialChkDetailList[0].SendWeight;}}
                //}
                  if (nonChk.ErrorNum == 0){
                String sql = "";
                if (nonChk.NonCheckNum != 0 && nonChk.NonCheckWeight != Decimal.Parse("0"))
                {
                    if (i == 1)
                    {
                        sql = String.Format(@"select * from QmcSampleLedger 
where LedgerId>'{0}'
and MaterialCode='{1}' and FactoryCode='{2}'
order by LedgerId", ledgerList[0].LedgerId, ledgerList[0].MaterialCode, ledgerList[0].FactoryCode);

                        DataSet ds = ledgerManager.GetBySql(sql).ToDataSet();
                        for (int i2 = 0; i2 < ds.Tables[0].Rows.Count; i2++)
                        {
                            if (nonChk.TotalNum >= nonChk.NonCheckNum && nonChk.NonCheckNum != 0) break;
                            if (nonChk.TotalWeight >= nonChk.NonCheckWeight && nonChk.NonCheckWeight != 0) break;
                            if (AutoCreateQmcCheckData(ds.Tables[0].Rows[i2]["LedgerId"].ToString(), ledgerList[0].LedgerId.ToString()))
                            {
                                nonChk.TotalNum = nonChk.TotalNum + 1;
                                nonChk.TotalWeight = nonChk.TotalWeight + materialChkDetailManager.GetListByWhere(PstMaterialChkDetail._.ObjID == ds.Tables[0].Rows[i2]["BillDetailId"].ToString())[0].SendWeight;
                                nonCheckManager.Update(nonChk);
                                if (nonChk.TotalNum >= nonChk.NonCheckNum && nonChk.NonCheckNum != 0) break;
                                if (nonChk.TotalWeight >= nonChk.NonCheckWeight && nonChk.NonCheckWeight != 0) break;
                            }
                            else break;

                        }

                    }
                    else
                    {
                        sql = String.Format(@"select * from QmcSampleLedger 
where LedgerId>'{0}'
and MaterialCode='{1}'  and FactoryCode not in 
(select FactoryCode from QmcFactoryNonCheck
where MaterialCode='{1}' and FactoryCode <>'' )
order by LedgerId", ledgerList[0].LedgerId, ledgerList[0].MaterialCode, ledgerList[0].FactoryCode);

                        DataSet ds = ledgerManager.GetBySql(sql).ToDataSet();
                        for (int i2 = 0; i2 < ds.Tables[0].Rows.Count; i2++)
                        {
                            if (nonChk.TotalNum >= nonChk.NonCheckNum && nonChk.NonCheckNum != 0) break;
                            if (nonChk.TotalWeight >= nonChk.NonCheckWeight && nonChk.NonCheckWeight != 0) break;
                            if (AutoCreateQmcCheckData(ds.Tables[0].Rows[i2]["LedgerId"].ToString(), ledgerList[0].LedgerId.ToString()))
                            {
                                nonChk.TotalNum = nonChk.TotalNum + 1;
                                nonChk.TotalWeight = nonChk.TotalWeight + materialChkDetailManager.GetListByWhere(PstMaterialChkDetail._.ObjID == ds.Tables[0].Rows[i2]["BillDetailId"].ToString())[0].SendWeight;
                                nonCheckManager.Update(nonChk);
                                if (nonChk.TotalNum >= nonChk.NonCheckNum && nonChk.NonCheckNum != 0) break;
                                if (nonChk.TotalWeight >= nonChk.NonCheckWeight && nonChk.NonCheckWeight != 0) break;
                            }
                            else break;

                        }
                    }
                }
                
                
                
                
                
                }
                
            }
            else
            {
                //X.Msg.Alert("2", "审批成功").Show();
                nonChk.ErrorNum = 5;//不合格则需质检5次
                //nonChk.TotalNum = 0;
                //nonChk.TotalWeight = materialChkDetailList[0].SendWeight;

            }
        }
        if(!String.IsNullOrEmpty(nonChk.MaterialCode))
        nonCheckManager.Update(nonChk);

        materialChkDetailList[0].ChkResultFlag = mQmcCheckData.CheckResult;
        materialChkDetailManager.Update(materialChkDetailList[0]);
        // 日志
        try
        {
            this.AppendWebLog("原材料质检数据审核，审核人编号：" + mQmcCheckData.ApproverId.ToString() + "，质检主数据编号：" + mQmcCheckData.CheckId.ToString());
        }
        catch (Exception ex)
        {
            this.AppendWebLog("原材料质检数据审核", "日志记录导常:" + ex.Message);
        }

        X.Msg.Alert("提示", "审批成功").Show();
        this.WindowApprove.Close();
        QueryCheckData();
    }
    public Boolean AutoCreateQmcCheckData(string ledgerId, String oldledgerId)
    {
        IQmcCheckDataDetailManager checkDataDetailManager = new QmcCheckDataDetailManager();
        IQmcSampleLedgerManager ledgerManager = new QmcSampleLedgerManager();
        IQmcCheckDataManager checkDataManager = new QmcCheckDataManager();
        IQmcLedgerManager eLedgerManager = new QmcLedgerManager();
        IQmcLedgerDetailManager eLedgerDetailManager = new QmcLedgerDetailManager();
        IPstMaterialChkManager materialChkManager = new PstMaterialChkManager();
        IPstMaterialChkDetailManager materialChkDetailManager = new PstMaterialChkDetailManager();
        QmcFactoryNonCheckManager NonCheckManager = new QmcFactoryNonCheckManager();
        //this.AppendWebLog("样品台账免质检批次数据生成", "台账编号：" + ledgerId + ",操作人编号：" + UserID);
        EntityArrayList<QmcSampleLedger> currentledgerList = ledgerManager.GetListByWhere(QmcSampleLedger._.LedgerId == ledgerId);
        String UserID = "000001";

        if (currentledgerList.Count > 0)
        {

            QmcSampleLedger currentLedger = currentledgerList[0];
            string currentBatchCode = currentLedger.BatchCode;
            string currentFactoryCode = currentLedger.FactoryCode;
            string currentMaterialCode = currentLedger.MaterialCode;
            //checkDataManager.DeleteByWhere(QmcCheckData._.BillNo == currentLedger.BillNo);
            //currentFactoryCode = "100561";
            //currentMaterialCode = "1010000000001";
            EntityArrayList<QmcSampleLedger> oldLedgerList = ledgerManager.GetListByWhere(QmcSampleLedger._.LedgerId == oldledgerId); 
      

           
            if (oldLedgerList.Count > 0)
            {

                QmcSampleLedger oldLedger = oldLedgerList[0];
                EntityArrayList<QmcCheckData> oldCheckDateList = checkDataManager.GetListByWhere(QmcCheckData._.LedgerId == oldLedger.LedgerId && QmcCheckData._.DeleteFlag == "0");
                //X.Js.Alert(oldLedger.LedgerId.ToString());


                if (oldCheckDateList.Count > 0)
                {
                    #region 复制CheckData
                    QmcCheckData oldCheckData = oldCheckDateList[0];
                    QmcCheckData newCheckData = new QmcCheckData();
                    newCheckData.BillNo = currentLedger.BillNo;
                    newCheckData.Barcode = currentLedger.Barcode;
                    newCheckData.BatchCode = currentLedger.BatchCode;
                    newCheckData.Frequency = currentLedger.Frequency;
                    newCheckData.OrderID = currentLedger.OrderId;
                    newCheckData.MaterCode = currentLedger.MaterialCode;
                    newCheckData.SupplyFac = currentLedger.SupplierId;
                    newCheckData.CheckDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    newCheckData.CheckResult = oldCheckData.CheckResult;
                    newCheckData.RecorderId = UserID;
                    newCheckData.RecordTime = DateTime.Now;
                    newCheckData.LastModifierId = UserID;
                    newCheckData.LastModifyTime = DateTime.Now;
                    newCheckData.LedgerId = currentLedger.LedgerId;
                    newCheckData.DeleteFlag = "0";
                    newCheckData.RecordStat = 1;
                    newCheckData.StandardId = oldCheckData.StandardId;
                    newCheckData.ApproverId = UserID;
                    newCheckData.ApproveFlag = "1";
                    checkDataManager.Insert(newCheckData);
                    EntityArrayList<QmcCheckData> newCheckDateList = checkDataManager.GetListByWhere(
                        QmcCheckData._.LedgerId == newCheckData.LedgerId &&
                        QmcCheckData._.BatchCode == newCheckData.BatchCode &&
                        QmcCheckData._.MaterCode == newCheckData.MaterCode &&
                        QmcCheckData._.BillNo == newCheckData.BillNo &&
                        QmcCheckData._.DeleteFlag == "0");
                    #endregion
                    #region 复制CheckDataDetail
                    if (newCheckDateList.Count > 0)
                    {
                        EntityArrayList<QmcCheckDataDetail> oldCheckDataDetailList = checkDataDetailManager.GetListByWhere(QmcCheckDataDetail._.CheckId == oldCheckData.CheckId);
                        foreach (QmcCheckDataDetail oldDetail in oldCheckDataDetailList)
                        {
                            QmcCheckDataDetail newDetail = new QmcCheckDataDetail();
                            newDetail.CheckId = newCheckDateList[0].CheckId;
                            newDetail.ItemDetailId = oldDetail.ItemDetailId;
                            newDetail.CheckValue = oldDetail.CheckValue;
                            newDetail.GoodCheckRange = oldDetail.GoodCheckRange;
                            newDetail.AutoCheckResult = oldDetail.AutoCheckResult;
                            newDetail.PrimeCheckRange = oldDetail.PrimeCheckRange;
                            newDetail.IsPrime = oldDetail.IsPrime;
                            checkDataDetailManager.Insert(newDetail);
                        }
                    }
                    #endregion
                    #region 生成电子台账并回写送检明细信息
                    QmcLedger eLedger = new QmcLedger();
                    eLedger.LedgerId = Convert.ToInt32(ledgerManager.GetNextLedgerId());
                    eLedger.BillDetailId = 0;
                    eLedger.BillNo = currentLedger.BillNo;
                    eLedger.Barcode = currentLedger.Barcode;
                    eLedger.BatchCode = currentLedger.BatchCode;
                    eLedger.OrderId = currentLedger.OrderId;
                    eLedger.Frequency = currentLedger.Frequency;
                    eLedger.SpecId = currentLedger.SpecId;
                    eLedger.SupplierId = currentLedger.SupplierId;
                    eLedger.ManufacturerId = currentLedger.ManufacturerId;
                    eLedger.CheckerId = UserID;
                    eLedger.ExtractorId = currentLedger.ExtractorId;
                    eLedger.ReceiverId = currentLedger.ReceiverId;
                    eLedger.FetcherId = currentLedger.FetcherId;
                    eLedger.HandlerId = currentLedger.HandlerId;
                    eLedger.SendNum = currentLedger.SampleNum;
                    eLedger.CheckResult = newCheckData.CheckResult;
                    eLedger.ReceiveDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    eLedger.SendDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                    eLedger.RecordDate = DateTime.Now;
                    eLedger.HandleMethod = "";
                    eLedger.DeleteFlag = "0";
                    if (newCheckDateList.Count > 0)
                    {
                        eLedger.CheckId = newCheckDateList[0].CheckId;
                    }
                    eLedger.SendUnit = currentLedger.SampleUnit;
                    eLedgerManager.Insert(eLedger);

                    EntityArrayList<QmcLedger> oldeLedgerList = eLedgerManager.GetListByWhereAndOrder(
                        QmcLedger._.BatchCode.Like(currentBatchCode.Substring(0, currentBatchCode.Length - 3) + "%") &&
                        QmcLedger._.BatchCode != currentBatchCode && QmcLedger._.DeleteFlag == "0",
                        QmcLedger._.BatchCode.Desc);
                    EntityArrayList<QmcLedger> neweLedgerList = eLedgerManager.GetListByWhere(QmcLedger._.BatchCode == currentBatchCode && QmcLedger._.DeleteFlag == "0");
                    if (oldeLedgerList.Count > 0 && neweLedgerList.Count > 0)
                    {
                        QmcLedger oldeLedger = oldeLedgerList[0];
                        QmcLedger neweLedger = neweLedgerList[0];
                        EntityArrayList<QmcLedgerDetail> ledgerKeyList = eLedgerDetailManager.GetListByWhere(QmcLedgerDetail._.LedgerId == oldeLedger.LedgerId);
                        foreach (QmcLedgerDetail oldeLedgerDetail in ledgerKeyList)
                        {
                            QmcLedgerDetail neweLedgerDetail = new QmcLedgerDetail();
                            neweLedgerDetail.DetailId = Convert.ToInt32(eLedgerDetailManager.GetNextDetailId());
                            neweLedgerDetail.LedgerId = neweLedger.LedgerId;
                            neweLedgerDetail.KeyId = oldeLedgerDetail.KeyId;
                            neweLedgerDetail.KeyValue = oldeLedgerDetail.KeyValue;
                            eLedgerDetailManager.Insert(neweLedgerDetail);

                        }
                    }
                    #region 回写送检明细信息
                    //2014-04-18新需求，回写所有同Barcode的送检单
                    PstMaterialChk check = materialChkManager.GetById(currentLedger.BillNo);
                    if (check != null)
                    {
                        //禁止修改已经入库原材料的台账
                        if (check.StockInFlag != "1")
                        {
                            EntityArrayList<PstMaterialChkDetail> chkDetailList = materialChkDetailManager.GetListByWhere(PstMaterialChkDetail._.Barcode == currentLedger.Barcode);
                            if (chkDetailList.Count > 0)
                            {
                                foreach (PstMaterialChkDetail chkDetail in chkDetailList)
                                {
                                    chkDetail.ChkDate = DateTime.Now;
                                    chkDetail.ChkPerson = eLedger.CheckerId;
                                    chkDetail.ChkResultFlag = eLedger.CheckResult;
                                    //若合格则设置合格数量与重量
                                    if (chkDetail.ChkResultFlag == "1")
                                    {
                                        chkDetail.PassNum = chkDetail.SendNum;
                                        chkDetail.PassWeight = chkDetail.SendWeight;
                                    }
                                    materialChkDetailManager.Update(chkDetail);
                                }
                            }
                        }
                    }
                    #endregion
                    #endregion
                    return true;
                    //msg.Alert("提示", "已成功自动生成原材料质检数据及电子台账!").Show();
                }
                else
                {
                    return false;
                    //msg.Alert("提示", "最新样品没有相关质检信息，请手动录入!").Show();
                };
            }
        }
        return false;
    }
    /// <summary>
    /// 执行行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CommandColumnCenterMaster_Command(object sender, DirectEventArgs e)
    {
        string checkId = e.ExtraParams["CheckId"];
        string commandName = e.ExtraParams["CommandName"];

        if (checkId == "")
        {
            X.Msg.Alert("提示", "检验数据记录ID为空").Show();
            return;
        }

        IQmcCheckDataManager bQmcCheckDataManager = new QmcCheckDataManager();
        QmcCheckData mQmcCheckData = bQmcCheckDataManager.GetById(checkId);

        if (commandName == "Submit")
        {
            if (mQmcCheckData.RecordStat.HasValue == true && mQmcCheckData.RecordStat.Value == 1)
            {
                X.Msg.Alert("提示", "该记录已提交，不允许重复提交").Show();
                return;
            }
            if (mQmcCheckData.RecordStat.HasValue == true && mQmcCheckData.RecordStat.Value == 0
                && mQmcCheckData.RecorderId != this.UserID && this.HiddenSpecEditFlag.Value.ToString() == "0")
            {
                X.Msg.Alert("提示", "您不是该记录的录入人，不允许提交").Show();
                return;
            }

            mQmcCheckData.LastModifierId = this.UserID;
            mQmcCheckData.LastModifyTime = DateTime.Now;
            mQmcCheckData.RecordStat = 1;
            bQmcCheckDataManager.Update(mQmcCheckData);

            // 日志
            System.Text.StringBuilder checkDataInfo = new System.Text.StringBuilder();
            checkDataInfo.AppendFormat("主数据：CheckId={0},LastModifierId={1},LastModifyTime={2}"
                , new string[] { mQmcCheckData.CheckId.ToString(), mQmcCheckData.LastModifierId
                , mQmcCheckData.LastModifyTime.HasValue == true ? mQmcCheckData.LastModifyTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "" });

            try
            {
                this.AppendWebLog("原材料质检数据提交", checkDataInfo.ToString());
            }
            catch (Exception ex)
            {
                this.AppendWebLog("原材料质检数据提交", "日志记录导常:" + ex.Message);
            }

            X.Msg.Alert("提示", "确认成功").Show();

            QueryCheckData();
        }
        else
        {
            X.Msg.Alert("提示", "未知操作").Show();
            return;
        }

    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        HiddenMasterCheckId.SetValue("");
        this.WindowMaster.Close();
    }

    /// <summary>
    /// 点击取消按钮激发的事件（审核窗口）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancelApprove_Click(object sender, DirectEventArgs e)
    {
        this.WindowApprove.Close();
    }
    #endregion
}