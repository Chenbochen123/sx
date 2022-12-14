using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using System.Data;
public partial class Manager_ShopStorage_RubberSplitReset : Mesnac.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected PstMaterialRubberSplitManager splitManager = new PstMaterialRubberSplitManager();
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            解锁 = new SysPageAction() { ActionID = 2, ActionName = "Button1" };

        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 解锁 { get; private set; } //必须为 public
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            cbxChejian.Value = "all";
            cbxShiftID.Value = "all";
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    private PageResult<PstMaterialRubberSplit> GetPageResultData(PageResult<PstMaterialRubberSplit> pageParams)
    {
        PstMaterialRubberSplitManager.QueryParams queryParams = new PstMaterialRubberSplitManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.storagePlaceID = hiddenStoragePlaceID.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.chejianCode = cbxChejian.SelectedItem.Value;
        queryParams.barcode = txtBarcode.Text;
        queryParams.shiftID = cbxShiftID.SelectedItem.Value;
        queryParams.operPerson = hiddenOperPerson.Text;
        queryParams.shelfid = this.txtshelf.Text;
        return splitManager.GetTableSplitReset(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstMaterialRubberSplit> pageParams = new PageResult<PstMaterialRubberSplit>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        PageResult<PstMaterialRubberSplit> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];
        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
        hiddenBarcodeSplit.Text = e.ExtraParams["BarcodeSplit"];
    }

    protected void Search_Change(object sender, EventArgs e)
    {
        this.pageToolBar.DoRefresh();
    }

    protected void btnUnLock_Click(object sender, DirectEventArgs e)
    {
        if (string.IsNullOrEmpty(hiddenBarcodeSplit.Text))
        {
            X.Msg.Alert("错误提示", "请选择一个条码号").Show();
        }
        DataTable dt = splitManager.ProcUnReset(hiddenBarcodeSplit.Text.Trim(),this.UserID).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.pageToolBar.DoRefresh();
                X.Msg.Alert("提示", "解锁成功！").Show();
            }
            else
                X.Msg.Alert("提示", dt.Rows[0][0].ToString()).Show();
        }
        else
        {
            X.Msg.Alert("提示", "解锁失败！").Show();
        }

    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PstMaterialRubberSplit> pageParams = new PageResult<PstMaterialRubberSplit>();
        pageParams.PageSize = -100;
        PageResult<PstMaterialRubberSplit> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "扒胶车架报表");
    }
}