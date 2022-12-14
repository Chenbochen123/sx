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

public partial class Manager_ShopStorage_ShopDayStore : Mesnac.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
    }
    #endregion

    private BasMaterialManager materManager = new BasMaterialManager();
    //private BasEquipManager equipManager = new BasEquipManager();
    //private BasUserManager userManager = new BasUserManager();
    private PstMaterShopStoreManager shopStoreManager = new PstMaterShopStoreManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtPlanDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 分页相关方法
    private PageResult<PstMaterShopStore> GetPageResultData(PageResult<PstMaterShopStore> pageParams)
    {
        PstMaterShopStoreManager.QueryParams queryParams = new PstMaterShopStoreManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.planDate = Convert.ToDateTime(txtPlanDate.Text);
        queryParams.workShopCode = cbxWorkShopCode.SelectedItem.Value;
        queryParams.shiftID = cbxShiftID.SelectedItem.Value;
        if (cbxMaterialype.SelectedItem.Value != "all")
            queryParams.minorType = cbxMaterialype.SelectedItem.Text;

        return shopStoreManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterShopStore> pageParams = new PageResult<PstMaterShopStore>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "MaterCode ASC";

        PageResult<PstMaterShopStore> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 操作
    [Ext.Net.DirectMethod()]
    public string btnAudit_Click()
    {
        string result = shopStoreManager.UpdateAudit(Convert.ToDateTime(txtPlanDate.Text).ToShortDateString());
        pageToolBar.DoRefresh();

        return result;
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelAudit_Click()
    {
        string result = shopStoreManager.UpdateCancelAudit(Convert.ToDateTime(txtPlanDate.Text).ToShortDateString());
        pageToolBar.DoRefresh();

        return result;
    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string FID, string DiffWeight)
    {
        //PstMaterShopStore shopStore = shopStoreManager.GetListByWhere(PstMaterShopStore._.PlanDate == Convert.ToDateTime(PlanDate.Substring(1, 10)) && PstMaterShopStore._.MaterCode == MaterCode)[0];
        //txtPlanDate2.Text = shopStore.PlanDate.ToShortDateString();
        //hiddenMaterCode.Text = shopStore.MaterCode;
        //txtMaterName2.Text = materManager.GetMaterName(shopStore.MaterCode);
        //txtStoreWeight2.Text = shopStore.StoreWeight.ToString();
        //txtCheckWeight2.Text = shopStore.CheckWeight.ToString();

        PstMaterShopStore shopStore = shopStoreManager.GetById(Convert.ToInt32(FID));

        string IsAllowedAdd = shopStoreManager.DataAllowAddJZ(shopStore.PlanDate.ToString(), shopStore.WorkShopCode.ToString(), shopStore.ShiftID, shopStore.MaterCode, "0");
        if (IsAllowedAdd != "OK")
        {
            X.Msg.Alert("提示", IsAllowedAdd).Show();
            return;
        }

        shopStore.DiffWeight = Convert.ToDecimal(DiffWeight);
        shopStore.CheckWeight = shopStore.LastWeight + shopStore.InWeight - shopStore.OutWeight + shopStore.DiffWeight;

        shopStoreManager.Update(shopStore);

        pageToolBar.DoRefresh();
    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        PstMaterShopStore shopStore = shopStoreManager.GetListByWhere(PstMaterShopStore._.PlanDate == txtPlanDate2.Text && PstMaterShopStore._.MaterCode == hiddenMaterCode.Text)[0];
        shopStore.CheckWeight = Convert.ToDecimal(txtCheckWeight2.Text);
        shopStore.DiffWeight = shopStore.CheckWeight - shopStore.StoreWeight;

        shopStoreManager.Update(shopStore);
        hiddenMaterCode.Text = string.Empty;
        pageToolBar.DoRefresh();
        this.winModify.Close();
        X.MessageBox.Alert("操作", "更新成功！").Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.winModify.Close();
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PstMaterShopStore> pageParams = new PageResult<PstMaterShopStore>();
        pageParams.PageSize = -100;
        PageResult<PstMaterShopStore> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "车间每班结转");
    }

    #endregion

    #region 校验方法
    protected void CheckField(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        //string storageName = field.Text;
        //EntityArrayList<BasStorage> storageList = manager.GetListByWhere(BasStorage._.StorageName == storageName && BasStorage._.DeleteFlag == "0");
        //if (storageList.Count == 0)
        //{
        //    e.Success = true;
        //}
        //else
        //{
        //    if (storageList[0].StorageName.ToString() == hiddenStorageName.Text)
        //    {
        //        e.Success = true;
        //    }
        //    else
        //    {
        //        e.Success = false;
        //        e.ErrorMessage = "此库房名称已被使用！";
        //    }
        //}
    }
    #endregion
}