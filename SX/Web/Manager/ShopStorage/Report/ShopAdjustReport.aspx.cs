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

public partial class Manager_ShopStorage_Report_ShopAdjustReport : System.Web.UI.Page
{
    protected BasUserManager userManager = new BasUserManager();
    protected PstShopAdjustManager shopAdjustManager = new PstShopAdjustManager();

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxChejian.Value = "all";
            cbxOutIn.Value = "all";
        }
    }

    #region 分页相关方法
    private PageResult<PstShopAdjust> GetPageResultData(PageResult<PstShopAdjust> pageParams)
    {
        PstShopAdjustManager.QueryParams queryParams = new PstShopAdjustManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.workShopCode = cbxChejian.SelectedItem.Value;
        queryParams.storageID = hiddenStorageID.Text;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.AdjustType = cbxOutIn.SelectedItem.Value;
        queryParams.deleteFlag = "0";

        return shopAdjustManager.GetTablePageTotalBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PstShopAdjust> pageParams = new PageResult<PstShopAdjust>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<PstShopAdjust> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PstShopAdjust> pageParams = new PageResult<PstShopAdjust>();
        pageParams.PageSize = -100;
        PageResult<PstShopAdjust> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "车间原材料调拨报表");
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string storageID = e.Parameters["StorageID"];
        string storagePlaceID = e.Parameters["StoragePlaceID"];
        string materCode = e.Parameters["MaterCode"];
        string adjustType = e.Parameters["AdjustType"];

        if (adjustType == "I")
            ToStorageName1.Text = "源库房";
        else
            ToStorageName1.Text = "目的库房";

        this.storeDetail.DataSource = shopAdjustManager.GetByInfo(cbxChejian.SelectedItem.Value, Convert.ToDateTime(txtBeginTime.Text), Convert.ToDateTime(txtEndTime.Text), storageID, storagePlaceID, hiddenMaterCode.Text, adjustType);
        this.storeDetail.DataBind();
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
