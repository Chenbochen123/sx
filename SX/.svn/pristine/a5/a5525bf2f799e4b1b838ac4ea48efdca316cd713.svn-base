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

public partial class Manager_Storage_MaterialInventory : Mesnac.Web.UI.Page
{
    protected PstMaterialStoreinManager manager = new PstMaterialStoreinManager();
    protected PstMaterialStoreinDetailManager detailManager = new PstMaterialStoreinDetailManager();
    protected PstMaterialChkManager chkManager = new PstMaterialChkManager();
    protected PstMaterialChkDetailManager chkdetailManager = new PstMaterialChkDetailManager();
    protected BasMaterialManager materManager = new BasMaterialManager();
    protected BasUserManager userManager = new BasUserManager();
    protected BasStorageManager storageManager = new BasStorageManager();
    protected PstStorageManager pstStorageManager = new PstStorageManager();
    protected PstStorageDetailManager pstStorageDetailManager = new PstStorageDetailManager();
    protected PstMaterialInventoryManager inventoryManager = new PstMaterialInventoryManager();
    protected PstMaterialInventoryDetailManager inventoryDetailManager = new PstMaterialInventoryDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterialInventory> GetPageResultData(PageResult<PstMaterialInventory> pageParams)
    {
        PstMaterialInventoryManager.QueryParams queryParams = new PstMaterialInventoryManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.billNo = txtBillNo.Text;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Value);
        queryParams.hrCode = hiddenMakerPerson.Text;
        if (cbxInventoryType.SelectedItem.Value != "all")
            queryParams.inventoryType = cbxInventoryType.SelectedItem.Value;

        return inventoryManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialInventory> pageParams = new PageResult<PstMaterialInventory>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate DESC";

        PageResult<PstMaterialInventory> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    private PageResult<PstMaterialChk> GetPageResultData1(PageResult<PstMaterialChk> pageParams)
    {
        PstMaterialChkManager.QueryParams queryParams = new PstMaterialChkManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.stockInFlag = "0";
        queryParams.sendChkFlag = "1";
        queryParams.deleteFlag = "0";
        queryParams.chkResultFlag = "1";

        return chkManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData1(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialChk> pageParams = new PageResult<PstMaterialChk>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "BillNo ASC";

        PageResult<PstMaterialChk> lst = GetPageResultData1(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string billNo = e.Parameters["BillNo"];

        this.storeDetail.DataSource = inventoryDetailManager.GetByBillNo(billNo);
        this.storeDetail.DataBind();
    }
    #endregion

    #region 增删改查按钮激发的事件
    [Ext.Net.DirectMethod()]
    public string btnBatchChk_Click()
    {
        string strBillNo = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            strBillNo += "'" + row.RecordID + "', ";
            EntityArrayList<PstMaterialInventory> inventory = inventoryManager.GetListByWhere(PstMaterialInventory._.BillNo == row.RecordID);
            if (inventory[0].ChkResultFlag == "1")
                return "该单据已经审核";
            //判断是否在库房期间内
            DataSet ds = storageManager.GetDuration(inventory[0].StorageID);
            if (inventory[0].InventoryDate < Convert.ToDateTime(ds.Tables[0].Rows[0][5].ToString()))
            {
                return "false1";
            }
        }

        bool result = inventoryManager.UpdateChkResultFlag(strBillNo.Remove(strBillNo.Length - 2, 1), userManager.UserID);

        if (result == true)
        {
            pageToolBar.DoRefresh();
            return "审核成功！";
        }
        else
        {
            return "审核失败！";
        }
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string billNo)
    {
        try
        {
            PstMaterialInventory inventory = inventoryManager.GetById(billNo);
            inventory.DeleteFlag = "1";

            inventoryManager.Update(inventory);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_filed(string billNo)
    {
        try
        {
        }
        catch (Exception e)
        {
            return "操作失败：" + e;
        }
        return "已归档";
    }
    public void txtBeginTime_change(object sender, EventArgs e)
    {
        this.pageToolBar.DoRefresh();
    }
    public void txtEndTime_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    public void cbxInventoryType_change(object sender, EventArgs e)
    {
        pageToolBar.DoRefresh();
    }
    #endregion

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;

        if (field.Text != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须填写！";
        }
    }

    protected void CheckCombo(object sender, RemoteValidationEventArgs e)
    {
        ComboBox combo = (ComboBox)sender;

        if (combo.SelectedItem.Value != "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此属性必须选择！";
        }
    }
    #endregion
}