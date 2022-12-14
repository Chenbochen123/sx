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

public partial class Manager_Rubber_RubberInventory : Mesnac.Web.UI.Page
{
    protected PpmInventoryManager inventoryManager = new PpmInventoryManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM" + "-01");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxChejian.Value = "008";
            cbxIsHaveSulf.Value = "0";
            cbxProfitLoss.Value = "all";

            EndDateBind();
        }
    }

    public void EndDateBind()
    {
        #region 绑定盘点日期
        DataSet ds = inventoryManager.GetInverntoryEndDate(Convert.ToDateTime(txtBeginTime.Text), Convert.ToDateTime(txtEndTime.Text), cbxChejian.Value.ToString());
        cbxEndDate.Items.Clear();
        for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Value = ds.Tables[0].Rows[i]["EndDateValue"].ToString();
            item.Text = ds.Tables[0].Rows[i]["EndDateText"].ToString();
            cbxEndDate.Items.Add(item);
        }
        if (ds.Tables[0].Rows.Count>0)
            cbxEndDate.Text = ds.Tables[0].Rows[0][0].ToString();
        #endregion
    }

    private PageResult<PpmInventory> GetPageResultData(PageResult<PpmInventory> pageParams)
    {
        PpmInventoryManager.QueryParams queryParams = new PpmInventoryManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.chejianCode = cbxChejian.Value.ToString();
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.inventoryEndDate = Convert.ToDateTime(cbxEndDate.SelectedItem.Text);
        queryParams.isHaveSulf = cbxIsHaveSulf.Value.ToString();
        queryParams.profitLoss = cbxProfitLoss.Value.ToString();        

        return inventoryManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmInventory> pageParams = new PageResult<PpmInventory>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<PpmInventory> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
        hiddenCheckBarcode.Text = e.ExtraParams["Barcode"];
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PpmInventory> pageParams = new PageResult<PpmInventory>();
        pageParams.PageSize = -100;
        PageResult<PpmInventory> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "盘点数据");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.winAdd.Show();
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string barcode)
    {
        EntityArrayList<PpmInventory> list = inventoryManager.GetListByWhere(PpmInventory._.Barcode == barcode);
        if (list.Count > 1)
        {
            X.Msg.Alert("提示", "无法编辑：存在多个条码，请检查！").Show();
            return;
        }
        PpmInventory inventory = list[0];
        txtBarcode2.Text = inventory.Barcode;

        this.winModify.Show();
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string barcode)
    {
        try
        {
            EntityArrayList<PpmInventory> list = inventoryManager.GetListByWhere(PpmInventory._.Barcode == barcode);
            if (list.Count > 1)
            {
                return "false";
            }
            PpmInventory inventory = list[0];
            inventoryManager.Update(inventory);

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
        EntityArrayList<PpmInventory> returnList = inventoryManager.GetListByWhere(PpmInventory._.Barcode == txtBarcode1.Text);
        if (returnList.Count > 0)
        {
            X.Msg.Alert("提示", "已存在此条码的信息！").Show();
            return;
        }

        PpmInventory inventory = new PpmInventory();
        inventory.Barcode = txtBarcode1.Text;

        inventoryManager.Insert(inventory);

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
        //EntityArrayList<PpmInventory> returnList = inventoryManager.GetListByWhere(PpmInventory._.Barcode == txtBarcode2.Text);
        //if (returnList.Count > 0)
        //{
        //    X.Msg.Alert("提示", "已存在此条码的信息！").Show();
        //    return;
        //}

        PpmInventory inventory = inventoryManager.GetListByWhere(PpmInventory._.Barcode == txtBarcode2.Text)[0];

        inventoryManager.Update(inventory);

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
        EndDateBind();
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
        EndDateBind();
    }
    public void cbxChejian_change(object sender, EventArgs e)
    {
        EndDateBind();
    }
    public void cbxIsHaveSulf_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxEndDate_change(object sender, EventArgs e)
    {
        
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchReturn_Click()
    {
        string result = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            string barcode = row.RecordID;
            PpmInventory inventory = inventoryManager.GetListByWhere(PpmInventory._.Barcode == barcode)[0];

            string storageID = hiddenToStorageID.Text;
            string storagePlaceID = hiddenToStoragePlaceID.Text;

            pageToolBar.DoRefresh();
        }

        return result;
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelReturn_Click()
    {
        string result = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            string barcode = row.RecordID;
            PpmInventory inventory = inventoryManager.GetListByWhere(PpmInventory._.Barcode == barcode)[0];

            string storageID = string.Empty;
            string storagePlaceID = string.Empty;

            pageToolBar.DoRefresh();
        }

        return result;
    }
}