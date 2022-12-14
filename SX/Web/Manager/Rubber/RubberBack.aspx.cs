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

public partial class Manager_Rubber_RubberBack : Mesnac.Web.UI.Page
{
    protected PpmSemiStorageManager semiStorageManager = new PpmSemiStorageManager();
    protected PpmRubberBackReasonManager backReasonManager = new PpmRubberBackReasonManager();
    protected PptShiftTimeManager shiftTimeManager = new PptShiftTimeManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxShiftClass.Value = "all";
            cbxBackType.Value = "1";
            cbxStorage.SelectedItem.Index = 0;

            bindBackReason();
        }
    }

    private void bindBackReason()
    {
        this.cbxBackReason.ClearValue();
        EntityArrayList<PpmRubberBackReason> list = backReasonManager.GetAllList();
        this.storeBackReason.DataSource = list;
        this.storeBackReason.DataBind();
        this.cbxBackReason.SelectedItem.Index = 0;
    }

    private PageResult<PpmSemiStorage> GetPageResultData(PageResult<PpmSemiStorage> pageParams)
    {
        PpmSemiStorageManager.QueryParams queryParams = new PpmSemiStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.barcode = txtBarcode.Text;
        queryParams.equipCode = hiddenEquipCode.Text;
        queryParams.materCode = hiddenMaterCode.Text;
        queryParams.backFlag = cbxBackFlag.SelectedItem.Value;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.shiftClassID = cbxShiftClass.SelectedItem.Value;

        return semiStorageManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmSemiStorage> pageParams = new PageResult<PpmSemiStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "RecordDate desc";

        PageResult<PpmSemiStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void btnLockEdit_Click(object sender, EventArgs e)
    {
        if (this.rowSelectMuti.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "您没有选择任何项，请选择！").Show();
            return;
        }
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            string backFlag = hiddenBackFlag.Text;
            if (backFlag == "1")
            {
                X.Msg.Alert("提示", "胶料已经退回！").Show();
                return;
            }
        }

        this.winSet.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.winSet.Close();
    }

    [Ext.Net.DirectMethod()]
    public string btnBatchBack_Click()
    {
        string result = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            string barcode = row.RecordID;

            DataSet ds = shiftTimeManager.GetShiftDS("1", "");
            string shiftID = ds.Tables[0].Rows[0][0].ToString();

            result = semiStorageManager.SubmitRubberBack(cbxStorage.SelectedItem.Value, cbxStorage.SelectedItem.Value + "001", barcode, Convert.ToDecimal(txtBackWeight.Text), cbxBackType.SelectedItem.Value, cbxBackReason.SelectedItem.Value, shiftID, this.UserID);
            pageToolBar.DoRefresh();
        }

        return result;
    }

    [Ext.Net.DirectMethod()]
    public string btnCancelRubberBack_Click()
    {
        string result = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            string backFlag = hiddenBackFlag.Text;
            if (backFlag == "0")
            {
                return "胶料没有进行退回操作，不能撤销！";
            }

            DataSet ds = shiftTimeManager.GetShiftDS("1", "");
            string shiftID = ds.Tables[0].Rows[0][0].ToString();

            string barcode = row.RecordID;

            result = semiStorageManager.CancelRubberBack(barcode, shiftID, this.UserID);
            pageToolBar.DoRefresh();
        }

        return result;
    }

    public void Query_Change(object sender, EventArgs e)
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

    public void cbxBackType_Change(object sender, EventArgs e)
    {
        if (cbxBackType.SelectedItem.Value == "1")
        {
            cbxBackReason.Disabled = true;
            txtBackWeight.Disabled = true;
            cbxStorage.Disabled = true;
            //btnSubmitBack.Disabled = false;
        }
        else
        {
            cbxBackReason.Disabled = false;
            txtBackWeight.Disabled = false;
            cbxStorage.Disabled = false;
            //btnSubmitBack.Disabled = true;
        }
    }

    protected void rowSelectMuti_SelectionChange(object sender, DirectEventArgs e)
    {
        hiddenBarcode.Text = e.ExtraParams["Barcode"];
        hiddenBackFlag.Text = e.ExtraParams["BackFlag"];
        txtBackWeight.Text = e.ExtraParams["RealWeight"];
    }

}