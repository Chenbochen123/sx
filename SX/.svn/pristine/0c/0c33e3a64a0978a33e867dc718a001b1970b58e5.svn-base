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

using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

public partial class Manager_RawMaterialQuality_Ledger : BasePage
{
    #region 属性注入
    protected IQmcLedgerKeyManager ledgerKeyManager = new QmcLedgerKeyManager();
    protected IQmcLedgerKeyValueManager ledgerKeyValueManager = new QmcLedgerKeyValueManager();
    protected IQmcLedgerManager ledgerManager = new QmcLedgerManager();
    protected IQmcLedgerDetailManager ledgerDetailManager = new QmcLedgerDetailManager();
    protected IQmcSampleLedgerManager sampleLedgerManager = new QmcSampleLedgerManager();
    protected IQmcSpecMappingManager specMappingManager = new QmcSpecMappingManager();
    protected IQmcSpecManager specManager = new QmcSpecManager();
    protected IQmcCheckDataManager checkDataManager = new QmcCheckDataManager();
    protected IPstMaterialChkManager materialChkManager = new PstMaterialChkManager();
    protected IPstMaterialChkDetailManager materialChkDetailManager = new PstMaterialChkDetailManager();
    protected IBasFactoryInfoManager factoryManager = new BasFactoryInfoManager();
    protected IBasUserManager userManager = new BasUserManager();
    protected IBasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    protected static DataTable tempDT = new DataTable();//临时存放台账列表
    protected static bool inhibitor = false;//Run once指示器
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            台账录入 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            批量修改 = new SysPageAction() { ActionID = 4, ActionName = "btnBatchModify" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 台账录入 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 批量修改 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitSeries();
            inhibitor = false;
            //初始化保存按钮状态
            this.btnModifyKeySave.Disable(true);
            this.btnAddKeySave.Disable(true);
            InitialLedgerList();//初始化台账列表
        }
        InitCustomItem();//初始化自定义项目
        if ((txtHiddenLedgerId.Text != null) && (txtHiddenLedgerId.Text != String.Empty) && (txtHiddenLedgerId.Text != ""))
        {
            UpdateCustomItem();//更新自定义项目
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
            cbxSeries.Items.Add(item);
        }
    }

    /// <summary>
    /// 初始化查询，默认昨天到今天的台账
    /// </summary>
    public void InitialLedgerList()
    {
        //每次页面加载只运行一次
        if (!inhibitor)
        {
            //初始化查询起止日期
            this.txtBeginTime.Value = DateTime.Now.Date;
            this.txtEndTime.Value = DateTime.Now.Date.AddDays(1);
            return;
        }
        inhibitor = true;
        QmcLedgerManager.QueryParams param = new QmcLedgerManager.QueryParams();
        if (txtBeginTime.Text != "0001/1/1 0:00:00")
        {
            param.beginDate = Convert.ToDateTime(txtBeginTime.Value);
        }
        if (txtEndTime.Text != "0001/1/1 0:00:00")
        {
            param.endDate = Convert.ToDateTime(txtEndTime.Value).AddDays(1);
        }
        DataTable dt = ledgerManager.GetLedgerUnion(param);
        if (dt.Columns.Count > 0)
        {
            //台账数据不为空则初始化预制功能列
            model.Fields.Clear();
            this.pnlLedger.ColumnModel.Columns.Clear();
            this.pnlLedger.ColumnModel.Columns.Add(rowNumCol1);
            this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
        }
        else
        {
            //台账数据为空则初始化数据傀儡列
            EntityArrayList<QmcLedgerKey> keylist = ledgerKeyManager.GetListByWhere(QmcLedgerKey._.DeleteFlag == "0");
            int i = 25;//列数+1
            foreach (QmcLedgerKey key in keylist)
            {
                if (key.ValueType.Contains("日期"))
                {
                    ModelField field = new ModelField();
                    DateColumn column = new DateColumn();
                    field.Name = key.KeyName;
                    field.Type = ModelFieldType.Date;
                    column.ID = "ledgercolumn" + i;
                    column.DataIndex = key.KeyName;
                    column.Text = key.KeyName;
                    column.Format = "yyyy-MM-dd";
                    this.model.Fields.Add(field);
                    this.pnlLedger.ColumnModel.Columns.Add(column);
                    this.pnlLedger.ColumnModel.Columns.Remove(commandCol1);
                    this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
                }
                else
                {
                    ModelField field = new ModelField();
                    Column column = new Column();
                    field.Name = key.KeyName;
                    column.ID = "ledgercolumn" + i;
                    column.DataIndex = key.KeyName;
                    column.Text = key.KeyName;
                    this.model.Fields.Add(field);
                    this.pnlLedger.ColumnModel.Columns.Add(column);
                    this.pnlLedger.ColumnModel.Columns.Remove(commandCol1);
                    this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
                }
                i++;
            }
        }
        //初始化台账数据列
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].ColumnName.Contains("时间"))
            {
                ModelField field = new ModelField();
                DateColumn column = new DateColumn();
                field.Name = dt.Columns[i].ColumnName;
                field.Type = ModelFieldType.Date;
                column.ID = "ledgercolumn" + i;
                column.DataIndex = dt.Columns[i].ColumnName;
                column.Text = dt.Columns[i].ColumnName;
                if (dt.Columns[i].ColumnName.Contains("记录时间"))
                {
                    column.Format = "yyyy-MM-dd HH:mm:ss";
                }
                else
                {
                    column.Format = "yyyy-MM-dd";
                }
                if (column.Text == "BillDetailId" || column.Text == "OrderId" || column.Text == "LedgerId" || column.Text == "CheckId")
                {
                    column.Visible = false;
                }
                this.model.Fields.Add(field);
                this.pnlLedger.ColumnModel.Columns.Add(column);
                this.pnlLedger.ColumnModel.Columns.Remove(commandCol1);
                this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
            }
            else
            {
                ModelField field = new ModelField();
                Column column = new Column();
                field.Name = dt.Columns[i].ColumnName;
                column.ID = "ledgercolumn" + i;
                column.DataIndex = dt.Columns[i].ColumnName;
                column.Text = dt.Columns[i].ColumnName;
                if (column.Text == "BillDetailId" || column.Text == "OrderId" || column.Text == "LedgerId" || column.Text == "CheckId")
                {
                    column.Visible = false;
                }
                this.model.Fields.Add(field);
                this.pnlLedger.ColumnModel.Columns.Add(column);
                this.pnlLedger.ColumnModel.Columns.Remove(commandCol1);
                this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
            }
        }
        tempDT = dt;
        pnlLedger.GetStore().DataSource = dt;
        pnlLedger.GetStore().DataBind();
    }

    /// <summary>
    /// 根据条件查询台账
    /// </summary>
    [DirectMethod]
    public void ReloadLedgerList()
    {
        QmcLedgerManager.QueryParams param = new QmcLedgerManager.QueryParams();
        param.barcode = txtBarcode.Text.TrimStart().TrimEnd();
        param.billNo = txtBillNo.Text.TrimStart().TrimEnd();
        param.checkResult = cbxCheckResult.Value.ToString();
        param.specId = cbxSpec.Value.ToString();
        param.materialCode = cbxMaterial.Value.ToString();
        param.seriesId = cbxSeries.Value.ToString();
        if (txtBeginTime.Text != "0001/1/1 0:00:00")
        {
            param.beginDate = Convert.ToDateTime(txtBeginTime.Value);
        }
        if (txtEndTime.Text != "0001/1/1 0:00:00")
        {
            param.endDate = Convert.ToDateTime(txtEndTime.Value).AddDays(1);
        }
        DataTable dt = ledgerManager.GetLedgerUnion(param);
        if (dt.Columns.Count > 0)
        {
            //台账数据不为空则加载预制功能列
            model.Fields.Clear();
            this.pnlLedger.ColumnModel.Columns.Clear();
            this.pnlLedger.ColumnModel.Columns.Add(rowNumCol1);
            this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
        }
        else
        {
            //台账数据为空则加载傀儡数据列
            EntityArrayList<QmcLedgerKey> keylist = ledgerKeyManager.GetListByWhere(QmcLedgerKey._.DeleteFlag == "0");
            int i = 25;//列数+1
            foreach (QmcLedgerKey key in keylist)
            {
                if (key.ValueType.Contains("日期"))
                {
                    ModelField field = new ModelField();
                    DateColumn column = new DateColumn();
                    field.Name = key.KeyName;
                    field.Type = ModelFieldType.Date;
                    column.ID = "ledgercolumn" + i;
                    column.DataIndex = key.KeyName;
                    column.Text = key.KeyName;
                    column.Format = "yyyy-MM-dd";
                    this.model.Fields.Add(field);
                    this.pnlLedger.ColumnModel.Columns.Add(column);
                    this.pnlLedger.ColumnModel.Columns.Remove(commandCol1);
                    this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
                }
                else
                {
                    ModelField field = new ModelField();
                    Column column = new Column();
                    field.Name = key.KeyName;
                    column.ID = "ledgercolumn" + i;
                    column.DataIndex = key.KeyName;
                    column.Text = key.KeyName;
                    this.model.Fields.Add(field);
                    this.pnlLedger.ColumnModel.Columns.Add(column);
                    this.pnlLedger.ColumnModel.Columns.Remove(commandCol1);
                    this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
                }
                i++;
            }
        }
        //加载台账数据列
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].ColumnName.Contains("时间"))
            {
                ModelField field = new ModelField();
                DateColumn column = new DateColumn();
                field.Name = dt.Columns[i].ColumnName;
                field.Type = ModelFieldType.Date;
                column.ID = "ledgercolumn" + i;
                column.DataIndex = dt.Columns[i].ColumnName;
                column.Text = dt.Columns[i].ColumnName;
                if (dt.Columns[i].ColumnName.Contains("记录时间"))
                {
                    column.Format = "yyyy-MM-dd HH:mm:ss";
                }
                else
                {
                    column.Format = "yyyy-MM-dd";
                }
                if (column.Text == "BillDetailId" || column.Text == "OrderId" || column.Text == "LedgerId" || column.Text == "CheckId")
                {
                    column.Visible = false;
                }
                this.model.Fields.Add(field);
                this.pnlLedger.ColumnModel.Columns.Add(column);
                this.pnlLedger.ColumnModel.Columns.Remove(commandCol1);
                this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
            }
            else
            {
                ModelField field = new ModelField();
                Column column = new Column();
                field.Name = dt.Columns[i].ColumnName;
                column.ID = "ledgercolumn" + i;
                column.DataIndex = dt.Columns[i].ColumnName;
                column.Text = dt.Columns[i].ColumnName;
                if (column.Text == "BillDetailId" || column.Text == "OrderId" || column.Text == "LedgerId" || column.Text == "CheckId")
                {
                    column.Visible = false;
                }
                this.model.Fields.Add(field);
                this.pnlLedger.ColumnModel.Columns.Add(column);
                this.pnlLedger.ColumnModel.Columns.Remove(commandCol1);
                this.pnlLedger.ColumnModel.Columns.Add(commandCol1);
            }
        }
        tempDT = dt;
        pnlLedger.GetStore().DataSource = dt;
        pnlLedger.GetStore().DataBind();
    }

    /// <summary>
    /// 载入新增台账初始信息
    /// </summary>
    [DirectMethod]
    public void LoadAddCheckDetail()
    {
        string checkId = txtAddCheckId.Text;
        //根据选择的检测记录更新新增窗口
        QmcCheckData data = checkDataManager.GetById(checkId);
        QmcSampleLedger sampleLedger = sampleLedgerManager.GetById(data.LedgerId);
        BasFactoryInfo supplier = factoryManager.GetById(sampleLedger.SupplierId);
        BasFactoryInfo manufacturer = factoryManager.GetById(sampleLedger.ManufacturerId);
        txtAddOrderId.Text = sampleLedger.OrderId.ToString();
        txtAddSupplierId.Text = sampleLedger.SupplierId.ToString();
        txtAddManufacturerId.Text = sampleLedger.ManufacturerId.ToString();
        trfAddSupplierName.Text = supplier.FacName;
        if (manufacturer != null)
        {
            txtAddManufacturerName.Text = manufacturer.FacName;
        }
        txtAddMaterialName.Text = sampleLedger.SampleName;
        txtAddFrequency.Text = data.Frequency;
        txtAddBarcode.Text = data.Barcode;
        txtAddBatchCode.Text = data.BatchCode;
        txtAddSpecId.Text = data.SpecId.ToString();
        txtAddSendNum.Text = sampleLedger.SampleNum.ToString();
        txtAddUnit.Text = sampleLedger.SampleUnit;
        txtAddExtractorId.Text = sampleLedger.ExtractorId;
        txtAddFetcherId.Text = sampleLedger.FetcherId;
        txtAddReceiverId.Text = sampleLedger.ReceiverId;
        txtAddHandlerId.Text = sampleLedger.HandlerId;
        txtAddRemark.Text = sampleLedger.Remark;
        dtfAddHandleDate.Value = sampleLedger.HandleDate;
        dtfAddReceiveDate.Value = sampleLedger.ReceiveDate;
        dtfAddReturnDate.Value = sampleLedger.ReturnDate;
        dtfAddSendDate.Value = sampleLedger.SendDate;
        cbxAddHandleMethod.SetValue(sampleLedger.HandleMethod);
        if (data.CheckResult == "1" || data.CheckResult == "2")
        {
            txtAddCheckResult.Value = "合格";
        }
        else if (data.CheckResult == "0")
        {
            txtAddCheckResult.Text = "不合格";
        }
    }

    /// <summary>
    /// 初始化自定义项目
    /// </summary>
    public void InitCustomItem()
    {
        this.pnlAddLedgerCustom.Visible = true;
        EntityArrayList<QmcLedgerKey> keylist = ledgerKeyManager.GetListByWhere(QmcLedgerKey._.DeleteFlag == "0");
        bool isDouble = false;
        foreach (QmcLedgerKey key in keylist)
        {
            switch (key.ValueType)
            {
                case "文字":
                    if (key.HasSelection == "1")
                    {
                        EntityArrayList<QmcLedgerKeyValue> valueList = new EntityArrayList<QmcLedgerKeyValue>();
                        valueList = ledgerKeyValueManager.GetListByWhere(QmcLedgerKeyValue._.KeyId == key.KeyId && QmcLedgerKeyValue._.DeleteFlag == "0");
                        ComboBox cb = new ComboBox();
                        cb.FieldLabel = key.KeyName;
                        cb.LabelAlign = LabelAlign.Right;
                        cb.ID = "cc" + key.KeyId;
                        cb.MaxLength = 50;
                        cb.Padding = 5;
                        if (isDouble)
                        {
                            cb.LabelPad = 11;
                        }
                        foreach (QmcLedgerKeyValue value in valueList)
                        {
                            Ext.Net.ListItem selection = new Ext.Net.ListItem();
                            selection.Text = value.KeyValue;
                            cb.Items.Add(selection);
                        }
                        this.pnlAddLedgerCustom.Items.Add(cb);
                        isDouble = !isDouble;
                    }
                    else
                    {
                        TextField tf = new TextField();
                        tf.FieldLabel = key.KeyName;
                        tf.LabelAlign = LabelAlign.Right;
                        tf.ID = "cc" + key.KeyId;
                        tf.MaxLength = 50;
                        tf.Padding = 5;
                        if (isDouble)
                        {
                            tf.LabelPad = 11;
                        }
                        this.pnlAddLedgerCustom.Items.Add(tf);
                        isDouble = !isDouble;
                    }
                    break;
                case "数字":
                    if (key.HasSelection == "1")
                    {
                        EntityArrayList<QmcLedgerKeyValue> valueList = new EntityArrayList<QmcLedgerKeyValue>();
                        valueList = ledgerKeyValueManager.GetListByWhere(QmcLedgerKeyValue._.KeyId == key.KeyId && QmcLedgerKeyValue._.DeleteFlag == "0");
                        ComboBox cb = new ComboBox();
                        cb.FieldLabel = key.KeyName;
                        cb.LabelAlign = LabelAlign.Right;
                        cb.ID = "cc" + key.KeyId;
                        cb.MaxLength = 20;
                        cb.Padding = 5;
                        if (isDouble)
                        {
                            cb.LabelPad = 11;
                        }
                        foreach (QmcLedgerKeyValue value in valueList)
                        {
                            Ext.Net.ListItem selection = new Ext.Net.ListItem();
                            selection.Text = value.KeyValue;
                            cb.Items.Add(selection);
                        }
                        this.pnlAddLedgerCustom.Items.Add(cb);
                        isDouble = !isDouble;
                    }
                    else
                    {
                        TextField tf = new TextField();
                        tf.FieldLabel = key.KeyName;
                        tf.LabelAlign = LabelAlign.Right;
                        tf.ID = "cc" + key.KeyId;
                        tf.MaxLength = 20;
                        tf.Padding = 5;
                        if (isDouble)
                        {
                            tf.LabelPad = 11;
                        }
                        this.pnlAddLedgerCustom.Items.Add(tf);
                        isDouble = !isDouble;
                    }
                    break;
                case "日期":
                    DateField df = new DateField();
                    df.FieldLabel = key.KeyName;
                    df.LabelAlign = LabelAlign.Right;
                    df.Editable = false;
                    df.ID = "cc" + key.KeyId;
                    df.Padding = 5;
                    if (isDouble)
                    {
                        df.LabelPad = 11;
                    }
                    this.pnlAddLedgerCustom.Items.Add(df);
                    isDouble = !isDouble;
                    break;
                default:
                    break;
            }
        }
        if (this.pnlAddLedgerCustom.Items.Count == 0)
        {
            this.pnlAddLedgerCustom.Visible = false;
        }
        this.pnlAddLedgerCustom.Render();
    }

    /// <summary>
    /// 更新自定义项目
    /// </summary>
    public void UpdateCustomItem()
    {
        this.pnlModifyLedgerCustom.Visible = true;
        EntityArrayList<QmcLedgerKey> keylist = ledgerKeyManager.GetListByWhere(QmcLedgerKey._.DeleteFlag == "0");
        EntityArrayList<QmcLedgerDetail> detaillist = ledgerDetailManager.GetListByWhere(QmcLedgerDetail._.LedgerId == Convert.ToInt32(txtHiddenLedgerId.Text));
        bool isDouble = false;
        foreach (QmcLedgerKey key in keylist)
        {
            foreach (QmcLedgerDetail detail in detaillist)
            {
                if (key.KeyId == detail.KeyId)
                {
                    switch (key.ValueType)
                    {
                        case "文字":
                            if (key.HasSelection == "1")
                            {
                                EntityArrayList<QmcLedgerKeyValue> valueList = new EntityArrayList<QmcLedgerKeyValue>();
                                valueList = ledgerKeyValueManager.GetListByWhere(QmcLedgerKeyValue._.KeyId == key.KeyId && QmcLedgerKeyValue._.DeleteFlag == "0");
                                ComboBox cb = new ComboBox();
                                cb.FieldLabel = key.KeyName;
                                cb.LabelAlign = LabelAlign.Right;
                                cb.ID = "cm" + key.KeyId;
                                cb.MaxLength = 50;
                                cb.Padding = 5;
                                if (isDouble)
                                {
                                    cb.LabelPad = 11;
                                }
                                foreach (QmcLedgerKeyValue value in valueList)
                                {
                                    Ext.Net.ListItem selection = new Ext.Net.ListItem();
                                    selection.Text = value.KeyValue;
                                    cb.Items.Add(selection);
                                }
                                cb.Value = detail.KeyValue;
                                this.pnlModifyLedgerCustom.Items.Add(cb);
                                isDouble = !isDouble;
                            }
                            else
                            {
                                TextField tf = new TextField();
                                tf.FieldLabel = key.KeyName;
                                tf.LabelAlign = LabelAlign.Right;
                                tf.ID = "cm" + key.KeyId;
                                tf.MaxLength = 50;
                                tf.Padding = 5;
                                if (isDouble)
                                {
                                    tf.LabelPad = 11;
                                }
                                tf.Value = detail.KeyValue;
                                this.pnlModifyLedgerCustom.Items.Add(tf);
                                isDouble = !isDouble;
                            }
                            break;
                        case "数字":
                            if (key.HasSelection == "1")
                            {
                                EntityArrayList<QmcLedgerKeyValue> valueList = new EntityArrayList<QmcLedgerKeyValue>();
                                valueList = ledgerKeyValueManager.GetListByWhere(QmcLedgerKeyValue._.KeyId == key.KeyId && QmcLedgerKeyValue._.DeleteFlag == "0");
                                ComboBox cb = new ComboBox();
                                cb.FieldLabel = key.KeyName;
                                cb.LabelAlign = LabelAlign.Right;
                                cb.ID = "cm" + key.KeyId;
                                cb.MaxLength = 20;
                                cb.Padding = 5;
                                if (isDouble)
                                {
                                    cb.LabelPad = 11;
                                }
                                foreach (QmcLedgerKeyValue value in valueList)
                                {
                                    Ext.Net.ListItem selection = new Ext.Net.ListItem();
                                    selection.Text = value.KeyValue;
                                    cb.Items.Add(selection);
                                }
                                cb.Value = detail.KeyValue;
                                this.pnlModifyLedgerCustom.Items.Add(cb);
                                isDouble = !isDouble;
                            }
                            else
                            {
                                TextField tf = new TextField();
                                tf.FieldLabel = key.KeyName;
                                tf.LabelAlign = LabelAlign.Right;
                                tf.ID = "cm" + key.KeyId;
                                tf.MaxLength = 20;
                                tf.Padding = 5;
                                if (isDouble)
                                {
                                    tf.LabelPad = 11;
                                }
                                tf.Value = detail.KeyValue;
                                this.pnlModifyLedgerCustom.Items.Add(tf);
                                isDouble = !isDouble;
                            }
                            break;
                        case "日期":
                            DateField df = new DateField();
                            df.FieldLabel = key.KeyName;
                            df.LabelAlign = LabelAlign.Right;
                            df.Editable = false;
                            df.ID = "cm" + key.KeyId;
                            df.Padding = 5;
                            if (isDouble)
                            {
                                df.LabelPad = 11;
                            }
                            df.Value = detail.KeyValue;
                            this.pnlModifyLedgerCustom.Items.Add(df);
                            isDouble = !isDouble;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        if (this.pnlModifyLedgerCustom.Items.Count == 0)
        {
            this.pnlModifyLedgerCustom.Visible = false;
        }
        this.pnlModifyLedgerCustom.Render();
    }
    #endregion

    #region 增删改查按钮激发的事件

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
        EntityArrayList<BasMaterial> mBasMaterialList = materialManager.GetListByWhereAndOrder(
            BasMaterial._.DeleteFlag == "0"
            & BasMaterial._.MajorTypeID == 1
            & BasMaterial._.MinorTypeID == minorTypeId
            , BasMaterial._.MaterialName.Asc);
        foreach (BasMaterial mBasMaterial in mBasMaterialList)
        {
            cbxMaterial.AddItem(mBasMaterial.MaterialName, mBasMaterial.MaterialCode);
        }
        ReloadLedgerList();
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
            ReloadLedgerList();
            return;
        }
        EntityArrayList<QmcSpecMapping> specMappingList = specMappingManager.GetListByWhere((QmcSpecMapping._.MaterialCode == materialCode) && (QmcSpecMapping._.DeleteFlag == "0"));
        foreach (QmcSpecMapping mapping in specMappingList)
        {
            cbxSpec.AddItem(mapping.SpecName, mapping.SpecId);
        }
        ReloadLedgerList();
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
            ReloadLedgerList();
            return;
        }
        ReloadLedgerList();
    }

    /// <summary>
    /// 点击添加按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //初始化新增窗口
        this.txtAddBarcode.Value = "";
        this.txtAddBatchCode.Value = "";
        this.txtAddCheckerId.Value = "";
        this.txtAddCheckId.Value = "";
        this.txtAddExtractorId.Value = "";
        this.txtAddSpec.Value = "";
        this.trfAddSupplierName.Value = "";
        this.txtAddSupplierId.Value = "";
        this.txtAddManufacturerName.Value = "";
        this.txtAddManufacturerId.Value = "";
        this.txtAddFetcherId.Value = "";
        this.txtAddHandlerId.Value = "";
        this.txtAddMaterCode.Value = "";
        this.txtAddMaterialName.Value = "";
        this.txtAddOrderId.Value = "";
        this.txtAddFrequency.Value = "";
        this.txtAddUnit.Value = "";
        this.txtAddReceiverId.Value = "";
        this.txtAddRemark.Value = "";
        this.txtAddSendNum.Value = "";
        this.trfAddBillNo.Value = "";
        this.trfAddCheckerName.Value = "";
        this.trfAddExtractorName.Value = "";
        this.trfAddFetcherName.Value = "";
        this.trfAddHandlerName.Value = "";
        this.trfAddReceiverName.Value = "";
        this.dtfAddHandleDate.Value = "";
        this.dtfAddReceiveDate.Value = DateTime.Now.Date;
        this.dtfAddReturnDate.Value = "";
        this.dtfAddSendDate.Value = DateTime.Now.Date;
        this.cbxAddHandleMethod.Value = "";
        this.windowAddLedger.Show();
    }

    /// <summary>
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        if (tempDT.Rows.Count > 0)
        {
            if (CheckboxSelectionModelCenterMaster.SelectedRows.Count > 0)
            {
                string exportDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                string exportUser = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
                #region 加载excel模板
                string xlsPath = "Ledger.xls";
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
                HSSFSheet modelSheet = (HSSFSheet)workbook.GetSheetAt(0);
                ISheet sheet = modelSheet;
                sheet.GetRow(0).GetCell(5).SetCellValue(exportUser);
                sheet.GetRow(0).GetCell(9).SetCellValue(exportDate);
                int originColumnsCount = 24;
                int allColumnsCount = tempDT.Columns.Count;
                int customStartIndexInXls = 20;
                //for (int i = originColumnsCount; i < allColumnsCount; i++)
                //{
                //    sheet.GetRow(1).CreateCell(customStartIndexInXls).SetCellValue(tempDT.Columns[i].ColumnName);//设置台账自定义项目咧
                //    sheet.AddMergedRegion(new CellRangeAddress(1, 2, customStartIndexInXls, customStartIndexInXls));//合并单元格
                //    customStartIndexInXls++;
                //}
                #endregion
                int rowCounter = 0;
                foreach (SelectedRow row in CheckboxSelectionModelCenterMaster.SelectedRows)
                {
                    foreach (DataRow dr in tempDT.Rows)
                    {
                        if (row.RecordID == dr["LedgerId"].ToString())
                        {
                            int columnCounter = 0;
                            #region 写入台账自身数据
                            sheet.CreateRow(rowCounter + 3).CreateCell(0).SetCellValue(dr["原材料名称"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(1).SetCellValue(dr["送检单号"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(2).SetCellValue(dr["条码号"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(3).SetCellValue(dr["批次号"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(4).SetCellValue(dr["送检数量"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(5).SetCellValue(dr["单位"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(6).SetCellValue(dr["规格"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(7).SetCellValue(dr["检测结果"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(8).SetCellValue(dr["检测人"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(9).SetCellValue(dr["取样人"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(10).SetCellValue(dr["接收人"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(11).SetCellValue(dr["领取人"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(12).SetCellValue(dr["处置人"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(13).SetCellValue(dr["处置方式"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(14).SetCellValue(dr["处置时间"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(15).SetCellValue(dr["发放时间"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(16).SetCellValue(dr["接收时间"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(17).SetCellValue(dr["返库时间"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(18).SetCellValue(dr["记录时间"].ToString());
                            sheet.GetRow(rowCounter + 3).CreateCell(19).SetCellValue(dr["备注"].ToString());
                            #endregion

                            #region 写入检测数据
                            IQmcCheckDataDetailManager bQmcCheckDataDetailManager = new QmcCheckDataDetailManager();
                            DataSet dsQmcCheckDataDetail = bQmcCheckDataDetailManager.GetDataSetByCheckId(dr["CheckId"].ToString());
                            if (dsQmcCheckDataDetail.Tables[0].Rows.Count > 0)
                            {
                                DataColumn dcTextCheckResult = new DataColumn("TextCheckResult");
                                DataColumn dcTextIsPrime = new DataColumn("TextIsPrime");
                                dsQmcCheckDataDetail.Tables[0].Columns.Add(dcTextCheckResult);
                                dsQmcCheckDataDetail.Tables[0].Columns.Add(dcTextIsPrime);
                                foreach (DataRow detailDr in dsQmcCheckDataDetail.Tables[0].Rows)
                                {
                                    if (detailDr["AutoCheckResult"].ToString() == "0")
                                    {
                                        detailDr["TextCheckResult"] = "不合格";
                                        detailDr["TextIsPrime"] = "N/A";
                                    }
                                    else if (detailDr["AutoCheckResult"].ToString() == "1")
                                    {
                                        detailDr["TextCheckResult"] = "合格";
                                        if (detailDr["IsPrime"].ToString() == "1")
                                        {
                                            detailDr["TextIsPrime"] = "是";
                                        }
                                        else if (detailDr["IsPrime"].ToString() == "0")
                                        {
                                            detailDr["TextIsPrime"] = "否";
                                        }
                                        else
                                        {
                                            detailDr["TextIsPrime"] = "无标准";
                                        }
                                    }
                                }
                                DataTable dt = dsQmcCheckDataDetail.Tables[0];
                                int dataStartIndexInXls = 20;
                                foreach (DataRow resultRow in dt.Rows)
                                {
                                    sheet.GetRow(rowCounter + 3).CreateCell(columnCounter + dataStartIndexInXls).SetCellValue(resultRow["ItemName"].ToString());
                                    columnCounter++;
                                    sheet.GetRow(rowCounter + 3).CreateCell(columnCounter + dataStartIndexInXls).SetCellValue(resultRow["CheckValue"].ToString());
                                    columnCounter++;
                                }
                            }
                            #endregion
                            rowCounter++;
                        }
                    }
                }

                #region 生成台账下载
                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                string fileName = "电子台账";
                new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, fileName);
                #endregion
            }
            else
            {
                msg.Alert("错误", "请选择至少一条台账记录！");
                msg.Show();
            }
        }
        else
        {
            msg.Alert("操作", "没有可以导出的内容！");
            msg.Show();
            ReloadLedgerList();
        }
    }

    /// <summary>
    /// 查询按钮点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtBeginTime.Text) > Convert.ToDateTime(txtEndTime.Text))
        {
            msg.Alert("操作", "起始时间不能晚于结束时间！");
            msg.Show();
            return;
        }
        ReloadLedgerList();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// </summary>
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string ledgerId)
    {
        try
        {
            QmcLedger ledger = ledgerManager.GetById(Convert.ToInt32(ledgerId));
            ledger.DeleteFlag = "1";
            ledgerManager.Update(ledger);
            this.AppendWebLog("电子台账删除", "台账编号：" + ledgerId);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    /// <summary>
    /// 点击批量修改激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_batchModify_Click(object sender, EventArgs e)
    {
        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            msg.Alert("操作", "请至少选择一条台账！");
            msg.Show();
        }
        else
        {
            cbxBatchModifyHandleMethod.SelectedItem.Text = "";
            cbxBatchModifyHandleMethod.SelectedItem.Value = "";
            dtfBatchModifyHandleDate.Value = DateTime.Now.Date;
            dtfBatchModifyReturnDate.Value = DateTime.Now.Date;
            this.windowBatchModify.Show();
        }
    }

    /// <summary>
    /// 点击批量修改中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnBatchModifySave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            int batchCount = 0;
            foreach (SelectedRow row in CheckboxSelectionModelCenterMaster.SelectedRows)
            {
                QmcLedger ledger = ledgerManager.GetById(row.RecordID);
                if (ledger != null)
                {
                    if (dtfBatchModifyHandleDate.Text.Substring(0, 4) != "0001")
                    {
                        ledger.HandleDate = Convert.ToDateTime(dtfBatchModifyHandleDate.Value);
                    }
                    if (dtfBatchModifyReturnDate.Text.Substring(0, 4) != "0001")
                    {
                        ledger.ReturnDate = Convert.ToDateTime(dtfBatchModifyReturnDate.Value);
                    }
                    if (cbxBatchModifyHandleMethod.Value != null)
                    {
                        ledger.HandleMethod = cbxBatchModifyHandleMethod.Value.ToString();
                    }
                    if (txtBatchModifyHandlerId.Value != null)
                    {
                        ledger.HandlerId = txtBatchModifyHandlerId.Value.ToString();
                    }
                    ledgerManager.Update(ledger);
                    EntityArrayList<QmcCheckData> checkDataList = checkDataManager.GetListByWhere(QmcCheckData._.CheckId == ledger.CheckId);
                    if (checkDataList.Count > 0)
                        foreach (QmcCheckData cData in checkDataList)
                        {
                            EntityArrayList<QmcSampleLedger> sLedgerList = sampleLedgerManager.GetListByWhere(QmcSampleLedger._.LedgerId == cData.LedgerId);
                            if (sLedgerList.Count > 0)
                            {
                                foreach (QmcSampleLedger sLedger in sLedgerList)
                                {
                                    sLedger.HandleMethod = ledger.HandleMethod;
                                    sLedger.HandlerId = ledger.HandlerId;
                                    sLedger.HandleDate = ledger.HandleDate;
                                    sLedger.ExtractorId = ledger.ExtractorId;
                                    sLedger.FetcherId = ledger.FetcherId;
                                    sLedger.ManufacturerId = ledger.ManufacturerId;
                                    sLedger.ReceiveDate = ledger.ReceiveDate;
                                    sLedger.ReceiverId = ledger.ReceiverId;
                                    sLedger.ReturnDate = ledger.ReturnDate;
                                    sLedger.SendDate = ledger.SendDate;
                                    sLedger.SupplierId = ledger.SupplierId;
                                    sLedger.Remark = ledger.Remark;
                                    sampleLedgerManager.Update(sLedger);
                                }
                            }
                        }
                    batchCount++;
                }
            }
            this.AppendWebLog("电子台账批量修改", "条目数：" + batchCount);
            pageToolBar.DoRefresh();
            msg.Alert("操作", "已修改" + batchCount + "条电子台账！");
            msg.Show();
            this.windowBatchModify.Close();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string ledgerId)
    {
        QmcLedger ledger = ledgerManager.GetById(Convert.ToInt32(ledgerId));
        PstMaterialChk check = materialChkManager.GetById(ledger.BillNo);
        if (check != null)
        {
            //禁止修改已经入库原材料的台账
            if (check.StockInFlag == "1")
            {
                msg.Alert("操作", "此原材料已入库，不能修改！");
                msg.Show();
                return;
            }
        }
        BasFactoryInfo supplier = new BasFactoryInfo();
        BasFactoryInfo manufacturer = new BasFactoryInfo();
        QmcSpec spec = new QmcSpec();
        if (ledger.SupplierId != null)
        {
            supplier = factoryManager.GetById(ledger.SupplierId);
        }
        if (ledger.ManufacturerId != null)
        {
            manufacturer = factoryManager.GetById(ledger.ManufacturerId);
        }
        string checkId = ledger.CheckId.ToString();
        //根据选择的检测记录更新新增窗口
        QmcCheckData data = checkDataManager.GetById(checkId);
        if (data.SpecId != null)
        {
            spec = specManager.GetById(data.SpecId);
        }
        QmcSampleLedger sampleLedger = sampleLedgerManager.GetById(data.LedgerId);
        this.txtModifyMaterialName.Text = sampleLedger.SampleName;
        if (supplier != null)
        {
            this.trfModifySupplierName.Text = supplier.FacName;
            this.txtModifySupplierId.Text = supplier.ObjID.ToString();
        }
        if (manufacturer != null)
        {
            this.txtModifyManufacturerName.Text = manufacturer.FacName;
            this.txtModifyManufacturerId.Text = manufacturer.ObjID.ToString();
        }
        this.txtModifyMaterCode.Value = data.MaterCode;
        this.txtModifySpec.Value = spec.SpecName;

        this.txtModifyDetailId.Value = ledger.BillDetailId;
        this.txtModifyBillNo.Value = ledger.BillNo;
        this.txtModifyBarcode.Value = ledger.Barcode;
        this.txtModifyBatchCode.Value = ledger.BatchCode;
        this.txtModifyOrderId.Value = ledger.OrderId;

        this.txtModifyCheckerId.Value = ledger.CheckerId;
        this.txtModifyExtractorId.Value = ledger.ExtractorId;
        this.txtModifyReceiverId.Value = ledger.ReceiverId;
        this.txtModifyFetcherId.Value = ledger.FetcherId;
        this.txtModifyHandlerId.Value = ledger.HandlerId;

        this.trfModifyCheckerName.Value = GetUserName(ledger.CheckerId);
        this.trfModifyExtractorName.Value = GetUserName(ledger.ExtractorId);
        this.trfModifyReceiverName.Value = GetUserName(ledger.ReceiverId);
        this.trfModifyFetcherName.Value = GetUserName(ledger.FetcherId);
        this.trfModifyHandlerName.Value = GetUserName(ledger.HandlerId);

        this.dtfModifySendDate.Value = ledger.SendDate;
        this.dtfModifyReturnDate.Value = ledger.ReturnDate;
        this.dtfModifyReceiveDate.Value = ledger.ReceiveDate;
        this.dtfModifyHandleDate.Value = ledger.HandleDate;
        this.cbxModifyHandleMethod.Value = ledger.HandleMethod;

        this.txtModifySendNum.Value = ledger.SendNum;
        this.txtModifyUnit.Value = ledger.SendUnit;
        this.txtModifyFrequency.Value = ledger.Frequency;
        if (ledger.CheckResult == "2")
        {
            this.txtModifyCheckResult.Value = "不合格";
        }
        else if (ledger.CheckResult == "1")
        {
            this.txtModifyCheckResult.Value = "合格";
        }
        this.txtModifyRemark.Value = ledger.Remark;

        this.txtHiddenLedgerId.Text = ledger.LedgerId.ToString();
        UpdateCustomItem();
        this.windowModifyLedger.Show();
    }

    /// <summary>
    /// 根据HR代码获取用户名
    /// </summary>
    /// <param name="hrCode"></param>
    /// <returns></returns>
    private string GetUserName(string hrCode)
    {
        BasUser user = new BasUser();
        EntityArrayList<BasUser> lst = new EntityArrayList<BasUser>();
        lst = userManager.GetListByWhere(BasUser._.HRCode == hrCode);
        if (lst.Count > 0)
        {
            user = lst[0];
            if (!string.IsNullOrEmpty(user.UserName))
            {
                return user.UserName;
            }
        }
        return String.Empty;
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.windowAddLedger.Close();
        this.windowModifyLedger.Close();
        this.windowBatchModify.Close();
    }

    /// <summary>
    /// 点击添加台账中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            #region 校验输入
            if ((txtAddSendNum.Text != "") && (txtAddSendNum.Text != null))
            {
                if (!Regex.Match(txtAddSendNum.Value.ToString(), @"^(0|[1-9]\d{0,11})(\.\d{0,3})?$").Success)
                {
                    msg.Alert("操作", "输入数字格式不正确！应为12位有效数字，保留3位小数！");
                    msg.Show();
                    return;
                }
            }
            #endregion

            #region 保存台账
            QmcLedger ledger = new QmcLedger();
            ledger.LedgerId = Convert.ToInt32(ledgerManager.GetNextLedgerId());
            ledger.BillNo = trfAddBillNo.Text.ToString();
            ledger.Barcode = txtAddBarcode.Text.ToString();
            ledger.BatchCode = txtAddBatchCode.Text.ToString();
            ledger.CheckId = Convert.ToInt32(txtAddCheckId.Text);
            ledger.CheckerId = txtAddCheckerId.Text.ToString();
            ledger.ExtractorId = txtAddExtractorId.Text.ToString();
            ledger.FetcherId = txtAddFetcherId.Text.ToString();
            ledger.HandlerId = txtAddHandlerId.Text.ToString();
            ledger.ReceiverId = txtAddReceiverId.Text.ToString();
            ledger.Frequency = txtAddFrequency.Text.ToString();
            ledger.SendUnit = txtAddUnit.Text.ToString();
            ledger.SpecId = Convert.ToInt32(String.IsNullOrEmpty(txtAddSpecId.Text)? "9999" : txtAddSpecId.Text);
            if ((txtAddOrderId.Text != "") && (txtAddOrderId.Text != null) && (txtAddOrderId.Text != "0"))
            {
                ledger.OrderId = Convert.ToInt32(txtAddOrderId.Text);
            }
            if ((txtAddSupplierId.Text != "") && (txtAddSupplierId.Text != null) && (txtAddSupplierId.Text != "0"))
            {
                ledger.SupplierId = Convert.ToInt32(txtAddSupplierId.Text);
            }
            if ((txtAddManufacturerId.Text != "") && (txtAddManufacturerId.Text != null) && (txtAddManufacturerId.Text != "0"))
            {
                ledger.ManufacturerId = Convert.ToInt32(txtAddManufacturerId.Text);
            }
            ledger.RecordDate = DateTime.Now;
            ledger.Remark = txtAddRemark.Text.ToString();
            if (dtfAddHandleDate.Text.Substring(0, 4) != "0001")
            {
                ledger.HandleDate = Convert.ToDateTime(dtfAddHandleDate.Value);
            }
            if (dtfAddReceiveDate.Text.Substring(0, 4) != "0001")
            {
                ledger.ReceiveDate = Convert.ToDateTime(dtfAddReceiveDate.Value);
            }
            if (dtfAddReturnDate.Text.Substring(0, 4) != "0001")
            {
                ledger.ReturnDate = Convert.ToDateTime(dtfAddReturnDate.Value);
            }
            if (dtfAddSendDate.Text.Substring(0, 4) != "0001")
            {
                ledger.SendDate = Convert.ToDateTime(dtfAddSendDate.Value);
            }
            ledger.HandleMethod = cbxAddHandleMethod.Value.ToString();
            ledger.SendNum = Convert.ToDecimal(txtAddSendNum.Text);
            if (txtAddCheckResult.Value.ToString() == "合格")
            {
                ledger.CheckResult = "1";
            }
            else if (txtAddCheckResult.Value.ToString() == "不合格")
            {
                ledger.CheckResult = "2";
            }
            ledger.DeleteFlag = "0";
            try
            {
                ledgerManager.Insert(ledger);
            }
            catch (Exception ex)
            {
                msg.Alert("操作", "保存失败：" + ex);
                msg.Show();
                return;
            }
            #endregion

            #region 保存台账自定义项目值
            int i = this.pnlAddLedgerCustom.Items.Count;
            EntityArrayList<QmcLedgerKey> keylist = ledgerKeyManager.GetListByWhere(QmcLedgerKey._.DeleteFlag == "0");
            foreach (QmcLedgerKey key in keylist)
            {
                foreach (Control item in pnlAddLedgerCustom.Items)
                {
                    if (item.ID == "cc" + key.KeyId)
                    {
                        QmcLedgerDetail ledgerDetail = new QmcLedgerDetail();
                        string value = String.Empty;
                        switch (key.ValueType)
                        {
                            case "文字":
                                if (key.HasSelection == "1")
                                {
                                    ComboBox cb = item as ComboBox;
                                    value = cb.Text.ToString();
                                }
                                else
                                {
                                    TextField tf = item as TextField;
                                    value = tf.Text.ToString();
                                }
                                break;
                            case "数字":
                                if (key.HasSelection == "1")
                                {
                                    ComboBox cb = item as ComboBox;
                                    value = cb.Text.ToString();
                                }
                                else
                                {
                                    TextField tf = item as TextField;
                                    value = tf.Text.ToString();
                                }
                                break;
                            case "日期":
                                DateField df = item as DateField;
                                value = df.Text.ToString();
                                if (value == "0001/1/1 0:00:00")
                                {
                                    value = "";
                                }
                                break;
                            default:
                                break;
                        }
                        ledgerDetail.DetailId = Convert.ToInt32(ledgerDetailManager.GetNextDetailId());
                        ledgerDetail.KeyId = key.KeyId;
                        ledgerDetail.KeyValue = value;
                        ledgerDetail.LedgerId = ledger.LedgerId;
                        try
                        {
                            ledgerDetailManager.Insert(ledgerDetail);
                        }
                        catch (Exception ex)
                        {
                            msg.Alert("操作", "保存失败：" + ex);
                            msg.Show();
                            return;
                        }
                    }
                }
            }
            #endregion

            #region 回写送检明细信息
            //2014-04-18新需求，回写所有同Barcode的送检单
            PstMaterialChk check = materialChkManager.GetById(ledger.BillNo);
            if (check != null)
            {
                //禁止修改已经入库原材料的台账
                if (check.StockInFlag != "1")
                {
                    EntityArrayList<PstMaterialChkDetail> chkDetailList = materialChkDetailManager.GetListByWhere(PstMaterialChkDetail._.Barcode == ledger.Barcode);
                    if (chkDetailList.Count > 0)
                    {
                        foreach (PstMaterialChkDetail chkDetail in chkDetailList)
                        {
                            chkDetail.ChkDate = DateTime.Now;
                            chkDetail.ChkPerson = ledger.CheckerId;
                            chkDetail.ChkResultFlag = ledger.CheckResult;
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

            this.AppendWebLog("电子台账添加", "电子台账编号：" + ledger.LedgerId);
            pageToolBar.DoRefresh();
            this.windowAddLedger.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击保存台账中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnModifySave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            #region 校验输入
            if ((txtModifySendNum.Text != "") && (txtModifySendNum.Text != null))
            {
                if (!Regex.Match(txtModifySendNum.Value.ToString(), @"^(0|[1-9]\d{0,11})(\.\d{0,3})?$").Success)
                {
                    msg.Alert("操作", "输入数字格式不正确！应为12位有效数字，保留3位小数！");
                    msg.Show();
                    return;
                }
            }
            #endregion

            #region 保存台账
            QmcLedger ledger = ledgerManager.GetById(Convert.ToInt32(txtHiddenLedgerId.Text));
            ledger.CheckerId = txtModifyCheckerId.Text.ToString();
            ledger.ExtractorId = txtModifyExtractorId.Text.ToString();
            ledger.FetcherId = txtModifyFetcherId.Text.ToString();
            ledger.HandlerId = txtModifyHandlerId.Text.ToString();
            ledger.ReceiverId = txtModifyReceiverId.Text.ToString();
            if ((txtModifySupplierId.Text != "") && (txtModifySupplierId.Text != null) && (txtModifySupplierId.Text != "0"))
            {
                ledger.SupplierId = Convert.ToInt32(txtModifySupplierId.Text);
            }
            if ((txtModifyManufacturerId.Text != "") && (txtModifyManufacturerId.Text != null) && (txtModifyManufacturerId.Text != "0"))
            {
                ledger.ManufacturerId = Convert.ToInt32(txtModifyManufacturerId.Text);
            }
            if (dtfModifyHandleDate.Text != "0001/1/1 0:00:00")
            {
                ledger.HandleDate = Convert.ToDateTime(dtfModifyHandleDate.Value);
            }
            if (dtfModifyReceiveDate.Text != "0001/1/1 0:00:00")
            {
                ledger.ReceiveDate = Convert.ToDateTime(dtfModifyReceiveDate.Value);
            }
            if (dtfModifyReturnDate.Text != "0001/1/1 0:00:00")
            {
                ledger.ReturnDate = Convert.ToDateTime(dtfModifyReturnDate.Value);
            }
            if (dtfModifySendDate.Text != "0001/1/1 0:00:00")
            {
                ledger.SendDate = Convert.ToDateTime(dtfModifySendDate.Value);
            }
            ledger.HandleMethod = cbxModifyHandleMethod.Value.ToString();
            ledger.Remark = txtModifyRemark.Text.ToString();
            try
            {
                ledgerManager.Update(ledger);
                EntityArrayList<QmcCheckData> checkDataList = checkDataManager.GetListByWhere(QmcCheckData._.CheckId == ledger.CheckId);
                if (checkDataList.Count > 0)
                    foreach (QmcCheckData cData in checkDataList)
                    {
                        EntityArrayList<QmcSampleLedger> sLedgerList = sampleLedgerManager.GetListByWhere(QmcSampleLedger._.LedgerId == cData.LedgerId);
                        if (sLedgerList.Count > 0)
                        {
                            foreach (QmcSampleLedger sLedger in sLedgerList)
                            {
                                sLedger.HandleMethod = ledger.HandleMethod;
                                sLedger.HandlerId = ledger.HandlerId;
                                sLedger.HandleDate = ledger.HandleDate;
                                sLedger.ExtractorId = ledger.ExtractorId;
                                sLedger.FetcherId = ledger.FetcherId;
                                sLedger.ManufacturerId = ledger.ManufacturerId;
                                sLedger.ReceiveDate = ledger.ReceiveDate;
                                sLedger.ReceiverId = ledger.ReceiverId;
                                sLedger.ReturnDate = ledger.ReturnDate;
                                sLedger.SendDate = ledger.SendDate;
                                sLedger.SupplierId = ledger.SupplierId;
                                sLedger.Remark = ledger.Remark;
                                sampleLedgerManager.Update(sLedger);
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                msg.Alert("操作", "保存失败：" + ex);
                msg.Show();
                return;
            }
            #endregion

            #region 保存台账自定义项目值
            int i = this.pnlModifyLedgerCustom.Items.Count;
            EntityArrayList<QmcLedgerKey> keylist = ledgerKeyManager.GetListByWhere(QmcLedgerKey._.DeleteFlag == "0");
            EntityArrayList<QmcLedgerDetail> detaillist = ledgerDetailManager.GetListByWhere(QmcLedgerDetail._.LedgerId == Convert.ToInt32(txtHiddenLedgerId.Text));
            foreach (QmcLedgerKey key in keylist)
            {
                foreach (QmcLedgerDetail detail in detaillist)
                {
                    if (key.KeyId == detail.KeyId)
                    {
                        foreach (Control item in pnlModifyLedgerCustom.Items)
                        {
                            if (item.ID == "cm" + key.KeyId)
                            {
                                switch (key.ValueType)
                                {
                                    case "文字":
                                        if (key.HasSelection == "1")
                                        {
                                            ComboBox cb = item as ComboBox;
                                            detail.KeyValue = cb.Text.ToString();
                                        }
                                        else
                                        {
                                            TextField tf = item as TextField;
                                            detail.KeyValue = tf.Text.ToString();
                                        }
                                        break;
                                    case "数字":
                                        if (key.HasSelection == "1")
                                        {
                                            ComboBox cb = item as ComboBox;
                                            detail.KeyValue = cb.Text.ToString();
                                        }
                                        else
                                        {
                                            TextField tf = item as TextField;
                                            detail.KeyValue = tf.Text.ToString();
                                        }
                                        break;
                                    case "日期":
                                        DateField df = item as DateField;
                                        detail.KeyValue = df.Text.ToString();
                                        if (detail.KeyValue == "0001/1/1 0:00:00")
                                        {
                                            detail.KeyValue = "";
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                try
                                {
                                    ledgerDetailManager.Update(detail);
                                }
                                catch (Exception ex)
                                {
                                    msg.Alert("操作", "保存失败：" + ex);
                                    msg.Show();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 回写送检明细信息
            //2014-04-18新需求，回写所有同Barcode的送检单
            PstMaterialChk check = materialChkManager.GetById(ledger.BillNo);
            if (check != null)
            {
                //禁止修改已经入库原材料的台账
                if (check.StockInFlag != "1")
                {
                    EntityArrayList<PstMaterialChkDetail> chkDetailList = materialChkDetailManager.GetListByWhere(PstMaterialChkDetail._.Barcode == ledger.Barcode);
                    if (chkDetailList.Count > 0)
                    {
                        foreach (PstMaterialChkDetail chkDetail in chkDetailList)
                        {
                            chkDetail.ChkDate = DateTime.Now;
                            chkDetail.ChkPerson = ledger.CheckerId;
                            chkDetail.ChkResultFlag = ledger.CheckResult;
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

            this.AppendWebLog("电子台账修改", "电子台账编号：" + ledger.LedgerId);
            pageToolBar.DoRefresh();
            this.windowModifyLedger.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 选中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckboxSelectionModelCenterMaster_SelectionChange(object sender, DirectEventArgs e)
    {
        string checkId = e.ExtraParams["CheckId"].ToString();
        StoreCenterDetail.RemoveAll();
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

    #endregion
}