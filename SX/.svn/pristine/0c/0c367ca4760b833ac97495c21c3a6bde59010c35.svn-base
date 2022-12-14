using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using System.Text;

public partial class Manager_Rubber_ReturnRubber : Mesnac.Web.UI.Page
{
    protected PpmReturnRubberManager returnRubberManager = new PpmReturnRubberManager();
    protected BasEquipManager basEquipManager = new BasEquipManager();
    protected BasMaterialManager basMaterialManager = new BasMaterialManager();
    protected BasStorageManager basStorageManager = new BasStorageManager();
    protected BasStoragePlaceManager basStoragePlaceManager = new BasStoragePlaceManager();
    protected PptShiftTimeManager shiftTimeManager = new PptShiftTimeManager();
    protected BasEquipFacManager facManager = new BasEquipFacManager();
    protected SysUserCtrlManager userCtrlManager = new SysUserCtrlManager();
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };


            返回胶审核 = new SysPageAction() { ActionID = 4, ActionName = "Button1" };


            撤销返回 = new SysPageAction() { ActionID = 7, ActionName = "Button2" };
            导出 = new SysPageAction() { ActionID = 8, ActionName = "btnExport" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public

        public SysPageAction 返回胶审核 { get; private set; } //必须为 public

        public SysPageAction 撤销返回 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            #region 绑定设备类型
            Ext.Net.ListItem allitem = new Ext.Net.ListItem("全部", "");
            BasDeptManager bm = new BasDeptManager();
            EntityArrayList<BasDept> lstBasEquipFac = bm.GetListByWhere(BasDept._.Remark == "1");
            cbxEquip.Items.Clear();
            cbxEquip.Items.Add(allitem);
            foreach (BasDept m in lstBasEquipFac)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.DepCode.ToString();
                item.Text = m.DepName;
                cbxEquip.Items.Add(item);
            }
            cbxEquip.Text = "全部";
            #endregion
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxShiftClass.Value = "all";
            cbxChejian.Value = "all";
        }
    }

    private PageResult<PpmReturnRubber> GetPageResultData(PageResult<PpmReturnRubber> pageParams)
    {
        PpmReturnRubberManager.QueryParams queryParams = new PpmReturnRubberManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = txtBarcode.Text;
        queryParams.equipCode = cbxEquip.SelectedItem.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.stockInFlag = cbxInStock.SelectedItem.Value;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.shiftClassID = cbxShiftClass.SelectedItem.Value;
        queryParams.chejianCode = cbxChejian.SelectedItem.Value;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<PpmReturnRubber> GetTablePageDataBySql(PpmReturnRubberManager.QueryParams queryParams)
    {
        PageResult<PpmReturnRubber> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" select A.Barcode, A.ShelfBarcode, A.BarcodeStart, A.BarcodeEnd, A.PlanDate, A.EquipCode, B.EquipName, A.ShiftID, C.ShiftName,
                                    A.ShiftClassID, D.ClassName, A.MaterCode, E.MaterialName, A.ReturnWeight, A.RealWeight, A.OperDate,
                                    A.BackFlag, A.CustCode, F.DepName,F2.DepName as DepName2, A.OperPerson, G.UserName, A.ReturnReason, A.prodDate, A.StockInSign,
                                    A.StockNo, A.Mem_Note, A.ValidDate, I.UserName ScanPerson, '' as StorageName, '' as StoragePlaceName, A.MadeLine, L.WorkShopName
                                from PpmReturnRubber A
                                    left join BasEquip B on A.EquipCode = B.EquipCode
                                    left join PptShift C on A.ShiftID = C.ObjID
                                    left join PptClass D on A.ShiftClassID = D.ObjID
                                    left join BasMaterial E on A.MaterCode = E.MaterialCode
                                    left join BasDept F on A.dep_Code = F.DepCode
 left join BasDept F2 on A.Cust_Name = F2.DepCode
                                    left join BasUser G on A.ZJSID = G.HRCode
                                    left join BasUser I on A.OperPerson = I.WorkBarcode
                                    left join BasWorkShop L on A.WorkShopCode = L.ObjID
                                where 1 = 1 and A.DeleteFlag = '0'");
        if (!string.IsNullOrEmpty(queryParams.barcode))
        {
            sqlstr.AppendLine(" AND A.Barcode = '" + queryParams.barcode + "'");
        }
        if (queryParams.equipCode != "全部")
        {
            sqlstr.AppendLine(" AND F2.DepName = '" + queryParams.equipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materCode))
        {
            sqlstr.AppendLine(" AND A.MaterCode = '" + queryParams.materCode + "'");
        }

        if (queryParams.beginDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.PlanDate >=convert(datetime, '" + queryParams.beginDate.ToString() + "', 120)");

        if (queryParams.endDate != DateTime.MinValue)
            sqlstr.AppendLine(" AND A.PlanDate <= convert(datetime, '" + queryParams.endDate.ToString() + "', 120)");

        if (queryParams.shiftClassID != "all")
            sqlstr.AppendLine(" AND A.ShiftClassID = '" + queryParams.shiftClassID + "'");


     
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = returnRubberManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return returnRubberManager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmReturnRubber> pageParams = new PageResult<PpmReturnRubber>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "OperDate desc";

        PageResult<PpmReturnRubber> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void btnSetToStorage_Click(object sender, DirectEventArgs e)
    {
        if (this.rowSelectMuti.SelectedRows.Count == 0)
        {
            X.MessageBox.Alert("提示", "您没有选择任何项，请选择!").Show();
            return;
        }

        if (hiddenStockFlag.Text == "1")
        {
            X.MessageBox.Alert("提示", "您选择的项已入库，请检查！").Show();
            return;
        }

        this.winStorage.Show();
    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
        hiddenStockFlag.Text = e.ExtraParams["StockInSign"];
        hiddenCheckBarcode.Text = e.ExtraParams["Barcode"];
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {

        //X.Js.Alert(cbxChejian.SelectedItem.Value); return;
        PageResult<PpmReturnRubber> pageParams = new PageResult<PpmReturnRubber>();
        pageParams.PageSize = -100;
        PageResult<PpmReturnRubber> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "返回胶报表");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.winAdd.Show();
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string barcode)
    {
        EntityArrayList<PpmReturnRubber> list = returnRubberManager.GetListByWhere(PpmReturnRubber._.Barcode == barcode);
        if (list.Count > 1)
        {
            X.Msg.Alert("提示", "无法编辑：存在多个条码，请检查！").Show();
            return;
        }
        PpmReturnRubber returnRubber = list[0];
        txtBarcode2.Text = returnRubber.Barcode;
        EntityArrayList<BasEquip> equipList = basEquipManager.GetListByWhere(BasEquip._.EquipCode == returnRubber.EquipCode);
        if (equipList.Count > 0)
            txtEquipCode2.Text = basEquipManager.GetListByWhere(BasEquip._.EquipCode == returnRubber.EquipCode)[0].EquipName;
        else
            txtEquipCode2.Text = "无此机台信息！";
        hiddenEquipCode.Text = returnRubber.EquipCode;
        txtBarcodeStart2.Text = returnRubber.BarcodeStart.ToString();
        txtBarcodeEnd2.Text = returnRubber.BarcodeEnd.ToString();
        cbxShiftID2.Value = returnRubber.ShiftID;
        cbxShiftClassID2.Value = returnRubber.ShiftClassID;
        txtMaterName2.Text = basMaterialManager.GetMaterName(returnRubber.MaterCode);
        hiddenMaterCode.Text = returnRubber.MaterCode;
        txtPlanDate2.Text = returnRubber.PlanDate.ToString("yyyy-MM-dd");
        txtRealWeight2.Text = returnRubber.RealWeight.ToString();
        txtReturnWeight2.Text = returnRubber.ReturnWeight.ToString();
        string storageID = string.Empty;
        string storagePlaceID = string.Empty;
        if (!string.IsNullOrEmpty(returnRubber.CustCode))
        {
            string[] storageArr = returnRubber.CustCode.Split('/');
            storageID = storageArr[0];
            storagePlaceID = storageArr[1];
        }
        txtStorageName2.Text = basStorageManager.GetStorageName(storageID);
        hiddenStorageID.Text = storageID;
        txtStoragePlaceName2.Text = basStoragePlaceManager.GetStoragePlaceName(storageID, storagePlaceID);
        hiddenStoragePlaceID.Text = storagePlaceID;
        txtReturnReason2.Text = returnRubber.ReturnReason;

        this.winModify.Show();
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string barcode)
    {
        try
        {
            EntityArrayList<PpmReturnRubber> list = returnRubberManager.GetListByWhere(PpmReturnRubber._.Barcode == barcode);
            if (list.Count > 1)
            {
                return "false";
            }
            PpmReturnRubber returnRubber = list[0];
            returnRubber.DeleteFlag = "1";
            returnRubberManager.Update(returnRubber);

            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    public string CheckAddControls()
    {
        if (string.IsNullOrEmpty(txtBarcode1.Text))
            return "请输入条码号！";
        if (string.IsNullOrEmpty(txtEquipCode1.Text))
            return "请选择机台！";
        if (string.IsNullOrEmpty(txtBarcodeStart1.Text))
            return "请输入起始车次！";
        if (string.IsNullOrEmpty(txtBarcodeEnd1.Text))
            return "请输入截止车次！";
        if (string.IsNullOrEmpty(txtMaterName1.Text))
            return "请选择物料！";
        if (string.IsNullOrEmpty(txtPlanDate1.Text))
            return "请输入物料生产日期！";
        if (string.IsNullOrEmpty(txtRealWeight1.Text))
            return "请输入实际重量！";
        if (string.IsNullOrEmpty(txtReturnWeight1.Text))
            return "请输入返回重量！";
        if (string.IsNullOrEmpty(txtStorageName1.Text))
            return "请选择库房！";
        if (string.IsNullOrEmpty(txtStoragePlaceName1.Text))
            return "请选择库位！";
        if (string.IsNullOrEmpty(txtReturnReason1.Text))
            return "请输入返回原因！";

        return "OK";
    }

    protected void btnAddSave_Click(object sender, EventArgs e)
    {
        string checkResult = CheckAddControls();
        if (checkResult != "OK")
        {
            X.Msg.Alert("提示", checkResult).Show();
            return;
        }
        EntityArrayList<PpmReturnRubber> returnList = returnRubberManager.GetListByWhere(PpmReturnRubber._.Barcode == txtBarcode1.Text);
        if (returnList.Count > 0)
        {
            X.Msg.Alert("提示", "已存在此条码的信息！").Show();
            return;
        }

        PpmReturnRubber returnRubber = new PpmReturnRubber();
        returnRubber.Barcode = txtBarcode1.Text;
        returnRubber.ShelfBarcode = txtBarcode1.Text;
        returnRubber.BarcodeStart = Convert.ToInt32(txtBarcodeStart1.Text);
        returnRubber.BarcodeEnd = Convert.ToInt32(txtBarcodeEnd1.Text);
        returnRubber.PlanDate = Convert.ToDateTime(txtPlanDate1.Text);
        returnRubber.EquipCode = hiddenEquipCode.Text;
        returnRubber.ShiftID = cbxShiftID1.SelectedItem.Value;
        returnRubber.ShiftClassID = cbxShiftClassID1.SelectedItem.Value;
        returnRubber.MaterCode = hiddenMaterCode.Text;
        returnRubber.ReturnWeight = Convert.ToDecimal(txtReturnWeight1.Text);
        returnRubber.RealWeight = Convert.ToDecimal(txtRealWeight1.Text);
        returnRubber.OperDate = DateTime.Now;
        returnRubber.BackFlag = "0";
        returnRubber.CustCode = hiddenStorageID.Text + "/" + hiddenStoragePlaceID.Text;
        returnRubber.OperPerson = this.UserID;
        returnRubber.ReturnReason = txtReturnReason1.Text;
        returnRubber.StockType = "0";
        returnRubber.ProdDate = Convert.ToDateTime(txtPlanDate1.Text);
        returnRubber.StockInSign = 0;
        returnRubber.StockNo = "XXX";
        int barcodeStart = string.IsNullOrEmpty(txtBarcodeStart1.Text) ? 0 : Convert.ToInt32(txtBarcodeStart1.Text);
        int barcodeEnd = string.IsNullOrEmpty(txtBarcodeEnd1.Text) ? 0 : Convert.ToInt32(txtBarcodeEnd1.Text);
        string memNote = string.Empty;
        for (int i = barcodeStart; i < barcodeEnd + 1; i++)
        {
            memNote += i.ToString() + ",";
        }
        returnRubber.Mem_Note = string.IsNullOrEmpty(memNote) ? "" : memNote.Substring(0, memNote.Length - 1);
        returnRubber.PrintDate = DateTime.Now;
        returnRubber.ValidDate = Convert.ToDateTime(txtPlanDate1.Text).AddDays(15);
        returnRubber.DeleteFlag = "0";

        returnRubberManager.Insert(returnRubber);

        hiddenEquipCode.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        hiddenStorageID.Text = string.Empty;
        hiddenStoragePlaceID.Text = string.Empty;
        pageToolBar.DoRefresh();
        this.winAdd.Close();
        X.MessageBox.Alert("操作", "添加成功！").Show();
    }

    public string CheckModifyControls()
    {
        if (string.IsNullOrEmpty(txtBarcode2.Text))
            return "请输入条码号！";
        if (string.IsNullOrEmpty(txtEquipCode2.Text))
            return "请选择机台！";
        if (string.IsNullOrEmpty(txtBarcodeStart2.Text))
            return "请输入起始车次！";
        if (string.IsNullOrEmpty(txtBarcodeEnd2.Text))
            return "请输入截止车次！";
        if (string.IsNullOrEmpty(txtMaterName2.Text))
            return "请选择物料！";
        if (string.IsNullOrEmpty(txtPlanDate2.Text))
            return "请输入物料生产日期！";
        if (string.IsNullOrEmpty(txtRealWeight2.Text))
            return "请输入实际重量！";
        if (string.IsNullOrEmpty(txtReturnWeight2.Text))
            return "请输入返回重量！";
        if (string.IsNullOrEmpty(txtStorageName2.Text))
            return "请选择库房！";
        if (string.IsNullOrEmpty(txtStoragePlaceName2.Text))
            return "请选择库位！";
        if (string.IsNullOrEmpty(txtReturnReason2.Text))
            return "请输入返回原因！";

        return "OK";
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        string checkResult = CheckModifyControls();
        if (checkResult != "OK")
        {
            X.Msg.Alert("提示", checkResult).Show();
            return;
        }
        //EntityArrayList<PpmReturnRubber> returnList = returnRubberManager.GetListByWhere(PpmReturnRubber._.Barcode == txtBarcode2.Text);
        //if (returnList.Count > 0)
        //{
        //    X.Msg.Alert("提示", "已存在此条码的信息！").Show();
        //    return;
        //}

        PpmReturnRubber returnRubber = returnRubberManager.GetListByWhere(PpmReturnRubber._.Barcode == txtBarcode2.Text)[0];
        returnRubber.ShelfBarcode = txtBarcode2.Text;
        returnRubber.BarcodeStart = Convert.ToInt32(txtBarcodeStart2.Text);
        returnRubber.BarcodeEnd = Convert.ToInt32(txtBarcodeEnd2.Text);
        returnRubber.PlanDate = Convert.ToDateTime(txtPlanDate2.Text);
        returnRubber.EquipCode = hiddenEquipCode.Text;
        returnRubber.ShiftID = cbxShiftID2.SelectedItem.Value;
        returnRubber.ShiftClassID = cbxShiftClassID2.SelectedItem.Value;
        returnRubber.MaterCode = hiddenMaterCode.Text;
        returnRubber.ReturnWeight = Convert.ToDecimal(txtReturnWeight2.Text);
        returnRubber.RealWeight = Convert.ToDecimal(txtRealWeight2.Text);
        returnRubber.OperDate = DateTime.Now;
        returnRubber.BackFlag = "0";
        returnRubber.CustCode = hiddenStorageID.Text + "/" + hiddenStoragePlaceID.Text;
        returnRubber.OperPerson = this.UserID;
        returnRubber.ReturnReason = txtReturnReason2.Text;
        returnRubber.StockType = "0";
        returnRubber.ProdDate = Convert.ToDateTime(txtPlanDate2.Text);
        returnRubber.StockInSign = 0;
        returnRubber.StockNo = "XXX";
        int barcodeStart = string.IsNullOrEmpty(txtBarcodeStart2.Text) ? 0 : Convert.ToInt32(txtBarcodeStart2.Text);
        int barcodeEnd = string.IsNullOrEmpty(txtBarcodeEnd2.Text) ? 0 : Convert.ToInt32(txtBarcodeEnd2.Text);
        string memNote = string.Empty;
        for (int i = barcodeStart; i < barcodeEnd + 1; i++)
        {
            memNote += i.ToString() + ",";
        }
        returnRubber.Mem_Note = string.IsNullOrEmpty(memNote) ? "" : memNote.Substring(0, memNote.Length - 1);
        returnRubber.PrintDate = DateTime.Now;
        returnRubber.ValidDate = Convert.ToDateTime(txtPlanDate2.Text).AddDays(15);
        returnRubber.DeleteFlag = "0";

        returnRubberManager.Update(returnRubber);

        hiddenEquipCode.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        hiddenStorageID.Text = string.Empty;
        hiddenStoragePlaceID.Text = string.Empty;
        pageToolBar.DoRefresh();
        this.winModify.Close();
        X.MessageBox.Alert("操作", "修改成功！").Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hiddenEquipCode.Text = string.Empty;
        hiddenMaterCode.Text = string.Empty;
        hiddenStorageID.Text = string.Empty;
        hiddenStoragePlaceID.Text = string.Empty;
        this.winModify.Close();
        this.winAdd.Close();
        this.winStorage.Close();
    }

    public void txtBeginTime_change(object sender, EventArgs e)
    {
        if ((txtBeginTime.Text != DateTime.MinValue.ToString()) && (txtEndTime.Text != DateTime.MinValue.ToString()))
        {
            if (Convert.ToDateTime(txtBeginTime.Text) > Convert.ToDateTime(txtEndTime.Text))
            {
                X.MessageBox.Alert("提示", "开始时间不能大于结束时间!").Show();
                return;
            }
        }
        this.pageToolBar.DoRefresh();
    }
    public void txtEndTime_change(object sender, EventArgs e)
    {
        if ((txtBeginTime.Text != DateTime.MinValue.ToString()) && (txtEndTime.Text != DateTime.MinValue.ToString()))
        {
            if (Convert.ToDateTime(txtBeginTime.Text) > Convert.ToDateTime(txtEndTime.Text))
            {
                X.MessageBox.Alert("提示", "开始时间不能大于结束时间!").Show();
                return;
            }
        }
        this.pageToolBar.DoRefresh();
    }
    public void cbxShiftClass_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxInStock_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxChejian_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchReturn_Click()
    {
        string result = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            string barcode = row.RecordID;
            PpmReturnRubber returnRubber = returnRubberManager.GetListByWhere(PpmReturnRubber._.Barcode == barcode)[0];

            string storageID = hiddenToStorageID.Text;
            string storagePlaceID = hiddenToStoragePlaceID.Text;
            if (!string.IsNullOrEmpty(returnRubber.CustCode))
            {
                string[] storageArr = returnRubber.CustCode.Split('/');
                storageID = storageArr[0];
                storagePlaceID = storageArr[1];
            }

            DataSet ds = shiftTimeManager.GetShiftDS("1", "");
            string shiftID = ds.Tables[0].Rows[0][0].ToString();
            string shiftClassID = ds.Tables[0].Rows[0][1].ToString();

            result = returnRubberManager.SubmitReturnRubber(storageID, storagePlaceID, barcode, Convert.ToDecimal(returnRubber.RealWeight), this.UserID, shiftID, shiftClassID);

            if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "1")
            {
                try
                {
                    WebReference.Service ws = new WebReference.Service();
                    ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                    ws.ToSapReturnRubberAdjustInfo(barcode, "0", "0");
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

            pageToolBar.DoRefresh();
        }

        this.winStorage.Close();

        return result;
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelReturn_Click()
    {
        string result = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            string barcode = row.RecordID;
            PpmReturnRubber returnRubber = returnRubberManager.GetListByWhere(PpmReturnRubber._.Barcode == barcode)[0];

            string storageID = string.Empty;
            string storagePlaceID = string.Empty;
            if (!string.IsNullOrEmpty(returnRubber.CustCode))
            {
                string[] storageArr = returnRubber.CustCode.Split('/');
                storageID = storageArr[0];
                storagePlaceID = storageArr[1];
            }

            result = returnRubberManager.CancelReturnRubber(storageID, storagePlaceID, barcode);

            if (userCtrlManager.GetItemCtrl("SapInterfaceCtrl") == "1")
            {
                try
                {
                    WebReference.Service ws = new WebReference.Service();
                    ws.Url = userCtrlManager.GetItemCtrl("WebServiceIP");
                    ws.ToSapReturnRubberAdjustInfo(hiddenCheckBarcode.Text, "1", "0");
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

            pageToolBar.DoRefresh();
        }

        return result;
    }
}