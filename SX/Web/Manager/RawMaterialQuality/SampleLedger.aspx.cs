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

public partial class Manager_RawMaterialQuality_SampleLedger : BasePage
{
    #region 属性注入
    protected IQmcSampleLedgerManager ledgerManager = new QmcSampleLedgerManager();
    protected IQmcSpecMappingManager mappingManager = new QmcSpecMappingManager();
    protected IQmcSpecManager specManager = new QmcSpecManager();
    protected IPstMaterialChkManager materialChkManager = new PstMaterialChkManager();
    protected IPstMaterialChkDetailManager materialChkDetailManager = new PstMaterialChkDetailManager();
    protected IBasFactoryInfoManager factoryManager = new BasFactoryInfoManager();
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    protected IBasUserManager userManager = new BasUserManager();
    protected IQmcLedgerManager eLedgerManager = new QmcLedgerManager();
    protected IQmcLedgerDetailManager eLedgerDetailManager = new QmcLedgerDetailManager();
    protected IQmcCheckDataManager checkDataManager = new QmcCheckDataManager();
    protected IQmcCheckDataDetailManager checkDataDetailManager = new QmcCheckDataDetailManager();
    protected static DataTable tempDT = new DataTable();//临时存放台账列表
    protected static bool inhibitor = false;//Run once指示器
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    //2014-12-18 yuany添加
    protected IQmcFactoryNonCheckManager nonCheckManager = new QmcFactoryNonCheckManager();
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            台账录入 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            手动添加 = new SysPageAction() { ActionID = 3, ActionName = "btnManualAdd" };
            批量修改 = new SysPageAction() { ActionID = 4, ActionName = "btnBatchModify" };
            导出 = new SysPageAction() { ActionID = 5, ActionName = "btnExport" };
            生成标签 = new SysPageAction() { ActionID = 6, ActionName = "btnGenerateLabel" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 台账录入 { get; private set; } //必须为 public
        public SysPageAction 手动添加 { get; private set; } //必须为 public
        public SysPageAction 批量修改 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 生成标签 { get; private set; } //必须为 public
    }
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            //初始化保存按钮状态
            this.btnModifyKeySave.Disable(true);
            this.btnAddKeySave.Disable(true);
            InitialLedgerList();//初始化台账列表
        }
    }

    /// <summary>
    /// 初始化查询，默认昨天到今天的台账
    /// </summary>
    public void InitialLedgerList()
    {
        //每次页面加载只运行一次
        if (inhibitor)
        {
            this.txtBeginTime.Value = DateTime.Now.Date.AddDays(-1);
            this.txtEndTime.Value = DateTime.Now.Date;
            return;
        }
        //初始化查询起止日期
        this.txtBeginTime.Value = DateTime.Now.Date.AddDays(-1);
        this.txtEndTime.Value = DateTime.Now.Date;
        inhibitor = true;
        QmcSampleLedgerManager.QueryParams param = new QmcSampleLedgerManager.QueryParams();
        if (txtBeginTime.Text != "0001/1/1 0:00:00")
        {
            param.beginDate = Convert.ToDateTime(txtBeginTime.Value);
        }
        if (txtEndTime.Text != "0001/1/1 0:00:00")
        {
            param.endDate = Convert.ToDateTime(txtEndTime.Value).AddDays(1);
        }
        DataTable dt = ledgerManager.GetLedgerUnion(param);
        DataColumn dc = new DataColumn("可用规格");
        dt.Columns.Add(dc);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string materialCode = dr["MaterialCode"].ToString();
                string spec = String.Empty;
                EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhereAndOrder(QmcSpecMapping._.MaterialCode == materialCode && QmcSpecMapping._.DeleteFlag == "0", QmcSpecMapping._.MappingId.Asc);
                if (mappingList.Count > 0)
                {
                    foreach (QmcSpecMapping mapping in mappingList)
                    {
                        if (spec == String.Empty)
                        {
                            spec = mapping.SpecName;
                        }
                        else
                        {
                            spec = spec + "," + mapping.SpecName;
                        }
                    }
                }
                dr["可用规格"] = spec;
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
        QmcSampleLedgerManager.QueryParams param = new QmcSampleLedgerManager.QueryParams();
        param.barcode = txtBarcode.Text.TrimStart().TrimEnd();
        param.billNo = txtBillNo.Text.TrimStart().TrimEnd();
        if (txtBeginTime.Text != "0001/1/1 0:00:00")
        {
            param.beginDate = Convert.ToDateTime(txtBeginTime.Value);
        }
        if (txtEndTime.Text != "0001/1/1 0:00:00")
        {
            param.endDate = Convert.ToDateTime(txtEndTime.Value).AddDays(1);
        }
        DataTable dt = ledgerManager.GetLedgerUnion(param);
        tempDT = dt;
        pnlLedger.GetStore().DataSource = dt;
        pnlLedger.GetStore().DataBind();
    }

    #endregion

    #region 页面方法
    /// <summary>
    /// 载入新增台账初始信息
    /// </summary>
    [DirectMethod]
    public void LoadAddCheckDetail()
    {
        DataSet ds = new DataSet();
        ds = materialChkDetailManager.GetAddLedgerCheckDetail(trfAddBillNo.Value.ToString(), txtAddBarcode.Value.ToString(), txtAddOrderId.Value.ToString());
        DataTable data = ds.Tables[0];
        txtAddMaterialName.Text = data.Rows[0]["MaterialName"].ToString();
        trfAddSupplierName.Text = data.Rows[0]["FacName"].ToString();
        BasFactoryInfo supplier = new BasFactoryInfo();
        if (data.Rows[0]["FactoryID"] != null)
        {
            supplier = factoryManager.GetById(data.Rows[0]["FactoryID"]);
            txtAddSupplierId.Text = supplier.ObjID.ToString();
            txtAddFactoryCode.Text = supplier.ERPCode.ToString();
            txtAddSampleSource.Text = supplier.FacName.ToString().Trim();
        }
        txtAddDetailId.Text = data.Rows[0]["ObjId"].ToString();
        txtAddNoticeNo.Text = data.Rows[0]["NoticeNo"].ToString();
        txtAddBatchCode.Text = data.Rows[0]["LLBarcode"].ToString();
        txtAddSampleCode.Text = data.Rows[0]["BillNo"].ToString();
        txtAddSampleNum.Text = data.Rows[0]["SendWeight"].ToString();
        txtAddRemark.Text = data.Rows[0]["Remark"].ToString();
        txtAddManufacturerName.Value = "";
        txtAddManufacturerId.Value = "";
        //规格信息
        EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhere(QmcSpecMapping._.MaterialCode == data.Rows[0]["MaterCode"].ToString() && QmcSpecMapping._.DeleteFlag == "0");
        EntityArrayList<QmcSpec> specList = new EntityArrayList<QmcSpec>();
        if (mappingList.Count > 0)
        {
            foreach (QmcSpecMapping mapping in mappingList)
            {
                QmcSpec spec = specManager.GetById(mapping.SpecId);
                if (spec.DeleteFlag == "0")
                {
                    specList.Add(spec);
                }
            }
        }
        if (specList.Count > 0)
        {
            foreach(QmcSpec spec in specList)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = spec.SpecName;
                item.Value = spec.SpecId.ToString();
                cbxAddSpec.Items.Add(item);
            }
        }
        cbxAddSpec.ReRender();
    }

    /// <summary>
    /// 载入手动新增台账初始信息
    /// </summary>
    [DirectMethod]
    public void LoadManualAddCheckDetail()
    {
        //规格信息
        EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhere(QmcSpecMapping._.MaterialCode == txtManualAddMaterialCode.Value.ToString() && QmcSpecMapping._.DeleteFlag == "0");
        EntityArrayList<QmcSpec> specList = new EntityArrayList<QmcSpec>();
        if (mappingList.Count > 0)
        {
            foreach (QmcSpecMapping mapping in mappingList)
            {
                QmcSpec spec = specManager.GetById(mapping.SpecId);
                if (spec.DeleteFlag == "0")
                {
                    specList.Add(spec);
                }
            }
        }
        if (specList.Count > 0)
        {
            foreach (QmcSpec spec in specList)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = spec.SpecName;
                item.Value = spec.SpecId.ToString();
                cbxManualAddSpec.Items.Add(item);
            }
        }
        cbxManualAddSpec.ReRender();
    }

    /// <summary>
    /// 获取台账ID序列
    /// </summary>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string GetSampleIdSequence()
    {
        if (ledgerSelectionModel.SelectedRows.Count == 0)
        {
            return "";
        }
        else
        {
            string sequence = "";
            foreach (SelectedRow row in ledgerSelectionModel.SelectedRows)
            {
                if (sequence == "")
                {
                    sequence = row.RecordID;
                }
                else
                {
                    sequence = sequence + "," + row.RecordID;
                }
            }
            return sequence;
        }
    }

    /// <summary>
    /// 获取用户名
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
    #endregion

    #region 增删改查按钮激发的事件

    /// <summary>
    /// 添加台账中自动流水号复选框选中的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxAddIsFlow_checked(object sender, EventArgs e)
    {
        if (cbxAddIsFlow.Checked)
        {
            this.txtAddSampleCode.Disable(true);
        }
        else
        {
            this.txtAddSampleCode.Enable(true);
        }
    }

    /// <summary>
    /// 手动添加台账中自动流水号复选框选中的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxManualAddIsFlow_checked(object sender, EventArgs e)
    {
        if (cbxManualAddIsFlow.Checked)
        {
            this.txtManualAddSampleCode.Disable(true);
        }
        else
        {
            this.txtManualAddSampleCode.Enable(true);
        }
    }

    /// <summary>
    /// 修改台账中自动流水号复选框选中的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxModifyIsFlow_checked(object sender, EventArgs e)
    {
        if (cbxModifyIsFlow.Checked)
        {
            this.txtModifySampleCode.Disable(true);
        }
        else
        {
            this.txtModifySampleCode.Enable(true);
        }
    }

    /// <summary>
    /// 点击添加按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        //AutoCreateQmcCheckData("26998");

        //return;
        this.cbxAddSpec.Items.Clear();
        //初始化新增窗口
        this.txtAddSampleCode.Enable(true);
        this.cbxAddIsFlow.Checked = false;
        this.txtAddBarcode.Value = "";
        this.txtAddBatchCode.Value = "";
        this.txtAddDetailId.Value = "";
        this.txtAddExtractorId.Value = "";
        this.txtAddFactoryCode.Value = "";
        this.txtAddNoticeNo.Value = "";
        this.txtAddSampleCode.Value = "";
        this.txtAddSampleNum.Value = "";
        this.txtAddSampleSource.Value = "";
        this.trfAddSupplierName.Value = "";
        this.txtAddSupplierId.Value = "";
        this.txtAddManufacturerName.Value = "";
        this.txtAddManufacturerId.Value = "";
        this.txtAddFetcherId.Value = "";
        this.txtAddHandlerId.Value = "";
        this.txtAddMaterCode.Value = "";
        this.txtAddMaterialName.Value = "";
        this.txtAddOrderId.Value = "";
        this.txtAddReceiverId.Value = "";
        this.txtAddRemark.Value = "";
        this.trfAddBillNo.Value = "";
        this.trfAddExtractorName.Value = "";
        this.trfAddFetcherName.Value = "";
        this.trfAddHandlerName.Value = "";
        this.trfAddReceiverName.Value = "";
        this.dtfAddHandleDate.Value = "";
        this.dtfAddReceiveDate.Value = DateTime.Now.Date;
        this.dtfAddReturnDate.Value = "";
        this.dtfAddSendDate.Value = DateTime.Now.Date;
        this.txtAddUnit.Value = "";
        this.cbxAddHandleMethod.Value = "";
        this.cbxAddStatus.Value = "正常";
        this.btnAddKeySave.Disable(true);
        this.cbxAddSpec.Items.Clear();
        this.cbxAddSpec.SetValue("");
        this.cbxAddSpec.ReRender();
        this.windowAddLedger.Show();
    }

    /// <summary>
    /// 点击手动添加按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_manualAdd_Click(object sender, EventArgs e)
    {
        this.cbxManualAddSpec.Items.Clear();
        //初始化手动添加窗口
        this.txtManualAddSampleCode.Disable(true);
        this.cbxManualAddIsFlow.Checked = true;
        this.txtManualAddBarcode.Value = "";
        this.txtManualAddBatchCode.Value = "";

        this.txtManualAddExtractorId.Value = "";
        this.txtManualAddFactoryCode.Value = "";

        this.txtManualAddSampleCode.Value = "";
        this.txtManualAddSampleNum.Value = "";
        this.txtManualAddSampleSource.Value = "";
        this.trfManualAddSupplierName.Value = "";
        this.txtManualAddSupplierId.Value = "";
        this.txtManualAddManufacturerName.Value = "";
        this.txtManualAddManufacturerId.Value = "";
        this.txtManualAddFetcherId.Value = "";
        this.txtManualAddHandlerId.Value = "";
        this.txtManualAddMaterialCode.Value = "";
        this.trfManualAddMaterialName.Value = "";
        this.txtManualAddReceiverId.Value = "";
        this.txtManualAddRemark.Value = "";
        this.txtManualAddBillNo.Value = "";
        this.trfManualAddExtractorName.Value = "";
        this.trfManualAddFetcherName.Value = "";
        this.trfManualAddHandlerName.Value = "";
        this.trfManualAddReceiverName.Value = "";
        this.dtfManualAddHandleDate.Value = "";
        this.dtfManualAddReceiveDate.Value = DateTime.Now.Date;
        this.dtfManualAddReturnDate.Value = "";
        this.dtfManualAddSendDate.Value = DateTime.Now.Date;
        this.txtManualAddUnit.Value = "千克";
        this.cbxManualAddHandleMethod.Value = "";
        this.cbxManualAddStatus.Value = "正常";
        this.cbxManualAddSpec.Items.Clear();
        this.cbxManualAddSpec.SetValue("");
        this.cbxManualAddSpec.ReRender();
        this.windowManualAddLedger.Show();
    }

    /// <summary>
    /// 点击批量修改激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_batchModify_Click(object sender, EventArgs e)
    {
        if (ledgerSelectionModel.SelectedRows.Count == 0)
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
            foreach (SelectedRow row in ledgerSelectionModel.SelectedRows)
            {
                QmcSampleLedger ledger = ledgerManager.GetById(row.RecordID);
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
                    EntityArrayList<QmcCheckData> checkDataList = checkDataManager.GetListByWhere(QmcCheckData._.LedgerId == ledger.LedgerId);
                    if (checkDataList.Count > 0)
                    foreach (QmcCheckData cData in checkDataList)
                    {
                        EntityArrayList<QmcLedger> eLedgerList = eLedgerManager.GetListByWhere(QmcLedger._.CheckId == cData.CheckId);
                        if (eLedgerList.Count > 0)
                        {
                            foreach (QmcLedger eLedger in eLedgerList)
                            {
                                eLedger.HandleMethod = ledger.HandleMethod;
                                eLedger.HandlerId = ledger.HandlerId;
                                eLedger.HandleDate = ledger.HandleDate;
                                eLedger.ExtractorId = ledger.ExtractorId;
                                eLedger.FetcherId = ledger.FetcherId;
                                eLedger.ManufacturerId = ledger.ManufacturerId;
                                eLedger.ReceiveDate = ledger.ReceiveDate;
                                eLedger.ReceiverId = ledger.ReceiverId;
                                eLedger.ReturnDate = ledger.ReturnDate;
                                eLedger.SendDate = ledger.SendDate;
                                eLedger.SupplierId = ledger.SupplierId;
                                eLedger.Remark = ledger.Remark;
                                eLedgerManager.Update(eLedger);
                            }
                        }
                    }
                    batchCount++;
                }
            }
            this.AppendWebLog("样品台账批量修改", "条目数：" + batchCount);
            pageToolBar.DoRefresh();
            msg.Alert("操作", "已修改" + batchCount + "条样品台账！");
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
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < tempDT.Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = tempDT.Columns[i];
            foreach (ColumnBase cb in this.pnlLedger.ColumnModel.Columns)
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
                tempDT.Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        if (tempDT.Rows.Count > 0)
        {
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(tempDT, "样品台账报表");
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
            QmcSampleLedger ledger = ledgerManager.GetById(Convert.ToInt32(ledgerId));
            ledger.DeleteFlag = "1";
            ledgerManager.Update(ledger);
            this.AppendWebLog("样品台账删除", "台账编号：" + ledgerId);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        this.ledgerSelectionModel.SelectedRows.Clear();
        this.ledgerSelectionModel.UpdateSelection();
        return "删除成功";
    }

    /// <summary>
    /// 点击修改激发的事件
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string ledgerId)
    {
        this.cbxModifySpec.Items.Clear();
        //初始化修改窗口
        this.txtModifySampleCode.Enable(true);
        this.cbxModifyIsFlow.Checked = false;
        QmcSampleLedger ledger = ledgerManager.GetById(Convert.ToInt32(ledgerId));
        PstMaterialChk check = materialChkManager.GetById(ledger.BillNo);
        BasFactoryInfo supplier = null;
        BasFactoryInfo manufacturer = null;
        QmcSpec spec = null;
        if (ledger.SpecId != null)
        {
            spec = specManager.GetById(ledger.SpecId);
        }
        if (ledger.SupplierId != null)
        {
            supplier = factoryManager.GetById(ledger.SupplierId);
        }
        if (ledger.ManufacturerId != null)
        {
            manufacturer = factoryManager.GetById(ledger.ManufacturerId);
        }
        if (check != null)
        {
            if (check.StockInFlag == "1")
            {
                msg.Alert("操作", "此原材料已入库，不能修改！");
                msg.Show();
                return;
            }
        }
        DataSet ds = new DataSet();
        ds = materialChkDetailManager.GetAddLedgerCheckDetail(ledger.BillNo, ledger.Barcode, ledger.OrderId.ToString());
        DataTable data = ds.Tables[0];
        if (data.Rows.Count > 0)
        {
            //完整的数据
            this.txtModifyMaterialName.Text = data.Rows[0]["MaterialName"].ToString();
            if (supplier != null)
            {
                this.trfModifySupplierName.Text = supplier.FacName.Trim();
                this.txtModifySupplierId.Text = supplier.ObjID.ToString();
            }
            if (manufacturer != null)
            {
                this.txtModifyManufacturerName.Text = manufacturer.FacName.Trim();
                this.txtModifyManufacturerId.Text = manufacturer.ObjID.ToString();
            }
            if (spec != null)
            {
                this.cbxModifySpec.SelectedItem.Text = spec.SpecName;
                this.cbxModifySpec.SelectedItem.Value = spec.SpecId.ToString();
            }
            this.txtModifyMaterCode.Value = data.Rows[0]["MaterCode"].ToString();
            this.txtModifyNoticeNo.Value = data.Rows[0]["NoticeNo"].ToString();

            this.txtModifyDetailId.Value = ledger.BillDetailId;
            this.txtModifyBillNo.Value = ledger.BillNo;
            this.txtModifyBarcode.Value = ledger.Barcode;
            this.txtModifyBatchCode.Value = ledger.BatchCode;
            this.txtModifyOrderId.Value = ledger.OrderId;

            this.dtfModifyHandleDate.Value = ledger.HandleDate;
            this.dtfModifyReceiveDate.Value = ledger.ReceiveDate;
            this.dtfModifyReturnDate.Value = ledger.ReturnDate;
            this.dtfModifySendDate.Value = ledger.SendDate;

            this.txtModifyExtractorId.Value = ledger.ExtractorId;
            this.txtModifyReceiverId.Value = ledger.ReceiverId;
            this.txtModifyFetcherId.Value = ledger.FetcherId;
            this.txtModifyHandlerId.Value = ledger.HandlerId;

            this.txtModifyFactoryCode.Value = ledger.FactoryCode;
            this.txtModifySampleCode.Value = ledger.SampleCode;
            this.txtModifySampleNum.Value = ledger.SampleNum;
            this.txtModifySampleSource.Value = ledger.SampleSource;

            this.trfModifyExtractorName.Value = GetUserName(ledger.ExtractorId);
            this.trfModifyReceiverName.Value = GetUserName(ledger.ReceiverId);
            this.trfModifyFetcherName.Value = GetUserName(ledger.FetcherId);
            this.trfModifyHandlerName.Value = GetUserName(ledger.HandlerId);

            this.txtModifyUnit.Value = "千克";
            this.cbxModifyHandleMethod.Value = ledger.HandleMethod;
            this.cbxModifyStatus.Value = ledger.SampleStatus;

            this.txtModifyRemark.Value = ledger.Remark;
            //规格信息
            EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhere(QmcSpecMapping._.MaterialCode == ledger.MaterialCode && QmcSpecMapping._.DeleteFlag == "0");
            EntityArrayList<QmcSpec> specList = new EntityArrayList<QmcSpec>();
            if (mappingList.Count > 0)
            {
                foreach (QmcSpecMapping mapping in mappingList)
                {
                    QmcSpec specA = specManager.GetById(mapping.SpecId);
                    if (specA != null)
                    {
                        if (specA.DeleteFlag == "0")
                        {
                            specList.Add(specA);
                        }
                    }
                }
            }
            if (specList.Count > 0)
            {
                foreach (QmcSpec specA in specList)
                {
                    Ext.Net.ListItem item = new Ext.Net.ListItem();
                    item.Text = specA.SpecName;
                    item.Value = specA.SpecId.ToString();
                    cbxModifySpec.Items.Add(item);
                }
            }
            cbxModifySpec.ReRender();

            this.txtHiddenLedgerId.Text = ledger.LedgerId.ToString();

            this.windowModifyLedger.Show();
        }
        else
        {
            //不完整的数据
            this.txtModifyNoticeNo.Value = "";
            this.txtModifyMaterialName.Text = ledger.SampleName;
            if (supplier != null)
            {
                this.trfModifySupplierName.Text = supplier.FacName.Trim();
                this.txtModifySupplierId.Text = supplier.ObjID.ToString();
            }
            else
            {
                this.trfModifySupplierName.Value = "";
                this.txtModifySupplierId.Value = "";
            }
            if (manufacturer != null)
            {
                this.txtModifyManufacturerName.Text = manufacturer.FacName.Trim();
                this.txtModifyManufacturerId.Text = manufacturer.ObjID.ToString();
            }
            else
            {
                this.txtModifyManufacturerName.Value = "";
                this.txtModifyManufacturerId.Value = "";
            }
            if (spec != null)
            {
                this.cbxModifySpec.SelectedItem.Text = spec.SpecName;
                this.cbxModifySpec.SelectedItem.Value = spec.SpecId.ToString();
            }
            else
            {
                this.cbxModifySpec.Text = "";
                this.cbxModifySpec.Value = "";
            }
            this.txtModifyMaterCode.Value = ledger.MaterialCode;

            this.txtModifyDetailId.Value = ledger.BillDetailId;
            this.txtModifyBillNo.Value = ledger.BillNo;
            this.txtModifyBarcode.Value = ledger.Barcode;
            this.txtModifyBatchCode.Value = ledger.BatchCode;
            this.txtModifyOrderId.Value = ledger.OrderId;

            this.dtfModifyHandleDate.Value = ledger.HandleDate;
            this.dtfModifyReceiveDate.Value = ledger.ReceiveDate;
            this.dtfModifyReturnDate.Value = ledger.ReturnDate;
            this.dtfModifySendDate.Value = ledger.SendDate;

            this.txtModifyExtractorId.Value = ledger.ExtractorId;
            this.txtModifyReceiverId.Value = ledger.ReceiverId;
            this.txtModifyFetcherId.Value = ledger.FetcherId;
            this.txtModifyHandlerId.Value = ledger.HandlerId;

            this.txtModifyFactoryCode.Value = ledger.FactoryCode;
            this.txtModifySampleCode.Value = ledger.SampleCode;
            this.txtModifySampleNum.Value = ledger.SampleNum;
            this.txtModifySampleSource.Value = ledger.SampleSource;

            this.trfModifyExtractorName.Value = GetUserName(ledger.ExtractorId);
            this.trfModifyReceiverName.Value = GetUserName(ledger.ReceiverId);
            this.trfModifyFetcherName.Value = GetUserName(ledger.FetcherId);
            this.trfModifyHandlerName.Value = GetUserName(ledger.HandlerId);

            this.txtModifyUnit.Value = "千克";
            this.cbxModifyHandleMethod.Value = ledger.HandleMethod;
            this.cbxModifyStatus.Value = ledger.SampleStatus;

            this.txtModifyRemark.Value = ledger.Remark;

            //规格信息
            EntityArrayList<QmcSpecMapping> mappingList = mappingManager.GetListByWhere(QmcSpecMapping._.MaterialCode == ledger.MaterialCode && QmcSpecMapping._.DeleteFlag == "0");
            EntityArrayList<QmcSpec> specList = new EntityArrayList<QmcSpec>();
            if (mappingList.Count > 0)
            {
                foreach (QmcSpecMapping mapping in mappingList)
                {
                    QmcSpec specA = specManager.GetById(mapping.SpecId);
                    if (specA != null)
                    {
                        if (specA.DeleteFlag == "0")
                        {
                            specList.Add(specA);
                        }
                    }
                }
            }
            if (specList.Count > 0)
            {
                foreach (QmcSpec specA in specList)
                {
                    Ext.Net.ListItem item = new Ext.Net.ListItem();
                    item.Text = specA.SpecName;
                    item.Value = specA.SpecId.ToString();
                    cbxModifySpec.Items.Add(item);
                }
            }
            cbxModifySpec.ReRender();

            this.txtHiddenLedgerId.Text = ledger.LedgerId.ToString();

            this.windowModifyLedger.Show();
        }
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
        this.windowManualAddLedger.Close();
    }

    /// <summary>
    /// 点击添加台账中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
      
        string factoryCode = "";
        string materialCode = "";
        string batchCode = "";
        try
        {
            #region 校验输入
            if ((txtAddSampleNum.Text != "") && (txtAddSampleNum.Text != null))
            {
                if (!Regex.Match(txtAddSampleNum.Value.ToString(), @"^(0|[1-9]\d{0,11})(\.\d{0,3})?$").Success)
                {
                    msg.Alert("操作", "输入数字格式不正确！应为12位有效数字，保留3位小数！");
                    msg.Show();
                    return;
                }
            }
            #endregion
           
            #region 保存台账
            QmcSampleLedger ledger = new QmcSampleLedger();
            ledger.LedgerId = Convert.ToInt32(ledgerManager.GetNextLedgerId());
            ledger.BillNo = trfAddBillNo.Text.ToString();
            ledger.Barcode = txtAddBarcode.Text.ToString();
            ledger.BatchCode = txtAddBatchCode.Text.ToString();
            ledger.BillDetailId = Convert.ToInt32(txtAddDetailId.Text);
            ledger.ExtractorId = txtAddExtractorId.Text.ToString();
            ledger.FetcherId = txtAddFetcherId.Text.ToString();
            ledger.HandlerId = txtAddHandlerId.Text.ToString();
            ledger.ReceiverId = txtAddReceiverId.Text.ToString();
            ledger.OrderId = Convert.ToInt32(txtAddOrderId.Text);
            ledger.FactoryCode = txtAddFactoryCode.Text.ToString();
            ledger.MaterialCode = txtAddMaterCode.Text.ToString();
            factoryCode = ledger.FactoryCode;
            materialCode = ledger.MaterialCode;
            batchCode = ledger.BatchCode;
            if (cbxAddSpec.Value != null)
            {
                if (cbxAddSpec.Value.ToString() != "")
                {
                    ledger.SpecId = Convert.ToInt32(cbxAddSpec.Value);
                }
            }
            if ((txtAddSampleNum.Text != "") && (txtAddSampleNum.Text != null))
            {
                ledger.SampleNum = Convert.ToDecimal(txtAddSampleNum.Text);
            }
            ledger.SampleName = txtAddMaterialName.Text.ToString();
            if (cbxAddIsFlow.Checked)//自动流水选中
            {
                //获取自动生成的流水号
                ledger.SampleCode = ledgerManager.GetAutoFlowSampleCode();
            }
            else
            {
                //获取输入的样品编号
                ledger.SampleCode = txtAddSampleCode.Text.ToString();
            }
            ledger.SampleSource = txtAddSampleSource.Text.ToString();
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
            ledger.SampleStatus = cbxAddStatus.Value.ToString();
            ledger.SampleUnit = txtAddUnit.Value.ToString();
            ledger.HandleMethod = cbxAddHandleMethod.Value.ToString();
            if ((txtAddSupplierId.Text != "") && (txtAddSupplierId.Text != null))
            {
                ledger.SupplierId = Convert.ToInt32(txtAddSupplierId.Text);
            }
            if ((txtAddManufacturerId.Text != "") && (txtAddManufacturerId.Text != null))
            {
                ledger.ManufacturerId = Convert.ToInt32(txtAddManufacturerId.Text);
            }
            ledger.RecordDate = DateTime.Now;
            ledger.Remark = txtAddRemark.Text.ToString();
            //判断检测频次
            if (ledger.BatchCode.Length > 1)
            {
                string bcode = ledger.BatchCode;
                string suffix = bcode.Substring(bcode.Length - 2, 2);
                string firstPos = suffix.Substring(0, 1);
                string secondPos = suffix.Substring(1, 1);
                if (secondPos == "1")
                {
                    if ((firstPos == "0") || (firstPos == "2") || (firstPos == "4") || (firstPos == "6") || (firstPos == "8"))
                    {
                        ledger.Frequency = "CMN";
                    }
                    else
                    {
                        ledger.Frequency = "CM";
                    }
                }
                else
                {
                    ledger.Frequency = "C";
                }
            }
            ledger.DeleteFlag = "0";
            EntityArrayList<QmcSampleLedger> samList = new EntityArrayList<QmcSampleLedger>();
            try
            {
                samList = ledgerManager.GetListByWhere(QmcSampleLedger._.BillNo == ledger.BillNo && QmcSampleLedger._.DeleteFlag == '0');
                ledgerManager.Insert(ledger);
                PstMaterialChkDetail detail = materialChkDetailManager.GetEntity(ledger.BillNo, ledger.Barcode, ledger.OrderId.ToString());
                if (detail != null)
                {
                    detail.ChkResultFlag = "9";
                    materialChkDetailManager.Update(detail);
                }
            }
            catch (Exception ex)
            {
                msg.Alert("操作", "保存失败：" + ex);
                msg.Show();
                return;
            }
            #endregion
            this.AppendWebLog("样品台账添加", "台账编号：" + ledger.LedgerId);
            pageToolBar.DoRefresh();
            this.windowAddLedger.Close();
            msg.Notify("操作", "保存成功");
            msg.Show();
            #region 厂商免质检批次自动生成质检数据操作


            string sql2 = @"select * from qmcsampleledger
left join qmccheckdata on qmcsampleledger.ledgerid=qmccheckdata.ledgerid
where qmccheckdata.checkresult ='1' and qmcsampleledger.ledgerid<>'" + ledger.LedgerId + "' and qmcsampleledger.BatchCode='" + ledger.BatchCode + "'";
            DataSet dset = factoryManager.GetBySql(sql2).ToDataSet();
            if (dset.Tables[0].Rows.Count>0)
            {
                X.Msg.Confirm(
                    "提示", "有同批次质检信息是否需要自动生成质检数据?",
                    new MessageBoxButtonsConfig
                    {
                        Yes = new MessageBoxButtonConfig
                        {
                            Handler = "App.direct.AutoCreateQmcCheckData(" + ledger.LedgerId + ")",
                            Text = "是"
                        },
                        No = new MessageBoxButtonConfig
                        {
                            Text = "否"
                        }
                    }).Show();

                return;
            }




            EntityArrayList<BasFactoryInfo> facList = factoryManager.GetListByWhere(BasFactoryInfo._.ERPCode == factoryCode);
            EntityArrayList<QmcFactoryNonCheck> nonChkList = new EntityArrayList<QmcFactoryNonCheck>();
            if (facList.Count > 0)
            {
                nonChkList = nonCheckManager.GetListByWhere(
                    QmcFactoryNonCheck._.FactoryCode == facList[0].ObjID &&
                    QmcFactoryNonCheck._.MaterialCode == materialCode &&
                    QmcFactoryNonCheck._.DeleteFlag == '0'&&
                    QmcFactoryNonCheck._.NonCheckWeight == 0);
            }
            if (nonChkList.Count > 0)//存在设置
            {
              
                if (samList.Count == 0)//第一次录入样品台账
                {
                    string flowBatch = batchCode.Substring(batchCode.Length - 3, 3);
                    EntityArrayList<BasMaterial> materialList = materialManager.GetListByWhere(BasMaterial._.MaterialCode == materialCode);
                    EntityArrayList<BasFactoryInfo> factoryList = factoryManager.GetListByWhere(BasFactoryInfo._.ObjID == facList[0].ObjID);
                    try
                    {
                        int intFlowBatch = Convert.ToInt32(flowBatch);
                        bool flag = ((intFlowBatch - 1) % nonChkList[0].NonCheckNum) == 0 ? false : true;
                        if (flag)
                        {
                            X.Msg.Confirm(
                                "提示", factoryList[0].FacName + "的" + materialList[0].MaterialName + "设置了免质检批次为[" + nonChkList[0].NonCheckNum + "]，"
                                + "当前是第" + flowBatch + "批次，是否需要自动生成质检数据?",
                                new MessageBoxButtonsConfig
                            {
                                Yes = new MessageBoxButtonConfig
                                {
                                    Handler = "App.direct.AutoCreateQmcCheckData(" + ledger.LedgerId + ")",
                                    Text = "是"
                                },
                                No = new MessageBoxButtonConfig
                                {
                                    Text = "否"
                                }
                            }).Show();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }
            }
            #endregion
            #region 新厂商免质检批次自动生成质检数据操作 设定质量不为0 闫志旭 2015.3.5
            else {
                if (facList.Count > 0)
                {
                    nonChkList = nonCheckManager.GetListByWhere(
                        QmcFactoryNonCheck._.FactoryCode == facList[0].ObjID &&
                        QmcFactoryNonCheck._.MaterialCode == materialCode &&
                        QmcFactoryNonCheck._.DeleteFlag == '0'
                       );
                }
                if (nonChkList.Count == 0)
                {
                    nonChkList = nonCheckManager.GetListByWhere(
                                 QmcFactoryNonCheck._.FactoryCode == "" &&
                                 QmcFactoryNonCheck._.MaterialCode == materialCode &&
                                 QmcFactoryNonCheck._.DeleteFlag == '0'
                                );
                }
                QmcFactoryNonCheck nonChk = new QmcFactoryNonCheck();
                if (nonChkList.Count > 0)//存在设置
                {
                    nonChk = nonChkList[0];
                    if (samList.Count == 0)//第一次录入样品台账
                    {
                        if (nonChk.ErrorNum > 0)
                        {
                            msg.Alert("操作", "此物料需连续合格" + nonChk.ErrorNum+"次之后才能免检");
                            msg.Show(); return;
                        }
                        string flowBatch = batchCode.Substring(batchCode.Length - 3, 3);
                        EntityArrayList<BasMaterial> materialList = materialManager.GetListByWhere(BasMaterial._.MaterialCode == materialCode);
                        EntityArrayList<BasFactoryInfo> factoryList = factoryManager.GetListByWhere(BasFactoryInfo._.ObjID == facList[0].ObjID);
                        EntityArrayList<PstMaterialChkDetail> materialChkDetailList = materialChkDetailManager.GetListByWhere(PstMaterialChkDetail._.ObjID == ledger.BillDetailId);
                        try
                        {
                            //int intFlowBatch = Convert.ToInt32(flowBatch);
                            //bool flag = ((intFlowBatch - 1) % nonChkList[0].NonCheckNum) == 0 ? false : true;
                            //X.Js.Alert("d");
                            bool flag = ((nonChkList[0].TotalNum < nonChkList[0].NonCheckNum)||(nonChkList[0].NonCheckNum==0)) && ((nonChkList[0].TotalWeight + materialChkDetailList[0].SendWeight <= nonChkList[0].NonCheckWeight)||(nonChkList[0].NonCheckWeight==0)) ? true : false;
                            if (flag)
                            {
                                int i = int.Parse(nonChkList[0].TotalNum.ToString()) + 1;
                                X.Msg.Confirm(
                                    "提示", factoryList[0].FacName + "的" + materialList[0].MaterialName + "设置了免质检批次为[" + nonChkList[0].NonCheckNum + "]，"
                                    + "当前是第" +i + "批次，" + "设置了免质检重量为[" + nonChkList[0].NonCheckWeight + "]，"
                                    + "当前重量" + nonChkList[0].TotalWeight + "，本样品重量" + materialChkDetailList[0].SendWeight + "是否需要自动生成质检数据?",
                                    new MessageBoxButtonsConfig
                                    {
                                        Yes = new MessageBoxButtonConfig
                                        {
                                            Handler = "App.direct.AutoCreateQmcCheckData(" + ledger.LedgerId + ")",
                                            Text = "是"
                                        },
                                        No = new MessageBoxButtonConfig
                                        {
                                            Text = "否"
                                        }
                                    }).Show();
                                nonChk.TotalNum = nonChk.TotalNum + 1; nonChk.TotalWeight = nonChk.TotalWeight + materialChkDetailList[0].SendWeight;
                            }
                            else
                            { nonChk.TotalNum = 0; nonChk.TotalWeight = 0; }
                            nonCheckManager.Update(nonChk);
                        }
                        catch (Exception e2)
                        {
                            X.Js.Alert(e2.Message);

                            throw;
                        }

                    }
                }

            }
             #endregion
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 自动添加质检数据的方法
    /// </summary>
    /// <param name="ledgerId"></param>
    [DirectMethod]
    public void AutoCreateQmcCheckData(string ledgerId)
    {
        IQmcCheckDataDetailManager checkDataDetailManager = new QmcCheckDataDetailManager();
        IQmcSampleLedgerManager ledgerManager = new QmcSampleLedgerManager();
        IQmcCheckDataManager checkDataManager = new QmcCheckDataManager();
        IQmcLedgerManager eLedgerManager = new QmcLedgerManager();
        IQmcLedgerDetailManager eLedgerDetailManager = new QmcLedgerDetailManager();
        IPstMaterialChkManager materialChkManager = new PstMaterialChkManager();
        IPstMaterialChkDetailManager materialChkDetailManager = new PstMaterialChkDetailManager();
        QmcFactoryNonCheckManager NonCheckManager = new QmcFactoryNonCheckManager();
        EntityArrayList<QmcSampleLedger> currentledgerList = ledgerManager.GetListByWhere(QmcSampleLedger._.LedgerId == ledgerId);
        String UserID = "000001";
        this.AppendWebLog("样品台账免质检批次数据生成", "台账编号：" + ledgerId + ",操作人编号：" + UserID);
      
        if (currentledgerList.Count > 0)
        {

            QmcSampleLedger currentLedger = currentledgerList[0];
            string currentBatchCode = currentLedger.BatchCode;
            string currentFactoryCode = currentLedger.FactoryCode;
            string currentMaterialCode = currentLedger.MaterialCode;
            //checkDataManager.DeleteByWhere(QmcCheckData._.BillNo == currentLedger.BillNo);
            //currentFactoryCode = "100561";
            //currentMaterialCode = "1010000000001";
            EntityArrayList<QmcSampleLedger> oldLedgerList = new EntityArrayList<QmcSampleLedger>();
            //ledgerManager.GetListByWhereAndOrder(
            //QmcSampleLedger._.MaterialCode == currentMaterialCode &&
            //QmcSampleLedger._.FactoryCode == currentFactoryCode &&
            //QmcSampleLedger._.BatchCode != currentBatchCode &&
            //QmcSampleLedger._.DeleteFlag == "0",
            //QmcSampleLedger._.BatchCode.Desc);

            EntityArrayList<QmcFactoryNonCheck> NonCheckList = NonCheckManager.GetListByWhere(QmcFactoryNonCheck._.FactoryCode == currentFactoryCode && QmcFactoryNonCheck._.MaterialCode == currentMaterialCode);
            if (NonCheckList.Count > 0)
            {
                oldLedgerList = ledgerManager.GetListByWhereAndOrder(
              QmcSampleLedger._.MaterialCode == currentMaterialCode &&
              QmcSampleLedger._.FactoryCode == currentFactoryCode &&
              QmcSampleLedger._.LedgerId < currentLedger.LedgerId &&
              QmcSampleLedger._.DeleteFlag == "0",
              QmcSampleLedger._.RecordDate.Desc);
            }


            else
            {
                String sql = String.Format(@"select * from QmcSampleLedger
where MaterialCode='{0}' and FactoryCode not in 
(select FactoryCode from QmcFactoryNonCheck
where MaterialCode='{0}' and FactoryCode <>'' )
and BatchCode <> '{1}' and DeleteFlag = '0' and LedgerId < '{2}'order by RecordDate desc", currentMaterialCode, currentBatchCode, currentLedger.LedgerId);
                DataSet ds = ledgerManager.GetBySql(sql).ToDataSet();
                if (ds.Tables[0].Rows.Count > 0)
                { oldLedgerList = ledgerManager.GetListByWhere(QmcSampleLedger._.LedgerId == ds.Tables[0].Rows[0]["LedgerId"].ToString()); }


                //X.Js.Alert(sql);
            }



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
                            newDetail.MinValue = oldDetail.MinValue;
                            newDetail.MaxValue = oldDetail.MaxValue;
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

                    msg.Alert("提示", "已成功自动生成原材料质检数据及电子台账!").Show();
                }
                else
                {
                    msg.Alert("提示", "最新样品没有相关质检信息，请手动录入!").Show();
                };
            }
        }
    }
    public void AutoCreateQmcCheckData2(string ledgerId)
    {
        this.AppendWebLog("样品台账免质检批次数据生成", "台账编号：" + ledgerId + ",操作人编号：" + UserID);
        EntityArrayList<QmcSampleLedger> currentledgerList = ledgerManager.GetListByWhere(QmcSampleLedger._.LedgerId == ledgerId);
        if (currentledgerList.Count > 0)
        {
            QmcSampleLedger currentLedger = currentledgerList[0];
            string currentBatchCode = currentLedger.BatchCode;
            string currentFactoryCode = currentLedger.FactoryCode;
            string currentMaterialCode = currentLedger.MaterialCode;
            EntityArrayList<QmcSampleLedger> oldLedgerList = ledgerManager.GetListByWhereAndOrder(
                QmcSampleLedger._.MaterialCode == currentMaterialCode &&
                QmcSampleLedger._.FactoryCode == currentFactoryCode &&
                QmcSampleLedger._.BatchCode != currentBatchCode&&
                QmcSampleLedger._.DeleteFlag == "0",
                QmcSampleLedger._.BatchCode.Desc);
            if (oldLedgerList.Count > 0)
            {
                QmcSampleLedger oldLedger = oldLedgerList[0];
                EntityArrayList<QmcCheckData> oldCheckDateList = checkDataManager.GetListByWhere(QmcCheckData._.LedgerId == oldLedger.LedgerId && QmcCheckData._.DeleteFlag =="0");
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
                        QmcCheckData._.BillNo == newCheckData.BillNo&&
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
                    if(newCheckDateList.Count > 0){
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

                    msg.Alert("提示","已成功自动生成原材料质检数据及电子台账!").Show();
                }
                else { msg.Alert("提示", "最新样品没有相关质检信息，请手动录入!").Show(); };
            }
        }
    }

    /// <summary>
    /// 点击手动添加台账中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnManualAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            #region 校验输入
            if ((txtManualAddSampleNum.Text != "") && (txtManualAddSampleNum.Text != null))
            {
                if (!Regex.Match(txtManualAddSampleNum.Value.ToString(), @"^(0|[1-9]\d{0,11})(\.\d{0,3})?$").Success)
                {
                    msg.Alert("操作", "输入数字格式不正确！应为12位有效数字，保留3位小数！");
                    msg.Show();
                    return;
                }
            }
            #endregion

            #region 保存台账
            QmcSampleLedger ledger = new QmcSampleLedger();
            ledger.LedgerId = Convert.ToInt32(ledgerManager.GetNextLedgerId());
            ledger.BillNo = txtManualAddBillNo.Text.ToString();
            ledger.Barcode = txtManualAddBarcode.Text.ToString();
            ledger.BatchCode = txtManualAddBatchCode.Text.ToString();
            ledger.BillDetailId = 0;
            ledger.ExtractorId = txtManualAddExtractorId.Text.ToString();
            ledger.FetcherId = txtManualAddFetcherId.Text.ToString();
            ledger.HandlerId = txtManualAddHandlerId.Text.ToString();
            ledger.ReceiverId = txtManualAddReceiverId.Text.ToString();
            ledger.OrderId = 0;
            ledger.FactoryCode = txtManualAddFactoryCode.Text.ToString();
            ledger.MaterialCode = txtManualAddMaterialCode.Text.ToString();
            if (cbxManualAddSpec.Value != null)
            {
                if (cbxManualAddSpec.Value.ToString() != "")
                {
                    ledger.SpecId = Convert.ToInt32(cbxManualAddSpec.Value);
                }
            }
            if ((txtManualAddSampleNum.Text != "") && (txtManualAddSampleNum.Text != null))
            {
                ledger.SampleNum = Convert.ToDecimal(txtManualAddSampleNum.Text);
            }
            ledger.SampleName = trfManualAddMaterialName.Text.ToString();
            if (cbxManualAddIsFlow.Checked)//自动流水选中
            {
                //获取自动生成的流水号
                ledger.SampleCode = ledgerManager.GetAutoFlowSampleCode();
            }
            else
            {
                //获取输入的样品编号
                ledger.SampleCode = txtManualAddSampleCode.Text.ToString();
            }
            ledger.SampleSource = txtManualAddSampleSource.Text.ToString();
            if (dtfManualAddHandleDate.Text.Substring(0, 4) != "0001")
            {
                ledger.HandleDate = Convert.ToDateTime(dtfManualAddHandleDate.Value);
            }
            if (dtfManualAddReceiveDate.Text.Substring(0, 4) != "0001")
            {
                ledger.ReceiveDate = Convert.ToDateTime(dtfManualAddReceiveDate.Value);
            }
            if (dtfManualAddReturnDate.Text.Substring(0, 4) != "0001")
            {
                ledger.ReturnDate = Convert.ToDateTime(dtfManualAddReturnDate.Value);
            }
            if (dtfManualAddSendDate.Text.Substring(0, 4) != "0001")
            {
                ledger.SendDate = Convert.ToDateTime(dtfManualAddSendDate.Value);
            }
            ledger.SampleStatus = cbxManualAddStatus.Value.ToString();
            ledger.SampleUnit = txtManualAddUnit.Value.ToString();
            ledger.HandleMethod = cbxManualAddHandleMethod.Value.ToString();
            if ((txtManualAddSupplierId.Text != "") && (txtManualAddSupplierId.Text != null))
            {
                ledger.SupplierId = Convert.ToInt32(txtManualAddSupplierId.Text);
            }
            if ((txtManualAddManufacturerId.Text != "") && (txtManualAddManufacturerId.Text != null))
            {
                ledger.ManufacturerId = Convert.ToInt32(txtManualAddManufacturerId.Text);
            }
            ledger.RecordDate = DateTime.Now;
            ledger.Remark = txtManualAddRemark.Text.ToString();
            //判断检测频次
            if (ledger.BatchCode.Length > 1)
            {
                string bcode = ledger.BatchCode;
                string suffix = bcode.Substring(bcode.Length - 2, 2);
                string firstPos = suffix.Substring(0, 1);
                string secondPos = suffix.Substring(1, 1);
                if (secondPos == "1")
                {
                    if ((firstPos == "0") || (firstPos == "2") || (firstPos == "4") || (firstPos == "6") || (firstPos == "8"))
                    {
                        ledger.Frequency = "CMN";
                    }
                    else
                    {
                        ledger.Frequency = "CM";
                    }
                }
                else
                {
                    ledger.Frequency = "C";
                }
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

            this.AppendWebLog("样品台账手动添加", "台账编号：" + ledger.LedgerId);
            pageToolBar.DoRefresh();
            this.windowManualAddLedger.Close();
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
            if ((txtModifySampleNum.Text != "") && (txtModifySampleNum.Text != null))
            {
                if (!Regex.Match(txtModifySampleNum.Value.ToString(), @"^(0|[1-9]\d{0,11})(\.\d{0,3})?$").Success)
                {
                    msg.Alert("操作", "输入数字格式不正确！应为12位有效数字，保留3位小数！");
                    msg.Show();
                    return;
                }
            }
            #endregion

            #region 保存台账
            QmcSampleLedger ledger = ledgerManager.GetById(Convert.ToInt32(txtHiddenLedgerId.Text));
            ledger.ExtractorId = txtModifyExtractorId.Text.ToString();
            ledger.FetcherId = txtModifyFetcherId.Text.ToString();
            ledger.HandlerId = txtModifyHandlerId.Text.ToString();
            ledger.ReceiverId = txtModifyReceiverId.Text.ToString();
            ledger.FactoryCode = txtModifyFactoryCode.Text.ToString();
            ledger.BatchCode = txtModifyBatchCode.Text.ToString();
            if ((txtModifySampleNum.Text != "") && (txtModifySampleNum.Text != null))
            {
                ledger.SampleNum = Convert.ToDecimal(txtModifySampleNum.Text);
            }
            if (cbxModifyIsFlow.Checked)//自动流水选中
            {
                //获取自动生成的流水号
                ledger.SampleCode = ledgerManager.GetAutoFlowSampleCode();
            }
            else
            {
                //获取输入的样品编号
                ledger.SampleCode = txtModifySampleCode.Text.ToString();
            }
            ledger.SampleSource = txtModifySampleSource.Text.ToString();
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
            ledger.SampleStatus = cbxModifyStatus.Value.ToString();
            ledger.SampleUnit = txtModifyUnit.Value.ToString();
            ledger.HandleMethod = cbxModifyHandleMethod.Value.ToString();
            if (cbxModifySpec.Value != null)
            {
                if (cbxModifySpec.Value.ToString() != "")
                {
                    ledger.SpecId = Convert.ToInt32(cbxModifySpec.Value);
                }
            }
            if ((txtModifySupplierId.Text != "") && (txtModifySupplierId.Text != null))
            {
                ledger.SupplierId = Convert.ToInt32(txtModifySupplierId.Text);
            }
            if ((txtModifyManufacturerId.Text != "") && (txtModifyManufacturerId.Text != null))
            {
                ledger.ManufacturerId = Convert.ToInt32(txtModifyManufacturerId.Text);
            }
            ledger.Remark = txtModifyRemark.Text.ToString();
            try
            {
                ledgerManager.Update(ledger);
                EntityArrayList<QmcCheckData> checkDataList = checkDataManager.GetListByWhere(QmcCheckData._.LedgerId == ledger.LedgerId);
                if(checkDataList.Count > 0)
                foreach(QmcCheckData cData in checkDataList)
                {
                    EntityArrayList<QmcLedger> eLedgerList = eLedgerManager.GetListByWhere(QmcLedger._.CheckId == cData.CheckId);
                    if (eLedgerList.Count > 0)
                    {
                        foreach (QmcLedger eLedger in eLedgerList)
                        {
                            eLedger.HandleMethod = ledger.HandleMethod;
                            eLedger.HandlerId = ledger.HandlerId;
                            eLedger.HandleDate = ledger.HandleDate;
                            eLedger.ExtractorId = ledger.ExtractorId;
                            eLedger.FetcherId = ledger.FetcherId;
                            eLedger.ManufacturerId = ledger.ManufacturerId;
                            eLedger.ReceiveDate = ledger.ReceiveDate;
                            eLedger.ReceiverId = ledger.ReceiverId;
                            eLedger.ReturnDate = ledger.ReturnDate;
                            eLedger.SendDate = ledger.SendDate;
                            eLedger.SupplierId = ledger.SupplierId;
                            eLedger.Remark = ledger.Remark;
                            eLedgerManager.Update(eLedger);
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

            this.AppendWebLog("样品台账修改", "台账编号：" + ledger.LedgerId);
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
    #endregion
}