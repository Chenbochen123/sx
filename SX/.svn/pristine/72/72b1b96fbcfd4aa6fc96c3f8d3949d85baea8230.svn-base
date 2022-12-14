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
public partial class Manager_Rubber_RubberInvalid : Mesnac.Web.UI.Page
{
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 2, ActionName = "btnExport" };
            历史查询 = new SysPageAction() { ActionID = 3, ActionName = "Button1" };
            审核 = new SysPageAction() { ActionID = 4, ActionName = "btn_add" };
            延期处理 = new SysPageAction() { ActionID = 3, ActionName = "Button2" };
            删除 = new SysPageAction() { ActionID = 4, ActionName = "Button3" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 审核 { get; private set; } //必须为 public
        public SysPageAction 延期处理 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
    }

    protected PpmRubberStorageManager planManager = new PpmRubberStorageManager();
    protected PpmRubberStorageDealManager dealManager = new PpmRubberStorageDealManager();
    protected BasUserManager uManager = new BasUserManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private PageResult<PpmRubberStorageDeal> GetPageResultData(PageResult<PpmRubberStorageDeal> pageParams)
    {
        PpmRubberStorageDealManager.QueryParams queryParams = new PpmRubberStorageDealManager.QueryParams();
        queryParams.pageParams = pageParams;
        return dealManager.ProcPPMOutDateQueryInvalid(queryParams, string.IsNullOrEmpty(cbxChejian.SelectedItem.Value) ? "" : cbxChejian.SelectedItem.Value, this.hiddenStorageID.Text.Trim(), this.hiddenStoragePlaceID.Text.Trim(), this.txtBarcode.Text, this.txtShlefBarCode.Text, this.hldtype.Value == "" ? 0 : Convert.ToInt32(this.hldtype.Value),this.hiddenMakerPerson.Text.Trim());
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PpmRubberStorageDeal> pageParams = new PageResult<PpmRubberStorageDeal>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        PageResult<PpmRubberStorageDeal> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    protected void btn_delete_Click(object sender, DirectEventArgs e)
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
            string json = e.ExtraParams["Values"];
            XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + json + "}}");
            foreach (XmlNode row in xml.SelectNodes("records/record"))
            {

                string dealId = row.SelectSingleNode("DealId").InnerXml;
                if (dealId != "")
                {
                    dealManager.DeleteByWhere(PpmRubberStorageDeal._.DealId == dealId);
                }


            }
            this.store.Reload();
            X.MessageBox.Alert("操作", "操作成功").Show();
        }
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PpmRubberStorageDeal> pageParams = new PageResult<PpmRubberStorageDeal>();
        pageParams.PageSize = -100;
        PageResult<PpmRubberStorageDeal> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "超期胶料");
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
    public void BtnAdd2Save_Click(object sender, DirectEventArgs e)
    {
        try
        {
            string dealdate = Convert.ToDateTime( this.dealdate.Value.ToString()).ToString("yyyy-MM-dd");
            string dealremark = this.dealremark.Text.Trim();
            string json = e.ExtraParams["Values"];
            XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + json + "}}");
            foreach (XmlNode row in xml.SelectNodes("records/record"))
            {
                string dealId = row.SelectSingleNode("DealId").InnerXml;
                if (dealId != "")
                {
                    dealManager.SubmitOutDateRubberInValid(Convert.ToInt32(dealId),this.UserID,dealdate,dealremark);
                }
            }
            this.store.Reload();
            X.MessageBox.Alert("操作", "处理成功！").Show();
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
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        string type = this.hldtype.Value.ToString();
        if (type == "1")
        {
            X.MessageBox.Alert("操作", "请先点击查询!").Show();
            return;
        }
        string json = e.ExtraParams["Values"];
        XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + json + "}}");
        RowSelectionModel sm = this.pnlList.GetSelectionModel() as RowSelectionModel;
        if (sm.SelectedRows.Count == 0)
        {
            X.MessageBox.Alert("操作", "请至少选择一个条码!").Show();

        }
        else
        {
            foreach (XmlNode row in xml.SelectNodes("records/record"))
            {

                string dealId = row.SelectSingleNode("DealId").InnerXml;
                if (dealId != "")
                {
                    dealManager.SubmitRubberInValid(Convert.ToInt32(dealId), this.UserID);
                }
               
                
            }
            this.store.Reload();
            X.MessageBox.Alert("操作", "操作成功").Show();
        }

    }
}