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

public partial class Manager_ShopStorage_Report_RubberSplitReport : System.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected PstMaterialRubberSplitManager splitManager = new PstMaterialRubberSplitManager();

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

    #region 分页相关方法
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
        queryParams.shelfid = this.txtshelf.Text.Trim();
        return splitManager.GetTablePageTotalBySqlPrint(queryParams);
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

        this.storeDetail.DataSource = splitManager.GetByInfo("", "", "", "", ""); ;
        this.storeDetail.DataBind();

        int total = lst.RecordCount;
        return new { data, total };
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "扒胶统计报表");
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string barcode = e.Parameters["Barcode"];
        string storageID = e.Parameters["StorageID"];
        string storagePlaceID = e.Parameters["StoragePlaceID"];
        string operPerson = e.Parameters["OperPerson"];
        string operDate = e.Parameters["OperDate"];

        this.storeDetail.DataSource = splitManager.GetByInfo(barcode, storageID, storagePlaceID, operPerson, operDate);
        this.storeDetail.DataBind();
    }

    protected void Search_Change(object sender, EventArgs e)
    {
        this.pageToolBar.DoRefresh();
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

    #endregion
}