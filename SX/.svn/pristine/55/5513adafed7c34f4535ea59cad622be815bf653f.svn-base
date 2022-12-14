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
using System.Xml;
public partial class Manager_Rubber_RubberOverValidQuery : Mesnac.Web.UI.Page
{
    protected PpmRubberStorageManager planManager = new PpmRubberStorageManager();
    protected PpmRubberStorageDetailManager rubberStorageDetailManager = new PpmRubberStorageDetailManager();
    protected PpmRubberStorageDealManager dealManager = new PpmRubberStorageDealManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

    }
    protected void btn_deal_Click(object sender, EventArgs e)
    {
       
        RowSelectionModel sm = this.pnlList.GetSelectionModel() as RowSelectionModel;
        if (sm.SelectedRows.Count == 0)
        {
            X.MessageBox.Alert("操作", "请至少选择一个条码!").Show();

        }
        else
        {
            this.dealno.Text = sm.SelectedRows.Count.ToString();
            this.dealdate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            this.dealremark.Text = "";
            this.winAdd.Show();
        }

    }
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            string dealdate = Convert.ToDateTime(this.dealdate.Value.ToString()).ToString("yyyy-MM-dd");
            string dealremark = this.dealremark.Text.Trim();
            string json = e.ExtraParams["Values"];
            XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + json + "}}");
            foreach (XmlNode row in xml.SelectNodes("records/record"))
            {
                string code = row.SelectSingleNode("条码").InnerXml;
                string storageid = row.SelectSingleNode("StorageID").InnerXml;
                string storageplaceid = row.SelectSingleNode("StoragePlaceID").InnerXml;
                if (code != "")
                {
                    dealManager.SubmitOutDateDeal(code, storageid, storageplaceid, "正常使用", dealdate, dealremark, this.UserID);
                }
            }
            this.store.Reload();
            X.MessageBox.Alert("操作", "已提交超期处理，审核后方可生效！").Show();
            winAdd.Close();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("错误", ex.Message).Show();
        }
    }
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
    }
    private PageResult<PpmRubberStorage> GetPageResultData(PageResult<PpmRubberStorage> pageParams)
    {
        //首先判断是否最少包含一个分组,目前允许
        //if (!cbxByPlanDate.Checked && !cbxByShift.Checked && !cbxByClass.Checked && !cbxByEquip.Checked && !cbxByMaterCode.Checked)
        //{
        //    X.Msg.Alert("提示", "至少要有一个分组！").Show();
        //    return;
        //}
        PpmRubberStorageManager.QueryParams queryParams = new PpmRubberStorageManager.QueryParams();
        queryParams.pageParams = pageParams;
        return planManager.ProcPPMOutDateQuery(queryParams, Convert.ToDateTime(this.txtBeginTime.Text.ToString()).ToString("yyyy-MM-dd"), Convert.ToDateTime(this.txtEndTime.Value.ToString()).ToString("yyyy-MM-dd"), string.IsNullOrEmpty(cbxChejian.SelectedItem.Value) ? "" : cbxChejian.SelectedItem.Value, this.hiddenStorageID.Text.Trim(), this.hiddenStoragePlaceID.Text.Trim(), this.txtLimit.Text == "" ? 24 : Convert.ToInt32(this.txtLimit.Text.Trim()), this.txtBarcode.Text, this.txtShlefBarCode.Text, 0, 0,this.hiddenMaterCode.Text.Trim());
    }
    private DataSet GetPageTotalResultData()
    {
        return planManager.ProcPPMOutDateTotalQuery("", "", string.IsNullOrEmpty(cbxChejian.SelectedItem.Value) ? "" : cbxChejian.SelectedItem.Value, this.hiddenStorageID.Text.Trim(), this.hiddenStoragePlaceID.Text.Trim(), this.txtLimit.Text == "" ? 24 : Convert.ToInt32(this.txtLimit.Text.Trim()), this.txtBarcode.Text, this.txtShlefBarCode.Text);
    }
    [DirectMethod]
    public object GridTotalPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        DataSet lst = GetPageTotalResultData();
        DataTable data = lst.Tables[0];
        int total = data.Rows.Count;
        return new { data, total };
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmRubberStorage> pageParams = new PageResult<PpmRubberStorage>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        PageResult<PpmRubberStorage> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string barcode = e.Parameters["Barcode"];
        string storageID = e.Parameters["StorageID"];
        string storagePlaceID = e.Parameters["StoragePlaceID"];

        this.storeDetail.DataSource = rubberStorageDetailManager.GetByInfo(barcode, storageID, storagePlaceID);
        this.storeDetail.DataBind();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PpmRubberStorage> pageParams = new PageResult<PpmRubberStorage>();
        pageParams.PageSize = -100;
        PageResult<PpmRubberStorage> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "胶料预警报表");
    }
}