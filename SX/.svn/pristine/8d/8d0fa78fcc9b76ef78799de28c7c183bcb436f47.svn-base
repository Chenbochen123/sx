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
public partial class Manager_Rubber_RowOverValidDeal : Mesnac.Web.UI.Page
{
    protected PpmRubberStorageManager planManager = new PpmRubberStorageManager();
    protected PpmRubberStorageDealManager dealManager = new PpmRubberStorageDealManager();
    protected BasUserManager uManager = new BasUserManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框

    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExport" };
            历史查询 = new SysPageAction() { ActionID = 3, ActionName = "Button1" };
            批量处理 = new SysPageAction() { ActionID = 4, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 批量处理 { get; private set; } //必须为 public
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    private PageResult<PpmRubberStorage> GetPageResultData(PageResult<PpmRubberStorage> pageParams)
    {
        PpmRubberStorageManager.QueryParams queryParams = new PpmRubberStorageManager.QueryParams();
        queryParams.pageParams = pageParams;//this.hiddenEquipCode.Text.Trim()
        return planManager.ProcPPMOutDateQueryDeal(queryParams, this.hiddenMaterCode.Value.ToString(), Convert.ToDateTime(this.txtBeginTime.Text.ToString()).ToString("yyyy-MM-dd"), Convert.ToDateTime(this.txtEndTime.Text.ToString()).ToString("yyyy-MM-dd"), string.IsNullOrEmpty(cbxChejian.SelectedItem.Value) ? "" : cbxChejian.SelectedItem.Value, this.hiddenStorageID.Text.Trim(), this.txtBarcode.Text, this.txtBarcode.Text, "", this.hldtype.Value == "" ? 0 : Convert.ToInt32(this.hldtype.Value), "保质期", 0, 10000);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "已超期胶料");
    }
   
    protected void btn_deal_Click(object sender, EventArgs e)
    {
        string type = this.hldtype.Value.ToString();
        if (type == "1")
        {
            X.MessageBox.Alert("操作", "请先查询超期物料!").Show();
            return;
        }
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
            string dealway = this.dealway.Value.ToString();
            string dealdate = Convert.ToDateTime( this.dealdate.Value.ToString()).ToString("yyyy-MM-dd");
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
                    dealManager.SubmitOutDateDeal(code,storageid,storageplaceid,dealway,dealdate,dealremark,this.UserID);
                }
            }
            this.store.Reload();
            X.MessageBox.Alert("操作", "已提交超期处理，审核后方可生效！").Show();
            winAdd.Close();
        }
        catch (Exception ex)
        {
            X.MessageBox.Alert("错误",ex.Message).Show();
        }
    }
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        string barcode = e.Parameters["BarCode"];
        string storageID = e.Parameters["StorageID"];
        string storagePlaceID = e.Parameters["StoragePlaceID"];

        this.storeDetail.DataSource = dealManager.GetDateQueryByCode(barcode, storageID, storagePlaceID);
        this.storeDetail.DataBind();
    }
}