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
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text;

public partial class Manager_RawMaterialQuality_MaterialCheckReport : BasePage
{
    #region 属性注入
    protected IBasMaterialManager materialManager = new BasMaterialManager();
    protected IBasUserManager userManager = new BasUserManager();
    protected IQmcStandardManager standardManager = new QmcStandardManager();
    protected IQmcSpecManager specManager = new QmcSpecManager();
    protected IQmcLedgerManager ledgerManager = new QmcLedgerManager();
    protected IQmcLedgerKeyManager ledgerKeyManager = new QmcLedgerKeyManager();
    protected IQmcLedgerDetailManager ledgerDetailManager = new QmcLedgerDetailManager();
    protected IQmcSpecMappingManager specMappingManager = new QmcSpecMappingManager();
    protected IQmcCheckDataManager checkDataManager = new QmcCheckDataManager();
    protected IQmcCheckDataDetailManager detailManager = new QmcCheckDataDetailManager();
    protected IQmcSampleLedgerManager sampleLedgerManager = new QmcSampleLedgerManager();
    protected IPstMaterialChkManager materialChkManager = new PstMaterialChkManager();
    protected IPstMaterialChkDetailManager materialChkDetailManager = new PstMaterialChkDetailManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            批量重判级 = new SysPageAction() { ActionID = 2, ActionName = "btnBatchReCheck" };
            导出物理报告 = new SysPageAction() { ActionID = 3, ActionName = "btnExportPhysical" };
            导出化学报告 = new SysPageAction() { ActionID = 4, ActionName = "btnExportChemical" };
            生成台账 = new SysPageAction() { ActionID = 5, ActionName = "btnGenerateLedger" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 批量重判级 { get; private set; } //必须为 public
        public SysPageAction 导出物理报告 { get; private set; } //必须为 public
        public SysPageAction 导出化学报告 { get; private set; } //必须为 public
        public SysPageAction 生成台账 { get; private set; } //必须为 public
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
            InitControls();
            DateFieldNorthCheckDate.SetValue(DateTime.Today.ToString("yyyy-MM-dd"));
            DateFieldNorthCheckDate.SetRawValue(DateTime.Today.ToString("yyyy-MM-dd"));
            InitData();
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
    }

    /// <summary>
    /// 初始化查询数据
    /// </summary>
    private void InitData()
    {
        StoreCenterDetail.RemoveAll();
        StoreCenterMaster.RemoveAll();
        IQmcCheckDataQueryParams paras = new QmcCheckDataQueryParams();
        paras.RecordStat = "1";//只显示已提交的检测记录
        if (ComboBoxNorthMaterMinorType.Value != null)
        {
            if (ComboBoxNorthMaterMinorType.Value.ToString() != "")
            {
                paras.MinorTypeID = ComboBoxNorthMaterMinorType.Value.ToString();
            }
        }
        if (ComboBoxNorthMater.Value != null)
        {
            if (ComboBoxNorthMater.Value.ToString() != "")
            {
                paras.MaterCode = ComboBoxNorthMater.Value.ToString();
            }
        }
        if (DateFieldNorthCheckDate.RawText != null && DateFieldNorthCheckDate.RawText != "")
        {
            paras.BeginCheckDate = DateFieldNorthCheckDate.RawText;
            paras.EndCheckDate = DateFieldNorthCheckDate.RawText;
        }
        if (ComboBoxNorthSupplyFac.Value != null)
        {
            if (ComboBoxNorthSupplyFac.Value.ToString() != "")
            {
                paras.SupplyFacId = ComboBoxNorthSupplyFac.Value.ToString();
            }
        }
        if (ComboBoxNorthCheckResult.Value != null)
        {
            if (ComboBoxNorthCheckResult.Value.ToString() != "")
            {
                paras.CheckResult = ComboBoxNorthCheckResult.Value.ToString();
            }
        }
        if (TextFieldNorthBarcode.Text != null)
        {
            if (TextFieldNorthBarcode.Text.Trim() != "")
            {
                paras.Barcode = TextFieldNorthBarcode.Text.Trim();
            }
        }
        DataSet ds = GetReportDataSetByParams(paras);

        StoreCenterMaster.DataSource = ds;
        StoreCenterMaster.DataBind();
    }

    public DataSet GetReportDataSetByParams(IQmcCheckDataQueryParams paras)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT A.*");
        sb.AppendLine(", CASE A.CheckResult WHEN '1' THEN '合格' WHEN '0' THEN '不合格' ELSE '' END CheckResultDes");
        sb.AppendLine(", CASE ISNULL(A.RecordStat, 0) WHEN '0' THEN '未提交' WHEN '1' THEN '已提交' ELSE '' END RecordStatDes");
        sb.AppendLine(", B.MaterialName MaterName");
        sb.AppendLine(", C.FacName SupplyFacName");
        sb.AppendLine(", D.FacName ProductFacName");
        sb.AppendLine(", E.UserName RecorderName");
        sb.AppendLine(", F.UserName LastModifierName");
        sb.AppendLine(", G.SpecName");
        sb.AppendLine(", H.StandardName");
        sb.AppendLine(", I.ExtractorId, I.ReceiverId, I.FetcherId, I.HandlerId, I.ReceiveDate, I.SendDate, I.ReturnDate, I.HandleDate");
        sb.AppendLine(", J.UserName ExtractorName");
        sb.AppendLine(", K.UserName ReceiverName");
        sb.AppendLine(", L.UserName FetcherName");
        sb.AppendLine(", M.UserName HandlerName");
        sb.AppendLine("FROM QmcCheckData A");
        sb.AppendLine("LEFT JOIN BasMaterial B ON A.MaterCode = B.MaterialCode");
        sb.AppendLine("LEFT JOIN BasFactoryInfo C ON A.SupplyFac = C.ObjID");
        sb.AppendLine("LEFT JOIN BasFactoryInfo D ON A.ProductFac = D.ObjID");
        sb.AppendLine("LEFT JOIN BasUser E ON A.RecorderId = E.WorkBarcode");
        sb.AppendLine("LEFT JOIN BasUser F ON A.LastModifierId = F.WorkBarcode");
        sb.AppendLine("LEFT JOIN QmcSpec G On A.SpecId = G.SpecId");
        sb.AppendLine("LEFT JOIN QmcStandard H On A.StandardId = H.StandardId");
        sb.AppendLine("LEFT JOIN QmcSampleLedger I On A.LedgerId = I.LedgerId");
        sb.AppendLine("LEFT JOIN BasUser J ON I.ExtractorId = J.HRCode");
        sb.AppendLine("LEFT JOIN BasUser K ON I.ReceiverId = K.HRCode");
        sb.AppendLine("LEFT JOIN BasUser L ON I.FetcherId = L.HRCode");
        sb.AppendLine("LEFT JOIN BasUser M ON I.HandlerId = M.HRCode");
        sb.AppendLine("WHERE A.DeleteFlag = '0' AND A.ApproveFlag = '1'");
        if (paras.BillNo != null && paras.BillNo != "")
        {
            sb.AppendFormat("AND A.BillNo LIKE '%{0}%'", paras.BillNo);
            sb.AppendLine();
        }
        if (paras.Barcode != null && paras.Barcode != "")
        {
            sb.AppendFormat("AND A.Barcode LIKE '%{0}%'", paras.Barcode);
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
        //针对同一送检原材料，只可以根据最新的一条检测记录生成报告
        sb.AppendLine("AND A.CheckId in (select max(CheckId) from QmcCheckData where DeleteFlag = '0' AND ApproveFlag = '1' group by Barcode)");
        sb.AppendLine("ORDER BY A.RecordTime DESC");
        return checkDataManager.GetBySql(sb.ToString()).ToDataSet();
    }
    #endregion

    #region 页面方法

    /// <summary>
    /// 为选中的检测记录生成电子台账
    /// </summary>
    /// <returns></returns>
    private int GenerateLedger(string setting)
    {
        EntityArrayList<QmcLedger> addList = new EntityArrayList<QmcLedger>();
        EntityArrayList<QmcLedger> updateList = new EntityArrayList<QmcLedger>();
        EntityArrayList<QmcLedgerDetail> addDetailList = new EntityArrayList<QmcLedgerDetail>();
        EntityArrayList<QmcLedgerDetail> updateDetailList = new EntityArrayList<QmcLedgerDetail>();
        EntityArrayList<QmcLedgerKey> ledgerKeyList = ledgerKeyManager.GetListByWhere(QmcLedgerKey._.DeleteFlag == "0");
        int ledgerIdPointer = 0;
        int detailIdPointer = 0;
        foreach (SelectedRow row in CheckboxSelectionModelCenterMaster.SelectedRows)
        {
            QmcCheckData checkData = checkDataManager.GetById(row.RecordID);
            ////获取此样品台账下所有检测记录，只生成最新的一条
            ////注释掉，已在D层处理
            //EntityArrayList<QmcCheckData> thisSampleCheckDataList = checkDataManager.GetListByWhereAndOrder(((QmcCheckData._.LedgerId == checkData.LedgerId) && (QmcCheckData._.DeleteFlag == "0")), QmcCheckData._.RecordTime.Desc);
            //if (thisSampleCheckDataList.Count > 1)
            //{
            //    checkData = thisSampleCheckDataList[0];
            //}
            //else if (thisSampleCheckDataList.Count == 0)
            //{
            //    msg.Alert("错误", "无此检测记录！");
            //    msg.Show();
            //    return;
            //}
            DataSet checkDataDetail = detailManager.GetDataSetByCheckId(checkData.CheckId.ToString());
            QmcSampleLedger sampleLedger = sampleLedgerManager.GetById(checkData.LedgerId);
            BasMaterial material = materialManager.GetListByWhere(BasMaterial._.MaterialCode == checkData.MaterCode)[0];
            QmcStandard standard = standardManager.GetById(checkData.StandardId);
            EntityArrayList<QmcLedger> ledgerList = ledgerManager.GetListByWhere(QmcLedger._.CheckId == checkData.CheckId && QmcLedger._.DeleteFlag == "0");
            if (ledgerList.Count > 0)
            {
                QmcLedger ledger = ledgerList[0];
                ledger.Barcode = checkData.Barcode;
                ledger.BatchCode = checkData.BatchCode;
                ledger.BillDetailId = Convert.ToInt32(sampleLedger.BillDetailId);
                ledger.BillNo = sampleLedger.BillNo;
                ledger.OrderId = Convert.ToInt32(sampleLedger.OrderId);
                ledger.CheckerId = checkData.RecorderId;
                ledger.CheckId = checkData.CheckId;
                ledger.Frequency = checkData.Frequency;
                if (checkData.CheckResult == "0")
                {
                    ledger.CheckResult = "2";
                }
                else if (checkData.CheckResult == "1")
                {
                    ledger.CheckResult = "1";
                }
                else
                {
                    ledger.CheckResult = "2";
                }
                ledger.ExtractorId = sampleLedger.ExtractorId;
                ledger.FetcherId = sampleLedger.FetcherId;
                ledger.ReceiverId = sampleLedger.ReceiverId;
                ledger.HandlerId = sampleLedger.HandlerId;
                ledger.ManufacturerId = sampleLedger.ManufacturerId;
                ledger.SupplierId = sampleLedger.SupplierId;
                ledger.RecordDate = DateTime.Now;
                ledger.Remark = sampleLedger.Remark + "　" + txtRemark.Text;
                ledger.SendNum = sampleLedger.SampleNum;
                ledger.SendUnit = sampleLedger.SampleUnit;
                ledger.SpecId = checkData.SpecId;
                ledger.ReceiveDate = sampleLedger.ReceiveDate;
                ledger.HandleDate = sampleLedger.HandleDate;
                ledger.ReturnDate = sampleLedger.ReturnDate;
                ledger.SendDate = sampleLedger.SendDate;
                ledger.HandleMethod = sampleLedger.HandleMethod;
                ledger.DeleteFlag = "0";
                //读取生成设置
                if (setting == "generate")
                {
                    if (cbxChangeGenerateInfo.Checked)
                    {
                        if (dtfGenerateLedgerHandleDate.Text.Substring(0, 4) != "0001")
                        {
                            ledger.HandleDate = Convert.ToDateTime(dtfGenerateLedgerHandleDate.Value);
                            sampleLedger.HandleDate = Convert.ToDateTime(dtfGenerateLedgerHandleDate.Value);
                        }
                        if (dtfGenerateLedgerReturnDate.Text.Substring(0, 4) != "0001")
                        {
                            ledger.ReturnDate = Convert.ToDateTime(dtfGenerateLedgerReturnDate.Value);
                            sampleLedger.ReturnDate = Convert.ToDateTime(dtfGenerateLedgerReturnDate.Value);
                        }
                        ledger.HandleMethod = cbxGenerateLedgerHandleMethod.Text;
                        sampleLedger.HandleMethod = cbxGenerateLedgerHandleMethod.Text;
                        ledger.HandlerId = txtGenerateLedgerHandlerId.Text;
                        sampleLedger.HandlerId = txtGenerateLedgerHandlerId.Text;
                        try
                        {
                            sampleLedgerManager.Update(sampleLedger);
                        }
                        catch (Exception)
                        {
                            this.AppendWebLog("原材料质检管理-质检报告管理-生成质检报告-回写样品台账失败：样品台账编号" + sampleLedger.LedgerId);
                        }
                    }
                }
                else if (setting == "export")
                {
                    if (cbxChangeExportInfo.Checked)
                    {
                        if (dtfExportHandleDate.Text.Substring(0, 4) != "0001")
                        {
                            ledger.HandleDate = Convert.ToDateTime(dtfExportHandleDate.Value);
                            sampleLedger.HandleDate = Convert.ToDateTime(dtfExportHandleDate.Value);
                        }
                        if (dtfExportReturnDate.Text.Substring(0, 4) != "0001")
                        {
                            ledger.ReturnDate = Convert.ToDateTime(dtfExportReturnDate.Value);
                            sampleLedger.ReturnDate = Convert.ToDateTime(dtfExportReturnDate.Value);
                        }
                        ledger.HandleMethod = cbxExportHandleMethod.Text;
                        sampleLedger.HandleMethod = cbxExportHandleMethod.Text;
                        ledger.HandlerId = txtExportHandlerId.Text;
                        sampleLedger.HandlerId = txtExportHandlerId.Text;
                        try
                        {
                            sampleLedgerManager.Update(sampleLedger);
                        }
                        catch (Exception)
                        {
                            this.AppendWebLog("原材料质检管理-质检报告管理-生成质检报告-回写样品台账失败：样品台账编号" + sampleLedger.LedgerId);
                        }
                    }
                }
                updateList.Add(ledger);
            }
            else
            {
                QmcLedger ledger = new QmcLedger();
                ledger.LedgerId = Convert.ToInt32(ledgerManager.GetNextLedgerId()) + ledgerIdPointer;
                ledgerIdPointer++;
                ledger.Barcode = checkData.Barcode;
                ledger.BatchCode = checkData.BatchCode;
                ledger.BillDetailId = Convert.ToInt32(sampleLedger.BillDetailId);
                ledger.BillNo = sampleLedger.BillNo;
                ledger.OrderId = Convert.ToInt32(sampleLedger.OrderId);
                ledger.CheckerId = checkData.RecorderId;
                ledger.CheckId = checkData.CheckId;
                ledger.Frequency = checkData.Frequency;
                if (checkData.CheckResult == "0")
                {
                    ledger.CheckResult = "2";
                }
                else if (checkData.CheckResult == "1")
                {
                    ledger.CheckResult = "1";
                }
                else
                {
                    ledger.CheckResult = "2";
                }
                ledger.ExtractorId = sampleLedger.ExtractorId;
                ledger.FetcherId = sampleLedger.FetcherId;
                ledger.ReceiverId = sampleLedger.ReceiverId;
                ledger.HandlerId = sampleLedger.HandlerId;
                ledger.ManufacturerId = sampleLedger.ManufacturerId;
                ledger.SupplierId = sampleLedger.SupplierId;
                ledger.RecordDate = DateTime.Now;
                ledger.Remark = sampleLedger.Remark + "　" + txtRemark.Text;
                ledger.SendNum = sampleLedger.SampleNum;
                ledger.SendUnit = sampleLedger.SampleUnit;
                ledger.SpecId = checkData.SpecId;
                ledger.ReceiveDate = sampleLedger.ReceiveDate;
                ledger.HandleDate = sampleLedger.HandleDate;
                ledger.ReturnDate = sampleLedger.ReturnDate;
                ledger.SendDate = sampleLedger.SendDate;
                ledger.HandleMethod = sampleLedger.HandleMethod;
                ledger.DeleteFlag = "0";
                //读取生成设置
                if (setting == "generate")
                {
                    if (cbxChangeGenerateInfo.Checked)
                    {
                        if (dtfGenerateLedgerHandleDate.Text.Substring(0, 4) != "0001")
                        {
                            ledger.HandleDate = Convert.ToDateTime(dtfGenerateLedgerHandleDate.Value);
                            sampleLedger.HandleDate = Convert.ToDateTime(dtfGenerateLedgerHandleDate.Value);
                        }
                        if (dtfGenerateLedgerReturnDate.Text.Substring(0, 4) != "0001")
                        {
                            ledger.ReturnDate = Convert.ToDateTime(dtfGenerateLedgerReturnDate.Value);
                            sampleLedger.ReturnDate = Convert.ToDateTime(dtfGenerateLedgerReturnDate.Value);
                        }
                        ledger.HandleMethod = cbxGenerateLedgerHandleMethod.Text;
                        sampleLedger.HandleMethod = cbxGenerateLedgerHandleMethod.Text;
                        ledger.HandlerId = txtGenerateLedgerHandlerId.Text;
                        sampleLedger.HandlerId = txtGenerateLedgerHandlerId.Text;
                        try
                        {
                            sampleLedgerManager.Update(sampleLedger);
                        }
                        catch (Exception)
                        {
                            this.AppendWebLog("原材料质检管理-质检报告管理-生成质检报告-回写样品台账失败：样品台账编号" + sampleLedger.LedgerId);
                        }
                    }
                }
                else if (setting == "export")
                {
                    if (cbxChangeExportInfo.Checked)
                    {
                        if (dtfExportHandleDate.Text.Substring(0, 4) != "0001")
                        {
                            ledger.HandleDate = Convert.ToDateTime(dtfExportHandleDate.Value);
                            sampleLedger.HandleDate = Convert.ToDateTime(dtfExportHandleDate.Value);
                        }
                        if (dtfExportReturnDate.Text.Substring(0, 4) != "0001")
                        {
                            ledger.ReturnDate = Convert.ToDateTime(dtfExportReturnDate.Value);
                            sampleLedger.ReturnDate = Convert.ToDateTime(dtfExportReturnDate.Value);
                        }
                        ledger.HandleMethod = cbxExportHandleMethod.Text;
                        sampleLedger.HandleMethod = cbxExportHandleMethod.Text;
                        ledger.HandlerId = txtExportHandlerId.Text;
                        sampleLedger.HandlerId = txtExportHandlerId.Text;
                        try
                        {
                            sampleLedgerManager.Update(sampleLedger);
                        }
                        catch (Exception)
                        {
                            this.AppendWebLog("原材料质检管理-质检报告管理-生成质检报告-回写样品台账失败：样品台账编号" + sampleLedger.LedgerId);
                        }
                    }
                }
                addList.Add(ledger);
                foreach (QmcLedgerKey key in ledgerKeyList)
                {
                    EntityArrayList<QmcLedgerDetail> originList = ledgerDetailManager.GetListByWhere((QmcLedgerDetail._.LedgerId == ledger.LedgerId) && (QmcLedgerDetail._.KeyId == key.KeyId));
                    QmcLedgerDetail dummyDetail = null;
                    if (originList.Count == 0)
                    {
                        dummyDetail = new QmcLedgerDetail();
                        dummyDetail.DetailId = Convert.ToInt32(ledgerDetailManager.GetNextDetailId()) + detailIdPointer;
                        detailIdPointer++;
                        dummyDetail.LedgerId = ledger.LedgerId;
                        dummyDetail.KeyId = key.KeyId;
                        dummyDetail.KeyValue = null;
                        addDetailList.Add(dummyDetail);
                    }
                }
            }
        }
        int addCount = 0;
        int updateCount = 0;
        int failedCount = 0;
        if (addList.Count > 0)
        {
            //插入新建的台账
            foreach (QmcLedger ledger in addList)
            {
                try
                {
                    ledgerManager.Insert(ledger);
                }
                catch (Exception)
                {
                    failedCount++;
                    continue;
                }
                addCount++;
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
            }
        }
        if (updateList.Count > 0)
        {
            //更新原有的台账
            foreach (QmcLedger ledger in updateList)
            {
                try
                {
                    ledgerManager.Update(ledger);
                }
                catch (Exception)
                {
                    failedCount++;
                    continue;
                }
                updateCount++;
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
            }
        }
        if (addDetailList.Count > 0)
        {
            //新建空的占位台账自定义项目
            foreach (QmcLedgerDetail detail in addDetailList)
            {
                try
                {
                    ledgerDetailManager.Insert(detail);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        if ((addCount + updateCount) > 0)
        {
            this.AppendWebLog("电子台账生成", "条数：" + (addCount + updateCount));
        }
        else
        {
            this.AppendWebLog("电子台账生成失败", "条数：" + failedCount);
        }
        return addCount + updateCount;
    }

    #endregion

    #region 增删改按钮激发的事件
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        StoreCenterDetail.RemoveAll();
        StoreCenterMaster.RemoveAll();
        IQmcCheckDataQueryParams paras = new QmcCheckDataQueryParams();
        paras.RecordStat = "1";//只显示已提交的检测记录
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
        DataSet ds = checkDataManager.GetReportDataSetByParams(paras);
        StoreCenterMaster.DataSource = ds;
        StoreCenterMaster.DataBind();
    }

    /// <summary>
    /// 导出设置窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, DirectEventArgs e)
    {
        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            msg.Alert("操作", "请至少选择一条检测记录！");
            msg.Show();
            return;
        }
        string reportType = e.ExtraParams["reportType"];
        switch (reportType)
        {
            case "physical"://物理室报告
                windowExportSetting.Title = "导出物理报告设置";
                txtHiddenReportType.Text = "physical";
                break;
            case "chemical"://化学室报告
                windowExportSetting.Title = "导出化学报告设置";
                txtHiddenReportType.Text = "chemical";
                break;
            default:
                X.Msg.Alert("提示", "没有指定或不正确的报告类型！").Show();
                return;
        }
        this.dtfExportHandleDate.Value = DateTime.Now.Date;
        this.dtfExportReturnDate.Value = DateTime.Now.Date;
        this.dtfReportDate.Value = DateTime.Now.Date;
        this.txtExportHandlerId.Value = "";
        this.trfExportHandlerName.Value = "";
        this.cbxExportHandleMethod.Value = "";
        this.cbxChangeExportInfo.Checked = false;
        this.dtfExportHandleDate.Text = "保留原数据";
        this.dtfExportReturnDate.Text = "保留原数据";
        this.txtExportHandlerId.Value = "";
        this.trfExportHandlerName.Text = "保留原数据";
        this.cbxExportHandleMethod.Text = "保留原数据";
        this.dtfExportHandleDate.Disable(true);
        this.dtfExportReturnDate.Disable(true);
        this.trfExportHandlerName.Disable(true);
        this.cbxExportHandleMethod.Disable(true);
        this.windowExportSetting.Hidden = false;
        this.windowExportSetting.Show();
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnExportSave_Click(object sender, DirectEventArgs e)
    {
        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count > 0)
        {
            string reportDate = "";
            try
            {
                reportDate = Convert.ToDateTime(dtfReportDate.Value).ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                reportDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            }
            #region 获取报告类型
            string reportType = txtHiddenReportType.Value.ToString();
            string xlsPath = "";
            switch (reportType)
            {
                case "physical"://物理室报告
                    xlsPath = "MaterialPhysicalCheckReport.xls";
                    break;
                case "chemical"://化学室报告
                    xlsPath = "MaterialChemicalCheckReport.xls";
                    break;
                default:
                    X.Msg.Alert("提示", "没有指定或不正确的报告类型！").Show();
                    return;
            }
            #endregion

            #region 加载报告模板
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
            #endregion
            int counter = 1;
            foreach (SelectedRow row in CheckboxSelectionModelCenterMaster.SelectedRows)
            {
                #region 获取报告需要的数据
                QmcCheckData checkData = checkDataManager.GetById(row.RecordID);
                DataSet checkDataDetail = GetDataSetByCheckId(row.RecordID);
                QmcSampleLedger sampleLedger = sampleLedgerManager.GetById(checkData.LedgerId);
                EntityArrayList<BasMaterial> tempListMaterial = materialManager.GetListByWhere(BasMaterial._.MaterialCode == checkData.MaterCode);
                BasMaterial material = new BasMaterial();
                if (tempListMaterial.Count > 0)
                {
                    material = tempListMaterial[0];
                }
                else
                {
                    X.Msg.Alert("提示", "导出出错，物料代码" + checkData.MaterCode + "不存在！").Show();
                    return;
                }
                EntityArrayList<BasUser> tempListExtractor = userManager.GetListByWhere(BasUser._.HRCode == sampleLedger.ExtractorId);
                BasUser extractor = new BasUser();
                if (tempListExtractor.Count > 0)
                {
                    extractor = tempListExtractor[0];
                }
                EntityArrayList<BasUser> tempListApprover = userManager.GetListByWhere(BasUser._.WorkBarcode == checkData.ApproverId);
                BasUser approver = new BasUser();
                if (tempListApprover.Count > 0)
                {
                    approver = tempListApprover[0];
                }
                QmcStandard standard = standardManager.GetById(checkData.StandardId);
                string former = trfFormerName.Text;
                string confirmer = trfConfirmerName.Text;
                string approverName = "";
                if (approver != null)
                {
                    approverName = approver.UserName;
                }
                if (approverName == null)
                {
                    approverName = "";
                }
                string remark = sampleLedger.Remark + "　" + txtRemark.Text;
                #endregion

                #region 写入数据
                HSSFSheet modelSheet = (HSSFSheet)workbook.GetSheetAt(0);
                ISheet sheet = workbook.CloneSheet(0);//克隆模板sheet页
                workbook.SetSheetName(counter, counter + "_" + material.MaterialName.Replace("/", "∕").Replace("*", "x"));
                bool primeFlag = true;
                switch (reportType)
                {
                    #region 物理实验室报告
                    case "physical"://物理室报告
                        //报告编号
                        sheet.GetRow(2).GetCell(5).SetCellValue(sampleLedger.SampleCode);
                        //sheet.GetRow(2).GetCell(10).SetCellValue(checkData.Remark);
                        //样品名称
                        sheet.GetRow(3).GetCell(6).SetCellValue(material.MaterialName);
                        //样品来源
                        sheet.GetRow(3).GetCell(18).SetCellValue(sampleLedger.SampleSource);
                        //送检编号
                        sheet.GetRow(4).GetCell(6).SetCellValue(sampleLedger.BatchCode);
                        sheet.GetRow(4).GetCell(6).SetCellValue(checkData.Remark);

                      
                        //进货数量
                        sheet.GetRow(4).GetCell(18).SetCellValue(sampleLedger.SampleNum.ToString() + sampleLedger.SampleUnit);
                        //执行标准
                        sheet.GetRow(5).GetCell(6).SetCellValue(standard.StandardName);
                        //取样人
                        sheet.GetRow(5).GetCell(18).SetCellValue(extractor.UserName);
                        //报告日期
                        sheet.GetRow(28).GetCell(20).SetCellValue(reportDate);
                        //备注
                        sheet.GetRow(27).GetCell(2).SetCellValue(remark);
                        //批准人
                        sheet.GetRow(29).GetCell(13).SetCellValue(confirmer);
                        //制表人
                        sheet.GetRow(29).GetCell(20).SetCellValue(former);
                        //sheet.GetRow(32).GetCell(3).SetCellValue(approverName);
                        //动态项目
                        int physicalItemLimit = 18;
                        int physicalItemIndex = 0;
                        int physicalItemStart = 9;

                        //if (checkDataDetail.Tables[0].Rows.Count > physicalItemLimit)
                        //{
                        //    X.Msg.Alert("提示", "检测项目超过报告上限，请联系管理员更换报告模板！").Show();
                        //    return;
                        //}
                        if (checkDataDetail.Tables[0].Rows.Count == 0)
                        {
                            X.Msg.Alert("提示", "无检测数据，请检查送检单" + checkData.BillNo + "的检测数据！").Show();
                            return;
                        }
                        foreach (DataRow dr in checkDataDetail.Tables[0].Rows)
                        {
                            string result = String.Empty;
                            if (dr["AutoCheckResult"].ToString() == "0")
                            {
                                result = "不合格";
                                primeFlag = false;
                            }
                            else if (dr["AutoCheckResult"].ToString() == "1")
                            {
                                result = "合格品";
                                if (dr["IsPrime"].ToString() == "1")
                                {
                                    result = "一级品";
                                }
                                else
                                {
                                    primeFlag = false;
                                    if (dr["IsPrime"].ToString() == "0")
                                    {
                                        result = "合格(非一级品)";
                                    }
                                    else
                                    {
                                        result = "合格";
                                    }
                                }
                            }
                            #region 校验检测频次
                            string frequency = checkData.Frequency;
                            string checkValue = dr["CheckValue"].ToString();
                            string itemFrequency = dr["Frequency"].ToString();
                            //if (frequency.Contains(itemFrequency))
                            //{
                            //    if ((checkValue == "") || (checkValue == null))
                            //    {
                            //        X.Msg.Alert("提示", "有检测数据不完整，请检查送检单" + checkData.BillNo + "的检测数据！").Show();
                            //        return;
                            //    }
                            //}

                       
                            if (String.IsNullOrEmpty(checkValue) && String.IsNullOrEmpty(dr["MinValue"].ToString()) && String.IsNullOrEmpty(dr["MaxValue"].ToString()))
                            {
                                continue;
                            }

                            if (physicalItemIndex ==18)
                            {
                                X.Msg.Alert("提示", "检测项目超过报告上限，请联系管理员更换报告模板！").Show();
                                return;
                            }
                            #endregion
                    
                            double min = 0;
                            double max = 0;
                            if (dr["GoodMinValue"] != null && dr["GoodMinValue"].ToString()!="")
                            {
                                min = Convert.ToDouble(dr["GoodMinValue"].ToString());
                            }
                            if (dr["GoodMaxValue"] != null && dr["GoodMaxValue"].ToString() != "")
                            {
                                max = Convert.ToDouble(dr["GoodMaxValue"].ToString());
                            }
                            sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(0).SetCellValue(dr["OrderId"].ToString());
                            sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(1).SetCellValue(dr["TeXing"].ToString());
                            sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(2).SetCellValue(dr["ItemName"].ToString() + " " + dr["ItemCode"].ToString());
                            sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(8).SetCellValue(dr["CheckMethod"].ToString());
                           
                         


                            
                            if(dr["GoodDisplayValue"].ToString().Length>0)
                            {
                                String f = dr["GoodDisplayValue"].ToString().Substring(0, 1);
                                if(f==">"||f=="≥")
                                {
                                    sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(10).SetCellValue(dr["GoodMaxValue"] == null ? "" : max.ToString());
                                    sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(12).SetCellValue("-");
                                    sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(14).SetCellValue("-");
                      }
                            }


                            if (String.IsNullOrEmpty(dr["MinDisplayValue"].ToString()))
                            {
                                sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(10).SetCellValue("-");
                            }
                            else
                            {
                                sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(10).SetCellValue(dr["MinDisplayValue"].ToString().Replace(">", "").Replace("≥", "").Replace("<", "").Replace("≤", ""));
                            }
                            sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(12).SetCellValue(dr["GoodDisplayValue"].ToString());


                            if (String.IsNullOrEmpty(dr["GoodDisplayValue"].ToString()))
                            {
                                sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(12).SetCellValue("-");
                            }

                            if (String.IsNullOrEmpty(dr["MaxDisplayValue"].ToString()))
                            {
                                sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(14).SetCellValue("-");
                            }
                            else
                            {
                                sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(14).SetCellValue(dr["MaxDisplayValue"].ToString().Replace(">", "").Replace("≥", "").Replace("<", "").Replace("≤", ""));
                            }


                            sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(16).SetCellValue(dr["MinValue"].ToString());
                            if (String.IsNullOrEmpty(dr["MinValue"].ToString()))
                            {
                                sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(16).SetCellValue("-");
                            }
                            sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(18).SetCellValue(dr["CheckValue"].ToString());

                            if (String.IsNullOrEmpty(dr["CheckValue"].ToString()))
                            {
                                sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(18).SetCellValue("-");
                            }
                            
                            sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(20).SetCellValue(dr["MaxValue"].ToString());
                            if (String.IsNullOrEmpty(dr["MaxValue"].ToString()))
                            {
                                sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(20).SetCellValue("-");
                            } sheet.GetRow(physicalItemStart + physicalItemIndex).GetCell(22).SetCellValue(result);

                            physicalItemIndex++;
                        }
                        sheet.GetRow(28).GetCell(4).SetCellValue((checkData.CheckResult == "1") ? "经检验符合标准" : "不合格");
                        if (physicalItemIndex > 0)
                        {
                            if (primeFlag)
                            {
                                sheet.GetRow(28).GetCell(6).SetCellValue("一级品");
                            }
                        }
                        break;
                    #endregion
                    #region 化学实验室报告
                    case "chemical"://化学室报告
                        sheet.GetRow(2).GetCell(5).SetCellValue(sampleLedger.SampleCode);
                        sheet.GetRow(3).GetCell(3).SetCellValue(material.MaterialName);
                        sheet.GetRow(3).GetCell(9).SetCellValue(sampleLedger.SampleSource);
                        sheet.GetRow(3).GetCell(15).SetCellValue(sampleLedger.BatchCode);
                        sheet.GetRow(3).GetCell(21).SetCellValue(sampleLedger.SampleNum.ToString() + sampleLedger.SampleUnit);
                        sheet.GetRow(4).GetCell(3).SetCellValue(standard.StandardName);
                        sheet.GetRow(4).GetCell(9).SetCellValue(extractor.UserName);
                        sheet.GetRow(4).GetCell(15).SetCellValue(reportDate);
                        sheet.GetRow(26).GetCell(3).SetCellValue(remark);
                        sheet.GetRow(30).GetCell(11).SetCellValue(confirmer);
                        sheet.GetRow(30).GetCell(19).SetCellValue(former);
                        //动态项目
                        int chemicalItemLimit = 14;
                        int chemicalItemIndex = 0;
                        int chemicalItemStart = 3;
                        if (checkDataDetail.Tables[0].Rows.Count > chemicalItemLimit)
                        {
                            X.Msg.Alert("提示", "检测项目超过报告上限，请联系管理员更换报告模板！").Show();
                            return;
                        }
                        if (checkDataDetail.Tables[0].Rows.Count == 0)
                        {
                            X.Msg.Alert("提示", "无检测数据，请检查送检单" + checkData.BillNo + "的检测数据！").Show();
                            return;
                        }
                        foreach (DataRow dr in checkDataDetail.Tables[0].Rows)
                        {
                            string result = String.Empty;
                            if (dr["AutoCheckResult"].ToString() == "0")
                            {
                                result = "不合格";
                                primeFlag = false;
                            }
                            else if (dr["AutoCheckResult"].ToString() == "1")
                            {
                                result = "合格品";
                                if (dr["IsPrime"].ToString() == "1")
                                {
                                    result = "一级品";
                                }
                                else
                                {
                                    primeFlag = false;
                                    if (dr["IsPrime"].ToString() == "0")
                                    {
                                        result = "合格(非一级品)";
                                    }
                                    else
                                    {
                                        result = "合格";
                                    }
                                }
                            }
                            #region 校验检测频次
                            string frequency = checkData.Frequency;
                            string checkValue = dr["CheckValue"].ToString();
                            string itemFrequency = dr["Frequency"].ToString();
                            //if (frequency.Contains(itemFrequency))
                            //{
                            //    if ((checkValue == "") || (checkValue == null))
                            //    {
                            //        X.Msg.Alert("提示", "有检测数据不完整，请检查送检单" + checkData.BillNo + "的检测数据！").Show();
                            //        return;
                            //    }
                            //}
                            if ((checkValue == "") || (checkValue == null))
                            {
                                continue;
                            }
                            #endregion
                            //填充检测数据
                            if (chemicalItemIndex < chemicalItemLimit / 2)
                            {
                                sheet.GetRow(6).GetCell(chemicalItemStart + chemicalItemIndex * 3).SetCellValue(dr["ItemName"].ToString());
                                sheet.GetRow(8).GetCell(chemicalItemStart + chemicalItemIndex * 3).SetCellValue(dr["CheckMethod"].ToString());
                                sheet.GetRow(10).GetCell(chemicalItemStart + chemicalItemIndex * 3).SetCellValue(dr["GoodDisplayValue"].ToString());
                                sheet.GetRow(12).GetCell(chemicalItemStart + chemicalItemIndex * 3).SetCellValue(dr["CheckValue"].ToString());
                                sheet.GetRow(14).GetCell(chemicalItemStart + chemicalItemIndex * 3).SetCellValue(result);
                            }
                            else
                            {
                                sheet.GetRow(16).GetCell(chemicalItemStart + (chemicalItemIndex - chemicalItemLimit / 2) * 3).SetCellValue(dr["ItemName"].ToString());
                                sheet.GetRow(18).GetCell(chemicalItemStart + (chemicalItemIndex - chemicalItemLimit / 2) * 3).SetCellValue(dr["CheckMethod"].ToString());
                                sheet.GetRow(20).GetCell(chemicalItemStart + (chemicalItemIndex - chemicalItemLimit / 2) * 3).SetCellValue(dr["GoodDisplayValue"].ToString());
                                sheet.GetRow(22).GetCell(chemicalItemStart + (chemicalItemIndex - chemicalItemLimit / 2) * 3).SetCellValue(dr["CheckValue"].ToString());
                                sheet.GetRow(24).GetCell(chemicalItemStart + (chemicalItemIndex - chemicalItemLimit / 2) * 3).SetCellValue(result);
                            }
                            chemicalItemIndex++;
                        }
                        sheet.GetRow(4).GetCell(21).SetCellValue((checkData.CheckResult == "1") ? "合格" : "不合格");
                        if (chemicalItemIndex > 0)
                        {
                            if (primeFlag)
                            {
                                sheet.GetRow(4).GetCell(21).SetCellValue("一级品");
                            }
                        }
                        break;
                    #endregion
                    default:
                        X.Msg.Alert("提示", "没有指定或不正确的报告类型！").Show();
                        return;
                }
                counter++;
                #endregion
            }
            #region 生成电子台账
            if (cbxGenerateLedger.Checked)
            {
                GenerateLedger("export");
            }
            #endregion

            #region 生成报告下载
            workbook.RemoveSheetAt(0);
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            string fileName = "原材料质检报告";
            new Mesnac.Util.Excel.ExcelDownload().FileDown(ms, fileName);
            #endregion
        }
        else
        {
            msg.Alert("错误", "没有选择要导出的检测记录！");
            msg.Show();
        }
    }

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
    public DataSet GetDataSetByCheckId(string checkId)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT A.*");
        sb.AppendLine(", B.ItemId, B.CheckMethod, B.Remark, B.Frequency,B.OrderID,B.TeXing");
        sb.AppendLine(", B.GoodMaxValue, B.GoodMinValue, B.GoodOperator, B.GoodTextValue");
        sb.AppendLine(", B.PrimeMaxValue, B.PrimeMinValue, B.PrimeOperator, B.PrimeTextValue");
        sb.AppendLine(", B.GoodDisplayValue,  B.MinDisplayValue,  B.MaxDisplayValue,  B.MinMaxValue,  B.MaxMaxValue, B.PrimeDisplayValue, B.InputGoodMaxValue, B.InputGoodMinValue, B.InputPrimeMaxValue, B.InputPrimeMinValue");
        sb.AppendLine(", C.ItemName, C.ItemCode, C.ValueType");
        sb.AppendLine(", D.StandardName, D.ActivateDate");
        sb.AppendLine("FROM QmcCheckDataDetail A");
        sb.AppendLine("LEFT JOIN QmcCheckItemDetail B ON A.ItemDetailId = B.ItemDetailId");
        sb.AppendLine("LEFT JOIN QmcCheckItem C ON B.ItemId = C.ItemId");
        sb.AppendLine("Left Join QmcStandard D On C.StandardId = D.StandardId");
        sb.AppendFormat("WHERE A.CheckId = {0}", checkId);
        sb.AppendLine();
        sb.AppendLine("ORDER BY B.OrderID, C.ItemId");
        sb.AppendLine();

        return detailManager.GetBySql(sb.ToString()).ToDataSet();

    }
    /// <summary>
    /// 生成台账复选框选中的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void cbxGenerateLedger_checked(object sender, EventArgs e)
    {
        if (cbxGenerateLedger.Checked)
        {
            this.cbxChangeExportInfo.Enable(true);
            if (cbxChangeExportInfo.Checked)
            {
                this.txtExportHandlerId.Enable(true);
                this.cbxExportHandleMethod.Enable(true);
                this.trfExportHandlerName.Enable(true);
                this.dtfExportHandleDate.Enable(true);
                this.dtfExportReturnDate.Enable(true);
            }
        }
        else
        {
            this.cbxChangeExportInfo.Disable(true);
            this.txtExportHandlerId.Disable(true);
            this.cbxExportHandleMethod.Disable(true);
            this.trfExportHandlerName.Disable(true);
            this.dtfExportHandleDate.Disable(true);
            this.dtfExportReturnDate.Disable(true);
        }
    }

    /// <summary>
    /// 修改信息复选框选中的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxChangeGenerateInfo_checked(object sender, EventArgs e)
    {
        if (cbxChangeGenerateInfo.Checked)
        {
            this.txtGenerateLedgerHandlerId.Enable(true);
            this.txtGenerateLedgerHandlerId.Text = "";
            this.trfGenerateLedgerHandlerName.Enable(true);
            this.trfGenerateLedgerHandlerName.Text = "";
            this.cbxGenerateLedgerHandleMethod.Enable(true);
            this.cbxGenerateLedgerHandleMethod.Text = this.cbxGenerateLedgerHandleMethod.Items[0].Text;
            this.dtfGenerateLedgerHandleDate.Enable(true);
            this.dtfGenerateLedgerHandleDate.Value = DateTime.Now;
            this.dtfGenerateLedgerReturnDate.Enable(true);
            this.dtfGenerateLedgerReturnDate.Value = DateTime.Now;

        }
        else
        {
            this.txtGenerateLedgerHandlerId.Disable(true);
            this.txtGenerateLedgerHandlerId.Text = "";
            this.txtGenerateLedgerHandlerId.Value = null;
            this.cbxGenerateLedgerHandleMethod.Disable(true);
            this.cbxGenerateLedgerHandleMethod.Text = "保留原数据";
            this.trfGenerateLedgerHandlerName.Disable(true);
            this.trfGenerateLedgerHandlerName.Text = "保留原数据";
            this.dtfGenerateLedgerHandleDate.Disable(true);
            this.dtfGenerateLedgerHandleDate.Text = "保留原数据";
            this.dtfGenerateLedgerReturnDate.Disable(true);
            this.dtfGenerateLedgerReturnDate.Text = "保留原数据";
        }
    }

    /// <summary>
    /// 修改信息复选框选中的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbxChangeExportInfo_checked(object sender, EventArgs e)
    {
        if (cbxChangeExportInfo.Checked)
        {
            this.txtExportHandlerId.Enable(true);
            this.txtExportHandlerId.Text = "";
            this.trfExportHandlerName.Enable(true);
            this.trfExportHandlerName.Text = "";
            this.cbxExportHandleMethod.Enable(true);
            this.cbxExportHandleMethod.Text = this.cbxExportHandleMethod.Items[0].Text;
            this.dtfExportHandleDate.Enable(true);
            this.dtfExportHandleDate.Value = DateTime.Now;
            this.dtfExportReturnDate.Enable(true);
            this.dtfExportReturnDate.Value = DateTime.Now;

        }
        else
        {
            this.txtExportHandlerId.Disable(true);
            this.txtExportHandlerId.Text = "";
            this.txtExportHandlerId.Value = null;
            this.cbxExportHandleMethod.Disable(true);
            this.cbxExportHandleMethod.Text = "保留原数据";
            this.trfExportHandlerName.Disable(true);
            this.trfExportHandlerName.Text = "保留原数据";
            this.dtfExportHandleDate.Disable(true);
            this.dtfExportHandleDate.Text = "保留原数据";
            this.dtfExportReturnDate.Disable(true);
            this.dtfExportReturnDate.Text = "保留原数据";
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

    /// <summary>
    /// 点击批量重判级激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_batchModify_Click(object sender, EventArgs e)
    {
        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            msg.Alert("操作", "请至少选择一条检测记录！");
            msg.Show();
        }
        else
        {
            this.windowBatchReCheck.Show();
        }
    }

    /// <summary>
    /// 点击生成台账激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_generateLedger_Click(object sender, EventArgs e)
    {
        if (CheckboxSelectionModelCenterMaster.SelectedRows.Count == 0)
        {
            msg.Alert("操作", "请至少选择一条检测记录！");
            msg.Show();
        }
        else
        {
            this.dtfGenerateLedgerHandleDate.Text = "保留原数据";
            this.dtfGenerateLedgerReturnDate.Text = "保留原数据";
            this.txtGenerateLedgerHandlerId.Value = "";
            this.trfGenerateLedgerHandlerName.Text = "保留原数据";
            this.cbxGenerateLedgerHandleMethod.Text = "保留原数据";
            this.cbxChangeGenerateInfo.Checked = false;
            this.dtfGenerateLedgerHandleDate.Disable(true);
            this.dtfGenerateLedgerReturnDate.Disable(true);
            this.trfGenerateLedgerHandlerName.Disable(true);
            this.cbxGenerateLedgerHandleMethod.Disable(true);
            this.windowGenerateLedger.Hidden = false;
            this.windowGenerateLedger.Show();
        }
    }

    /// <summary>
    /// 点击批量重判级中保存按钮激发的事件
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
                QmcCheckData data = checkDataManager.GetById(row.RecordID);
                EntityArrayList<QmcLedger> ledgerList = ledgerManager.GetListByWhere((QmcLedger._.CheckId == data.CheckId) && (QmcLedger._.DeleteFlag == "0"));
                if (data != null)
                {
                    data.CheckResult = cbxBatchModifyResult.Value.ToString();
                    checkDataManager.Update(data);
                    batchCount++;
                    this.AppendWebLog("重判级", "检测ID：" + data.CheckId);
                    //同时修改对应的台账
                    if(ledgerList.Count > 0)
                    {
                        foreach (QmcLedger ledger in ledgerList)
                        {
                            if (data.CheckResult == "0")
                            {
                                ledger.CheckResult = "2";
                            }
                            else if (data.CheckResult == "1")
                            {
                                ledger.CheckResult = "1";
                            }
                            ledgerManager.Update(ledger);
                            this.AppendWebLog("重判级", "台账ID：" + ledger.LedgerId);
                        }
                    }
                }
            }
            this.AppendWebLog("批量重新判级", "条目数：" + batchCount);
            InitData();
            msg.Alert("操作", "对" + batchCount + "条检测记录进行重判级！判级结果：" + cbxBatchModifyResult.SelectedItem.Text);
            msg.Show();
            this.windowBatchReCheck.Close();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击生成台账中保存按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnGenerateLedgerSave_Click(object sender, DirectEventArgs e)
    {
        int count = 0;
        count = GenerateLedger("generate");
        msg.Alert("操作", "已生成" + count + "条电子台账！");
        msg.Show();
        this.windowGenerateLedger.Hidden = true;
        this.windowGenerateLedger.Close();
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.windowBatchReCheck.Close();
        this.windowExportSetting.Close();
        this.windowGenerateLedger.Close();
    }
    #endregion
}