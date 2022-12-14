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

public partial class Manager_ShopStorage_Report_OastReport : System.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected PstMaterialRubberSplitManager splitManager = new PstMaterialRubberSplitManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            cbxChejian.Value = "all";
            //cbxShiftID.Value = "all";
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
        //queryParams.shiftID = cbxShiftID.SelectedItem.Value;
        //queryParams.barcode = txtBarcode.Text;
        //queryParams.operPerson = hiddenOperPerson.Text;
        queryParams.barcode = this.txtCode.Text.Trim();
        return splitManager.GetTablePageOastBySqlPrint(queryParams);
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

        this.storeDetail.DataSource = splitManager.GetByOastInfo("", "", "", txtBeginTime.Text, Convert.ToDateTime(txtEndTime.Text).AddDays(1).ToString(),this.txtCode.Text.Trim());
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "烘胶房统计报表");
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        //string barcode = e.Parameters["Barcode"];
        string storageID = e.Parameters["StorageID"];
        string storagePlaceID = e.Parameters["StoragePlaceID"];
        string matercode = e.Parameters["MaterCode"];
        string barcode = this.txtCode.Text.Trim();
        //splitManager.GetByOastInfo("", "", "", txtBeginTime.Text, Convert.ToDateTime(txtEndTime.Text).AddDays(1).ToString());
        this.storeDetail.DataSource = splitManager.GetByOastInfo(storageID, storagePlaceID, matercode, txtBeginTime.Text, Convert.ToDateTime(txtEndTime.Text).AddDays(1).ToString(),barcode);
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